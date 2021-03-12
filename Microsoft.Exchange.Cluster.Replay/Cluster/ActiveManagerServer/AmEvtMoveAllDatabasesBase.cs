using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000085 RID: 133
	internal class AmEvtMoveAllDatabasesBase : AmEvtServerSwitchoverBase
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x0001AE07 File Offset: 0x00019007
		internal AmEvtMoveAllDatabasesBase(AmServerName nodeName) : base(nodeName)
		{
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001AE10 File Offset: 0x00019010
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0001AE18 File Offset: 0x00019018
		internal AmDbMoveArguments MoveArgs { get; set; }

		// Token: 0x0600053C RID: 1340 RVA: 0x0001AE4C File Offset: 0x0001904C
		internal List<AmDatabaseMoveResult> GetMoveResultsForOperationsRun()
		{
			List<AmDatabaseMoveResult> moveResults = new List<AmDatabaseMoveResult>(base.OperationList.Count);
			base.OperationList.ForEach(delegate(AmDbOperation op)
			{
				if (op.DetailedStatus != null)
				{
					moveResults.Add(op.ConvertDetailedStatusToRpcMoveResult(op.DetailedStatus));
				}
			});
			return moveResults;
		}
	}
}
