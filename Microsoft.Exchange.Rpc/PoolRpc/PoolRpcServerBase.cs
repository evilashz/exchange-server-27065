using System;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x0200038A RID: 906
	internal abstract class PoolRpcServerBase : PoolRpcServerCommonBase
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x00049E34 File Offset: 0x00049234
		public PoolRpcServerBase()
		{
		}

		// Token: 0x04000F59 RID: 3929
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.emsmdbpool_v0_1_s_ifspec;
	}
}
