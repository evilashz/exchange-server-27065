using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval
{
	// Token: 0x02000100 RID: 256
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeletedMailboxRemovalRequest : CmdletExecutionRequest<object>
	{
		// Token: 0x0600079A RID: 1946 RVA: 0x000156D4 File Offset: 0x000138D4
		public SoftDeletedMailboxRemovalRequest(Guid mailboxGuid, Guid databaseGuid, ILogger logger, CmdletExecutionPool cmdletPool, ILoadBalanceSettings settings) : base("Remove-StoreMailbox", cmdletPool, logger)
		{
			this.mailboxGuid = mailboxGuid;
			this.databaseGuid = databaseGuid;
			this.settings = settings;
			base.Command.AddParameter("Identity", mailboxGuid);
			base.Command.AddParameter("Database", databaseGuid.ToString());
			base.Command.AddParameter("MailboxState", "SoftDeleted");
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00015750 File Offset: 0x00013950
		protected override RequestDiagnosticData CreateDiagnosticData()
		{
			return new SoftDeletedMailboxRemovalDiagnosticData(this.databaseGuid, this.mailboxGuid);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00015763 File Offset: 0x00013963
		protected override void ProcessRequest()
		{
			if (this.settings.DontRemoveSoftDeletedMailboxes)
			{
				base.Command.AddParameter("WhatIf");
			}
			base.ProcessRequest();
		}

		// Token: 0x040002F4 RID: 756
		private const string RemoveStoreMailboxCmdletName = "Remove-StoreMailbox";

		// Token: 0x040002F5 RID: 757
		private const string SoftDeletedMailboxStateName = "SoftDeleted";

		// Token: 0x040002F6 RID: 758
		private readonly Guid databaseGuid;

		// Token: 0x040002F7 RID: 759
		private readonly ILoadBalanceSettings settings;

		// Token: 0x040002F8 RID: 760
		private readonly Guid mailboxGuid;
	}
}
