using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BankingCommunication;
using BankingEnumeration;

namespace Client
{
    public class BCPManagerViewModel
    {
        private TcpClient _tcpClient;
        private  Connection _connection;
        private readonly string SERVER_IP = "142.133.11.17";
        private readonly int SERVER_PORT = 8085;
     

        public BCPManagerViewModel()
        {
            _tcpClient = new TcpClient();
        }

        public void ConnectToServerAndStartListening()
        {
            _tcpClient.Connect(SERVER_IP, SERVER_PORT);
            this._connection = new Connection(_tcpClient.Client);//tcpClient.Client is the socket

            //Start Listening to packets from server
            this._connection.StartListening();
            Console.WriteLine("connect to server");
        }

        public void SendPacketToServer(ICommand commandToSend)
        {
            if (commandToSend == null)
            {
                throw new ArgumentNullException(nameof(commandToSend));
            }

            IPacket packetToSend;

            if (commandToSend.CommandType == ClientCommandType.Log_In || commandToSend.CommandType == ClientCommandType.Register)
            {
                packetToSend = new BCPPacketRequest(commandToSend);
            }
            else
            {
                var model = ClientViewModel.Instance.ModelManagemetVM;
                if (model.CurrentUser == null)
                {
                    throw new InvalidOperationException("User must be logged in to perform this action.");
                }
                packetToSend = new BCPPacketRequest(commandToSend, model.CurrentUser.Id);
            }

            Console.WriteLine("sending to server the packet");
            _connection.Send(packetToSend);
        }

        public void CloseConnection()
        {
            _connection.Dispose(); //Close all of connections
        }
    }

}
