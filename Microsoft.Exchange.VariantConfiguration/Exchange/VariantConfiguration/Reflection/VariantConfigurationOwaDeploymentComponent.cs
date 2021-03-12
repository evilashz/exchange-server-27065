using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200011B RID: 283
	public sealed class VariantConfigurationOwaDeploymentComponent : VariantConfigurationComponent
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x0001FBBC File Offset: 0x0001DDBC
		internal VariantConfigurationOwaDeploymentComponent() : base("OwaDeployment")
		{
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "PublicFolderTreePerTenanant", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "ExplicitLogonAuthFilter", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "Places", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "IncludeAccountAccessDisclaimer", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "FilterETag", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "CacheUMCultures", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "RedirectToServer", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "UseAccessProxyForInstantMessagingServerName", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "UseBackendVdirConfiguration", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "OneDriveProProviderAvailable", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "LogTenantInfo", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "RedirectToLogoffPage", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "IsBranded", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "SkipPushNotificationStorageTenantId", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "UseVdirConfigForInstantMessaging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "RenderPrivacyStatement", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "UseRootDirForAppCacheVdir", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "IsLogonFormatEmail", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "WacConfigurationFromOrgConfig", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "MrsConnectedAccountsSync", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "UsePersistedCapabilities", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "CheckFeatureRestrictions", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "HideInternalUrls", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "IncludeImportContactListButton", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "UseThemeStorageFolder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaDeployment.settings.ini", "ConnectedAccountsSync", typeof(IFeature), false));
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0001FF14 File Offset: 0x0001E114
		public VariantConfigurationSection PublicFolderTreePerTenanant
		{
			get
			{
				return base["PublicFolderTreePerTenanant"];
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0001FF21 File Offset: 0x0001E121
		public VariantConfigurationSection ExplicitLogonAuthFilter
		{
			get
			{
				return base["ExplicitLogonAuthFilter"];
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0001FF2E File Offset: 0x0001E12E
		public VariantConfigurationSection Places
		{
			get
			{
				return base["Places"];
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0001FF3B File Offset: 0x0001E13B
		public VariantConfigurationSection IncludeAccountAccessDisclaimer
		{
			get
			{
				return base["IncludeAccountAccessDisclaimer"];
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0001FF48 File Offset: 0x0001E148
		public VariantConfigurationSection FilterETag
		{
			get
			{
				return base["FilterETag"];
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0001FF55 File Offset: 0x0001E155
		public VariantConfigurationSection CacheUMCultures
		{
			get
			{
				return base["CacheUMCultures"];
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0001FF62 File Offset: 0x0001E162
		public VariantConfigurationSection RedirectToServer
		{
			get
			{
				return base["RedirectToServer"];
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0001FF6F File Offset: 0x0001E16F
		public VariantConfigurationSection UseAccessProxyForInstantMessagingServerName
		{
			get
			{
				return base["UseAccessProxyForInstantMessagingServerName"];
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0001FF7C File Offset: 0x0001E17C
		public VariantConfigurationSection UseBackendVdirConfiguration
		{
			get
			{
				return base["UseBackendVdirConfiguration"];
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0001FF89 File Offset: 0x0001E189
		public VariantConfigurationSection OneDriveProProviderAvailable
		{
			get
			{
				return base["OneDriveProProviderAvailable"];
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0001FF96 File Offset: 0x0001E196
		public VariantConfigurationSection LogTenantInfo
		{
			get
			{
				return base["LogTenantInfo"];
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0001FFA3 File Offset: 0x0001E1A3
		public VariantConfigurationSection RedirectToLogoffPage
		{
			get
			{
				return base["RedirectToLogoffPage"];
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		public VariantConfigurationSection IsBranded
		{
			get
			{
				return base["IsBranded"];
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x0001FFBD File Offset: 0x0001E1BD
		public VariantConfigurationSection SkipPushNotificationStorageTenantId
		{
			get
			{
				return base["SkipPushNotificationStorageTenantId"];
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x0001FFCA File Offset: 0x0001E1CA
		public VariantConfigurationSection UseVdirConfigForInstantMessaging
		{
			get
			{
				return base["UseVdirConfigForInstantMessaging"];
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0001FFD7 File Offset: 0x0001E1D7
		public VariantConfigurationSection RenderPrivacyStatement
		{
			get
			{
				return base["RenderPrivacyStatement"];
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0001FFE4 File Offset: 0x0001E1E4
		public VariantConfigurationSection UseRootDirForAppCacheVdir
		{
			get
			{
				return base["UseRootDirForAppCacheVdir"];
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0001FFF1 File Offset: 0x0001E1F1
		public VariantConfigurationSection IsLogonFormatEmail
		{
			get
			{
				return base["IsLogonFormatEmail"];
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0001FFFE File Offset: 0x0001E1FE
		public VariantConfigurationSection WacConfigurationFromOrgConfig
		{
			get
			{
				return base["WacConfigurationFromOrgConfig"];
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0002000B File Offset: 0x0001E20B
		public VariantConfigurationSection MrsConnectedAccountsSync
		{
			get
			{
				return base["MrsConnectedAccountsSync"];
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00020018 File Offset: 0x0001E218
		public VariantConfigurationSection UsePersistedCapabilities
		{
			get
			{
				return base["UsePersistedCapabilities"];
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00020025 File Offset: 0x0001E225
		public VariantConfigurationSection CheckFeatureRestrictions
		{
			get
			{
				return base["CheckFeatureRestrictions"];
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00020032 File Offset: 0x0001E232
		public VariantConfigurationSection HideInternalUrls
		{
			get
			{
				return base["HideInternalUrls"];
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0002003F File Offset: 0x0001E23F
		public VariantConfigurationSection IncludeImportContactListButton
		{
			get
			{
				return base["IncludeImportContactListButton"];
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0002004C File Offset: 0x0001E24C
		public VariantConfigurationSection UseThemeStorageFolder
		{
			get
			{
				return base["UseThemeStorageFolder"];
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00020059 File Offset: 0x0001E259
		public VariantConfigurationSection ConnectedAccountsSync
		{
			get
			{
				return base["ConnectedAccountsSync"];
			}
		}
	}
}
