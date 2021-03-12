using System;
using System.Web;
using System.Web.Caching;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006DF RID: 1759
	internal static class BadExchangePrincipalCache
	{
		// Token: 0x060035F5 RID: 13813 RVA: 0x000C1B34 File Offset: 0x000BFD34
		public static void Add(string key)
		{
			if (key.Length > 1000)
			{
				return;
			}
			if (!BadExchangePrincipalCache.Contains(key))
			{
				Cache cache = HttpRuntime.Cache;
				cache.Add(BadExchangePrincipalCache.BuildKey(key), key, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5.0), CacheItemPriority.Low, null);
			}
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000C1B84 File Offset: 0x000BFD84
		public static bool Contains(string key)
		{
			Cache cache = HttpRuntime.Cache;
			return cache.Get(BadExchangePrincipalCache.BuildKey(key)) != null;
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000C1BA9 File Offset: 0x000BFDA9
		private static string BuildKey(string key)
		{
			return "_BPKP_" + key;
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000C1BB6 File Offset: 0x000BFDB6
		internal static string BuildKey(string key, string orgPrefix)
		{
			return orgPrefix + key;
		}

		// Token: 0x04001E26 RID: 7718
		public const int MaxEntryLength = 1000;

		// Token: 0x04001E27 RID: 7719
		private const string BadPrincipalKeyPrefix = "_BPKP_";

		// Token: 0x04001E28 RID: 7720
		private const int CacheTimeoutInMinutes = 5;
	}
}
