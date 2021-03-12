using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000D6 RID: 214
	internal class WorkingSetAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x00024273 File Offset: 0x00022473
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new WorkingSetAgent();
		}
	}
}
