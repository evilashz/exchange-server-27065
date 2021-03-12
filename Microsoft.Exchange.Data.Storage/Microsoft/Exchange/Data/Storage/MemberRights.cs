using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007C0 RID: 1984
	[Flags]
	internal enum MemberRights
	{
		// Token: 0x04002864 RID: 10340
		None = 0,
		// Token: 0x04002865 RID: 10341
		ReadAny = 1,
		// Token: 0x04002866 RID: 10342
		Create = 2,
		// Token: 0x04002867 RID: 10343
		EditOwned = 8,
		// Token: 0x04002868 RID: 10344
		DeleteOwned = 16,
		// Token: 0x04002869 RID: 10345
		EditAny = 32,
		// Token: 0x0400286A RID: 10346
		DeleteAny = 64,
		// Token: 0x0400286B RID: 10347
		CreateSubfolder = 128,
		// Token: 0x0400286C RID: 10348
		Owner = 256,
		// Token: 0x0400286D RID: 10349
		Contact = 512,
		// Token: 0x0400286E RID: 10350
		Visible = 1024,
		// Token: 0x0400286F RID: 10351
		FreeBusySimple = 2048,
		// Token: 0x04002870 RID: 10352
		FreeBusyDetailed = 4096
	}
}
