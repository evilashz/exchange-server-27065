using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000106 RID: 262
	public sealed class VariantConfigurationEwsComponent : VariantConfigurationComponent
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x0001C850 File Offset: 0x0001AA50
		internal VariantConfigurationEwsComponent() : base("Ews")
		{
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "AutoSubscribeNewGroupMembers", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "OnlineArchive", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "UserPasswordExpirationDate", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "InstantSearchFoldersForPublicFolders", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "LinkedAccountTokenMunging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "EwsServiceCredentials", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "ExternalUser", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "UseInternalEwsUrlForExtensionEwsProxyInOwa", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "EwsClientAccessRulesEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "LongRunningScenarioThrottling", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "HttpProxyToCafe", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "OData", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "EwsHttpHandler", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "WsPerformanceCounters", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ews.settings.ini", "CreateUnifiedMailbox", typeof(IFeature), false));
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0001CA48 File Offset: 0x0001AC48
		public VariantConfigurationSection AutoSubscribeNewGroupMembers
		{
			get
			{
				return base["AutoSubscribeNewGroupMembers"];
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0001CA55 File Offset: 0x0001AC55
		public VariantConfigurationSection OnlineArchive
		{
			get
			{
				return base["OnlineArchive"];
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0001CA62 File Offset: 0x0001AC62
		public VariantConfigurationSection UserPasswordExpirationDate
		{
			get
			{
				return base["UserPasswordExpirationDate"];
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0001CA6F File Offset: 0x0001AC6F
		public VariantConfigurationSection InstantSearchFoldersForPublicFolders
		{
			get
			{
				return base["InstantSearchFoldersForPublicFolders"];
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0001CA7C File Offset: 0x0001AC7C
		public VariantConfigurationSection LinkedAccountTokenMunging
		{
			get
			{
				return base["LinkedAccountTokenMunging"];
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0001CA89 File Offset: 0x0001AC89
		public VariantConfigurationSection EwsServiceCredentials
		{
			get
			{
				return base["EwsServiceCredentials"];
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0001CA96 File Offset: 0x0001AC96
		public VariantConfigurationSection ExternalUser
		{
			get
			{
				return base["ExternalUser"];
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0001CAA3 File Offset: 0x0001ACA3
		public VariantConfigurationSection UseInternalEwsUrlForExtensionEwsProxyInOwa
		{
			get
			{
				return base["UseInternalEwsUrlForExtensionEwsProxyInOwa"];
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0001CAB0 File Offset: 0x0001ACB0
		public VariantConfigurationSection EwsClientAccessRulesEnabled
		{
			get
			{
				return base["EwsClientAccessRulesEnabled"];
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0001CABD File Offset: 0x0001ACBD
		public VariantConfigurationSection LongRunningScenarioThrottling
		{
			get
			{
				return base["LongRunningScenarioThrottling"];
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0001CACA File Offset: 0x0001ACCA
		public VariantConfigurationSection HttpProxyToCafe
		{
			get
			{
				return base["HttpProxyToCafe"];
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0001CAD7 File Offset: 0x0001ACD7
		public VariantConfigurationSection OData
		{
			get
			{
				return base["OData"];
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		public VariantConfigurationSection EwsHttpHandler
		{
			get
			{
				return base["EwsHttpHandler"];
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0001CAF1 File Offset: 0x0001ACF1
		public VariantConfigurationSection WsPerformanceCounters
		{
			get
			{
				return base["WsPerformanceCounters"];
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0001CAFE File Offset: 0x0001ACFE
		public VariantConfigurationSection CreateUnifiedMailbox
		{
			get
			{
				return base["CreateUnifiedMailbox"];
			}
		}
	}
}
