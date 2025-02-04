using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using BankingEnumeration;
namespace BankingCommunication {

    
	/// <summary>
	/// Represent a generic interface for packet.
	/// </summary>
	public interface IPacket  
	{
		public BCPPacketType Type { get; }
		public void IsValidPacket();
	}

}