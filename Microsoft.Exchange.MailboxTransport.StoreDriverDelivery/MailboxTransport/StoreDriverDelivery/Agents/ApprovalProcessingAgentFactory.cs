using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x0200005A RID: 90
	internal class ApprovalProcessingAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0000FCED File Offset: 0x0000DEED
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			if (this.counterInstance == null)
			{
				this.counterInstance = MSExchangeTransportApproval.GetInstance("_Total");
			}
			return new ApprovalProcessingAgent(server, this.counterInstance);
		}

		// Token: 0x040001D2 RID: 466
		private const string CounterInstanceName = "_Total";

		// Token: 0x040001D3 RID: 467
		private MSExchangeTransportApprovalInstance counterInstance;
	}
}
