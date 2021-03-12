using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000021 RID: 33
	public sealed class JournalReportDecryptionAgentFactory : RoutingAgentFactory
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x0000954D File Offset: 0x0000774D
		public JournalReportDecryptionAgentFactory()
		{
			JournalReportDecryptionAgentPerfCounters.TotalJRDecrypted.RawValue = 0L;
			JournalReportDecryptionAgentPerfCounters.TotalJRFailed.RawValue = 0L;
			JournalReportDecryptionAgentPerfCounters.TotalDeferrals.RawValue = 0L;
			AgentInstanceController.Initialize();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000957E File Offset: 0x0000777E
		internal AgentInstanceController InstanceController
		{
			get
			{
				return AgentInstanceController.Instance;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00009585 File Offset: 0x00007785
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new JournalReportDecryptionAgent(this, server);
		}

		// Token: 0x04000108 RID: 264
		internal static ExEventLog Logger = new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies");
	}
}
