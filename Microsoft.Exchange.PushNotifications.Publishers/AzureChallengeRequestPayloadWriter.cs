using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000062 RID: 98
	internal class AzureChallengeRequestPayloadWriter
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x0000C5EA File Offset: 0x0000A7EA
		public void AddPlatform(PushNotificationPlatform platform)
		{
			ArgumentValidator.ThrowIfInvalidValue<PushNotificationPlatform>("platform", platform, (PushNotificationPlatform x) => x.SupportsIssueRegistrationSecret());
			this.targetPlatform = AzureChallengeRequestPayloadWriter.PlatformMapping[platform];
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000C625 File Offset: 0x0000A825
		public void AddDeviceId(string deviceId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("deviceId", deviceId);
			this.deviceId = deviceId;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000C639 File Offset: 0x0000A839
		public void AddChallenge(string challenge)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("challenge", challenge);
			this.deviceChallenge = challenge;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000C64D File Offset: 0x0000A84D
		public override string ToString()
		{
			return string.Format("{{\"Channel\":\"{0}\",\"ApplicationPlatform\":\"{1}\",\"DeviceChallenge\":\"{2}\"}}", this.deviceId, this.targetPlatform.ToString(), this.deviceChallenge);
		}

		// Token: 0x04000195 RID: 405
		private const string RequestBodyTemplate = "{{\"Channel\":\"{0}\",\"ApplicationPlatform\":\"{1}\",\"DeviceChallenge\":\"{2}\"}}";

		// Token: 0x04000196 RID: 406
		private static readonly Dictionary<PushNotificationPlatform, AzureChallengeRequestPayloadWriter.IssueSecretPlatform> PlatformMapping = new Dictionary<PushNotificationPlatform, AzureChallengeRequestPayloadWriter.IssueSecretPlatform>
		{
			{
				PushNotificationPlatform.APNS,
				AzureChallengeRequestPayloadWriter.IssueSecretPlatform.apple
			},
			{
				PushNotificationPlatform.WNS,
				AzureChallengeRequestPayloadWriter.IssueSecretPlatform.windows
			},
			{
				PushNotificationPlatform.GCM,
				AzureChallengeRequestPayloadWriter.IssueSecretPlatform.gcm
			}
		};

		// Token: 0x04000197 RID: 407
		private string deviceChallenge;

		// Token: 0x04000198 RID: 408
		private AzureChallengeRequestPayloadWriter.IssueSecretPlatform targetPlatform;

		// Token: 0x04000199 RID: 409
		private string deviceId;

		// Token: 0x02000063 RID: 99
		private enum IssueSecretPlatform
		{
			// Token: 0x0400019C RID: 412
			windows,
			// Token: 0x0400019D RID: 413
			apple,
			// Token: 0x0400019E RID: 414
			gcm,
			// Token: 0x0400019F RID: 415
			windowsphone,
			// Token: 0x040001A0 RID: 416
			adm
		}
	}
}
