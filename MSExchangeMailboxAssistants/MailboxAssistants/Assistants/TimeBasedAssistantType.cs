using System;
using Microsoft.Exchange.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200000A RID: 10
	internal abstract class TimeBasedAssistantType
	{
		// Token: 0x06000057 RID: 87
		public abstract TimeBasedDatabaseDriver CreateDriver(ThrottleGovernor governor, DatabaseInfo databaseInfo, ITimeBasedAssistantType timeBasedAssistantType, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters);

		// Token: 0x06000058 RID: 88 RVA: 0x00003B0A File Offset: 0x00001D0A
		public virtual void OnWorkCycleStart(DatabaseInfo databaseInfo)
		{
		}
	}
}
