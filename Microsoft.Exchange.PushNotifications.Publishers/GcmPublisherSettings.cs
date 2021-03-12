using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000A6 RID: 166
	internal class GcmPublisherSettings : PushNotificationPublisherSettings
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x00012FBE File Offset: 0x000111BE
		public GcmPublisherSettings(string appId, bool enabled, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout, GcmChannelSettings channelSettings) : base(appId, enabled, minimumVersion, maximumVersion, queueSize, numberOfChannels, addTimeout)
		{
			ArgumentValidator.ThrowIfNull("channelSettings", channelSettings);
			this.ChannelSettings = channelSettings;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00012FE5 File Offset: 0x000111E5
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00012FED File Offset: 0x000111ED
		public GcmChannelSettings ChannelSettings { get; private set; }

		// Token: 0x060005C7 RID: 1479 RVA: 0x00012FF6 File Offset: 0x000111F6
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (!this.ChannelSettings.IsValid)
			{
				errors.AddRange(this.ChannelSettings.ValidationErrors);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00013020 File Offset: 0x00011220
		protected override bool RunSuitabilityCheck()
		{
			bool flag = base.RunSuitabilityCheck();
			return this.ChannelSettings.IsSuitable && flag;
		}

		// Token: 0x040002CB RID: 715
		public const bool DefaultEnabled = true;

		// Token: 0x040002CC RID: 716
		public const Version DefaultMinimumVersion = null;

		// Token: 0x040002CD RID: 717
		public const Version DefaultMaximumVersion = null;

		// Token: 0x040002CE RID: 718
		public const int DefaultQueueSize = 10000;

		// Token: 0x040002CF RID: 719
		public const int DefaultNumberOfChannels = 1;

		// Token: 0x040002D0 RID: 720
		public const int DefaultAddTimeout = 15;
	}
}
