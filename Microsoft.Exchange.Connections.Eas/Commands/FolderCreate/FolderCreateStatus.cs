using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderCreate
{
	// Token: 0x02000036 RID: 54
	[Flags]
	public enum FolderCreateStatus
	{
		// Token: 0x04000118 RID: 280
		Success = 1,
		// Token: 0x04000119 RID: 281
		FolderExists = 4098,
		// Token: 0x0400011A RID: 282
		ParentFolderNotFound = 4101,
		// Token: 0x0400011B RID: 283
		ServerError = 518,
		// Token: 0x0400011C RID: 284
		SyncKeyMismatchOrInvalid = 1033,
		// Token: 0x0400011D RID: 285
		IncorrectRequestFormat = 4106,
		// Token: 0x0400011E RID: 286
		UnknownError = 523,
		// Token: 0x0400011F RID: 287
		CodeUnknown = 268
	}
}
