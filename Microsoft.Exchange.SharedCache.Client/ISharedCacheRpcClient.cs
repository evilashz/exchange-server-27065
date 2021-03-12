using System;
using Microsoft.Exchange.Rpc.SharedCache;

namespace Microsoft.Exchange.SharedCache.Client
{
	// Token: 0x02000004 RID: 4
	internal interface ISharedCacheRpcClient
	{
		// Token: 0x06000005 RID: 5
		CacheResponse Get(Guid guid, string key);

		// Token: 0x06000006 RID: 6
		CacheResponse Insert(Guid guid, string key, byte[] inBlob, long entryValidTime);

		// Token: 0x06000007 RID: 7
		CacheResponse Delete(Guid guid, string key);
	}
}
