using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BankingEntities;
using BankingEnumeration;

namespace BankingCommunication
{
    [Serializable]
    /// <summary>
    /// Represent a data structure for packet to the server.
    /// </summary>
    public struct BCPPacketRequest : IPacket
    {
        public readonly ICommand Command;
        public readonly string UserId;

        public BCPPacketType Type { get; private set; }

        public BCPPacketRequest(ICommand command, string userId)
        {
            Command = command;
            UserId = userId;
            Type = BCPPacketType.Request;
        }

        public BCPPacketRequest(ICommand command)
        {
            Command = command;
        }

        public void IsValidPacket()
        {
            if (Command is IValidatableCommand) (Command as IValidatableCommand).IsValidCommand();
            
            BankingEntities.UserValidator.IsValidIsraeliID(UserId);
            
        }
    }
}
