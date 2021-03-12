using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001FA RID: 506
	[Flags]
	internal enum MessageStatusFlags : uint
	{
		// Token: 0x04000E46 RID: 3654
		None = 0U,
		// Token: 0x04000E47 RID: 3655
		Highlighted = 1U,
		// Token: 0x04000E48 RID: 3656
		Tagged = 2U,
		// Token: 0x04000E49 RID: 3657
		Hidden = 4U,
		// Token: 0x04000E4A RID: 3658
		DeleteMarked = 8U,
		// Token: 0x04000E4B RID: 3659
		Draft = 256U,
		// Token: 0x04000E4C RID: 3660
		Answered = 512U,
		// Token: 0x04000E4D RID: 3661
		InConflict = 2048U,
		// Token: 0x04000E4E RID: 3662
		RemoteDownload = 4096U,
		// Token: 0x04000E4F RID: 3663
		RemoteDelete = 8192U,
		// Token: 0x04000E50 RID: 3664
		MessageDeliveryNotificationSent = 16384U,
		// Token: 0x04000E51 RID: 3665
		MimeConversionFailed = 32768U
	}
}
