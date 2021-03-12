using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000067 RID: 103
	internal interface ITimeBasedAssistantType : IAssistantType
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002E3 RID: 739
		TimeBasedAssistantIdentifier Identifier { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002E4 RID: 740
		WorkloadType WorkloadType { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002E5 RID: 741
		PropertyTagPropertyDefinition ControlDataPropertyDefinition { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002E6 RID: 742
		PropertyTagPropertyDefinition[] MailboxExtendedProperties { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002E7 RID: 743
		TimeSpan WorkCycle { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002E8 RID: 744
		TimeSpan WorkCycleCheckpoint { get; }

		// Token: 0x060002E9 RID: 745
		void OnWorkCycleStart(DatabaseInfo databaseInfo);

		// Token: 0x060002EA RID: 746
		void OnWorkCycleCheckpoint();

		// Token: 0x060002EB RID: 747
		bool IsMailboxInteresting(MailboxInformation mailboxInformation);

		// Token: 0x060002EC RID: 748
		ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo);

		// Token: 0x060002ED RID: 749
		TimeBasedDatabaseDriver CreateDriver(ThrottleGovernor governor, DatabaseInfo databaseInfo, ITimeBasedAssistantType timeBasedAssistantType, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters);
	}
}
