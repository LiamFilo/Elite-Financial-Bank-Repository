using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingEnumeration;

namespace Server.Classes
{
    public struct ResponseData
    {
        private readonly object[] _data;

        public ResponseData(ClientCommandType commandType, DataTable dataFromServer)
        {
            switch (commandType)
            {
                case ClientCommandType.Log_In:
                    break;
            }
        }

        
    }
}
