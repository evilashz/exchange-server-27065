using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISubscriptionStatus
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		AggregationStatus Status { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		DetailedAggregationStatus SubStatus { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		MigrationSubscriptionStatus MigrationSubscriptionStatus { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4
		bool IsInitialSyncComplete { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5
		DateTime? LastSyncTime { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000006 RID: 6
		DateTime? LastSuccessfulSyncTime { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000007 RID: 7
		long? ItemsSynced { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000008 RID: 8
		long? ItemsSkipped { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000009 RID: 9
		DateTime? LastSyncNowRequestTime { get; }
	}
}
