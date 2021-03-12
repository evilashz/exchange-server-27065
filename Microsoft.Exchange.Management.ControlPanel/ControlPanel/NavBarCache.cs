using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000246 RID: 582
	public static class NavBarCache
	{
		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x0007E448 File Offset: 0x0007C648
		private static Dictionary<string, NavBarPack> Cache
		{
			get
			{
				if (NavBarCache.navBarCache == null)
				{
					ExTraceGlobals.WebServiceTracer.TraceInformation(0, 0L, "Create NavBarCache.cache.");
					NavBarCache.navBarCache = new Dictionary<string, NavBarPack>(NavBarCache.navBarCacheCapacity);
				}
				return NavBarCache.navBarCache;
			}
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x0007E478 File Offset: 0x0007C678
		public static NavBarPack Get(string userPuid, string userPrincipalName, string cultureName)
		{
			NavBarPack navBarPack = null;
			Dictionary<string, NavBarPack> cache = NavBarCache.Cache;
			lock (cache)
			{
				cache.TryGetValue(userPuid, out navBarPack);
			}
			if (navBarPack != null && navBarPack.Culture != cultureName)
			{
				ExTraceGlobals.WebServiceTracer.TraceInformation(0, 0L, "NavPack exist but with different culture so can't use. Requested for user: {0} {1}, culure: {2}, cache created: {3} with culture: {4}.", new object[]
				{
					userPuid,
					userPrincipalName,
					cultureName,
					navBarPack.CreateTime,
					navBarPack.Culture
				});
				navBarPack = null;
			}
			return navBarPack;
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x0007E518 File Offset: 0x0007C718
		public static void Set(string userPuid, string userPrincipalName, NavBarPack navBarPack)
		{
			if (!string.IsNullOrEmpty(userPuid))
			{
				ExTraceGlobals.WebServiceTracer.TraceInformation<string>(0, 0L, "NavBarCache::Set() Add {0} to NavBarCache.", userPuid);
				Dictionary<string, NavBarPack> cache = NavBarCache.Cache;
				lock (cache)
				{
					cache[userPuid] = navBarPack;
				}
			}
			NavBarCache.CleanUpIfNeeded();
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x0007E57C File Offset: 0x0007C77C
		private static void CleanUpIfNeeded()
		{
			if (NavBarCache.Cache.Count > NavBarCache.navBarCacheWarningSize && DateTime.UtcNow - NavBarCache.lastCleanUpTime > NavBarCache.minCleanUpInterval)
			{
				NavBarCache.lastCleanUpTime = DateTime.UtcNow;
				DateTime t = DateTime.UtcNow - NavBarCache.minCacheLife;
				List<string> list = new List<string>();
				Dictionary<string, NavBarPack> cache = NavBarCache.Cache;
				lock (cache)
				{
					foreach (KeyValuePair<string, NavBarPack> keyValuePair in cache)
					{
						if (keyValuePair.Value.CreateTime < t)
						{
							list.Add(keyValuePair.Key);
						}
					}
					foreach (string key in list)
					{
						cache.Remove(key);
					}
				}
				ExTraceGlobals.WebServiceTracer.TraceInformation<DateTime, int, int>(0, 0L, "NavBarCache::CleanUpIfNeeded() Time: {0}, cleaned: {1}, left: {2}.", NavBarCache.lastCleanUpTime, list.Count, NavBarCache.Cache.Count);
			}
		}

		// Token: 0x04002046 RID: 8262
		private static int navBarCacheCapacity = 8192;

		// Token: 0x04002047 RID: 8263
		private static int navBarCacheWarningSize = (int)((double)NavBarCache.navBarCacheCapacity * 0.7);

		// Token: 0x04002048 RID: 8264
		private static TimeSpan minCacheLife = new TimeSpan(4, 0, 0);

		// Token: 0x04002049 RID: 8265
		private static TimeSpan minCleanUpInterval = new TimeSpan(1, 0, 0);

		// Token: 0x0400204A RID: 8266
		private static DateTime lastCleanUpTime = DateTime.MinValue;

		// Token: 0x0400204B RID: 8267
		private static Dictionary<string, NavBarPack> navBarCache;
	}
}
