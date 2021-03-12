using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000034 RID: 52
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncManagerConfiguration
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002A3 RID: 675
		TimeSpan WorkTypeBudgetManagerSlidingWindowLength { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002A4 RID: 676
		TimeSpan WorkTypeBudgetManagerSlidingBucketLength { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002A5 RID: 677
		TimeSpan WorkTypeBudgetManagerSampleDispatchedWorkFrequency { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002A6 RID: 678
		TimeSpan DatabaseBackoffTime { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002A7 RID: 679
		int MaxSyncsPerDB { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002A8 RID: 680
		TimeSpan DispatchEntryExpirationCheckFrequency { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002A9 RID: 681
		TimeSpan DispatchEntryExpirationTime { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002AA RID: 682
		TimeSpan DispatcherDatabaseRefreshFrequency { get; }
	}
}
