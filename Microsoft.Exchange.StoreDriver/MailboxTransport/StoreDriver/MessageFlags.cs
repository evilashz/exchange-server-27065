using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000009 RID: 9
	internal enum MessageFlags
	{
		// Token: 0x04000029 RID: 41
		Read = 1,
		// Token: 0x0400002A RID: 42
		UnModified,
		// Token: 0x0400002B RID: 43
		Submit = 4,
		// Token: 0x0400002C RID: 44
		UnSent = 8,
		// Token: 0x0400002D RID: 45
		HasAttach = 16,
		// Token: 0x0400002E RID: 46
		FromMe = 32,
		// Token: 0x0400002F RID: 47
		Associated = 64,
		// Token: 0x04000030 RID: 48
		Resend = 128,
		// Token: 0x04000031 RID: 49
		RnPending = 256,
		// Token: 0x04000032 RID: 50
		NrnPending = 512,
		// Token: 0x04000033 RID: 51
		OriginX400 = 4096,
		// Token: 0x04000034 RID: 52
		OriginInternet = 8192,
		// Token: 0x04000035 RID: 53
		OriginMiscExt = 32768
	}
}
