using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization
{
	// Token: 0x0200005E RID: 94
	public enum CollectionItemType : byte
	{
		// Token: 0x0400020E RID: 526
		NotDefined,
		// Token: 0x0400020F RID: 527
		Short,
		// Token: 0x04000210 RID: 528
		Int,
		// Token: 0x04000211 RID: 529
		Long,
		// Token: 0x04000212 RID: 530
		Double,
		// Token: 0x04000213 RID: 531
		Guid,
		// Token: 0x04000214 RID: 532
		String,
		// Token: 0x04000215 RID: 533
		Blob
	}
}
