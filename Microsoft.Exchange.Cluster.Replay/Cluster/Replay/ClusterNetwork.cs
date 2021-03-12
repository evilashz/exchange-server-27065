using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200024C RID: 588
	internal class ClusterNetwork
	{
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0005D052 File Offset: 0x0005B252
		// (set) Token: 0x060016C3 RID: 5827 RVA: 0x0005D05A File Offset: 0x0005B25A
		public DatabaseAvailabilityGroupSubnetId SubnetId { get; private set; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x0005D063 File Offset: 0x0005B263
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x0005D06B File Offset: 0x0005B26B
		public AmNetworkState ClusterState { get; set; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0005D074 File Offset: 0x0005B274
		// (set) Token: 0x060016C7 RID: 5831 RVA: 0x0005D07C File Offset: 0x0005B27C
		public bool HasDnsNic { get; set; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0005D085 File Offset: 0x0005B285
		public List<ClusterNic> Nics
		{
			get
			{
				return this.m_nics;
			}
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0005D090 File Offset: 0x0005B290
		public ClusterNetwork(AmClusterNetwork clusNet)
		{
			this.SubnetId = ExchangeSubnet.ExtractSubnetId(clusNet);
			this.ClusterState = clusNet.GetState(false);
			if (this.SubnetId == null)
			{
				ExTraceGlobals.NetworkManagerTracer.TraceError<string>(0L, "ClusterNetwork.Subnet is null for network {0}", clusNet.Name);
				throw new ClusterNetworkNullSubnetException(clusNet.Name);
			}
			IEnumerable<AmClusterNetInterface> enumerable = clusNet.EnumerateNetworkInterfaces();
			try
			{
				foreach (AmClusterNetInterface clusNic in enumerable)
				{
					ClusterNic item = new ClusterNic(clusNic, this);
					this.m_nics.Add(item);
				}
			}
			finally
			{
				foreach (AmClusterNetInterface amClusterNetInterface in enumerable)
				{
					using (amClusterNetInterface)
					{
					}
				}
			}
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0005D1A8 File Offset: 0x0005B3A8
		public ClusterNetwork(DatabaseAvailabilityGroupSubnetId subnetId)
		{
			this.SubnetId = subnetId;
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0005D1C3 File Offset: 0x0005B3C3
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x0005D1CB File Offset: 0x0005B3CB
		public LogicalNetwork LogicalNetwork { get; set; }

		// Token: 0x040008EF RID: 2287
		private List<ClusterNic> m_nics = new List<ClusterNic>(8);
	}
}
