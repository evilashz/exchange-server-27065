using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001D1 RID: 465
	[Flags]
	internal enum RecipientFlags
	{
		// Token: 0x04000CED RID: 3309
		UnSendable = 0,
		// Token: 0x04000CEE RID: 3310
		Sendable = 1,
		// Token: 0x04000CEF RID: 3311
		Organizer = 2,
		// Token: 0x04000CF0 RID: 3312
		ExceptionalResponse = 16,
		// Token: 0x04000CF1 RID: 3313
		ExceptionalDeleted = 32,
		// Token: 0x04000CF2 RID: 3314
		Added = 64,
		// Token: 0x04000CF3 RID: 3315
		AddedOnSend = 128,
		// Token: 0x04000CF4 RID: 3316
		OriginalRecipient = 256,
		// Token: 0x04000CF5 RID: 3317
		EvaluatedForRoom = 512
	}
}
