using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200003A RID: 58
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class RemoteUserNotification : UserNotification<RemoteUserNotificationPayload>
	{
		// Token: 0x0600016E RID: 366 RVA: 0x0000504C File Offset: 0x0000324C
		public RemoteUserNotification(string workloadId, RemoteUserNotificationPayload payload, List<UserNotificationRecipient> recipients) : base(workloadId, payload, recipients)
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005057 File Offset: 0x00003257
		protected override Notification CreateFragment(RemoteUserNotificationPayload payload, UserNotificationRecipient recipient)
		{
			return new RemoteUserNotificationFragment(base.Identifier, payload, recipient);
		}
	}
}
