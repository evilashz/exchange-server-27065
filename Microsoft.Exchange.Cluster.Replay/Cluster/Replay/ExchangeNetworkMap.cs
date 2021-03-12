using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000257 RID: 599
	internal class ExchangeNetworkMap
	{
		// Token: 0x06001767 RID: 5991 RVA: 0x0006057C File Offset: 0x0005E77C
		internal ExchangeNetworkMap(NetworkManager mgr)
		{
			this.m_mgr = mgr;
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x000605D8 File Offset: 0x0005E7D8
		internal NetworkManager NetworkManager
		{
			get
			{
				return this.m_mgr;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x000605E0 File Offset: 0x0005E7E0
		internal SortedList<string, ExchangeNetwork> Networks
		{
			get
			{
				return this.m_networks;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x000605E8 File Offset: 0x0005E7E8
		internal SortedList<DatabaseAvailabilityGroupSubnetId, ExchangeSubnet> SortedSubnets
		{
			get
			{
				return this.m_subnets;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x000605F0 File Offset: 0x0005E7F0
		internal SortedList<string, ExchangeNetworkNode> Nodes
		{
			get
			{
				return this.m_nodes;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x000605F8 File Offset: 0x0005E7F8
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x00060600 File Offset: 0x0005E800
		internal bool ConfigUpdated
		{
			get
			{
				return this.m_configUpdated;
			}
			set
			{
				this.m_configUpdated = value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x00060609 File Offset: 0x0005E809
		// (set) Token: 0x0600176F RID: 5999 RVA: 0x00060611 File Offset: 0x0005E811
		private int SourceNodeIndex
		{
			get
			{
				return this.m_sourceNodeIndex;
			}
			set
			{
				this.m_sourceNodeIndex = value;
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0006061A File Offset: 0x0005E81A
		public void GetWriterLock()
		{
			this.m_rwLock.AcquireWriterLock(-1);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00060628 File Offset: 0x0005E828
		public void ReleaseWriterLock()
		{
			this.m_rwLock.ReleaseWriterLock();
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00060635 File Offset: 0x0005E835
		public void GetReaderLock()
		{
			this.m_rwLock.AcquireReaderLock(-1);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00060643 File Offset: 0x0005E843
		public void ReleaseReaderLock()
		{
			this.m_rwLock.ReleaseReaderLock();
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00060650 File Offset: 0x0005E850
		public void RecordInconsistency(string errorText)
		{
			ExchangeNetworkMap.Tracer.TraceError<string>((long)this.GetHashCode(), "ExchangeNetworkMap Inconsistency: {0}", errorText);
			ReplayCrimsonEvents.NetworkDiscoveryInconsistent.LogPeriodic<string>("NetworkMap", DiagCore.DefaultEventSuppressionInterval, errorText);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00060680 File Offset: 0x0005E880
		public NetworkPath ChoosePath(string targetNodeName, string selectedNetworkName)
		{
			this.m_rwLock.AcquireWriterLock(-1);
			NetworkPath result;
			try
			{
				int num = this.Nodes.IndexOfKey(targetNodeName);
				if (num < 0)
				{
					NetworkManager.TraceDebug("Node {0} is not in the DAG", new object[]
					{
						targetNodeName
					});
					result = null;
				}
				else
				{
					ExchangeNetworkNode exchangeNetworkNode = this.Nodes.Values[num];
					if (exchangeNetworkNode.ClusterState == AmNodeState.Down)
					{
						NetworkManager.TraceDebug("Node {0} is reported as down.", new object[]
						{
							exchangeNetworkNode.Name
						});
						result = null;
					}
					else
					{
						ExchangeNetwork exchangeNetwork = null;
						NetworkEndPoint networkEndPoint = null;
						NetworkEndPoint networkEndPoint2 = null;
						if (selectedNetworkName != null)
						{
							exchangeNetwork = this.UseNetwork(selectedNetworkName, exchangeNetworkNode, num, out networkEndPoint, out networkEndPoint2);
						}
						else
						{
							if (this.m_preferredNets != null)
							{
								exchangeNetwork = this.ChooseNetwork(this.m_preferredNets, exchangeNetworkNode, num, out networkEndPoint, out networkEndPoint2);
							}
							if (exchangeNetwork == null && this.m_regularNets != null)
							{
								exchangeNetwork = this.ChooseNetwork(this.m_regularNets, exchangeNetworkNode, num, out networkEndPoint, out networkEndPoint2);
							}
						}
						if (exchangeNetwork == null)
						{
							NetworkManager.TraceDebug("All paths to Node {0} are down", new object[]
							{
								targetNodeName
							});
							result = null;
						}
						else
						{
							IPAddress ipaddress = networkEndPoint2.IPAddress;
							if (ipaddress.IsIPv6LinkLocal)
							{
								if (networkEndPoint.IPAddress.AddressFamily != AddressFamily.InterNetworkV6)
								{
									NetworkManager.TraceError("Target {0} has linkLocal v6 addr {1} which is unreachable on outbound ip {2}", new object[]
									{
										targetNodeName,
										ipaddress,
										networkEndPoint.IPAddress
									});
									return null;
								}
								byte[] addressBytes = ipaddress.GetAddressBytes();
								ipaddress = new IPAddress(addressBytes, networkEndPoint.IPAddress.ScopeId);
							}
							result = new NetworkPath(targetNodeName, ipaddress, (int)this.m_mgr.ReplicationPort, networkEndPoint.IPAddress)
							{
								NetworkName = exchangeNetwork.Name,
								CrossSubnet = (networkEndPoint.Subnet != networkEndPoint2.Subnet)
							};
						}
					}
				}
			}
			finally
			{
				this.m_rwLock.ReleaseWriterLock();
			}
			return result;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00060860 File Offset: 0x0005EA60
		private static IPAddress ConvertIP4Over6To4(IPAddress input)
		{
			IPAddress result = input;
			if (input.AddressFamily == AddressFamily.InterNetworkV6)
			{
				byte[] addressBytes = input.GetAddressBytes();
				int num = 0;
				while (num < 10 && addressBytes[num] == 0)
				{
					num++;
				}
				if (num == 10 && ((addressBytes[10] == 0 && addressBytes[11] == 0) || (addressBytes[10] == 255 && addressBytes[11] == 255)))
				{
					byte[] array = new byte[4];
					Array.Copy(addressBytes, 12, array, 0, 4);
					result = new IPAddress(array);
				}
			}
			return result;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000608D4 File Offset: 0x0005EAD4
		public List<NetworkPath> EnumeratePaths(string targetNodeName, bool ignoreNodeDown)
		{
			this.m_rwLock.AcquireReaderLock(-1);
			List<NetworkPath> result;
			try
			{
				int num = this.Nodes.IndexOfKey(targetNodeName);
				if (num < 0)
				{
					NetworkManager.TraceDebug("Node {0} is not in the DAG", new object[]
					{
						targetNodeName
					});
					result = null;
				}
				else
				{
					ExchangeNetworkNode exchangeNetworkNode = this.Nodes.Values[num];
					if (exchangeNetworkNode.ClusterState == AmNodeState.Down && !ignoreNodeDown)
					{
						NetworkManager.TraceDebug("Node {0} is reported as down.", new object[]
						{
							exchangeNetworkNode.Name
						});
						result = null;
					}
					else
					{
						List<NetworkPath> list = new List<NetworkPath>();
						this.EnumeratePaths(list, this.m_preferredNets, exchangeNetworkNode, num);
						this.EnumeratePaths(list, this.m_regularNets, exchangeNetworkNode, num);
						result = list;
					}
				}
			}
			finally
			{
				this.m_rwLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x000609A0 File Offset: 0x0005EBA0
		internal void SetupPerfmon()
		{
			foreach (KeyValuePair<string, ExchangeNetwork> keyValuePair in this.Networks)
			{
				ExchangeNetwork value = keyValuePair.Value;
				if (value.ReplicationEnabled && !value.IgnoreNetwork && value.Subnets.Count > 0)
				{
					NetworkManagerPerfmonInstance instance = NetworkManagerPerfmon.GetInstance(value.Name);
					ExchangeNetworkPerfmonCounters perfCounters = new ExchangeNetworkPerfmonCounters(instance);
					value.PerfCounters = perfCounters;
				}
			}
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00060A2C File Offset: 0x0005EC2C
		internal void SynchronizeClusterNetworkRoles(IAmCluster cluster)
		{
			IEnumerable<AmClusterNetwork> enumerable = cluster.EnumerateNetworks();
			try
			{
				foreach (AmClusterNetwork amClusterNetwork in enumerable)
				{
					DatabaseAvailabilityGroupSubnetId databaseAvailabilityGroupSubnetId = ExchangeSubnet.ExtractSubnetId(amClusterNetwork);
					if (databaseAvailabilityGroupSubnetId != null)
					{
						ExchangeSubnet exchangeSubnet = this.FindSubnet(databaseAvailabilityGroupSubnetId);
						if (exchangeSubnet != null)
						{
							AmNetworkRole nativeClusterNetworkRole = exchangeSubnet.Network.GetNativeClusterNetworkRole();
							AmNetworkRole nativeRole = amClusterNetwork.GetNativeRole();
							if (nativeRole != nativeClusterNetworkRole)
							{
								NetworkManager.TraceDebug("Changing network role for subnet {0} from {1} to {2}", new object[]
								{
									databaseAvailabilityGroupSubnetId,
									nativeRole,
									nativeClusterNetworkRole
								});
								bool flag = false;
								try
								{
									amClusterNetwork.SetNativeRole(nativeClusterNetworkRole);
									flag = true;
								}
								finally
								{
									if (!flag)
									{
										if (nativeClusterNetworkRole == AmNetworkRole.ClusterNetworkRoleNone)
										{
											exchangeSubnet.Network.IgnoreNetwork = false;
											this.ConfigUpdated = true;
										}
									}
									else
									{
										ReplayEventLogConstants.Tuple_NetworkRoleChanged.LogEvent(null, new object[]
										{
											amClusterNetwork.Name,
											databaseAvailabilityGroupSubnetId.ToString(),
											nativeRole.ToString(),
											nativeClusterNetworkRole.ToString()
										});
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				foreach (AmClusterNetwork amClusterNetwork2 in enumerable)
				{
					using (amClusterNetwork2)
					{
					}
				}
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00060C0C File Offset: 0x0005EE0C
		internal void Load(NetworkDiscovery netConfig)
		{
			this.m_preferredNets = new List<ExchangeNetwork>();
			this.m_regularNets = new List<ExchangeNetwork>();
			foreach (ClusterNode clusterNode in netConfig.Nodes)
			{
				ExchangeNetworkNode exchangeNetworkNode = new ExchangeNetworkNode(clusterNode.Name.NetbiosName);
				exchangeNetworkNode.ClusterState = clusterNode.ClusterState;
				this.m_nodes.Add(exchangeNetworkNode.Name, exchangeNetworkNode);
			}
			string machineName = Environment.MachineName;
			int sourceNodeIndex = this.Nodes.IndexOfKey(machineName);
			this.SourceNodeIndex = sourceNodeIndex;
			foreach (LogicalNetwork logicalNetwork in netConfig.LogicalNetworks)
			{
				ExchangeNetwork exchangeNetwork = new ExchangeNetwork(logicalNetwork.Name, this);
				exchangeNetwork.Description = logicalNetwork.Description;
				exchangeNetwork.ReplicationEnabled = logicalNetwork.ReplicationEnabled;
				exchangeNetwork.IgnoreNetwork = logicalNetwork.IgnoreNetwork;
				exchangeNetwork.MapiAccessEnabled = logicalNetwork.HasDnsNic();
				this.m_networks.Add(exchangeNetwork.Name, exchangeNetwork);
				foreach (Subnet subnet in logicalNetwork.Subnets)
				{
					if (this.m_subnets.ContainsKey(subnet.SubnetId))
					{
						exchangeNetwork.IsMisconfigured = true;
						string errorText = string.Format("Ignoring subnet {0}. It was mapped to multiple nets.", subnet.SubnetId);
						this.RecordInconsistency(errorText);
					}
					else
					{
						ExchangeSubnet exchangeSubnet = this.AddSubnetToMap(subnet.SubnetId, exchangeNetwork);
						ClusterNetwork clusterNetwork = subnet.ClusterNetwork;
						if (clusterNetwork == null)
						{
							exchangeNetwork.IsMisconfigured = true;
							string errorText2 = string.Format("Subnet {0} configured but no cluster network matched.", subnet.SubnetId);
							this.RecordInconsistency(errorText2);
						}
						else
						{
							exchangeSubnet.SubnetAndState.State = ExchangeSubnet.MapSubnetState(clusterNetwork.ClusterState);
							foreach (ClusterNic clusterNic in clusterNetwork.Nics)
							{
								int num = this.Nodes.IndexOfKey(clusterNic.NodeName);
								if (num < 0)
								{
									string errorText3 = string.Format("Nic {0} has unknown Node {1}.", clusterNic.IPAddress, clusterNic.NodeName);
									this.RecordInconsistency(errorText3);
								}
								else
								{
									NetworkEndPoint networkEndPoint = new NetworkEndPoint(clusterNic.IPAddress, clusterNic.NodeName, exchangeSubnet);
									exchangeSubnet.Network.AddEndPoint(networkEndPoint, num);
									networkEndPoint.CopyClusterNicState(clusterNic.ClusterState);
								}
							}
						}
					}
				}
				if (!exchangeNetwork.IgnoreNetwork && exchangeNetwork.Subnets.Count != 0 && exchangeNetwork.ReplicationEnabled)
				{
					if (exchangeNetwork.MapiAccessEnabled)
					{
						this.m_regularNets.Add(exchangeNetwork);
					}
					else
					{
						this.m_preferredNets.Add(exchangeNetwork);
					}
				}
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00060F58 File Offset: 0x0005F158
		internal ExchangeNetwork GetNetwork(string netName)
		{
			if (netName != null)
			{
				ExchangeNetwork result = null;
				if (this.Networks.TryGetValue(netName, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00060F7D File Offset: 0x0005F17D
		public PersistentDagNetworkConfig BuildPersistentDagNetworkConfig()
		{
			return this.BuildNetConfigWithChange(null);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00060F88 File Offset: 0x0005F188
		private PersistentDagNetworkConfig BuildNetConfigWithChange(SetDagNetworkRequest changeReq)
		{
			PersistentDagNetworkConfig persistentDagNetworkConfig = new PersistentDagNetworkConfig();
			persistentDagNetworkConfig.ReplicationPort = this.m_mgr.ReplicationPort;
			persistentDagNetworkConfig.NetworkCompression = this.m_mgr.NetworkCompression;
			persistentDagNetworkConfig.NetworkEncryption = this.m_mgr.NetworkEncryption;
			foreach (KeyValuePair<string, ExchangeNetwork> keyValuePair in this.m_networks)
			{
				ExchangeNetwork value = keyValuePair.Value;
				PersistentDagNetwork persistentDagNetwork = new PersistentDagNetwork();
				persistentDagNetwork.Name = value.Name;
				persistentDagNetwork.Description = value.Description;
				persistentDagNetwork.ReplicationEnabled = value.ReplicationEnabled;
				persistentDagNetwork.IgnoreNetwork = value.IgnoreNetwork;
				bool flag = false;
				if (changeReq != null && DatabaseAvailabilityGroupNetwork.NameComparer.Equals(changeReq.Name, value.Name))
				{
					if (changeReq.NewName != null)
					{
						persistentDagNetwork.Name = changeReq.NewName;
					}
					if (changeReq.Description != null)
					{
						persistentDagNetwork.Description = changeReq.Description;
					}
					if (changeReq.IsIgnoreChanged)
					{
						persistentDagNetwork.IgnoreNetwork = changeReq.IgnoreNetwork;
					}
					if (changeReq.IsReplicationChanged)
					{
						persistentDagNetwork.ReplicationEnabled = changeReq.ReplicationEnabled;
					}
					if (changeReq.SubnetListIsSet || changeReq.Subnets.Count > 0)
					{
						flag = true;
						foreach (KeyValuePair<DatabaseAvailabilityGroupSubnetId, object> keyValuePair2 in changeReq.Subnets)
						{
							persistentDagNetwork.Subnets.Add(keyValuePair2.Key.ToString());
						}
					}
				}
				if (!flag)
				{
					foreach (ExchangeSubnet exchangeSubnet in value.Subnets)
					{
						if (changeReq != null && changeReq.Subnets.Count > 0 && changeReq.Subnets.ContainsKey(exchangeSubnet.SubnetId))
						{
							NetworkManager.TraceDebug("Subnet '{0}' moving from net '{1}' to net '{2}'", new object[]
							{
								exchangeSubnet.SubnetId,
								value.Name,
								changeReq.LatestName
							});
						}
						else
						{
							string item = exchangeSubnet.SubnetId.ToString();
							persistentDagNetwork.Subnets.Add(item);
						}
					}
				}
				persistentDagNetworkConfig.Networks.Add(persistentDagNetwork);
			}
			return persistentDagNetworkConfig;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0006121C File Offset: 0x0005F41C
		internal PersistentDagNetworkConfig UpdateNetConfig(SetDagNetworkRequest changeReq)
		{
			ExchangeNetwork network = this.GetNetwork(changeReq.Name);
			if (changeReq.NewName != null)
			{
				ExchangeNetwork network2 = this.GetNetwork(changeReq.NewName);
				if (network2 != null && network2 != network)
				{
					NetworkManager.TraceError("SetDagNetwork Cannot rename {0} because {1} already exists", new object[]
					{
						changeReq.Name,
						changeReq.NewName
					});
					throw new DagNetworkManagementException(ServerStrings.DagNetworkCreateDupName(changeReq.NewName));
				}
			}
			PersistentDagNetworkConfig persistentDagNetworkConfig = this.BuildNetConfigWithChange(changeReq);
			if (network == null)
			{
				PersistentDagNetwork persistentDagNetwork = new PersistentDagNetwork();
				persistentDagNetwork.Name = changeReq.Name;
				persistentDagNetwork.Description = changeReq.Description;
				persistentDagNetwork.IgnoreNetwork = changeReq.IgnoreNetwork;
				persistentDagNetwork.ReplicationEnabled = changeReq.ReplicationEnabled;
				if (changeReq.Subnets.Count > 0)
				{
					foreach (DatabaseAvailabilityGroupSubnetId databaseAvailabilityGroupSubnetId in changeReq.Subnets.Keys)
					{
						persistentDagNetwork.Subnets.Add(databaseAvailabilityGroupSubnetId.ToString());
					}
				}
				persistentDagNetworkConfig.Networks.Add(persistentDagNetwork);
			}
			return persistentDagNetworkConfig;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00061348 File Offset: 0x0005F548
		internal void ReportError(NetworkPath path, ExchangeNetwork xNet)
		{
			this.GetWriterLock();
			try
			{
				int num = this.Nodes.IndexOfKey(path.TargetNodeName);
				if (num >= 0)
				{
					NetworkNodeEndPoints[] endPoints = xNet.EndPoints;
					if (endPoints != null && endPoints[num] != null && endPoints[num].EndPoints.Count != 0)
					{
						NetworkEndPoint networkEndPoint = endPoints[num].EndPoints[0];
						if (networkEndPoint.IPAddress.Equals(path.TargetEndPoint.Address))
						{
							networkEndPoint.Usable = false;
						}
					}
				}
			}
			finally
			{
				this.ReleaseWriterLock();
			}
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000613DC File Offset: 0x0005F5DC
		internal ExchangeNetwork LookupEndPoint(IPAddress ipAddr, out NetworkEndPoint matchingEndPoint)
		{
			matchingEndPoint = null;
			ipAddr = ExchangeNetworkMap.ConvertIP4Over6To4(ipAddr);
			this.GetReaderLock();
			try
			{
				foreach (KeyValuePair<string, ExchangeNetwork> keyValuePair in this.m_networks)
				{
					ExchangeNetwork value = keyValuePair.Value;
					if (value.EndPoints != null)
					{
						foreach (NetworkNodeEndPoints networkNodeEndPoints in value.EndPoints)
						{
							if (networkNodeEndPoints != null && networkNodeEndPoints.EndPoints.Count > 0)
							{
								NetworkEndPoint networkEndPoint = networkNodeEndPoints.EndPoints[0];
								if (networkEndPoint.IPAddress.Equals(ipAddr))
								{
									matchingEndPoint = networkEndPoint;
									return value;
								}
							}
						}
					}
				}
			}
			finally
			{
				this.ReleaseReaderLock();
			}
			return null;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000614B8 File Offset: 0x0005F6B8
		internal DatabaseAvailabilityGroupNetwork DescribeDagNetwork(ExchangeNetwork xNet)
		{
			DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork = new DatabaseAvailabilityGroupNetwork();
			databaseAvailabilityGroupNetwork.Name = xNet.Name;
			databaseAvailabilityGroupNetwork.Description = xNet.Description;
			databaseAvailabilityGroupNetwork.MapiAccessEnabled = xNet.MapiAccessEnabled;
			databaseAvailabilityGroupNetwork.ReplicationEnabled = xNet.ReplicationEnabled;
			databaseAvailabilityGroupNetwork.IgnoreNetwork = xNet.IgnoreNetwork;
			List<DatabaseAvailabilityGroupNetworkSubnet> list = new List<DatabaseAvailabilityGroupNetworkSubnet>();
			foreach (ExchangeSubnet exchangeSubnet in xNet.Subnets)
			{
				if (xNet.IsMisconfigured)
				{
					exchangeSubnet.SubnetAndState.State = DatabaseAvailabilityGroupNetworkSubnet.SubnetState.Misconfigured;
				}
				list.Add(exchangeSubnet.SubnetAndState);
			}
			databaseAvailabilityGroupNetwork.Subnets = list.ToArray();
			List<DatabaseAvailabilityGroupNetworkInterface> list2 = new List<DatabaseAvailabilityGroupNetworkInterface>();
			if (xNet.EndPoints != null)
			{
				foreach (NetworkNodeEndPoints networkNodeEndPoints in xNet.EndPoints)
				{
					if (networkNodeEndPoints != null)
					{
						foreach (NetworkEndPoint networkEndPoint in networkNodeEndPoints.EndPoints)
						{
							list2.Add(new DatabaseAvailabilityGroupNetworkInterface
							{
								NodeName = networkEndPoint.NodeName,
								State = networkEndPoint.ClusterNicState,
								IPAddress = networkEndPoint.IPAddress
							});
						}
					}
				}
			}
			databaseAvailabilityGroupNetwork.Interfaces = list2.ToArray();
			return databaseAvailabilityGroupNetwork;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00061644 File Offset: 0x0005F844
		internal DatabaseAvailabilityGroupNetwork[] GetDagNets()
		{
			if (this.m_networks.Count <= 0)
			{
				return null;
			}
			DatabaseAvailabilityGroupNetwork[] array = new DatabaseAvailabilityGroupNetwork[this.m_networks.Count];
			int num = 0;
			foreach (KeyValuePair<string, ExchangeNetwork> keyValuePair in this.m_networks)
			{
				ExchangeNetwork value = keyValuePair.Value;
				array[num++] = this.DescribeDagNetwork(value);
			}
			return array;
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x000616CC File Offset: 0x0005F8CC
		private ExchangeSubnet AddSubnetToMap(DatabaseAvailabilityGroupSubnetId subnetId, ExchangeNetwork owningNetwork)
		{
			ExchangeSubnet exchangeSubnet = new ExchangeSubnet(subnetId);
			exchangeSubnet.Network = owningNetwork;
			owningNetwork.Subnets.Add(exchangeSubnet);
			this.m_subnets.Add(exchangeSubnet.SubnetId, exchangeSubnet);
			return exchangeSubnet;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00061708 File Offset: 0x0005F908
		private ExchangeSubnet FindSubnet(DatabaseAvailabilityGroupSubnetId subnetId)
		{
			ExchangeSubnet result = null;
			if (!this.m_subnets.TryGetValue(subnetId, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0006172C File Offset: 0x0005F92C
		private ExchangeNetwork ChooseNetwork(List<ExchangeNetwork> lruList, ExchangeNetworkNode xNode, int targetNodeIndex, out NetworkEndPoint sourceEp, out NetworkEndPoint targetEp)
		{
			sourceEp = null;
			targetEp = null;
			foreach (ExchangeNetwork exchangeNetwork in lruList)
			{
				if (this.ExtractEndpoints(exchangeNetwork, xNode, targetNodeIndex, out sourceEp, out targetEp))
				{
					lruList.Remove(exchangeNetwork);
					lruList.Add(exchangeNetwork);
					return exchangeNetwork;
				}
			}
			return null;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000617A0 File Offset: 0x0005F9A0
		private ExchangeNetwork UseNetwork(string selectedNetworkName, ExchangeNetworkNode xNode, int targetNodeIndex, out NetworkEndPoint sourceEp, out NetworkEndPoint targetEp)
		{
			sourceEp = null;
			targetEp = null;
			ExchangeNetwork exchangeNetwork;
			if (!this.Networks.TryGetValue(selectedNetworkName, out exchangeNetwork))
			{
				throw new NetworkNameException(selectedNetworkName);
			}
			if (!exchangeNetwork.ReplicationEnabled)
			{
				throw new NetworkNotUsableException(selectedNetworkName, xNode.Name, ReplayStrings.NetworkIsDisabled);
			}
			if (!this.ExtractEndpoints(exchangeNetwork, xNode, targetNodeIndex, out sourceEp, out targetEp))
			{
				throw new NetworkNotUsableException(selectedNetworkName, xNode.Name, ReplayStrings.NetworkNoUsableEndpoints);
			}
			return exchangeNetwork;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00061814 File Offset: 0x0005FA14
		private bool ExtractEndpoints(ExchangeNetwork xNet, ExchangeNetworkNode xNode, int targetNodeIndex, out NetworkEndPoint sourceEp, out NetworkEndPoint targetEp)
		{
			sourceEp = null;
			targetEp = null;
			NetworkNodeEndPoints[] endPoints = xNet.EndPoints;
			if (endPoints == null || endPoints[this.SourceNodeIndex] == null || endPoints[targetNodeIndex] == null || endPoints[this.SourceNodeIndex].EndPoints.Count == 0 || endPoints[targetNodeIndex].EndPoints.Count == 0)
			{
				NetworkManager.TraceDebug("Node {0} is not reachable over network {1}", new object[]
				{
					xNode.Name,
					xNet.Name
				});
				return false;
			}
			sourceEp = endPoints[this.SourceNodeIndex].EndPoints[0];
			targetEp = endPoints[targetNodeIndex].EndPoints[0];
			if (!sourceEp.Usable || !targetEp.Usable)
			{
				NetworkManager.TraceDebug("Node {0} is not reachable over network {1}. EP({2})Usable:{3}. EP({4})Usable:{5}", new object[]
				{
					xNode.Name,
					xNet.Name,
					sourceEp.IPAddress,
					sourceEp.Usable,
					targetEp.IPAddress,
					targetEp.Usable
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00061924 File Offset: 0x0005FB24
		private void EnumeratePaths(List<NetworkPath> possiblePaths, List<ExchangeNetwork> netList, ExchangeNetworkNode xNode, int targetNodeIndex)
		{
			NetworkEndPoint networkEndPoint = null;
			NetworkEndPoint networkEndPoint2 = null;
			foreach (ExchangeNetwork exchangeNetwork in netList)
			{
				if (this.ExtractEndpoints(exchangeNetwork, xNode, targetNodeIndex, out networkEndPoint, out networkEndPoint2))
				{
					possiblePaths.Add(new NetworkPath(xNode.Name, networkEndPoint2.IPAddress, (int)this.m_mgr.ReplicationPort, networkEndPoint.IPAddress)
					{
						NetworkName = exchangeNetwork.Name,
						CrossSubnet = (networkEndPoint.Subnet != networkEndPoint2.Subnet)
					});
				}
			}
		}

		// Token: 0x04000930 RID: 2352
		private static readonly Trace Tracer = ExTraceGlobals.NetworkManagerTracer;

		// Token: 0x04000931 RID: 2353
		protected ReaderWriterLock m_rwLock = new ReaderWriterLock();

		// Token: 0x04000932 RID: 2354
		private NetworkManager m_mgr;

		// Token: 0x04000933 RID: 2355
		private SortedList<string, ExchangeNetwork> m_networks = new SortedList<string, ExchangeNetwork>(DatabaseAvailabilityGroupNetwork.NameComparer);

		// Token: 0x04000934 RID: 2356
		private List<ExchangeNetwork> m_preferredNets;

		// Token: 0x04000935 RID: 2357
		private List<ExchangeNetwork> m_regularNets;

		// Token: 0x04000936 RID: 2358
		private SortedList<DatabaseAvailabilityGroupSubnetId, ExchangeSubnet> m_subnets = new SortedList<DatabaseAvailabilityGroupSubnetId, ExchangeSubnet>(DagSubnetIdComparer.Comparer);

		// Token: 0x04000937 RID: 2359
		private SortedList<string, ExchangeNetworkNode> m_nodes = new SortedList<string, ExchangeNetworkNode>(MachineName.Comparer);

		// Token: 0x04000938 RID: 2360
		private int m_sourceNodeIndex = -1;

		// Token: 0x04000939 RID: 2361
		private bool m_configUpdated;
	}
}
