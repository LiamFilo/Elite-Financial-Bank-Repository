using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BankingCommunication
{
    public class Connection : IDisposable
    {
        private const string SERVER_IP = "127.21.12.4"; // Can make configurable
        private const int SERVER_PORT = 43;            // Can make configurable
        private const int DELAY_IN_MILLISECONDS = 100; // Typo fixed (DELEY -> DELAY)

        public event EventHandler<PacketReceivedEventArgs> GotPacket;

        private Socket _socket;
        private CancellationTokenSource _cancellationTokenSource;

        private NetworkStream _networkStream;
        private NetworkStream NetworkStream
        {
            get => _networkStream;
            set
            {
                _networkStream?.Dispose();
                _networkStream = value;
            }
        }

        public Connection()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        // **Added: ConnectAsync method to initialize the socket and establish connection**
        public async Task ConnectAsync()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                await _socket.ConnectAsync(SERVER_IP, SERVER_PORT); // Establish connection
                NetworkStream = new NetworkStream(_socket, true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error connecting to the server.", ex);
            }
        }

        /// <summary>
        /// Starts listening for incoming messages asynchronously.
        /// </summary>
        public async Task ListenToMessagesAsync()
        {
            if (_socket == null || !(_socket.Connected))
                throw new InvalidOperationException("Socket is not connected. Call ConnectAsync first.");

            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await ProcessIncomingPacketsAsync();
                    await Task.Delay(DELAY_IN_MILLISECONDS);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error in connection listening loop.", ex);
            }
        }

        /// <summary>
        /// Processes incoming packets and triggers the GotPacket event.
        /// </summary>
        private async Task ProcessIncomingPacketsAsync()
        {
            int readableSocketBytes = _socket.Available; // Fixed case (ReadableSocketBytes -> readableSocketBytes)

            if (readableSocketBytes > 0)
            {
                try
                {
                    using (var reader = new StreamReader(NetworkStream, leaveOpen: true)) // Leave stream open
                    {
                        var json = await reader.ReadLineAsync(); // Changed ReadToEndAsync to ReadLineAsync
                        if (string.IsNullOrEmpty(json)) return; // Skip empty or invalid JSON

                        IPacket packet = JsonSerializer.Deserialize<IPacket>(json)
                            ?? throw new NullReferenceException("The BCP packet can't be null");

                        GotPacket?.Invoke(this, new PacketReceivedEventArgs(packet));
                    }
                }
                catch (JsonException jsonEx)
                {
                    throw new InvalidOperationException("JSON deserialization error while processing packet.", jsonEx); // Specific exception for JSON errors
                }
                catch (IOException ioEx)
                {
                    throw new InvalidOperationException("I/O Error while receiving packet.", ioEx);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error while processing packet.", ex);
                }
            }
        }

        /// <summary>
        /// Sends a packet to the server.
        /// </summary>
        public async Task SendPacketAsync(IPacket packetToSend)
        {
            ValidatePacket(packetToSend); // **Reintroduced packet validation**

            try
            {
                using (var writer = new StreamWriter(NetworkStream, leaveOpen: true)) // Leave stream open
                {
                    var json = JsonSerializer.Serialize(packetToSend);
                    await writer.WriteLineAsync(json); // Changed to WriteLineAsync
                    await writer.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error in SendPacketAsync.", ex);
            }
        }

        /// <summary>
        /// Validates the packet data to ensure all required fields are present.
        /// </summary>
        private static void ValidatePacket(IPacket packetToServer) // **Reintroduced validation logic**
        {
            if (packetToServer == null || packetToServer.Command == null || string.IsNullOrEmpty(packetToServer.UserID))
            {
                throw new ArgumentException("Invalid packet data. Sending aborted.");
            }

            if (packetToServer.Command.ExecutionDate < DateTime.Now)
            {
                throw new ArgumentException("ExecutionDate cannot be in the past.");
            }

            if (packetToServer.Command is BankOperationCommand operationCommand && string.IsNullOrWhiteSpace(operationCommand.AccountNumber))
            {
                throw new ArgumentException("AccountNumber is required for bank operations.");
            }
        }

        /// <summary>
        /// Releases unmanaged resources and gracefully shuts down connections.
        /// </summary>
        public void Dispose()
        {
            try
            {
                _cancellationTokenSource.Cancel();
                NetworkStream = null;
                if (_socket != null && _socket.Connected)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.Close();
                }
            }
            catch (ObjectDisposedException ex)
            {
                throw new InvalidOperationException("Resource already disposed.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error in Dispose.", ex);
            }
        }
    }
}
