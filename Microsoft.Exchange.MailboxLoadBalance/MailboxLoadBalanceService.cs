using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxLoadBalanceService
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000305C File Offset: 0x0000125C
		public MailboxLoadBalanceService(LoadBalanceAnchorContext serviceContext)
		{
			AnchorUtil.ThrowOnNullArgument(serviceContext, "serviceContext");
			this.serviceContext = serviceContext;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003078 File Offset: 0x00001278
		public LoadContainer GetDatabaseData(Guid databaseGuid, bool includeMailboxes)
		{
			TopologyExtractorFactoryContext topologyExtractorFactoryContext = this.serviceContext.GetTopologyExtractorFactoryContext();
			TopologyExtractorFactory topologyExtractorFactory = includeMailboxes ? topologyExtractorFactoryContext.GetEntitySelectorFactory() : topologyExtractorFactoryContext.GetLoadBalancingLocalFactory(false);
			DirectoryDatabase database = this.serviceContext.Directory.GetDatabase(databaseGuid);
			return topologyExtractorFactory.GetExtractor(database).ExtractTopology();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000030C4 File Offset: 0x000012C4
		public void MoveMailboxes(BandMailboxRebalanceData rebalanceData)
		{
			IList<Guid> nonMovableOrgsList = LoadBalanceUtils.GetNonMovableOrgsList(this.serviceContext.Settings);
			MailboxRebalanceRequest request = new MailboxRebalanceRequest(rebalanceData, this.serviceContext, nonMovableOrgsList);
			this.serviceContext.QueueManager.GetProcessingQueue(rebalanceData.SourceDatabase).EnqueueRequest(request);
		}

		// Token: 0x04000017 RID: 23
		private readonly LoadBalanceAnchorContext serviceContext;
	}
}
