using System;
using System.Text;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200053C RID: 1340
	internal class QuorumGroupCheck : DagMemberCheck
	{
		// Token: 0x0600300A RID: 12298 RVA: 0x000C2520 File Offset: 0x000C0720
		public QuorumGroupCheck(string serverName, IEventManager eventManager, string momeventsource, IADDatabaseAvailabilityGroup dag) : base(serverName, "QuorumGroup", CheckId.QuorumGroup, Strings.QuorumGroupCheckDesc, CheckCategory.SystemMediumPriority, eventManager, momeventsource, dag, true)
		{
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000C254C File Offset: 0x000C074C
		public QuorumGroupCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold, IADDatabaseAvailabilityGroup dag) : base(serverName, "QuorumGroup", CheckId.QuorumGroup, Strings.QuorumGroupCheckDesc, CheckCategory.SystemMediumPriority, eventManager, momeventsource, ignoreTransientErrorsThreshold, dag, true)
		{
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000C2578 File Offset: 0x000C0778
		protected override void RunCheck()
		{
			using (IAmClusterGroup amClusterGroup = base.Cluster.FindCoreClusterGroup())
			{
				if (amClusterGroup.State != AmGroupState.Online)
				{
					base.Fail(Strings.QuorumGroupNotOnline(amClusterGroup.Name, amClusterGroup.OwnerNode.NetbiosName, base.Cluster.Name, QuorumGroupCheck.ReportResourcesNotOnline(amClusterGroup), Environment.NewLine));
				}
			}
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000C25E8 File Offset: 0x000C07E8
		public static string ReportResourcesNotOnline(IAmClusterGroup resGroup)
		{
			if (resGroup == null)
			{
				throw new ArgumentNullException("resGroup cannot be null!");
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (AmClusterResource amClusterResource in resGroup.EnumerateResources())
			{
				using (amClusterResource)
				{
					AmResourceState state = amClusterResource.GetState();
					if (state != AmResourceState.Online)
					{
						stringBuilder.AppendFormat("\t\t{0}: {1}{2}", amClusterResource.Name, state.ToString(), Environment.NewLine);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400222E RID: 8750
		public const string ReportResourcesFormatString = "\t\t{0}: {1}{2}";
	}
}
