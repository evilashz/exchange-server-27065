using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000004 RID: 4
	internal class DelegatedPrincipalCache
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00003B70 File Offset: 0x00001D70
		static DelegatedPrincipalCache()
		{
			DelegatedPrincipalCache.delegatedCache = new Dictionary<DelegatedPrincipalCache.CacheKey, DelegatedPrincipalCacheData>();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003BAC File Offset: 0x00001DAC
		internal static bool TrySetEntry(string targetOrgId, string userId, string securityToken, DelegatedPrincipalCacheData data)
		{
			DelegatedPrincipalCache.CacheKey key = new DelegatedPrincipalCache.CacheKey(targetOrgId, userId, securityToken);
			bool result = false;
			lock (DelegatedPrincipalCache.syncObj)
			{
				if (DelegatedPrincipalCache.delegatedCache.Count < 5000)
				{
					DelegatedPrincipalCache.delegatedCache[key] = data;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003C10 File Offset: 0x00001E10
		internal static DelegatedPrincipalCacheData GetEntry(string targetOrgId, string userId, string securityToken)
		{
			DelegatedPrincipalCache.CacheKey key = new DelegatedPrincipalCache.CacheKey(targetOrgId, userId, securityToken);
			DelegatedPrincipalCacheData delegatedPrincipalCacheData = null;
			if (DelegatedPrincipalCache.delegatedCache.ContainsKey(key))
			{
				lock (DelegatedPrincipalCache.syncObj)
				{
					if (DelegatedPrincipalCache.delegatedCache.TryGetValue(key, out delegatedPrincipalCacheData))
					{
						if (delegatedPrincipalCacheData != null && DelegatedPrincipalCache.IsCacheBucketExpired(delegatedPrincipalCacheData))
						{
							DelegatedPrincipalCache.delegatedCache.Remove(key);
							delegatedPrincipalCacheData = null;
						}
						else
						{
							delegatedPrincipalCacheData.LastReadTime = DateTime.UtcNow;
						}
					}
				}
			}
			return delegatedPrincipalCacheData;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003C98 File Offset: 0x00001E98
		internal static void Cleanup()
		{
			if (DelegatedPrincipalCache.cleanupTime > DateTime.UtcNow)
			{
				return;
			}
			lock (DelegatedPrincipalCache.syncObj)
			{
				if (!(DelegatedPrincipalCache.cleanupTime > DateTime.UtcNow))
				{
					List<DelegatedPrincipalCache.CacheKey> list = new List<DelegatedPrincipalCache.CacheKey>();
					foreach (DelegatedPrincipalCache.CacheKey cacheKey in DelegatedPrincipalCache.delegatedCache.Keys)
					{
						DelegatedPrincipalCacheData data = DelegatedPrincipalCache.delegatedCache[cacheKey];
						if (DelegatedPrincipalCache.IsCacheBucketExpired(data))
						{
							list.Add(cacheKey);
						}
					}
					foreach (DelegatedPrincipalCache.CacheKey key in list)
					{
						DelegatedPrincipalCache.delegatedCache.Remove(key);
					}
					DelegatedPrincipalCache.cleanupTime = DateTime.UtcNow.AddHours(6.0);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003DC0 File Offset: 0x00001FC0
		internal static DateTime NextScheduleCacheCleanUp()
		{
			return DelegatedPrincipalCache.cleanupTime;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003DC8 File Offset: 0x00001FC8
		internal static bool RemoveEntry(string targetOrgId, string userId, string securityToken)
		{
			DelegatedPrincipalCache.CacheKey key = new DelegatedPrincipalCache.CacheKey(targetOrgId, userId, securityToken);
			bool result;
			lock (DelegatedPrincipalCache.syncObj)
			{
				result = DelegatedPrincipalCache.delegatedCache.Remove(key);
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003E18 File Offset: 0x00002018
		private static bool IsCacheBucketExpired(DelegatedPrincipalCacheData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return data.IsExpired() && data.LastReadTime.AddMinutes(6.0) < DateTime.UtcNow;
		}

		// Token: 0x04000018 RID: 24
		private const int ReadExpirationTimeWindowInMinutes = 6;

		// Token: 0x04000019 RID: 25
		private const int CacheCleanupTimeInHours = 6;

		// Token: 0x0400001A RID: 26
		private const int MaximumCacheSize = 5000;

		// Token: 0x0400001B RID: 27
		private static Dictionary<DelegatedPrincipalCache.CacheKey, DelegatedPrincipalCacheData> delegatedCache;

		// Token: 0x0400001C RID: 28
		private static object syncObj = new object();

		// Token: 0x0400001D RID: 29
		private static DateTime cleanupTime = DateTime.UtcNow.AddHours(6.0);

		// Token: 0x02000005 RID: 5
		private class CacheKey : IEquatable<DelegatedPrincipalCache.CacheKey>
		{
			// Token: 0x06000033 RID: 51 RVA: 0x00003E66 File Offset: 0x00002066
			internal CacheKey(string targetOrgId, string userId) : this(targetOrgId, userId, null)
			{
			}

			// Token: 0x06000034 RID: 52 RVA: 0x00003E74 File Offset: 0x00002074
			internal CacheKey(string targetOrgId, string userId, string securityToken)
			{
				if (string.IsNullOrEmpty(targetOrgId))
				{
					throw new ArgumentNullException("targetOrgId");
				}
				if (string.IsNullOrEmpty(userId))
				{
					throw new ArgumentNullException("userId");
				}
				this.targetOrgId = targetOrgId;
				this.userId = userId;
				this.securityToken = securityToken;
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00003EC2 File Offset: 0x000020C2
			public override bool Equals(object obj)
			{
				return this.Equals(obj as DelegatedPrincipalCache.CacheKey);
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00003ED0 File Offset: 0x000020D0
			public override int GetHashCode()
			{
				return this.userId.GetHashCode();
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00003EDD File Offset: 0x000020DD
			public bool Equals(DelegatedPrincipalCache.CacheKey other)
			{
				return other != null && (this.targetOrgId.Equals(other.targetOrgId) && this.userId.Equals(other.userId)) && string.Equals(this.securityToken, other.securityToken);
			}

			// Token: 0x0400001E RID: 30
			private string targetOrgId;

			// Token: 0x0400001F RID: 31
			private string userId;

			// Token: 0x04000020 RID: 32
			private string securityToken;
		}
	}
}
