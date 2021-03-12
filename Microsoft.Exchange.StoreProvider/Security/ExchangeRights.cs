using System;
using System.Runtime.InteropServices;

namespace Microsoft.Mapi.Security
{
	// Token: 0x02000257 RID: 599
	[ComVisible(false)]
	[Flags]
	internal enum ExchangeRights
	{
		// Token: 0x0400108F RID: 4239
		None = 0,
		// Token: 0x04001090 RID: 4240
		fReadAny = 1,
		// Token: 0x04001091 RID: 4241
		fCreate = 2,
		// Token: 0x04001092 RID: 4242
		fEditOwned = 8,
		// Token: 0x04001093 RID: 4243
		fDeleteOwned = 16,
		// Token: 0x04001094 RID: 4244
		fEditAny = 32,
		// Token: 0x04001095 RID: 4245
		fDeleteAny = 64,
		// Token: 0x04001096 RID: 4246
		fCreateSubfolder = 128,
		// Token: 0x04001097 RID: 4247
		fOwner = 256,
		// Token: 0x04001098 RID: 4248
		fContact = 512,
		// Token: 0x04001099 RID: 4249
		fVisible = 1024,
		// Token: 0x0400109A RID: 4250
		frightsFreeBusySimple = 2048,
		// Token: 0x0400109B RID: 4251
		frightsFreeBusyDetailed = 4096,
		// Token: 0x0400109C RID: 4252
		ReadWrite = 33,
		// Token: 0x0400109D RID: 4253
		AllRights = 1531,
		// Token: 0x0400109E RID: 4254
		AllFreeBusyRights = 6144
	}
}
