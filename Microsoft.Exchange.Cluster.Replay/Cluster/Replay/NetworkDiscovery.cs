using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000250 RID: 592
	internal class NetworkDiscovery
	{
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0005D3AB File Offset: 0x0005B5AB
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.NetworkManagerTracer;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0005D3B2 File Offset: 0x0005B5B2
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x0005D3BA File Offset: 0x0005B5BA
		public bool InconsistencyDetected { get; set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0005D3C3 File Offset: 0x0005B5C3
		public IEnumerable<ClusterNode> Nodes
		{
			get
			{
				return this.m_clusterNodes;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0005D3CB File Offset: 0x0005B5CB
		public IEnumerable<LogicalNetwork> LogicalNetworks
		{
			get
			{
				return this.m_logicalNets;
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0005D3D4 File Offset: 0x0005B5D4
		public ClusterNode FindNode(string nodeName)
		{
			foreach (ClusterNode clusterNode in this.Nodes)
			{
				if (SharedHelper.StringIEquals(nodeName, clusterNode.Name.NetbiosName))
				{
					return clusterNode;
				}
			}
			return null;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0005D434 File Offset: 0x0005B634
		private void LinkNicsToNodes()
		{
			foreach (ClusterNetwork clusterNetwork in this.m_clusterNets)
			{
				foreach (ClusterNic clusterNic in clusterNetwork.Nics)
				{
					ClusterNode clusterNode = this.FindNode(clusterNic.NodeName);
					if (clusterNode != null)
					{
						clusterNic.ClusterNode = clusterNode;
						clusterNode.Nics.Add(clusterNic);
					}
					else
					{
						string errorText = string.Format("Failed to find node {0} for nic {1}.", clusterNic.NodeName, clusterNic.Name);
						this.RecordInconsistency(errorText);
					}
				}
			}
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0005D504 File Offset: 0x0005B704
		public void LoadClusterObjects(IAmCluster cluster)
		{
			IEnumerable<IAmClusterNode> enumerable = cluster.EnumerateNodes();
			try
			{
				foreach (IAmClusterNode clusNode in enumerable)
				{
					ClusterNode item = new ClusterNode(clusNode);
					this.m_clusterNodes.Add(item);
				}
			}
			finally
			{
				foreach (IAmClusterNode amClusterNode in enumerable)
				{
					using (amClusterNode)
					{
					}
				}
			}
			IEnumerable<AmClusterNetwork> enumerable2 = cluster.EnumerateNetworks();
			try
			{
				foreach (AmClusterNetwork clusNet in enumerable2)
				{
					ClusterNetwork item2 = new ClusterNetwork(clusNet);
					this.m_clusterNets.Add(item2);
				}
			}
			finally
			{
				foreach (AmClusterNetwork amClusterNetwork in enumerable2)
				{
					using (amClusterNetwork)
					{
					}
				}
			}
			int testWithFakeNetwork = RegistryParameters.GetTestWithFakeNetwork();
			if (testWithFakeNetwork > 0)
			{
				this.AddFakeNetworkForTesting(testWithFakeNetwork);
			}
			this.LinkNicsToNodes();
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0005D698 File Offset: 0x0005B898
		private void AddFakeNetworkForTesting(int fakeMode)
		{
			DatabaseAvailabilityGroupSubnetId subnetId = new DatabaseAvailabilityGroupSubnetId("1.1.1.0/24");
			ClusterNetwork clusterNetwork = new ClusterNetwork(subnetId);
			clusterNetwork.ClusterState = AmNetworkState.Up;
			this.m_clusterNets.Add(clusterNetwork);
			int num = 1;
			foreach (ClusterNode clusterNode in this.m_clusterNodes)
			{
				string ipString = string.Format("1.1.1.{0}", num);
				IPAddress ipaddress = IPAddress.Parse(ipString);
				ClusterNic clusterNic = new ClusterNic();
				clusterNic.Name = string.Format("FakeNic{0}", num);
				clusterNic.NodeName = clusterNode.Name.NetbiosName;
				clusterNic.HasIPAddress = true;
				clusterNic.IPAddress = ipaddress;
				clusterNic.ClusterState = AmNetInterfaceState.Up;
				clusterNic.ClusterNetwork = clusterNetwork;
				clusterNetwork.Nics.Add(clusterNic);
				if (fakeMode > 1)
				{
					break;
				}
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0005D790 File Offset: 0x0005B990
		public void DetermineDnsStatus()
		{
			foreach (ClusterNode clusterNode in this.Nodes)
			{
				bool flag = false;
				Exception arg;
				clusterNode.DnsAddresses = NetworkUtil.GetDnsAddresses(clusterNode.Name.Fqdn, ref arg);
				if (clusterNode.DnsAddresses.Length > 0)
				{
					using (List<ClusterNic>.Enumerator enumerator2 = clusterNode.Nics.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ClusterNic clusterNic = enumerator2.Current;
							if (NetworkUtil.IsAddressPresent(clusterNode.DnsAddresses, clusterNic.IPAddress))
							{
								if (flag)
								{
									NetworkDiscovery.Tracer.TraceError<AmServerName, IPAddress>((long)this.GetHashCode(), "Node({0}) has multiple dns nics. DupIP: {1}", clusterNode.Name, clusterNic.IPAddress);
								}
								else
								{
									NetworkDiscovery.Tracer.TraceDebug<AmServerName, IPAddress>((long)this.GetHashCode(), "Node({0}) has DNS IP {1}", clusterNode.Name, clusterNic.IPAddress);
								}
								clusterNic.IsDnsRegistered = true;
								clusterNic.ClusterNetwork.HasDnsNic = true;
								flag = true;
							}
						}
						goto IL_103;
					}
					goto IL_E6;
				}
				goto IL_E6;
				IL_103:
				if (!flag)
				{
					foreach (ClusterNic clusterNic2 in clusterNode.Nics)
					{
						clusterNic2.IsDnsRegistered = true;
						clusterNic2.ClusterNetwork.HasDnsNic = true;
						NetworkDiscovery.Tracer.TraceError<AmServerName, IPAddress>((long)this.GetHashCode(), "Node({0}) Nic({1}) being treated as DNS as a fallback.", clusterNode.Name, clusterNic2.IPAddress);
					}
					continue;
				}
				continue;
				IL_E6:
				NetworkDiscovery.Tracer.TraceError<AmServerName, Exception>((long)this.GetHashCode(), "Node({0}) failed to resolve in DNS: {1}", clusterNode.Name, arg);
				goto IL_103;
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0005D974 File Offset: 0x0005BB74
		private LogicalNetwork AddNewLogicalNetwork()
		{
			LogicalNetwork logicalNetwork = new LogicalNetwork();
			this.m_logicalNets.Add(logicalNetwork);
			return logicalNetwork;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0005D994 File Offset: 0x0005BB94
		public void LoadExistingConfiguration(PersistentDagNetworkConfig existingConfig)
		{
			foreach (PersistentDagNetwork persistentDagNetwork in existingConfig.Networks)
			{
				LogicalNetwork logicalNetwork = this.AddNewLogicalNetwork();
				logicalNetwork.Name = persistentDagNetwork.Name;
				logicalNetwork.Description = persistentDagNetwork.Description;
				logicalNetwork.ReplicationEnabled = persistentDagNetwork.ReplicationEnabled;
				logicalNetwork.IgnoreNetwork = persistentDagNetwork.IgnoreNetwork;
				foreach (string expression in persistentDagNetwork.Subnets)
				{
					DatabaseAvailabilityGroupSubnetId databaseAvailabilityGroupSubnetId = new DatabaseAvailabilityGroupSubnetId(expression);
					if (this.m_subnets.ContainsKey(databaseAvailabilityGroupSubnetId))
					{
						string errorText = string.Format("Ignoring inconsistent DagNetworkConfig. Subnet {0} is defined in multiple places.", databaseAvailabilityGroupSubnetId);
						this.RecordInconsistency(errorText);
					}
					else
					{
						Subnet subnet = new Subnet(databaseAvailabilityGroupSubnetId);
						subnet.LogicalNetwork = logicalNetwork;
						this.m_subnets.Add(databaseAvailabilityGroupSubnetId, subnet);
						logicalNetwork.Add(subnet);
					}
				}
			}
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0005DAAC File Offset: 0x0005BCAC
		private Subnet GetSubnet(DatabaseAvailabilityGroupSubnetId subnetId)
		{
			Subnet result = null;
			if (this.m_subnets.TryGetValue(subnetId, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0005DAD0 File Offset: 0x0005BCD0
		public bool AggregateNetworks(bool pingAllowed)
		{
			bool result = false;
			List<ClusterNetwork> list = new List<ClusterNetwork>(3);
			List<ClusterNetwork> list2 = new List<ClusterNetwork>(3);
			foreach (ClusterNetwork clusterNetwork in this.m_clusterNets)
			{
				Subnet subnet = null;
				if (this.m_subnets.TryGetValue(clusterNetwork.SubnetId, out subnet))
				{
					if (subnet.ClusterNetwork != null)
					{
						string errorText = string.Format("Subnet {0} is used in multiple cluster networks.", clusterNetwork.SubnetId);
						this.RecordInconsistency(errorText);
					}
					else
					{
						subnet.ClusterNetwork = clusterNetwork;
						clusterNetwork.LogicalNetwork = subnet.LogicalNetwork;
					}
				}
				else
				{
					subnet = new Subnet(clusterNetwork);
					this.m_subnets.Add(subnet.SubnetId, subnet);
					NetworkDiscovery.Tracer.TraceDebug<DatabaseAvailabilityGroupSubnetId, bool>((long)this.GetHashCode(), "Subnet '{0}' has DNS = {1}", clusterNetwork.SubnetId, clusterNetwork.HasDnsNic);
					result = true;
					if (clusterNetwork.HasDnsNic)
					{
						list.Add(clusterNetwork);
					}
					else
					{
						list2.Add(clusterNetwork);
					}
				}
			}
			if (list.Count > 0)
			{
				this.HandleUnassignedDnsNets(list);
			}
			if (list2.Count > 0 && pingAllowed)
			{
				this.HandleUnassignedReplNets(list2);
			}
			foreach (ClusterNetwork clusterNetwork2 in this.m_clusterNets)
			{
				if (clusterNetwork2.LogicalNetwork == null)
				{
					Subnet subnet2 = this.GetSubnet(clusterNetwork2.SubnetId);
					if (subnet2 == null)
					{
						string errorText2 = string.Format("A subnet was not constructed for cluster net {0}.", clusterNetwork2.SubnetId);
						this.RecordInconsistency(errorText2);
					}
					else
					{
						LogicalNetwork logicalNetwork = this.AddNewLogicalNetwork();
						logicalNetwork.Add(subnet2);
						clusterNetwork2.LogicalNetwork = logicalNetwork;
					}
				}
			}
			this.ResolveLogicalNetworkNames();
			return result;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0005DC9C File Offset: 0x0005BE9C
		private void RecordInconsistency(string errorText)
		{
			this.InconsistencyDetected = true;
			NetworkDiscovery.Tracer.TraceError<string>((long)this.GetHashCode(), "NetworkDiscovery Inconsistency: {0}", errorText);
			ReplayCrimsonEvents.NetworkDiscoveryInconsistent.LogPeriodic<string>("NetworkDiscovery", DiagCore.DefaultEventSuppressionInterval, errorText);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0005DCD4 File Offset: 0x0005BED4
		public bool RemoveEmptyNets()
		{
			bool result = false;
			for (int i = this.m_logicalNets.Count - 1; i >= 0; i--)
			{
				LogicalNetwork logicalNetwork = this.m_logicalNets[i];
				bool flag = true;
				foreach (Subnet subnet in logicalNetwork.Subnets)
				{
					if (subnet.ClusterNetwork != null)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					foreach (Subnet subnet2 in logicalNetwork.Subnets)
					{
						this.m_subnets.Remove(subnet2.SubnetId);
					}
					this.m_logicalNets.RemoveAt(i);
					result = true;
					NetworkDiscovery.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Net {0} removed since it has no active subnets", logicalNetwork.Name);
				}
			}
			return result;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0005DDDC File Offset: 0x0005BFDC
		private void HandleUnassignedDnsNets(List<ClusterNetwork> unassignedNets)
		{
			LogicalNetwork logicalNetwork = null;
			LogicalNetwork logicalNetwork2 = null;
			foreach (LogicalNetwork logicalNetwork3 in this.LogicalNetworks)
			{
				if (DatabaseAvailabilityGroupNetwork.NameComparer.Equals("MapiDagNetwork", logicalNetwork3.Name))
				{
					logicalNetwork = logicalNetwork3;
				}
				if (logicalNetwork3.HasDnsNic() && logicalNetwork2 == null)
				{
					logicalNetwork2 = logicalNetwork3;
				}
			}
			if (logicalNetwork2 == null)
			{
				if (logicalNetwork == null)
				{
					logicalNetwork = this.AddNewLogicalNetwork();
					logicalNetwork.Name = "MapiDagNetwork";
					NetworkDiscovery.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0} created to hold DNS nets", "MapiDagNetwork");
				}
				logicalNetwork2 = logicalNetwork;
			}
			else if (logicalNetwork != logicalNetwork2 && logicalNetwork != null && logicalNetwork.HasDnsNic())
			{
				logicalNetwork2 = logicalNetwork;
			}
			foreach (ClusterNetwork clusterNetwork in unassignedNets)
			{
				Subnet subnet;
				if (this.m_subnets.TryGetValue(clusterNetwork.SubnetId, out subnet))
				{
					logicalNetwork2.Add(subnet);
					clusterNetwork.LogicalNetwork = logicalNetwork2;
				}
			}
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0005DEF4 File Offset: 0x0005C0F4
		private void HandleUnassignedReplNets(List<ClusterNetwork> unassignedNets)
		{
			ClusterNode clusterNode = this.FindNode(Environment.MachineName);
			if (clusterNode == null)
			{
				string errorText = "Local machine is not present in nodes.";
				this.RecordInconsistency(errorText);
				return;
			}
			foreach (ClusterNic clusterNic in clusterNode.Nics)
			{
				if (clusterNic.ClusterNetwork.LogicalNetwork == null)
				{
					this.ResolveLocalNic(clusterNic);
				}
				if (!clusterNic.ClusterNetwork.HasDnsNic)
				{
					this.ResolveReplNets(clusterNic, unassignedNets);
				}
			}
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0005DF88 File Offset: 0x0005C188
		private bool DoesClusterNetworkContainNode(ClusterNetwork clusNet, string nodeName)
		{
			foreach (ClusterNic clusterNic in clusNet.Nics)
			{
				if (SharedHelper.StringIEquals(nodeName, clusterNic.NodeName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0005DFEC File Offset: 0x0005C1EC
		private List<LogicalNetwork> FindLogicalNetsWithoutNode(string nodeName)
		{
			List<LogicalNetwork> list = new List<LogicalNetwork>(3);
			foreach (LogicalNetwork logicalNetwork in this.LogicalNetworks)
			{
				if (!logicalNetwork.HasDnsNic())
				{
					bool flag = false;
					foreach (Subnet subnet in logicalNetwork.Subnets)
					{
						if (subnet.ClusterNetwork != null)
						{
							flag = this.DoesClusterNetworkContainNode(subnet.ClusterNetwork, nodeName);
							if (flag)
							{
								break;
							}
						}
					}
					if (!flag)
					{
						list.Add(logicalNetwork);
					}
				}
			}
			return list;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0005E0A8 File Offset: 0x0005C2A8
		private void SelectPingCandidates(ClusterNetwork clusnet, List<PingRequest> pingTargets)
		{
			foreach (ClusterNic clusterNic in clusnet.Nics)
			{
				if (clusterNic.ClusterState == AmNetInterfaceState.Up)
				{
					pingTargets.Add(new PingRequest
					{
						IPAddress = clusterNic.IPAddress,
						UserContext = clusterNic
					});
				}
			}
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0005E120 File Offset: 0x0005C320
		private void ResolveLocalNic(ClusterNic localNic)
		{
			Subnet subnet = this.GetSubnet(localNic.ClusterNetwork.SubnetId);
			if (localNic.ClusterState == AmNetInterfaceState.Up)
			{
				List<LogicalNetwork> list = this.FindLogicalNetsWithoutNode(localNic.NodeName);
				if (list.Count > 0)
				{
					List<PingRequest> list2 = new List<PingRequest>(list.Count * 3);
					foreach (LogicalNetwork logicalNetwork in list)
					{
						foreach (Subnet subnet2 in logicalNetwork.Subnets)
						{
							if (subnet2.ClusterNetwork != null)
							{
								this.SelectPingCandidates(subnet2.ClusterNetwork, list2);
							}
						}
					}
					if (list2.Count > 0)
					{
						PingRequest[] array = list2.ToArray();
						PingProber pingProber = null;
						try
						{
							pingProber = new PingProber(localNic.IPAddress);
							pingProber.SendPings(array);
							pingProber.GatherReplies(3000);
							foreach (PingRequest pingRequest in array)
							{
								if (pingRequest.Success)
								{
									ClusterNic clusterNic = (ClusterNic)pingRequest.UserContext;
									localNic.ClusterNetwork.LogicalNetwork = clusterNic.ClusterNetwork.LogicalNetwork;
									localNic.ClusterNetwork.LogicalNetwork.Add(subnet);
									break;
								}
							}
						}
						catch (SocketException arg)
						{
							NetworkDiscovery.Tracer.TraceError<IPAddress, SocketException>((long)this.GetHashCode(), "Prober failed from {0}:{1}", localNic.IPAddress, arg);
						}
						finally
						{
							if (pingProber != null)
							{
								pingProber.Dispose();
							}
						}
					}
				}
			}
			if (localNic.ClusterNetwork.LogicalNetwork == null)
			{
				LogicalNetwork logicalNetwork2 = this.AddNewLogicalNetwork();
				logicalNetwork2.Add(subnet);
				localNic.ClusterNetwork.LogicalNetwork = logicalNetwork2;
			}
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0005E310 File Offset: 0x0005C510
		private void ResolveReplNets(ClusterNic localNic, List<ClusterNetwork> unassignedNets)
		{
			List<PingRequest> list = new List<PingRequest>(30);
			foreach (ClusterNetwork clusterNetwork in unassignedNets)
			{
				if (clusterNetwork.LogicalNetwork == null && !this.DoesClusterNetworkContainNode(clusterNetwork, localNic.NodeName))
				{
					this.SelectPingCandidates(clusterNetwork, list);
				}
			}
			if (list.Count > 0)
			{
				PingRequest[] array = list.ToArray();
				PingProber pingProber = null;
				try
				{
					pingProber = new PingProber(localNic.IPAddress);
					pingProber.SendPings(array);
					pingProber.GatherReplies(3000);
					foreach (PingRequest pingRequest in array)
					{
						if (pingRequest.Success)
						{
							ClusterNic clusterNic = (ClusterNic)pingRequest.UserContext;
							if (clusterNic.ClusterNetwork.LogicalNetwork == null)
							{
								Subnet subnet = this.GetSubnet(clusterNic.ClusterNetwork.SubnetId);
								clusterNic.ClusterNetwork.LogicalNetwork = localNic.ClusterNetwork.LogicalNetwork;
								clusterNic.ClusterNetwork.LogicalNetwork.Add(subnet);
							}
						}
					}
				}
				catch (SocketException arg)
				{
					NetworkDiscovery.Tracer.TraceError<IPAddress, SocketException>((long)this.GetHashCode(), "Prober failed from {0}:{1}", localNic.IPAddress, arg);
				}
				finally
				{
					if (pingProber != null)
					{
						pingProber.Dispose();
					}
				}
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0005E47C File Offset: 0x0005C67C
		private void ResolveLogicalNetworkNames()
		{
			bool[] array = new bool[this.m_logicalNets.Count];
			bool flag = false;
			foreach (LogicalNetwork logicalNetwork in this.m_logicalNets)
			{
				int num;
				if (string.IsNullOrEmpty(logicalNetwork.Name))
				{
					flag = true;
				}
				else if (logicalNetwork.Name.StartsWith("ReplicationDagNetwork", DatabaseAvailabilityGroupNetwork.NameComparison) && int.TryParse(logicalNetwork.Name.Substring("ReplicationDagNetwork".Length), out num) && num > 0 && num <= array.Length)
				{
					array[num - 1] = true;
				}
			}
			if (flag)
			{
				int index = 0;
				foreach (LogicalNetwork logicalNetwork2 in this.m_logicalNets)
				{
					if (string.IsNullOrEmpty(logicalNetwork2.Name))
					{
						while (array[index++])
						{
						}
						logicalNetwork2.Name = LogicalNetwork.BuildDefaultReplNetName(index);
					}
				}
			}
		}

		// Token: 0x04000902 RID: 2306
		private List<LogicalNetwork> m_logicalNets = new List<LogicalNetwork>(3);

		// Token: 0x04000903 RID: 2307
		private List<ClusterNetwork> m_clusterNets = new List<ClusterNetwork>(3);

		// Token: 0x04000904 RID: 2308
		private List<ClusterNode> m_clusterNodes = new List<ClusterNode>(8);

		// Token: 0x04000905 RID: 2309
		private SortedList<DatabaseAvailabilityGroupSubnetId, Subnet> m_subnets = new SortedList<DatabaseAvailabilityGroupSubnetId, Subnet>(DagSubnetIdComparer.Comparer);
	}
}
