using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200005D RID: 93
	internal sealed class AzureChallengeRequestChannelSettings : PushNotificationSettingsBase
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000C0CF File Offset: 0x0000A2CF
		public AzureChallengeRequestChannelSettings(string appId, int requestTimeout, int requestStepTimeout, int backOffTimeInSeconds) : base(appId)
		{
			this.RequestTimeout = requestTimeout;
			this.RequestStepTimeout = requestStepTimeout;
			this.BackOffTimeInSeconds = backOffTimeInSeconds;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000C0EE File Offset: 0x0000A2EE
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000C0F6 File Offset: 0x0000A2F6
		public int RequestTimeout { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000C0FF File Offset: 0x0000A2FF
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000C107 File Offset: 0x0000A307
		public int RequestStepTimeout { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000C110 File Offset: 0x0000A310
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000C118 File Offset: 0x0000A318
		public int AuthenticateRetryDelay { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000C121 File Offset: 0x0000A321
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000C129 File Offset: 0x0000A329
		public int BackOffTimeInSeconds { get; private set; }

		// Token: 0x06000385 RID: 901 RVA: 0x0000C134 File Offset: 0x0000A334
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			if (!PushNotificationCannedApp.AzureChallengeRequest.Name.Equals(base.AppId))
			{
				errors.Add(Strings.ValidationErrorChallengeRequestAppId(base.AppId, PushNotificationCannedApp.AzureChallengeRequest.Name));
			}
			if (this.RequestTimeout < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("RequestTimeout", this.RequestTimeout));
			}
			if (this.RequestStepTimeout < 0 || this.RequestStepTimeout > this.RequestTimeout)
			{
				errors.Add(Strings.ValidationErrorRangeInteger("RequestStepTimeout", 0, this.RequestTimeout, this.RequestStepTimeout));
			}
			if (this.BackOffTimeInSeconds < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("BackOffTimeInSeconds", this.BackOffTimeInSeconds));
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000C1E5 File Offset: 0x0000A3E5
		protected override bool RunSuitabilityCheck()
		{
			return base.RunSuitabilityCheck();
		}

		// Token: 0x04000182 RID: 386
		public const int DefaultRequestTimeout = 60000;

		// Token: 0x04000183 RID: 387
		public const int DefaultRequestStepTimeout = 500;

		// Token: 0x04000184 RID: 388
		public const int DefaultBackOffTimeInSeconds = 600;
	}
}
