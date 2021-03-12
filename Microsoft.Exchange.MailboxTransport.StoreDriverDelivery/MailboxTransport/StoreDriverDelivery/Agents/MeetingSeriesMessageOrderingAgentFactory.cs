using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000A0 RID: 160
	internal class MeetingSeriesMessageOrderingAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x0001D66C File Offset: 0x0001B86C
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new MeetingSeriesMessageOrderingAgent();
		}
	}
}
