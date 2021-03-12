using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000BE RID: 190
	internal sealed class SmsDeliveryAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0002049B File Offset: 0x0001E69B
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new SmsDeliveryAgent();
		}
	}
}
