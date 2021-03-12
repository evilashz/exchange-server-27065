using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200005F RID: 95
	internal class AzureChallengeRequestNotification : PushNotification
	{
		// Token: 0x0600038B RID: 907 RVA: 0x0000C334 File Offset: 0x0000A534
		public AzureChallengeRequestNotification(string appId, string targetAppId, IAzureSasTokenProvider sasTokenProvider, AzureUriTemplate uriTemplate, string deviceId, AzureChallengeRequestPayload payload, string hubName) : base(appId, OrganizationId.ForestWideOrgId)
		{
			this.TargetAppId = targetAppId;
			this.AzureSasTokenProvider = sasTokenProvider;
			this.UriTemplate = uriTemplate;
			this.deviceId = deviceId;
			this.Payload = payload;
			this.HubName = hubName;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000C370 File Offset: 0x0000A570
		public override string RecipientId
		{
			get
			{
				return this.deviceId;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000C378 File Offset: 0x0000A578
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000C380 File Offset: 0x0000A580
		public AzureChallengeRequestPayload Payload { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000C389 File Offset: 0x0000A589
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000C391 File Offset: 0x0000A591
		public string TargetAppId { get; private set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000C39A File Offset: 0x0000A59A
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000C3A2 File Offset: 0x0000A5A2
		public IAzureSasTokenProvider AzureSasTokenProvider { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000C3AB File Offset: 0x0000A5AB
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000C3B3 File Offset: 0x0000A5B3
		public AzureUriTemplate UriTemplate { get; private set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public string SerializedPaylod { get; private set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000C3CD File Offset: 0x0000A5CD
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000C3D5 File Offset: 0x0000A5D5
		public string HubName { get; private set; }

		// Token: 0x06000399 RID: 921 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (this.AzureSasTokenProvider == null)
			{
				errors.Add(Strings.AzureChallengeEmptySasKey);
			}
			if (this.UriTemplate == null)
			{
				errors.Add(Strings.AzureChallengeEmptyUriTemplate);
			}
			if (string.IsNullOrWhiteSpace(this.deviceId))
			{
				errors.Add(Strings.AzureChallengeInvalidDeviceId(this.RecipientId));
			}
			if (this.Payload == null)
			{
				errors.Add(Strings.AzureChallengeMissingPayload);
			}
			if (!this.Payload.TargetPlatform.SupportsIssueRegistrationSecret())
			{
				errors.Add(Strings.AzureChallengeInvalidPlatformOnPayload(this.Payload.TargetPlatform.ToString()));
			}
			this.SerializedPaylod = this.ToAzureHubCreationFormat();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000C48C File Offset: 0x0000A68C
		protected override string InternalToFullString()
		{
			return string.Format("{0}; targetAppId:{1}; sasKey:{2}; uriTemplate:{3}; deviceId:{4}; payload:{5}; hubName:{6}", new object[]
			{
				base.InternalToFullString(),
				this.TargetAppId,
				this.AzureSasTokenProvider,
				this.UriTemplate,
				this.RecipientId,
				this.Payload.ToString(),
				this.HubName
			});
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		private string ToAzureHubCreationFormat()
		{
			AzureChallengeRequestPayloadWriter azureChallengeRequestPayloadWriter = new AzureChallengeRequestPayloadWriter();
			azureChallengeRequestPayloadWriter.AddDeviceId(this.RecipientId);
			if (this.Payload != null)
			{
				this.Payload.WriteAzureSecretRequestPayload(azureChallengeRequestPayloadWriter);
			}
			return azureChallengeRequestPayloadWriter.ToString();
		}

		// Token: 0x0400018B RID: 395
		private readonly string deviceId;
	}
}
