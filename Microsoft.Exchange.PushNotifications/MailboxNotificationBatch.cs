using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000020 RID: 32
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class MailboxNotificationBatch
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003D1D File Offset: 0x00001F1D
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003D25 File Offset: 0x00001F25
		[DataMember(Name = "notifications", IsRequired = true, EmitDefaultValue = false)]
		public List<MailboxNotification> Notifications { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003D2E File Offset: 0x00001F2E
		public bool IsEmpty
		{
			get
			{
				return this.Notifications == null || this.Notifications.Count == 0;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003D48 File Offset: 0x00001F48
		public int Count
		{
			get
			{
				if (this.Notifications != null)
				{
					return this.Notifications.Count;
				}
				return 0;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003D5F File Offset: 0x00001F5F
		public void Add(MailboxNotification notification)
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			if (this.Notifications == null)
			{
				this.Notifications = new List<MailboxNotification>();
			}
			this.Notifications.Add(notification);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003D8C File Offset: 0x00001F8C
		public bool IsMonitoring()
		{
			return this.Notifications != null && this.Notifications.Count > 0 && this.Notifications[0].Payload != null && this.Notifications[0].Payload.IsMonitoring;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003DDA File Offset: 0x00001FDA
		public string ToJson()
		{
			return JsonConverter.Serialize<MailboxNotificationBatch>(this, null);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003DE3 File Offset: 0x00001FE3
		public string ToFullString()
		{
			return string.Format("{{ \"notifications\": {0} }}", this.Notifications.ToNullableString(null));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003DFB File Offset: 0x00001FFB
		public override string ToString()
		{
			return string.Format("MailboxNotificationBatch: Count={0};", this.Count);
		}
	}
}
