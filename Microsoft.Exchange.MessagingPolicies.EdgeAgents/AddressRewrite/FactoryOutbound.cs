using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000025 RID: 37
	public sealed class FactoryOutbound : RoutingAgentFactory
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00005D8C File Offset: 0x00003F8C
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new AgentOutbound(server);
		}
	}
}
