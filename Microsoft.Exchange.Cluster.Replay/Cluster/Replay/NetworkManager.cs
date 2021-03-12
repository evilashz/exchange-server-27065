using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000251 RID: 593
	internal class NetworkManager
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0005E5D8 File Offset: 0x0005C7D8
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.NetworkManagerTracer;
			}
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0005E5E0 File Offset: 0x0005C7E0
		private static NetworkOption MapESEReplNetworkOption(DatabaseAvailabilityGroup.NetworkOption option)
		{
			switch (option)
			{
			case DatabaseAvailabilityGroup.NetworkOption.Disabled:
				return NetworkOption.Disabled;
			case DatabaseAvailabilityGroup.NetworkOption.Enabled:
				return NetworkOption.Enabled;
			case DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly:
				return NetworkOption.InterSubnetOnly;
			case DatabaseAvailabilityGroup.NetworkOption.SeedOnly:
				return NetworkOption.SeedOnly;
			default:
				return NetworkOption.Disabled;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0005E610 File Offset: 0x0005C810
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x0005E618 File Offset: 0x0005C818
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

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0005E621 File Offset: 0x0005C821
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x0005E629 File Offset: 0x0005C829
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

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0005E632 File Offset: 0x0005C832
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x0005E63A File Offset: 0x0005C83A
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

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0005E643 File Offset: 0x0005C843
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x0005E64B File Offset: 0x0005C84B
		public bool ManualDagNetworkConfiguration { get; private set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0005E654 File Offset: 0x0005C854
		private AmClusterHandle ClusterHandle
		{
			get
			{
				return this.m_hCluster;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x0005E65C File Offset: 0x0005C85C
		// (set) Token: 0x0600170F RID: 5903 RVA: 0x0005E664 File Offset: 0x0005C864
		private bool EseReplDagNetConfigIsStale { get; set; }

		// Token: 0x06001710 RID: 5904 RVA: 0x0005E670 File Offset: 0x0005C870
		public static NetworkPath ChooseNetworkPath(string targetName, string networkName, NetworkPath.ConnectionPurpose purpose)
		{
			ITcpConnector tcpConnector = Dependencies.TcpConnector;
			return tcpConnector.ChooseDagNetworkPath(targetName, networkName, purpose);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0005E68C File Offset: 0x0005C88C
		internal static NetworkPath InternalChooseDagNetworkPath(string targetName, string networkName, NetworkPath.ConnectionPurpose purpose)
		{
			string nodeNameFromFqdn = MachineName.GetNodeNameFromFqdn(targetName);
			NetworkManager manager = NetworkManager.GetManager();
			NetworkPath networkPath = null;
			DagNetConfig dagNetConfig = null;
			manager.TryWaitForInitialization();
			if (manager.m_netMap != null)
			{
				networkPath = manager.m_netMap.ChoosePath(nodeNameFromFqdn, networkName);
			}
			else
			{
				DagNetRoute dagNetRoute = null;
				DagNetRoute[] array = DagNetChooser.ProposeRoutes(targetName, out dagNetConfig);
				if (networkName != null)
				{
					foreach (DagNetRoute dagNetRoute2 in array)
					{
						if (StringUtil.IsEqualIgnoreCase(dagNetRoute2.NetworkName, networkName))
						{
							dagNetRoute = dagNetRoute2;
							break;
						}
					}
				}
				else if (array != null && array.Length > 0)
				{
					dagNetRoute = array[0];
				}
				if (dagNetRoute != null)
				{
					networkPath = new NetworkPath(targetName, dagNetRoute.TargetIPAddr, dagNetRoute.TargetPort, dagNetRoute.SourceIPAddr);
					networkPath.NetworkName = dagNetRoute.NetworkName;
					networkPath.CrossSubnet = dagNetRoute.IsCrossSubnet;
				}
			}
			if (networkPath == null)
			{
				networkPath = NetworkManager.BuildDnsNetworkPath(targetName, NetworkManager.GetReplicationPort());
			}
			networkPath.Purpose = purpose;
			if (dagNetConfig == null)
			{
				dagNetConfig = DagNetEnvironment.FetchNetConfig();
			}
			networkPath.ApplyNetworkPolicy(dagNetConfig);
			return networkPath;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0005E784 File Offset: 0x0005C984
		private static NetworkPath BuildDnsNetworkPath(string targetName, ushort replicationPort)
		{
			try
			{
				IPAddress ipaddress = NetworkManager.ChooseAddressFromDNS(targetName);
				if (ipaddress != null)
				{
					return new NetworkPath(targetName, ipaddress, (int)replicationPort, null)
					{
						NetworkChoiceIsMandatory = true
					};
				}
			}
			catch (SocketException ex)
			{
				throw new NetworkTransportException(ReplayStrings.NetworkAddressResolutionFailed(targetName, ex.Message), ex);
			}
			throw new NetworkTransportException(ReplayStrings.NetworkAddressResolutionFailedNoDnsEntry(targetName));
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0005E7F0 File Offset: 0x0005C9F0
		public static IPAddress ChooseAddressFromDNS(string targetName)
		{
			Exception ex;
			IPAddress[] dnsAddresses = NetworkUtil.GetDnsAddresses(targetName, ref ex);
			foreach (IPAddress ipaddress in dnsAddresses)
			{
				if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					return ipaddress;
				}
			}
			foreach (IPAddress ipaddress2 in dnsAddresses)
			{
				if (ipaddress2.AddressFamily == AddressFamily.InterNetworkV6)
				{
					return ipaddress2;
				}
			}
			return null;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0005E85F File Offset: 0x0005CA5F
		public static NetworkManager GetManager()
		{
			return NetworkManager.s_mgr;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0005E866 File Offset: 0x0005CA66
		public static ExchangeNetworkMap GetMap()
		{
			return NetworkManager.s_mgr.m_netMap;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0005E874 File Offset: 0x0005CA74
		public static void Start()
		{
			lock (NetworkManager.s_mgrLock)
			{
				if (!NetworkManager.s_initialized)
				{
					NetworkManager.s_mgr.Initialize();
					NetworkManager.s_initialized = true;
				}
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0005E8C4 File Offset: 0x0005CAC4
		public static void ReportError(NetworkPath path, Exception e)
		{
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0005E8C6 File Offset: 0x0005CAC6
		public static void HandleUnexpectedException(Exception e)
		{
			ExTraceGlobals.NetworkManagerTracer.TraceError<Exception>(0L, "UnexpectedException {0}", e);
			throw e;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0005E8DC File Offset: 0x0005CADC
		internal static ushort GetReplicationPort()
		{
			NetworkManager manager = NetworkManager.GetManager();
			if (manager == null)
			{
				return 64327;
			}
			return manager.ReplicationPort;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0005E8FE File Offset: 0x0005CAFE
		internal static void TraceDebug(string format, params object[] args)
		{
			ExTraceGlobals.NetworkManagerTracer.TraceDebug(0L, format, args);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0005E90E File Offset: 0x0005CB0E
		internal static void TraceError(string format, params object[] args)
		{
			ExTraceGlobals.NetworkManagerTracer.TraceError(0L, format, args);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0005E920 File Offset: 0x0005CB20
		internal static void ThrowException(Exception e)
		{
			NetworkManager.TraceError("Throwing exception: {0}", new object[]
			{
				e
			});
			throw e;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0005E944 File Offset: 0x0005CB44
		internal static TcpClientChannel OpenConnection(ref NetworkPath actualPath, int timeoutInMsec, bool ignoreNodeDown)
		{
			NetworkPath networkPath = actualPath;
			NetworkTransportException ex = null;
			ITcpConnector tcpConnector = Dependencies.TcpConnector;
			TcpClientChannel tcpClientChannel = tcpConnector.TryConnect(networkPath, timeoutInMsec, out ex);
			if (tcpClientChannel != null)
			{
				return tcpClientChannel;
			}
			if (!networkPath.NetworkChoiceIsMandatory)
			{
				NetworkManager.TraceError("Attempting alternate routes", new object[0]);
				List<NetworkPath> list = null;
				ExchangeNetworkMap map = NetworkManager.GetMap();
				if (map != null)
				{
					list = map.EnumeratePaths(networkPath.TargetNodeName, ignoreNodeDown);
					if (list != null)
					{
						NetworkPath networkPath2 = null;
						foreach (NetworkPath networkPath3 in list)
						{
							if (string.Equals(networkPath3.NetworkName, networkPath.NetworkName, DatabaseAvailabilityGroupNetwork.NameComparison))
							{
								networkPath2 = networkPath3;
								break;
							}
						}
						if (networkPath2 != null)
						{
							list.Remove(networkPath2);
						}
						DagNetConfig dagConfig = DagNetEnvironment.FetchNetConfig();
						foreach (NetworkPath networkPath4 in list)
						{
							networkPath4.Purpose = networkPath.Purpose;
							networkPath4.ApplyNetworkPolicy(dagConfig);
							tcpClientChannel = tcpConnector.TryConnect(networkPath, timeoutInMsec, out ex);
							if (tcpClientChannel != null)
							{
								actualPath = networkPath4;
								return tcpClientChannel;
							}
						}
					}
				}
			}
			throw ex;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0005EA90 File Offset: 0x0005CC90
		internal static ExchangeNetwork GetNetwork(string netName)
		{
			ExchangeNetworkMap map = NetworkManager.GetMap();
			if (map != null)
			{
				return map.GetNetwork(netName);
			}
			return null;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0005EAB0 File Offset: 0x0005CCB0
		internal static ExchangeNetwork LookupEndPoint(IPAddress ipAddr, out NetworkEndPoint endPoint)
		{
			endPoint = null;
			ExchangeNetworkMap map = NetworkManager.GetMap();
			if (map != null)
			{
				return map.LookupEndPoint(ipAddr, out endPoint);
			}
			return null;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0005EAD8 File Offset: 0x0005CCD8
		internal static ExchangeNetworkPerfmonCounters GetPerfCounters(string netName)
		{
			ExchangeNetwork network = NetworkManager.GetNetwork(netName);
			if (network != null)
			{
				return network.PerfCounters;
			}
			return null;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0005EAF8 File Offset: 0x0005CCF8
		internal static void Shutdown()
		{
			lock (NetworkManager.s_mgrLock)
			{
				NetworkManager.TraceDebug("Shutdown initiated.", new object[0]);
				NetworkManager.s_mgr.m_shutdown = true;
				NetworkManager.s_mgr.m_firstDriveMapRefreshCompleted.Close();
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0005EBD8 File Offset: 0x0005CDD8
		internal static DagNetworkConfiguration GetDagNetworkConfig()
		{
			DagNetworkConfiguration config = null;
			NetworkManager.RunRpcOperation("GetDagNetworkConfig", delegate(object param0, EventArgs param1)
			{
				ExchangeNetworkMap exchangeNetworkMap = NetworkManager.FetchInitializedMap();
				config = new DagNetworkConfiguration();
				config.NetworkCompression = exchangeNetworkMap.NetworkManager.NetworkCompression;
				config.NetworkEncryption = exchangeNetworkMap.NetworkManager.NetworkEncryption;
				config.ReplicationPort = exchangeNetworkMap.NetworkManager.ReplicationPort;
				config.Networks = exchangeNetworkMap.GetDagNets();
			});
			return config;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0005EE08 File Offset: 0x0005D008
		internal static void SetDagNetworkConfig(SetDagNetworkConfigRequest configChange)
		{
			NetworkManager.RunRpcOperation("SetDagNetworkConfig", delegate(object param0, EventArgs param1)
			{
				NetworkManager manager = NetworkManager.GetManager();
				if (manager == null)
				{
					throw new DagNetworkManagementException(ReplayStrings.NetworkManagerInitError);
				}
				lock (manager.m_mapRefreshLock)
				{
					using (IAmCluster amCluster = ClusterFactory.Instance.Open())
					{
						using (DagConfigurationStore dagConfigurationStore = new DagConfigurationStore())
						{
							dagConfigurationStore.Open();
							PersistentDagNetworkConfig persistentDagNetworkConfig = dagConfigurationStore.LoadNetworkConfig();
							if (persistentDagNetworkConfig == null)
							{
								persistentDagNetworkConfig = new PersistentDagNetworkConfig();
							}
							else
							{
								string text = persistentDagNetworkConfig.Serialize();
								ReplayEventLogConstants.Tuple_DagNetworkConfigOld.LogEvent(DateTime.UtcNow.ToString(), new object[]
								{
									text
								});
							}
							if (configChange.SetPort)
							{
								persistentDagNetworkConfig.ReplicationPort = configChange.ReplicationPort;
								manager.ReplicationPort = configChange.ReplicationPort;
							}
							manager.NetworkCompression = configChange.NetworkCompression;
							persistentDagNetworkConfig.NetworkCompression = configChange.NetworkCompression;
							manager.NetworkEncryption = configChange.NetworkEncryption;
							persistentDagNetworkConfig.NetworkEncryption = configChange.NetworkEncryption;
							manager.ManualDagNetworkConfiguration = configChange.ManualDagNetworkConfiguration;
							persistentDagNetworkConfig.ManualDagNetworkConfiguration = configChange.ManualDagNetworkConfiguration;
							if (configChange.DiscoverNetworks)
							{
								NetworkDiscovery networkDiscovery = new NetworkDiscovery();
								networkDiscovery.LoadClusterObjects(amCluster);
								networkDiscovery.DetermineDnsStatus();
								networkDiscovery.AggregateNetworks(true);
								ExchangeNetworkMap exchangeNetworkMap = new ExchangeNetworkMap(manager);
								exchangeNetworkMap.Load(networkDiscovery);
								persistentDagNetworkConfig = exchangeNetworkMap.BuildPersistentDagNetworkConfig();
							}
							manager.UpdateNetworkConfig(persistentDagNetworkConfig);
						}
					}
				}
			});
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0005EF18 File Offset: 0x0005D118
		internal static void SetDagNetwork(SetDagNetworkRequest changeReq)
		{
			NetworkManager.RunRpcOperation("SetDagNetwork", delegate(object param0, EventArgs param1)
			{
				NetworkManager manager = NetworkManager.GetManager();
				if (manager == null)
				{
					throw new DagNetworkManagementException(ReplayStrings.NetworkManagerInitError);
				}
				lock (manager.m_mapRefreshLock)
				{
					using (DagConfigurationStore dagConfigurationStore = new DagConfigurationStore())
					{
						dagConfigurationStore.Open();
						PersistentDagNetworkConfig persistentDagNetworkConfig = dagConfigurationStore.LoadNetworkConfig();
						if (persistentDagNetworkConfig != null)
						{
							string text = persistentDagNetworkConfig.Serialize();
							ReplayEventLogConstants.Tuple_DagNetworkConfigOld.LogEvent(DateTime.UtcNow.ToString(), new object[]
							{
								text
							});
						}
					}
					ExchangeNetworkMap exchangeNetworkMap = NetworkManager.FetchInitializedMap();
					PersistentDagNetworkConfig netConfig = exchangeNetworkMap.UpdateNetConfig(changeReq);
					manager.UpdateNetworkConfig(netConfig);
				}
			});
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0005F078 File Offset: 0x0005D278
		internal static void RemoveDagNetwork(RemoveDagNetworkRequest req)
		{
			NetworkManager.RunRpcOperation("RemoveDagNetwork", delegate(object param0, EventArgs param1)
			{
				NetworkManager manager = NetworkManager.GetManager();
				if (manager == null)
				{
					throw new DagNetworkManagementException(ReplayStrings.NetworkManagerInitError);
				}
				lock (manager.m_mapRefreshLock)
				{
					using (DagConfigurationStore dagConfigurationStore = new DagConfigurationStore())
					{
						dagConfigurationStore.Open();
						PersistentDagNetworkConfig persistentDagNetworkConfig = dagConfigurationStore.LoadNetworkConfig();
						if (persistentDagNetworkConfig != null)
						{
							string text = persistentDagNetworkConfig.Serialize();
							ReplayEventLogConstants.Tuple_DagNetworkConfigOld.LogEvent(DateTime.UtcNow.ToString(), new object[]
							{
								text
							});
						}
					}
					ExchangeNetworkMap exchangeNetworkMap = NetworkManager.FetchInitializedMap();
					PersistentDagNetworkConfig persistentDagNetworkConfig2 = exchangeNetworkMap.BuildPersistentDagNetworkConfig();
					if (!persistentDagNetworkConfig2.RemoveNetwork(req.Name))
					{
						NetworkManager.TraceError("RemoveDagNetwork {0} not found", new object[]
						{
							req.Name
						});
						throw new DagNetworkManagementException(ReplayStrings.NetworkNameNotFound(req.Name));
					}
					manager.UpdateNetworkConfig(persistentDagNetworkConfig2);
				}
			});
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0005F0A8 File Offset: 0x0005D2A8
		protected static void RunRpcOperation(string rpcName, EventHandler ev)
		{
			Exception ex = null;
			NetworkManager.TraceDebug("RunRpcOperation({0})", new object[]
			{
				rpcName
			});
			try
			{
				ev(null, null);
				return;
			}
			catch (ClusterNetworkDeletedException ex2)
			{
				ex = ex2;
			}
			catch (ClusterException ex3)
			{
				ex = ex3;
			}
			catch (DagNetworkManagementException ex4)
			{
				ex = ex4;
			}
			catch (COMException ex5)
			{
				ex = ex5;
			}
			catch (IOException ex6)
			{
				ex = ex6;
			}
			catch (UnauthorizedAccessException ex7)
			{
				ex = ex7;
			}
			catch (TransientException ex8)
			{
				ex = ex8;
			}
			catch (Win32Exception ex9)
			{
				ex = ex9;
			}
			if (ex != null)
			{
				NetworkManager.TraceError("RunRpcOperation({0}) hit exception {1}", new object[]
				{
					rpcName,
					ex
				});
				throw new DagNetworkRpcServerException(rpcName, ex.Message, ex);
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0005F1A0 File Offset: 0x0005D3A0
		private static ExchangeNetworkMap FetchInitializedMap()
		{
			ExchangeNetworkMap map = NetworkManager.GetMap();
			if (map == null)
			{
				NetworkManager.TraceError("NetworkMap has not yet been initialized. Sleeping for {0}ms", new object[]
				{
					NetworkManager.GetInitializationTimeoutInMsec()
				});
				Thread.Sleep(NetworkManager.GetInitializationTimeoutInMsec());
				map = NetworkManager.GetMap();
				if (map == null)
				{
					throw new DagNetworkManagementException(ReplayStrings.NetworkManagerInitError);
				}
			}
			return map;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0005F1F9 File Offset: 0x0005D3F9
		private static int GetInitializationTimeoutInMsec()
		{
			return 1000 * RegistryParameters.NetworkManagerStartupTimeoutInSec;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0005F208 File Offset: 0x0005D408
		private bool IsPAM()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (!config.IsPAM && !config.IsSAM)
			{
				NetworkManager.TraceDebug("NetworkManager startup skipped.  Not running in DAG role", new object[0]);
				return false;
			}
			return config.IsPAM;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0005F248 File Offset: 0x0005D448
		private void Initialize()
		{
			TcpPortFallback.LoadPortNumber(out this.m_replicationPort);
			this.m_threadClusterNotification = new Thread(new ThreadStart(this.ClusterNotificationThread));
			this.m_threadClusterNotification.Start();
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0005F278 File Offset: 0x0005D478
		private void EnumerateNetworkMap()
		{
			NetworkManager.TraceDebug("EnumerateNetworkMap: attempting to reload", new object[0]);
			using (IAmCluster amCluster = ClusterFactory.Instance.Open())
			{
				PersistentDagNetworkConfig persistentDagNetworkConfig = null;
				string text = null;
				Exception ex = null;
				using (DagConfigurationStore dagConfigurationStore = new DagConfigurationStore())
				{
					dagConfigurationStore.Open();
					persistentDagNetworkConfig = dagConfigurationStore.LoadNetworkConfig(out text);
					PersistentDagNetworkConfig persistentDagNetworkConfig2;
					if (persistentDagNetworkConfig == null)
					{
						persistentDagNetworkConfig2 = new PersistentDagNetworkConfig();
					}
					else
					{
						persistentDagNetworkConfig2 = persistentDagNetworkConfig.Copy();
					}
					IADDatabaseAvailabilityGroup localDag = Dependencies.ADConfig.GetLocalDag();
					if (localDag == null)
					{
						NetworkManager.TraceError("EnumerateNetworkMap can't get the DAG!", new object[0]);
					}
					else
					{
						if (persistentDagNetworkConfig2.NetworkCompression != localDag.NetworkCompression)
						{
							persistentDagNetworkConfig2.NetworkCompression = localDag.NetworkCompression;
						}
						if (persistentDagNetworkConfig2.NetworkEncryption != localDag.NetworkEncryption)
						{
							persistentDagNetworkConfig2.NetworkEncryption = localDag.NetworkEncryption;
						}
						if (persistentDagNetworkConfig2.ManualDagNetworkConfiguration != localDag.ManualDagNetworkConfiguration)
						{
							persistentDagNetworkConfig2.ManualDagNetworkConfiguration = localDag.ManualDagNetworkConfiguration;
						}
					}
					this.NetworkCompression = persistentDagNetworkConfig2.NetworkCompression;
					this.NetworkEncryption = persistentDagNetworkConfig2.NetworkEncryption;
					this.ReplicationPort = persistentDagNetworkConfig2.ReplicationPort;
					this.ManualDagNetworkConfiguration = persistentDagNetworkConfig2.ManualDagNetworkConfiguration;
					if (this.m_portInLocalRegistry != this.ReplicationPort && TcpPortFallback.StorePortNumber(this.ReplicationPort))
					{
						this.m_portInLocalRegistry = this.ReplicationPort;
					}
					NetworkDiscovery networkDiscovery = new NetworkDiscovery();
					networkDiscovery.LoadClusterObjects(amCluster);
					if (this.ManualDagNetworkConfiguration)
					{
						networkDiscovery.LoadExistingConfiguration(persistentDagNetworkConfig2);
					}
					networkDiscovery.DetermineDnsStatus();
					networkDiscovery.AggregateNetworks(true);
					if (!this.ManualDagNetworkConfiguration)
					{
						networkDiscovery.RemoveEmptyNets();
					}
					ExchangeNetworkMap exchangeNetworkMap = new ExchangeNetworkMap(this);
					exchangeNetworkMap.Load(networkDiscovery);
					AmConfig config = AmSystemManager.Instance.Config;
					if (config.IsPAM)
					{
						try
						{
							exchangeNetworkMap.SynchronizeClusterNetworkRoles(amCluster);
						}
						catch (ClusCommonFailException ex2)
						{
							NetworkManager.TraceError("SynchronizeClusterNetworkRoles threw: {0}", new object[]
							{
								ex2
							});
							ex = ex2;
						}
					}
					exchangeNetworkMap.SetupPerfmon();
					persistentDagNetworkConfig2 = exchangeNetworkMap.BuildPersistentDagNetworkConfig();
					string text2 = persistentDagNetworkConfig2.Serialize();
					bool flag = false;
					if (config.IsPAM)
					{
						if (persistentDagNetworkConfig == null || text != text2)
						{
							flag = true;
							Interlocked.Exchange(ref this.m_skipNextClusterRegistryEvent, 1);
							dagConfigurationStore.StoreNetworkConfig(text2);
							if (persistentDagNetworkConfig != null)
							{
								ReplayEventLogConstants.Tuple_DagNetworkConfigOld.LogEvent("DAGNET", new object[]
								{
									text
								});
							}
						}
					}
					else if (this.m_lastWrittenClusterNetConfigXML != null && this.m_lastWrittenClusterNetConfigXML != text2)
					{
						flag = true;
					}
					if (flag)
					{
						ReplayEventLogConstants.Tuple_DagNetworkConfigNew.LogEvent("DAGNET", new object[]
						{
							text2
						});
					}
					this.m_lastWrittenClusterNetConfigXML = text2;
					DagNetConfig dagNetConfig = this.Convert2DagNetConfig(networkDiscovery);
					string text3 = dagNetConfig.Serialize();
					if (this.m_lastWrittenEseReplNetConfigXML == null || this.EseReplDagNetConfigIsStale || text3 != this.m_lastWrittenEseReplNetConfigXML)
					{
						DagNetEnvironment.PublishDagNetConfig(text3);
						this.EseReplDagNetConfigIsStale = false;
					}
					this.m_lastWrittenEseReplNetConfigXML = text3;
					this.m_mapLoadTime = ExDateTime.Now;
					this.m_netMap = exchangeNetworkMap;
					NetworkManager.TraceDebug("EnumerateNetworkMap: completed reload", new object[0]);
					AmSystemManager instance = AmSystemManager.Instance;
					if (instance != null)
					{
						AmNetworkMonitor networkMonitor = instance.NetworkMonitor;
						if (networkMonitor != null)
						{
							networkMonitor.RefreshMapiNetwork();
						}
					}
					if (ex != null)
					{
						throw ex;
					}
				}
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0005F5DC File Offset: 0x0005D7DC
		private DagNetConfig Convert2DagNetConfig(NetworkDiscovery map)
		{
			DagNetConfig dagNetConfig = new DagNetConfig();
			dagNetConfig.ReplicationPort = (int)this.ReplicationPort;
			dagNetConfig.NetworkCompression = NetworkManager.MapESEReplNetworkOption(this.NetworkCompression);
			dagNetConfig.NetworkEncryption = NetworkManager.MapESEReplNetworkOption(this.NetworkEncryption);
			foreach (LogicalNetwork logicalNetwork in map.LogicalNetworks)
			{
				DagNetwork dagNetwork = new DagNetwork();
				dagNetwork.Name = logicalNetwork.Name;
				dagNetwork.Description = logicalNetwork.Description;
				dagNetwork.ReplicationEnabled = logicalNetwork.ReplicationEnabled;
				dagNetwork.IsDnsMapped = logicalNetwork.HasDnsNic();
				foreach (Subnet subnet in logicalNetwork.Subnets)
				{
					dagNetwork.Subnets.Add(subnet.SubnetId.ToString());
				}
				dagNetConfig.Networks.Add(dagNetwork);
			}
			foreach (ClusterNode clusterNode in map.Nodes)
			{
				DagNode dagNode = new DagNode();
				dagNode.Name = clusterNode.Name.NetbiosName;
				foreach (ClusterNic clusterNic in clusterNode.Nics)
				{
					if (clusterNic.IPAddress != null)
					{
						DagNode.Nic nic = new DagNode.Nic();
						nic.IpAddress = clusterNic.IPAddress.ToString();
						nic.NetworkName = clusterNic.ClusterNetwork.LogicalNetwork.Name;
						dagNode.Nics.Add(nic);
					}
				}
				dagNetConfig.Nodes.Add(dagNode);
			}
			return dagNetConfig;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0005F7E8 File Offset: 0x0005D9E8
		private void UpdateNetworkConfig(PersistentDagNetworkConfig netConfig)
		{
			using (DagConfigurationStore dagConfigurationStore = new DagConfigurationStore())
			{
				dagConfigurationStore.Open();
				Interlocked.Exchange(ref this.m_skipNextClusterRegistryEvent, 1);
				string text = dagConfigurationStore.StoreNetworkConfig(netConfig);
				this.m_lastWrittenClusterNetConfigXML = text;
				ReplayEventLogConstants.Tuple_DagNetworkConfigNew.LogEvent(DateTime.UtcNow.ToString(), new object[]
				{
					text
				});
				this.EseReplDagNetConfigIsStale = true;
				this.DriveMapRefresh();
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0005F870 File Offset: 0x0005DA70
		private void DriveMapRefresh()
		{
			try
			{
				this.DriveMapRefreshInternal();
			}
			finally
			{
				this.m_firstDriveMapRefreshCompleted.Set();
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0005F8A4 File Offset: 0x0005DAA4
		private void DriveMapRefreshInternal()
		{
			lock (this.m_mapRefreshLock)
			{
				bool flag2 = false;
				Exception ex = null;
				int num = 4;
				for (int i = 1; i <= num; i++)
				{
					try
					{
						this.EnumerateNetworkMap();
						flag2 = true;
						break;
					}
					catch (ClusterNetworkDeletedException ex2)
					{
						ex = ex2;
					}
					catch (ClusterException ex3)
					{
						ex = ex3;
					}
					if (i < num)
					{
						NetworkManager.TraceError("DriveMapRefresh hit an exception during EnumerateNetworkMap, sleeping for 1 second and re-trying: {0}", new object[]
						{
							ex
						});
						Thread.Sleep(1000);
					}
				}
				if (!flag2)
				{
					throw ex;
				}
				if (this.m_netMap != null)
				{
					ExchangeNetwork exchangeNetwork = null;
					foreach (KeyValuePair<string, ExchangeNetwork> keyValuePair in this.m_netMap.Networks)
					{
						ExchangeNetwork value = keyValuePair.Value;
						if (value.ReplicationEnabled)
						{
							exchangeNetwork = value;
							break;
						}
					}
					if (exchangeNetwork == null)
					{
						ReplayEventLogConstants.Tuple_NetworkReplicationDisabled.LogEvent("AllNetsDisabled", new object[0]);
					}
				}
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0005F9CC File Offset: 0x0005DBCC
		private bool TryDriveMapRefresh()
		{
			Exception ex = null;
			try
			{
				this.DriveMapRefresh();
			}
			catch (ClusterException ex2)
			{
				ex = ex2;
			}
			catch (DataSourceTransientException ex3)
			{
				ex = ex3;
			}
			catch (DataSourceOperationException ex4)
			{
				ex = ex4;
			}
			catch (TransientException ex5)
			{
				ex = ex5;
			}
			catch (Win32Exception ex6)
			{
				ex = ex6;
			}
			catch (SerializationException ex7)
			{
				ex = ex7;
				ReplayCrimsonEvents.GeneralSerializationError.LogPeriodic<string>("NetworkManager", DiagCore.DefaultEventSuppressionInterval, ex7.ToString());
			}
			if (ex != null)
			{
				NetworkManager.TraceError("TryDriveMapRefresh hit exception: {0}", new object[]
				{
					ex
				});
				ReplayEventLogConstants.Tuple_NetworkMonitoringError.LogEvent(ex.Message.GetHashCode().ToString(), new object[]
				{
					ex.ToString()
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0005FAC0 File Offset: 0x0005DCC0
		private void ClusterNotificationThread()
		{
			if (this.RefreshClusterHandles())
			{
				this.TryDriveMapRefresh();
			}
			while (!this.m_shutdown)
			{
				Exception ex = null;
				try
				{
					AmConfig config = AmSystemManager.Instance.Config;
					if (!config.IsPAM && !config.IsSAM)
					{
						NetworkManager.TraceDebug("NetworkManager sleeping.  Not running in DAG role", new object[0]);
						Thread.Sleep(NetworkManager.GetInitializationTimeoutInMsec());
					}
					else if (this.RefreshClusterHandles())
					{
						this.MonitorEvents();
					}
					else
					{
						Thread.Sleep(NetworkManager.GetInitializationTimeoutInMsec());
					}
				}
				catch (ClusterException ex2)
				{
					ex = ex2;
				}
				catch (DataSourceTransientException ex3)
				{
					ex = ex3;
				}
				catch (DataSourceOperationException ex4)
				{
					ex = ex4;
				}
				catch (TransientException ex5)
				{
					ex = ex5;
				}
				catch (COMException ex6)
				{
					ex = ex6;
				}
				catch (Win32Exception ex7)
				{
					ex = ex7;
				}
				if (ex != null)
				{
					NetworkManager.TraceError("ClusterNotificationThread monitoring encountered an exception: {0}", new object[]
					{
						ex
					});
					ReplayEventLogConstants.Tuple_NetworkMonitoringError.LogEvent(ex.Message.GetHashCode().ToString(), new object[]
					{
						ex.ToString()
					});
				}
				if (!this.m_shutdown)
				{
					Thread.Sleep(1000);
				}
			}
			NetworkManager.TraceDebug("ClusterNotificationThread exiting", new object[0]);
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0005FC28 File Offset: 0x0005DE28
		private bool TryWaitForInitialization()
		{
			TimeSpan timeout = TimeSpan.FromSeconds((double)RegistryParameters.NetworkManagerStartupTimeoutInSec);
			return this.m_firstDriveMapRefreshCompleted.WaitOne(timeout) == ManualOneShotEvent.Result.Success;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0005FC50 File Offset: 0x0005DE50
		private void CloseClusterHandles()
		{
			this.m_clusterHandlesAreValid = false;
			if (this.m_hCluster != null && !this.m_hCluster.IsInvalid)
			{
				this.m_hCluster.Dispose();
				this.m_hCluster = null;
			}
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0005FC80 File Offset: 0x0005DE80
		private bool RefreshClusterHandles()
		{
			this.CloseClusterHandles();
			Exception ex = null;
			try
			{
				this.m_hCluster = ClusapiMethods.OpenCluster(null);
				if (this.m_hCluster == null || this.m_hCluster.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					NetworkManager.TraceError("OpenCluster() failed with error {0}.", new object[]
					{
						lastWin32Error
					});
					return false;
				}
				this.m_clusterHandlesAreValid = true;
				return true;
			}
			catch (ClusterException ex2)
			{
				ex = ex2;
			}
			catch (TransientException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				NetworkManager.TraceError("RefreshClusterHandles() failed with error {0}.", new object[]
				{
					ex
				});
			}
			return false;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0005FD34 File Offset: 0x0005DF34
		private AmClusterRegkeyHandle GetClusdbKeyHandle(IDistributedStoreKey key)
		{
			DistributedStoreKey distributedStoreKey = key as DistributedStoreKey;
			ClusterDbKey clusterDbKey;
			if (distributedStoreKey != null)
			{
				clusterDbKey = (distributedStoreKey.PrimaryStoreKey as ClusterDbKey);
			}
			else
			{
				clusterDbKey = (key as ClusterDbKey);
			}
			if (clusterDbKey != null)
			{
				return clusterDbKey.KeyHandle;
			}
			return null;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0005FD70 File Offset: 0x0005DF70
		private void RegisterForChangeNotification(IDistributedStoreKey dsKey, AmClusterNotifyHandle hChange)
		{
			AmClusterRegkeyHandle clusdbKeyHandle = this.GetClusdbKeyHandle(dsKey);
			if (clusdbKeyHandle != null && !clusdbKeyHandle.IsInvalid)
			{
				ClusterNotifyFlags dwFilter = ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_VALUE;
				IntPtr dwNotifyKey = (IntPtr)1;
				int num = ClusapiMethods.RegisterClusterNotify(hChange, dwFilter, clusdbKeyHandle, dwNotifyKey);
				if (num != 0)
				{
					NetworkManager.TraceError("RegisterClusterNotify for reg notification 0x{0:X8}", new object[]
					{
						num
					});
					throw AmExceptionHelper.ConstructClusterApiException(num, "RegisterClusterNotify(Network Registry)", new object[0]);
				}
			}
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0005FDD8 File Offset: 0x0005DFD8
		private void MonitorEvents()
		{
			AmClusterNotifyHandle amClusterNotifyHandle = null;
			IDistributedStoreKey distributedStoreKey = null;
			IDistributedStoreChangeNotify distributedStoreChangeNotify = null;
			try
			{
				ClusterNotifyFlags networkClusterNotificationMask = RegistryParameters.NetworkClusterNotificationMask;
				NetworkManager.TraceDebug("SettingClusterMask as 0x{0:x}", new object[]
				{
					networkClusterNotificationMask
				});
				amClusterNotifyHandle = ClusapiMethods.CreateClusterNotifyPort(AmClusterNotifyHandle.InvalidHandle, this.ClusterHandle, networkClusterNotificationMask, IntPtr.Zero);
				if (amClusterNotifyHandle == null || amClusterNotifyHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					NetworkManager.TraceError("CreateClusterNotifyPort failed. Error code 0x{0:X8}", new object[]
					{
						lastWin32Error
					});
					throw new ClusCommonTransientException("CreateClusterNotifyPort", new Win32Exception(lastWin32Error));
				}
				using (IDistributedStoreKey clusterKey = DistributedStore.Instance.GetClusterKey(this.ClusterHandle, null, null, DxStoreKeyAccessMode.Write, false))
				{
					distributedStoreKey = clusterKey.OpenKey("Exchange\\DagNetwork", DxStoreKeyAccessMode.CreateIfNotExist, false, null);
				}
				this.RegisterForChangeNotification(distributedStoreKey, amClusterNotifyHandle);
				TimeSpan t = new TimeSpan(0, 0, RegistryParameters.NetworkStatusPollingPeriodInSecs);
				while (this.m_clusterHandlesAreValid && !this.m_shutdown)
				{
					StringBuilder stringBuilder = new StringBuilder(256);
					uint num = Convert.ToUInt32(stringBuilder.Capacity);
					IntPtr zero = IntPtr.Zero;
					ClusterNotifyFlags clusterNotifyFlags;
					int clusterNotify = ClusapiMethods.GetClusterNotify(amClusterNotifyHandle, out zero, out clusterNotifyFlags, stringBuilder, ref num, 3000U);
					if (this.m_shutdown)
					{
						break;
					}
					if (this.m_netMap == null)
					{
						if (!this.TryDriveMapRefresh())
						{
							break;
						}
					}
					else if (clusterNotify == 258)
					{
						if (t < ExDateTime.TimeDiff(ExDateTime.Now, this.m_mapLoadTime) && !this.TryDriveMapRefresh())
						{
							break;
						}
					}
					else if (clusterNotify != 0)
					{
						NetworkManager.TraceDebug("GetClusterNotify() returned unexpected status code 0x{0:X)", new object[]
						{
							clusterNotify
						});
					}
					else
					{
						string text = stringBuilder.ToString();
						NetworkManager.TraceDebug("GetClusterNotify() returned notifyKey={0}, filterType=0x{1:x}, resName={2}", new object[]
						{
							zero,
							clusterNotifyFlags,
							text
						});
						if ((clusterNotifyFlags & ~(ClusterNotifyFlags.CLUSTER_CHANGE_NODE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_NAME | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_ATTRIBUTES | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_VALUE | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_SUBTREE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_RECONNECT | ClusterNotifyFlags.CLUSTER_CHANGE_QUORUM_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_PROPERTY)) != ~(ClusterNotifyFlags.CLUSTER_CHANGE_NODE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_NAME | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_ATTRIBUTES | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_VALUE | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_SUBTREE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_RECONNECT | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_QUORUM_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_HANDLE_CLOSE) && !this.TryDriveMapRefresh())
						{
							break;
						}
					}
				}
			}
			finally
			{
				if (amClusterNotifyHandle != null)
				{
					amClusterNotifyHandle.Dispose();
					amClusterNotifyHandle = null;
				}
				if (distributedStoreChangeNotify != null)
				{
					distributedStoreChangeNotify.Dispose();
				}
				if (distributedStoreKey != null)
				{
					distributedStoreKey.Dispose();
				}
			}
		}

		// Token: 0x04000907 RID: 2311
		private const int SleepTransientException = 1000;

		// Token: 0x04000908 RID: 2312
		private const uint ClusterNotifyTimeoutMilliseconds = 3000U;

		// Token: 0x04000909 RID: 2313
		private const string FirstDriveMapRefreshCompletedEventName = "FirstDriveMapRefreshCompletedEvent";

		// Token: 0x0400090A RID: 2314
		private static object s_mgrLock = new object();

		// Token: 0x0400090B RID: 2315
		private static NetworkManager s_mgr = new NetworkManager();

		// Token: 0x0400090C RID: 2316
		private static bool s_initialized = false;

		// Token: 0x0400090D RID: 2317
		private ManualOneShotEvent m_firstDriveMapRefreshCompleted = new ManualOneShotEvent("FirstDriveMapRefreshCompletedEvent");

		// Token: 0x0400090E RID: 2318
		private ExchangeNetworkMap m_netMap;

		// Token: 0x0400090F RID: 2319
		private object m_mapRefreshLock = new object();

		// Token: 0x04000910 RID: 2320
		private ExDateTime m_mapLoadTime;

		// Token: 0x04000911 RID: 2321
		private AmClusterHandle m_hCluster;

		// Token: 0x04000912 RID: 2322
		private bool m_clusterHandlesAreValid;

		// Token: 0x04000913 RID: 2323
		private bool m_shutdown;

		// Token: 0x04000914 RID: 2324
		private Thread m_threadClusterNotification;

		// Token: 0x04000915 RID: 2325
		private int m_skipNextClusterRegistryEvent;

		// Token: 0x04000916 RID: 2326
		private ushort m_replicationPort = 64327;

		// Token: 0x04000917 RID: 2327
		private ushort m_portInLocalRegistry;

		// Token: 0x04000918 RID: 2328
		private DatabaseAvailabilityGroup.NetworkOption m_networkCompression = DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly;

		// Token: 0x04000919 RID: 2329
		private DatabaseAvailabilityGroup.NetworkOption m_networkEncryption = DatabaseAvailabilityGroup.NetworkOption.InterSubnetOnly;

		// Token: 0x0400091A RID: 2330
		private string m_lastWrittenClusterNetConfigXML;

		// Token: 0x0400091B RID: 2331
		private string m_lastWrittenEseReplNetConfigXML;
	}
}
