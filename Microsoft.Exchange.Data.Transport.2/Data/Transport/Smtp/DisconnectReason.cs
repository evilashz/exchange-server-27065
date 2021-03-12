using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000007 RID: 7
	public enum DisconnectReason
	{
		// Token: 0x0400001A RID: 26
		QuitVerb,
		// Token: 0x0400001B RID: 27
		Timeout,
		// Token: 0x0400001C RID: 28
		SenderDisconnected,
		// Token: 0x0400001D RID: 29
		TooManyErrors,
		// Token: 0x0400001E RID: 30
		DroppedSession,
		// Token: 0x0400001F RID: 31
		Remote,
		// Token: 0x04000020 RID: 32
		Local,
		// Token: 0x04000021 RID: 33
		SuppressLogging,
		// Token: 0x04000022 RID: 34
		None
	}
}
