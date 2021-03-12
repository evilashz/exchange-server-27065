using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x02000136 RID: 310
	public enum ContentIndexStatusType
	{
		// Token: 0x04000A7A RID: 2682
		[LocDescription(CoreStrings.IDs.ContentIndexStatusUnknown)]
		Unknown,
		// Token: 0x04000A7B RID: 2683
		[LocDescription(CoreStrings.IDs.ContentIndexStatusHealthy)]
		Healthy,
		// Token: 0x04000A7C RID: 2684
		[LocDescription(CoreStrings.IDs.ContentIndexStatusCrawling)]
		Crawling,
		// Token: 0x04000A7D RID: 2685
		[LocDescription(CoreStrings.IDs.ContentIndexStatusFailed)]
		Failed,
		// Token: 0x04000A7E RID: 2686
		[LocDescription(CoreStrings.IDs.ContentIndexStatusSeeding)]
		Seeding,
		// Token: 0x04000A7F RID: 2687
		[LocDescription(CoreStrings.IDs.ContentIndexStatusFailedAndSuspended)]
		FailedAndSuspended,
		// Token: 0x04000A80 RID: 2688
		[LocDescription(CoreStrings.IDs.ContentIndexStatusSuspended)]
		Suspended,
		// Token: 0x04000A81 RID: 2689
		[LocDescription(CoreStrings.IDs.ContentIndexStatusDisabled)]
		Disabled,
		// Token: 0x04000A82 RID: 2690
		[LocDescription(CoreStrings.IDs.ContentIndexStatusAutoSuspended)]
		AutoSuspended,
		// Token: 0x04000A83 RID: 2691
		[LocDescription(CoreStrings.IDs.ContentIndexStatusHealthyAndUpgrading)]
		HealthyAndUpgrading
	}
}
