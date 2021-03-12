using System;
using Microsoft.Exchange.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200001E RID: 30
	internal abstract class StoreAssistantType : TimeBasedAssistantType
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000056C8 File Offset: 0x000038C8
		public override TimeBasedDatabaseDriver CreateDriver(ThrottleGovernor governor, DatabaseInfo databaseInfo, ITimeBasedAssistantType timeBasedAssistantType, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters)
		{
			return new TimeBasedStoreDatabaseDriver(governor, databaseInfo, timeBasedAssistantType, poisonControl, databaseCounters);
		}
	}
}
