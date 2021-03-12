using System;
using Microsoft.Exchange.Directory.TopologyService.Data;

namespace Microsoft.Exchange.Directory.TopologyService.Common
{
	// Token: 0x02000008 RID: 8
	internal class ADTopologyDiscoveryResult
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003630 File Offset: 0x00001830
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00003638 File Offset: 0x00001838
		public DiscoveryFlags DiscoveryFlags { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003641 File Offset: 0x00001841
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003649 File Offset: 0x00001849
		public ADTopology Topology { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003652 File Offset: 0x00001852
		// (set) Token: 0x0600004D RID: 77 RVA: 0x0000365A File Offset: 0x0000185A
		public TopologyDiscoveryInfo TopologyDiscoveryInfo { get; set; }

		// Token: 0x0600004E RID: 78 RVA: 0x00003663 File Offset: 0x00001863
		public override string ToString()
		{
			return string.Format("Forest Fqdn:", (this.TopologyDiscoveryInfo == null) ? "<NULL>" : this.TopologyDiscoveryInfo.ForestFqdn);
		}
	}
}
