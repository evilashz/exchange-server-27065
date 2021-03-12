using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x0200000C RID: 12
	internal enum MessageAction
	{
		// Token: 0x04000017 RID: 23
		Success,
		// Token: 0x04000018 RID: 24
		Retry,
		// Token: 0x04000019 RID: 25
		RetryQueue,
		// Token: 0x0400001A RID: 26
		NDR,
		// Token: 0x0400001B RID: 27
		Reroute,
		// Token: 0x0400001C RID: 28
		Throw,
		// Token: 0x0400001D RID: 29
		LogDuplicate,
		// Token: 0x0400001E RID: 30
		LogProcess,
		// Token: 0x0400001F RID: 31
		Skip,
		// Token: 0x04000020 RID: 32
		RetryMailboxServer
	}
}
