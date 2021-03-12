using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000119 RID: 281
	public sealed class VariantConfigurationOwaClientServerComponent : VariantConfigurationComponent
	{
		// Token: 0x06000CF9 RID: 3321 RVA: 0x0001F294 File Offset: 0x0001D494
		internal VariantConfigurationOwaClientServerComponent() : base("OwaClientServer")
		{
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "FolderBasedClutter", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "FlightsView", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "O365Header", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "OwaVersioning", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "AutoSubscribeNewGroupMembers", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "XOWAHolidayCalendars", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "AttachmentsFilePicker", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "GroupRegionalConfiguration", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "DocCollab", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "OwaPublicFolders", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "O365ParityHeader", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "ModernMail", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "SmimeConversation", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "ActiveViewConvergence", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "ModernGroupsWorkingSet", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "InlinePreview", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "PeopleCentricTriage", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "ChangeLayout", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "SuperStart", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "SuperNormal", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "FasterPhoto", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "NotificationBroker", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "ModernGroupsNewArchitecture", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "SuperSort", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "AutoSubscribeSetByDefault", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "SafeHtml", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "Weather", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "ModernGroups", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "ModernAttachments", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "OWAPLTPerf", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClientServer.settings.ini", "O365G2Header", typeof(IFeature), false));
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0001F68C File Offset: 0x0001D88C
		public VariantConfigurationSection FolderBasedClutter
		{
			get
			{
				return base["FolderBasedClutter"];
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0001F699 File Offset: 0x0001D899
		public VariantConfigurationSection FlightsView
		{
			get
			{
				return base["FlightsView"];
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0001F6A6 File Offset: 0x0001D8A6
		public VariantConfigurationSection O365Header
		{
			get
			{
				return base["O365Header"];
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0001F6B3 File Offset: 0x0001D8B3
		public VariantConfigurationSection OwaVersioning
		{
			get
			{
				return base["OwaVersioning"];
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
		public VariantConfigurationSection AutoSubscribeNewGroupMembers
		{
			get
			{
				return base["AutoSubscribeNewGroupMembers"];
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0001F6CD File Offset: 0x0001D8CD
		public VariantConfigurationSection XOWAHolidayCalendars
		{
			get
			{
				return base["XOWAHolidayCalendars"];
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0001F6DA File Offset: 0x0001D8DA
		public VariantConfigurationSection AttachmentsFilePicker
		{
			get
			{
				return base["AttachmentsFilePicker"];
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0001F6E7 File Offset: 0x0001D8E7
		public VariantConfigurationSection GroupRegionalConfiguration
		{
			get
			{
				return base["GroupRegionalConfiguration"];
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		public VariantConfigurationSection DocCollab
		{
			get
			{
				return base["DocCollab"];
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0001F701 File Offset: 0x0001D901
		public VariantConfigurationSection OwaPublicFolders
		{
			get
			{
				return base["OwaPublicFolders"];
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0001F70E File Offset: 0x0001D90E
		public VariantConfigurationSection O365ParityHeader
		{
			get
			{
				return base["O365ParityHeader"];
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0001F71B File Offset: 0x0001D91B
		public VariantConfigurationSection ModernMail
		{
			get
			{
				return base["ModernMail"];
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0001F728 File Offset: 0x0001D928
		public VariantConfigurationSection SmimeConversation
		{
			get
			{
				return base["SmimeConversation"];
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0001F735 File Offset: 0x0001D935
		public VariantConfigurationSection ActiveViewConvergence
		{
			get
			{
				return base["ActiveViewConvergence"];
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0001F742 File Offset: 0x0001D942
		public VariantConfigurationSection ModernGroupsWorkingSet
		{
			get
			{
				return base["ModernGroupsWorkingSet"];
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0001F74F File Offset: 0x0001D94F
		public VariantConfigurationSection InlinePreview
		{
			get
			{
				return base["InlinePreview"];
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0001F75C File Offset: 0x0001D95C
		public VariantConfigurationSection PeopleCentricTriage
		{
			get
			{
				return base["PeopleCentricTriage"];
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0001F769 File Offset: 0x0001D969
		public VariantConfigurationSection ChangeLayout
		{
			get
			{
				return base["ChangeLayout"];
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0001F776 File Offset: 0x0001D976
		public VariantConfigurationSection SuperStart
		{
			get
			{
				return base["SuperStart"];
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0001F783 File Offset: 0x0001D983
		public VariantConfigurationSection SuperNormal
		{
			get
			{
				return base["SuperNormal"];
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0001F790 File Offset: 0x0001D990
		public VariantConfigurationSection FasterPhoto
		{
			get
			{
				return base["FasterPhoto"];
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0001F79D File Offset: 0x0001D99D
		public VariantConfigurationSection NotificationBroker
		{
			get
			{
				return base["NotificationBroker"];
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0001F7AA File Offset: 0x0001D9AA
		public VariantConfigurationSection ModernGroupsNewArchitecture
		{
			get
			{
				return base["ModernGroupsNewArchitecture"];
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0001F7B7 File Offset: 0x0001D9B7
		public VariantConfigurationSection SuperSort
		{
			get
			{
				return base["SuperSort"];
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0001F7C4 File Offset: 0x0001D9C4
		public VariantConfigurationSection AutoSubscribeSetByDefault
		{
			get
			{
				return base["AutoSubscribeSetByDefault"];
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0001F7D1 File Offset: 0x0001D9D1
		public VariantConfigurationSection SafeHtml
		{
			get
			{
				return base["SafeHtml"];
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0001F7DE File Offset: 0x0001D9DE
		public VariantConfigurationSection Weather
		{
			get
			{
				return base["Weather"];
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0001F7EB File Offset: 0x0001D9EB
		public VariantConfigurationSection ModernGroups
		{
			get
			{
				return base["ModernGroups"];
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0001F7F8 File Offset: 0x0001D9F8
		public VariantConfigurationSection ModernAttachments
		{
			get
			{
				return base["ModernAttachments"];
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0001F805 File Offset: 0x0001DA05
		public VariantConfigurationSection OWAPLTPerf
		{
			get
			{
				return base["OWAPLTPerf"];
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0001F812 File Offset: 0x0001DA12
		public VariantConfigurationSection O365G2Header
		{
			get
			{
				return base["O365G2Header"];
			}
		}
	}
}
