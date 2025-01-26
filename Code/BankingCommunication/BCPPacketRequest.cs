using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingCommunication
{
    /// <summary>
    /// Represent a data structure for packet to the server.
    /// </summary>
    public struct BCPPacketRequest : IPacket
    {
        public readonly ICommand Command;
        public readonly string UserId;

        public BCPPacketRequest(ICommand command, string userId)
        {
            Command = command;
            UserId = userId; 
        }
    }
}
