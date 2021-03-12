using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200024F RID: 591
	internal class LogicalNetwork
	{
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0005D299 File Offset: 0x0005B499
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x0005D2A1 File Offset: 0x0005B4A1
		public string Name { get; set; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x0005D2AA File Offset: 0x0005B4AA
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x0005D2B2 File Offset: 0x0005B4B2
		public string Description { get; set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0005D2BB File Offset: 0x0005B4BB
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x0005D2C3 File Offset: 0x0005B4C3
		public bool ReplicationEnabled { get; set; }

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0005D2CC File Offset: 0x0005B4CC
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x0005D2D4 File Offset: 0x0005B4D4
		public bool IgnoreNetwork { get; set; }

		// Token: 0x060016E5 RID: 5861 RVA: 0x0005D2E0 File Offset: 0x0005B4E0
		public static string BuildDefaultReplNetName(int index)
		{
			return string.Format("{0}{1:D2}", "ReplicationDagNetwork", index);
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0005D304 File Offset: 0x0005B504
		public List<Subnet> Subnets
		{
			get
			{
				return this.m_memberNets;
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0005D30C File Offset: 0x0005B50C
		public bool HasDnsNic()
		{
			foreach (Subnet subnet in this.Subnets)
			{
				if (subnet.ClusterNetwork != null && subnet.ClusterNetwork.HasDnsNic)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0005D374 File Offset: 0x0005B574
		public void Add(Subnet subnet)
		{
			subnet.LogicalNetwork = this;
			this.m_memberNets.Add(subnet);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0005D389 File Offset: 0x0005B589
		public LogicalNetwork()
		{
			this.ReplicationEnabled = true;
			this.IgnoreNetwork = false;
		}

		// Token: 0x040008FB RID: 2299
		public const string DefaultDnsNetName = "MapiDagNetwork";

		// Token: 0x040008FC RID: 2300
		public const string DefaultPrefix = "ReplicationDagNetwork";

		// Token: 0x040008FD RID: 2301
		private List<Subnet> m_memberNets = new List<Subnet>(3);
	}
}
