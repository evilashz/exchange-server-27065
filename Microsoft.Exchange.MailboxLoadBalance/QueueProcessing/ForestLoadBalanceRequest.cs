using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000D9 RID: 217
	[DataContract]
	internal class ForestLoadBalanceRequest : BaseRequest
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x00013284 File Offset: 0x00011484
		public ForestLoadBalanceRequest(ILoadBalance loadBalancer, bool startMoves, ILogger logger, IRebalancingRequestProcessor moveStarter, LoadContainer forestTopology, PartitionExtractor partitionExtractor)
		{
			AnchorUtil.ThrowOnNullArgument(loadBalancer, "loadBalancer");
			AnchorUtil.ThrowOnNullArgument(logger, "logger");
			this.loadBalancer = loadBalancer;
			this.startMoves = startMoves;
			this.logger = logger;
			this.moveStarter = moveStarter;
			this.forestTopology = forestTopology;
			this.partitionExtractor = partitionExtractor;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x000132DA File Offset: 0x000114DA
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x000132E2 File Offset: 0x000114E2
		[DataMember]
		public IList<BandMailboxRebalanceData> Results { get; private set; }

		// Token: 0x060006C8 RID: 1736 RVA: 0x000132EC File Offset: 0x000114EC
		protected override void ProcessRequest()
		{
			string text = string.Format("MsExchMlb:Band:{0:yyyyMMddhhmm}", ExDateTime.UtcNow);
			using (OperationTracker.Create(this.logger, "Rebalancing forest with StartMoves={0}, using LoadBalancer={1}, BatchName={2}", new object[]
			{
				this.startMoves,
				this.loadBalancer,
				text
			}))
			{
				List<BandMailboxRebalanceData> list = new List<BandMailboxRebalanceData>();
				foreach (LoadPartition loadPartition in this.partitionExtractor.GetPartitions(this.forestTopology))
				{
					this.logger.LogInformation("Load balancing partition for MailboxProvisioningConstraint \"{0}\"", new object[]
					{
						loadPartition.ConstraintSetIdentity
					});
					IEnumerable<BandMailboxRebalanceData> enumerable = this.loadBalancer.BalanceForest(loadPartition.Root);
					foreach (BandMailboxRebalanceData bandMailboxRebalanceData in enumerable)
					{
						bandMailboxRebalanceData.RebalanceBatchName = text;
						bandMailboxRebalanceData.ConstraintSetIdentity = loadPartition.ConstraintSetIdentity;
						list.Add(bandMailboxRebalanceData);
						if (this.startMoves)
						{
							this.moveStarter.ProcessRebalanceRequest(bandMailboxRebalanceData);
						}
					}
				}
				this.Results = list;
			}
		}

		// Token: 0x04000291 RID: 657
		[DataMember]
		private readonly ILoadBalance loadBalancer;

		// Token: 0x04000292 RID: 658
		[IgnoreDataMember]
		private readonly ILogger logger;

		// Token: 0x04000293 RID: 659
		[IgnoreDataMember]
		private readonly IRebalancingRequestProcessor moveStarter;

		// Token: 0x04000294 RID: 660
		[IgnoreDataMember]
		private readonly LoadContainer forestTopology;

		// Token: 0x04000295 RID: 661
		[DataMember]
		private readonly bool startMoves;

		// Token: 0x04000296 RID: 662
		[IgnoreDataMember]
		private readonly PartitionExtractor partitionExtractor;
	}
}
