using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingCommunication;

namespace Server.Classes
{
    public class ModelManagemetViewModel
    {
        private ConnectedUsers _connectedUsers;
        private PriorityQueue _priorityQueue;

        public PriorityQueue PriorityQueue { get { return _priorityQueue; } }

        public ModelManagemetViewModel()
        {
            _connectedUsers = new ConnectedUsers();
            _priorityQueue = new PriorityQueue();
        }

        public void ExecuteCommandInModel(ICommand command)
        {

        }
    }
}
