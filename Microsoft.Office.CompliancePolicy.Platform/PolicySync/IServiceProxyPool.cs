using System;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200010D RID: 269
	internal interface IServiceProxyPool<TClient>
	{
		// Token: 0x06000753 RID: 1875
		bool TryCallServiceWithRetryAsyncBegin(Action<IPooledServiceProxy<TClient>> action, string debugMessage, int numberOfRetries, out Exception exception);

		// Token: 0x06000754 RID: 1876
		bool TryCallServiceWithRetryAsyncEnd(IPooledServiceProxy<TClient> cachedProxy, Action<IPooledServiceProxy<TClient>> action, string debugMessage, out Exception exception);
	}
}
