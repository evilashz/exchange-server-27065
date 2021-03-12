using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200001F RID: 31
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class MailboxNotification : BasicMulticastNotification<MailboxNotificationPayload, MailboxNotificationRecipient>
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00003D04 File Offset: 0x00001F04
		public MailboxNotification(MailboxNotificationPayload payload, List<MailboxNotificationRecipient> recipients) : base(payload, recipients)
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003D0E File Offset: 0x00001F0E
		protected override Notification CreateFragment(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient)
		{
			return new MailboxNotificationFragment(base.Identifier, payload, recipient);
		}
	}
}
