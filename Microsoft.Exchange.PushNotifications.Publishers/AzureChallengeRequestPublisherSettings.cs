using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000066 RID: 102
	internal class AzureChallengeRequestPublisherSettings : PushNotificationPublisherSettings
	{
		// Token: 0x060003BB RID: 955 RVA: 0x0000C81E File Offset: 0x0000AA1E
		public AzureChallengeRequestPublisherSettings(string appId, bool enabled, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout, AzureChallengeRequestChannelSettings channelSettings) : base(appId, enabled, minimumVersion, maximumVersion, queueSize, numberOfChannels, addTimeout)
		{
			ArgumentValidator.ThrowIfNull("channelSettings", channelSettings);
			this.ChannelSettings = channelSettings;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000C845 File Offset: 0x0000AA45
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000C84D File Offset: 0x0000AA4D
		public AzureChallengeRequestChannelSettings ChannelSettings { get; private set; }

		// Token: 0x060003BE RID: 958 RVA: 0x0000C856 File Offset: 0x0000AA56
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (!this.ChannelSettings.IsValid)
			{
				errors.AddRange(this.ChannelSettings.ValidationErrors);
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000C880 File Offset: 0x0000AA80
		protected override bool RunSuitabilityCheck()
		{
			bool flag = base.RunSuitabilityCheck();
			return this.ChannelSettings.IsSuitable && flag;
		}

		// Token: 0x040001A5 RID: 421
		public const bool DefaultEnabled = true;

		// Token: 0x040001A6 RID: 422
		public const Version DefaultMinimumVersion = null;

		// Token: 0x040001A7 RID: 423
		public const Version DefaultMaximumVersion = null;

		// Token: 0x040001A8 RID: 424
		public const int DefaultQueueSize = 10000;

		// Token: 0x040001A9 RID: 425
		public const int DefaultNumberOfChannels = 1;

		// Token: 0x040001AA RID: 426
		public const int DefaultAddTimeout = 15;
	}
}
