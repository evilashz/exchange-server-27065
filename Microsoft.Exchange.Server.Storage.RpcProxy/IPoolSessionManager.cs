using System;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeServer;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000014 RID: 20
	internal interface IPoolSessionManager
	{
		// Token: 0x06000099 RID: 153
		ErrorCode CreateProxySession(ClientSecurityContext callerSecurityContext, uint flags, string userDn, uint connectionMode, uint codePageId, uint localeIdString, uint localeIdSort, short[] clientVersion, byte[] auxiliaryIn, Action<ErrorCode, uint> notificationPendingCallback, out uint sessionHandle, out byte[] auxiliaryOut);

		// Token: 0x0600009A RID: 154
		ErrorCode BeginPoolDoRpc(ref uint sessionHandle, uint flags, uint maximumResponseSize, ArraySegment<byte> request, ArraySegment<byte> auxiliaryIn, DoRpcCompleteCallback callback, Action<RpcException> exceptionCallback);

		// Token: 0x0600009B RID: 155
		ErrorCode QueueNotificationWait(ref uint sessionHandle, IProxyAsyncWaitCompletion completion);

		// Token: 0x0600009C RID: 156
		void CloseSession(uint sessionHandle);
	}
}
