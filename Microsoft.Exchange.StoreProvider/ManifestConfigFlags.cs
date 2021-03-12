using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001CE RID: 462
	[Flags]
	internal enum ManifestConfigFlags
	{
		// Token: 0x04000624 RID: 1572
		NoChanges = 1,
		// Token: 0x04000625 RID: 1573
		NoDeletions = 2,
		// Token: 0x04000626 RID: 1574
		NoSoftDeletions = 4,
		// Token: 0x04000627 RID: 1575
		NoReadUnread = 8,
		// Token: 0x04000628 RID: 1576
		Associated = 16,
		// Token: 0x04000629 RID: 1577
		Normal = 32,
		// Token: 0x0400062A RID: 1578
		OrderByDeliveryTime = 64,
		// Token: 0x0400062B RID: 1579
		ReevaluateOnRestrictionChange = 128,
		// Token: 0x0400062C RID: 1580
		Catchup = 256,
		// Token: 0x0400062D RID: 1581
		Conversations = 512
	}
}
