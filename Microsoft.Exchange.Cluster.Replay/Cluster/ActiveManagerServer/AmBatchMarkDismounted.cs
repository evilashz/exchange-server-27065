using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000012 RID: 18
	internal class AmBatchMarkDismounted : AmBatchOperationBase
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00004ECF File Offset: 0x000030CF
		internal AmBatchMarkDismounted(AmServerName nodeName, AmDbActionReason reasonCode)
		{
			this.m_nodeName = nodeName;
			this.m_reasonCode = reasonCode;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004EE5 File Offset: 0x000030E5
		protected override void LogStartupInternal()
		{
			ReplayCrimsonEvents.InitiatingMarkDismounted.Log<AmServerName>(this.m_nodeName);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2439392573U);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004F06 File Offset: 0x00003106
		protected override void LogCompletionInternal()
		{
			ReplayCrimsonEvents.MarkedDatabasesStatesAsDismounted.Log<AmServerName, int>(this.m_nodeName, this.m_clusDbSyncRequests);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004F20 File Offset: 0x00003120
		protected override void RunInternal()
		{
			IADToplogyConfigurationSession iadtoplogyConfigurationSession = ADSessionFactory.CreateIgnoreInvalidRootOrgSession(true);
			IADServer iadserver = iadtoplogyConfigurationSession.FindServerByName(this.m_nodeName.NetbiosName);
			if (iadserver == null)
			{
				throw new ServerNotFoundException(this.m_nodeName.NetbiosName);
			}
			IADDatabase[] array = iadtoplogyConfigurationSession.GetAllDatabases(iadserver).ToArray<IADDatabase>();
			if (array.Length <= 0)
			{
				AmTrace.Info("Server '{0}' does not have any databases that needs to be marked as dismounted", new object[]
				{
					this.m_nodeName
				});
				return;
			}
			AmDbActionCode actionCode = new AmDbActionCode(AmDbActionInitiator.Automatic, this.m_reasonCode, AmDbActionCategory.SyncState);
			foreach (IADDatabase db in array)
			{
				AmDbClusterDatabaseSyncOperation operation = new AmDbClusterDatabaseSyncOperation(db, actionCode);
				this.m_clusDbSyncRequests++;
				base.EnqueueDatabaseOperation(operation);
			}
			base.StartDatabaseOperations();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004FE0 File Offset: 0x000031E0
		protected override List<AmServerName> GetServers()
		{
			return new List<AmServerName>
			{
				this.m_nodeName
			};
		}

		// Token: 0x04000041 RID: 65
		private AmServerName m_nodeName;

		// Token: 0x04000042 RID: 66
		private AmDbActionReason m_reasonCode;
	}
}
