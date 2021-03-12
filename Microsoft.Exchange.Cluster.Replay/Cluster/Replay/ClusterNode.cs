using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200024D RID: 589
	internal class ClusterNode
	{
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0005D1D4 File Offset: 0x0005B3D4
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0005D1DC File Offset: 0x0005B3DC
		public AmServerName Name { get; set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0005D1E5 File Offset: 0x0005B3E5
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0005D1ED File Offset: 0x0005B3ED
		public AmNodeState ClusterState { get; set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0005D1F6 File Offset: 0x0005B3F6
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x0005D1FE File Offset: 0x0005B3FE
		public IPAddress[] DnsAddresses { get; set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0005D207 File Offset: 0x0005B407
		public List<ClusterNic> Nics
		{
			get
			{
				return this.m_nics;
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0005D20F File Offset: 0x0005B40F
		public ClusterNode(IAmClusterNode clusNode)
		{
			this.Name = clusNode.Name;
			this.ClusterState = clusNode.GetState(false);
		}

		// Token: 0x040008F4 RID: 2292
		private List<ClusterNic> m_nics = new List<ClusterNic>(3);
	}
}
