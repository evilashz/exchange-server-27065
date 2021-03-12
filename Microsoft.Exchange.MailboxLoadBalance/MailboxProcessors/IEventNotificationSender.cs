using System;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors
{
	// Token: 0x020000B4 RID: 180
	internal interface IEventNotificationSender
	{
		// Token: 0x06000603 RID: 1539
		void CreateAndPublishMailboxEventNotification(string notificationReason, DirectoryMailbox mailbox, DirectoryDatabase database);
	}
}
