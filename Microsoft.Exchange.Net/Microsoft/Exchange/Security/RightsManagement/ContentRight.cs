using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000A28 RID: 2600
	[Flags]
	internal enum ContentRight
	{
		// Token: 0x04003003 RID: 12291
		None = 0,
		// Token: 0x04003004 RID: 12292
		View = 1,
		// Token: 0x04003005 RID: 12293
		Edit = 2,
		// Token: 0x04003006 RID: 12294
		Print = 4,
		// Token: 0x04003007 RID: 12295
		Extract = 8,
		// Token: 0x04003008 RID: 12296
		ObjectModel = 16,
		// Token: 0x04003009 RID: 12297
		Owner = 32,
		// Token: 0x0400300A RID: 12298
		ViewRightsData = 64,
		// Token: 0x0400300B RID: 12299
		Forward = 128,
		// Token: 0x0400300C RID: 12300
		Reply = 256,
		// Token: 0x0400300D RID: 12301
		ReplyAll = 512,
		// Token: 0x0400300E RID: 12302
		Sign = 1024,
		// Token: 0x0400300F RID: 12303
		DocumentEdit = 2048,
		// Token: 0x04003010 RID: 12304
		Export = 4096,
		// Token: 0x04003011 RID: 12305
		EditRightsData = 8192
	}
}
