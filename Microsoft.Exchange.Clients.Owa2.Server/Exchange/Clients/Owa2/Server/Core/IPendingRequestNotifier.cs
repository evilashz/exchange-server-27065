using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000136 RID: 310
	public interface IPendingRequestNotifier
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000A6E RID: 2670
		// (remove) Token: 0x06000A6F RID: 2671
		event DataAvailableEventHandler DataAvailable;

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A70 RID: 2672
		bool ShouldThrottle { get; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A71 RID: 2673
		string SubscriptionId { get; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A72 RID: 2674
		string ContextKey { get; }

		// Token: 0x06000A73 RID: 2675
		IList<NotificationPayloadBase> ReadDataAndResetState();
	}
}
