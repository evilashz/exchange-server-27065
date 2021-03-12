using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000D4 RID: 212
	internal class WebAppPublisherSettings : PushNotificationPublisherSettings
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x00015CB0 File Offset: 0x00013EB0
		public WebAppPublisherSettings(string appId, bool enabled, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout, WebAppChannelSettings channelSettings) : base(appId, enabled, minimumVersion, maximumVersion, queueSize, numberOfChannels, addTimeout)
		{
			ArgumentValidator.ThrowIfNull("channelSettings", channelSettings);
			this.ChannelSettings = channelSettings;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00015CD7 File Offset: 0x00013ED7
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00015CDF File Offset: 0x00013EDF
		public WebAppChannelSettings ChannelSettings { get; private set; }

		// Token: 0x060006EA RID: 1770 RVA: 0x00015CE8 File Offset: 0x00013EE8
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (!this.ChannelSettings.IsValid)
			{
				errors.AddRange(this.ChannelSettings.ValidationErrors);
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00015D10 File Offset: 0x00013F10
		protected override bool RunSuitabilityCheck()
		{
			bool flag = base.RunSuitabilityCheck();
			return this.ChannelSettings.IsSuitable && flag;
		}

		// Token: 0x04000375 RID: 885
		public const bool DefaultEnabled = true;

		// Token: 0x04000376 RID: 886
		public const Version DefaultMinimumVersion = null;

		// Token: 0x04000377 RID: 887
		public const Version DefaultMaximumVersion = null;

		// Token: 0x04000378 RID: 888
		public const int DefaultQueueSize = 10000;

		// Token: 0x04000379 RID: 889
		public const int DefaultNumberOfChannels = 1;

		// Token: 0x0400037A RID: 890
		public const int DefaultAddTimeout = 15;
	}
}
