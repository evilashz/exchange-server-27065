using System;
using Microsoft.Exchange.Search;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200011E RID: 286
	public sealed class VariantConfigurationSearchComponent : VariantConfigurationComponent
	{
		// Token: 0x06000D60 RID: 3424 RVA: 0x0002044C File Offset: 0x0001E64C
		internal VariantConfigurationSearchComponent() : base("Search")
		{
			base.Add(new VariantConfigurationSection("Search.settings.ini", "TransportFlowSettings", typeof(ITransportFlowSettings), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "RequireMountedForCrawl", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "RemoveOrphanedCatalogs", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "IndexStatusInvalidateInterval", typeof(IIndexStatusSettings), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "ProcessItemsWithNullCompositeId", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "InstantSearch", typeof(IInstantSearch), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "CrawlerFeederUpdateCrawlingStatusResetCache", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "LanguageDetection", typeof(ILanguageDetection), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "CachePreWarmingEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "MonitorDocumentValidationFailures", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "UseAlphaSchema", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "EnableIndexPartsCache", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "SchemaUpgrading", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "EnableGracefulDegradation", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "EnableIndexStatusTimestampVerification", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "EnableDynamicActivationPreference", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "UseExecuteAndReadPage", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "Completions", typeof(ICompletions), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "UseBetaSchema", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "ReadFlag", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "EnableSingleValueRefiners", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "DocumentFeederSettings", typeof(IDocumentFeederSettings), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "CrawlerFeederCollectDocumentsVerifyPendingWatermarks", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "MemoryModel", typeof(IMemoryModelSettings), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "EnableTopN", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "FeederSettings", typeof(IFeederSettings), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "WaitForMountPoints", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Search.settings.ini", "EnableInstantSearch", typeof(IFeature), false));
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x000207E4 File Offset: 0x0001E9E4
		public VariantConfigurationSection TransportFlowSettings
		{
			get
			{
				return base["TransportFlowSettings"];
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x000207F1 File Offset: 0x0001E9F1
		public VariantConfigurationSection RequireMountedForCrawl
		{
			get
			{
				return base["RequireMountedForCrawl"];
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000207FE File Offset: 0x0001E9FE
		public VariantConfigurationSection RemoveOrphanedCatalogs
		{
			get
			{
				return base["RemoveOrphanedCatalogs"];
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0002080B File Offset: 0x0001EA0B
		public VariantConfigurationSection IndexStatusInvalidateInterval
		{
			get
			{
				return base["IndexStatusInvalidateInterval"];
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00020818 File Offset: 0x0001EA18
		public VariantConfigurationSection ProcessItemsWithNullCompositeId
		{
			get
			{
				return base["ProcessItemsWithNullCompositeId"];
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00020825 File Offset: 0x0001EA25
		public VariantConfigurationSection InstantSearch
		{
			get
			{
				return base["InstantSearch"];
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00020832 File Offset: 0x0001EA32
		public VariantConfigurationSection CrawlerFeederUpdateCrawlingStatusResetCache
		{
			get
			{
				return base["CrawlerFeederUpdateCrawlingStatusResetCache"];
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0002083F File Offset: 0x0001EA3F
		public VariantConfigurationSection LanguageDetection
		{
			get
			{
				return base["LanguageDetection"];
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0002084C File Offset: 0x0001EA4C
		public VariantConfigurationSection CachePreWarmingEnabled
		{
			get
			{
				return base["CachePreWarmingEnabled"];
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00020859 File Offset: 0x0001EA59
		public VariantConfigurationSection MonitorDocumentValidationFailures
		{
			get
			{
				return base["MonitorDocumentValidationFailures"];
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00020866 File Offset: 0x0001EA66
		public VariantConfigurationSection UseAlphaSchema
		{
			get
			{
				return base["UseAlphaSchema"];
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00020873 File Offset: 0x0001EA73
		public VariantConfigurationSection EnableIndexPartsCache
		{
			get
			{
				return base["EnableIndexPartsCache"];
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00020880 File Offset: 0x0001EA80
		public VariantConfigurationSection SchemaUpgrading
		{
			get
			{
				return base["SchemaUpgrading"];
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0002088D File Offset: 0x0001EA8D
		public VariantConfigurationSection EnableGracefulDegradation
		{
			get
			{
				return base["EnableGracefulDegradation"];
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0002089A File Offset: 0x0001EA9A
		public VariantConfigurationSection EnableIndexStatusTimestampVerification
		{
			get
			{
				return base["EnableIndexStatusTimestampVerification"];
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x000208A7 File Offset: 0x0001EAA7
		public VariantConfigurationSection EnableDynamicActivationPreference
		{
			get
			{
				return base["EnableDynamicActivationPreference"];
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x000208B4 File Offset: 0x0001EAB4
		public VariantConfigurationSection UseExecuteAndReadPage
		{
			get
			{
				return base["UseExecuteAndReadPage"];
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000208C1 File Offset: 0x0001EAC1
		public VariantConfigurationSection Completions
		{
			get
			{
				return base["Completions"];
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x000208CE File Offset: 0x0001EACE
		public VariantConfigurationSection UseBetaSchema
		{
			get
			{
				return base["UseBetaSchema"];
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x000208DB File Offset: 0x0001EADB
		public VariantConfigurationSection ReadFlag
		{
			get
			{
				return base["ReadFlag"];
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x000208E8 File Offset: 0x0001EAE8
		public VariantConfigurationSection EnableSingleValueRefiners
		{
			get
			{
				return base["EnableSingleValueRefiners"];
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x000208F5 File Offset: 0x0001EAF5
		public VariantConfigurationSection DocumentFeederSettings
		{
			get
			{
				return base["DocumentFeederSettings"];
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00020902 File Offset: 0x0001EB02
		public VariantConfigurationSection CrawlerFeederCollectDocumentsVerifyPendingWatermarks
		{
			get
			{
				return base["CrawlerFeederCollectDocumentsVerifyPendingWatermarks"];
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0002090F File Offset: 0x0001EB0F
		public VariantConfigurationSection MemoryModel
		{
			get
			{
				return base["MemoryModel"];
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0002091C File Offset: 0x0001EB1C
		public VariantConfigurationSection EnableTopN
		{
			get
			{
				return base["EnableTopN"];
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00020929 File Offset: 0x0001EB29
		public VariantConfigurationSection FeederSettings
		{
			get
			{
				return base["FeederSettings"];
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00020936 File Offset: 0x0001EB36
		public VariantConfigurationSection WaitForMountPoints
		{
			get
			{
				return base["WaitForMountPoints"];
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00020943 File Offset: 0x0001EB43
		public VariantConfigurationSection EnableInstantSearch
		{
			get
			{
				return base["EnableInstantSearch"];
			}
		}
	}
}
