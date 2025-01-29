using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingCommunication;

namespace Client
{
    public class BCPManagerViewModel
    {
        private  bool _isConnected;
        private Connection _connection;

        public BCPManagerViewModel()
        {
            try
            {
                this._connection = new Connection();

                _connection.ConnectAsync();
            }
            catch(Exception ex) { ExceptionHandler.ExceptionHandle(ex); }
        }

    }

}
