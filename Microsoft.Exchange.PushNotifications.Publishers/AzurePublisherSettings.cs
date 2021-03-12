using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000052 RID: 82
	internal class AzurePublisherSettings : PushNotificationPublisherSettings
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0000AE34 File Offset: 0x00009034
		public AzurePublisherSettings(string appId, bool enabled, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout, bool isMultifactorRegistrationEnabled, AzureChannelSettings channelSettings) : base(appId, enabled, minimumVersion, maximumVersion, queueSize, numberOfChannels, addTimeout)
		{
			ArgumentValidator.ThrowIfNull("channelSettings", channelSettings);
			this.ChannelSettings = channelSettings;
			this.IsMultifactorRegistrationEnabled = isMultifactorRegistrationEnabled;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000AE63 File Offset: 0x00009063
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000AE6B File Offset: 0x0000906B
		public AzureChannelSettings ChannelSettings { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000AE74 File Offset: 0x00009074
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000AE7C File Offset: 0x0000907C
		public bool IsMultifactorRegistrationEnabled { get; private set; }

		// Token: 0x0600031E RID: 798 RVA: 0x0000AE85 File Offset: 0x00009085
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (!this.ChannelSettings.IsValid)
			{
				errors.AddRange(this.ChannelSettings.ValidationErrors);
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000AEAC File Offset: 0x000090AC
		protected override bool RunSuitabilityCheck()
		{
			bool flag = base.RunSuitabilityCheck();
			return this.ChannelSettings.IsSuitable && flag;
		}

		// Token: 0x04000148 RID: 328
		public const bool DefaultEnabled = true;

		// Token: 0x04000149 RID: 329
		public const Version DefaultMinimumVersion = null;

		// Token: 0x0400014A RID: 330
		public const Version DefaultMaximumVersion = null;

		// Token: 0x0400014B RID: 331
		public const int DefaultQueueSize = 10000;

		// Token: 0x0400014C RID: 332
		public const int DefaultNumberOfChannels = 1;

		// Token: 0x0400014D RID: 333
		public const int DefaultAddTimeout = 15;

		// Token: 0x0400014E RID: 334
		public const bool DefaultMultifactorRegistrationEnabled = false;
	}
}
