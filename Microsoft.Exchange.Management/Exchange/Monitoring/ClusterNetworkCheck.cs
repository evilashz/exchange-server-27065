﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200053F RID: 1343
	internal class ClusterNetworkCheck : DagMemberCheck
	{
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x000C2C9E File Offset: 0x000C0E9E
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.HealthChecksTracer;
			}
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000C2CA8 File Offset: 0x000C0EA8
		public ClusterNetworkCheck(string serverName, IEventManager eventManager, string momeventsource, IADDatabaseAvailabilityGroup dag) : base(serverName, "ClusterNetwork", CheckId.ClusterNetwork, Strings.ClusterNetworkCheckDesc, CheckCategory.SystemMediumPriority, eventManager, momeventsource, dag, true)
		{
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000C2CE8 File Offset: 0x000C0EE8
		public ClusterNetworkCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold, IADDatabaseAvailabilityGroup dag) : base(serverName, "ClusterNetwork", CheckId.ClusterNetwork, Strings.ClusterNetworkCheckDesc, CheckCategory.SystemMediumPriority, eventManager, momeventsource, ignoreTransientErrorsThreshold, dag, true)
		{
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000C2D2C File Offset: 0x000C0F2C
		protected override void RunCheck()
		{
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "CheckNetworksOnNode(): Checking the networks on node '{0}'.", base.ServerName);
			if (SharedHelper.StringIEquals(base.ServerName, Environment.MachineName) && !this.m_dag.ManualDagNetworkConfiguration && this.LoadNetworkDescription())
			{
				this.m_serverToCheck = this.m_networkConfig.FindNode(Environment.MachineName);
				if (this.m_serverToCheck == null)
				{
					this.m_errors.Add(Strings.DagNetworkEnumerationFailed(Strings.DagNodeNotFound(Environment.MachineName)));
				}
				else
				{
					this.CheckForSingleDnsNic();
					this.CheckForUniformNetworks();
				}
			}
			Exception ex = null;
			try
			{
				DagNetworkConfiguration dagNetworkConfig = DagNetworkRpc.GetDagNetworkConfig(base.ServerName);
				int num = 0;
				foreach (DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork in dagNetworkConfig.Networks)
				{
					if (!databaseAvailabilityGroupNetwork.IgnoreNetwork)
					{
						if (databaseAvailabilityGroupNetwork.ReplicationEnabled)
						{
							num++;
						}
						foreach (DatabaseAvailabilityGroupNetworkSubnet databaseAvailabilityGroupNetworkSubnet in databaseAvailabilityGroupNetwork.Subnets)
						{
							if (databaseAvailabilityGroupNetworkSubnet.State != DatabaseAvailabilityGroupNetworkSubnet.SubnetState.Up)
							{
								foreach (DatabaseAvailabilityGroupNetworkInterface databaseAvailabilityGroupNetworkInterface in databaseAvailabilityGroupNetwork.Interfaces)
								{
									if (!base.IsNodeStopped(new AmServerName(databaseAvailabilityGroupNetworkInterface.NodeName)))
									{
										this.m_errors.Add(Strings.DagSubnetDown(databaseAvailabilityGroupNetworkSubnet.SubnetId.ToString(), databaseAvailabilityGroupNetwork.Name, databaseAvailabilityGroupNetworkSubnet.State.ToString()));
									}
								}
							}
						}
						foreach (DatabaseAvailabilityGroupNetworkInterface databaseAvailabilityGroupNetworkInterface2 in databaseAvailabilityGroupNetwork.Interfaces)
						{
							if (databaseAvailabilityGroupNetworkInterface2.State != DatabaseAvailabilityGroupNetworkInterface.InterfaceState.Up && !base.IsNodeStopped(new AmServerName(databaseAvailabilityGroupNetworkInterface2.NodeName)))
							{
								this.m_errors.Add(Strings.DagNicDown(databaseAvailabilityGroupNetworkInterface2.NodeName, databaseAvailabilityGroupNetworkInterface2.IPAddress.ToString(), databaseAvailabilityGroupNetworkInterface2.State.ToString()));
							}
						}
					}
				}
				if (num == 0)
				{
					this.m_errors.Add(Strings.DagNetworkAllDisabledWarning);
				}
			}
			catch (DagNetworkManagementException ex2)
			{
				ex = ex2;
			}
			catch (HaRpcServerBaseException ex3)
			{
				ex = ex3;
			}
			catch (HaRpcServerTransientBaseException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				this.m_errors.Add(ServerStrings.DagNetworkManagementError(ex.Message));
			}
			if (this.m_errors.Count > 0)
			{
				string value = string.Join("\n", this.m_errors.ToArray());
				base.FailContinue(new LocalizedString(value));
			}
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000C3074 File Offset: 0x000C1274
		private bool LoadNetworkDescription()
		{
			Exception ex = null;
			try
			{
				this.m_networkConfig.LoadClusterObjects(base.Cluster);
				using (DagConfigurationStore dagConfigurationStore = new DagConfigurationStore())
				{
					dagConfigurationStore.Open();
					PersistentDagNetworkConfig persistentDagNetworkConfig = dagConfigurationStore.LoadNetworkConfig();
					if (persistentDagNetworkConfig == null)
					{
						persistentDagNetworkConfig = new PersistentDagNetworkConfig();
					}
					this.m_networkConfig.LoadExistingConfiguration(persistentDagNetworkConfig);
					this.m_networkConfig.DetermineDnsStatus();
					if (!this.m_networkConfig.AggregateNetworks(false))
					{
						return true;
					}
					this.m_errors.Add(Strings.DagNetworkSubnetAssignmentIncomplete);
				}
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
				ClusterNetworkCheck.Tracer.TraceError<Exception>((long)this.GetHashCode(), "LoadNetworkDescription hit exception {0}", ex);
				this.m_errors.Add(Strings.DagNetworkEnumerationFailed(ex.Message));
			}
			return false;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000C31E0 File Offset: 0x000C13E0
		private void CheckForSingleDnsNic()
		{
			int num = 0;
			foreach (ClusterNic clusterNic in this.m_serverToCheck.Nics)
			{
				if (clusterNic.IsDnsRegistered)
				{
					num++;
				}
			}
			if (num != 1)
			{
				this.m_errors.Add(Strings.DagNetworkSingleDNSNicViolation(base.ServerName));
			}
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000C3260 File Offset: 0x000C1460
		private void CheckForUniformNetworks()
		{
			foreach (LogicalNetwork logicalNetwork in this.m_networkConfig.LogicalNetworks)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
				foreach (Subnet subnet in logicalNetwork.Subnets)
				{
					if (subnet.ClusterNetwork != null)
					{
						foreach (ClusterNic clusterNic in subnet.ClusterNetwork.Nics)
						{
							int num;
							if (dictionary.TryGetValue(clusterNic.NodeName, out num))
							{
								this.m_errors.Add(Strings.DagNetworkHasMultiNicForNode(logicalNetwork.Name, clusterNic.NodeName));
								return;
							}
							dictionary.Add(clusterNic.NodeName, 1);
						}
					}
				}
				foreach (ClusterNode clusterNode in this.m_networkConfig.Nodes)
				{
					string netbiosName = clusterNode.Name.NetbiosName;
					int num2;
					if (!dictionary.TryGetValue(netbiosName, out num2))
					{
						this.m_errors.Add(Strings.DagNetworkHasNoNicForNode(logicalNetwork.Name, netbiosName));
						return;
					}
				}
			}
		}

		// Token: 0x0400223B RID: 8763
		private List<string> m_errors = new List<string>();

		// Token: 0x0400223C RID: 8764
		private NetworkDiscovery m_networkConfig = new NetworkDiscovery();

		// Token: 0x0400223D RID: 8765
		private ClusterNode m_serverToCheck;
	}
}
