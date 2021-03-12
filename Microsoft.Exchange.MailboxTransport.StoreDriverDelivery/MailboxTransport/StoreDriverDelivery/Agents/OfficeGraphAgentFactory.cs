using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000A5 RID: 165
	internal class OfficeGraphAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x0001E174 File Offset: 0x0001C374
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new OfficeGraphAgent();
		}
	}
}
