using System;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Logging;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors.Policies
{
	// Token: 0x020000BF RID: 191
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxPolicyProcessor : MailboxProcessor
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00010190 File Offset: 0x0000E390
		public MailboxPolicyProcessor(ILogger logger, IGetMoveInfo getMoveInfo, MoveInjector moveInjector, IMailboxPolicy[] policy) : base(logger)
		{
			this.getMoveInfo = getMoveInfo;
			this.moveInjector = moveInjector;
			this.policy = policy;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x000101AF File Offset: 0x0000E3AF
		public override bool RequiresRunspace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public override void ProcessMailbox(DirectoryMailbox mailbox, IAnchorRunspaceProxy runspace)
		{
			DirectoryDatabase directoryDatabase = (DirectoryDatabase)mailbox.Parent;
			IMailboxPolicy violatedPolicy = this.GetViolatedPolicy(mailbox, directoryDatabase);
			if (violatedPolicy == null)
			{
				return;
			}
			MoveInfo info = this.getMoveInfo.GetInfo(mailbox, runspace);
			ProvisioningConstraintFixStateLog.Write(mailbox, directoryDatabase, info);
			switch (info.Status)
			{
			case MoveStatus.MoveExistsNotInProgress:
				violatedPolicy.HandleExistingButNotInProgressMove(mailbox, directoryDatabase);
				return;
			case MoveStatus.MoveExistsInProgress:
				break;
			case MoveStatus.MoveDoesNotExist:
				this.InjectMoveRequest(mailbox, violatedPolicy);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00010238 File Offset: 0x0000E438
		public override bool ShouldProcess(DirectoryMailbox mailbox)
		{
			DirectoryDatabase database = (DirectoryDatabase)mailbox.Parent;
			return database != null && this.policy.Any((IMailboxPolicy p) => p.IsMailboxOutOfPolicy(mailbox, database));
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000102A8 File Offset: 0x0000E4A8
		private IMailboxPolicy GetViolatedPolicy(DirectoryMailbox mailbox, DirectoryDatabase database)
		{
			return this.policy.FirstOrDefault((IMailboxPolicy p) => p.IsMailboxOutOfPolicy(mailbox, database));
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000102E0 File Offset: 0x0000E4E0
		private void InjectMoveRequest(DirectoryMailbox mailbox, IMailboxPolicy violatedPolicy)
		{
			BatchName batchName = violatedPolicy.GetBatchName();
			if (batchName == null)
			{
				throw new InvalidOperationException("Batch names should never be null.");
			}
			base.Logger.Log(MigrationEventType.Information, "Starting a new load balancing procedure to fix provisioning constraint, batch name will be '{0}'", new object[]
			{
				batchName
			});
			this.moveInjector.InjectMoveForMailbox(mailbox, batchName);
		}

		// Token: 0x0400024C RID: 588
		private readonly IGetMoveInfo getMoveInfo;

		// Token: 0x0400024D RID: 589
		private readonly MoveInjector moveInjector;

		// Token: 0x0400024E RID: 590
		private readonly IMailboxPolicy[] policy;
	}
}
