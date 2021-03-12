using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x0200000C RID: 12
	public sealed class JournalAgentFactory : RoutingAgentFactory
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002FBC File Offset: 0x000011BC
		public JournalAgentFactory()
		{
			PerfCounters.UsersJournaled.RawValue = 0L;
			PerfCounters.UsersJournaledPerHour.RawValue = 0L;
			PerfCounters.ReportsGenerated.RawValue = 0L;
			PerfCounters.ReportsGeneratedPerHour.RawValue = 0L;
			PerfCounters.ProcessingTime.RawValue = 0L;
			PerfCounters.MessagesProcessed.RawValue = 0L;
			PerfCounters.ReportsGeneratedWithRMSProtectedMessage.RawValue = 0L;
			PerfCounters.MessagesDeferredWithinJournalAgent.RawValue = 0L;
			PerfCounters.MessagesDeferredWithinJournalAgentPerHour.RawValue = 0L;
			PerfCounters.JournalReportsThatCouldNotBeCreated.RawValue = 0L;
			PerfCounters.JournalReportsThatCouldNotBeCreatedPerHour.RawValue = 0L;
			PerfCounters.MessagesDeferredWithinJournalAgentLawfulIntercept.RawValue = 0L;
			PerfCounters.MessagesDeferredWithinJournalAgentLawfulInterceptPerHour.RawValue = 0L;
			Components.PerfCountersLoaderComponent.AddCounterToGetExchangeDiagnostics(typeof(PerfCounters), "JournalAgentCounters");
			this.perfCountersWrapper = new JournalPerfCountersWrapper(new Tuple<ExPerformanceCounter, ExPerformanceCounter>[]
			{
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.UsersJournaled, PerfCounters.UsersJournaledPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.ReportsGenerated, PerfCounters.ReportsGeneratedPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.MessagesDeferredWithinJournalAgent, PerfCounters.MessagesDeferredWithinJournalAgentPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.JournalReportsThatCouldNotBeCreated, PerfCounters.JournalReportsThatCouldNotBeCreatedPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.MessagesDeferredWithinJournalAgentLawfulIntercept, PerfCounters.MessagesDeferredWithinJournalAgentLawfulInterceptPerHour)
			});
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000030F1 File Offset: 0x000012F1
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new JournalAgent(server, this.perfCountersWrapper);
		}

		// Token: 0x04000052 RID: 82
		internal static JournalingDistibutionGroupCache JournalingDistributionGroupCacheInstance = new JournalingDistibutionGroupCache();

		// Token: 0x04000053 RID: 83
		private JournalPerfCountersWrapper perfCountersWrapper;
	}
}
