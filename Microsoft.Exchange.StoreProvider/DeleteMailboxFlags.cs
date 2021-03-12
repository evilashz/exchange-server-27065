using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200002F RID: 47
	[Flags]
	internal enum DeleteMailboxFlags
	{
		// Token: 0x04000329 RID: 809
		DeleteDSCache = 0,
		// Token: 0x0400032A RID: 810
		MailboxMoved = 1,
		// Token: 0x0400032B RID: 811
		HardDelete = 2,
		// Token: 0x0400032C RID: 812
		MailboxMoveFailed = 4,
		// Token: 0x0400032D RID: 813
		SoftDelete = 8,
		// Token: 0x0400032E RID: 814
		RemoveSoftDeleted = 16
	}
}
