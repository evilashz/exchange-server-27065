using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x0200000B RID: 11
	internal enum MessageFlags
	{
		// Token: 0x04000009 RID: 9
		Read = 1,
		// Token: 0x0400000A RID: 10
		UnModified,
		// Token: 0x0400000B RID: 11
		Submit = 4,
		// Token: 0x0400000C RID: 12
		UnSent = 8,
		// Token: 0x0400000D RID: 13
		HasAttach = 16,
		// Token: 0x0400000E RID: 14
		FromMe = 32,
		// Token: 0x0400000F RID: 15
		Associated = 64,
		// Token: 0x04000010 RID: 16
		Resend = 128,
		// Token: 0x04000011 RID: 17
		RnPending = 256,
		// Token: 0x04000012 RID: 18
		NrnPending = 512,
		// Token: 0x04000013 RID: 19
		OriginX400 = 4096,
		// Token: 0x04000014 RID: 20
		OriginInternet = 8192,
		// Token: 0x04000015 RID: 21
		OriginMiscExt = 32768
	}
}
