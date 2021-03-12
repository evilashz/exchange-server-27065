using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001EB RID: 491
	internal class AmMultiNodeCopyStatusFetcher_ForPoller : AmMultiNodeCopyStatusFetcher
	{
		// Token: 0x06001377 RID: 4983 RVA: 0x0004EE02 File Offset: 0x0004D002
		public AmMultiNodeCopyStatusFetcher_ForPoller(IMonitoringADConfig adConfig, ActiveManager activeManager, ConcurrentDictionary<AmServerName, bool> rpcThreadsInProgress) : base(adConfig.AmServerNames, adConfig.DatabaseMap, RpcGetDatabaseCopyStatusFlags2.None, activeManager, false)
		{
			this.m_rpcThreadsInProgress = rpcThreadsInProgress;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004EE34 File Offset: 0x0004D034
		protected override bool TryStartRpc(AmServerName server)
		{
			bool inProgress = false;
			bool flag = this.m_rpcThreadsInProgress.TryGetValue(server, out inProgress);
			if (flag && inProgress)
			{
				return false;
			}
			this.m_rpcThreadsInProgress.AddOrUpdate(server, true, delegate(AmServerName serverName, bool oldValue)
			{
				inProgress = oldValue;
				return true;
			});
			return !inProgress;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0004EE91 File Offset: 0x0004D091
		protected override void RecordRpcCompleted(AmServerName server)
		{
			this.m_rpcThreadsInProgress.TryUpdate(server, false, true);
		}

		// Token: 0x04000782 RID: 1922
		private ConcurrentDictionary<AmServerName, bool> m_rpcThreadsInProgress;
	}
}
