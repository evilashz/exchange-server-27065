using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000AB RID: 171
	internal interface IPendingGetConnection
	{
		// Token: 0x060005E2 RID: 1506
		bool FireUnseenEmailNotification(int unseenCount, int notificationId);

		// Token: 0x060005E3 RID: 1507
		void SubscribeToUnseenEmailNotification(PendingGetContext pendingGetContext, long timeoutInMilliseconds, int latestUnseenEmailNotificationId);
	}
}
