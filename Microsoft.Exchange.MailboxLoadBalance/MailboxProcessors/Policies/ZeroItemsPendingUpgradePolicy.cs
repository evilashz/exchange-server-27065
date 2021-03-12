using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors.Policies
{
	// Token: 0x020000C2 RID: 194
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ZeroItemsPendingUpgradePolicy : IMailboxPolicy
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x000103FE File Offset: 0x0000E5FE
		public ZeroItemsPendingUpgradePolicy(ILoadBalanceSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00010410 File Offset: 0x0000E610
		public bool IsMailboxOutOfPolicy(DirectoryMailbox mailbox, DirectoryDatabase currentDatabase)
		{
			TimeSpan minimumAgeInDatabase = mailbox.MinimumAgeInDatabase;
			return mailbox.ItemsPendingUpgrade > 0 && minimumAgeInDatabase >= this.settings.MinimumTimeInDatabaseForItemUpgrade;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00010440 File Offset: 0x0000E640
		public void HandleExistingButNotInProgressMove(DirectoryMailbox mailbox, DirectoryDatabase database)
		{
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00010442 File Offset: 0x0000E642
		public BatchName GetBatchName()
		{
			return BatchName.CreateItemUpgradeBatch();
		}

		// Token: 0x04000252 RID: 594
		private readonly ILoadBalanceSettings settings;
	}
}
