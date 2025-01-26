using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingEnumeration;

namespace BankingExceptions
{
    public class InvalidCommandException : Exception
    {
        public readonly ClientCommandType CommandType;

        public InvalidCommandException(ClientCommandType commandType) : base($"Invalid {commandType} command")
        {
            this.CommandType = commandType;
        }
    }
}
