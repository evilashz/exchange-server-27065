using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001AF RID: 431
	internal interface INotificationHandler
	{
		// Token: 0x0600088E RID: 2190
		bool HasPendingNotifications();

		// Token: 0x0600088F RID: 2191
		void CollectNotifications(NotificationCollector collector);

		// Token: 0x06000890 RID: 2192
		void RegisterCallback(Action callback);

		// Token: 0x06000891 RID: 2193
		void CancelCallback();
	}
}
