using System;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000299 RID: 665
	internal class AdDriverConfigSchema : ConfigSchemaBase
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x0008ADE4 File Offset: 0x00088FE4
		public override string Name
		{
			get
			{
				return "AdDriver";
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x0008ADEB File Offset: 0x00088FEB
		public override string SectionName
		{
			get
			{
				return "AdDriverConfiguration";
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x0008ADF2 File Offset: 0x00088FF2
		protected override ExchangeConfigurationSection ScopeSchema
		{
			get
			{
				return AdDriverConfigSchema.scopeSchema;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0008ADF9 File Offset: 0x00088FF9
		// (set) Token: 0x06001F12 RID: 7954 RVA: 0x0008AE0B File Offset: 0x0008900B
		[ConfigurationProperty("IsSoftLinkResolutionEnabledForAllProcesses", DefaultValue = true)]
		public bool IsSoftLinkResolutionEnabledForAllProcesses
		{
			get
			{
				return (bool)base["IsSoftLinkResolutionEnabledForAllProcesses"];
			}
			set
			{
				base["IsSoftLinkResolutionEnabledForAllProcesses"] = value;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x0008AE1E File Offset: 0x0008901E
		// (set) Token: 0x06001F14 RID: 7956 RVA: 0x0008AE30 File Offset: 0x00089030
		[ConfigurationProperty("IsSoftLinkResolutionCacheEnabled", DefaultValue = true)]
		public bool IsSoftLinkResolutionCacheEnabled
		{
			get
			{
				return (bool)base["IsSoftLinkResolutionCacheEnabled"];
			}
			set
			{
				base["IsSoftLinkResolutionCacheEnabled"] = value;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x0008AE43 File Offset: 0x00089043
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x0008AE55 File Offset: 0x00089055
		[ConfigurationProperty("SoftLinkFormatVersion", DefaultValue = 1)]
		public int SoftLinkFormatVersion
		{
			get
			{
				return (int)base["SoftLinkFormatVersion"];
			}
			set
			{
				base["SoftLinkFormatVersion"] = value;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x0008AE68 File Offset: 0x00089068
		// (set) Token: 0x06001F18 RID: 7960 RVA: 0x0008AE7A File Offset: 0x0008907A
		[ConfigurationProperty("SoftLinkFilterVersion2Enabled", DefaultValue = true)]
		public bool SoftLinkFilterVersion2Enabled
		{
			get
			{
				return (bool)base["SoftLinkFilterVersion2Enabled"];
			}
			set
			{
				base["SoftLinkFilterVersion2Enabled"] = value;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0008AE8D File Offset: 0x0008908D
		// (set) Token: 0x06001F1A RID: 7962 RVA: 0x0008AE9F File Offset: 0x0008909F
		[ConfigurationProperty("GlsCacheServiceMode", DefaultValue = GlsCacheServiceMode.CacheDisabled)]
		public GlsCacheServiceMode GlsCacheServiceMode
		{
			get
			{
				return (GlsCacheServiceMode)base["GlsCacheServiceMode"];
			}
			set
			{
				base["GlsCacheServiceMode"] = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x0008AEB2 File Offset: 0x000890B2
		// (set) Token: 0x06001F1C RID: 7964 RVA: 0x0008AEC4 File Offset: 0x000890C4
		[ConfigurationProperty("OverrideGlsCacheLoadType", DefaultValue = false)]
		public bool OverrideGlsCacheLoadType
		{
			get
			{
				return (bool)base["OverrideGlsCacheLoadType"];
			}
			set
			{
				base["OverrideGlsCacheLoadType"] = value;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x0008AED7 File Offset: 0x000890D7
		// (set) Token: 0x06001F1E RID: 7966 RVA: 0x0008AEE9 File Offset: 0x000890E9
		[ConfigurationProperty("OfflineDataCacheExpirationTimeInMinutes", DefaultValue = 5)]
		public int OfflineDataCacheExpirationTimeInMinutes
		{
			get
			{
				return (int)base["OfflineDataCacheExpirationTimeInMinutes"];
			}
			set
			{
				base["OfflineDataCacheExpirationTimeInMinutes"] = value;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x0008AEFC File Offset: 0x000890FC
		// (set) Token: 0x06001F20 RID: 7968 RVA: 0x0008AF0E File Offset: 0x0008910E
		[TypeConverter(typeof(GlsOverrideCollectionConverter))]
		[ConfigurationProperty("GlsTenantOverrides", DefaultValue = null)]
		public GlsOverrideCollection GlsTenantOverrides
		{
			get
			{
				return (GlsOverrideCollection)base["GlsTenantOverrides"];
			}
			set
			{
				base["GlsTenantOverrides"] = value;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x0008AF1C File Offset: 0x0008911C
		// (set) Token: 0x06001F22 RID: 7970 RVA: 0x0008AF2E File Offset: 0x0008912E
		[ConfigurationProperty("DelayForADWriteThrottlingInMsec", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 3000, ExcludeRange = false)]
		public int DelayForADWriteThrottlingInMsec
		{
			get
			{
				return (int)base["DelayForADWriteThrottlingInMsec"];
			}
			set
			{
				base["DelayForADWriteThrottlingInMsec"] = value;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x0008AF41 File Offset: 0x00089141
		// (set) Token: 0x06001F24 RID: 7972 RVA: 0x0008AF53 File Offset: 0x00089153
		[ConfigurationProperty("IsADWriteDisabled", DefaultValue = false)]
		public bool IsADWriteDisabled
		{
			get
			{
				return (bool)base["IsADWriteDisabled"];
			}
			set
			{
				base["IsADWriteDisabled"] = value;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x0008AF66 File Offset: 0x00089166
		// (set) Token: 0x06001F26 RID: 7974 RVA: 0x0008AF78 File Offset: 0x00089178
		[ConfigurationProperty("MsoEndpointType", DefaultValue = MsoEndpointType.OLD)]
		public MsoEndpointType MsoEndpointType
		{
			get
			{
				return (MsoEndpointType)base["MsoEndpointType"];
			}
			set
			{
				base["MsoEndpointType"] = value;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x0008AF8B File Offset: 0x0008918B
		// (set) Token: 0x06001F28 RID: 7976 RVA: 0x0008AF9D File Offset: 0x0008919D
		[ConfigurationProperty("AccountValidationEnabled", DefaultValue = false)]
		public bool AccountValidationEnabled
		{
			get
			{
				return (bool)base["AccountValidationEnabled"];
			}
			set
			{
				base["AccountValidationEnabled"] = value;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0008AFB0 File Offset: 0x000891B0
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x0008AFC2 File Offset: 0x000891C2
		[ConfigurationProperty("MservEndpoint", DefaultValue = "10.1.25.251")]
		public string MservEndpoint
		{
			get
			{
				return (string)base["MservEndpoint"];
			}
			set
			{
				base["MservEndpoint"] = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0008AFD0 File Offset: 0x000891D0
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0008AFE2 File Offset: 0x000891E2
		[ConfigurationProperty("ConsumerMbxLookupDisabled", DefaultValue = false)]
		public bool ConsumerMbxLookupDisabled
		{
			get
			{
				return (bool)base["ConsumerMbxLookupDisabled"];
			}
			set
			{
				base["ConsumerMbxLookupDisabled"] = value;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0008AFF5 File Offset: 0x000891F5
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0008B007 File Offset: 0x00089207
		[ConfigurationProperty("ConsumerMailboxScenarioDisabled", DefaultValue = false)]
		public bool ConsumerMailboxScenarioDisabled
		{
			get
			{
				return (bool)base["ConsumerMailboxScenarioDisabled"];
			}
			set
			{
				base["ConsumerMailboxScenarioDisabled"] = value;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0008B01A File Offset: 0x0008921A
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x0008B02C File Offset: 0x0008922C
		[ConfigurationProperty("TolerateInvalidInputInAggregateSession", DefaultValue = true)]
		public bool TolerateInvalidInputInAggregateSession
		{
			get
			{
				return (bool)base["TolerateInvalidInputInAggregateSession"];
			}
			set
			{
				base["TolerateInvalidInputInAggregateSession"] = value;
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0008B03F File Offset: 0x0008923F
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>(0L, "Unrecognized configuration attribute {0}={1}", name, value);
			return base.OnDeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x04001288 RID: 4744
		private static readonly AdDriverConfigSchema.AdDriverScopeSchema scopeSchema = new AdDriverConfigSchema.AdDriverScopeSchema();

		// Token: 0x0200029A RID: 666
		[Serializable]
		public static class Scope
		{
			// Token: 0x04001289 RID: 4745
			public const string ForestFqdn = "ForestFqdn";
		}

		// Token: 0x0200029B RID: 667
		public static class Setting
		{
			// Token: 0x0400128A RID: 4746
			public const string IsSoftLinkResolutionEnabledForAllProcesses = "IsSoftLinkResolutionEnabledForAllProcesses";

			// Token: 0x0400128B RID: 4747
			public const string IsSoftLinkResolutionCacheEnabled = "IsSoftLinkResolutionCacheEnabled";

			// Token: 0x0400128C RID: 4748
			public const string SoftLinkFormatVersion = "SoftLinkFormatVersion";

			// Token: 0x0400128D RID: 4749
			public const string SoftLinkFilterVersion2Enabled = "SoftLinkFilterVersion2Enabled";

			// Token: 0x0400128E RID: 4750
			public const string GlsCacheServiceMode = "GlsCacheServiceMode";

			// Token: 0x0400128F RID: 4751
			public const string OverrideGlsCacheLoadType = "OverrideGlsCacheLoadType";

			// Token: 0x04001290 RID: 4752
			public const string OfflineDataCacheExpirationTimeInMinutes = "OfflineDataCacheExpirationTimeInMinutes";

			// Token: 0x04001291 RID: 4753
			public const string DelayForADWriteThrottlingInMsec = "DelayForADWriteThrottlingInMsec";

			// Token: 0x04001292 RID: 4754
			public const string IsADWriteDisabled = "IsADWriteDisabled";

			// Token: 0x04001293 RID: 4755
			public const string GlsTenantOverrides = "GlsTenantOverrides";

			// Token: 0x04001294 RID: 4756
			public const string MsoEndpointType = "MsoEndpointType";

			// Token: 0x04001295 RID: 4757
			public const string AccountValidationEnabled = "AccountValidationEnabled";

			// Token: 0x04001296 RID: 4758
			public const string TolerateInvalidInputInAggregateSession = "TolerateInvalidInputInAggregateSession";

			// Token: 0x04001297 RID: 4759
			public const string MservEndpoint = "MservEndpoint";

			// Token: 0x04001298 RID: 4760
			public const string ConsumerMbxLookupDisabled = "ConsumerMbxLookupDisabled";

			// Token: 0x04001299 RID: 4761
			public const string ConsumerMailboxScenarioDisabled = "ConsumerMailboxScenarioDisabled";
		}

		// Token: 0x0200029C RID: 668
		[Serializable]
		private class AdDriverScopeSchema : ExchangeConfigurationSection
		{
			// Token: 0x17000777 RID: 1911
			// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0008B070 File Offset: 0x00089270
			// (set) Token: 0x06001F35 RID: 7989 RVA: 0x0008B07D File Offset: 0x0008927D
			[ConfigurationProperty("ForestFqdn", DefaultValue = "")]
			public string ForestFqdn
			{
				get
				{
					return this.InternalGetConfig<string>("ForestFqdn");
				}
				set
				{
					this.InternalSetConfig<string>(value, "ForestFqdn");
				}
			}
		}
	}
}
