using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.Drain
{
	// Token: 0x0200008B RID: 139
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DatabaseDrainRequest : BaseRequest
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public DatabaseDrainRequest(DirectoryDatabase directoryDatabase, MoveInjector moveInjector, LoadBalanceAnchorContext serviceContext, BatchName batchName)
		{
			AnchorUtil.ThrowOnNullArgument(directoryDatabase, "directoryDatabase");
			AnchorUtil.ThrowOnNullArgument(moveInjector, "moveInjector");
			AnchorUtil.ThrowOnNullArgument(serviceContext, "serviceContext");
			AnchorUtil.ThrowOnNullArgument(batchName, "batchName");
			this.directoryDatabase = directoryDatabase;
			this.moveInjector = moveInjector;
			this.serviceContext = serviceContext;
			this.logger = serviceContext.Logger;
			this.batchName = batchName;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000522 RID: 1314 RVA: 0x0000D138 File Offset: 0x0000B338
		// (remove) Token: 0x06000523 RID: 1315 RVA: 0x0000D170 File Offset: 0x0000B370
		public event Action<DirectoryDatabase> OnDrainFinished;

		// Token: 0x06000524 RID: 1316 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		protected override void ProcessRequest()
		{
			this.logger.LogInformation("Starting to drain database {0} with batch name {1}.", new object[]
			{
				this.directoryDatabase.Identity,
				this.batchName
			});
			IOperationRetryManager operationRetryManager = LoadBalanceOperationRetryManager.Create(this.logger);
			using (OperationTracker.Create(this.logger, "Moving mailboxes out of {0}.", new object[]
			{
				this.directoryDatabase.Identity
			}))
			{
				foreach (DirectoryMailbox mailboxToMove2 in this.directoryDatabase.GetMailboxes())
				{
					DirectoryMailbox mailboxToMove = mailboxToMove2;
					operationRetryManager.TryRun(delegate
					{
						this.moveInjector.InjectMoveForMailbox(mailboxToMove, this.batchName);
					});
				}
			}
			this.logger.LogInformation("Draining database {0}: Cleaning soft deleted mailboxes.", new object[]
			{
				this.directoryDatabase.Identity
			});
			using (OperationTracker.Create(this.logger, "Starting soft deleted cleanup for {0}.", new object[]
			{
				this.directoryDatabase.Identity
			}))
			{
				this.serviceContext.CleanupSoftDeletedMailboxesOnDatabase(this.directoryDatabase.Identity, ByteQuantifiedSize.Zero);
			}
			this.logger.LogInformation("Finished processing the draining of database {0}.", new object[]
			{
				this.directoryDatabase.Identity
			});
			if (this.OnDrainFinished != null)
			{
				this.OnDrainFinished(this.directoryDatabase);
			}
		}

		// Token: 0x0400019F RID: 415
		private readonly BatchName batchName;

		// Token: 0x040001A0 RID: 416
		private readonly DirectoryDatabase directoryDatabase;

		// Token: 0x040001A1 RID: 417
		private readonly ILogger logger;

		// Token: 0x040001A2 RID: 418
		private readonly MoveInjector moveInjector;

		// Token: 0x040001A3 RID: 419
		private readonly LoadBalanceAnchorContext serviceContext;
	}
}
