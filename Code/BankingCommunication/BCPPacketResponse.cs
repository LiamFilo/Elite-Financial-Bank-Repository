using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingCommunication
{
    /// <summary>
    /// Represent a BCP packet that server send to the client.
    /// </summary>
    public class BCPPacketResponse : IPacket
    {
        public readonly object[] dataFromServer;
        public readonly string BankAccountId;

        public BCPPacketResponse(object[] dataFromServer, string bankAccountId)
        {
            this.dataFromServer = dataFromServer;
            BankAccountId = bankAccountId;
        }

    }
}
