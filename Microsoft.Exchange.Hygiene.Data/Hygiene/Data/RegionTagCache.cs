using System;
using System.Configuration;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200009C RID: 156
	internal class RegionTagCache
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x000118C4 File Offset: 0x0000FAC4
		private RegionTagCache()
		{
			this.cache = new MemoryCache("RegionTag", null);
			RegionTagCache.RegionTagCacheConfiguration instance = RegionTagCache.RegionTagCacheConfiguration.GetInstance();
			this.goodTenantCachePolicy = new CacheItemPolicy();
			this.goodTenantEntryTTL = instance.TenantWithRegionTagEntryTTL;
			this.badTenantCachePolicy = new CacheItemPolicy();
			this.badTenantEntryTTL = instance.TenantWithoutRegionTagEntryTTL;
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001191C File Offset: 0x0000FB1C
		public long Count
		{
			get
			{
				return this.cache.GetCount(null);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0001192A File Offset: 0x0000FB2A
		public long CacheMemoryLimit
		{
			get
			{
				return this.cache.CacheMemoryLimit;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00011937 File Offset: 0x0000FB37
		public long PhysicalMemoryLimit
		{
			get
			{
				return this.cache.PhysicalMemoryLimit;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00011944 File Offset: 0x0000FB44
		public static RegionTagCache GetInstance()
		{
			if (RegionTagCache.singleton == null)
			{
				lock (RegionTagCache.singletonLock)
				{
					if (RegionTagCache.singleton == null)
					{
						RegionTagCache regionTagCache = new RegionTagCache();
						Thread.MemoryBarrier();
						RegionTagCache.singleton = regionTagCache;
					}
				}
			}
			return RegionTagCache.singleton;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000119A4 File Offset: 0x0000FBA4
		public void AddGoodTenant(Guid tenantId, string regionTag)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("regionTag", regionTag);
			this.goodTenantCachePolicy.AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.Add(this.goodTenantEntryTTL));
			this.cache.Set(tenantId.ToString(), regionTag, this.goodTenantCachePolicy, null);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00011A00 File Offset: 0x0000FC00
		public void AddBadTenant(Guid tenantId)
		{
			this.badTenantCachePolicy.AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.Add(this.badTenantEntryTTL));
			this.cache.Set(tenantId.ToString(), string.Empty, this.badTenantCachePolicy, null);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00011A54 File Offset: 0x0000FC54
		public string Get(Guid tenantId)
		{
			string key = tenantId.ToString();
			if (this.cache.Contains(key, null))
			{
				return (string)this.cache[key];
			}
			return null;
		}

		// Token: 0x0400034F RID: 847
		private const string Name = "RegionTag";

		// Token: 0x04000350 RID: 848
		private static RegionTagCache singleton;

		// Token: 0x04000351 RID: 849
		private static object singletonLock = new object();

		// Token: 0x04000352 RID: 850
		private readonly TimeSpan goodTenantEntryTTL;

		// Token: 0x04000353 RID: 851
		private readonly TimeSpan badTenantEntryTTL;

		// Token: 0x04000354 RID: 852
		private MemoryCache cache;

		// Token: 0x04000355 RID: 853
		private CacheItemPolicy goodTenantCachePolicy;

		// Token: 0x04000356 RID: 854
		private CacheItemPolicy badTenantCachePolicy;

		// Token: 0x0200009D RID: 157
		private class RegionTagCacheConfiguration : ConfigurationSection
		{
			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x0600054A RID: 1354 RVA: 0x00011A9D File Offset: 0x0000FC9D
			// (set) Token: 0x0600054B RID: 1355 RVA: 0x00011AAF File Offset: 0x0000FCAF
			[ConfigurationProperty("TenantWithRegionTagEntryTTL", IsRequired = false, DefaultValue = "1.00:00:00")]
			public TimeSpan TenantWithRegionTagEntryTTL
			{
				get
				{
					return (TimeSpan)base["TenantWithRegionTagEntryTTL"];
				}
				internal set
				{
					base["TenantWithRegionTagEntryTTL"] = value;
				}
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x0600054C RID: 1356 RVA: 0x00011AC2 File Offset: 0x0000FCC2
			// (set) Token: 0x0600054D RID: 1357 RVA: 0x00011AD4 File Offset: 0x0000FCD4
			[ConfigurationProperty("TenantWithoutRegionTagEntryTTL", IsRequired = false, DefaultValue = "00:15:00")]
			public TimeSpan TenantWithoutRegionTagEntryTTL
			{
				get
				{
					return (TimeSpan)base["TenantWithoutRegionTagEntryTTL"];
				}
				internal set
				{
					base["TenantWithoutRegionTagEntryTTL"] = value;
				}
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x00011AE8 File Offset: 0x0000FCE8
			public static RegionTagCache.RegionTagCacheConfiguration GetInstance()
			{
				if (RegionTagCache.RegionTagCacheConfiguration.instance == null)
				{
					RegionTagCache.RegionTagCacheConfiguration.instance = (RegionTagCache.RegionTagCacheConfiguration)ConfigurationManager.GetSection("RegionTagCache");
					if (RegionTagCache.RegionTagCacheConfiguration.instance == null)
					{
						string exeConfigFilename = Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path) + ".config";
						ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
						{
							ExeConfigFilename = exeConfigFilename
						};
						Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
						RegionTagCache.RegionTagCacheConfiguration.instance = (RegionTagCache.RegionTagCacheConfiguration)configuration.GetSection("RegionTagCache");
					}
					if (RegionTagCache.RegionTagCacheConfiguration.instance == null)
					{
						RegionTagCache.RegionTagCacheConfiguration.instance = new RegionTagCache.RegionTagCacheConfiguration();
					}
				}
				return RegionTagCache.RegionTagCacheConfiguration.instance;
			}

			// Token: 0x04000357 RID: 855
			private const string TenantWithRegionTagEntryExpirationParam = "TenantWithRegionTagEntryTTL";

			// Token: 0x04000358 RID: 856
			private const string TenantWithoutRegionTagEntryExpirationParam = "TenantWithoutRegionTagEntryTTL";

			// Token: 0x04000359 RID: 857
			private const string SectionName = "RegionTagCache";

			// Token: 0x0400035A RID: 858
			private static RegionTagCache.RegionTagCacheConfiguration instance;
		}
	}
}
