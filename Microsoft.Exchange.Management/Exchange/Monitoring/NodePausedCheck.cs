using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200053A RID: 1338
	internal class NodePausedCheck : DagMemberCheck
	{
		// Token: 0x06003003 RID: 12291 RVA: 0x000C21D4 File Offset: 0x000C03D4
		public NodePausedCheck(string serverName, IEventManager eventManager, string momeventsource, IADDatabaseAvailabilityGroup dag) : base(serverName, "NodePaused", CheckId.NodePaused, Strings.NodePausedCheckDesc, CheckCategory.SystemMediumPriority, eventManager, momeventsource, dag, false)
		{
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000C2200 File Offset: 0x000C0400
		public NodePausedCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold, IADDatabaseAvailabilityGroup dag) : base(serverName, "NodePaused", CheckId.NodePaused, Strings.NodePausedCheckDesc, CheckCategory.SystemMediumPriority, eventManager, momeventsource, ignoreTransientErrorsThreshold, dag, false)
		{
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000C222C File Offset: 0x000C042C
		protected override bool RunIndividualCheck(IAmClusterNode node)
		{
			base.InstanceIdentity = node.Name.NetbiosName;
			AmNodeState state = node.GetState(false);
			if (state == AmNodeState.Paused)
			{
				base.FailContinue(Strings.DagMemberPausedFailed(node.Name.NetbiosName, this.m_dag.Name));
				return false;
			}
			return true;
		}
	}
}
