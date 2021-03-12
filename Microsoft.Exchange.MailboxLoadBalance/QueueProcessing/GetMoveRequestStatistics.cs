using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000DA RID: 218
	internal class GetMoveRequestStatistics : CmdletExecutionRequest<MoveRequestStatistics>
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x0001345C File Offset: 0x0001165C
		public GetMoveRequestStatistics(DirectoryMailbox mailbox, ILogger logger, CmdletExecutionPool cmdletPool) : base("Get-MoveRequestStatistics", cmdletPool, logger)
		{
			this.Mailbox = mailbox;
			AnchorUtil.ThrowOnNullArgument(mailbox, "mailbox");
			string value;
			if (mailbox.OrganizationId == TenantPartitionHint.ExternalDirectoryOrganizationIdForRootOrg)
			{
				value = string.Format("{0}", mailbox.Guid);
			}
			else
			{
				value = string.Format("{0}\\{1}", mailbox.OrganizationId, mailbox.Guid);
			}
			base.Command.AddParameter("Identity", value);
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x000134E5 File Offset: 0x000116E5
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x000134ED File Offset: 0x000116ED
		public DirectoryMailbox Mailbox { get; private set; }
	}
}
