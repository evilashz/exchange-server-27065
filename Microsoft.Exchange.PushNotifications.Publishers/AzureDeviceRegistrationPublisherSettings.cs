using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000073 RID: 115
	internal class AzureDeviceRegistrationPublisherSettings : PushNotificationPublisherSettings
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000DBD2 File Offset: 0x0000BDD2
		public AzureDeviceRegistrationPublisherSettings(string appId, bool enabled, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout, AzureDeviceRegistrationChannelSettings channelSettings) : base(appId, enabled, minimumVersion, maximumVersion, queueSize, numberOfChannels, addTimeout)
		{
			ArgumentValidator.ThrowIfNull("channelSettings", channelSettings);
			this.ChannelSettings = channelSettings;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000DBF9 File Offset: 0x0000BDF9
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000DC01 File Offset: 0x0000BE01
		public AzureDeviceRegistrationChannelSettings ChannelSettings { get; private set; }

		// Token: 0x0600041C RID: 1052 RVA: 0x0000DC0A File Offset: 0x0000BE0A
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (!this.ChannelSettings.IsValid)
			{
				errors.AddRange(this.ChannelSettings.ValidationErrors);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000DC34 File Offset: 0x0000BE34
		protected override bool RunSuitabilityCheck()
		{
			bool flag = base.RunSuitabilityCheck();
			return this.ChannelSettings.IsSuitable && flag;
		}

		// Token: 0x040001DC RID: 476
		public const bool DefaultEnabled = true;

		// Token: 0x040001DD RID: 477
		public const Version DefaultMinimumVersion = null;

		// Token: 0x040001DE RID: 478
		public const Version DefaultMaximumVersion = null;

		// Token: 0x040001DF RID: 479
		public const int DefaultQueueSize = 10000;

		// Token: 0x040001E0 RID: 480
		public const int DefaultNumberOfChannels = 1;

		// Token: 0x040001E1 RID: 481
		public const int DefaultAddTimeout = 15;
	}
}
