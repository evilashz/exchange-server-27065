using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors
{
	// Token: 0x020000B7 RID: 183
	internal class GetMoveInfo : IGetMoveInfo
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x0000FCE2 File Offset: 0x0000DEE2
		public GetMoveInfo(ILogger logger, CmdletExecutionPool cmdletPool)
		{
			this.logger = logger;
			this.cmdletPool = cmdletPool;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000FCF8 File Offset: 0x0000DEF8
		public MoveInfo GetInfo(DirectoryMailbox mailbox, IAnchorRunspaceProxy runspace)
		{
			GetMoveRequestStatistics getMoveRequestStatistics = new GetMoveRequestStatistics(mailbox, this.logger, this.cmdletPool);
			getMoveRequestStatistics.Process();
			if (getMoveRequestStatistics.Result == null)
			{
				return new MoveInfo(MoveStatus.MoveDoesNotExist, Guid.Empty);
			}
			MoveStatus status;
			if (getMoveRequestStatistics.Result.Status == RequestStatus.InProgress || getMoveRequestStatistics.Result.Status == RequestStatus.Queued)
			{
				status = MoveStatus.MoveExistsInProgress;
			}
			else
			{
				status = MoveStatus.MoveExistsNotInProgress;
			}
			return new MoveInfo(status, getMoveRequestStatistics.Result.RequestGuid);
		}

		// Token: 0x0400023C RID: 572
		private readonly ILogger logger;

		// Token: 0x0400023D RID: 573
		private readonly CmdletExecutionPool cmdletPool;
	}
}
