using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000639 RID: 1593
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OrganizationIdCache : LazyLookupTimeoutCache<OrganizationId, OrganizationIdCacheValue>
	{
		// Token: 0x06004B44 RID: 19268 RVA: 0x001158C5 File Offset: 0x00113AC5
		private OrganizationIdCache() : base(1, 1000, false, CacheTimeToLive.FederatedCacheTimeToLive)
		{
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x001158D9 File Offset: 0x00113AD9
		public static OrganizationIdCache Singleton
		{
			get
			{
				return OrganizationIdCache.singleton;
			}
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x001158E0 File Offset: 0x00113AE0
		protected override OrganizationIdCacheValue CreateOnCacheMiss(OrganizationId key, ref bool shouldAdd)
		{
			return new OrganizationIdCacheValue(key);
		}

		// Token: 0x040033B7 RID: 13239
		private static OrganizationIdCache singleton = new OrganizationIdCache();
	}
}
