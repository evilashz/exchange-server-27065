using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007E7 RID: 2023
	[Flags]
	internal enum ForwardSyncDivergenceProvisioningFlags
	{
		// Token: 0x0400428C RID: 17036
		None = 0,
		// Token: 0x0400428D RID: 17037
		Temporary = 1,
		// Token: 0x0400428E RID: 17038
		IncrementalOnly = 2,
		// Token: 0x0400428F RID: 17039
		LinkRelated = 4,
		// Token: 0x04004290 RID: 17040
		IgnoredInHaltCondition = 8,
		// Token: 0x04004291 RID: 17041
		TenantWideDivergence = 16,
		// Token: 0x04004292 RID: 17042
		ValidationDivergence = 32,
		// Token: 0x04004293 RID: 17043
		Retriable = 64
	}
}
