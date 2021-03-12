using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000CA RID: 202
	internal interface IAmRpcClientHelper
	{
		// Token: 0x06000833 RID: 2099
		int RpcchGetAutomountConsensusState(string serverName);

		// Token: 0x06000834 RID: 2100
		bool IsReplayRunning(AmServerName serverName);

		// Token: 0x06000835 RID: 2101
		bool IsReplayRunning(string serverFqdn);

		// Token: 0x06000836 RID: 2102
		AmRole GetActiveManagerRole(string serverToRpc, out string errorMessage);

		// Token: 0x06000837 RID: 2103
		void MountDatabaseDirectEx(string serverToRpc, Guid dbGuid, AmMountArg mountArg);

		// Token: 0x06000838 RID: 2104
		void DismountDatabaseDirect(string serverToRpc, Guid dbGuid, AmDismountArg dismountArg);

		// Token: 0x06000839 RID: 2105
		void AttemptCopyLastLogsDirect(string serverToRpc, Guid dbGuid, DatabaseMountDialOverride mountDialOverride, int numRetries, int e00timeoutMs, int networkIOtimeoutMs, int networkConnecttimeoutMs, string sourceServer, int actionCode, int skipValidationChecks, bool mountPending, string uniqueOperationId, int subactionAttemptNumber, ref AmAcllReturnStatus acllStatus);
	}
}
