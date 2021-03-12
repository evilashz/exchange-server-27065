using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000EF RID: 239
	internal class WnsPublisherFactory : PushNotificationPublisherFactory
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x00017F2B File Offset: 0x0001612B
		public WnsPublisherFactory(IThrottlingManager throttlingManager, List<IPushNotificationMapping<WnsNotification>> mappings = null)
		{
			ArgumentValidator.ThrowIfNull("throttlingManager", throttlingManager);
			this.ThrottlingManager = throttlingManager;
			this.Mappings = mappings;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00017F4C File Offset: 0x0001614C
		public override PushNotificationPlatform Platform
		{
			get
			{
				return PushNotificationPlatform.WNS;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00017F4F File Offset: 0x0001614F
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00017F57 File Offset: 0x00016157
		private List<IPushNotificationMapping<WnsNotification>> Mappings { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00017F60 File Offset: 0x00016160
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00017F68 File Offset: 0x00016168
		private IThrottlingManager ThrottlingManager { get; set; }

		// Token: 0x060007B2 RID: 1970 RVA: 0x00017F74 File Offset: 0x00016174
		public override PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings)
		{
			WnsPublisherSettings wnsPublisherSettings = settings as WnsPublisherSettings;
			if (wnsPublisherSettings == null)
			{
				throw new ArgumentException(string.Format("settings should be an WnsPublisherSettings instance: {0}", (settings == null) ? "null" : settings.GetType().ToString()));
			}
			return new WnsPublisher(wnsPublisherSettings, this.ThrottlingManager, this.Mappings);
		}
	}
}
