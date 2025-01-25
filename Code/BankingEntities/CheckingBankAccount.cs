using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace BankingEntities {
	public class CheckingBankAccount : BankAccount 
	{

        #region Data-Members
        private bool _isAccountBlock;
        public readonly decimal ACCOUNT_OVERDRAFT_LIMIT;
        public event EventHandler<EventArgs>? AccountBlock;

        #endregion

        #region Properties
        public bool IsAccountBlock { get; }
        #endregion

        public CheckingBankAccount(decimal balance, string accountNumber, decimal accountOverdraftLimit) 
            : base(balance,accountNumber) 
		{
            this.ACCOUNT_OVERDRAFT_LIMIT = accountOverdraftLimit;
            this._isAccountBlock = false;
        }

        public override decimal Balance
        {
            get
            {
                return base.Balance;
            }

            protected set
            {
                if (_balance - value < ACCOUNT_OVERDRAFT_LIMIT)
                {
                    this._isAccountBlock = true;
                    this.AccountBlock?.Invoke(this, new EventArgs());
                }
            }
        }

        protected override bool isValidCashFlowOperation(decimal amountToCheck)
        {
            return base.isValidCashFlowOperation(amountToCheck) && this._isAccountBlock == false;
        }

    }

}