using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000D3 RID: 211
	internal class WebAppPublisherFactory : PushNotificationPublisherFactory
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x00015C44 File Offset: 0x00013E44
		public WebAppPublisherFactory(List<IPushNotificationMapping<WebAppNotification>> mappings = null)
		{
			this.Mappings = mappings;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00015C53 File Offset: 0x00013E53
		public override PushNotificationPlatform Platform
		{
			get
			{
				return PushNotificationPlatform.WebApp;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00015C56 File Offset: 0x00013E56
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00015C5E File Offset: 0x00013E5E
		private List<IPushNotificationMapping<WebAppNotification>> Mappings { get; set; }

		// Token: 0x060006E6 RID: 1766 RVA: 0x00015C68 File Offset: 0x00013E68
		public override PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings)
		{
			WebAppPublisherSettings webAppPublisherSettings = settings as WebAppPublisherSettings;
			if (webAppPublisherSettings == null)
			{
				throw new ArgumentException(string.Format("settings should be an WebAppPublisherSettings instance: {0}", (settings == null) ? "null" : settings.GetType().ToString()));
			}
			return new WebAppPublisher(webAppPublisherSettings, this.Mappings);
		}
	}
}
