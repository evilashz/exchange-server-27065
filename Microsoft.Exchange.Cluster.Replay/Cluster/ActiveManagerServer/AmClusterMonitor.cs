using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000019 RID: 25
	internal class AmClusterMonitor
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00006539 File Offset: 0x00004739
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ClusterEventsTracer;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00006540 File Offset: 0x00004740
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00006548 File Offset: 0x00004748
		internal IAmCluster Cluster { get; private set; }

		// Token: 0x060000E2 RID: 226 RVA: 0x00006554 File Offset: 0x00004754
		public void ReportHasADAccess(bool hasAccess)
		{
			bool flag = false;
			AmClusterNodeNetworkStatus amClusterNodeNetworkStatus = new AmClusterNodeNetworkStatus();
			amClusterNodeNetworkStatus.HasADAccess = hasAccess;
			if (this.Cluster == null || this.m_cem == null)
			{
				AmClusterMonitor.Tracer.TraceError(0L, "ReportHasADAccess fails because we aren't initialized or running in a DAG");
				return;
			}
			try
			{
				using (AmClusterNodeStatusAccessor amClusterNodeStatusAccessor = new AmClusterNodeStatusAccessor(this.Cluster, AmServerName.LocalComputerName, DxStoreKeyAccessMode.CreateIfNotExist))
				{
					AmClusterNodeNetworkStatus amClusterNodeNetworkStatus2 = amClusterNodeStatusAccessor.Read();
					if (amClusterNodeNetworkStatus2 != null)
					{
						if (amClusterNodeNetworkStatus2.ClusterErrorOverride && amClusterNodeNetworkStatus.HasADAccess && !AmSystemManager.Instance.NetworkMonitor.AreAnyMapiNicsUp(AmServerName.LocalComputerName))
						{
							amClusterNodeNetworkStatus.ClusterErrorOverride = true;
						}
						if (!amClusterNodeNetworkStatus.IsEqual(amClusterNodeNetworkStatus2))
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						amClusterNodeStatusAccessor.Write(amClusterNodeNetworkStatus);
					}
				}
				if (flag)
				{
					if (amClusterNodeNetworkStatus.IsHealthy)
					{
						if (amClusterNodeNetworkStatus.ClusterErrorOverride)
						{
							ReplayCrimsonEvents.AmMapiAccessExpectedByAD.Log();
						}
						else
						{
							ReplayCrimsonEvents.AmADStatusRecordedAsAccessible.Log();
						}
					}
					else
					{
						ReplayCrimsonEvents.AmADStatusRecordedAsFailed.Log();
					}
				}
			}
			catch (ClusterException ex)
			{
				AmClusterMonitor.Tracer.TraceError<ClusterException>(0L, "ReportNodeState failed: {0}", ex);
				ReplayCrimsonEvents.AmNodeStatusUpdateFailed.Log<AmClusterNodeNetworkStatus, string>(amClusterNodeNetworkStatus, ex.Message);
			}
			catch (SerializationException ex2)
			{
				AmClusterMonitor.Tracer.TraceError<SerializationException>(0L, "ReportNodeState failed: {0}", ex2);
				ReplayCrimsonEvents.AmNodeStatusUpdateFailed.Log<AmClusterNodeNetworkStatus, string>(amClusterNodeNetworkStatus, ex2.ToString());
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000066B4 File Offset: 0x000048B4
		public void Start(IAmCluster cluster)
		{
			if (this.m_cem == null || !this.m_cem.IsMonitoring || !object.ReferenceEquals(this.Cluster, cluster))
			{
				AmClusterMonitor.Tracer.TraceDebug(0L, "Starting AmClusterEventManager");
				this.Cluster = cluster;
				AmSystemManager.Instance.NetworkMonitor.UseCluster(this.Cluster);
				AmSystemManager.Instance.NetworkMonitor.RefreshMapiNetwork();
				if (this.m_cem != null)
				{
					this.m_cem.Stop();
				}
				this.m_cem = new AmClusterEventManager(this.Cluster);
				this.m_cem.Start();
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006750 File Offset: 0x00004950
		public void Stop()
		{
			if (this.m_cem != null)
			{
				AmClusterMonitor.Tracer.TraceDebug(0L, "Stopping AmClusterEventManager");
				this.m_cem.Stop();
				this.m_cem = null;
				this.Cluster = null;
				if (AmSystemManager.Instance.NetworkMonitor != null)
				{
					AmSystemManager.Instance.NetworkMonitor.UseCluster(this.Cluster);
					return;
				}
			}
			else
			{
				AmClusterMonitor.Tracer.TraceDebug(0L, "Ignoring cluster event manager stop since it is not active");
			}
		}

		// Token: 0x0400005F RID: 95
		private AmClusterEventManager m_cem;
	}
}
