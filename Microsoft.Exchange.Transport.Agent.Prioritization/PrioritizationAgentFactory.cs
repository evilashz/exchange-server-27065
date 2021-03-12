using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.Transport.Agent.Prioritization
{
	// Token: 0x02000003 RID: 3
	public sealed class PrioritizationAgentFactory : RoutingAgentFactory
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000022F5 File Offset: 0x000004F5
		public PrioritizationAgentFactory()
		{
			if (PrioritizationAgentFactory.prioritizationEnabled)
			{
				PrioritizationAgentFactory.prioritization = new MessagePrioritization();
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000230E File Offset: 0x0000050E
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new PrioritizationAgent(PrioritizationAgentFactory.prioritization);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000231A File Offset: 0x0000051A
		internal RoutingAgent CreateAgent(MessagePrioritization prioritization)
		{
			return new PrioritizationAgent(prioritization);
		}

		// Token: 0x04000002 RID: 2
		private static bool prioritizationEnabled = Components.TransportAppConfig.DeliveryQueuePrioritizationConfiguration.PrioritizationEnabled;

		// Token: 0x04000003 RID: 3
		private static MessagePrioritization prioritization;
	}
}
