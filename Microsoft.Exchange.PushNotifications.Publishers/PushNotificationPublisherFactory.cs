using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000012 RID: 18
	internal abstract class PushNotificationPublisherFactory
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009D RID: 157
		public abstract PushNotificationPlatform Platform { get; }

		// Token: 0x0600009E RID: 158
		public abstract PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings);
	}
}
