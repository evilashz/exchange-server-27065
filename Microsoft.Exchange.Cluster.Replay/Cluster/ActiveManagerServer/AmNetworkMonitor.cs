using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000059 RID: 89
	internal class AmNetworkMonitor
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00014E9E File Offset: 0x0001309E
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AmNetworkMonitorTracer;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00014EA5 File Offset: 0x000130A5
		public void UseCluster(IAmCluster cluster)
		{
			this.m_sharedCluster = cluster;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00014EAE File Offset: 0x000130AE
		private IAmCluster GetCluster()
		{
			return this.m_sharedCluster;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00014EB6 File Offset: 0x000130B6
		private void TriggerClusterRefresh(string reason)
		{
			AmNetworkMonitor.Tracer.TraceError<string>(0L, "TriggerClusterRefresh because {0}. Not yet implemented", reason);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00014ECC File Offset: 0x000130CC
		public void RefreshMapiNetwork()
		{
			Exception ex = null;
			try
			{
				Dictionary<AmServerName, AmNetworkMonitor.Node> dictionary = new Dictionary<AmServerName, AmNetworkMonitor.Node>();
				Dictionary<string, AmNetworkMonitor.Nic> dictionary2 = new Dictionary<string, AmNetworkMonitor.Nic>();
				using (IAmCluster amCluster = ClusterFactory.Instance.Open())
				{
					lock (this)
					{
						ExTraceGlobals.AmNetworkMonitorTracer.TraceDebug((long)this.GetHashCode(), "RefreshMapiNetwork running");
						foreach (IAmClusterNode amClusterNode in amCluster.EnumerateNodes())
						{
							using (amClusterNode)
							{
								AmNetworkMonitor.Node node = new AmNetworkMonitor.Node(amClusterNode.Name);
								dictionary.Add(node.Name, node);
								IPAddress[] dnsAddresses = NetworkUtil.GetDnsAddresses(amClusterNode.Name.Fqdn, ref ex);
								foreach (AmClusterNetInterface amClusterNetInterface in amClusterNode.EnumerateNetInterfaces())
								{
									using (amClusterNetInterface)
									{
										bool flag2 = false;
										IPAddress ipaddress = NetworkUtil.ConvertStringToIpAddress(amClusterNetInterface.GetAddress());
										if (ipaddress != null && NetworkUtil.IsAddressPresent(dnsAddresses, ipaddress))
										{
											flag2 = true;
											AmNetworkMonitor.Tracer.TraceDebug<string, IPAddress>((long)this.GetHashCode(), "NIC '{0}' on DNS at {1}", amClusterNetInterface.Name, ipaddress);
										}
										if (!flag2)
										{
											string[] ipv6Addresses = amClusterNetInterface.GetIPv6Addresses();
											if (ipv6Addresses != null && ipv6Addresses.Length > 0)
											{
												foreach (string text in ipv6Addresses)
												{
													ipaddress = NetworkUtil.ConvertStringToIpAddress(text);
													if (ipaddress != null && NetworkUtil.IsAddressPresent(dnsAddresses, ipaddress))
													{
														flag2 = true;
														AmNetworkMonitor.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "NIC '{0}' on DNS at {1}", amClusterNetInterface.Name, text);
														break;
													}
												}
											}
										}
										if (flag2)
										{
											AmNetworkMonitor.Nic nic = new AmNetworkMonitor.Nic(amClusterNetInterface.Name, node);
											node.MapiNics.Add(nic);
											dictionary2.Add(nic.Name, nic);
										}
									}
								}
							}
						}
						this.m_nodeTable = dictionary;
						this.m_nicTable = dictionary2;
					}
				}
			}
			catch (ClusterException ex2)
			{
				ex = ex2;
			}
			finally
			{
				this.m_firstRefreshCompleted.Set();
				this.m_firstRefreshCompleted.Close();
			}
			if (ex != null)
			{
				AmNetworkMonitor.Tracer.TraceError<Exception>(0L, "RefreshMapiNetwork fails:{0}", ex);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000151E0 File Offset: 0x000133E0
		public bool TryWaitForInitialization()
		{
			TimeSpan timeout = TimeSpan.FromSeconds((double)RegistryParameters.NetworkManagerStartupTimeoutInSec);
			return this.m_firstRefreshCompleted.WaitOne(timeout) == ManualOneShotEvent.Result.Success;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00015208 File Offset: 0x00013408
		public bool IsNodePubliclyUp(AmServerName nodeName)
		{
			this.TryWaitForInitialization();
			Dictionary<AmServerName, AmNetworkMonitor.Node> nodeTable = this.m_nodeTable;
			if (nodeTable == null || this.GetCluster() == null)
			{
				AmNetworkMonitor.Tracer.TraceError<string>(0L, "Not yet initialized. Assuming {0} is Down", nodeName.NetbiosName);
				return false;
			}
			AmNetworkMonitor.Node node;
			if (!nodeTable.TryGetValue(nodeName, out node))
			{
				AmNetworkMonitor.Tracer.TraceError<string>((long)nodeTable.GetHashCode(), "{0} is not known. Assuming Down", nodeName.NetbiosName);
				return false;
			}
			return this.IsNodePubliclyUp(node);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00015278 File Offset: 0x00013478
		private bool IsNodePubliclyUp(AmNetworkMonitor.Node node)
		{
			IAmCluster cluster = this.GetCluster();
			if (cluster == null)
			{
				AmNetworkMonitor.Tracer.TraceError<AmServerName>(0L, "If cluster object is not valid, then assume node {0} is up", node.Name);
				return true;
			}
			Exception ex;
			AmNodeState nodeState = cluster.GetNodeState(node.Name, out ex);
			if (ex != null)
			{
				return false;
			}
			if (!AmClusterNode.IsNodeUp(nodeState))
			{
				return false;
			}
			AmClusterNodeNetworkStatus amClusterNodeNetworkStatus = AmClusterNodeStatusAccessor.Read(cluster, node.Name, out ex);
			return amClusterNodeNetworkStatus == null || amClusterNodeNetworkStatus.HasADAccess;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000152E4 File Offset: 0x000134E4
		public List<AmServerName> GetServersReportedAsPubliclyUp()
		{
			List<AmServerName> list = new List<AmServerName>();
			Dictionary<AmServerName, AmNetworkMonitor.Node> nodeTable = this.m_nodeTable;
			if (nodeTable != null)
			{
				foreach (KeyValuePair<AmServerName, AmNetworkMonitor.Node> keyValuePair in nodeTable)
				{
					AmNetworkMonitor.Node value = keyValuePair.Value;
					if (this.IsNodePubliclyUp(value))
					{
						list.Add(value.Name);
					}
				}
			}
			return list;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001535C File Offset: 0x0001355C
		public bool AreAnyMapiNicsUp(AmServerName nodeName)
		{
			Dictionary<AmServerName, AmNetworkMonitor.Node> nodeTable = this.m_nodeTable;
			if (nodeTable == null || this.GetCluster() == null)
			{
				AmNetworkMonitor.Tracer.TraceError<string>(0L, "Not yet initialized. Assuming {0} is Up", nodeName.NetbiosName);
				return true;
			}
			AmNetworkMonitor.Node node;
			if (!nodeTable.TryGetValue(nodeName, out node))
			{
				AmNetworkMonitor.Tracer.TraceError<string>((long)nodeTable.GetHashCode(), "{0} is not known. Assuming Up", nodeName.NetbiosName);
				return true;
			}
			return this.AreAnyMapiNicsUp(node);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000153C4 File Offset: 0x000135C4
		private bool AreAnyMapiNicsUp(AmNetworkMonitor.Node node)
		{
			int num = 0;
			foreach (AmNetworkMonitor.Nic nic in node.MapiNics)
			{
				num++;
				AmNetInterfaceState nicState = this.GetNicState(nic.Name);
				switch (nicState)
				{
				case AmNetInterfaceState.Unavailable:
				case AmNetInterfaceState.Failed:
				case AmNetInterfaceState.Unreachable:
					AmNetworkMonitor.Tracer.TraceError<string, AmNetInterfaceState>(0L, "Nic '{0}' is {1}.", nic.Name, nicState);
					break;
				case AmNetInterfaceState.Up:
					return true;
				default:
					AmNetworkMonitor.Tracer.TraceError<string, AmNetInterfaceState>(0L, "Nic '{0}' is {1}.", nic.Name, nicState);
					return true;
				}
			}
			AmNetworkMonitor.Tracer.TraceError<AmServerName, int>(0L, "Node {0} has {1} MAPI nics. None appear usable.", node.Name, num);
			return false;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00015498 File Offset: 0x00013698
		private AmNetInterfaceState GetNicState(string nicName)
		{
			AmNetInterfaceState result = AmNetInterfaceState.Unknown;
			IAmCluster cluster = this.GetCluster();
			if (cluster != null)
			{
				Exception ex;
				result = cluster.GetNetInterfaceState(nicName, out ex);
				if (ex != null)
				{
					AmNetworkMonitor.Tracer.TraceError<string, Exception>(0L, "Failed to get state for nic '{0}': {1}", nicName, ex);
					this.TriggerClusterRefresh("GetNicState failed");
				}
			}
			return result;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000154DD File Offset: 0x000136DD
		public void ProcessEvent(AmClusterEventInfo cei)
		{
			if (cei.IsNetInterfaceStateChanged)
			{
				this.ProcessNicEvent(cei);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000154F0 File Offset: 0x000136F0
		private void ProcessNicEvent(AmClusterEventInfo cei)
		{
			Dictionary<string, AmNetworkMonitor.Nic> nicTable = this.m_nicTable;
			IAmCluster cluster = this.GetCluster();
			if (nicTable == null || cluster == null)
			{
				AmNetworkMonitor.Tracer.TraceError(0L, "Not yet initialized. Ignoring event");
				return;
			}
			AmNetworkMonitor.Nic nic;
			if (!nicTable.TryGetValue(cei.ObjectName, out nic))
			{
				this.TriggerClusterRefresh("nic not found");
				return;
			}
			AmNetInterfaceState nicState = this.GetNicState(cei.ObjectName);
			switch (nicState)
			{
			case AmNetInterfaceState.Unavailable:
				AmNetworkMonitor.Tracer.TraceError<string, AmNetInterfaceState>(0L, "MAPI NIC '{0}' is {1}.", cei.ObjectName, nicState);
				return;
			case AmNetInterfaceState.Failed:
			case AmNetInterfaceState.Unreachable:
			{
				AmNetworkMonitor.Tracer.TraceError<string, AmNetInterfaceState>(0L, "MAPI NIC '{0}' is {1}.", cei.ObjectName, nicState);
				AmEvtMapiNetworkFailure amEvtMapiNetworkFailure = new AmEvtMapiNetworkFailure(nic.Node.Name);
				amEvtMapiNetworkFailure.Notify(true);
				return;
			}
			case AmNetInterfaceState.Up:
				AmNetworkMonitor.Tracer.TraceDebug<string>(0L, "MAPI NIC '{0}' is Up.", cei.ObjectName);
				if (nic.Node.Name.IsLocalComputerName)
				{
					AmClusterNodeNetworkStatus amClusterNodeNetworkStatus = new AmClusterNodeNetworkStatus();
					Exception ex = AmClusterNodeStatusAccessor.Write(cluster, nic.Node.Name, amClusterNodeNetworkStatus);
					if (ex != null)
					{
						ReplayCrimsonEvents.AmNodeStatusUpdateFailed.Log<string, string>(amClusterNodeNetworkStatus.ToString(), ex.Message);
						return;
					}
				}
				break;
			default:
				AmNetworkMonitor.Tracer.TraceError<AmNetInterfaceState, string>(0L, "Unexpected NIC state {0} for {1}", nicState, cei.ObjectName);
				break;
			}
		}

		// Token: 0x040001C1 RID: 449
		private const string FirstRefreshCompletedEventName = "FirstRefreshCompletedEvent";

		// Token: 0x040001C2 RID: 450
		private Dictionary<AmServerName, AmNetworkMonitor.Node> m_nodeTable;

		// Token: 0x040001C3 RID: 451
		private Dictionary<string, AmNetworkMonitor.Nic> m_nicTable;

		// Token: 0x040001C4 RID: 452
		private IAmCluster m_sharedCluster;

		// Token: 0x040001C5 RID: 453
		private ManualOneShotEvent m_firstRefreshCompleted = new ManualOneShotEvent("FirstRefreshCompletedEvent");

		// Token: 0x0200005A RID: 90
		private class Node
		{
			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x060003EE RID: 1006 RVA: 0x00015635 File Offset: 0x00013835
			// (set) Token: 0x060003EF RID: 1007 RVA: 0x0001563D File Offset: 0x0001383D
			public AmServerName Name { get; set; }

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00015646 File Offset: 0x00013846
			// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0001564E File Offset: 0x0001384E
			public List<AmNetworkMonitor.Nic> MapiNics { get; set; }

			// Token: 0x060003F2 RID: 1010 RVA: 0x00015657 File Offset: 0x00013857
			public Node(AmServerName name)
			{
				this.Name = name;
				this.MapiNics = new List<AmNetworkMonitor.Nic>();
			}
		}

		// Token: 0x0200005B RID: 91
		private class Nic
		{
			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00015671 File Offset: 0x00013871
			// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00015679 File Offset: 0x00013879
			public string Name { get; set; }

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00015682 File Offset: 0x00013882
			// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0001568A File Offset: 0x0001388A
			public AmNetworkMonitor.Node Node { get; set; }

			// Token: 0x060003F7 RID: 1015 RVA: 0x00015693 File Offset: 0x00013893
			public Nic(string name, AmNetworkMonitor.Node node)
			{
				this.Name = name;
				this.Node = node;
			}
		}
	}
}
