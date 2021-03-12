using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000072 RID: 114
	internal class GroupEscalationAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x0001482A File Offset: 0x00012A2A
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new GroupEscalationAgent(this.processedMessages);
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00014837 File Offset: 0x00012A37
		public ProcessedMessageTracker ProcessedMessages
		{
			get
			{
				return this.processedMessages;
			}
		}

		// Token: 0x04000254 RID: 596
		private ProcessedMessageTracker processedMessages = new ProcessedMessageTracker();
	}
}
