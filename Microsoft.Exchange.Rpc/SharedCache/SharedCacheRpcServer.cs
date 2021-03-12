using System;

namespace Microsoft.Exchange.Rpc.SharedCache
{
	// Token: 0x020003F0 RID: 1008
	internal abstract class SharedCacheRpcServer : RpcServerBase
	{
		// Token: 0x06001146 RID: 4422
		public abstract void Get(Guid guid, string key, ref CacheResponse cacheResponse);

		// Token: 0x06001147 RID: 4423
		public abstract void Insert(Guid guid, string key, byte[] pInBytes, long entryValidTime, ref CacheResponse cacheResponse);

		// Token: 0x06001148 RID: 4424
		public abstract void Delete(Guid guid, string key, ref CacheResponse cacheResponse);

		// Token: 0x06001149 RID: 4425 RVA: 0x00056C74 File Offset: 0x00056074
		public SharedCacheRpcServer()
		{
		}

		// Token: 0x04001020 RID: 4128
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.ISharedCache_v1_0_s_ifspec;
	}
}
