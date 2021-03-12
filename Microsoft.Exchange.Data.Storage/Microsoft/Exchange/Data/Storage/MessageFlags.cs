using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200007D RID: 125
	[Flags]
	internal enum MessageFlags
	{
		// Token: 0x0400024F RID: 591
		None = 0,
		// Token: 0x04000250 RID: 592
		IsRead = 1,
		// Token: 0x04000251 RID: 593
		IsUnmodified = 2,
		// Token: 0x04000252 RID: 594
		HasBeenSubmitted = 4,
		// Token: 0x04000253 RID: 595
		IsDraft = 8,
		// Token: 0x04000254 RID: 596
		IsFromMe = 32,
		// Token: 0x04000255 RID: 597
		IsAssociated = 64,
		// Token: 0x04000256 RID: 598
		IsResend = 128,
		// Token: 0x04000257 RID: 599
		IsReadReceiptPending = 256,
		// Token: 0x04000258 RID: 600
		IsNotReadReceiptPending = 512,
		// Token: 0x04000259 RID: 601
		WasEverRead = 1024,
		// Token: 0x0400025A RID: 602
		IsRestricted = 2048,
		// Token: 0x0400025B RID: 603
		NeedSpecialRecipientProcessing = 131072
	}
}
