using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000038 RID: 56
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class LocalUserNotificationPayload : UserNotificationPayload
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00004EC9 File Offset: 0x000030C9
		public LocalUserNotificationPayload(string notificationType, string data = null, string userId = null, string tenantId = null) : base(notificationType, data)
		{
			this.userId = userId;
			this.tenantId = tenantId;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00004EE2 File Offset: 0x000030E2
		public override string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004EEA File Offset: 0x000030EA
		public override string TenantId
		{
			get
			{
				return this.tenantId;
			}
		}

		// Token: 0x04000070 RID: 112
		[DataMember(Name = "userId", EmitDefaultValue = false)]
		private readonly string userId;

		// Token: 0x04000071 RID: 113
		[DataMember(Name = "tenantId", EmitDefaultValue = false)]
		private readonly string tenantId;
	}
}
