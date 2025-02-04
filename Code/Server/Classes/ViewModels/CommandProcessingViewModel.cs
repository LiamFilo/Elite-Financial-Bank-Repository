using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingEnumeration;
using BankingCommunication;

namespace Server {
	public class CommandProcessingViewModel
	{

		public CommandProcessingViewModel()
		{

		}

		public void HandleCommand(ICommand command, string userID)
		{
			switch (command.CommandType)
			{
				case ClientCommandType.Log_In:
					HandleLogInCommand(command, userID);
					break;
			}

		}


		private void HandleLogInCommand(ICommand command, string userID)
		{
			
		}

	}

}