using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Drain
{
	// Token: 0x0200008A RID: 138
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DatabaseDrainControl
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x0000CFB5 File Offset: 0x0000B1B5
		public DatabaseDrainControl(LoadBalanceAnchorContext serviceContext)
		{
			this.serviceContext = serviceContext;
			this.inProgressDrainRequests = new ConcurrentDictionary<DirectoryIdentity, BatchName>(12, 12);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		public virtual BatchName BeginDrainDatabase(DirectoryDatabase database)
		{
			BatchName batchName = BatchName.CreateDrainBatch(database.Identity);
			DatabaseDrainRequest databaseDrainRequest = new DatabaseDrainRequest(database, this.serviceContext.MoveInjector, this.serviceContext, batchName);
			if (!this.inProgressDrainRequests.TryAdd(database.Identity, batchName))
			{
				return this.inProgressDrainRequests[database.Identity];
			}
			databaseDrainRequest.OnDrainFinished += this.DatabaseDrainFinished;
			this.serviceContext.QueueManager.GetProcessingQueue(database).EnqueueRequest(databaseDrainRequest);
			return batchName;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000D058 File Offset: 0x0000B258
		protected void DatabaseDrainFinished(DirectoryDatabase database)
		{
			BatchName batchName;
			if (this.inProgressDrainRequests.TryRemove(database.Identity, out batchName))
			{
				this.serviceContext.Logger.LogVerbose("Draining processing for database '{0}' using batch name '{1}' has completed.", new object[]
				{
					database.Identity,
					batchName
				});
				return;
			}
			this.serviceContext.Logger.LogWarning("Received a signal that database {0} finished draining, but the database wasn't tracked.", new object[]
			{
				database.Identity
			});
		}

		// Token: 0x0400019D RID: 413
		private readonly LoadBalanceAnchorContext serviceContext;

		// Token: 0x0400019E RID: 414
		private readonly ConcurrentDictionary<DirectoryIdentity, BatchName> inProgressDrainRequests;
	}
}
