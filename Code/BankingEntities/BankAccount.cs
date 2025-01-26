using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using BankingExceptions;
using BankingEnumeration;

namespace BankingEntities
{
	/// <summary>
	/// Represent a abstract class for bank account.
	/// </summary>
    public abstract class BankAccount 
	{
        #region Data Members
        protected decimal _balance;
		protected string _accountNumber;
		#endregion

		#region Constructors
		public BankAccount(decimal balance, string accountNumber)
        {
			this.Balance = balance;
			this.AccountNumber = accountNumber;
        }

		#endregion

        #region Properties
        public virtual decimal Balance
		{
			get { return _balance; }
			protected set{} 
		}

		public string AccountNumber
		{
			get { return _accountNumber; }
			protected set
			{
                if (value.Length != 8 && Regex.IsMatch(value, @"^\d+$"))
                    throw new InvalidUserInput(UserInputField.Bank_Account_Number);
				_accountNumber = value;
            }
        }
		#endregion

		#region Methods

		protected virtual bool isValidCashFlowOperation(decimal amountToCheck)
		{
			if(amountToCheck <= 0)
				return false;
			return true;
		}

		/// 
		/// <param name="amount">represent the amount which passing into your bank account </param>
		public virtual void Deposit(decimal amount){
			if (!this.isValidCashFlowOperation(amount))
				throw new InvalidOperationException("Invalid Deposit Operation.");
			Balance += amount;
		}


		public virtual void ReceiveMoney(decimal amount)
		{
            if (!this.isValidCashFlowOperation(amount))
                throw new InvalidOperationException("Invalid ReceiveMoney Operation.");
            Balance += amount;
        }


		public virtual void Transfer(decimal amount)
		{
            if (!this.isValidCashFlowOperation(amount))
                throw new InvalidOperationException("Invalid Transfer Operation.");
            Balance -= amount;
        }
			        

		public virtual void Withdraw(decimal amount){
            if (!this.isValidCashFlowOperation(amount))
                throw new InvalidOperationException("Invalid Withdraw Operation.");
            Balance -= amount;
        }


		#endregion
	}

}