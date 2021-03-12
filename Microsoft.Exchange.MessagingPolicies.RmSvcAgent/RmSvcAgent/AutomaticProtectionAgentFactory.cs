using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000012 RID: 18
	public sealed class AutomaticProtectionAgentFactory : RoutingAgentFactory
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00005270 File Offset: 0x00003470
		internal AgentInstanceController InstanceController
		{
			get
			{
				return AgentInstanceController.Instance;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005278 File Offset: 0x00003478
		public AutomaticProtectionAgentFactory()
		{
			ApaAgentPerfCounters.TotalMessagesEncrypted.RawValue = 0L;
			ApaAgentPerfCounters.TotalMessagesFailed.RawValue = 0L;
			ApaAgentPerfCounters.TotalDeferrals.RawValue = 0L;
			ApaAgentPerfCounters.TotalMessagesReencrypted.RawValue = 0L;
			ApaAgentPerfCounters.TotalMessagesFailedToReencrypt.RawValue = 0L;
			ApaAgentPerfCounters.Percentile95FailedToEncrypt.RawValue = 0L;
			AgentInstanceController.Initialize();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000052D8 File Offset: 0x000034D8
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new AutomaticProtectionAgent(this);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000052E0 File Offset: 0x000034E0
		internal void UpdatePercentileCounters(bool success)
		{
			if (success)
			{
				AutomaticProtectionAgentFactory.percentileCounter.AddValue(0L);
			}
			else
			{
				AutomaticProtectionAgentFactory.percentileCounter.AddValue(1L);
			}
			ApaAgentPerfCounters.Percentile95FailedToEncrypt.RawValue = AutomaticProtectionAgentFactory.percentileCounter.PercentileQuery(95.0);
		}

		// Token: 0x04000088 RID: 136
		internal static readonly ExEventLog Logger = new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies");

		// Token: 0x04000089 RID: 137
		private static PercentileCounter percentileCounter = new PercentileCounter(TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(1.0), 1L, 2L);
	}
}
