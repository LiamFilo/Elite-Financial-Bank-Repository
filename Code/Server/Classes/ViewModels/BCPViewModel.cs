using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingCommunication;
using BankingCommunication;
using Server.Classes;
using BankingEntities;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization;

namespace Server {
	public class BCPViewModel 
    {
		private ConnectedUsers ConnectedUsers;
        private List<Connection> _unrecotnizedConnections;
        private TcpListener _listener;
        private Thread _listen2NewClientsThread;
        private bool _keepListening;

        public bool KeepListening { get { return _keepListening; } }

		public BCPViewModel()
		{
            ConnectedUsers = new ConnectedUsers();
            _unrecotnizedConnections = new List<Connection>();
            _listener = new TcpListener(IPAddress.Parse("142.133.11.17"), 8085);
            _keepListening = false;
            _listen2NewClientsThread = new Thread(Listen2NewClients);
            _listen2NewClientsThread.IsBackground = true;
        }

        public void StartListening()
        {
            _listener.Start();
            _keepListening = true;
            _listen2NewClientsThread.Start();//Invoke Thread that listen to connections
        }

        private void Listen2NewClients()
        {
            Console.WriteLine("Start Listening to clients");
            while (_keepListening)
            {
                try
                {
                    //Handle Client Connection
                    Socket socket = _listener.AcceptSocket();//Blocking Operation
                    Connection conn = new Connection(socket);
                    conn.StartListening();
                    Console.WriteLine("Client connected!");
                    _unrecotnizedConnections.Add(conn);
                    conn.GotPacket += PacketReceivedHandler;    
                }
                catch (SerializationException)
                { break; }
                catch (IOException)
                { break; }
            }

        }

        //Handle a message receivied event
        private void PacketReceivedHandler(object? sender, PacketReceivedEventArgs e)
        {
            Console.WriteLine("PacketReceivedHandler");

            IPacket packet = e.packetReceived ?? throw new Exception("Packet Received is null");
            BCPPacketRequest packetFromClient = (BCPPacketRequest)packet;
            ICommand command = packetFromClient.Command;
            string UserID = packetFromClient.UserId;
            Console.WriteLine($"Packet Receivied from type {command.CommandType}");

            //Create the request
            RequestPriority priority = PriorityQueue.SetPriorityQueue(command.CommandType);
            Request rqs = new Request(command, priority, UserID);
        }

		public void SendPacketToClient(BCPPacketResponse packetToSend,string userID)
		{
			try
			{
                Connection connection = this.ConnectedUsers.GetConnection(userID);
				connection.Send(packetToSend);
            }
			catch(Exception ex) { ExceptionHandler.ExceptionHandle(ex); }
        }
	}

}