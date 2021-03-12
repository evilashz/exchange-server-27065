using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000037 RID: 55
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal abstract class UserNotificationPayload : BasicDataContract
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00004DA3 File Offset: 0x00002FA3
		public UserNotificationPayload(string notificationType, string data = null)
		{
			this.NotificationType = notificationType;
			this.Data = data;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00004DB9 File Offset: 0x00002FB9
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00004DC1 File Offset: 0x00002FC1
		[DataMember(Name = "notificationType", EmitDefaultValue = false)]
		public string NotificationType { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004DCA File Offset: 0x00002FCA
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00004DD2 File Offset: 0x00002FD2
		[DataMember(Name = "data", EmitDefaultValue = false)]
		public string Data { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600015D RID: 349
		public abstract string UserId { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600015E RID: 350
		public abstract string TenantId { get; }

		// Token: 0x0600015F RID: 351 RVA: 0x00004DDC File Offset: 0x00002FDC
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("notificationType:").Append(this.NotificationType.ToNullableString()).Append("; ");
			sb.Append("data:").Append(this.Data.ToNullableString()).Append("; ");
			sb.Append("userId:").Append(this.UserId.ToNullableString()).Append("; ");
			sb.Append("tenantId:").Append(this.TenantId.ToNullableString()).Append("; ");
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004E88 File Offset: 0x00003088
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			Guid guid;
			if (!string.IsNullOrEmpty(this.TenantId) && !Guid.TryParse(this.TenantId, out guid))
			{
				errors.Add(Strings.InvalidTenantId(this.TenantId));
			}
		}
	}
}
