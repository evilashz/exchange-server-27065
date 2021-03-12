using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000539 RID: 1337
	internal class DagMembersUpCheck : DagMemberCheck
	{
		// Token: 0x06003000 RID: 12288 RVA: 0x000C2118 File Offset: 0x000C0318
		public DagMembersUpCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold, IADDatabaseAvailabilityGroup dag) : base(serverName, "DagMembersUp", CheckId.DagMembersUp, Strings.DagMembersUpCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, ignoreTransientErrorsThreshold, dag, false)
		{
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000C2144 File Offset: 0x000C0344
		public DagMembersUpCheck(string serverName, IEventManager eventManager, string momeventsource, IADDatabaseAvailabilityGroup dag) : base(serverName, "DagMembersUp", CheckId.DagMembersUp, Strings.DagMembersUpCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, dag, false)
		{
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000C2170 File Offset: 0x000C0370
		protected override bool RunIndividualCheck(IAmClusterNode node)
		{
			base.InstanceIdentity = node.Name.NetbiosName;
			AmNodeState state = node.GetState(false);
			if ((state == AmNodeState.Down || state == AmNodeState.Joining || state == AmNodeState.Unknown) && !base.IsNodeStopped(node.Name))
			{
				base.FailContinue(Strings.DagMemberUpCheckFailed(node.Name.NetbiosName, this.m_dag.Name));
				return false;
			}
			return true;
		}

		// Token: 0x0400222C RID: 8748
		public const string CheckTitle = "DagMembersUp";
	}
}
