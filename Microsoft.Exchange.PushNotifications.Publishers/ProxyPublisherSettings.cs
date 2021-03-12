using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000C8 RID: 200
	internal class ProxyPublisherSettings : PushNotificationPublisherSettings
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x00015406 File Offset: 0x00013606
		public ProxyPublisherSettings(string appId, bool enabled, string hubName, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout, ProxyChannelSettings channelSettings) : base(appId, enabled, minimumVersion, maximumVersion, queueSize, numberOfChannels, addTimeout)
		{
			ArgumentValidator.ThrowIfNull("channelSettings", channelSettings);
			this.HubName = hubName;
			this.ChannelSettings = channelSettings;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00015435 File Offset: 0x00013635
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0001543D File Offset: 0x0001363D
		public string HubName { get; private set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00015446 File Offset: 0x00013646
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0001544E File Offset: 0x0001364E
		public ProxyChannelSettings ChannelSettings { get; private set; }

		// Token: 0x060006AA RID: 1706 RVA: 0x00015457 File Offset: 0x00013657
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (!this.ChannelSettings.IsValid)
			{
				errors.AddRange(this.ChannelSettings.ValidationErrors);
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00015480 File Offset: 0x00013680
		protected override bool RunSuitabilityCheck()
		{
			bool flag = base.RunSuitabilityCheck();
			return this.ChannelSettings.IsSuitable && flag;
		}

		// Token: 0x04000357 RID: 855
		public const bool DefaultEnabled = false;

		// Token: 0x04000358 RID: 856
		public const Version DefaultMinimumVersion = null;

		// Token: 0x04000359 RID: 857
		public const Version DefaultMaximumVersion = null;

		// Token: 0x0400035A RID: 858
		public const int DefaultQueueSize = 10000;

		// Token: 0x0400035B RID: 859
		public const int DefaultNumberOfChannels = 1;

		// Token: 0x0400035C RID: 860
		public const int DefaultAddTimeout = 15;
	}
}
