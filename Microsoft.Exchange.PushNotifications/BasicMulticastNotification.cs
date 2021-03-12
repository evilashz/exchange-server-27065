using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000005 RID: 5
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal abstract class BasicMulticastNotification<TPayload, TRecipient> : MulticastNotification where TPayload : BasicDataContract where TRecipient : BasicNotificationRecipient
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000022C5 File Offset: 0x000004C5
		public BasicMulticastNotification(TPayload payload, List<TRecipient> recipients)
		{
			this.Payload = payload;
			this.Recipients = recipients;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022DB File Offset: 0x000004DB
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000022E3 File Offset: 0x000004E3
		[DataMember(Name = "payload", EmitDefaultValue = false)]
		public TPayload Payload { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022EC File Offset: 0x000004EC
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000022F4 File Offset: 0x000004F4
		[DataMember(Name = "recipients", EmitDefaultValue = false)]
		public List<TRecipient> Recipients { get; private set; }

		// Token: 0x06000018 RID: 24 RVA: 0x000024AC File Offset: 0x000006AC
		public override IEnumerable<Notification> GetFragments()
		{
			if (this.Recipients != null)
			{
				foreach (TRecipient recipient in this.Recipients)
				{
					yield return this.CreateFragment(this.Payload, recipient);
				}
			}
			yield break;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024C9 File Offset: 0x000006C9
		public void Validate()
		{
			if (!base.IsValid)
			{
				throw new InvalidNotificationException(base.ValidationErrors[0]);
			}
		}

		// Token: 0x0600001A RID: 26
		protected abstract Notification CreateFragment(TPayload payload, TRecipient recipient);

		// Token: 0x0600001B RID: 27 RVA: 0x00002504 File Offset: 0x00000704
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("payload:").Append(this.Payload.ToNullableString((TPayload x) => x.ToFullString())).Append("; ");
			sb.Append("recipients:").Append(this.Recipients.ToNullableString((TRecipient x) => x.ToFullString())).Append("; ");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000259E File Offset: 0x0000079E
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			if (this.Recipients == null || this.Recipients.Count <= 0)
			{
				errors.Add(Strings.InvalidRecipientsList);
			}
		}
	}
}
