using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderUpdate
{
	// Token: 0x02000048 RID: 72
	[Flags]
	public enum FolderUpdateStatus
	{
		// Token: 0x04000148 RID: 328
		Success = 1,
		// Token: 0x04000149 RID: 329
		FolderExists = 4098,
		// Token: 0x0400014A RID: 330
		FolderNotFound = 4100,
		// Token: 0x0400014B RID: 331
		ParentFolderNotFound = 4101,
		// Token: 0x0400014C RID: 332
		ServerError = 518,
		// Token: 0x0400014D RID: 333
		SyncKeyMismatchOrInvalid = 1033,
		// Token: 0x0400014E RID: 334
		IncorrectRequestFormat = 4106,
		// Token: 0x0400014F RID: 335
		UnknownError = 523,
		// Token: 0x04000150 RID: 336
		CodeUnknown = 268
	}
}
