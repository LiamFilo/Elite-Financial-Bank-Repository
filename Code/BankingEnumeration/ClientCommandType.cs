using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingEnumeration
{
    /// <summary>
    /// Represent command's type which receiving from the client to the server. 
    /// </summary>
    public enum ClientCommandType
    {
        Transaction_History,
        Log_In,
        Log_Out,
        Register, 
        Deposit,
        Withdraw,
        Transfer
    }
}
