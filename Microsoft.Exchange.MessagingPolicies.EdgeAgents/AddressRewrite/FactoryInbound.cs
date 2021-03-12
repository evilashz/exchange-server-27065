using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000023 RID: 35
	public sealed class FactoryInbound : SmtpReceiveAgentFactory
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00005808 File Offset: 0x00003A08
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new AgentInbound(server);
		}
	}
}
