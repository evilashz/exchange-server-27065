using System;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors
{
	// Token: 0x020000B5 RID: 181
	internal class EventNotificationSender : IEventNotificationSender
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x0000FC40 File Offset: 0x0000DE40
		public void CreateAndPublishMailboxEventNotification(string notificationReason, DirectoryMailbox mailbox, DirectoryDatabase database)
		{
			if (mailbox == null)
			{
				return;
			}
			Guid guid = (database == null) ? Guid.Empty : database.Guid;
			EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration.Name, notificationReason, ResultSeverityLevel.Error)
			{
				StateAttribute1 = mailbox.Guid.ToString(),
				StateAttribute2 = guid.ToString(),
				StateAttribute3 = mailbox.OrganizationId.ToString(),
				StateAttribute4 = "Client=MSExchangeMailboxLoadBalance"
			};
			eventNotificationItem.Publish(false);
		}
	}
}
