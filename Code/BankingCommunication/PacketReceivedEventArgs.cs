using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace BankingCommunication {

	/// <summary>
	/// Data Structure for argument from Packet Received Event which connection object raise. 
	/// </summary>
	public class PacketReceivedEventArgs : EventArgs {

		public readonly IPacket packetReceived;

		public PacketReceivedEventArgs(IPacket packet)
		{
			this.packetReceived = packet ?? throw new ArgumentNullException(nameof(packet), "The packet cannot be null.");
        }
	}

}