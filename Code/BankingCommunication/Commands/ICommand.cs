using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingEnumeration;

namespace BankingCommunication {
	public interface ICommand
	{
        public DateTime ExecutionDate { get; }
		public ClientCommandType CommandType { get; }

		public string ExportCommandsToJson();

    }

}