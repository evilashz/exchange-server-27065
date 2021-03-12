using System;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync
{
	// Token: 0x020000E3 RID: 227
	[Serializable]
	public sealed class HotmailSubscriptionProxy : WindowsLiveSubscriptionProxy
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x000209FB File Offset: 0x0001EBFB
		public HotmailSubscriptionProxy() : this(new DeltaSyncAggregationSubscription())
		{
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00020A08 File Offset: 0x0001EC08
		internal HotmailSubscriptionProxy(DeltaSyncAggregationSubscription subscription) : base(subscription)
		{
		}
	}
}
