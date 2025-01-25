using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BankingEntities {
	public class CheckingBankAccountCreator : IBankAccountCreator {

        private readonly decimal _accountOverdraftLimit;

        public CheckingBankAccountCreator(decimal accountOverdraftLimit)
		{
			this._accountOverdraftLimit = accountOverdraftLimit;
		}

        public BankAccount CreateBankAccount(decimal balance, string accountNumber)
        {
            return new CheckingBankAccount(balance, accountNumber, this._accountOverdraftLimit);
        }
    }

}