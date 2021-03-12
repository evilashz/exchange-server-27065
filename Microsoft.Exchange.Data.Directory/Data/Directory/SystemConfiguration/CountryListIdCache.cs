using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory.EventLog;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000659 RID: 1625
	internal sealed class CountryListIdCache : LazyLookupTimeoutCache<CountryListKey, CountryList>
	{
		// Token: 0x06004C16 RID: 19478 RVA: 0x0011907A File Offset: 0x0011727A
		private CountryListIdCache() : base(1, 10, false, CacheTimeToLive.GlobalCountryListCacheTimeToLive)
		{
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x06004C17 RID: 19479 RVA: 0x0011908B File Offset: 0x0011728B
		public static CountryListIdCache Singleton
		{
			get
			{
				return CountryListIdCache.singleton;
			}
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x00119094 File Offset: 0x00117294
		protected override CountryList CreateOnCacheMiss(CountryListKey key, ref bool shouldAdd)
		{
			CountryList countryList = null;
			if (null != key)
			{
				ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, OrganizationId.ForestWideOrgId, OrganizationId.ForestWideOrgId, false);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, sessionSettings, 145, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\CountryListIdCache.cs");
				countryList = tenantOrTopologyConfigurationSession.Read<CountryList>(key.Key);
				if (countryList == null)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_UMCountryListNotFound, key.ToString(), new object[]
					{
						key
					});
				}
			}
			return countryList;
		}

		// Token: 0x04003434 RID: 13364
		private static CountryListIdCache singleton = new CountryListIdCache();
	}
}
