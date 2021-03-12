using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D19 RID: 3353
	[Flags]
	internal enum DistributionGroupExpansionError
	{
		// Token: 0x040050EF RID: 20719
		NoError = 0,
		// Token: 0x040050F0 RID: 20720
		ToGroupExpansionFailed = 1,
		// Token: 0x040050F1 RID: 20721
		CcGroupExpansionFailed = 2,
		// Token: 0x040050F2 RID: 20722
		BccGroupExpansionFailed = 4,
		// Token: 0x040050F3 RID: 20723
		ToGroupExpansionHitRecipientsLimit = 8,
		// Token: 0x040050F4 RID: 20724
		CcGroupExpansionHitRecipientsLimit = 16,
		// Token: 0x040050F5 RID: 20725
		BccGroupExpansionHitRecipientsLimit = 32,
		// Token: 0x040050F6 RID: 20726
		ToGroupExpansionHitDepthsLimit = 64,
		// Token: 0x040050F7 RID: 20727
		CcGroupExpansionHitDepthsLimit = 128,
		// Token: 0x040050F8 RID: 20728
		BccGroupExpansionHitDepthsLimit = 256
	}
}
