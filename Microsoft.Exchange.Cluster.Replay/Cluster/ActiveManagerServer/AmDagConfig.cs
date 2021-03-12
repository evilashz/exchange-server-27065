using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000020 RID: 32
	internal class AmDagConfig
	{
		// Token: 0x06000139 RID: 313 RVA: 0x000077B0 File Offset: 0x000059B0
		internal AmDagConfig(ADObjectId id, AmServerName[] memberServers, AmServerName currentPAM, IAmCluster cluster, bool tprEnabled)
		{
			this.Id = id;
			this.MemberServers = memberServers;
			this.CurrentPAM = currentPAM;
			this.Cluster = cluster;
			this.IsThirdPartyReplEnabled = tprEnabled;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000077DD File Offset: 0x000059DD
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000077E5 File Offset: 0x000059E5
		internal bool IsThirdPartyReplEnabled { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000077EE File Offset: 0x000059EE
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000077F6 File Offset: 0x000059F6
		internal ADObjectId Id { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000077FF File Offset: 0x000059FF
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00007807 File Offset: 0x00005A07
		internal AmServerName[] MemberServers { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00007810 File Offset: 0x00005A10
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00007818 File Offset: 0x00005A18
		internal AmServerName CurrentPAM { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00007821 File Offset: 0x00005A21
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00007829 File Offset: 0x00005A29
		internal IAmCluster Cluster { get; set; }

		// Token: 0x06000144 RID: 324 RVA: 0x00007834 File Offset: 0x00005A34
		internal AmNodeState GetNodeState(AmServerName nodeName)
		{
			AmNodeState result = AmNodeState.Unknown;
			try
			{
				IAmClusterNode amClusterNode2;
				IAmClusterNode amClusterNode = amClusterNode2 = this.Cluster.OpenNode(nodeName);
				try
				{
					result = amClusterNode.State;
				}
				finally
				{
					if (amClusterNode2 != null)
					{
						amClusterNode2.Dispose();
					}
				}
			}
			catch (ClusterException ex)
			{
				AmTrace.Error("Failed to open cluster node {0} (error={1})", new object[]
				{
					nodeName,
					ex.Message
				});
			}
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000078AC File Offset: 0x00005AAC
		internal bool IsNodePubliclyUp(AmServerName nodeName)
		{
			return AmSystemManager.Instance.NetworkMonitor == null || AmSystemManager.Instance.NetworkMonitor.IsNodePubliclyUp(nodeName);
		}
	}
}
