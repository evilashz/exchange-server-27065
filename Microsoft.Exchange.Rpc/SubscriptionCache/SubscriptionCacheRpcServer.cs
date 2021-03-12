using System;

namespace Microsoft.Exchange.Rpc.SubscriptionCache
{
	// Token: 0x020003F2 RID: 1010
	internal abstract class SubscriptionCacheRpcServer : RpcServerBase
	{
		// Token: 0x0600114F RID: 4431
		public abstract byte[] TestUserCache(int version, byte[] pInBytes);

		// Token: 0x06001150 RID: 4432 RVA: 0x00056DF8 File Offset: 0x000561F8
		public SubscriptionCacheRpcServer()
		{
		}

		// Token: 0x04001022 RID: 4130
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.ISubscriptionCacheServer_v1_0_s_ifspec;
	}
}
