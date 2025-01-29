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
        private ConnectedUsers ConnectedUsers { get; }
        private PriorityQueue _priorityQueue;

        public PriorityQueue PriorityQueue { get { return _priorityQueue; } }

        public ModelManagemetViewModel()
        {
            ConnectedUsers = new ConnectedUsers();
            _priorityQueue = new PriorityQueue();
        }

        public void ExecuteCommandInModel(ICommand command)
        {

        }
    }
}
