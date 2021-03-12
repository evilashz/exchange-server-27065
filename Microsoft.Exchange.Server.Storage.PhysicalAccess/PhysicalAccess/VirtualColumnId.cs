using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000089 RID: 137
	public enum VirtualColumnId
	{
		// Token: 0x040001D5 RID: 469
		PageNumber = 1,
		// Token: 0x040001D6 RID: 470
		DataSize,
		// Token: 0x040001D7 RID: 471
		LongValueDataSize,
		// Token: 0x040001D8 RID: 472
		OverheadSize,
		// Token: 0x040001D9 RID: 473
		LongValueOverheadSize,
		// Token: 0x040001DA RID: 474
		NonTaggedColumnCount,
		// Token: 0x040001DB RID: 475
		TaggedColumnCount,
		// Token: 0x040001DC RID: 476
		LongValueCount,
		// Token: 0x040001DD RID: 477
		MultiValueCount,
		// Token: 0x040001DE RID: 478
		CompressedColumnCount,
		// Token: 0x040001DF RID: 479
		CompressedDataSize,
		// Token: 0x040001E0 RID: 480
		CompressedLongValueDataSize
	}
}
