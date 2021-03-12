using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000007 RID: 7
	[Flags]
	public enum DistributionGroupExpansionError
	{
		// Token: 0x04000017 RID: 23
		NoError = 0,
		// Token: 0x04000018 RID: 24
		ToGroupExpansionFailed = 1,
		// Token: 0x04000019 RID: 25
		CcGroupExpansionFailed = 2,
		// Token: 0x0400001A RID: 26
		BccGroupExpansionFailed = 4,
		// Token: 0x0400001B RID: 27
		ToGroupExpansionHitRecipientsLimit = 8,
		// Token: 0x0400001C RID: 28
		CcGroupExpansionHitRecipientsLimit = 16,
		// Token: 0x0400001D RID: 29
		BccGroupExpansionHitRecipientsLimit = 32,
		// Token: 0x0400001E RID: 30
		ToGroupExpansionHitDepthsLimit = 64,
		// Token: 0x0400001F RID: 31
		CcGroupExpansionHitDepthsLimit = 128,
		// Token: 0x04000020 RID: 32
		BccGroupExpansionHitDepthsLimit = 256
	}
}
