using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200006D RID: 109
	internal class AzureDeviceRegistrationNotification : PushNotification
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000D712 File Offset: 0x0000B912
		public AzureDeviceRegistrationNotification(string appId, string targetAppId, IAzureSasTokenProvider sasTokenProvider, AzureUriTemplate uriTemplate, AzureDeviceRegistrationPayload payload, string hubName, string serverChallenge = null) : base(appId, OrganizationId.ForestWideOrgId)
		{
			this.TargetAppId = targetAppId;
			this.AzureSasTokenProvider = sasTokenProvider;
			this.UriTemplate = uriTemplate;
			this.Payload = payload;
			this.HubName = hubName;
			this.ServerChallenge = serverChallenge;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000D74E File Offset: 0x0000B94E
		public override string RecipientId
		{
			get
			{
				return this.azureTag;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000D756 File Offset: 0x0000B956
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000D75E File Offset: 0x0000B95E
		public string TargetAppId { get; private set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000D767 File Offset: 0x0000B967
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000D76F File Offset: 0x0000B96F
		public IAzureSasTokenProvider AzureSasTokenProvider { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000D778 File Offset: 0x0000B978
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000D780 File Offset: 0x0000B980
		public AzureUriTemplate UriTemplate { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000D789 File Offset: 0x0000B989
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000D791 File Offset: 0x0000B991
		public string HubName { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000D79A File Offset: 0x0000B99A
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000D7A2 File Offset: 0x0000B9A2
		public string ServerChallenge { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000D7AB File Offset: 0x0000B9AB
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000D7B3 File Offset: 0x0000B9B3
		public AzureDeviceRegistrationPayload Payload { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		public string SerializedPaylod { get; private set; }

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (this.AzureSasTokenProvider == null)
			{
				errors.Add(Strings.AzureDeviceEmptySasKey);
			}
			if (this.UriTemplate == null)
			{
				errors.Add(Strings.AzureDeviceEmptyUriTemplate);
			}
			if (this.Payload == null)
			{
				errors.Add(Strings.AzureDeviceMissingPayload);
			}
			else
			{
				if (string.IsNullOrWhiteSpace(this.Payload.DeviceId))
				{
					errors.Add(Strings.AzureDeviceInvalidDeviceId(this.Payload.DeviceId));
				}
				if (string.IsNullOrWhiteSpace(this.Payload.AzureTag))
				{
					errors.Add(Strings.AzureDeviceInvalidTag(this.Payload.AzureTag));
				}
				this.azureTag = this.Payload.AzureTag;
			}
			this.SerializedPaylod = this.ToAzureDeviceRegistrationFormat();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000D88C File Offset: 0x0000BA8C
		protected override string InternalToFullString()
		{
			return string.Format("{0}; targetAppId:{1}; sasKey:{2}; uriTemplate:{3}; deviceId:{4}; payload:{5}; hubName:{6}; challenge:{7}", new object[]
			{
				base.InternalToFullString(),
				this.TargetAppId,
				this.AzureSasTokenProvider,
				this.UriTemplate,
				this.RecipientId,
				this.Payload.ToString(),
				this.HubName,
				this.ServerChallenge
			});
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		private string ToAzureDeviceRegistrationFormat()
		{
			AzureDeviceRegistrationPayloadWriter azureDeviceRegistrationPayloadWriter = new AzureDeviceRegistrationPayloadWriter();
			if (this.Payload != null)
			{
				this.Payload.WriteAzureDeviceRegistrationPayload(azureDeviceRegistrationPayloadWriter);
			}
			return azureDeviceRegistrationPayloadWriter.ToString();
		}

		// Token: 0x040001C8 RID: 456
		private string azureTag;
	}
}
