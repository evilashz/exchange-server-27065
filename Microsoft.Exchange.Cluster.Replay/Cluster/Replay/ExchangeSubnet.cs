using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000254 RID: 596
	internal class ExchangeSubnet
	{
		// Token: 0x06001748 RID: 5960 RVA: 0x0006020D File Offset: 0x0005E40D
		internal ExchangeSubnet(DatabaseAvailabilityGroupNetworkSubnet subnet)
		{
			this.m_subnet = subnet;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0006021C File Offset: 0x0005E41C
		internal ExchangeSubnet(DatabaseAvailabilityGroupSubnetId subnetId)
		{
			this.m_subnet = new DatabaseAvailabilityGroupNetworkSubnet();
			this.m_subnet.SubnetId = subnetId;
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x0006023B File Offset: 0x0005E43B
		internal DatabaseAvailabilityGroupSubnetId SubnetId
		{
			get
			{
				return this.m_subnet.SubnetId;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00060248 File Offset: 0x0005E448
		internal DatabaseAvailabilityGroupNetworkSubnet SubnetAndState
		{
			get
			{
				return this.m_subnet;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00060250 File Offset: 0x0005E450
		// (set) Token: 0x0600174D RID: 5965 RVA: 0x00060258 File Offset: 0x0005E458
		internal ExchangeNetwork Network
		{
			get
			{
				return this.m_network;
			}
			set
			{
				this.m_network = value;
			}
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00060264 File Offset: 0x0005E464
		internal static DatabaseAvailabilityGroupNetworkSubnet.SubnetState MapSubnetState(AmNetworkState clusterState)
		{
			switch (clusterState)
			{
			case AmNetworkState.Unavailable:
				return DatabaseAvailabilityGroupNetworkSubnet.SubnetState.Unavailable;
			case AmNetworkState.Down:
				return DatabaseAvailabilityGroupNetworkSubnet.SubnetState.Down;
			case AmNetworkState.Partitioned:
				return DatabaseAvailabilityGroupNetworkSubnet.SubnetState.Partitioned;
			case AmNetworkState.Up:
				return DatabaseAvailabilityGroupNetworkSubnet.SubnetState.Up;
			default:
				return DatabaseAvailabilityGroupNetworkSubnet.SubnetState.Unknown;
			}
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00060294 File Offset: 0x0005E494
		internal static DatabaseAvailabilityGroupSubnetId ExtractSubnetId(AmClusterNetwork clusNet)
		{
			DatabaseAvailabilityGroupSubnetId databaseAvailabilityGroupSubnetId = null;
			IEnumerable<string> enumerable = clusNet.EnumerateAlternateIPv4Names();
			foreach (string text in enumerable)
			{
				try
				{
					databaseAvailabilityGroupSubnetId = new DatabaseAvailabilityGroupSubnetId(text);
					break;
				}
				catch (FormatException ex)
				{
					NetworkManager.TraceError("Ignoring invalid ipv4 subnet {0}. Exception:{1}", new object[]
					{
						text,
						ex
					});
					databaseAvailabilityGroupSubnetId = null;
				}
			}
			if (databaseAvailabilityGroupSubnetId == null)
			{
				IEnumerable<string> enumerable2 = clusNet.EnumeratePureAlternateIPv6Names();
				foreach (string text2 in enumerable2)
				{
					try
					{
						databaseAvailabilityGroupSubnetId = new DatabaseAvailabilityGroupSubnetId(text2);
						break;
					}
					catch (FormatException ex2)
					{
						NetworkManager.TraceError("Ignoring invalid ipv6 subnet {0}. Exception:{1}", new object[]
						{
							text2,
							ex2
						});
						databaseAvailabilityGroupSubnetId = null;
					}
				}
			}
			return databaseAvailabilityGroupSubnetId;
		}

		// Token: 0x04000924 RID: 2340
		private DatabaseAvailabilityGroupNetworkSubnet m_subnet;

		// Token: 0x04000925 RID: 2341
		private ExchangeNetwork m_network;
	}
}
