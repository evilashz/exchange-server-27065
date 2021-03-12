using System;
using Microsoft.Exchange.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200000B RID: 11
	internal abstract class AdminRpcAssistantType : TimeBasedAssistantType
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003B14 File Offset: 0x00001D14
		public override TimeBasedDatabaseDriver CreateDriver(ThrottleGovernor governor, DatabaseInfo databaseInfo, ITimeBasedAssistantType timeBasedAssistantType, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters)
		{
			return new TimeBasedAdminRpcDatabaseDriver(governor, databaseInfo, timeBasedAssistantType, poisonControl, databaseCounters);
		}
	}
}
