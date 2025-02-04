using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.ComponentModel.Design;

namespace BankingCommunication
{
    public class Connection : IDisposable
    {
        #region Data-Members
        private Socket _socket;
        private NetworkStream _networkStream;
        private StreamWriter _writer; 
        private Thread _listeningThread;
        private bool _keepListening;
        public event EventHandler<PacketReceivedEventArgs> GotPacket;
        public event EventHandler<EventArgs> CommunicationDisconnect;
        #endregion

        public Connection(Socket socket)
        {
            _socket = socket;
            _networkStream = new NetworkStream(socket);
            _writer = new StreamWriter(_networkStream, System.Text.Encoding.UTF8, 1024, leaveOpen: true);
            _listeningThread = new Thread(Listen2Messages);
            _listeningThread.IsBackground = true;
            _keepListening = socket.Connected;
        }

        public bool IsConnected { get { return _socket != null ? _socket.Connected : false; } }

        public void StartListening()
        {
            _keepListening = true;
            _listeningThread.Start();
        }

        private void Listen2Messages()
        {
            Console.WriteLine("start listening to packets");
            using (var reader = new StreamReader(_networkStream, System.Text.Encoding.UTF8, false, 1024, leaveOpen: true))
            {
                while (_keepListening)
                {
                    Console.WriteLine("Wait for packet...");
                    try
                    {
                        string jsonLine = reader.ReadLine();
                        if (string.IsNullOrEmpty(jsonLine))
                        {
                            Console.WriteLine("string.IsNullOrEmpty(jsonLine)");
                            continue;
                        }
                            

                        IPacket packetReceived = JsonSerializer.Deserialize<BCPPacketRequest>(jsonLine);
                        if (packetReceived != null)
                        {
                            GotPacket?.Invoke(this, new PacketReceivedEventArgs(packetReceived));
                        }
                        else
                        {
                            Console.WriteLine("the packet is null");
                        }
                    }
                    catch (SerializationException ex)
                    {
                        Console.WriteLine("new exception \n " + ex.Message);
                        break;
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("new exception \n " + ex.Message);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("new exception \n" + ex.Message);
                    }
                }
            }
        }

        public void Send(IPacket packetToSend)
        {
            try
            {
                if (packetToSend == null)
                    Console.WriteLine("packet to send is null");

                BCPPacketRequest requestPacket = (BCPPacketRequest)packetToSend;
                

                var options = new JsonSerializerOptions
                {
                    IncludeFields = true
                };
                string jsonString = JsonSerializer.Serialize(requestPacket, options);

                Console.WriteLine("send jason: " + jsonString);
                _writer.WriteLine(jsonString);
                _writer.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception in sending: " + ex.Message);
            }
        }

        public void Dispose()
        {
            Console.WriteLine("מבצע Dispose");
            _keepListening = false;
            _writer?.Dispose();
            _networkStream.Dispose();
            _socket.Dispose();
            CommunicationDisconnect?.Invoke(this, EventArgs.Empty);
        }
    }

}
    
