using System;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000003 RID: 3
	internal interface INotificationBrokerClient : IDisposable
	{
		// Token: 0x06000006 RID: 6
		void Subscribe(BrokerSubscription subscription);

		// Token: 0x06000007 RID: 7
		void Unsubscribe(BrokerSubscription subscription);

		// Token: 0x06000008 RID: 8
		void StartNotificationCallbacks(Action<BrokerNotification> notificationCallback);

		// Token: 0x06000009 RID: 9
		void StopNotificationCallbacks();
	}
}
