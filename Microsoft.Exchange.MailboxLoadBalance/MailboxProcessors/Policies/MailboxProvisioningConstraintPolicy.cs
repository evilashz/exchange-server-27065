using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors.Policies
{
	// Token: 0x020000C0 RID: 192
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxProvisioningConstraintPolicy : IMailboxPolicy
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x0001032C File Offset: 0x0000E52C
		public MailboxProvisioningConstraintPolicy(IEventNotificationSender eventNotificationSender)
		{
			this.eventNotificationSender = eventNotificationSender;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001033B File Offset: 0x0000E53B
		public BatchName GetBatchName()
		{
			return BatchName.CreateProvisioningConstraintFixBatch();
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00010342 File Offset: 0x0000E542
		public bool IsMailboxOutOfPolicy(DirectoryMailbox mailbox, DirectoryDatabase currentDatabase)
		{
			return mailbox.MailboxProvisioningConstraints != null && mailbox.MailboxProvisioningConstraints.HardConstraint != null && !mailbox.MailboxProvisioningConstraints.IsMatch(currentDatabase.MailboxProvisioningAttributes);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00010371 File Offset: 0x0000E571
		public void HandleExistingButNotInProgressMove(DirectoryMailbox mailbox, DirectoryDatabase database)
		{
			this.eventNotificationSender.CreateAndPublishMailboxEventNotification(MigrationNotifications.CannotMoveMailboxDueToExistingMoveNotInProgress, mailbox, database);
		}

		// Token: 0x0400024F RID: 591
		private readonly IEventNotificationSender eventNotificationSender;
	}
}
