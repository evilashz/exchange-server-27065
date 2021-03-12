using System;
using Microsoft.Exchange.Rpc.SharedCache;

namespace Microsoft.Exchange.SharedCache.Client
{
	// Token: 0x02000009 RID: 9
	internal class SharedCacheRpcClientImpl : SharedCacheRpcClient, ISharedCacheRpcClient
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002880 File Offset: 0x00000A80
		public SharedCacheRpcClientImpl(string machineName, int timeoutMilliseconds) : base(machineName, timeoutMilliseconds)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000288A File Offset: 0x00000A8A
		CacheResponse ISharedCacheRpcClient.Get(Guid A_1, string A_2)
		{
			return base.Get(A_1, A_2);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002894 File Offset: 0x00000A94
		CacheResponse ISharedCacheRpcClient.Insert(Guid A_1, string A_2, byte[] A_3, long A_4)
		{
			return base.Insert(A_1, A_2, A_3, A_4);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028A1 File Offset: 0x00000AA1
		CacheResponse ISharedCacheRpcClient.Delete(Guid A_1, string A_2)
		{
			return base.Delete(A_1, A_2);
		}
	}
}
