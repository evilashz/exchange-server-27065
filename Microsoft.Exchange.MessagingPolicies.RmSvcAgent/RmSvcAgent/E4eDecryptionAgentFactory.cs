using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200002C RID: 44
	internal sealed class E4eDecryptionAgentFactory : RoutingAgentFactory
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000C42F File Offset: 0x0000A62F
		internal AgentInstanceController InstanceController
		{
			get
			{
				return AgentInstanceController.Instance;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000C436 File Offset: 0x0000A636
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new E4eDecryptionAgent(this);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000C43E File Offset: 0x0000A63E
		public E4eDecryptionAgentFactory()
		{
			E4eAgentPerfCounters.DecryptionSuccessCount.RawValue = 0L;
			E4eAgentPerfCounters.DecryptionFailureCount.RawValue = 0L;
			AgentInstanceController.Initialize();
		}
	}
}
