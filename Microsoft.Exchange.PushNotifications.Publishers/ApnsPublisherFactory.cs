using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000038 RID: 56
	internal class ApnsPublisherFactory : PushNotificationPublisherFactory
	{
		// Token: 0x06000223 RID: 547 RVA: 0x000084ED File Offset: 0x000066ED
		public ApnsPublisherFactory(IApnsFeedbackProvider feedbackProvider, IThrottlingManager throttlingManager, List<IPushNotificationMapping<ApnsNotification>> mappings = null)
		{
			ArgumentValidator.ThrowIfNull("feedbackProvider", feedbackProvider);
			ArgumentValidator.ThrowIfNull("throttlingManager", throttlingManager);
			this.FeedbackProvider = feedbackProvider;
			this.ThrottlingManager = throttlingManager;
			this.Mappings = mappings;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00008520 File Offset: 0x00006720
		public override PushNotificationPlatform Platform
		{
			get
			{
				return PushNotificationPlatform.APNS;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00008523 File Offset: 0x00006723
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000852B File Offset: 0x0000672B
		private IApnsFeedbackProvider FeedbackProvider { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00008534 File Offset: 0x00006734
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000853C File Offset: 0x0000673C
		private IThrottlingManager ThrottlingManager { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00008545 File Offset: 0x00006745
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000854D File Offset: 0x0000674D
		private List<IPushNotificationMapping<ApnsNotification>> Mappings { get; set; }

		// Token: 0x0600022B RID: 555 RVA: 0x00008558 File Offset: 0x00006758
		public override PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings)
		{
			ApnsPublisherSettings apnsPublisherSettings = settings as ApnsPublisherSettings;
			if (apnsPublisherSettings == null)
			{
				throw new ArgumentException(string.Format("settings should be an ApnsPublisherSettings instance: {0}", (settings == null) ? "null" : settings.GetType().ToString()));
			}
			return new ApnsPublisher(apnsPublisherSettings, this.FeedbackProvider, this.ThrottlingManager, this.Mappings);
		}
	}
}
