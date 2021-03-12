using System;
using Microsoft.Exchange.Flighting;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200011A RID: 282
	public sealed class VariantConfigurationOwaServerComponent : VariantConfigurationComponent
	{
		// Token: 0x06000D19 RID: 3353 RVA: 0x0001F820 File Offset: 0x0001DA20
		internal VariantConfigurationOwaServerComponent() : base("OwaServer")
		{
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "OwaMailboxSessionCloning", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "PeopleCentricConversation", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "OwaSessionDataPreload", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "ShouldSkipAdfsGroupReadOnFrontend", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "XOWABirthdayAssistant", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "InlineExploreSettings", typeof(IInlineExploreSettings), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "InferenceUI", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "OwaHttpHandler", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "FlightFormat", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "AndroidPremium", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "ModernConversationPrep", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "OptimizedParticipantResolver", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "OwaHostNameSwitch", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "OwaVNext", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "OWAEdgeMode", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "OwaCompositeSessionData", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "ReportJunk", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "OwaClientAccessRulesEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "OwaServerLogonActivityLogging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaServer.settings.ini", "InlineExploreUI", typeof(IFeature), false));
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0001FAB8 File Offset: 0x0001DCB8
		public VariantConfigurationSection OwaMailboxSessionCloning
		{
			get
			{
				return base["OwaMailboxSessionCloning"];
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0001FAC5 File Offset: 0x0001DCC5
		public VariantConfigurationSection PeopleCentricConversation
		{
			get
			{
				return base["PeopleCentricConversation"];
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0001FAD2 File Offset: 0x0001DCD2
		public VariantConfigurationSection OwaSessionDataPreload
		{
			get
			{
				return base["OwaSessionDataPreload"];
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0001FADF File Offset: 0x0001DCDF
		public VariantConfigurationSection ShouldSkipAdfsGroupReadOnFrontend
		{
			get
			{
				return base["ShouldSkipAdfsGroupReadOnFrontend"];
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0001FAEC File Offset: 0x0001DCEC
		public VariantConfigurationSection XOWABirthdayAssistant
		{
			get
			{
				return base["XOWABirthdayAssistant"];
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0001FAF9 File Offset: 0x0001DCF9
		public VariantConfigurationSection InlineExploreSettings
		{
			get
			{
				return base["InlineExploreSettings"];
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0001FB06 File Offset: 0x0001DD06
		public VariantConfigurationSection InferenceUI
		{
			get
			{
				return base["InferenceUI"];
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0001FB13 File Offset: 0x0001DD13
		public VariantConfigurationSection OwaHttpHandler
		{
			get
			{
				return base["OwaHttpHandler"];
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0001FB20 File Offset: 0x0001DD20
		public VariantConfigurationSection FlightFormat
		{
			get
			{
				return base["FlightFormat"];
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0001FB2D File Offset: 0x0001DD2D
		public VariantConfigurationSection AndroidPremium
		{
			get
			{
				return base["AndroidPremium"];
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0001FB3A File Offset: 0x0001DD3A
		public VariantConfigurationSection ModernConversationPrep
		{
			get
			{
				return base["ModernConversationPrep"];
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0001FB47 File Offset: 0x0001DD47
		public VariantConfigurationSection OptimizedParticipantResolver
		{
			get
			{
				return base["OptimizedParticipantResolver"];
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0001FB54 File Offset: 0x0001DD54
		public VariantConfigurationSection OwaHostNameSwitch
		{
			get
			{
				return base["OwaHostNameSwitch"];
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0001FB61 File Offset: 0x0001DD61
		public VariantConfigurationSection OwaVNext
		{
			get
			{
				return base["OwaVNext"];
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0001FB6E File Offset: 0x0001DD6E
		public VariantConfigurationSection OWAEdgeMode
		{
			get
			{
				return base["OWAEdgeMode"];
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0001FB7B File Offset: 0x0001DD7B
		public VariantConfigurationSection OwaCompositeSessionData
		{
			get
			{
				return base["OwaCompositeSessionData"];
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0001FB88 File Offset: 0x0001DD88
		public VariantConfigurationSection ReportJunk
		{
			get
			{
				return base["ReportJunk"];
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0001FB95 File Offset: 0x0001DD95
		public VariantConfigurationSection OwaClientAccessRulesEnabled
		{
			get
			{
				return base["OwaClientAccessRulesEnabled"];
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0001FBA2 File Offset: 0x0001DDA2
		public VariantConfigurationSection OwaServerLogonActivityLogging
		{
			get
			{
				return base["OwaServerLogonActivityLogging"];
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0001FBAF File Offset: 0x0001DDAF
		public VariantConfigurationSection InlineExploreUI
		{
			get
			{
				return base["InlineExploreUI"];
			}
		}
	}
}
