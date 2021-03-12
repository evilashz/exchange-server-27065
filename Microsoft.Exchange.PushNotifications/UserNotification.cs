using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000032 RID: 50
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal abstract class UserNotification<TPayload> : BasicMulticastNotification<TPayload, UserNotificationRecipient> where TPayload : UserNotificationPayload
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00004C74 File Offset: 0x00002E74
		public UserNotification(string workloadId, TPayload payload, List<UserNotificationRecipient> recipients) : base(payload, recipients)
		{
			this.WorkloadId = workloadId;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004C85 File Offset: 0x00002E85
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00004C8D File Offset: 0x00002E8D
		[DataMember(Name = "workloadId", EmitDefaultValue = false)]
		public string WorkloadId { get; private set; }

		// Token: 0x0600014A RID: 330 RVA: 0x00004C96 File Offset: 0x00002E96
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("workloadId:").Append(this.WorkloadId.ToNullableString()).Append("; ");
		}
	}
}
