using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000041 RID: 65
	internal enum NotificationListenerState
	{
		// Token: 0x0400010B RID: 267
		Idle,
		// Token: 0x0400010C RID: 268
		Connecting,
		// Token: 0x0400010D RID: 269
		ConnectingForDeletedNofications,
		// Token: 0x0400010E RID: 270
		Listening,
		// Token: 0x0400010F RID: 271
		Disconnecting
	}
}
