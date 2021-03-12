using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200003A RID: 58
	internal class ApnsPublisherSettings : PushNotificationPublisherSettings
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000880A File Offset: 0x00006A0A
		public ApnsPublisherSettings(string appId, bool enabled, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout, ApnsChannelSettings channelSettings) : base(appId, enabled, minimumVersion, maximumVersion, queueSize, numberOfChannels, addTimeout)
		{
			ArgumentValidator.ThrowIfNull("channelSettings", channelSettings);
			this.ChannelSettings = channelSettings;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00008831 File Offset: 0x00006A31
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00008839 File Offset: 0x00006A39
		public ApnsChannelSettings ChannelSettings { get; private set; }

		// Token: 0x06000240 RID: 576 RVA: 0x00008842 File Offset: 0x00006A42
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (!this.ChannelSettings.IsValid)
			{
				errors.AddRange(this.ChannelSettings.ValidationErrors);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000886C File Offset: 0x00006A6C
		protected override bool RunSuitabilityCheck()
		{
			bool flag = base.RunSuitabilityCheck();
			return this.ChannelSettings.IsSuitable && flag;
		}

		// Token: 0x040000DF RID: 223
		public const bool DefaultEnabled = true;

		// Token: 0x040000E0 RID: 224
		public const Version DefaultMinimumVersion = null;

		// Token: 0x040000E1 RID: 225
		public const Version DefaultMaximumVersion = null;

		// Token: 0x040000E2 RID: 226
		public const int DefaultQueueSize = 10000;

		// Token: 0x040000E3 RID: 227
		public const int DefaultNumberOfChannels = 1;

		// Token: 0x040000E4 RID: 228
		public const int DefaultAddTimeout = 15;
	}
}
