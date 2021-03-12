using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000015 RID: 21
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class AzureChallengeRequestInfo : Notification
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003515 File Offset: 0x00001715
		public AzureChallengeRequestInfo(string targetAppId, PushNotificationPlatform platform, string deviceId, string challenge = null, string hubName = null) : base(PushNotificationCannedApp.AzureChallengeRequest.Name, deviceId, null)
		{
			this.TargetAppId = targetAppId;
			this.Platform = new PushNotificationPlatform?(platform);
			this.DeviceChallenge = challenge;
			this.HubName = hubName;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000354C File Offset: 0x0000174C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003554 File Offset: 0x00001754
		[DataMember(Name = "targetAppId", EmitDefaultValue = false)]
		public string TargetAppId { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000355D File Offset: 0x0000175D
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003565 File Offset: 0x00001765
		[DataMember(Name = "challenge", EmitDefaultValue = false)]
		public string DeviceChallenge { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000356E File Offset: 0x0000176E
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003576 File Offset: 0x00001776
		[DataMember(Name = "hubName", EmitDefaultValue = false)]
		public string HubName { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000357F File Offset: 0x0000177F
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003587 File Offset: 0x00001787
		[DataMember(Name = "platform", EmitDefaultValue = false)]
		public PushNotificationPlatform? Platform { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003590 File Offset: 0x00001790
		public string DeviceId
		{
			get
			{
				return base.RecipientId;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003598 File Offset: 0x00001798
		public static AzureChallengeRequestInfo CreateMonitoringAzureChallengeRequestInfo(string targetAppId, PushNotificationPlatform platform, string deviceId, string challenge, string hubName = null)
		{
			return new AzureChallengeRequestInfo(targetAppId, platform, deviceId, challenge, hubName)
			{
				IsMonitoring = true
			};
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000035BC File Offset: 0x000017BC
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("targetAppId:").Append(this.TargetAppId.ToNullableString()).Append("; ");
			sb.Append("challenge:").Append(this.DeviceChallenge.ToNullableString()).Append("; ");
			sb.Append("hubName:").Append(this.HubName.ToNullableString()).Append("; ");
			sb.Append("platform:").Append(this.Platform.ToNullableString<PushNotificationPlatform>()).Append("; ");
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003668 File Offset: 0x00001868
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			if (string.IsNullOrWhiteSpace(this.TargetAppId))
			{
				errors.Add(Strings.InvalidTargetAppId(base.GetType().Name));
			}
			if (this.Platform != null && !this.Platform.Value.SupportsIssueRegistrationSecret())
			{
				errors.Add(Strings.InvalidPlatform);
			}
		}
	}
}
