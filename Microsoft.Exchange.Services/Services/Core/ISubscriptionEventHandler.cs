using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000239 RID: 569
	internal interface ISubscriptionEventHandler
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000ECA RID: 3786
		bool IsDisposed { get; }

		// Token: 0x06000ECB RID: 3787
		void EventsAvailable(StreamingSubscription subscription);

		// Token: 0x06000ECC RID: 3788
		void DisconnectSubscription(StreamingSubscription subscription, LocalizedException exception);
	}
}
