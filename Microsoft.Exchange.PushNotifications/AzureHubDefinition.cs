using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200001D RID: 29
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class AzureHubDefinition : Notification
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00003C58 File Offset: 0x00001E58
		public AzureHubDefinition(string hubName, string targetAppId) : base(PushNotificationCannedApp.AzureHubCreation.Name, hubName, null)
		{
			this.TargetAppId = targetAppId;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003C73 File Offset: 0x00001E73
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003C7B File Offset: 0x00001E7B
		[DataMember(Name = "targetAppId", EmitDefaultValue = false)]
		public string TargetAppId { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003C84 File Offset: 0x00001E84
		public string HubName
		{
			get
			{
				return base.RecipientId;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003C8C File Offset: 0x00001E8C
		public static AzureHubDefinition CreateMonitoringHubDefinition(string hubName, string targetAppId)
		{
			return new AzureHubDefinition(hubName, targetAppId)
			{
				IsMonitoring = true
			};
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003CA9 File Offset: 0x00001EA9
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("targetAppId:").Append(this.TargetAppId.ToNullableString()).Append("; ");
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003CD8 File Offset: 0x00001ED8
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
