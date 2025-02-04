using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingEnumeration;
using BankingExceptions;

namespace BankingCommunication
{
    public interface IValidatableCommand
    {
        public void IsValidCommand();
    }
}
