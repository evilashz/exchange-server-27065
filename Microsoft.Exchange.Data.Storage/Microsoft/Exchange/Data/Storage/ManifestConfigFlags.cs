using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000241 RID: 577
	[Flags]
	internal enum ManifestConfigFlags
	{
		// Token: 0x04001134 RID: 4404
		None = 0,
		// Token: 0x04001135 RID: 4405
		NoDeletions = 2,
		// Token: 0x04001136 RID: 4406
		NoSoftDeletions = 4,
		// Token: 0x04001137 RID: 4407
		ReadState = 8,
		// Token: 0x04001138 RID: 4408
		Associated = 16,
		// Token: 0x04001139 RID: 4409
		Normal = 32,
		// Token: 0x0400113A RID: 4410
		Catchup = 64,
		// Token: 0x0400113B RID: 4411
		NoChanges = 128,
		// Token: 0x0400113C RID: 4412
		OrderByDeliveryTime = 1048576
	}
}
