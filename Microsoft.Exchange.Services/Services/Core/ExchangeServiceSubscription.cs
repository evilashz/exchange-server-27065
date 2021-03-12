using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002DB RID: 731
	internal abstract class ExchangeServiceSubscription : DisposeTrackableBase
	{
		// Token: 0x06001442 RID: 5186 RVA: 0x000654FC File Offset: 0x000636FC
		protected ExchangeServiceSubscription(string subscriptionId)
		{
			this.SubscriptionId = subscriptionId;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0006550B File Offset: 0x0006370B
		internal virtual void HandleNotification(Notification notification)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00065512 File Offset: 0x00063712
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x0006551A File Offset: 0x0006371A
		internal string SubscriptionId { get; private set; }
	}
}
