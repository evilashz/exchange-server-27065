using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.MessagingPolicies.Journaling;
using Microsoft.Exchange.Transport;
using Microsoft.Win32;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000021 RID: 33
	public class UnwrapJournalAgentFactory : RoutingAgentFactory
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00007494 File Offset: 0x00005694
		public UnwrapJournalAgentFactory()
		{
			PerfCounters.ProcessingTime.RawValue = 0L;
			PerfCounters.MessagesProcessed.RawValue = 0L;
			PerfCounters.MessagesProcessedPerHour.RawValue = 0L;
			PerfCounters.MessagesUnjournaled.RawValue = 0L;
			PerfCounters.MessagesUnjournaledPerHour.RawValue = 0L;
			PerfCounters.DefectiveJournals.RawValue = 0L;
			PerfCounters.DefectiveJournalsPerHour.RawValue = 0L;
			PerfCounters.TotalMessagesUnjournaledSize.RawValue = 0L;
			PerfCounters.UsersUnjournaled.RawValue = 0L;
			PerfCounters.UsersUnjournaledPerHour.RawValue = 0L;
			PerfCounters.PermanentError.RawValue = 0L;
			PerfCounters.PermanentErrorPerHour.RawValue = 0L;
			PerfCounters.TransientError.RawValue = 0L;
			PerfCounters.TransientErrorPerHour.RawValue = 0L;
			PerfCounters.NdrProcessSuccess.RawValue = 0L;
			PerfCounters.NdrProcessSuccessPerHour.RawValue = 0L;
			PerfCounters.LegacyArchiveJournallingDisabled.RawValue = 0L;
			PerfCounters.LegacyArchiveJournallingDisabledPerHour.RawValue = 0L;
			PerfCounters.NonJournalMsgFromLegacyArchiveCustomer.RawValue = 0L;
			PerfCounters.NonJournalMsgFromLegacyArchiveCustomerPerHour.RawValue = 0L;
			PerfCounters.AlreadyProcessed.RawValue = 0L;
			PerfCounters.AlreadyProcessedPerHour.RawValue = 0L;
			PerfCounters.DropJournalReportWithoutNdr.RawValue = 0L;
			PerfCounters.DropJournalReportWithoutNdrPerHour.RawValue = 0L;
			PerfCounters.NoUsersResolved.RawValue = 0L;
			PerfCounters.NoUsersResolvedPerHour.RawValue = 0L;
			PerfCounters.NdrJournalReport.RawValue = 0L;
			PerfCounters.NdrJournalReportPerHour.RawValue = 0L;
			this.isEnabled = this.IsUnJournalingEnabled();
			Components.PerfCountersLoaderComponent.AddCounterToGetExchangeDiagnostics(typeof(PerfCounters), "UnwrapJournalAgentCounters");
			this.perfCountersWrapper = new JournalPerfCountersWrapper(new Tuple<ExPerformanceCounter, ExPerformanceCounter>[]
			{
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.MessagesProcessed, PerfCounters.MessagesProcessedPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.MessagesUnjournaled, PerfCounters.MessagesUnjournaledPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.DefectiveJournals, PerfCounters.DefectiveJournalsPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.UsersUnjournaled, PerfCounters.UsersUnjournaledPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.PermanentError, PerfCounters.PermanentErrorPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.TransientError, PerfCounters.TransientErrorPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.NdrProcessSuccess, PerfCounters.NdrProcessSuccessPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.LegacyArchiveJournallingDisabled, PerfCounters.LegacyArchiveJournallingDisabledPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.NonJournalMsgFromLegacyArchiveCustomer, PerfCounters.NonJournalMsgFromLegacyArchiveCustomerPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.AlreadyProcessed, PerfCounters.AlreadyProcessedPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.DropJournalReportWithoutNdr, PerfCounters.DropJournalReportWithoutNdrPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.NoUsersResolved, PerfCounters.NoUsersResolvedPerHour),
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.NdrJournalReport, PerfCounters.NdrJournalReportPerHour)
			});
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000771E File Offset: 0x0000591E
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new UnwrapJournalAgent(this.isEnabled, this.perfCountersWrapper);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007734 File Offset: 0x00005934
		private bool IsUnJournalingEnabled()
		{
			try
			{
				object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Exchange_Test\\v15\\BCM", "DisableUnJournalAgent", 0);
				if (value is int && (int)value != 0)
				{
					return false;
				}
			}
			catch (SecurityException ex)
			{
				ExTraceGlobals.JournalingTracer.TraceError<string, string>(0L, "Exception encountered while reading registry key for Unjournal agent: '{0}'.  The agent is ON by default.  Exception details: '{1}'.", "DisableUnJournalAgent", ex.ToString());
			}
			catch (IOException ex2)
			{
				ExTraceGlobals.JournalingTracer.TraceError<string, string>(0L, "Exception encountered while reading registry key for Unjournal agent: '{0}'.  The agent is ON by default.  Exception details: '{1}'.", "DisableUnJournalAgent", ex2.ToString());
			}
			return true;
		}

		// Token: 0x040000F7 RID: 247
		private readonly bool isEnabled;

		// Token: 0x040000F8 RID: 248
		private JournalPerfCountersWrapper perfCountersWrapper;
	}
}
