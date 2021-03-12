using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000259 RID: 601
	internal class ExchangeNetworkNode
	{
		// Token: 0x0600178C RID: 6028 RVA: 0x000619F7 File Offset: 0x0005FBF7
		public ExchangeNetworkNode(string name)
		{
			this.m_name = name;
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x00061A0D File Offset: 0x0005FC0D
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x00061A15 File Offset: 0x0005FC15
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

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x00061A1E File Offset: 0x0005FC1E
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x00061A26 File Offset: 0x0005FC26
		public AmNodeState ClusterState
		{
			get
			{
				return this.m_clusterState;
			}
			set
			{
				this.m_clusterState = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x00061A2F File Offset: 0x0005FC2F
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x00061A37 File Offset: 0x0005FC37
		internal bool HasDnsBeenChecked
		{
			get
			{
				return this.m_hasDnsBeenChecked;
			}
			set
			{
				this.m_hasDnsBeenChecked = value;
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00061A40 File Offset: 0x0005FC40
		public static List<IPAddress> FindCandidateDnsAddrs()
		{
			List<IPAddress> list = new List<IPAddress>();
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
				bool flag = ipproperties.IsDnsEnabled || ipproperties.IsDynamicDnsEnabled;
				if (flag)
				{
					foreach (IPAddressInformation ipaddressInformation in ipproperties.UnicastAddresses)
					{
						if (ipaddressInformation.IsDnsEligible && !ipaddressInformation.IsTransient)
						{
							list.Add(ipaddressInformation.Address);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00061AF4 File Offset: 0x0005FCF4
		internal static bool IsAddressPresent(IPAddress[] set, IPAddress addr)
		{
			if (set != null)
			{
				foreach (IPAddress obj in set)
				{
					if (addr.Equals(obj))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00061B28 File Offset: 0x0005FD28
		internal void GetDnsRecords()
		{
			Exception ex = null;
			try
			{
				if (MachineName.Comparer.Equals(this.Name, Environment.MachineName))
				{
					List<IPAddress> list = ExchangeNetworkNode.FindCandidateDnsAddrs();
					this.m_dnsAddresses = list.ToArray();
				}
				else
				{
					this.m_dnsAddresses = Dns.GetHostAddresses(this.Name);
				}
				foreach (IPAddress ipaddress in this.m_dnsAddresses)
				{
					NetworkManager.TraceDebug("Node {0} has DNS for {1}", new object[]
					{
						this.Name,
						ipaddress
					});
				}
			}
			catch (SocketException ex2)
			{
				ex = ex2;
			}
			catch (NetworkInformationException ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					NetworkManager.TraceError("NetworkMap.GetDnsRecords failed: {0}", new object[]
					{
						ex
					});
				}
				this.m_hasDnsBeenChecked = true;
			}
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00061C14 File Offset: 0x0005FE14
		internal bool IsEndPointMappedByDNS(IPAddress ipAddr)
		{
			return ExchangeNetworkNode.IsAddressPresent(this.m_dnsAddresses, ipAddr);
		}

		// Token: 0x0400093B RID: 2363
		private string m_name;

		// Token: 0x0400093C RID: 2364
		private AmNodeState m_clusterState = AmNodeState.Unknown;

		// Token: 0x0400093D RID: 2365
		private bool m_hasDnsBeenChecked;

		// Token: 0x0400093E RID: 2366
		private IPAddress[] m_dnsAddresses;
	}
}
