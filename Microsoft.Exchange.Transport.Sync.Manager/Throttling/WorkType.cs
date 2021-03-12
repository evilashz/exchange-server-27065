using System;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000056 RID: 86
	internal enum WorkType
	{
		// Token: 0x04000242 RID: 578
		AggregationSubscriptionSaved,
		// Token: 0x04000243 RID: 579
		AggregationIncremental,
		// Token: 0x04000244 RID: 580
		AggregationInitial,
		// Token: 0x04000245 RID: 581
		MigrationInitial,
		// Token: 0x04000246 RID: 582
		MigrationIncremental,
		// Token: 0x04000247 RID: 583
		MigrationFinalization,
		// Token: 0x04000248 RID: 584
		OwaLogonTriggeredSyncNow,
		// Token: 0x04000249 RID: 585
		OwaActivityTriggeredSyncNow,
		// Token: 0x0400024A RID: 586
		OwaRefreshButtonTriggeredSyncNow,
		// Token: 0x0400024B RID: 587
		PeopleConnectionInitial,
		// Token: 0x0400024C RID: 588
		PeopleConnectionTriggered,
		// Token: 0x0400024D RID: 589
		PeopleConnectionIncremental,
		// Token: 0x0400024E RID: 590
		PolicyInducedDelete
	}
}
