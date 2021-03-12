using System;
using Microsoft.Exchange.SharedCache.Client;

namespace Microsoft.Exchange.HttpProxy.Routing.Providers
{
	// Token: 0x0200000F RID: 15
	public interface ISharedCache
	{
		// Token: 0x0600003A RID: 58
		bool TryGet(string key, out byte[] returnBytes, IRoutingDiagnostics diagnostics);

		// Token: 0x0600003B RID: 59
		bool TryGet<T>(string key, out T value, IRoutingDiagnostics diagnostics) where T : ISharedCacheEntry, new();

		// Token: 0x0600003C RID: 60
		string GetSharedCacheKeyFromRoutingKey(IRoutingKey key);
	}
}
