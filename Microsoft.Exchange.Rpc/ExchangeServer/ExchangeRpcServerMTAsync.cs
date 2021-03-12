using System;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x0200025C RID: 604
	internal abstract class ExchangeRpcServerMTAsync : ExchangeRpcServerAsyncBase
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x00027FA0 File Offset: 0x000273A0
		public ExchangeRpcServerMTAsync()
		{
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00027344 File Offset: 0x00026744
		// Note: this type is marked as 'beforefieldinit'.
		static ExchangeRpcServerMTAsync()
		{
			IntPtr rpcIntfHandle = new IntPtr(<Module>.asyncemsmdbMT_v0_1_s_ifspec);
			ExchangeRpcServerMTAsync.RpcIntfHandle = rpcIntfHandle;
		}

		// Token: 0x04000CD4 RID: 3284
		public static IntPtr RpcIntfHandle;
	}
}
