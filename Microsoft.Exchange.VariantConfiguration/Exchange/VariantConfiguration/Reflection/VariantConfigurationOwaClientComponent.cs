using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000118 RID: 280
	public sealed class VariantConfigurationOwaClientComponent : VariantConfigurationComponent
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
		internal VariantConfigurationOwaClientComponent() : base("OwaClient")
		{
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "TopNSuggestions", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "O365ShellPlus", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "XOWABirthdayCalendar", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "CalendarSearchSurvey", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "LWX", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "FlagPlus", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "PALDogfoodEnforcement", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "EnableFBL", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "ModernGroupsQuotedText", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "InstantEventCreate", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "BuildGreenLightSurveyFlight", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "XOWAShowPersonaCardOnHover", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "CalendarEventSearch", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "InstantSearch", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "Like", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "iOSSharePointRichTextEditor", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "ModernGroupsTrendingConversations", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "AttachmentsHub", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "LocationReminder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "OWADiagnostics", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "DeleteGroupConversation", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "Oops", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "DisableAnimations", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "UnifiedMailboxUI", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "XOWAUnifiedForms", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "O365ShellCore", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "XOWAFrequentContacts", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "InstantPopout", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "Water", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "EmailReminders", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "ProposeNewTime", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "EnableAnimations", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "SuperMailLink", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "OwaFlow", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "OptionsLimited", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "XOWACalendar", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "SuperSwipe", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "XOWASuperCommand", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "OWADelayedBinding", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "SharePointOneDrive", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "SendLinkClickedSignalToSP", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "XOWAAwesomeReadingPane", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "OrganizationBrowser", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "O365Miniatures", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "ModernGroupsSurveyGroupA", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "OwaPublicFolderFavorites", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "MailSatisfactionSurvey", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "QuickCapture", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "OwaLinkPrefetch", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "Options", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "SuppressPushNotificationsWhenOof", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "AndroidCED", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "InstantPopout2", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "LanguageQualitySurvey", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "O365Panorama", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "ShowClientWatson", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "HelpPanel", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "InstantSearchAlpha", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "MowaInternalFeedback", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "XOWATasks", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "XOWAEmoji", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "ContextualApps", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "SuperZoom", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "AgavePerformance", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "ComposeBread1", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OwaClient.settings.ini", "WorkingSetAgent", typeof(IFeature), false));
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0001EF38 File Offset: 0x0001D138
		public VariantConfigurationSection TopNSuggestions
		{
			get
			{
				return base["TopNSuggestions"];
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0001EF45 File Offset: 0x0001D145
		public VariantConfigurationSection O365ShellPlus
		{
			get
			{
				return base["O365ShellPlus"];
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0001EF52 File Offset: 0x0001D152
		public VariantConfigurationSection XOWABirthdayCalendar
		{
			get
			{
				return base["XOWABirthdayCalendar"];
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0001EF5F File Offset: 0x0001D15F
		public VariantConfigurationSection CalendarSearchSurvey
		{
			get
			{
				return base["CalendarSearchSurvey"];
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0001EF6C File Offset: 0x0001D16C
		public VariantConfigurationSection LWX
		{
			get
			{
				return base["LWX"];
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0001EF79 File Offset: 0x0001D179
		public VariantConfigurationSection FlagPlus
		{
			get
			{
				return base["FlagPlus"];
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0001EF86 File Offset: 0x0001D186
		public VariantConfigurationSection PALDogfoodEnforcement
		{
			get
			{
				return base["PALDogfoodEnforcement"];
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0001EF93 File Offset: 0x0001D193
		public VariantConfigurationSection EnableFBL
		{
			get
			{
				return base["EnableFBL"];
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0001EFA0 File Offset: 0x0001D1A0
		public VariantConfigurationSection ModernGroupsQuotedText
		{
			get
			{
				return base["ModernGroupsQuotedText"];
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0001EFAD File Offset: 0x0001D1AD
		public VariantConfigurationSection InstantEventCreate
		{
			get
			{
				return base["InstantEventCreate"];
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0001EFBA File Offset: 0x0001D1BA
		public VariantConfigurationSection BuildGreenLightSurveyFlight
		{
			get
			{
				return base["BuildGreenLightSurveyFlight"];
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0001EFC7 File Offset: 0x0001D1C7
		public VariantConfigurationSection XOWAShowPersonaCardOnHover
		{
			get
			{
				return base["XOWAShowPersonaCardOnHover"];
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0001EFD4 File Offset: 0x0001D1D4
		public VariantConfigurationSection CalendarEventSearch
		{
			get
			{
				return base["CalendarEventSearch"];
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0001EFE1 File Offset: 0x0001D1E1
		public VariantConfigurationSection InstantSearch
		{
			get
			{
				return base["InstantSearch"];
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0001EFEE File Offset: 0x0001D1EE
		public VariantConfigurationSection Like
		{
			get
			{
				return base["Like"];
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0001EFFB File Offset: 0x0001D1FB
		public VariantConfigurationSection iOSSharePointRichTextEditor
		{
			get
			{
				return base["iOSSharePointRichTextEditor"];
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0001F008 File Offset: 0x0001D208
		public VariantConfigurationSection ModernGroupsTrendingConversations
		{
			get
			{
				return base["ModernGroupsTrendingConversations"];
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0001F015 File Offset: 0x0001D215
		public VariantConfigurationSection AttachmentsHub
		{
			get
			{
				return base["AttachmentsHub"];
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0001F022 File Offset: 0x0001D222
		public VariantConfigurationSection LocationReminder
		{
			get
			{
				return base["LocationReminder"];
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0001F02F File Offset: 0x0001D22F
		public VariantConfigurationSection OWADiagnostics
		{
			get
			{
				return base["OWADiagnostics"];
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0001F03C File Offset: 0x0001D23C
		public VariantConfigurationSection DeleteGroupConversation
		{
			get
			{
				return base["DeleteGroupConversation"];
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0001F049 File Offset: 0x0001D249
		public VariantConfigurationSection Oops
		{
			get
			{
				return base["Oops"];
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0001F056 File Offset: 0x0001D256
		public VariantConfigurationSection DisableAnimations
		{
			get
			{
				return base["DisableAnimations"];
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0001F063 File Offset: 0x0001D263
		public VariantConfigurationSection UnifiedMailboxUI
		{
			get
			{
				return base["UnifiedMailboxUI"];
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0001F070 File Offset: 0x0001D270
		public VariantConfigurationSection XOWAUnifiedForms
		{
			get
			{
				return base["XOWAUnifiedForms"];
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0001F07D File Offset: 0x0001D27D
		public VariantConfigurationSection O365ShellCore
		{
			get
			{
				return base["O365ShellCore"];
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0001F08A File Offset: 0x0001D28A
		public VariantConfigurationSection XOWAFrequentContacts
		{
			get
			{
				return base["XOWAFrequentContacts"];
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0001F097 File Offset: 0x0001D297
		public VariantConfigurationSection InstantPopout
		{
			get
			{
				return base["InstantPopout"];
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
		public VariantConfigurationSection Water
		{
			get
			{
				return base["Water"];
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0001F0B1 File Offset: 0x0001D2B1
		public VariantConfigurationSection EmailReminders
		{
			get
			{
				return base["EmailReminders"];
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0001F0BE File Offset: 0x0001D2BE
		public VariantConfigurationSection ProposeNewTime
		{
			get
			{
				return base["ProposeNewTime"];
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0001F0CB File Offset: 0x0001D2CB
		public VariantConfigurationSection EnableAnimations
		{
			get
			{
				return base["EnableAnimations"];
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		public VariantConfigurationSection SuperMailLink
		{
			get
			{
				return base["SuperMailLink"];
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0001F0E5 File Offset: 0x0001D2E5
		public VariantConfigurationSection OwaFlow
		{
			get
			{
				return base["OwaFlow"];
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0001F0F2 File Offset: 0x0001D2F2
		public VariantConfigurationSection OptionsLimited
		{
			get
			{
				return base["OptionsLimited"];
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0001F0FF File Offset: 0x0001D2FF
		public VariantConfigurationSection XOWACalendar
		{
			get
			{
				return base["XOWACalendar"];
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0001F10C File Offset: 0x0001D30C
		public VariantConfigurationSection SuperSwipe
		{
			get
			{
				return base["SuperSwipe"];
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0001F119 File Offset: 0x0001D319
		public VariantConfigurationSection XOWASuperCommand
		{
			get
			{
				return base["XOWASuperCommand"];
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0001F126 File Offset: 0x0001D326
		public VariantConfigurationSection OWADelayedBinding
		{
			get
			{
				return base["OWADelayedBinding"];
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0001F133 File Offset: 0x0001D333
		public VariantConfigurationSection SharePointOneDrive
		{
			get
			{
				return base["SharePointOneDrive"];
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0001F140 File Offset: 0x0001D340
		public VariantConfigurationSection SendLinkClickedSignalToSP
		{
			get
			{
				return base["SendLinkClickedSignalToSP"];
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0001F14D File Offset: 0x0001D34D
		public VariantConfigurationSection XOWAAwesomeReadingPane
		{
			get
			{
				return base["XOWAAwesomeReadingPane"];
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0001F15A File Offset: 0x0001D35A
		public VariantConfigurationSection OrganizationBrowser
		{
			get
			{
				return base["OrganizationBrowser"];
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0001F167 File Offset: 0x0001D367
		public VariantConfigurationSection O365Miniatures
		{
			get
			{
				return base["O365Miniatures"];
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0001F174 File Offset: 0x0001D374
		public VariantConfigurationSection ModernGroupsSurveyGroupA
		{
			get
			{
				return base["ModernGroupsSurveyGroupA"];
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0001F181 File Offset: 0x0001D381
		public VariantConfigurationSection OwaPublicFolderFavorites
		{
			get
			{
				return base["OwaPublicFolderFavorites"];
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0001F18E File Offset: 0x0001D38E
		public VariantConfigurationSection MailSatisfactionSurvey
		{
			get
			{
				return base["MailSatisfactionSurvey"];
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0001F19B File Offset: 0x0001D39B
		public VariantConfigurationSection QuickCapture
		{
			get
			{
				return base["QuickCapture"];
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
		public VariantConfigurationSection OwaLinkPrefetch
		{
			get
			{
				return base["OwaLinkPrefetch"];
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0001F1B5 File Offset: 0x0001D3B5
		public VariantConfigurationSection Options
		{
			get
			{
				return base["Options"];
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0001F1C2 File Offset: 0x0001D3C2
		public VariantConfigurationSection SuppressPushNotificationsWhenOof
		{
			get
			{
				return base["SuppressPushNotificationsWhenOof"];
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0001F1CF File Offset: 0x0001D3CF
		public VariantConfigurationSection AndroidCED
		{
			get
			{
				return base["AndroidCED"];
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0001F1DC File Offset: 0x0001D3DC
		public VariantConfigurationSection InstantPopout2
		{
			get
			{
				return base["InstantPopout2"];
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0001F1E9 File Offset: 0x0001D3E9
		public VariantConfigurationSection LanguageQualitySurvey
		{
			get
			{
				return base["LanguageQualitySurvey"];
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0001F1F6 File Offset: 0x0001D3F6
		public VariantConfigurationSection O365Panorama
		{
			get
			{
				return base["O365Panorama"];
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0001F203 File Offset: 0x0001D403
		public VariantConfigurationSection ShowClientWatson
		{
			get
			{
				return base["ShowClientWatson"];
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0001F210 File Offset: 0x0001D410
		public VariantConfigurationSection HelpPanel
		{
			get
			{
				return base["HelpPanel"];
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0001F21D File Offset: 0x0001D41D
		public VariantConfigurationSection InstantSearchAlpha
		{
			get
			{
				return base["InstantSearchAlpha"];
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0001F22A File Offset: 0x0001D42A
		public VariantConfigurationSection MowaInternalFeedback
		{
			get
			{
				return base["MowaInternalFeedback"];
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0001F237 File Offset: 0x0001D437
		public VariantConfigurationSection XOWATasks
		{
			get
			{
				return base["XOWATasks"];
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0001F244 File Offset: 0x0001D444
		public VariantConfigurationSection XOWAEmoji
		{
			get
			{
				return base["XOWAEmoji"];
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0001F251 File Offset: 0x0001D451
		public VariantConfigurationSection ContextualApps
		{
			get
			{
				return base["ContextualApps"];
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0001F25E File Offset: 0x0001D45E
		public VariantConfigurationSection SuperZoom
		{
			get
			{
				return base["SuperZoom"];
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0001F26B File Offset: 0x0001D46B
		public VariantConfigurationSection AgavePerformance
		{
			get
			{
				return base["AgavePerformance"];
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0001F278 File Offset: 0x0001D478
		public VariantConfigurationSection ComposeBread1
		{
			get
			{
				return base["ComposeBread1"];
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0001F285 File Offset: 0x0001D485
		public VariantConfigurationSection WorkingSetAgent
		{
			get
			{
				return base["WorkingSetAgent"];
			}
		}
	}
}
