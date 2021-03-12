using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001EC RID: 492
	public enum ResultType : byte
	{
		// Token: 0x04000A46 RID: 2630
		AddScore,
		// Token: 0x04000A47 RID: 2631
		MarkAsSpam,
		// Token: 0x04000A48 RID: 2632
		SkipFiltering,
		// Token: 0x04000A49 RID: 2633
		AddFeatureScore,
		// Token: 0x04000A4A RID: 2634
		Reject,
		// Token: 0x04000A4B RID: 2635
		MarkAsHighRisk,
		// Token: 0x04000A4C RID: 2636
		SetPartition,
		// Token: 0x04000A4D RID: 2637
		SilentDrop,
		// Token: 0x04000A4E RID: 2638
		MarkAsBulk
	}
}
