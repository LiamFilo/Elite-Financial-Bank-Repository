using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingExceptions;


namespace Server {
	public static class ExceptionHandler {


		public static void ExceptionHandle(Exception exception)
		{
            Console.WriteLine(exception.Message);
		}

	}

}