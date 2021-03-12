using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000B4 RID: 180
	public interface INotificationSubscriptionList
	{
		// Token: 0x0600070A RID: 1802
		void RegisterSubscription(NotificationSubscription subscription);

		// Token: 0x0600070B RID: 1803
		void UnregisterSubscription(NotificationSubscription subscription);

		// Token: 0x0600070C RID: 1804
		void EnumerateSubscriptionsForEvent(NotificationPublishPhase phase, Context transactionContext, NotificationEvent nev, SubscriptionEnumerationCallback callback);
	}
}
