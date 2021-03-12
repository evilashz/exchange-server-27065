using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x0200006F RID: 111
	internal class ConversationsProcessingAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x000138FD File Offset: 0x00011AFD
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new ConversationsProcessingAgent();
		}
	}
}
