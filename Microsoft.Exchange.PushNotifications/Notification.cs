using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000007 RID: 7
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal abstract class Notification : BasicNotification
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002699 File Offset: 0x00000899
		public Notification(string appId, string recipientId, string identifier = null) : base(identifier)
		{
			this.AppId = appId;
			this.RecipientId = recipientId;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000026B0 File Offset: 0x000008B0
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000026B8 File Offset: 0x000008B8
		[DataMember(Name = "appId", EmitDefaultValue = false)]
		public string AppId { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000026C1 File Offset: 0x000008C1
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000026C9 File Offset: 0x000008C9
		[DataMember(Name = "recipientId", EmitDefaultValue = false)]
		public string RecipientId { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026D2 File Offset: 0x000008D2
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000026DA File Offset: 0x000008DA
		[DataMember(Name = "isMonitoring")]
		public bool IsMonitoring { get; protected set; }

		// Token: 0x0600002D RID: 45 RVA: 0x000026E4 File Offset: 0x000008E4
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("appId:").Append(this.AppId.ToNullableString()).Append("; ");
			sb.Append("recipientId:").Append(this.RecipientId.ToNullableString()).Append("; ");
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002744 File Offset: 0x00000944
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			if (string.IsNullOrWhiteSpace(this.AppId))
			{
				errors.Add(Strings.InvalidRecipientAppId);
			}
			if (string.IsNullOrWhiteSpace(this.RecipientId))
			{
				errors.Add(Strings.InvalidRecipientDeviceId);
			}
		}
	}
}
