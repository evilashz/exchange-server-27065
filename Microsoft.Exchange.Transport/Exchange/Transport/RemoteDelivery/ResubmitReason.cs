using System;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003B6 RID: 950
	internal enum ResubmitReason
	{
		// Token: 0x040015AB RID: 5547
		Admin,
		// Token: 0x040015AC RID: 5548
		ConfigUpdate,
		// Token: 0x040015AD RID: 5549
		Inactivity,
		// Token: 0x040015AE RID: 5550
		Recovery,
		// Token: 0x040015AF RID: 5551
		UnreachableSameVersionHubs,
		// Token: 0x040015B0 RID: 5552
		Redirect,
		// Token: 0x040015B1 RID: 5553
		ShadowHeartbeatFailure,
		// Token: 0x040015B2 RID: 5554
		ShadowStateChange,
		// Token: 0x040015B3 RID: 5555
		OutboundConnectorChange
	}
}
