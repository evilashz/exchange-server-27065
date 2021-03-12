using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000060 RID: 96
	[Flags]
	internal enum MessageStatusFlags : uint
	{
		// Token: 0x04000134 RID: 308
		None = 0U,
		// Token: 0x04000135 RID: 309
		Highlighted = 1U,
		// Token: 0x04000136 RID: 310
		Tagged = 2U,
		// Token: 0x04000137 RID: 311
		Hidden = 4U,
		// Token: 0x04000138 RID: 312
		DeleteMarked = 8U,
		// Token: 0x04000139 RID: 313
		Draft = 256U,
		// Token: 0x0400013A RID: 314
		Answered = 512U,
		// Token: 0x0400013B RID: 315
		InConflict = 2048U,
		// Token: 0x0400013C RID: 316
		RemoteDownload = 4096U,
		// Token: 0x0400013D RID: 317
		RemoteDelete = 8192U,
		// Token: 0x0400013E RID: 318
		MessageDeliveryNotificationSent = 16384U,
		// Token: 0x0400013F RID: 319
		MimeConversionFailed = 32768U
	}
}
