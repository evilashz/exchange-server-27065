using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000081 RID: 129
	internal class ClassificationApplicationAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x000178BC File Offset: 0x00015ABC
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new ClassificationApplicationAgent();
		}
	}
}
