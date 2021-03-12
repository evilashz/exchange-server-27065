using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000538 RID: 1336
	internal abstract class DagMemberCheck : ReplicationCheck
	{
		// Token: 0x06002FF6 RID: 12278 RVA: 0x000C1DA4 File Offset: 0x000BFFA4
		public DagMemberCheck(string serverName, string title, CheckId checkId, LocalizedString description, CheckCategory checkCategory, IEventManager eventManager, string momeventsource, IADDatabaseAvailabilityGroup dag, bool fClusterLevelCheck) : this(serverName, title, checkId, description, checkCategory, eventManager, momeventsource, null, dag, fClusterLevelCheck)
		{
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000C1DD0 File Offset: 0x000BFFD0
		public DagMemberCheck(string serverName, string title, CheckId checkId, LocalizedString description, CheckCategory checkCategory, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold, IADDatabaseAvailabilityGroup dag, bool fClusterLevelCheck) : base(title, checkId, description, checkCategory, eventManager, momeventsource, serverName, ignoreTransientErrorsThreshold)
		{
			this.m_dag = dag;
			this.m_fClusterLevelCheck = fClusterLevelCheck;
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06002FF8 RID: 12280 RVA: 0x000C1E00 File Offset: 0x000C0000
		public AmCluster Cluster
		{
			get
			{
				if (this.m_cluster == null)
				{
					AmServerName serverName = new AmServerName(base.ServerName);
					try
					{
						this.m_cluster = AmCluster.OpenByName(serverName);
						if (this.m_cluster == null)
						{
							base.Fail(Strings.CouldNotConnectToCluster(base.ServerName));
						}
					}
					catch (ClusterException ex)
					{
						base.Fail(Strings.CouldNotConnectToClusterError(base.ServerName, ex.Message));
					}
				}
				return this.m_cluster;
			}
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000C1E78 File Offset: 0x000C0078
		protected override void InternalRun()
		{
			AmServerName serverName = new AmServerName(base.ServerName);
			if (DagHelper.IsNodeClustered(serverName))
			{
				if (!IgnoreTransientErrors.HasPassed(base.GetDefaultErrorKey(typeof(ClusterRpcCheck))))
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "ClusterRpcCheck didn't pass! Skipping check {0}.", base.Title);
					base.Skip();
				}
				if (this.m_fClusterLevelCheck)
				{
					this.RunCheck();
					return;
				}
				using (IEnumerator<AmServerName> enumerator = this.Cluster.EnumerateNodeNames().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AmServerName amServerName = enumerator.Current;
						try
						{
							if (this.IsNodeMemberOfDag(amServerName, this.m_dag))
							{
								using (IAmClusterNode amClusterNode = this.Cluster.OpenNode(amServerName))
								{
									if (this.RunIndividualCheck(amClusterNode))
									{
										base.ReportPassedInstance();
									}
									continue;
								}
							}
							ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Cluster node {0} is not a member of DAG {1} so check will not run here.", amServerName.NetbiosName, this.m_dag.Name);
						}
						finally
						{
							base.InstanceIdentity = null;
						}
					}
					return;
				}
			}
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "Local machine is not clustered! Skipping check {0}.", base.Title);
			base.Skip();
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000C1FC4 File Offset: 0x000C01C4
		private bool IsNodeMemberOfDag(AmServerName node, IADDatabaseAvailabilityGroup dag)
		{
			foreach (ADObjectId adobjectId in dag.Servers)
			{
				if (SharedHelper.StringIEquals(node.NetbiosName, adobjectId.Name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000C202C File Offset: 0x000C022C
		protected bool IsNodeStopped(AmServerName node)
		{
			return this.m_dag.StoppedMailboxServers.Contains(node.Fqdn);
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000C2044 File Offset: 0x000C0244
		protected virtual void RunCheck()
		{
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000C2046 File Offset: 0x000C0246
		protected virtual bool RunIndividualCheck(IAmClusterNode node)
		{
			return true;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000C204C File Offset: 0x000C024C
		protected void SkipOnSamNodeIfMonitoringContext()
		{
			if (ReplicationCheckGlobals.RunningInMonitoringContext && (ReplicationCheckGlobals.ServerConfiguration & ServerConfig.DagMember) == ServerConfig.DagMember)
			{
				if (!IgnoreTransientErrors.HasPassed(base.GetDefaultErrorKey(typeof(ActiveManagerCheck))))
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "ActiveManagerCheck didn't pass! Skipping check {0}.", base.Title);
					base.Skip();
				}
				if (ReplicationCheckGlobals.ActiveManagerRole == AmRole.SAM)
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "SkipOnSAMNode(): Local machine is the SAM. Skipping check {0}.", base.Title);
					base.Skip();
					return;
				}
				if (ReplicationCheckGlobals.ActiveManagerRole == AmRole.PAM)
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug((long)this.GetHashCode(), "SkipOnSAMNode(): Local machine is the PAM. Checks will be run here.");
					return;
				}
				AmRole activeManagerRole = ReplicationCheckGlobals.ActiveManagerRole;
			}
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000C20FB File Offset: 0x000C02FB
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
			if (this.m_cluster != null)
			{
				this.m_cluster.Dispose();
			}
		}

		// Token: 0x04002229 RID: 8745
		protected readonly IADDatabaseAvailabilityGroup m_dag;

		// Token: 0x0400222A RID: 8746
		private AmCluster m_cluster;

		// Token: 0x0400222B RID: 8747
		private readonly bool m_fClusterLevelCheck;
	}
}
