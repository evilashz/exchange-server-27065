using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderDelete
{
	// Token: 0x0200003C RID: 60
	[Flags]
	public enum FolderDeleteStatus
	{
		// Token: 0x04000127 RID: 295
		Success = 1,
		// Token: 0x04000128 RID: 296
		SystemFolder = 4099,
		// Token: 0x04000129 RID: 297
		FolderNotFound = 4100,
		// Token: 0x0400012A RID: 298
		ServerError = 518,
		// Token: 0x0400012B RID: 299
		SyncKeyMismatchOrInvalid = 1033,
		// Token: 0x0400012C RID: 300
		IncorrectRequestFormat = 4106,
		// Token: 0x0400012D RID: 301
		UnknownError = 523,
		// Token: 0x0400012E RID: 302
		CodeUnknown = 268
	}
}
