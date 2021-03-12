using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000023 RID: 35
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class MailboxNotificationPayload : BasicDataContract
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00003E25 File Offset: 0x00002025
		public MailboxNotificationPayload(string tenantId = null, int? unseenEmailCount = null, BackgroundSyncType backgroundSyncType = BackgroundSyncType.None, string language = null)
		{
			this.TenantId = tenantId;
			this.UnseenEmailCount = unseenEmailCount;
			this.BackgroundSyncType = backgroundSyncType;
			this.Language = language;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003E4A File Offset: 0x0000204A
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003E52 File Offset: 0x00002052
		[DataMember(Name = "tenantId", EmitDefaultValue = false)]
		public string TenantId { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003E5B File Offset: 0x0000205B
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003E63 File Offset: 0x00002063
		[DataMember(Name = "unseenEmailCount", EmitDefaultValue = false)]
		public int? UnseenEmailCount { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003E6C File Offset: 0x0000206C
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00003E74 File Offset: 0x00002074
		[DataMember(Name = "language", EmitDefaultValue = false)]
		public string Language { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003E7D File Offset: 0x0000207D
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00003E85 File Offset: 0x00002085
		[DataMember(Name = "backgroundSyncType", EmitDefaultValue = false)]
		public BackgroundSyncType BackgroundSyncType { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003E8E File Offset: 0x0000208E
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00003E96 File Offset: 0x00002096
		[DataMember(Name = "isMonitoring", EmitDefaultValue = false)]
		public bool IsMonitoring { get; private set; }

		// Token: 0x060000F0 RID: 240 RVA: 0x00003EA0 File Offset: 0x000020A0
		internal static MailboxNotificationPayload CreateMonitoringPayload(string monitoringTenantId = "")
		{
			return new MailboxNotificationPayload(monitoringTenantId, new int?(1), BackgroundSyncType.None, null)
			{
				IsMonitoring = true
			};
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003EC4 File Offset: 0x000020C4
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("tenantId:").Append(this.TenantId.ToNullableString()).Append("; ");
			sb.Append("unseenEmailCount:").Append(this.UnseenEmailCount.ToNullableString<int>()).Append("; ");
			sb.Append("language:").Append(this.Language.ToNullableString()).Append("; ");
			sb.Append("backgroundSyncType:").Append(this.BackgroundSyncType.ToString()).Append("; ");
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003F78 File Offset: 0x00002178
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			if (this.UnseenEmailCount == null && this.BackgroundSyncType != BackgroundSyncType.None)
			{
				errors.Add(Strings.InvalidMnPayloadContent);
			}
			Guid guid;
			if (!string.IsNullOrEmpty(this.TenantId) && !Guid.TryParse(this.TenantId, out guid))
			{
				errors.Add(Strings.InvalidTenantId(this.TenantId));
			}
		}
	}
}
