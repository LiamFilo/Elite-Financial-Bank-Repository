using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingEnumeration;
using System.Reflection.Metadata;
using BankingEntities;
using System.Text.Json;
using BankingExceptions;

namespace BankingCommunication
{
	public class LogInCommand : ICommand, IValidatableCommand
    {
		public readonly string Id;
		public readonly string PhoneNumber;


        public DateTime ExecutionDate { get; private set; }
        public ClientCommandType CommandType { get; private set; }

        public LogInCommand(string id, string phoneNumber)
		{
            this.IsValidCommand();

			this.Id = id;
			this.PhoneNumber = phoneNumber;
			this.ExecutionDate = DateTime.Now;
			this.CommandType = ClientCommandType.Log_In;
		}

        public void IsValidCommand()
        {
            if (BankingEntities.UserValidator.IsValidIsraeliID(this.Id) &&
                BankingEntities.UserValidator.IsValidIsraeliPhoneNumber(this.PhoneNumber))
                    throw new InvalidCommandException(CommandType);
        }

        public string ExportCommandsToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true // Format the JSON for readability
            });
        }

    }

}