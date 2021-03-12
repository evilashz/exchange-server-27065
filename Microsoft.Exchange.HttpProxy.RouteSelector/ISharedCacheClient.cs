using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.SharedCache.Client;

namespace Microsoft.Exchange.HttpProxy.RouteSelector
{
	// Token: 0x02000007 RID: 7
	public interface ISharedCacheClient : ISharedCache
	{
		// Token: 0x0600000F RID: 15
		bool TryInsert(string key, byte[] dataBytes, DateTime cacheExpiry, out string diagInfo);

		// Token: 0x06000010 RID: 16
		bool TryInsert(string key, ISharedCacheEntry value, DateTime valueTimeStamp, out string diagInfo);
	}
}
