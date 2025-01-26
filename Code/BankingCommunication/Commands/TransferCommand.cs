using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingExceptions;


using BankingCommunication;
using BankingEnumeration;
using System.Text.Json;
namespace BankingCommunication
{
	public class TransferCommand : ICashFlowCommand
	{

		public readonly string TargetAccountNumber;

		public TransferCommand(decimal amount, string targetAccountNumber)
		{
            this.ExecutionDate = DateTime.Now;
            this.CommandType = ClientCommandType.Transfer;
            this.TargetAccountNumber = targetAccountNumber;
            this.Amount = amount;

            this.IsValidCommand();
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
            if (Amount <= 0) throw new InvalidCommandException(this.CommandType);
        }
    }
}