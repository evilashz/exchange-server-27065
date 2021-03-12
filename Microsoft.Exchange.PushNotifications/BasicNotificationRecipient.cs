using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000006 RID: 6
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class BasicNotificationRecipient : BasicDataContract
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000025C8 File Offset: 0x000007C8
		public BasicNotificationRecipient(string appId, string deviceId)
		{
			this.AppId = appId;
			this.DeviceId = deviceId;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025DE File Offset: 0x000007DE
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000025E6 File Offset: 0x000007E6
		[DataMember(Name = "appId", EmitDefaultValue = false, IsRequired = true)]
		public string AppId { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000025EF File Offset: 0x000007EF
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000025F7 File Offset: 0x000007F7
		[DataMember(Name = "deviceId", EmitDefaultValue = false, IsRequired = true)]
		public string DeviceId { get; private set; }

		// Token: 0x06000024 RID: 36 RVA: 0x00002600 File Offset: 0x00000800
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("appId:").Append(this.AppId.ToNullableString()).Append("; ");
			sb.Append("deviceId:").Append(this.DeviceId.ToNullableString()).Append("; ");
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002660 File Offset: 0x00000860
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			if (string.IsNullOrWhiteSpace(this.AppId))
			{
				errors.Add(Strings.InvalidRecipientAppId);
			}
			if (string.IsNullOrWhiteSpace(this.DeviceId))
			{
				errors.Add(Strings.InvalidRecipientDeviceId);
			}
		}
	}
}
