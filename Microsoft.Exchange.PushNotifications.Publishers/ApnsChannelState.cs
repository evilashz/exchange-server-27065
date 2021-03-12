using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200001C RID: 28
	internal enum ApnsChannelState
	{
		// Token: 0x04000049 RID: 73
		Init,
		// Token: 0x0400004A RID: 74
		Connecting,
		// Token: 0x0400004B RID: 75
		DelayingConnect,
		// Token: 0x0400004C RID: 76
		Authenticating,
		// Token: 0x0400004D RID: 77
		Reading,
		// Token: 0x0400004E RID: 78
		Sending,
		// Token: 0x0400004F RID: 79
		Waiting
	}
}
