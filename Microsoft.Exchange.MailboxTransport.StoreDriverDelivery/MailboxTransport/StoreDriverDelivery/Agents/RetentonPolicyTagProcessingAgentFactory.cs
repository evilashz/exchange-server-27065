using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000088 RID: 136
	internal class RetentonPolicyTagProcessingAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x00018367 File Offset: 0x00016567
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new RetentonPolicyTagProcessingAgent();
		}
	}
}
