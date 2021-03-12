using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000027 RID: 39
	internal enum MDBThrottleType
	{
		// Token: 0x040000D3 RID: 211
		DynamicMDBThrottleDisabled,
		// Token: 0x040000D4 RID: 212
		PendingConnections,
		// Token: 0x040000D5 RID: 213
		ConnectionAcquireTimeout
	}
}
