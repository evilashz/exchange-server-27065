using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000C7 RID: 199
	internal class ProxyPublisherFactory : PushNotificationPublisherFactory
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000153B6 File Offset: 0x000135B6
		public override PushNotificationPlatform Platform
		{
			get
			{
				return PushNotificationPlatform.Proxy;
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000153BC File Offset: 0x000135BC
		public override PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings)
		{
			ProxyPublisherSettings proxyPublisherSettings = settings as ProxyPublisherSettings;
			if (proxyPublisherSettings == null)
			{
				throw new ArgumentException(string.Format("settings should be an ProxyPublisherFactory instance: {0}", (settings == null) ? "null" : settings.GetType().ToString()));
			}
			return new ProxyPublisher(proxyPublisherSettings);
		}
	}
}
