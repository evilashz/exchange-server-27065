using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors.Policies
{
	// Token: 0x020000BE RID: 190
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IMailboxPolicy
	{
		// Token: 0x06000623 RID: 1571
		BatchName GetBatchName();

		// Token: 0x06000624 RID: 1572
		bool IsMailboxOutOfPolicy(DirectoryMailbox mailbox, DirectoryDatabase currentDatabase);

		// Token: 0x06000625 RID: 1573
		void HandleExistingButNotInProgressMove(DirectoryMailbox mailbox, DirectoryDatabase database);
	}
}
