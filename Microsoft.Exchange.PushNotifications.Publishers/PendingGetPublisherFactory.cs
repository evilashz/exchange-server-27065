using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000B7 RID: 183
	internal class PendingGetPublisherFactory : PushNotificationPublisherFactory
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x00013BEA File Offset: 0x00011DEA
		public PendingGetPublisherFactory(IPendingGetConnectionCache connectionCache, List<IPushNotificationMapping<PendingGetNotification>> mappings = null)
		{
			this.connectionCache = connectionCache;
			this.Mappings = mappings;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00013C00 File Offset: 0x00011E00
		public override PushNotificationPlatform Platform
		{
			get
			{
				return PushNotificationPlatform.PendingGet;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00013C03 File Offset: 0x00011E03
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00013C0B File Offset: 0x00011E0B
		private List<IPushNotificationMapping<PendingGetNotification>> Mappings { get; set; }

		// Token: 0x0600061F RID: 1567 RVA: 0x00013C14 File Offset: 0x00011E14
		public override PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings)
		{
			PendingGetPublisherSettings pendingGetPublisherSettings = settings as PendingGetPublisherSettings;
			if (pendingGetPublisherSettings == null)
			{
				throw new ArgumentException(string.Format("settings should be an PendingGetPublisherSettings instance: {0}", (settings == null) ? "null" : settings.GetType().ToString()));
			}
			return new PendingGetPublisher(pendingGetPublisherSettings, this.connectionCache, null, null, this.Mappings);
		}

		// Token: 0x0400030A RID: 778
		private IPendingGetConnectionCache connectionCache;
	}
}
