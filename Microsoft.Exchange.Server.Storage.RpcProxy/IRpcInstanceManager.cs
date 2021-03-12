using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Rpc.AdminRpc;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000018 RID: 24
	internal interface IRpcInstanceManager
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000B3 RID: 179
		// (remove) Token: 0x060000B4 RID: 180
		event OnPoolNotificationsReceivedCallback NotificationsReceived;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000B5 RID: 181
		// (remove) Token: 0x060000B6 RID: 182
		event OnRpcInstanceClosedCallback RpcInstanceClosed;

		// Token: 0x060000B7 RID: 183
		void StopAcceptingCalls();

		// Token: 0x060000B8 RID: 184
		ErrorCode StartInstance(Guid instanceId, uint flags, ref bool isNewInstanceStarted, CancellationToken cancellationToken);

		// Token: 0x060000B9 RID: 185
		void StopInstance(Guid instanceId, bool terminate);

		// Token: 0x060000BA RID: 186
		bool IsInstanceStarted(Guid instanceId);

		// Token: 0x060000BB RID: 187
		string GetInstanceDisplayName(Guid instanceId);

		// Token: 0x060000BC RID: 188
		RpcInstanceManager.AdminCallGuard GetAdminRpcClient(Guid instanceId, string functionName, out AdminRpcClient adminRpc);

		// Token: 0x060000BD RID: 189
		RpcInstanceManager.RpcClient<RpcInstancePool> GetPoolRpcClient(Guid instanceId, ref int generation, out RpcInstancePool rpcClient);

		// Token: 0x060000BE RID: 190
		IEnumerable<Guid> GetActiveInstances();
	}
}
