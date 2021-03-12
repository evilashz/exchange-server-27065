using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000061 RID: 97
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceServiceDiagnosable : LoadBalanceDiagnosableBase<LoadBalanceServiceDiagnosableArgument, LoadBalanceDiagnosableResult>
	{
		// Token: 0x06000358 RID: 856 RVA: 0x00009FD5 File Offset: 0x000081D5
		public LoadBalanceServiceDiagnosable(LoadBalanceAnchorContext anchorContext) : base(anchorContext.Logger)
		{
			this.anchorContext = anchorContext;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00009FEC File Offset: 0x000081EC
		protected override LoadBalanceDiagnosableResult ProcessDiagnostic()
		{
			LoadBalanceDiagnosableResult result = default(LoadBalanceDiagnosableResult);
			if (base.Arguments.ShowQueues)
			{
				result.QueueManager = this.anchorContext.QueueManager.GetDiagnosticData(base.Arguments.ShowQueuedRequests, base.Arguments.Verbose);
			}
			if (base.Arguments.CleanQueues)
			{
				this.anchorContext.QueueManager.Clean();
			}
			if (base.Arguments.ShowLoadBalancerResults)
			{
				ILoadBalance loadBalancer = this.anchorContext.CreateLoadBalancer(base.Logger);
				result.RebalanceResults = new AutomaticLoadBalancer(this.anchorContext).LoadBalanceForest(loadBalancer, base.Arguments.StartLoadBalance, base.Logger, TimeSpan.FromMinutes(5.0));
			}
			if (base.Arguments.RemoveSoftDeletedMailbox)
			{
				result.SoftDeletedMailboxRemovalResult = new SoftDeletedMailboxRemovalResult();
				result.SoftDeletedMailboxRemovalResult.MailboxGuid = base.Arguments.MailboxGuid;
				result.SoftDeletedMailboxRemovalResult.DatabaseGuid = base.Arguments.DatabaseGuid;
				Exception ex;
				result.SoftDeletedMailboxRemovalResult.Success = this.anchorContext.TryRemoveSoftDeletedMailbox(base.Arguments.MailboxGuid, base.Arguments.DatabaseGuid, out ex);
			}
			if (base.Arguments.GetMoveHistory)
			{
				result.SoftDeletedMoveHistoryResult = new SoftDeletedMoveHistoryResult();
				result.SoftDeletedMoveHistoryResult.MailboxGuid = base.Arguments.MailboxGuid;
				result.SoftDeletedMoveHistoryResult.TargetDatabaseGuid = base.Arguments.TargetDatabaseGuid;
				result.SoftDeletedMoveHistoryResult.SourceDatabaseGuid = base.Arguments.SourceDatabaseGuid;
				result.SoftDeletedMoveHistoryResult.MoveHistory = this.anchorContext.RetrieveSoftDeletedMailboxMoveHistory(base.Arguments.MailboxGuid, base.Arguments.TargetDatabaseGuid, base.Arguments.SourceDatabaseGuid);
			}
			if (base.Arguments.IsDrainingDatabase)
			{
				DirectoryDatabase database = this.anchorContext.Directory.GetDatabase(base.Arguments.DatabaseToDrainGuid);
				BatchName drainBatchName = this.anchorContext.DrainControl.BeginDrainDatabase(database);
				result.DatabaseToDrain = database.Identity;
				result.DrainBatchName = drainBatchName;
			}
			return result;
		}

		// Token: 0x04000108 RID: 264
		private readonly LoadBalanceAnchorContext anchorContext;
	}
}
