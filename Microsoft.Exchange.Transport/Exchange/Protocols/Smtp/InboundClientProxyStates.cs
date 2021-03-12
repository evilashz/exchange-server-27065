using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200043A RID: 1082
	public enum InboundClientProxyStates : byte
	{
		// Token: 0x040018FC RID: 6396
		None,
		// Token: 0x040018FD RID: 6397
		XProxyReceived,
		// Token: 0x040018FE RID: 6398
		XProxyReceivedAndAuthenticated
	}
}
