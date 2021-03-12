using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200024D RID: 589
	public enum ConnectionLostPoint
	{
		// Token: 0x04000944 RID: 2372
		None,
		// Token: 0x04000945 RID: 2373
		OnConnect,
		// Token: 0x04000946 RID: 2374
		OnAuthenticate,
		// Token: 0x04000947 RID: 2375
		OnHelo,
		// Token: 0x04000948 RID: 2376
		OnStartTls,
		// Token: 0x04000949 RID: 2377
		OnHeloAfterStartTls,
		// Token: 0x0400094A RID: 2378
		OnMailFrom,
		// Token: 0x0400094B RID: 2379
		OnRcptTo,
		// Token: 0x0400094C RID: 2380
		OnData
	}
}
