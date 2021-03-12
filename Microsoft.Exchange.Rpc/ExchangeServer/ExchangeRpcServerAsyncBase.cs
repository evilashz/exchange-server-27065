using System;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x0200025A RID: 602
	internal abstract class ExchangeRpcServerAsyncBase : RpcServerBase
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x00027F78 File Offset: 0x00027378
		public ExchangeRpcServerAsyncBase()
		{
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00027318 File Offset: 0x00026718
		public virtual IProxyServer GetProxyServer()
		{
			return null;
		}
	}
}
