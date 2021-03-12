using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000004 RID: 4
	internal interface IPushNotificationMapping<T> where T : PushNotification
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11
		Type InputType { get; }

		// Token: 0x0600000C RID: 12
		bool TryMap(Notification notification, PushNotificationPublishingContext context, out T pushNotification);
	}
}
