using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000034 RID: 52
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class LocalUserNotificationBatch
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00004CDF File Offset: 0x00002EDF
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00004CE7 File Offset: 0x00002EE7
		[DataMember(Name = "notifications", IsRequired = true, EmitDefaultValue = false)]
		public List<LocalUserNotification> Notifications { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00004CF0 File Offset: 0x00002EF0
		public bool IsEmpty
		{
			get
			{
				return this.Notifications == null || this.Notifications.Count == 0;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004D0A File Offset: 0x00002F0A
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

		// Token: 0x06000151 RID: 337 RVA: 0x00004D21 File Offset: 0x00002F21
		public void Add(LocalUserNotification notification)
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			if (this.Notifications == null)
			{
				this.Notifications = new List<LocalUserNotification>();
			}
			this.Notifications.Add(notification);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004D4D File Offset: 0x00002F4D
		public string ToJson()
		{
			return JsonConverter.Serialize<LocalUserNotificationBatch>(this, null);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004D56 File Offset: 0x00002F56
		public string ToFullString()
		{
			return string.Format("{{ \"notifications\": {0} }}", this.Notifications.ToNullableString(null));
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004D6E File Offset: 0x00002F6E
		public override string ToString()
		{
			return string.Format("UserNotificationBatch: Count={0};", this.Count);
		}
	}
}
