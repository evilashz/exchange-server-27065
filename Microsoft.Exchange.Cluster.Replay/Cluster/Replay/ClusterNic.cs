using System;
using System.Net;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200024B RID: 587
	internal class ClusterNic
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x0005CEEB File Offset: 0x0005B0EB
		// (set) Token: 0x060016B1 RID: 5809 RVA: 0x0005CEF3 File Offset: 0x0005B0F3
		public string Name { get; set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0005CEFC File Offset: 0x0005B0FC
		// (set) Token: 0x060016B3 RID: 5811 RVA: 0x0005CF04 File Offset: 0x0005B104
		public string NodeName { get; set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x0005CF0D File Offset: 0x0005B10D
		// (set) Token: 0x060016B5 RID: 5813 RVA: 0x0005CF15 File Offset: 0x0005B115
		public bool IsDnsRegistered { get; set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0005CF1E File Offset: 0x0005B11E
		// (set) Token: 0x060016B7 RID: 5815 RVA: 0x0005CF26 File Offset: 0x0005B126
		public bool HasIPAddress { get; set; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0005CF2F File Offset: 0x0005B12F
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x0005CF37 File Offset: 0x0005B137
		public IPAddress IPAddress { get; set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0005CF40 File Offset: 0x0005B140
		// (set) Token: 0x060016BB RID: 5819 RVA: 0x0005CF48 File Offset: 0x0005B148
		public AmNetInterfaceState ClusterState { get; set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x0005CF51 File Offset: 0x0005B151
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x0005CF59 File Offset: 0x0005B159
		public ClusterNetwork ClusterNetwork { get; set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x0005CF62 File Offset: 0x0005B162
		// (set) Token: 0x060016BF RID: 5823 RVA: 0x0005CF6A File Offset: 0x0005B16A
		public ClusterNode ClusterNode { get; set; }

		// Token: 0x060016C0 RID: 5824 RVA: 0x0005CF73 File Offset: 0x0005B173
		public ClusterNic()
		{
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0005CF7C File Offset: 0x0005B17C
		public ClusterNic(AmClusterNetInterface clusNic, ClusterNetwork owningClusNet)
		{
			this.Name = clusNic.Name;
			this.ClusterNetwork = owningClusNet;
			this.NodeName = clusNic.GetNodeName();
			this.ClusterState = clusNic.GetState(false);
			bool flag = false;
			string address = clusNic.GetAddress();
			IPAddress ipaddress = NetworkUtil.ConvertStringToIpAddress(address);
			if (ipaddress != null)
			{
				flag = true;
			}
			else
			{
				NetworkManager.TraceError("Ignoring invalid IPV4 address on NIC {0} since it has invalid ip={1}", new object[]
				{
					this.Name,
					address
				});
				string[] ipv6Addresses = clusNic.GetIPv6Addresses();
				if (ipv6Addresses != null && ipv6Addresses.Length > 0)
				{
					ipaddress = NetworkUtil.ConvertStringToIpAddress(ipv6Addresses[0]);
					if (ipaddress != null)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.IPAddress = ipaddress;
				this.HasIPAddress = true;
				return;
			}
			NetworkManager.TraceError("ClusterNic '{0}' has no ip addr. Nic state is {1}", new object[]
			{
				this.Name,
				this.ClusterState
			});
		}
	}
}
