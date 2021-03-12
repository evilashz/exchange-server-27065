using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000C8 RID: 200
	internal sealed class UMPartnerMessageAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x000225E4 File Offset: 0x000207E4
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new UMPartnerMessageAgent(server);
		}
	}
}
