using System;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000010 RID: 16
	[Flags]
	internal enum Imap4State
	{
		// Token: 0x04000048 RID: 72
		None = 0,
		// Token: 0x04000049 RID: 73
		Nonauthenticated = 1,
		// Token: 0x0400004A RID: 74
		Authenticated = 2,
		// Token: 0x0400004B RID: 75
		Selected = 4,
		// Token: 0x0400004C RID: 76
		Idle = 8,
		// Token: 0x0400004D RID: 77
		Disconnected = 16,
		// Token: 0x0400004E RID: 78
		AuthenticatedButFailed = 32,
		// Token: 0x0400004F RID: 79
		AuthenticatedAsCafe = 64,
		// Token: 0x04000050 RID: 80
		All = 7
	}
}
