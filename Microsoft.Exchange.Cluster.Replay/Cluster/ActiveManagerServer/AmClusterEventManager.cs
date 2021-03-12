using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000018 RID: 24
	internal class AmClusterEventManager : ChangePoller
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00006108 File Offset: 0x00004308
		internal AmClusterEventManager(IAmCluster cluster) : base(true)
		{
			this.Cluster = cluster;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00006132 File Offset: 0x00004332
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ClusterEventsTracer;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000613C File Offset: 0x0000433C
		internal bool IsMonitoring
		{
			get
			{
				bool isMonitoring;
				lock (this.m_locker)
				{
					isMonitoring = this.m_isMonitoring;
				}
				return isMonitoring;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00006180 File Offset: 0x00004380
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00006188 File Offset: 0x00004388
		private IAmCluster Cluster { get; set; }

		// Token: 0x060000DA RID: 218 RVA: 0x00006194 File Offset: 0x00004394
		public override void PrepareToStop()
		{
			AmClusterEventManager.Tracer.TraceDebug((long)this.GetHashCode(), "Stopping Cluster event manager");
			base.PrepareToStop();
			lock (this.m_locker)
			{
				if (this.m_cen != null)
				{
					this.m_cen.ForceClose();
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006200 File Offset: 0x00004400
		protected override void PollerThread()
		{
			AmTrace.Entering("ClusterEventManager.PollerThread", new object[0]);
			try
			{
				lock (this.m_locker)
				{
					if (!this.m_fShutdown)
					{
						this.CleanupClusterNotify();
						this.m_isMonitoring = this.InitializeClusterNotify();
						if (!this.m_isMonitoring)
						{
							AmTrace.Error("Failed to initialize cluster notification", new object[0]);
						}
					}
				}
				if (!this.m_fShutdown && this.m_isMonitoring)
				{
					this.MonitorClusterEvents();
				}
			}
			catch (ClusterException ex)
			{
				AmTrace.Error("MonitorForClusterEvents() got a cluster api exception: {0}", new object[]
				{
					ex
				});
			}
			catch (AmServerNameResolveFqdnException ex2)
			{
				AmTrace.Error("MonitorForClusterEvents() got a AmGetFqdnFailedADErrorException exception: {0}", new object[]
				{
					ex2
				});
			}
			lock (this.m_locker)
			{
				this.CleanupClusterNotify();
				this.m_isMonitoring = false;
			}
			if (!this.m_fShutdown)
			{
				AmClusterEventManager.Tracer.TraceDebug(0L, "Triggering refresh for error recovery");
				AmSystemManager.Instance.ConfigManager.TriggerRefresh(true);
			}
			AmClusterEventManager.Tracer.TraceDebug(0L, "Leaving ClusterEventManager.PollerThread");
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006358 File Offset: 0x00004558
		private void MonitorClusterEvents()
		{
			while (!this.m_fShutdown)
			{
				AmClusterEventInfo amClusterEventInfo;
				bool flag = this.m_cen.WaitForEvent(out amClusterEventInfo, this.DefaultClusterEventNotifierTimeout);
				if (this.m_fShutdown)
				{
					AmClusterEventManager.Tracer.TraceDebug((long)this.GetHashCode(), "MonitorClusterEvents: Detected shutdown flag is set. Exiting from cluster notification monitoring");
					return;
				}
				if (flag)
				{
					if (amClusterEventInfo.IsNotifyHandleClosed)
					{
						AmClusterEventManager.Tracer.TraceDebug((long)this.GetHashCode(), "Cluster notification handle closed. Exiting cluster event monitoring");
						return;
					}
					if (amClusterEventInfo.IsNodeStateChanged)
					{
						AmServerName nodeName = new AmServerName(amClusterEventInfo.ObjectName);
						if (!string.IsNullOrEmpty(amClusterEventInfo.ObjectName))
						{
							Exception ex;
							AmNodeState nodeState = this.Cluster.GetNodeState(nodeName, out ex);
							if (ex != null)
							{
								AmClusterEventManager.Tracer.TraceError<Exception>(0L, "MonitorClusterEvents fails to get node state: {0}", ex);
							}
							AmEvtNodeStateChanged amEvtNodeStateChanged = new AmEvtNodeStateChanged(nodeName, nodeState);
							amEvtNodeStateChanged.Notify();
							if (ex != null)
							{
								throw ex;
							}
						}
						else
						{
							AmTrace.Error("Node state change detected but node name is invalid", new object[0]);
						}
					}
					if (amClusterEventInfo.IsGroupStateChanged)
					{
						AmSystemManager.Instance.ConfigManager.TriggerRefresh(false);
					}
					if (amClusterEventInfo.IsClusterStateChanged)
					{
						AmEvtClusterStateChanged amEvtClusterStateChanged = new AmEvtClusterStateChanged();
						amEvtClusterStateChanged.Notify();
						AmSystemManager.Instance.ConfigManager.TriggerRefresh(true);
						return;
					}
					if (amClusterEventInfo.IsNodeAdded)
					{
						AmEvtNodeAdded amEvtNodeAdded = new AmEvtNodeAdded(new AmServerName(amClusterEventInfo.ObjectName));
						amEvtNodeAdded.Notify();
					}
					if (amClusterEventInfo.IsNodeRemoved)
					{
						AmEvtNodeRemoved amEvtNodeRemoved = new AmEvtNodeRemoved(new AmServerName(amClusterEventInfo.ObjectName));
						amEvtNodeRemoved.Notify();
						AmServerNameCache.Instance.RemoveEntry(amClusterEventInfo.ObjectName);
					}
					AmSystemManager.Instance.NetworkMonitor.ProcessEvent(amClusterEventInfo);
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000064E0 File Offset: 0x000046E0
		private bool InitializeClusterNotify()
		{
			ClusterNotifyFlags eventMask = ~(ClusterNotifyFlags.CLUSTER_CHANGE_NODE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_NAME | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_ATTRIBUTES | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_VALUE | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_SUBTREE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_RECONNECT | ClusterNotifyFlags.CLUSTER_CHANGE_QUORUM_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_PROPERTY);
			this.m_cen = new AmClusterNotify(this.Cluster.Handle);
			this.m_cen.Initialize(eventMask, (IntPtr)1L);
			return true;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000651D File Offset: 0x0000471D
		private void CleanupClusterNotify()
		{
			if (this.m_cen != null)
			{
				this.m_cen.Dispose();
				this.m_cen = null;
			}
		}

		// Token: 0x0400005A RID: 90
		private readonly TimeSpan DefaultClusterEventNotifierTimeout = new TimeSpan(0, 0, 30);

		// Token: 0x0400005B RID: 91
		private AmClusterNotify m_cen;

		// Token: 0x0400005C RID: 92
		private bool m_isMonitoring;

		// Token: 0x0400005D RID: 93
		private object m_locker = new object();
	}
}
