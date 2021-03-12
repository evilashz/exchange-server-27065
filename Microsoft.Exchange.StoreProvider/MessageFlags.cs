using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000028 RID: 40
	[Flags]
	internal enum MessageFlags : uint
	{
		// Token: 0x0400011E RID: 286
		None = 0U,
		// Token: 0x0400011F RID: 287
		Associated = 64U,
		// Token: 0x04000120 RID: 288
		FromMe = 32U,
		// Token: 0x04000121 RID: 289
		HasAttach = 16U,
		// Token: 0x04000122 RID: 290
		NrnPending = 512U,
		// Token: 0x04000123 RID: 291
		EverRead = 1024U,
		// Token: 0x04000124 RID: 292
		OriginInternet = 8192U,
		// Token: 0x04000125 RID: 293
		OriginMiscExt = 32768U,
		// Token: 0x04000126 RID: 294
		OriginX400 = 4096U,
		// Token: 0x04000127 RID: 295
		Read = 1U,
		// Token: 0x04000128 RID: 296
		Resend = 128U,
		// Token: 0x04000129 RID: 297
		RnPending = 256U,
		// Token: 0x0400012A RID: 298
		Submit = 4U,
		// Token: 0x0400012B RID: 299
		Unmodified = 2U,
		// Token: 0x0400012C RID: 300
		Unsent = 8U
	}
}
