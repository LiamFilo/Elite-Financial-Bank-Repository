using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;

namespace BankingCommunication {

    
	/// <summary>
	/// Represent a generic interface for packet.
	/// </summary>
	public interface IPacket  
	{
		public void IsValidPacket();
	}

}