using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x02000812 RID: 2066
	internal sealed class RateLimiter<TKey> : IRateLimiter<TKey>
	{
		// Token: 0x06002B7F RID: 11135 RVA: 0x00060138 File Offset: 0x0005E338
		public bool TryFetchToken(TKey key, int rateLimitPerMinute)
		{
			bool result;
			lock (this.syncRoot)
			{
				TokenRateLimiter tokenRateLimiter = this.GetTokenRateLimiter(key);
				result = tokenRateLimiter.TryFetchToken(rateLimitPerMinute);
			}
			return result;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00060184 File Offset: 0x0005E384
		public void ReleaseUnusedToken(TKey key)
		{
			lock (this.syncRoot)
			{
				TokenRateLimiter tokenRateLimiter = this.GetTokenRateLimiter(key);
				tokenRateLimiter.ReleaseUnusedToken();
			}
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000601CC File Offset: 0x0005E3CC
		public void CleanupIdleEntries(DateTime currentTime)
		{
			List<TKey> list = null;
			lock (this.syncRoot)
			{
				foreach (KeyValuePair<TKey, TokenRateLimiter> keyValuePair in this.keyToTokenRateLimiterMap)
				{
					if (keyValuePair.Value.IsIdle(currentTime))
					{
						if (list == null)
						{
							list = new List<TKey>(10);
						}
						list.Add(keyValuePair.Key);
					}
				}
				if (list != null)
				{
					foreach (TKey key in list)
					{
						this.keyToTokenRateLimiterMap.Remove(key);
					}
				}
			}
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000602B4 File Offset: 0x0005E4B4
		private TokenRateLimiter GetTokenRateLimiter(TKey key)
		{
			TokenRateLimiter tokenRateLimiter;
			if (!this.keyToTokenRateLimiterMap.TryGetValue(key, out tokenRateLimiter))
			{
				tokenRateLimiter = new TokenRateLimiter();
				this.keyToTokenRateLimiterMap.Add(key, tokenRateLimiter);
			}
			return tokenRateLimiter;
		}

		// Token: 0x040025D6 RID: 9686
		private const int KeyToTokenRateLimiterMapInitialCapacity = 10;

		// Token: 0x040025D7 RID: 9687
		private Dictionary<TKey, TokenRateLimiter> keyToTokenRateLimiterMap = new Dictionary<TKey, TokenRateLimiter>(10);

		// Token: 0x040025D8 RID: 9688
		private object syncRoot = new object();
	}
}
