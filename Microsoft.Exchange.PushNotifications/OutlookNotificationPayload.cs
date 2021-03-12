using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200002D RID: 45
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class OutlookNotificationPayload : BasicDataContract
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00004A0D File Offset: 0x00002C0D
		public OutlookNotificationPayload(string tenantId = null, byte[] data = null)
		{
			this.TenantId = tenantId;
			if (data != null)
			{
				this.Data = Convert.ToBase64String(data, 0, data.Length);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004A2F File Offset: 0x00002C2F
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00004A37 File Offset: 0x00002C37
		[DataMember(Name = "tenantId", EmitDefaultValue = false)]
		public string TenantId { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004A40 File Offset: 0x00002C40
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00004A48 File Offset: 0x00002C48
		[DataMember(Name = "data", EmitDefaultValue = false)]
		public string Data { get; private set; }

		// Token: 0x06000136 RID: 310 RVA: 0x00004A54 File Offset: 0x00002C54
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("tenantId:").Append(this.TenantId.ToNullableString()).Append("; ");
			sb.Append("data:").Append(this.Data.ToNullableString()).Append("; ");
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004AB4 File Offset: 0x00002CB4
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			Guid guid;
			if (!string.IsNullOrEmpty(this.TenantId) && !Guid.TryParse(this.TenantId, out guid))
			{
				errors.Add(Strings.InvalidTenantId(this.TenantId));
			}
			if (string.IsNullOrEmpty(this.Data))
			{
				errors.Add(Strings.OutlookInvalidPayloadData);
			}
		}
	}
}
