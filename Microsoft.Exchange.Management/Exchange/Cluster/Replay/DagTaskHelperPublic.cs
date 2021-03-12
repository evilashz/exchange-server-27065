using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200088D RID: 2189
	public static class DagTaskHelperPublic
	{
		// Token: 0x06004C48 RID: 19528 RVA: 0x0013DAAE File Offset: 0x0013BCAE
		public static bool IsLocalNodeClustered()
		{
			return DagHelper.IsLocalNodeClustered();
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0013DAC0 File Offset: 0x0013BCC0
		public static bool MovePrimaryActiveManagerRole(string CurrentPrimaryName)
		{
			AmServerName serverName = new AmServerName(CurrentPrimaryName);
			bool result;
			using (IAmCluster amCluster = ClusterFactory.Instance.OpenByName(serverName))
			{
				using (IAmClusterGroup amClusterGroup = amCluster.FindCoreClusterGroup())
				{
					string text;
					result = amClusterGroup.MoveGroupToReplayEnabledNode((string targetNode) => AmHelper.IsReplayRunning(targetNode), "Network Name", new TimeSpan(0, 3, 0), out text);
				}
			}
			return result;
		}
	}
}
