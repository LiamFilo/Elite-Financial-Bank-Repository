using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingCommunication;

namespace Server.Classes
{
    public readonly struct Request
    {
        public ICommand CommandToExecute { get; }
        public RequestPriority Priority { get; }    

        public string UserID { get; }   

        public Request(ICommand commandToExecute, RequestPriority priority, string UserID)
        {
            CommandToExecute = commandToExecute;
            Priority = priority;
            this.UserID = UserID;
        }
    }
}
