using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000CA RID: 202
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CachedHeatMap : HeatMap
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x000123F0 File Offset: 0x000105F0
		public CachedHeatMap(LoadBalanceAnchorContext context, HeatMapConstructionRequest heatMapConstructor)
		{
			this.heatMapRequest = heatMapConstructor;
			ScheduledRequest request = new ScheduledRequest(this.heatMapRequest, TimeProvider.UtcNow, () => context.Settings.LocalCacheRefreshPeriod);
			context.QueueManager.MainProcessingQueue.EnqueueRequest(request);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00012451 File Offset: 0x00010651
		public override bool IsReady
		{
			get
			{
				return this.heatMapRequest.Topology != null;
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00012464 File Offset: 0x00010664
		public override LoadContainer GetLoadTopology()
		{
			while (this.heatMapRequest.Topology == null)
			{
				this.heatMapRequest.WaitExecutionAndThrowOnFailure(TimeSpan.FromMinutes(5.0));
			}
			return this.heatMapRequest.Topology;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001249A File Offset: 0x0001069A
		public override void UpdateBands(Band[] bands)
		{
			this.heatMapRequest.UpdateBands(bands);
		}

		// Token: 0x04000272 RID: 626
		private readonly HeatMapConstructionRequest heatMapRequest;
	}
}
