using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200006B RID: 107
	internal sealed class AzureDeviceRegistrationChannelSettings : PushNotificationSettingsBase
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000D44C File Offset: 0x0000B64C
		public AzureDeviceRegistrationChannelSettings(string appId, int requestTimeout, int requestStepTimeout, int maxDevicesRegisteredCacheSize, int backOffTimeInSeconds) : base(appId)
		{
			this.RequestTimeout = requestTimeout;
			this.RequestStepTimeout = requestStepTimeout;
			this.MaxDevicesRegistrationCacheSize = maxDevicesRegisteredCacheSize;
			this.BackOffTimeInSeconds = backOffTimeInSeconds;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000D473 File Offset: 0x0000B673
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000D47B File Offset: 0x0000B67B
		public int RequestTimeout { get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000D484 File Offset: 0x0000B684
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000D48C File Offset: 0x0000B68C
		public int RequestStepTimeout { get; private set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000D495 File Offset: 0x0000B695
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000D49D File Offset: 0x0000B69D
		public int MaxDevicesRegistrationCacheSize { get; private set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000D4A6 File Offset: 0x0000B6A6
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000D4AE File Offset: 0x0000B6AE
		public int BackOffTimeInSeconds { get; private set; }

		// Token: 0x060003E1 RID: 993 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			if (!PushNotificationCannedApp.AzureDeviceRegistration.Name.Equals(base.AppId))
			{
				errors.Add(Strings.ValidationErrorDeviceRegistrationAppId(base.AppId, PushNotificationCannedApp.AzureDeviceRegistration.Name));
			}
			if (this.RequestTimeout < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("RequestTimeout", this.RequestTimeout));
			}
			if (this.RequestStepTimeout < 0 || this.RequestStepTimeout > this.RequestTimeout)
			{
				errors.Add(Strings.ValidationErrorRangeInteger("RequestStepTimeout", 0, this.RequestTimeout, this.RequestStepTimeout));
			}
			if (this.MaxDevicesRegistrationCacheSize < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("MaxDevicesRegistrationCacheSize", this.MaxDevicesRegistrationCacheSize));
			}
			if (this.BackOffTimeInSeconds < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("BackOffTimeInSeconds", this.BackOffTimeInSeconds));
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000D588 File Offset: 0x0000B788
		protected override bool RunSuitabilityCheck()
		{
			return base.RunSuitabilityCheck();
		}

		// Token: 0x040001BE RID: 446
		public const int DefaultRequestTimeout = 60000;

		// Token: 0x040001BF RID: 447
		public const int DefaultRequestStepTimeout = 500;

		// Token: 0x040001C0 RID: 448
		public const int DefaultBackOffTimeInSeconds = 600;

		// Token: 0x040001C1 RID: 449
		public const int DefaultDevicesRegistrationCacheSize = 10000;
	}
}
