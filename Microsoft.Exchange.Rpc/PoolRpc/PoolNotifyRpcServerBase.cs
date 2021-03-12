using System;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x0200038B RID: 907
	internal abstract class PoolNotifyRpcServerBase : PoolRpcServerCommonBase
	{
		// Token: 0x0600101A RID: 4122 RVA: 0x00049E48 File Offset: 0x00049248
		public PoolNotifyRpcServerBase()
		{
		}

		// Token: 0x04000F5A RID: 3930
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.emsmdbpoolNotify_v0_1_s_ifspec;
	}
}
