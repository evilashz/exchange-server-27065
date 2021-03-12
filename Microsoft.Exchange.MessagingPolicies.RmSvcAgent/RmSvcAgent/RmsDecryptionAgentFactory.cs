using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000026 RID: 38
	public sealed class RmsDecryptionAgentFactory : RoutingAgentFactory
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000A354 File Offset: 0x00008554
		public RmsDecryptionAgentFactory()
		{
			RmsDecryptionAgentPerfCounters.MessageDecrypted.RawValue = 0L;
			RmsDecryptionAgentPerfCounters.MessageFailedToDecrypt.RawValue = 0L;
			AgentInstanceController.Initialize();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000A379 File Offset: 0x00008579
		internal AgentInstanceController InstanceController
		{
			get
			{
				return AgentInstanceController.Instance;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000A380 File Offset: 0x00008580
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new RmsDecryptionAgent(AgentInstanceController.Instance);
		}
	}
}
