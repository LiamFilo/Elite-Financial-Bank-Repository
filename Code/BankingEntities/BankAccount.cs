using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using BankingExceptions;

namespace BankingEntities
{
	/// <summary>
	/// Represent a abstract class for bank account.
	/// </summary>
    public abstract class BankAccount 
	{
        #region Data Members
        protected decimal _balance;
		public readonly string AccountNumber;
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
			protected set{ } 
		}
		#endregion

		#region Methods
		protected void isValidAccountNumber(string accountNumber)
		{
			if (accountNumber.Length != 8 && Regex.IsMatch(accountNumber, @"^\d+$"))
				throw new GeneralException("The accountNumber is not valid");


        }

		protected virtual bool isValidCashFlowOperation(decimal amountToCheck)
		{
			if(amountToCheck <= 0)
				return false;
			return true;
		}

		//protected virtual void InitializeBalanceLimit(decimal balanceLimit)
		//{
		//	this._balance = balanceLimit;
		//}

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