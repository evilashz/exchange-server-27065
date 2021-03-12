using System;
using System.Runtime.Caching;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x02000099 RID: 153
	internal interface ICacheConfiguration
	{
		// Token: 0x060008BE RID: 2238
		bool IsCacheEnabled(string processNameOrProcessAppName);

		// Token: 0x060008BF RID: 2239
		CacheMode GetCacheMode(string processNameOrProcessAppName);

		// Token: 0x060008C0 RID: 2240
		CacheMode GetCacheModeForCurrentProcess();

		// Token: 0x060008C1 RID: 2241
		bool IsCacheEnableForCurrentProcess();

		// Token: 0x060008C2 RID: 2242
		bool IsCacheEnabled(Type type);

		// Token: 0x060008C3 RID: 2243
		bool IsCacheEnabledForInsertOnSave(ADRawEntry rawEntry);

		// Token: 0x060008C4 RID: 2244
		int GetCacheExpirationForObject(ADRawEntry rawEntry);

		// Token: 0x060008C5 RID: 2245
		CacheItemPriority GetCachePriorityForObject(ADRawEntry rawEntry);
	}
}
