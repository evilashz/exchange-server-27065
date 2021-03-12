using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect
{
	// Token: 0x020000D1 RID: 209
	public enum ConnectState
	{
		// Token: 0x04000358 RID: 856
		Disconnected,
		// Token: 0x04000359 RID: 857
		Connected,
		// Token: 0x0400035A RID: 858
		ConnectedNeedsToken,
		// Token: 0x0400035B RID: 859
		Disabled,
		// Token: 0x0400035C RID: 860
		Delayed
	}
}
