using System;
using System.Linq;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000CF RID: 207
	internal abstract class HeatMapConstructionRequest : BaseRequest
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x00012ABB File Offset: 0x00010CBB
		protected HeatMapConstructionRequest(LoadBalanceAnchorContext context)
		{
			this.ServiceContext = context;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00012ACA File Offset: 0x00010CCA
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x00012AD2 File Offset: 0x00010CD2
		public LoadContainer Topology { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00012ADB File Offset: 0x00010CDB
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x00012AE3 File Offset: 0x00010CE3
		private protected LoadBalanceAnchorContext ServiceContext { protected get; private set; }

		// Token: 0x06000690 RID: 1680 RVA: 0x00012AEC File Offset: 0x00010CEC
		public void UpdateBands(Band[] newBands)
		{
			if (this.NeedToRebuildForBands(newBands))
			{
				this.Topology = null;
			}
		}

		// Token: 0x06000691 RID: 1681
		protected abstract LoadContainer BuildTopology(TopologyExtractorFactoryContext topologyExtractorContext);

		// Token: 0x06000692 RID: 1682 RVA: 0x00012B0F File Offset: 0x00010D0F
		protected bool NeedToRebuildForBands(Band[] newBands)
		{
			return this.bands != null && newBands.Any((Band band) => !this.bands.Contains(band));
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00012B30 File Offset: 0x00010D30
		protected override void ProcessRequest()
		{
			this.bands = this.ServiceContext.GetActiveBands();
			LoadContainer loadContainer = this.BuildTopology(this.GetTopologyExtractorContext(this.bands));
			if (loadContainer != null)
			{
				this.ServiceContext.Logger.LogVerbose("Refreshed topology for {0}, new timestamp is {1}.", new object[]
				{
					loadContainer.DirectoryObjectIdentity,
					loadContainer.DataRetrievedTimestampUtc
				});
			}
			this.Topology = loadContainer;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00012B9F File Offset: 0x00010D9F
		protected TopologyExtractorFactoryContext GetTopologyExtractorContext(Band[] bandsToUse)
		{
			return this.ServiceContext.TopologyExtractorFactoryContextPool.GetContext(this.ServiceContext.ClientFactory, bandsToUse, LoadBalanceUtils.GetNonMovableOrgsList(this.ServiceContext.Settings), this.ServiceContext.Logger);
		}

		// Token: 0x04000278 RID: 632
		private Band[] bands;
	}
}
