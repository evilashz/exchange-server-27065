using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000058 RID: 88
	internal enum WacRequestType
	{
		// Token: 0x0400013E RID: 318
		Unknown,
		// Token: 0x0400013F RID: 319
		CheckFile,
		// Token: 0x04000140 RID: 320
		GetFile,
		// Token: 0x04000141 RID: 321
		Lock,
		// Token: 0x04000142 RID: 322
		UnLock,
		// Token: 0x04000143 RID: 323
		RefreshLock,
		// Token: 0x04000144 RID: 324
		UnlockAndRelock,
		// Token: 0x04000145 RID: 325
		PutFile,
		// Token: 0x04000146 RID: 326
		Cobalt,
		// Token: 0x04000147 RID: 327
		DeleteFile
	}
}
