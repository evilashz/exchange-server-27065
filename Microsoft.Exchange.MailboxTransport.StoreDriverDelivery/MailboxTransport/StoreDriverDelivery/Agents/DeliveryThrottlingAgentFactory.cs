using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000025 RID: 37
	public sealed class DeliveryThrottlingAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0000AD15 File Offset: 0x00008F15
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new DeliveryThrottlingAgent();
		}
	}
}
