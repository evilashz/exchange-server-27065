using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderSync
{
	// Token: 0x02000042 RID: 66
	[Flags]
	public enum FolderSyncStatus
	{
		// Token: 0x04000138 RID: 312
		Success = 1,
		// Token: 0x04000139 RID: 313
		ServerError = 518,
		// Token: 0x0400013A RID: 314
		SyncKeyMismatchOrInvalid = 1033,
		// Token: 0x0400013B RID: 315
		IncorrectRequestFormat = 4106,
		// Token: 0x0400013C RID: 316
		UnknownError = 523,
		// Token: 0x0400013D RID: 317
		CodeUnknown = 268,
		// Token: 0x0400013E RID: 318
		ServerBusy = 8302
	}
}
