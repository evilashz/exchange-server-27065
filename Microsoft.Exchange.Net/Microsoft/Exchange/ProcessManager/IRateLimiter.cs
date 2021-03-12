using System;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x02000811 RID: 2065
	public interface IRateLimiter<TKey>
	{
		// Token: 0x06002B7C RID: 11132
		bool TryFetchToken(TKey key, int rateLimitPerMinute);

		// Token: 0x06002B7D RID: 11133
		void ReleaseUnusedToken(TKey key);

		// Token: 0x06002B7E RID: 11134
		void CleanupIdleEntries(DateTime currentTime);
	}
}
