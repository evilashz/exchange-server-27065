using System;
using System.Net;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000252 RID: 594
	internal class NetworkEndPoint
	{
		// Token: 0x0600173A RID: 5946 RVA: 0x00060074 File Offset: 0x0005E274
		public NetworkEndPoint(IPAddress ipAddr, string nodeName, ExchangeSubnet subnet)
		{
			this.m_ipAddress = ipAddr;
			this.m_nodeName = nodeName;
			this.m_subnet = subnet;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00060098 File Offset: 0x0005E298
		public IPAddress IPAddress
		{
			get
			{
				return this.m_ipAddress;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x000600A0 File Offset: 0x0005E2A0
		public string NodeName
		{
			get
			{
				return this.m_nodeName;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x000600A8 File Offset: 0x0005E2A8
		public ExchangeSubnet Subnet
		{
			get
			{
				return this.m_subnet;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x000600B0 File Offset: 0x0005E2B0
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x000600B8 File Offset: 0x0005E2B8
		internal DatabaseAvailabilityGroupNetworkInterface.InterfaceState ClusterNicState
		{
			get
			{
				return this.m_clusterNicState;
			}
			set
			{
				this.m_clusterNicState = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x000600C1 File Offset: 0x0005E2C1
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x000600C9 File Offset: 0x0005E2C9
		internal bool Usable
		{
			get
			{
				return this.m_usable;
			}
			set
			{
				this.m_usable = value;
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000600D4 File Offset: 0x0005E2D4
		internal static DatabaseAvailabilityGroupNetworkInterface.InterfaceState MapNicState(AmNetInterfaceState clusterNicState)
		{
			switch (clusterNicState)
			{
			case AmNetInterfaceState.Unavailable:
				return DatabaseAvailabilityGroupNetworkInterface.InterfaceState.Unavailable;
			case AmNetInterfaceState.Failed:
				return DatabaseAvailabilityGroupNetworkInterface.InterfaceState.Failed;
			case AmNetInterfaceState.Unreachable:
				return DatabaseAvailabilityGroupNetworkInterface.InterfaceState.Unreachable;
			case AmNetInterfaceState.Up:
				return DatabaseAvailabilityGroupNetworkInterface.InterfaceState.Up;
			default:
				return DatabaseAvailabilityGroupNetworkInterface.InterfaceState.Unknown;
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00060104 File Offset: 0x0005E304
		internal void CopyClusterNicState(AmNetInterfaceState clusterNicState)
		{
			this.ClusterNicState = NetworkEndPoint.MapNicState(clusterNicState);
			if (this.ClusterNicState != DatabaseAvailabilityGroupNetworkInterface.InterfaceState.Up && this.ClusterNicState != DatabaseAvailabilityGroupNetworkInterface.InterfaceState.Unreachable)
			{
				NetworkManager.TraceError("NIC {0} is down: {1}", new object[]
				{
					this.IPAddress,
					clusterNicState
				});
				this.Usable = false;
			}
		}

		// Token: 0x0400091E RID: 2334
		private IPAddress m_ipAddress;

		// Token: 0x0400091F RID: 2335
		private string m_nodeName;

		// Token: 0x04000920 RID: 2336
		private ExchangeSubnet m_subnet;

		// Token: 0x04000921 RID: 2337
		private DatabaseAvailabilityGroupNetworkInterface.InterfaceState m_clusterNicState;

		// Token: 0x04000922 RID: 2338
		private bool m_usable = true;
	}
}
