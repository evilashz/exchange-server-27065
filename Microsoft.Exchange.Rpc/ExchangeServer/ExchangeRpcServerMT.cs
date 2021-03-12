using System;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x0200025B RID: 603
	internal abstract class ExchangeRpcServerMT : ExchangeRpcServerBase
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x00027F8C File Offset: 0x0002738C
		public ExchangeRpcServerMT()
		{
		}

		// Token: 0x04000CD3 RID: 3283
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.emsmdbMT_v0_81_s_ifspec;
	}
}
