using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200002B RID: 43
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class OutlookNotificationBatch
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004954 File Offset: 0x00002B54
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000495C File Offset: 0x00002B5C
		[DataMember(Name = "notifications", IsRequired = true, EmitDefaultValue = false)]
		public List<OutlookNotification> Notifications { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00004965 File Offset: 0x00002B65
		public bool IsEmpty
		{
			get
			{
				return this.Notifications == null || this.Notifications.Count == 0;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000497F File Offset: 0x00002B7F
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

		// Token: 0x0600012B RID: 299 RVA: 0x00004996 File Offset: 0x00002B96
		public void Add(OutlookNotification notification)
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			if (this.Notifications == null)
			{
				this.Notifications = new List<OutlookNotification>();
			}
			this.Notifications.Add(notification);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000049C2 File Offset: 0x00002BC2
		public string ToJson()
		{
			return JsonConverter.Serialize<OutlookNotificationBatch>(this, null);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000049CB File Offset: 0x00002BCB
		public string ToFullString()
		{
			return string.Format("{{ \"notifications\": {0} }}", this.Notifications.ToNullableString(null));
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000049E3 File Offset: 0x00002BE3
		public override string ToString()
		{
			return string.Format("OutlookNotificationBatch: Count={0};", this.Count);
		}
	}
}
