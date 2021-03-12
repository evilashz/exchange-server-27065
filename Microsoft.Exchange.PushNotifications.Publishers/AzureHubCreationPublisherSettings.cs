using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000086 RID: 134
	internal class AzureHubCreationPublisherSettings : PushNotificationPublisherSettings
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		public AzureHubCreationPublisherSettings(string appId, bool enabled, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout, AzureHubCreationChannelSettings channelSettings) : base(appId, enabled, minimumVersion, maximumVersion, queueSize, numberOfChannels, addTimeout)
		{
			ArgumentValidator.ThrowIfNull("channelSettings", channelSettings);
			this.ChannelSettings = channelSettings;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000F5CF File Offset: 0x0000D7CF
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0000F5D7 File Offset: 0x0000D7D7
		public AzureHubCreationChannelSettings ChannelSettings { get; private set; }

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (!this.ChannelSettings.IsValid)
			{
				errors.AddRange(this.ChannelSettings.ValidationErrors);
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000F608 File Offset: 0x0000D808
		protected override bool RunSuitabilityCheck()
		{
			bool flag = base.RunSuitabilityCheck();
			return this.ChannelSettings.IsSuitable && flag;
		}

		// Token: 0x04000244 RID: 580
		public const bool DefaultEnabled = true;

		// Token: 0x04000245 RID: 581
		public const Version DefaultMinimumVersion = null;

		// Token: 0x04000246 RID: 582
		public const Version DefaultMaximumVersion = null;

		// Token: 0x04000247 RID: 583
		public const int DefaultQueueSize = 10000;

		// Token: 0x04000248 RID: 584
		public const int DefaultNumberOfChannels = 1;

		// Token: 0x04000249 RID: 585
		public const int DefaultAddTimeout = 15;
	}
}
