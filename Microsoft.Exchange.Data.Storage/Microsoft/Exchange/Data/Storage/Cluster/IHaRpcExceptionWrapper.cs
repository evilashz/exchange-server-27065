using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x02000317 RID: 791
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IHaRpcExceptionWrapper
	{
		// Token: 0x0600237E RID: 9086
		void ClientRetryableOperation(string serverName, RpcClientOperation rpcOperation);

		// Token: 0x0600237F RID: 9087
		void ClientRethrowIfFailed(string serverName, RpcErrorExceptionInfo errorInfo);

		// Token: 0x06002380 RID: 9088
		void ClientRethrowIfFailed(string databaseName, string serverName, RpcErrorExceptionInfo errorInfo);

		// Token: 0x06002381 RID: 9089
		RpcErrorExceptionInfo RunRpcServerOperation(RpcServerOperation rpcOperation);

		// Token: 0x06002382 RID: 9090
		RpcErrorExceptionInfo RunRpcServerOperation(string databaseName, RpcServerOperation rpcOperation);
	}
}
