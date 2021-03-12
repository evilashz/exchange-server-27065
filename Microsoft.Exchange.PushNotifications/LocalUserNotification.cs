using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000033 RID: 51
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class LocalUserNotification : UserNotification<LocalUserNotificationPayload>
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00004CC5 File Offset: 0x00002EC5
		public LocalUserNotification(string workloadId, LocalUserNotificationPayload payload, List<UserNotificationRecipient> recipients) : base(workloadId, payload, recipients)
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004CD0 File Offset: 0x00002ED0
		protected override Notification CreateFragment(LocalUserNotificationPayload payload, UserNotificationRecipient recipient)
		{
			return new LocalUserNotificationFragment(base.Identifier, payload, recipient);
		}
	}
}
