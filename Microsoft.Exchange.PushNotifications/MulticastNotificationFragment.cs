using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000008 RID: 8
	internal class MulticastNotificationFragment<TPayload, TRecipient> : Notification where TPayload : BasicDataContract where TRecipient : BasicNotificationRecipient
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000277D File Offset: 0x0000097D
		public MulticastNotificationFragment(string notificationId, TPayload payload, TRecipient recipient) : this(new MulticastNotificationFragment<TPayload, TRecipient>.FragmentId<TRecipient>(notificationId, recipient), payload, recipient)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002790 File Offset: 0x00000990
		private MulticastNotificationFragment(MulticastNotificationFragment<TPayload, TRecipient>.FragmentId<TRecipient> fragmentId, TPayload payload, TRecipient recipient) : base(fragmentId.AppId, fragmentId.RecipientId, fragmentId.Identifier)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("notificationId", fragmentId.NotificationId);
			this.NotificationIdentifier = fragmentId.NotificationId;
			this.Payload = payload;
			this.Recipient = recipient;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027DF File Offset: 0x000009DF
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000027E7 File Offset: 0x000009E7
		public string NotificationIdentifier { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000027F0 File Offset: 0x000009F0
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000027F8 File Offset: 0x000009F8
		public TPayload Payload { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002801 File Offset: 0x00000A01
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002809 File Offset: 0x00000A09
		public TRecipient Recipient { get; private set; }

		// Token: 0x06000037 RID: 55 RVA: 0x00002814 File Offset: 0x00000A14
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("notificationId:").Append(this.NotificationIdentifier).Append("; ");
			StringBuilder stringBuilder = sb.Append("payload:");
			TPayload payload = this.Payload;
			stringBuilder.Append(payload.ToFullString()).Append("; ");
			StringBuilder stringBuilder2 = sb.Append("recipients:");
			TRecipient recipient = this.Recipient;
			stringBuilder2.Append(recipient.ToFullString()).Append("; ");
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028A8 File Offset: 0x00000AA8
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			if (this.Payload == null)
			{
				errors.Add(Strings.InvalidPayload);
			}
			else
			{
				TPayload payload = this.Payload;
				if (!payload.IsValid)
				{
					TPayload payload2 = this.Payload;
					errors.AddRange(payload2.ValidationErrors);
				}
			}
			if (this.Recipient == null)
			{
				errors.Add(Strings.InvalidRecipient);
				return;
			}
			TRecipient recipient = this.Recipient;
			if (!recipient.IsValid)
			{
				TRecipient recipient2 = this.Recipient;
				errors.AddRange(recipient2.ValidationErrors);
			}
		}

		// Token: 0x02000009 RID: 9
		private class FragmentId<T> where T : BasicNotificationRecipient
		{
			// Token: 0x06000039 RID: 57 RVA: 0x00002950 File Offset: 0x00000B50
			public FragmentId(string notificationId, T recipient)
			{
				if (recipient != null)
				{
					this.AppId = recipient.AppId;
					this.RecipientId = recipient.DeviceId;
				}
				if (notificationId != null || this.AppId != null)
				{
					this.Identifier = string.Format("{0}-{1}", notificationId ?? string.Empty, this.AppId ?? string.Empty);
				}
				this.NotificationId = notificationId;
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600003A RID: 58 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x0600003B RID: 59 RVA: 0x000029D4 File Offset: 0x00000BD4
			public string NotificationId { get; private set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600003C RID: 60 RVA: 0x000029DD File Offset: 0x00000BDD
			// (set) Token: 0x0600003D RID: 61 RVA: 0x000029E5 File Offset: 0x00000BE5
			public string Identifier { get; private set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600003E RID: 62 RVA: 0x000029EE File Offset: 0x00000BEE
			// (set) Token: 0x0600003F RID: 63 RVA: 0x000029F6 File Offset: 0x00000BF6
			public string AppId { get; private set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000040 RID: 64 RVA: 0x000029FF File Offset: 0x00000BFF
			// (set) Token: 0x06000041 RID: 65 RVA: 0x00002A07 File Offset: 0x00000C07
			public string RecipientId { get; private set; }
		}
	}
}
