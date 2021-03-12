using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x020000FC RID: 252
	public sealed class VariantConfigurationCafeComponent : VariantConfigurationComponent
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x000197E8 File Offset: 0x000179E8
		internal VariantConfigurationCafeComponent() : base("Cafe")
		{
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "CheckServerOnlineForActiveServer", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "ExplicitDomain", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "UseExternalPopIMapSettings", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "NoServiceTopologyTryGetServerVersion", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "NoFormBasedAuthentication", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "RUMUseADCache", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "PartitionedRouting", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "DownLevelServerPing", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "UseResourceForest", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "TrustClientXForwardedFor", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "MailboxServerSharedCache", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "LoadBalancedPartnerRouting", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "CompositeIdentity", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "CafeV2", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "RetryOnError", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "PreferServersCacheForRandomBackEnd", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "AnchorMailboxSharedCache", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "CafeV1RUM", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "DebugResponseHeaders", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "SyndicatedAdmin", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "EnableTls11", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "ConfigurePerformanceCounters", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "EnableTls12", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "ServersCache", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "NoCrossForestServerLocate", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "SiteNameFromServerFqdnTranslation", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "CacheLocalSiteLiveE15Servers", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "EnforceConcurrencyGuards", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "NoVDirLocationHint", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "NoCrossSiteRedirect", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "CheckServerLocatorServersForMaintenanceMode", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "UseExchClientVerInRPS", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Cafe.settings.ini", "RUMLegacyRoutingEntry", typeof(IFeature), false));
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00019C20 File Offset: 0x00017E20
		public VariantConfigurationSection CheckServerOnlineForActiveServer
		{
			get
			{
				return base["CheckServerOnlineForActiveServer"];
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00019C2D File Offset: 0x00017E2D
		public VariantConfigurationSection ExplicitDomain
		{
			get
			{
				return base["ExplicitDomain"];
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00019C3A File Offset: 0x00017E3A
		public VariantConfigurationSection UseExternalPopIMapSettings
		{
			get
			{
				return base["UseExternalPopIMapSettings"];
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00019C47 File Offset: 0x00017E47
		public VariantConfigurationSection NoServiceTopologyTryGetServerVersion
		{
			get
			{
				return base["NoServiceTopologyTryGetServerVersion"];
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00019C54 File Offset: 0x00017E54
		public VariantConfigurationSection NoFormBasedAuthentication
		{
			get
			{
				return base["NoFormBasedAuthentication"];
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00019C61 File Offset: 0x00017E61
		public VariantConfigurationSection RUMUseADCache
		{
			get
			{
				return base["RUMUseADCache"];
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00019C6E File Offset: 0x00017E6E
		public VariantConfigurationSection PartitionedRouting
		{
			get
			{
				return base["PartitionedRouting"];
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00019C7B File Offset: 0x00017E7B
		public VariantConfigurationSection DownLevelServerPing
		{
			get
			{
				return base["DownLevelServerPing"];
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00019C88 File Offset: 0x00017E88
		public VariantConfigurationSection UseResourceForest
		{
			get
			{
				return base["UseResourceForest"];
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00019C95 File Offset: 0x00017E95
		public VariantConfigurationSection TrustClientXForwardedFor
		{
			get
			{
				return base["TrustClientXForwardedFor"];
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00019CA2 File Offset: 0x00017EA2
		public VariantConfigurationSection MailboxServerSharedCache
		{
			get
			{
				return base["MailboxServerSharedCache"];
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00019CAF File Offset: 0x00017EAF
		public VariantConfigurationSection LoadBalancedPartnerRouting
		{
			get
			{
				return base["LoadBalancedPartnerRouting"];
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00019CBC File Offset: 0x00017EBC
		public VariantConfigurationSection CompositeIdentity
		{
			get
			{
				return base["CompositeIdentity"];
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00019CC9 File Offset: 0x00017EC9
		public VariantConfigurationSection CafeV2
		{
			get
			{
				return base["CafeV2"];
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00019CD6 File Offset: 0x00017ED6
		public VariantConfigurationSection RetryOnError
		{
			get
			{
				return base["RetryOnError"];
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00019CE3 File Offset: 0x00017EE3
		public VariantConfigurationSection PreferServersCacheForRandomBackEnd
		{
			get
			{
				return base["PreferServersCacheForRandomBackEnd"];
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00019CF0 File Offset: 0x00017EF0
		public VariantConfigurationSection AnchorMailboxSharedCache
		{
			get
			{
				return base["AnchorMailboxSharedCache"];
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00019CFD File Offset: 0x00017EFD
		public VariantConfigurationSection CafeV1RUM
		{
			get
			{
				return base["CafeV1RUM"];
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00019D0A File Offset: 0x00017F0A
		public VariantConfigurationSection DebugResponseHeaders
		{
			get
			{
				return base["DebugResponseHeaders"];
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00019D17 File Offset: 0x00017F17
		public VariantConfigurationSection SyndicatedAdmin
		{
			get
			{
				return base["SyndicatedAdmin"];
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00019D24 File Offset: 0x00017F24
		public VariantConfigurationSection EnableTls11
		{
			get
			{
				return base["EnableTls11"];
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00019D31 File Offset: 0x00017F31
		public VariantConfigurationSection ConfigurePerformanceCounters
		{
			get
			{
				return base["ConfigurePerformanceCounters"];
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00019D3E File Offset: 0x00017F3E
		public VariantConfigurationSection EnableTls12
		{
			get
			{
				return base["EnableTls12"];
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00019D4B File Offset: 0x00017F4B
		public VariantConfigurationSection ServersCache
		{
			get
			{
				return base["ServersCache"];
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00019D58 File Offset: 0x00017F58
		public VariantConfigurationSection NoCrossForestServerLocate
		{
			get
			{
				return base["NoCrossForestServerLocate"];
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00019D65 File Offset: 0x00017F65
		public VariantConfigurationSection SiteNameFromServerFqdnTranslation
		{
			get
			{
				return base["SiteNameFromServerFqdnTranslation"];
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00019D72 File Offset: 0x00017F72
		public VariantConfigurationSection CacheLocalSiteLiveE15Servers
		{
			get
			{
				return base["CacheLocalSiteLiveE15Servers"];
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00019D7F File Offset: 0x00017F7F
		public VariantConfigurationSection EnforceConcurrencyGuards
		{
			get
			{
				return base["EnforceConcurrencyGuards"];
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00019D8C File Offset: 0x00017F8C
		public VariantConfigurationSection NoVDirLocationHint
		{
			get
			{
				return base["NoVDirLocationHint"];
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00019D99 File Offset: 0x00017F99
		public VariantConfigurationSection NoCrossSiteRedirect
		{
			get
			{
				return base["NoCrossSiteRedirect"];
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00019DA6 File Offset: 0x00017FA6
		public VariantConfigurationSection CheckServerLocatorServersForMaintenanceMode
		{
			get
			{
				return base["CheckServerLocatorServersForMaintenanceMode"];
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00019DB3 File Offset: 0x00017FB3
		public VariantConfigurationSection UseExchClientVerInRPS
		{
			get
			{
				return base["UseExchClientVerInRPS"];
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00019DC0 File Offset: 0x00017FC0
		public VariantConfigurationSection RUMLegacyRoutingEntry
		{
			get
			{
				return base["RUMLegacyRoutingEntry"];
			}
		}
	}
}
