using System;
using System.Threading;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000CE RID: 206
	internal class ForestHeatMap : HeatMap
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x00012A68 File Offset: 0x00010C68
		public ForestHeatMap(LoadBalanceAnchorContext context)
		{
			this.context = context;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00012A78 File Offset: 0x00010C78
		public override LoadContainer GetLoadTopology()
		{
			ForestHeatMapConstructionRequest forestHeatMapConstructionRequest = new ForestHeatMapConstructionRequest(this.context);
			this.context.QueueManager.MainProcessingQueue.EnqueueRequest(forestHeatMapConstructionRequest);
			forestHeatMapConstructionRequest.WaitExecutionAndThrowOnFailure(Timeout.InfiniteTimeSpan);
			return forestHeatMapConstructionRequest.Topology;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00012AB9 File Offset: 0x00010CB9
		public override void UpdateBands(Band[] bands)
		{
		}

		// Token: 0x04000277 RID: 631
		private readonly LoadBalanceAnchorContext context;
	}
}
