using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000051 RID: 81
	internal class AzurePublisherFactory : PushNotificationPublisherFactory
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0000ADC6 File Offset: 0x00008FC6
		public AzurePublisherFactory(List<IPushNotificationMapping<AzureNotification>> mappings = null)
		{
			this.Mappings = mappings;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000ADD5 File Offset: 0x00008FD5
		public override PushNotificationPlatform Platform
		{
			get
			{
				return PushNotificationPlatform.Azure;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		private List<IPushNotificationMapping<AzureNotification>> Mappings { get; set; }

		// Token: 0x06000318 RID: 792 RVA: 0x0000ADEC File Offset: 0x00008FEC
		public override PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings)
		{
			AzurePublisherSettings azurePublisherSettings = settings as AzurePublisherSettings;
			if (azurePublisherSettings == null)
			{
				throw new ArgumentException(string.Format("settings should be an AzurePublisherFactory instance: {0}", (settings == null) ? "null" : settings.GetType().ToString()));
			}
			return new AzurePublisher(azurePublisherSettings, this.Mappings);
		}
	}
}
