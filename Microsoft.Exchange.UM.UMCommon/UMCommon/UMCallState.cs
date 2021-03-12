using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	public enum UMCallState
	{
		// Token: 0x04000099 RID: 153
		Idle,
		// Token: 0x0400009A RID: 154
		Connecting,
		// Token: 0x0400009B RID: 155
		Alerted,
		// Token: 0x0400009C RID: 156
		Connected,
		// Token: 0x0400009D RID: 157
		Disconnected,
		// Token: 0x0400009E RID: 158
		Incoming,
		// Token: 0x0400009F RID: 159
		Transferring,
		// Token: 0x040000A0 RID: 160
		Forwarding
	}
}
