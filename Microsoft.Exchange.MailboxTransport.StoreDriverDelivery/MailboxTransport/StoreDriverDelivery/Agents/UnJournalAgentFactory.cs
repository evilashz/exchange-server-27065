using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000CB RID: 203
	internal class UnJournalAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x000226D0 File Offset: 0x000208D0
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new UnJournalAgent();
		}
	}
}
