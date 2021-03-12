using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x0200009A RID: 154
	internal class MeetingMessageProcessingAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x0001C47F File Offset: 0x0001A67F
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new MeetingMessageProcessingAgent();
		}
	}
}
