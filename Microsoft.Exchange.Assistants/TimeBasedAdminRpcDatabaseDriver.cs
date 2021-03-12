using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Assistants;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000087 RID: 135
	internal class TimeBasedAdminRpcDatabaseDriver : TimeBasedDatabaseDriver
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x00013828 File Offset: 0x00011A28
		internal TimeBasedAdminRpcDatabaseDriver(ThrottleGovernor parentGovernor, DatabaseInfo databaseInfo, ITimeBasedAssistantType timeBasedAssistantType, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters) : base(parentGovernor, databaseInfo, timeBasedAssistantType, poisonControl, databaseCounters)
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013838 File Offset: 0x00011A38
		public override void RunNow(Guid mailboxGuid, string parameters)
		{
			MailboxData mailboxData = base.Assistant.CreateOnDemandMailboxData(mailboxGuid, parameters);
			if (mailboxData == null)
			{
				return;
			}
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<TimeBasedAdminRpcDatabaseDriver, Guid>((long)this.GetHashCode(), "{0}: RunNow: about to start processing mailbox {1} on this database.", this, mailboxGuid);
			base.RunNow(mailboxData);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00013878 File Offset: 0x00011A78
		protected override List<MailboxData> GetMailboxesForCurrentWindow(out int totalMailboxOnDatabaseCount, out int notInterestingMailboxCount, out int filteredMailboxCount, out int failedFilteringCount)
		{
			List<MailboxData> list = base.Assistant.GetMailboxesToProcess();
			list = (list ?? new List<MailboxData>());
			totalMailboxOnDatabaseCount = list.Count;
			notInterestingMailboxCount = 0;
			filteredMailboxCount = 0;
			failedFilteringCount = 0;
			Guid activityId = Guid.NewGuid();
			foreach (MailboxData mailboxData in list)
			{
				AssistantsLog.LogMailboxInterestingEvent(activityId, base.Assistant.NonLocalizedName, base.Assistant as AssistantBase, null, mailboxData.MailboxGuid, mailboxData.DisplayName);
			}
			return list;
		}
	}
}
