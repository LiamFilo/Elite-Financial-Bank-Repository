using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingCommunication;
using BankingCommunication;
using Server.Classes;
using BankingEntities;

namespace Server {
	public class BCPViewModel {

		private ConnectedUsers ConnectedUsers;
		private ServerConnection _connection;

		public BCPViewModel()
		{
            ConnectedUsers = new ConnectedUsers();
			_connection = new ServerConnection();
            _connection.PacketReceived += PacketReceivedHandler;
        }

        /// <summary>
        /// The function handle packet received event from the client. It breaks the package down into a command
		/// and user ID and calls the View Model which is responsible for running the commands on the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void PacketReceivedHandler(object? sender, PacketReceivedEventArgs e)
        {
            IPacket packet = e.packetReceived ?? throw new Exception("Packet Received is null");
			BCPPacketRequest packetFromClient = (BCPPacketRequest)packet;
			ICommand command = packetFromClient.Command;
			string UserID = packetFromClient.UserId;
			ServerViewModel.Instance.CommandProcessingVM.HandleCommand(command, UserID);
        }

		public void SendPacketToClient(BCPPacketResponse packetToSend,string userID)
		{
			try
			{
                Connection connection = this.ConnectedUsers.GetConnection(userID);
				connection.SendPacketAsync(packetToSend);
            }
			catch(Exception ex) { ExceptionHandler.ExceptionHandle(ex); }
        }

	}

}