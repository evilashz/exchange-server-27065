using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000056 RID: 86
	internal enum MimeTokenId : short
	{
		// Token: 0x0400027A RID: 634
		None,
		// Token: 0x0400027B RID: 635
		Header,
		// Token: 0x0400027C RID: 636
		HeaderContinuation,
		// Token: 0x0400027D RID: 637
		EndOfHeaders,
		// Token: 0x0400027E RID: 638
		PartData,
		// Token: 0x0400027F RID: 639
		NestedStart,
		// Token: 0x04000280 RID: 640
		NestedNext,
		// Token: 0x04000281 RID: 641
		NestedEnd,
		// Token: 0x04000282 RID: 642
		InlineStart,
		// Token: 0x04000283 RID: 643
		InlineEnd,
		// Token: 0x04000284 RID: 644
		EmbeddedStart,
		// Token: 0x04000285 RID: 645
		EmbeddedEnd,
		// Token: 0x04000286 RID: 646
		EndOfFile
	}
}
