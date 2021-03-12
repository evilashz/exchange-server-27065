using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002FB RID: 763
	internal interface IReplayRpcClient
	{
		// Token: 0x06001EFA RID: 7930
		void RequestSuspend3(string serverName, Guid guid, string suspendComment, uint flags, uint initiator);

		// Token: 0x06001EFB RID: 7931
		void RequestResume2(string serverName, Guid guid, uint flags);

		// Token: 0x06001EFC RID: 7932
		void RpccDisableReplayLag(string serverName, Guid dbGuid, string disableReason, ActionInitiatorType actionInitiator);

		// Token: 0x06001EFD RID: 7933
		void RpccEnableReplayLag(string serverName, Guid dbGuid, ActionInitiatorType actionInitiator);

		// Token: 0x06001EFE RID: 7934
		RpcDatabaseCopyStatus2[] GetCopyStatus(string serverName, RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids);

		// Token: 0x06001EFF RID: 7935
		RpcDatabaseCopyStatus2[] GetCopyStatus(string serverName, RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, int timeoutMs);

		// Token: 0x06001F00 RID: 7936
		RpcCopyStatusContainer GetCopyStatusWithHealthState(string serverName, RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, int timeoutMs);

		// Token: 0x06001F01 RID: 7937
		void NotifyChangedReplayConfiguration(string serverName, Guid dbGuid, ServerVersion serverVersion, bool waitForCompletion, bool isHighPriority, ReplayConfigChangeHints changeHint);
	}
}
