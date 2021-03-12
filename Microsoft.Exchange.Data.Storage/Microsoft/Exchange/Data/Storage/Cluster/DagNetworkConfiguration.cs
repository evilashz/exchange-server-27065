using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x02000424 RID: 1060
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DagNetworkConfiguration
	{
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x000C3887 File Offset: 0x000C1A87
		// (set) Token: 0x06002F91 RID: 12177 RVA: 0x000C388F File Offset: 0x000C1A8F
		public ushort ReplicationPort
		{
			get
			{
				return this.m_replicationPort;
			}
			set
			{
				this.m_replicationPort = value;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x000C3898 File Offset: 0x000C1A98
		// (set) Token: 0x06002F93 RID: 12179 RVA: 0x000C38A0 File Offset: 0x000C1AA0
		public DatabaseAvailabilityGroup.NetworkOption NetworkCompression
		{
			get
			{
				return this.m_networkCompression;
			}
			set
			{
				this.m_networkCompression = value;
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x000C38A9 File Offset: 0x000C1AA9
		// (set) Token: 0x06002F95 RID: 12181 RVA: 0x000C38B1 File Offset: 0x000C1AB1
		public DatabaseAvailabilityGroup.NetworkOption NetworkEncryption
		{
			get
			{
				return this.m_networkEncryption;
			}
			set
			{
				this.m_networkEncryption = value;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000C38BA File Offset: 0x000C1ABA
		// (set) Token: 0x06002F97 RID: 12183 RVA: 0x000C38C2 File Offset: 0x000C1AC2
		public DatabaseAvailabilityGroupNetwork[] Networks
		{
			get
			{
				return this.m_networks;
			}
			set
			{
				this.m_networks = value;
			}
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000C38CC File Offset: 0x000C1ACC
		public DatabaseAvailabilityGroupNetwork FindNetwork(string nameToFind)
		{
			foreach (DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork in this.Networks)
			{
				if (DatabaseAvailabilityGroupNetwork.NameComparer.Equals(nameToFind, databaseAvailabilityGroupNetwork.Name))
				{
					return databaseAvailabilityGroupNetwork;
				}
			}
			return null;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000C390C File Offset: 0x000C1B0C
		public bool FindSubNet(DatabaseAvailabilityGroupSubnetId subnetToFind, out DatabaseAvailabilityGroupNetwork existingNetwork, out DatabaseAvailabilityGroupNetworkSubnet existingSubnet)
		{
			existingNetwork = null;
			existingSubnet = null;
			foreach (DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork in this.Networks)
			{
				foreach (DatabaseAvailabilityGroupNetworkSubnet databaseAvailabilityGroupNetworkSubnet in databaseAvailabilityGroupNetwork.Subnets)
				{
					if (databaseAvailabilityGroupNetworkSubnet.SubnetId.Equals(subnetToFind))
					{
						existingNetwork = databaseAvailabilityGroupNetwork;
						existingSubnet = databaseAvailabilityGroupNetworkSubnet;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040019FB RID: 6651
		private ushort m_replicationPort = 64327;

		// Token: 0x040019FC RID: 6652
		private DatabaseAvailabilityGroup.NetworkOption m_networkCompression = DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly;

		// Token: 0x040019FD RID: 6653
		private DatabaseAvailabilityGroup.NetworkOption m_networkEncryption = DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly;

		// Token: 0x040019FE RID: 6654
		private DatabaseAvailabilityGroupNetwork[] m_networks;
	}
}
