using System;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000018 RID: 24
	internal interface IVerifyRpcProxyClient
	{
		// Token: 0x060000A4 RID: 164
		IAsyncResult BeginVerifyRpcProxy(bool makeHangingRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060000A5 RID: 165
		VerifyRpcProxyResult EndVerifyRpcProxy(IAsyncResult asyncResult);
	}
}
