using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000125 RID: 293
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IQueuedCallback
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000B44 RID: 2884
		DateTime CreateTimeUtc { get; }

		// Token: 0x1700027D RID: 637
		// (set) Token: 0x06000B45 RID: 2885
		DateTime StartTimeUtc { set; }

		// Token: 0x1700027E RID: 638
		// (set) Token: 0x06000B46 RID: 2886
		DateTime EndTimeUtc { set; }

		// Token: 0x06000B47 RID: 2887
		bool IsEquivalentOrSuperset(IQueuedCallback otherCallback);

		// Token: 0x06000B48 RID: 2888
		void Cancel();

		// Token: 0x06000B49 RID: 2889
		void Execute();

		// Token: 0x06000B4A RID: 2890
		void ReportStatus(QueuedItemStatus status);

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B4B RID: 2891
		Exception LastException { get; }
	}
}
