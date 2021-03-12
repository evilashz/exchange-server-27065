using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200002A RID: 42
	public sealed class E4eEncryptionAgentFactory : RoutingAgentFactory
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000BAC2 File Offset: 0x00009CC2
		internal AgentInstanceController InstanceController
		{
			get
			{
				return AgentInstanceController.Instance;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000BAC9 File Offset: 0x00009CC9
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new E4eEncryptionAgent(this);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public E4eEncryptionAgentFactory()
		{
			E4eAgentPerfCounters.EncryptionSuccessCount.RawValue = 0L;
			E4eAgentPerfCounters.AfterEncryptionSuccessCount.RawValue = 0L;
			E4eAgentPerfCounters.ReEncryptionSuccessCount.RawValue = 0L;
			E4eAgentPerfCounters.EncryptionFailureCount.RawValue = 0L;
			E4eAgentPerfCounters.AfterEncryptionFailureCount.RawValue = 0L;
			E4eAgentPerfCounters.ReEncryptionFailureCount.RawValue = 0L;
			AgentInstanceController.Initialize();
		}
	}
}
