using System;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200000E RID: 14
	internal interface INotificationHandler
	{
		// Token: 0x06000054 RID: 84
		void SubscriptionRemoved(BrokerSubscription subscription);
	}
}
