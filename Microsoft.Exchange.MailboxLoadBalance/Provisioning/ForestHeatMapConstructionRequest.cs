using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000D0 RID: 208
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ForestHeatMapConstructionRequest : HeatMapConstructionRequest
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x00012BD8 File Offset: 0x00010DD8
		public ForestHeatMapConstructionRequest(LoadBalanceAnchorContext context) : base(context)
		{
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00012BE4 File Offset: 0x00010DE4
		protected override LoadContainer BuildTopology(TopologyExtractorFactoryContext topologyExtractorContext)
		{
			TopologyExtractorFactory loadBalancingCentralFactory = topologyExtractorContext.GetLoadBalancingCentralFactory();
			TopologyExtractor extractor = loadBalancingCentralFactory.GetExtractor(base.ServiceContext.Directory.GetLocalForest());
			LoadContainer loadContainer = extractor.ExtractTopology();
			ExAssert.RetailAssert(loadContainer != null, "Extracted toplogy for the forest should never be null.");
			return loadContainer;
		}
	}
}
