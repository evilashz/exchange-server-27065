using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200024E RID: 590
	internal class Subnet
	{
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0005D23C File Offset: 0x0005B43C
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x0005D244 File Offset: 0x0005B444
		public DatabaseAvailabilityGroupSubnetId SubnetId { get; private set; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0005D24D File Offset: 0x0005B44D
		// (set) Token: 0x060016D8 RID: 5848 RVA: 0x0005D255 File Offset: 0x0005B455
		public ClusterNetwork ClusterNetwork { get; set; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0005D25E File Offset: 0x0005B45E
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x0005D266 File Offset: 0x0005B466
		public LogicalNetwork LogicalNetwork { get; set; }

		// Token: 0x060016DB RID: 5851 RVA: 0x0005D26F File Offset: 0x0005B46F
		public Subnet(ClusterNetwork clusNet)
		{
			this.SubnetId = clusNet.SubnetId;
			this.ClusterNetwork = clusNet;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0005D28A File Offset: 0x0005B48A
		public Subnet(DatabaseAvailabilityGroupSubnetId subnetId)
		{
			this.SubnetId = subnetId;
		}
	}
}
