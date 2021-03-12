using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors.Policies
{
	// Token: 0x020000C1 RID: 193
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PolicyActivationControl : IMailboxPolicy
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x00010385 File Offset: 0x0000E585
		public PolicyActivationControl(IMailboxPolicy policy, ILoadBalanceSettings settings)
		{
			AnchorUtil.ThrowOnNullArgument(policy, "policy");
			this.policy = policy;
			this.settings = settings;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000103A6 File Offset: 0x0000E5A6
		public BatchName GetBatchName()
		{
			return this.policy.GetBatchName();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000103B3 File Offset: 0x0000E5B3
		public bool IsMailboxOutOfPolicy(DirectoryMailbox mailbox, DirectoryDatabase currentDatabase)
		{
			return !(this.settings.DisabledMailboxPolicies ?? string.Empty).Contains(this.policy.GetType().Name) && this.policy.IsMailboxOutOfPolicy(mailbox, currentDatabase);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000103EF File Offset: 0x0000E5EF
		public void HandleExistingButNotInProgressMove(DirectoryMailbox mailbox, DirectoryDatabase database)
		{
			this.policy.HandleExistingButNotInProgressMove(mailbox, database);
		}

		// Token: 0x04000250 RID: 592
		private readonly IMailboxPolicy policy;

		// Token: 0x04000251 RID: 593
		private readonly ILoadBalanceSettings settings;
	}
}
