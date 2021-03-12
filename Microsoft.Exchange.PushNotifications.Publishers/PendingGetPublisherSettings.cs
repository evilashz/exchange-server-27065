using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000B8 RID: 184
	internal class PendingGetPublisherSettings : PushNotificationPublisherSettings
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x00013C64 File Offset: 0x00011E64
		public PendingGetPublisherSettings(string appId, bool enabled, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout) : base(appId, enabled, minimumVersion, maximumVersion, queueSize, numberOfChannels, addTimeout)
		{
		}

		// Token: 0x0400030C RID: 780
		public const bool DefaultEnabled = true;

		// Token: 0x0400030D RID: 781
		public const Version DefaultMinimumVersion = null;

		// Token: 0x0400030E RID: 782
		public const Version DefaultMaximumVersion = null;

		// Token: 0x0400030F RID: 783
		public const int DefaultQueueSize = 10000;

		// Token: 0x04000310 RID: 784
		public const int DefaultNumberOfChannels = 1;

		// Token: 0x04000311 RID: 785
		public const int DefaultAddTimeout = 15;
	}
}
