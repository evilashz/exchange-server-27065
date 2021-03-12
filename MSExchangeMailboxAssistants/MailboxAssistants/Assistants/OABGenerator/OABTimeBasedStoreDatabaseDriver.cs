using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Diagnostics.Components.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001E0 RID: 480
	internal sealed class OABTimeBasedStoreDatabaseDriver : TimeBasedStoreDatabaseDriver
	{
		// Token: 0x06001275 RID: 4725 RVA: 0x0006A036 File Offset: 0x00068236
		internal OABTimeBasedStoreDatabaseDriver(ThrottleGovernor parentGovernor, DatabaseInfo databaseInfo, ITimeBasedAssistantType timeBasedAssistantType, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters) : base(parentGovernor, databaseInfo, timeBasedAssistantType, poisonControl, databaseCounters)
		{
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0006A048 File Offset: 0x00068248
		public override void RunNow(Guid mailboxGuid, string parameters)
		{
			MailboxData mailboxData = base.Assistant.CreateOnDemandMailboxData(mailboxGuid, parameters);
			if (mailboxData == null)
			{
				return;
			}
			ExTraceGlobals.TimeBasedDatabaseDriverTracer.TraceDebug<OABTimeBasedStoreDatabaseDriver, Guid>((long)this.GetHashCode(), "{0}: OABTimeBasedStoreDatabaseDriver.RunNow: about to start processing mailbox {1} on this database.", this, mailboxGuid);
			base.RunNow(mailboxData);
		}
	}
}
