using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000537 RID: 1335
	internal class ClusterRpcCheck : ReplicationCheck
	{
		// Token: 0x06002FF1 RID: 12273 RVA: 0x000C1CA0 File Offset: 0x000BFEA0
		public ClusterRpcCheck(string serverName, IEventManager eventManager, string momeventsource) : base("ClusterService", CheckId.ClusterService, Strings.ClusterRpcCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName)
		{
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000C1CBC File Offset: 0x000BFEBC
		public ClusterRpcCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold) : base("ClusterService", CheckId.ClusterService, Strings.ClusterRpcCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000C1CE4 File Offset: 0x000BFEE4
		protected override void InternalRun()
		{
			AmServerName serverName = new AmServerName(base.ServerName);
			if (DagHelper.IsNodeClustered(serverName))
			{
				try
				{
					this.m_Cluster = AmCluster.OpenByName(serverName);
					if (this.m_Cluster == null)
					{
						base.Fail(Strings.CouldNotConnectToCluster(base.ServerName));
					}
					return;
				}
				catch (ClusterException ex)
				{
					base.Fail(Strings.CouldNotConnectToClusterError(base.ServerName, ex.Message));
					return;
				}
			}
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Server {1} is not clustered! Skipping check {0}.", base.Title, base.ServerName);
			base.Skip();
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000C1D80 File Offset: 0x000BFF80
		public AmCluster GetClusterHandle()
		{
			return this.m_Cluster;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000C1D88 File Offset: 0x000BFF88
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
			if (this.m_Cluster != null)
			{
				this.m_Cluster.Dispose();
			}
		}

		// Token: 0x04002228 RID: 8744
		private AmCluster m_Cluster;
	}
}
