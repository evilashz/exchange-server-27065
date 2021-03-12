using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000F0 RID: 240
	internal class WnsPublisherSettings : PushNotificationPublisherSettings
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x00017FC2 File Offset: 0x000161C2
		public WnsPublisherSettings(string appId, bool enabled, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout, WnsChannelSettings channelSettings) : base(appId, enabled, minimumVersion, maximumVersion, queueSize, numberOfChannels, addTimeout)
		{
			ArgumentValidator.ThrowIfNull("channelSettings", channelSettings);
			this.ChannelSettings = channelSettings;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00017FE9 File Offset: 0x000161E9
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00017FF1 File Offset: 0x000161F1
		public WnsChannelSettings ChannelSettings { get; private set; }

		// Token: 0x060007B6 RID: 1974 RVA: 0x00017FFA File Offset: 0x000161FA
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (!this.ChannelSettings.IsValid)
			{
				errors.AddRange(this.ChannelSettings.ValidationErrors);
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00018024 File Offset: 0x00016224
		protected override bool RunSuitabilityCheck()
		{
			bool flag = base.RunSuitabilityCheck();
			return this.ChannelSettings.IsSuitable && flag;
		}

		// Token: 0x04000436 RID: 1078
		public const bool DefaultEnabled = true;

		// Token: 0x04000437 RID: 1079
		public const Version DefaultMinimumVersion = null;

		// Token: 0x04000438 RID: 1080
		public const Version DefaultMaximumVersion = null;

		// Token: 0x04000439 RID: 1081
		public const int DefaultQueueSize = 10000;

		// Token: 0x0400043A RID: 1082
		public const int DefaultNumberOfChannels = 1;

		// Token: 0x0400043B RID: 1083
		public const int DefaultAddTimeout = 15;
	}
}
