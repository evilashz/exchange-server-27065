using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000890 RID: 2192
	internal interface IServiceProxyPool<TClient>
	{
		// Token: 0x06002ED3 RID: 11987
		bool TryCallServiceWithRetryAsyncBegin(Action<IPooledServiceProxy<TClient>> action, string debugMessage, int numberOfRetries, out Exception exception);

		// Token: 0x06002ED4 RID: 11988
		bool TryCallServiceWithRetryAsyncEnd(IPooledServiceProxy<TClient> cachedProxy, Action<IPooledServiceProxy<TClient>> action, string debugMessage, out Exception exception);
	}
}
