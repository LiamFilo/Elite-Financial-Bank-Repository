using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingEnumeration;
using System.Text.Json;
using BankingExceptions;

namespace BankingCommunication
{
    public class WithdrawCommand : ICashFlowCommand
    {

        public WithdrawCommand(decimal amount)
        {
            this.Amount = amount;
            this.ExecutionDate = DateTime.Now;
            this.CommandType = ClientCommandType.Withdraw;
        }


        public decimal Amount { get; private set; }

        public DateTime ExecutionDate { get; private set; }

        public ClientCommandType CommandType { get; private set; }

        public string ExportCommandsToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true // Format the JSON for readability
            });
        }

        public void IsValidCommand()
        {
            if (Amount < 0) throw new InvalidCommandException(this.CommandType);
        }
    }
}