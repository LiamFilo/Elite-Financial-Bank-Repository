using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BankingEntities {
	public interface IBankAccountCreator  
	{
        public BankAccount CreateBankAccount(decimal balance, string accountNumber);
    }
}