using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EseRepl;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000256 RID: 598
	internal class ExchangeNetwork
	{
		// Token: 0x06001751 RID: 5969 RVA: 0x000603A4 File Offset: 0x0005E5A4
		public ExchangeNetwork(string name, ExchangeNetworkMap map)
		{
			this.m_name = name;
			this.m_map = map;
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x000603D3 File Offset: 0x0005E5D3
		// (set) Token: 0x06001753 RID: 5971 RVA: 0x000603DB File Offset: 0x0005E5DB
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x000603E4 File Offset: 0x0005E5E4
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x000603EC File Offset: 0x0005E5EC
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x000603F5 File Offset: 0x0005E5F5
		// (set) Token: 0x06001757 RID: 5975 RVA: 0x000603FD File Offset: 0x0005E5FD
		public bool MapiAccessEnabled
		{
			get
			{
				return this.m_mapiAccessEnabled;
			}
			internal set
			{
				this.m_mapiAccessEnabled = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x00060406 File Offset: 0x0005E606
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x0006040E File Offset: 0x0005E60E
		public bool ReplicationEnabled
		{
			get
			{
				return this.m_replicationEnabled;
			}
			set
			{
				this.m_replicationEnabled = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00060417 File Offset: 0x0005E617
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x0006041F File Offset: 0x0005E61F
		public bool IgnoreNetwork
		{
			get
			{
				return this.m_ignoreNetwork;
			}
			set
			{
				this.m_ignoreNetwork = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00060428 File Offset: 0x0005E628
		public DatabaseAvailabilityGroup.NetworkOption Compression
		{
			get
			{
				return this.m_map.NetworkManager.NetworkCompression;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0006043A File Offset: 0x0005E63A
		public DatabaseAvailabilityGroup.NetworkOption Encryption
		{
			get
			{
				return this.m_map.NetworkManager.NetworkEncryption;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0006044C File Offset: 0x0005E64C
		public ExchangeSubnetList Subnets
		{
			get
			{
				return this.m_subnets;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x00060454 File Offset: 0x0005E654
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x0006045C File Offset: 0x0005E65C
		internal bool IsMisconfigured
		{
			get
			{
				return this.m_isMisconfigured;
			}
			set
			{
				this.m_isMisconfigured = value;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x00060465 File Offset: 0x0005E665
		internal NetworkNodeEndPoints[] EndPoints
		{
			get
			{
				return this.m_endPoints;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0006046D File Offset: 0x0005E66D
		// (set) Token: 0x06001763 RID: 5987 RVA: 0x00060475 File Offset: 0x0005E675
		internal ExchangeNetworkPerfmonCounters PerfCounters
		{
			get
			{
				return this.m_perfmonCounters;
			}
			set
			{
				this.m_perfmonCounters = value;
			}
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00060480 File Offset: 0x0005E680
		internal void AddEndPoint(NetworkEndPoint ep, int nodeIndex)
		{
			if (this.m_endPoints == null)
			{
				this.m_endPoints = new NetworkNodeEndPoints[this.m_map.Nodes.Count];
			}
			NetworkNodeEndPoints networkNodeEndPoints = this.m_endPoints[nodeIndex];
			if (networkNodeEndPoints == null)
			{
				networkNodeEndPoints = new NetworkNodeEndPoints();
				this.m_endPoints[nodeIndex] = networkNodeEndPoints;
			}
			if (networkNodeEndPoints.EndPoints.Count > 0)
			{
				string errorText = string.Format("Multiple endpoints for node {0} on network {1}. Ignoring ep:{2}.", ep.NodeName, this.Name, ep.IPAddress);
				this.m_map.RecordInconsistency(errorText);
				this.IsMisconfigured = true;
			}
			else
			{
				NetworkManager.TraceDebug("Added endpoint for node {0} on network {1} at {2}", new object[]
				{
					ep.NodeName,
					this.Name,
					ep.IPAddress
				});
			}
			networkNodeEndPoints.EndPoints.Add(ep);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00060544 File Offset: 0x0005E744
		internal void ReportError(NetworkPath path)
		{
			this.m_map.ReportError(path, this);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00060554 File Offset: 0x0005E754
		internal AmNetworkRole GetNativeClusterNetworkRole()
		{
			AmNetworkRole result;
			if (this.IgnoreNetwork)
			{
				result = AmNetworkRole.ClusterNetworkRoleNone;
			}
			else if (this.MapiAccessEnabled)
			{
				result = AmNetworkRole.ClusterNetworkRoleInternalAndClient;
			}
			else
			{
				result = AmNetworkRole.ClusterNetworkRoleInternalUse;
			}
			return result;
		}

		// Token: 0x04000926 RID: 2342
		private string m_name;

		// Token: 0x04000927 RID: 2343
		private string m_description;

		// Token: 0x04000928 RID: 2344
		private ExchangeNetworkMap m_map;

		// Token: 0x04000929 RID: 2345
		private bool m_mapiAccessEnabled = true;

		// Token: 0x0400092A RID: 2346
		private bool m_replicationEnabled = true;

		// Token: 0x0400092B RID: 2347
		private bool m_ignoreNetwork;

		// Token: 0x0400092C RID: 2348
		private bool m_isMisconfigured;

		// Token: 0x0400092D RID: 2349
		private ExchangeSubnetList m_subnets = new ExchangeSubnetList();

		// Token: 0x0400092E RID: 2350
		private NetworkNodeEndPoints[] m_endPoints;

		// Token: 0x0400092F RID: 2351
		private ExchangeNetworkPerfmonCounters m_perfmonCounters;
	}
}
