using System;
using Microsoft.Exchange.Flighting;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000105 RID: 261
	public sealed class VariantConfigurationEacComponent : VariantConfigurationComponent
	{
		// Token: 0x06000BE6 RID: 3046 RVA: 0x0001C400 File Offset: 0x0001A600
		internal VariantConfigurationEacComponent() : base("Eac")
		{
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "ManageMailboxAuditing", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "UnifiedPolicy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "DiscoverySearchStats", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "AllowRemoteOnboardingMovesOnly", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "DlpFingerprint", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "GeminiShell", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "DevicePolicyMgmtUI", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "UnifiedAuditPolicy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "EACClientAccessRulesEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "RemoteDomain", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "CmdletLogging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "UnifiedComplianceCenter", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "Office365DIcon", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "DiscoveryPFSearch", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "ModernGroups", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "OrgIdADSeverSettings", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "UCCPermissions", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "AllowMailboxArchiveOnlyMigration", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "DiscoveryDocIdHint", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "AdminHomePage", typeof(IUrl), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "CrossPremiseMigration", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "UCCAuditReports", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "UnlistedServices", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Eac.settings.ini", "BulkPermissionAddRemove", typeof(IFeature), false));
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0001C718 File Offset: 0x0001A918
		public VariantConfigurationSection ManageMailboxAuditing
		{
			get
			{
				return base["ManageMailboxAuditing"];
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0001C725 File Offset: 0x0001A925
		public VariantConfigurationSection UnifiedPolicy
		{
			get
			{
				return base["UnifiedPolicy"];
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0001C732 File Offset: 0x0001A932
		public VariantConfigurationSection DiscoverySearchStats
		{
			get
			{
				return base["DiscoverySearchStats"];
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0001C73F File Offset: 0x0001A93F
		public VariantConfigurationSection AllowRemoteOnboardingMovesOnly
		{
			get
			{
				return base["AllowRemoteOnboardingMovesOnly"];
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0001C74C File Offset: 0x0001A94C
		public VariantConfigurationSection DlpFingerprint
		{
			get
			{
				return base["DlpFingerprint"];
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0001C759 File Offset: 0x0001A959
		public VariantConfigurationSection GeminiShell
		{
			get
			{
				return base["GeminiShell"];
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0001C766 File Offset: 0x0001A966
		public VariantConfigurationSection DevicePolicyMgmtUI
		{
			get
			{
				return base["DevicePolicyMgmtUI"];
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0001C773 File Offset: 0x0001A973
		public VariantConfigurationSection UnifiedAuditPolicy
		{
			get
			{
				return base["UnifiedAuditPolicy"];
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0001C780 File Offset: 0x0001A980
		public VariantConfigurationSection EACClientAccessRulesEnabled
		{
			get
			{
				return base["EACClientAccessRulesEnabled"];
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0001C78D File Offset: 0x0001A98D
		public VariantConfigurationSection RemoteDomain
		{
			get
			{
				return base["RemoteDomain"];
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0001C79A File Offset: 0x0001A99A
		public VariantConfigurationSection CmdletLogging
		{
			get
			{
				return base["CmdletLogging"];
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0001C7A7 File Offset: 0x0001A9A7
		public VariantConfigurationSection UnifiedComplianceCenter
		{
			get
			{
				return base["UnifiedComplianceCenter"];
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0001C7B4 File Offset: 0x0001A9B4
		public VariantConfigurationSection Office365DIcon
		{
			get
			{
				return base["Office365DIcon"];
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0001C7C1 File Offset: 0x0001A9C1
		public VariantConfigurationSection DiscoveryPFSearch
		{
			get
			{
				return base["DiscoveryPFSearch"];
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0001C7CE File Offset: 0x0001A9CE
		public VariantConfigurationSection ModernGroups
		{
			get
			{
				return base["ModernGroups"];
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0001C7DB File Offset: 0x0001A9DB
		public VariantConfigurationSection OrgIdADSeverSettings
		{
			get
			{
				return base["OrgIdADSeverSettings"];
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		public VariantConfigurationSection UCCPermissions
		{
			get
			{
				return base["UCCPermissions"];
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0001C7F5 File Offset: 0x0001A9F5
		public VariantConfigurationSection AllowMailboxArchiveOnlyMigration
		{
			get
			{
				return base["AllowMailboxArchiveOnlyMigration"];
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0001C802 File Offset: 0x0001AA02
		public VariantConfigurationSection DiscoveryDocIdHint
		{
			get
			{
				return base["DiscoveryDocIdHint"];
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0001C80F File Offset: 0x0001AA0F
		public VariantConfigurationSection AdminHomePage
		{
			get
			{
				return base["AdminHomePage"];
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0001C81C File Offset: 0x0001AA1C
		public VariantConfigurationSection CrossPremiseMigration
		{
			get
			{
				return base["CrossPremiseMigration"];
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0001C829 File Offset: 0x0001AA29
		public VariantConfigurationSection UCCAuditReports
		{
			get
			{
				return base["UCCAuditReports"];
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0001C836 File Offset: 0x0001AA36
		public VariantConfigurationSection UnlistedServices
		{
			get
			{
				return base["UnlistedServices"];
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0001C843 File Offset: 0x0001AA43
		public VariantConfigurationSection BulkPermissionAddRemove
		{
			get
			{
				return base["BulkPermissionAddRemove"];
			}
		}
	}
}
