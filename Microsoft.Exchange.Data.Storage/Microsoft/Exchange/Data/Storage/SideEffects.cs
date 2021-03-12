using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200023A RID: 570
	[Flags]
	internal enum SideEffects
	{
		// Token: 0x040010F5 RID: 4341
		None = 0,
		// Token: 0x040010F6 RID: 4342
		OpenToDelete = 1,
		// Token: 0x040010F7 RID: 4343
		DeleteFB = 2,
		// Token: 0x040010F8 RID: 4344
		RemoteItem = 4,
		// Token: 0x040010F9 RID: 4345
		NoFrame = 8,
		// Token: 0x040010FA RID: 4346
		CoerceToInbox = 16,
		// Token: 0x040010FB RID: 4347
		OpenToCopy = 32,
		// Token: 0x040010FC RID: 4348
		OpenToMove = 64,
		// Token: 0x040010FD RID: 4349
		Frame = 128,
		// Token: 0x040010FE RID: 4350
		OpenForCtxMenu = 256,
		// Token: 0x040010FF RID: 4351
		AbortSubmit = 512,
		// Token: 0x04001100 RID: 4352
		CannotUndoDelete = 1024,
		// Token: 0x04001101 RID: 4353
		CannotUndoCopy = 2048,
		// Token: 0x04001102 RID: 4354
		CannotUndoMove = 4096,
		// Token: 0x04001103 RID: 4355
		HasScript = 8192,
		// Token: 0x04001104 RID: 4356
		OpenToPermDelete = 16384
	}
}
