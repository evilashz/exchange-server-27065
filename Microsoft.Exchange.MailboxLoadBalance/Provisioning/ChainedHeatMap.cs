using System;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000CB RID: 203
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ChainedHeatMap : HeatMap
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x000124A8 File Offset: 0x000106A8
		public ChainedHeatMap(params IHeatMap[] heatMaps)
		{
			AnchorUtil.ThrowOnCollectionEmptyArgument(heatMaps, "heatMaps");
			this.heatMaps = heatMaps;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000124CC File Offset: 0x000106CC
		public override LoadContainer GetLoadTopology()
		{
			IHeatMap heatMap = this.heatMaps.FirstOrDefault((IHeatMap map) => map.IsReady);
			if (heatMap == null)
			{
				throw new HeatMapNotBuiltException();
			}
			return heatMap.GetLoadTopology();
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00012514 File Offset: 0x00010714
		public override void UpdateBands(Band[] bands)
		{
			foreach (IHeatMap heatMap in this.heatMaps)
			{
				heatMap.UpdateBands(bands);
			}
		}

		// Token: 0x04000273 RID: 627
		private readonly IHeatMap[] heatMaps;
	}
}
