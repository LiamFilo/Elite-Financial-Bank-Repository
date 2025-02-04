using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingCommunication;
using Microsoft.Identity.Client;
using Server.Classes;
namespace Server {
	public class DataBaseViewModel 
	{
		private DatabaseManager _databaseManager;

		public DataBaseViewModel() 
		{
            _databaseManager = new DatabaseManager();
        }


		public void ConnectToDB()
		{
			_databaseManager.OpenConnection();
        }

		public void CloseConnection()
		{
			_databaseManager?.CloseConnection();
		}

		/// 
		/// <param name="command"></param>
		public ResponseData FetchDataByCommand(ICommand command) 
		{

			return new ResponseData();
		}

		public ResponseData FetchLogInData() {

			return new ResponseData();
        }
	}

}