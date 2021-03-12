using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000009 RID: 9
	internal class AmStoreStartupAutoMounter : AmStartupAutoMounter
	{
		// Token: 0x06000069 RID: 105 RVA: 0x0000391B File Offset: 0x00001B1B
		internal AmStoreStartupAutoMounter(AmServerName nodeName)
		{
			this.m_nodeName = nodeName;
			this.m_reasonCode = AmDbActionReason.StoreStarted;
			base.IsSelectOnlyActives = true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003938 File Offset: 0x00001B38
		protected override void LogStartupInternal()
		{
			AmTrace.Debug("Starting {0} for {1}", new object[]
			{
				base.GetType().Name,
				this.m_nodeName
			});
			ReplayCrimsonEvents.InitiatingStoreStartAutomount.Log<AmServerName>(this.m_nodeName);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2909154621U);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003990 File Offset: 0x00001B90
		protected override void LogCompletionInternal()
		{
			AmTrace.Debug("Finished {0} for {1}", new object[]
			{
				base.GetType().Name,
				this.m_nodeName
			});
			ReplayCrimsonEvents.CompletedStoreStartAutomount.Log<AmServerName, int, int, int, int>(this.m_nodeName, this.m_mountRequests, this.m_dismountRequests, this.m_clusDbSyncRequests, this.m_moveRequests);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000039EE File Offset: 0x00001BEE
		protected override void RunInternal()
		{
			this.ClearFailureTimeForAllDatabasesOnServer(this.m_nodeName);
			base.RunInternalCommon();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A04 File Offset: 0x00001C04
		protected void ClearFailureTimeForAllDatabasesOnServer(AmServerName serverName)
		{
			IADConfig adconfig = Dependencies.ADConfig;
			IEnumerable<IADDatabase> databasesOnServer = adconfig.GetDatabasesOnServer(serverName);
			if (databasesOnServer != null)
			{
				foreach (IADDatabase iaddatabase in databasesOnServer)
				{
					AmSystemManager.Instance.DbNodeAttemptTable.ClearFailedTime(iaddatabase.Guid);
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003A6C File Offset: 0x00001C6C
		protected override List<AmServerName> GetServers()
		{
			return new List<AmServerName>
			{
				this.m_nodeName
			};
		}

		// Token: 0x04000034 RID: 52
		private AmServerName m_nodeName;
	}
}
