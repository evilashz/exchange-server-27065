using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AutomaticLoadBalancer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AutomaticLoadBalancer(LoadBalanceAnchorContext anchorContext)
		{
			this.context = anchorContext;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020DF File Offset: 0x000002DF
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020E7 File Offset: 0x000002E7
		private protected LoadBalanceAnchorContext Context { protected get; private set; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020F0 File Offset: 0x000002F0
		public void LoadBalanceForest()
		{
			ILoadBalance loadBalancer = this.context.CreateLoadBalancer(this.context.Logger);
			this.LoadBalanceForest(loadBalancer, true, this.context.Logger, Timeout.InfiniteTimeSpan);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		public IList<BandMailboxRebalanceData> LoadBalanceForest(ILoadBalance loadBalancer, bool startMoves, ILogger logger, TimeSpan timeout)
		{
			RebalancingRequestMoveStarter moveStarter = new RebalancingRequestMoveStarter(this.context.ClientFactory, logger, this.context.QueueManager);
			LoadContainer loadTopology = this.context.HeatMap.GetLoadTopology();
			if (this.context.Settings.SoftDeletedCleanupEnabled)
			{
				int softDeletedCleanupThreshold = this.context.Settings.SoftDeletedCleanupThreshold;
				SoftDeletedCleanUpRequest request = new SoftDeletedCleanUpRequest(loadTopology, this.context.ClientFactory, softDeletedCleanupThreshold, logger);
				this.context.QueueManager.MainProcessingQueue.EnqueueRequest(request);
			}
			long aggregateConsumedLoad = loadTopology.GetAggregateConsumedLoad(InProgressLoadBalancingMoveCount.Instance);
			if (aggregateConsumedLoad > this.context.Settings.MaximumPendingMoveCount)
			{
				logger.LogWarning("Did not start load balancing run because current number of pending moves {0} is greater than MaximumPendingMoveCount {1}", new object[]
				{
					aggregateConsumedLoad,
					this.context.Settings.MaximumPendingMoveCount
				});
				return Array<BandMailboxRebalanceData>.Empty;
			}
			ForestLoadBalanceRequest forestLoadBalanceRequest = new ForestLoadBalanceRequest(loadBalancer, startMoves, logger, moveStarter, loadTopology, new PartitionExtractor());
			this.context.QueueManager.MainProcessingQueue.EnqueueRequest(forestLoadBalanceRequest);
			forestLoadBalanceRequest.WaitExecutionAndThrowOnFailure(timeout);
			return forestLoadBalanceRequest.Results;
		}

		// Token: 0x04000001 RID: 1
		private readonly LoadBalanceAnchorContext context;
	}
}
