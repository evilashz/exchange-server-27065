using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000017 RID: 23
	public sealed class PrelicenseAgentFactory : RoutingAgentFactory
	{
		// Token: 0x06000071 RID: 113 RVA: 0x0000725C File Offset: 0x0000545C
		public PrelicenseAgentFactory()
		{
			PrelicenseAgentPerfCounters.TotalMessagesPreLicensed.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalMessagesFailedToPreLicense.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalDeferralsToPreLicense.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalRecipientsPreLicensed.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalRecipientsFailedToPreLicense.RawValue = 0L;
			PrelicenseAgentPerfCounters.Percentile95FailedToLicense.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalMessagesLicensed.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalMessagesFailedToLicense.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalDeferralsToLicense.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalExternalMessagesPreLicensed.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalExternalMessagesFailedToPreLicense.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalRecipientsPreLicensedForExternalMessages.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalRecipientsFailedToPreLicenseForExternalMessages.RawValue = 0L;
			PrelicenseAgentPerfCounters.TotalDeferralsToPreLicenseForExternalMessages.RawValue = 0L;
			AgentInstanceController.Initialize();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000731C File Offset: 0x0000551C
		internal AgentInstanceController InstanceController
		{
			get
			{
				return AgentInstanceController.Instance;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00007323 File Offset: 0x00005523
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new PrelicenseAgent(this, server);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000732C File Offset: 0x0000552C
		internal void UpdatePercentileCounters(bool success)
		{
			if (success)
			{
				PrelicenseAgentFactory.percentileCounter.AddValue(0L);
			}
			else
			{
				PrelicenseAgentFactory.percentileCounter.AddValue(1L);
			}
			PrelicenseAgentPerfCounters.Percentile95FailedToLicense.RawValue = PrelicenseAgentFactory.percentileCounter.PercentileQuery(95.0);
		}

		// Token: 0x040000AE RID: 174
		internal static readonly ExEventLog Logger = new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies");

		// Token: 0x040000AF RID: 175
		private static PercentileCounter percentileCounter = new PercentileCounter(TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(1.0), 1L, 2L);
	}
}
