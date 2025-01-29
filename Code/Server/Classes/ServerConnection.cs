using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankingCommunication;

namespace Server.Classes
{
    public class ServerConnection
    {
        private const int SERVER_PORT = 43;
        private TcpListener _listener;
        private bool _isRunning;
        private CancellationTokenSource _cancellationTokenSource;
        public event EventHandler<PacketReceivedEventArgs> PacketReceived;
        public bool IsRunning { get { return _isRunning; } }

        public ServerConnection()
        {
            _listener = new TcpListener(IPAddress.Any, SERVER_PORT);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Starts the server and listens for incoming client connections.
        /// </summary>
        public async Task StartAsync()
        {
            _isRunning = true;
            _listener.Start();
            Console.WriteLine($"Server started on port {SERVER_PORT}...");

            while (_isRunning && !_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    _ = HandleClientAsync(client);
                    Console.WriteLine("lisening");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Server error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Handles a connected client asynchronously.
        /// </summary>
        private async Task HandleClientAsync(TcpClient client)
        {
            var clientEndPoint = client.Client.RemoteEndPoint.ToString();
            Console.WriteLine($"Client connected: {clientEndPoint}");
           
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
            {
                try
                {
                    while (_isRunning && client.Connected)
                    {
                        string receivedJson = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(receivedJson))
                        {
                            break; // Client disconnected
                        }

                        Console.WriteLine($"Received from {clientEndPoint}: {receivedJson}");

                        // Deserialize packet and process request
                        IPacket receivedPacket = JsonSerializer.Deserialize<IPacket>(receivedJson);
                        IPacket responsePacket = ProcessPacket(receivedPacket);

                        // Send response
                        string responseJson = JsonSerializer.Serialize(responsePacket);
                        await writer.WriteLineAsync(responseJson);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling client {clientEndPoint}: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine($"Client disconnected: {clientEndPoint}");
                    client.Close();
                }
            }
        }

        /// <summary>
        /// Processes received packets and generates appropriate responses.
        /// </summary>
        private IPacket ProcessPacket(IPacket packet)
        {
            // Implement logic to process different types of packets
            Console.WriteLine("Processing packet...");
            return new BCPPacketResponse(new object[] {"success"}, "d");
        }

        /// <summary>
        /// Stops the server and closes all client connections.
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            _cancellationTokenSource.Cancel();
            _listener.Stop();
            Console.WriteLine("Server stopped.");
        }
    }
}
