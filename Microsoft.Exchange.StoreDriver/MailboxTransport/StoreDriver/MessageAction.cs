using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x0200000A RID: 10
	internal enum MessageAction
	{
		// Token: 0x04000037 RID: 55
		Success,
		// Token: 0x04000038 RID: 56
		Retry,
		// Token: 0x04000039 RID: 57
		RetryQueue,
		// Token: 0x0400003A RID: 58
		NDR,
		// Token: 0x0400003B RID: 59
		Reroute,
		// Token: 0x0400003C RID: 60
		Throw,
		// Token: 0x0400003D RID: 61
		LogDuplicate,
		// Token: 0x0400003E RID: 62
		LogProcess,
		// Token: 0x0400003F RID: 63
		Skip,
		// Token: 0x04000040 RID: 64
		RetryMailboxServer
	}
}
