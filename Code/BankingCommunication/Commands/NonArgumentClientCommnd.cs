using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingEnumeration;
using System.Text.Json;
using BankingExceptions;

namespace BankingCommunication {
	public class NonArgumentClientCommnd : ICommand,IValidatableCommand
	{
        public static List<ClientCommandType> NonArgumentClientCommandList;
        public DateTime ExecutionDate { get; private set; }
        public ClientCommandType CommandType { get; private set; }

        static NonArgumentClientCommnd()
        {
            NonArgumentClientCommandList = new List<ClientCommandType>();
            NonArgumentClientCommnd.InitializeNonArguemntList();
        }


        public NonArgumentClientCommnd(ClientCommandType commandType)
		{
            this.CommandType = commandType;

            IsValidCommand();
		}
        private static void InitializeNonArguemntList()
        {

            NonArgumentClientCommandList.Add(ClientCommandType.Log_Out);
            NonArgumentClientCommandList.Add(ClientCommandType.Transaction_History);
        }

        public string ExportCommandsToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true // Format the JSON for readability
            });
        }

        public void IsValidCommand()
        {
            if (NonArgumentClientCommandList.Contains(this.CommandType))
                throw new Exception("The type command you give is not non-argument command");
            
        }
    }
}