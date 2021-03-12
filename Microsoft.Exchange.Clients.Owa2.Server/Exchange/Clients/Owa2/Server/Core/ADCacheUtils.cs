using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000AD RID: 173
	internal static class ADCacheUtils
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00014BF8 File Offset: 0x00012DF8
		private static TenantConfigurationCache<OwaPerTenantTransportSettings> OwaPerTenantTransportSettingsCache
		{
			get
			{
				if (ADCacheUtils.owasPerTenantTransportSettingsCache == null)
				{
					lock (ADCacheUtils.lockObjectTransportSettings)
					{
						if (ADCacheUtils.owasPerTenantTransportSettingsCache == null)
						{
							ADCacheUtils.owasPerTenantTransportSettingsCache = new TenantConfigurationCache<OwaPerTenantTransportSettings>(ADCacheUtils.owaTransportSettingsCacheSize, ADCacheUtils.owaTransportSettingsCacheExpirationInterval, ADCacheUtils.owaTransportSettingsCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.OwaPerTenantCacheTracer, "OwaPerTenantTransportSettings"), new PerTenantCachePerformanceCounters("OwaPerTenantTransportSettings"));
						}
					}
				}
				return ADCacheUtils.owasPerTenantTransportSettingsCache;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00014C78 File Offset: 0x00012E78
		private static TenantConfigurationCache<OwaPerTenantAcceptedDomains> OwaPerTenantAcceptedDomainsCache
		{
			get
			{
				if (ADCacheUtils.owasPerTenantAcceptedDomainsCache == null)
				{
					lock (ADCacheUtils.lockObjectAcceptedDomains)
					{
						if (ADCacheUtils.owasPerTenantAcceptedDomainsCache == null)
						{
							ADCacheUtils.owasPerTenantAcceptedDomainsCache = new TenantConfigurationCache<OwaPerTenantAcceptedDomains>(ADCacheUtils.owaAcceptedDomainsCacheSize, ADCacheUtils.owaRemoteDomainsCacheExpirationInterval, ADCacheUtils.owaRemoteDomainsCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.OwaPerTenantCacheTracer, "OwaPerTenantAcceptedDomains"), new PerTenantCachePerformanceCounters("OwaPerTenantAcceptedDomains"));
						}
					}
				}
				return ADCacheUtils.owasPerTenantAcceptedDomainsCache;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00014CF8 File Offset: 0x00012EF8
		private static TenantConfigurationCache<OwaPerTenantRemoteDomains> OwaPerTenantRemoteDomainsCache
		{
			get
			{
				if (ADCacheUtils.owasPerTenantRemoteDomainsCache == null)
				{
					lock (ADCacheUtils.lockObjectRemoteDomains)
					{
						if (ADCacheUtils.owasPerTenantRemoteDomainsCache == null)
						{
							ADCacheUtils.owasPerTenantRemoteDomainsCache = new TenantConfigurationCache<OwaPerTenantRemoteDomains>(ADCacheUtils.owaRemoteDomainsCacheSize, ADCacheUtils.owaRemoteDomainsCacheExpirationInterval, ADCacheUtils.owaRemoteDomainsCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.OwaPerTenantCacheTracer, "OwaPerTenantRemoteDomains"), new PerTenantCachePerformanceCounters("OwaPerTenantRemoteDomains"));
						}
					}
				}
				return ADCacheUtils.owasPerTenantRemoteDomainsCache;
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00014D78 File Offset: 0x00012F78
		public static OwaPerTenantTransportSettings GetOwaPerTenantTransportSettings(OrganizationId organizationId)
		{
			return ADCacheUtils.OwaPerTenantTransportSettingsCache.GetValue(organizationId);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00014D85 File Offset: 0x00012F85
		public static OwaPerTenantAcceptedDomains GetOwaPerTenantAcceptedDomains(OrganizationId organizationId)
		{
			return ADCacheUtils.OwaPerTenantAcceptedDomainsCache.GetValue(organizationId);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00014D92 File Offset: 0x00012F92
		public static OwaPerTenantRemoteDomains GetOwaPerTenantRemoteDomains(OrganizationId organizationId)
		{
			return ADCacheUtils.OwaPerTenantRemoteDomainsCache.GetValue(organizationId);
		}

		// Token: 0x040003B6 RID: 950
		private const string PerTenantCacheTransportSettingsName = "OwaPerTenantTransportSettings";

		// Token: 0x040003B7 RID: 951
		private const string PerTenantCacheAcceptedDomainsName = "OwaPerTenantAcceptedDomains";

		// Token: 0x040003B8 RID: 952
		private const string PerTenantCacheRemoteDomainsName = "OwaPerTenantRemoteDomains";

		// Token: 0x040003B9 RID: 953
		private static long owaTransportSettingsCacheSize = BaseApplication.GetAppSetting<long>("OwaTransportSettingsCacheSize", 20L) * 1024L * 1024L;

		// Token: 0x040003BA RID: 954
		private static TimeSpan owaTransportSettingsCacheExpirationInterval = TimeSpan.FromMinutes((double)BaseApplication.GetAppSetting<int>("OwaTransportSettingsCacheExpirationInterval", 60));

		// Token: 0x040003BB RID: 955
		private static TimeSpan owaTransportSettingsCacheCleanupInterval = TimeSpan.FromMinutes((double)BaseApplication.GetAppSetting<int>("OwaTransportSettingsCacheCleanupInterval", 60));

		// Token: 0x040003BC RID: 956
		private static long owaAcceptedDomainsCacheSize = BaseApplication.GetAppSetting<long>("OwaAcceptedDomainsCacheSize", 20L) * 1024L * 1024L;

		// Token: 0x040003BD RID: 957
		private static TimeSpan owaAcceptedDomainsCacheExpirationInterval = TimeSpan.FromMinutes((double)BaseApplication.GetAppSetting<int>("OwaAcceptedDomainsCacheExpirationInterval", 60));

		// Token: 0x040003BE RID: 958
		private static TimeSpan owaAcceptedDomainsCacheCleanupInterval = TimeSpan.FromMinutes((double)BaseApplication.GetAppSetting<int>("OwaAcceptedDomainsCacheCleanupInterval", 60));

		// Token: 0x040003BF RID: 959
		private static long owaRemoteDomainsCacheSize = BaseApplication.GetAppSetting<long>("OwaRemoteDomainsCacheSize", 20L) * 1024L * 1024L;

		// Token: 0x040003C0 RID: 960
		private static TimeSpan owaRemoteDomainsCacheExpirationInterval = TimeSpan.FromMinutes((double)BaseApplication.GetAppSetting<int>("OwaRemoteDomainsCacheExpirationInterval", 60));

		// Token: 0x040003C1 RID: 961
		private static TimeSpan owaRemoteDomainsCacheCleanupInterval = TimeSpan.FromMinutes((double)BaseApplication.GetAppSetting<int>("OwaRemoteDomainsCacheCleanupInterval", 60));

		// Token: 0x040003C2 RID: 962
		private static TenantConfigurationCache<OwaPerTenantTransportSettings> owasPerTenantTransportSettingsCache;

		// Token: 0x040003C3 RID: 963
		private static TenantConfigurationCache<OwaPerTenantAcceptedDomains> owasPerTenantAcceptedDomainsCache;

		// Token: 0x040003C4 RID: 964
		private static TenantConfigurationCache<OwaPerTenantRemoteDomains> owasPerTenantRemoteDomainsCache;

		// Token: 0x040003C5 RID: 965
		private static readonly List<PropertyDefinition> PropertiesToGet = new List<PropertyDefinition>
		{
			ADRecipientSchema.EmailAddresses
		};

		// Token: 0x040003C6 RID: 966
		private static object lockObjectTransportSettings = new object();

		// Token: 0x040003C7 RID: 967
		private static object lockObjectAcceptedDomains = new object();

		// Token: 0x040003C8 RID: 968
		private static object lockObjectRemoteDomains = new object();
	}
}
