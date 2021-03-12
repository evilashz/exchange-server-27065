using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000CA RID: 202
	internal sealed class UMPlayonPhoneAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x06000635 RID: 1589 RVA: 0x000226C8 File Offset: 0x000208C8
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new UMPlayonPhoneAgent(server);
		}
	}
}
