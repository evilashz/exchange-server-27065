using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000A5 RID: 165
	internal class GcmPublisherFactory : PushNotificationPublisherFactory
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x00012F32 File Offset: 0x00011132
		public GcmPublisherFactory(IThrottlingManager throttlingManager, List<IPushNotificationMapping<GcmNotification>> mappings = null)
		{
			this.ThrottlingManager = throttlingManager;
			this.Mappings = mappings;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00012F48 File Offset: 0x00011148
		public override PushNotificationPlatform Platform
		{
			get
			{
				return PushNotificationPlatform.GCM;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00012F4B File Offset: 0x0001114B
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x00012F53 File Offset: 0x00011153
		private IThrottlingManager ThrottlingManager { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00012F5C File Offset: 0x0001115C
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00012F64 File Offset: 0x00011164
		private List<IPushNotificationMapping<GcmNotification>> Mappings { get; set; }

		// Token: 0x060005C3 RID: 1475 RVA: 0x00012F70 File Offset: 0x00011170
		public override PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings)
		{
			GcmPublisherSettings gcmPublisherSettings = settings as GcmPublisherSettings;
			if (gcmPublisherSettings == null)
			{
				throw new ArgumentException(string.Format("settings should be an GcmPublisherSettings instance: {0}", (settings == null) ? "null" : settings.GetType().ToString()));
			}
			return new GcmPublisher(gcmPublisherSettings, this.ThrottlingManager, this.Mappings);
		}
	}
}
