using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000217 RID: 535
	[Serializable]
	public class DatabaseAvailabilityGroupNetworkSubnet
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0003988A File Offset: 0x00037A8A
		// (set) Token: 0x060012BA RID: 4794 RVA: 0x00039892 File Offset: 0x00037A92
		public DatabaseAvailabilityGroupNetworkSubnet.SubnetState State { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x0003989B File Offset: 0x00037A9B
		// (set) Token: 0x060012BC RID: 4796 RVA: 0x000398A3 File Offset: 0x00037AA3
		public DatabaseAvailabilityGroupSubnetId SubnetId { get; set; }

		// Token: 0x060012BD RID: 4797 RVA: 0x000398AC File Offset: 0x00037AAC
		internal DatabaseAvailabilityGroupNetworkSubnet(DatabaseAvailabilityGroupSubnetId netId)
		{
			this.State = DatabaseAvailabilityGroupNetworkSubnet.SubnetState.Unknown;
			this.SubnetId = netId;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000398C2 File Offset: 0x00037AC2
		internal DatabaseAvailabilityGroupNetworkSubnet()
		{
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000398CC File Offset: 0x00037ACC
		public override bool Equals(object obj)
		{
			DatabaseAvailabilityGroupNetworkSubnet databaseAvailabilityGroupNetworkSubnet = obj as DatabaseAvailabilityGroupNetworkSubnet;
			return databaseAvailabilityGroupNetworkSubnet != null && this.SubnetId.IPRange.Equals(databaseAvailabilityGroupNetworkSubnet.SubnetId.IPRange);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00039900 File Offset: 0x00037B00
		public override int GetHashCode()
		{
			return this.SubnetId.GetHashCode();
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00039910 File Offset: 0x00037B10
		public override string ToString()
		{
			return string.Format("{{{0},{1}}}", this.SubnetId.ToString(), this.State.ToString());
		}

		// Token: 0x02000218 RID: 536
		public enum SubnetState
		{
			// Token: 0x04000B20 RID: 2848
			[LocDescription(DataStrings.IDs.Unknown)]
			Unknown,
			// Token: 0x04000B21 RID: 2849
			[LocDescription(DataStrings.IDs.Up)]
			Up,
			// Token: 0x04000B22 RID: 2850
			[LocDescription(DataStrings.IDs.Down)]
			Down,
			// Token: 0x04000B23 RID: 2851
			[LocDescription(DataStrings.IDs.Partitioned)]
			Partitioned,
			// Token: 0x04000B24 RID: 2852
			[LocDescription(DataStrings.IDs.Misconfigured)]
			Misconfigured,
			// Token: 0x04000B25 RID: 2853
			[LocDescription(DataStrings.IDs.Unavailable)]
			Unavailable
		}
	}
}
