using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingExceptions;

namespace BankingCommunication {
	public interface ICashFlowCommand : ICommand, IValidatableCommand
	{
		public decimal Amount { get; }
    }

}