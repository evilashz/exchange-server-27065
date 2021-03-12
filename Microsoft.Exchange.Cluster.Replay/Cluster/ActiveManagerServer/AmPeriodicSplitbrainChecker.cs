using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200000C RID: 12
	internal class AmPeriodicSplitbrainChecker : AmStartupAutoMounter
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00004445 File Offset: 0x00002645
		internal AmPeriodicSplitbrainChecker()
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004450 File Offset: 0x00002650
		protected override void LogStartupInternal()
		{
			AmTrace.Debug("Starting {0}", new object[]
			{
				base.GetType().Name
			});
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3446025533U);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000448C File Offset: 0x0000268C
		protected override void LogCompletionInternal()
		{
			AmTrace.Debug("Finished {0}", new object[]
			{
				base.GetType().Name
			});
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000044BC File Offset: 0x000026BC
		protected override void PopulateWithDatabaseOperations(Dictionary<Guid, DatabaseInfo> dbMap)
		{
			new Dictionary<Guid, DatabaseInfo>();
			foreach (DatabaseInfo databaseInfo in dbMap.Values)
			{
				new List<AmDbOperation>();
				databaseInfo.Analyze();
				IADDatabase database = databaseInfo.Database;
				if (databaseInfo.IsMismounted)
				{
					AmDbActionCode actionCode = new AmDbActionCode(AmDbActionInitiator.Automatic, AmDbActionReason.PeriodicAction, AmDbActionCategory.ForceDismount);
					AmDbDismountMismountedOperation item = new AmDbDismountMismountedOperation(database, actionCode, databaseInfo.MisMountedServerList);
					databaseInfo.OperationsQueued = new List<AmDbOperation>
					{
						item
					};
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000455C File Offset: 0x0000275C
		protected override List<AmServerName> GetServers()
		{
			return new List<AmServerName>
			{
				AmServerName.LocalComputerName
			};
		}
	}
}
