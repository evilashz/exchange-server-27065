using System;
using Microsoft.Exchange.Flighting;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000103 RID: 259
	public sealed class VariantConfigurationDiscoveryComponent : VariantConfigurationComponent
	{
		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001BDD4 File Offset: 0x00019FD4
		internal VariantConfigurationDiscoveryComponent() : base("Discovery")
		{
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryServerLookupConcurrency", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryMaxAllowedExecutorItems", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryKeywordsBatchSize", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryExecutesInParallel", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "UrlRebind", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryDisplaySearchPageSize", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryLocalSearchConcurrency", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "SearchTimeout", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "ServiceTopologyTimeout", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryDisplaySearchBatchSize", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryDefaultPageSize", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryServerLookupBatch", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryMaxAllowedResultsPageSize", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "SearchScale", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "MailboxServerLocatorTimeout", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryADPageSize", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryMailboxMaxProhibitSendReceiveQuota", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryFanoutConcurrency", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryExcludedFolders", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryUseFastSearch", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryFanoutBatch", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryLocalSearchIsParallel", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryAggregateLogs", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryMailboxMaxProhibitSendQuota", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryMaxAllowedMailboxQueriesPerRequest", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryMaxMailboxes", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryADLookupConcurrency", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("Discovery.settings.ini", "DiscoveryExcludedFoldersEnabled", typeof(ISettingsValue), false));
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0001C16C File Offset: 0x0001A36C
		public VariantConfigurationSection DiscoveryServerLookupConcurrency
		{
			get
			{
				return base["DiscoveryServerLookupConcurrency"];
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0001C179 File Offset: 0x0001A379
		public VariantConfigurationSection DiscoveryMaxAllowedExecutorItems
		{
			get
			{
				return base["DiscoveryMaxAllowedExecutorItems"];
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0001C186 File Offset: 0x0001A386
		public VariantConfigurationSection DiscoveryKeywordsBatchSize
		{
			get
			{
				return base["DiscoveryKeywordsBatchSize"];
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0001C193 File Offset: 0x0001A393
		public VariantConfigurationSection DiscoveryExecutesInParallel
		{
			get
			{
				return base["DiscoveryExecutesInParallel"];
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0001C1A0 File Offset: 0x0001A3A0
		public VariantConfigurationSection UrlRebind
		{
			get
			{
				return base["UrlRebind"];
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0001C1AD File Offset: 0x0001A3AD
		public VariantConfigurationSection DiscoveryDisplaySearchPageSize
		{
			get
			{
				return base["DiscoveryDisplaySearchPageSize"];
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0001C1BA File Offset: 0x0001A3BA
		public VariantConfigurationSection DiscoveryLocalSearchConcurrency
		{
			get
			{
				return base["DiscoveryLocalSearchConcurrency"];
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0001C1C7 File Offset: 0x0001A3C7
		public VariantConfigurationSection SearchTimeout
		{
			get
			{
				return base["SearchTimeout"];
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
		public VariantConfigurationSection ServiceTopologyTimeout
		{
			get
			{
				return base["ServiceTopologyTimeout"];
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0001C1E1 File Offset: 0x0001A3E1
		public VariantConfigurationSection DiscoveryDisplaySearchBatchSize
		{
			get
			{
				return base["DiscoveryDisplaySearchBatchSize"];
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0001C1EE File Offset: 0x0001A3EE
		public VariantConfigurationSection DiscoveryDefaultPageSize
		{
			get
			{
				return base["DiscoveryDefaultPageSize"];
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0001C1FB File Offset: 0x0001A3FB
		public VariantConfigurationSection DiscoveryServerLookupBatch
		{
			get
			{
				return base["DiscoveryServerLookupBatch"];
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0001C208 File Offset: 0x0001A408
		public VariantConfigurationSection DiscoveryMaxAllowedResultsPageSize
		{
			get
			{
				return base["DiscoveryMaxAllowedResultsPageSize"];
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0001C215 File Offset: 0x0001A415
		public VariantConfigurationSection SearchScale
		{
			get
			{
				return base["SearchScale"];
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0001C222 File Offset: 0x0001A422
		public VariantConfigurationSection MailboxServerLocatorTimeout
		{
			get
			{
				return base["MailboxServerLocatorTimeout"];
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0001C22F File Offset: 0x0001A42F
		public VariantConfigurationSection DiscoveryADPageSize
		{
			get
			{
				return base["DiscoveryADPageSize"];
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0001C23C File Offset: 0x0001A43C
		public VariantConfigurationSection DiscoveryMailboxMaxProhibitSendReceiveQuota
		{
			get
			{
				return base["DiscoveryMailboxMaxProhibitSendReceiveQuota"];
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0001C249 File Offset: 0x0001A449
		public VariantConfigurationSection DiscoveryFanoutConcurrency
		{
			get
			{
				return base["DiscoveryFanoutConcurrency"];
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0001C256 File Offset: 0x0001A456
		public VariantConfigurationSection DiscoveryExcludedFolders
		{
			get
			{
				return base["DiscoveryExcludedFolders"];
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0001C263 File Offset: 0x0001A463
		public VariantConfigurationSection DiscoveryUseFastSearch
		{
			get
			{
				return base["DiscoveryUseFastSearch"];
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0001C270 File Offset: 0x0001A470
		public VariantConfigurationSection DiscoveryFanoutBatch
		{
			get
			{
				return base["DiscoveryFanoutBatch"];
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0001C27D File Offset: 0x0001A47D
		public VariantConfigurationSection DiscoveryLocalSearchIsParallel
		{
			get
			{
				return base["DiscoveryLocalSearchIsParallel"];
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0001C28A File Offset: 0x0001A48A
		public VariantConfigurationSection DiscoveryAggregateLogs
		{
			get
			{
				return base["DiscoveryAggregateLogs"];
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0001C297 File Offset: 0x0001A497
		public VariantConfigurationSection DiscoveryMailboxMaxProhibitSendQuota
		{
			get
			{
				return base["DiscoveryMailboxMaxProhibitSendQuota"];
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0001C2A4 File Offset: 0x0001A4A4
		public VariantConfigurationSection DiscoveryMaxAllowedMailboxQueriesPerRequest
		{
			get
			{
				return base["DiscoveryMaxAllowedMailboxQueriesPerRequest"];
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0001C2B1 File Offset: 0x0001A4B1
		public VariantConfigurationSection DiscoveryMaxMailboxes
		{
			get
			{
				return base["DiscoveryMaxMailboxes"];
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0001C2BE File Offset: 0x0001A4BE
		public VariantConfigurationSection DiscoveryADLookupConcurrency
		{
			get
			{
				return base["DiscoveryADLookupConcurrency"];
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0001C2CB File Offset: 0x0001A4CB
		public VariantConfigurationSection DiscoveryExcludedFoldersEnabled
		{
			get
			{
				return base["DiscoveryExcludedFoldersEnabled"];
			}
		}
	}
}
