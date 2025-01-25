using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingEntities;

namespace BankingEntities {
	public class BankAccounts<T> where T : BankAccount
	{

		private readonly Dictionary<string, T> _accountsByAccountNumber;

		public BankAccounts()
		{
			_accountsByAccountNumber = new Dictionary<string, T>();
		}

        public bool IsAccountExist(string accountNumberToCheck)
        {
			return this._accountsByAccountNumber.ContainsKey(accountNumberToCheck);	
        }

        public void AddBankAccount(T accountToAdd)
		{
			if (this.IsAccountExist(accountToAdd.AccountNumber))
				throw new InvalidOperationException("Invalid adding operation, " +
					"the bank account you want to add is already exist.");
			this._accountsByAccountNumber.Add(accountToAdd.AccountNumber, accountToAdd);
		}

		public BankAccount GetBankAccountByNumber(string accountNumber){

            if (!this.IsAccountExist(accountNumber))
                throw new InvalidOperationException("the bank account you want  is not exist.");
            return this._accountsByAccountNumber[accountNumber];
        }

		
		public void RemoveBankAccount(string AccountNumberToRemove){
            if (!this.IsAccountExist(AccountNumberToRemove))
                throw new InvalidOperationException("Invalid deleting operation, " +
                    "the bank account you want to delete is not exist.");
			this._accountsByAccountNumber.Remove(AccountNumberToRemove);
        }

	}

}