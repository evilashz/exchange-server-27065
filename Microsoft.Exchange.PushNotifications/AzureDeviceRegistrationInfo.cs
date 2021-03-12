using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000018 RID: 24
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class AzureDeviceRegistrationInfo : Notification
	{
		// Token: 0x060000AB RID: 171 RVA: 0x0000382C File Offset: 0x00001A2C
		public AzureDeviceRegistrationInfo(string deviceId, string azureTag, string targetAppId, string serverChallenge, string hubName = null) : base(PushNotificationCannedApp.AzureDeviceRegistration.Name, deviceId, null)
		{
			this.Tag = azureTag;
			this.TargetAppId = targetAppId;
			this.HubName = hubName;
			this.ServerChallenge = serverChallenge;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000385E File Offset: 0x00001A5E
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003866 File Offset: 0x00001A66
		[DataMember(Name = "targetAppId", EmitDefaultValue = false)]
		public string TargetAppId { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000386F File Offset: 0x00001A6F
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003877 File Offset: 0x00001A77
		[DataMember(Name = "hubName", EmitDefaultValue = false)]
		public string HubName { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003880 File Offset: 0x00001A80
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003888 File Offset: 0x00001A88
		[DataMember(Name = "tag", EmitDefaultValue = false)]
		public string Tag { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003891 File Offset: 0x00001A91
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003899 File Offset: 0x00001A99
		[DataMember(Name = "challenge", EmitDefaultValue = false)]
		public string ServerChallenge { get; private set; }

		// Token: 0x060000B4 RID: 180 RVA: 0x000038A4 File Offset: 0x00001AA4
		public static AzureDeviceRegistrationInfo CreateMonitoringDeviceRegistrationInfo(string deviceId, string azureTag, string targetAppId, string hubName, string serverChallenge = null)
		{
			return new AzureDeviceRegistrationInfo(deviceId, azureTag, targetAppId, serverChallenge, hubName)
			{
				IsMonitoring = true
			};
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000038C8 File Offset: 0x00001AC8
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("targetAppId:").Append(this.TargetAppId.ToNullableString()).Append("; ");
			sb.Append("hubName:").Append(this.HubName.ToNullableString()).Append("; ");
			sb.Append("tag:").Append(this.Tag.ToNullableString()).Append("; ");
			sb.Append("challenge:").Append(this.ServerChallenge.ToNullableString()).Append("; ");
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003974 File Offset: 0x00001B74
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			if (string.IsNullOrWhiteSpace(this.TargetAppId))
			{
				errors.Add(Strings.InvalidTargetAppId(base.GetType().Name));
			}
		}
	}
}
