using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000024 RID: 36
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class MailboxNotificationRecipient : BasicNotificationRecipient
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00003FDC File Offset: 0x000021DC
		public MailboxNotificationRecipient(string appId, string deviceId, DateTime lastSubscriptionUpdate, string installationId = null) : base(appId, deviceId)
		{
			this.LastSubscriptionUpdate = lastSubscriptionUpdate;
			this.InstallationId = installationId;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003FF5 File Offset: 0x000021F5
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00003FFD File Offset: 0x000021FD
		[DataMember(Name = "lastSubscriptionUpdate", EmitDefaultValue = false)]
		public DateTime LastSubscriptionUpdate { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004006 File Offset: 0x00002206
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000400E File Offset: 0x0000220E
		[DataMember(Name = "InstallationId", EmitDefaultValue = false)]
		public string InstallationId { get; private set; }

		// Token: 0x060000F8 RID: 248 RVA: 0x00004018 File Offset: 0x00002218
		internal static MailboxNotificationRecipient CreateMonitoringRecipient(string appId, int deviceId, PushNotificationPlatform platform)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("appId", appId);
			ArgumentValidator.ThrowIfNegative("deviceId", deviceId);
			switch (platform)
			{
			case PushNotificationPlatform.APNS:
				return new MailboxNotificationRecipient(appId, string.Format("{0:d64}", deviceId), DateTime.UtcNow, null);
			case PushNotificationPlatform.WNS:
				return new MailboxNotificationRecipient(appId, string.Format("http://127.0.0.1:0/send?id={0}", deviceId), DateTime.UtcNow, null);
			case PushNotificationPlatform.GCM:
			case PushNotificationPlatform.WebApp:
				return new MailboxNotificationRecipient(appId, deviceId.ToString(), DateTime.UtcNow, null);
			}
			throw new InvalidOperationException(string.Format("Platform {0} is not supported for monitoring", platform.ToString()));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000040C8 File Offset: 0x000022C8
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("lastSubscriptionUpdate:").Append(this.LastSubscriptionUpdate.ToString()).Append("; ");
			sb.Append("installationId:").Append(this.InstallationId ?? "null");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000412C File Offset: 0x0000232C
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			if ((ExDateTime)this.LastSubscriptionUpdate > ExDateTime.UtcNow)
			{
				errors.Add(Strings.InvalidMnRecipientLastSubscriptionUpdate(this.LastSubscriptionUpdate.ToString()));
			}
		}
	}
}
