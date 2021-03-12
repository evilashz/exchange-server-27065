using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200003C RID: 60
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class RemoteUserNotificationPayload : UserNotificationPayload
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00005071 File Offset: 0x00003271
		public RemoteUserNotificationPayload(string notificationType, string data = null) : base(notificationType, data)
		{
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000507B File Offset: 0x0000327B
		public override string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005083 File Offset: 0x00003283
		public override string TenantId
		{
			get
			{
				return this.tenantId;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000508B File Offset: 0x0000328B
		public void SetUserId(string userId)
		{
			this.userId = userId;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005094 File Offset: 0x00003294
		public void SetTenantId(string tenantId)
		{
			this.tenantId = tenantId;
		}

		// Token: 0x04000076 RID: 118
		public const string UserIdHeader = "X-PUN-UserId";

		// Token: 0x04000077 RID: 119
		public const string TenantIdHeader = "X-PUN-TenantId";

		// Token: 0x04000078 RID: 120
		private string userId;

		// Token: 0x04000079 RID: 121
		private string tenantId;
	}
}
