using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000127 RID: 295
	public static class Globals
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0004513E File Offset: 0x0004333E
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x00045145 File Offset: 0x00043345
		internal static OWAVDirType OwaVDirType { get; private set; }

		// Token: 0x060009B3 RID: 2483 RVA: 0x0004514D File Offset: 0x0004334D
		internal static void Initialize(OWAVDirType owaVDirType)
		{
			Globals.OwaVDirType = owaVDirType;
			Globals.applicationStopwatch.Start();
			if (owaVDirType == OWAVDirType.OWA)
			{
				Globals.owaSettings = new OwaSettingsLoader();
			}
			else if (owaVDirType == OWAVDirType.Calendar)
			{
				Globals.owaSettings = new CalendarVDirSettingsLoader();
			}
			Globals.owaSettings.Load();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00045186 File Offset: 0x00043386
		internal static void UnloadOwaSettings()
		{
			if (Globals.owaSettings != null)
			{
				Globals.owaSettings.Unload();
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00045199 File Offset: 0x00043399
		public static bool IsPushNotificationsEnabled
		{
			get
			{
				return Globals.owaSettings.IsPushNotificationsEnabled;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x000451A5 File Offset: 0x000433A5
		public static bool IsPullNotificationsEnabled
		{
			get
			{
				return Globals.owaSettings.IsPullNotificationsEnabled;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x000451B1 File Offset: 0x000433B1
		public static bool IsFolderContentNotificationsEnabled
		{
			get
			{
				return Globals.owaSettings.IsFolderContentNotificationsEnabled;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x000451BD File Offset: 0x000433BD
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x000451C4 File Offset: 0x000433C4
		public static bool IsInitialized
		{
			get
			{
				return Globals.isInitialized;
			}
			set
			{
				Globals.isInitialized = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x000451CC File Offset: 0x000433CC
		public static CultureInfo ServerCulture
		{
			get
			{
				if (!Globals.IsInitialized && Globals.owaSettings.ServerCulture == null)
				{
					return Culture.GetCultureInfoInstance(1033);
				}
				return Globals.owaSettings.ServerCulture;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x000451F6 File Offset: 0x000433F6
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x000451FD File Offset: 0x000433FD
		public static Exception InitializationError
		{
			get
			{
				return Globals.initializationError;
			}
			set
			{
				Globals.initializationError = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00045205 File Offset: 0x00043405
		public static string ApplicationVersion
		{
			get
			{
				return OwaRegistryKeys.OwaBasicVersion;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0004520C File Offset: 0x0004340C
		public static ServerVersion LocalHostVersion
		{
			get
			{
				return Globals.owaSettings.LocalHostVersion;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00045218 File Offset: 0x00043418
		public static Dictionary<ServerVersion, ServerVersion> LocalVersionFolders
		{
			get
			{
				return Globals.owaSettings.LocalVersionFolders;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00045224 File Offset: 0x00043424
		public static string ScriptDirectory
		{
			get
			{
				return Globals.ApplicationVersion + "/scripts/";
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00045235 File Offset: 0x00043435
		public static bool ArePerfCountersEnabled
		{
			get
			{
				return Globals.owaSettings.ArePerfCountersEnabled;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00045241 File Offset: 0x00043441
		public static int MaxSearchStringLength
		{
			get
			{
				return Globals.owaSettings.MaxSearchStringLength;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0004524D File Offset: 0x0004344D
		public static int AutoSaveInterval
		{
			get
			{
				return Globals.owaSettings.AutoSaveInterval;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00045259 File Offset: 0x00043459
		public static bool ChangeExpiredPasswordEnabled
		{
			get
			{
				return Globals.owaSettings.ChangeExpiredPasswordEnabled;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00045265 File Offset: 0x00043465
		public static int ConnectionCacheSize
		{
			get
			{
				return Globals.owaSettings.ConnectionCacheSize;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00045271 File Offset: 0x00043471
		public static bool ShowDebugInformation
		{
			get
			{
				return Globals.owaSettings.ShowDebugInformation;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0004527D File Offset: 0x0004347D
		public static bool EnableEmailReports
		{
			get
			{
				return Globals.owaSettings.EnableEmailReports;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00045289 File Offset: 0x00043489
		public static bool ListenAdNotifications
		{
			get
			{
				return Globals.owaSettings.ListenAdNotifications;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00045295 File Offset: 0x00043495
		public static bool RenderBreadcrumbsInAboutPage
		{
			get
			{
				return Globals.owaSettings.RenderBreadcrumbsInAboutPage;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x000452A1 File Offset: 0x000434A1
		public static bool CollectPerRequestPerformanceStats
		{
			get
			{
				return Globals.owaSettings.CollectPerRequestPerformanceStats;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x000452AD File Offset: 0x000434AD
		public static bool CollectSearchStrings
		{
			get
			{
				return Globals.owaSettings.CollectSearchStrings;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x000452B9 File Offset: 0x000434B9
		public static bool DisablePrefixSearch
		{
			get
			{
				return Globals.owaSettings.DisablePrefixSearch;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x000452C5 File Offset: 0x000434C5
		public static bool FilterETag
		{
			get
			{
				return Globals.owaSettings.FilterETag;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x000452D1 File Offset: 0x000434D1
		public static string ContentDeliveryNetworkEndpoint
		{
			get
			{
				return Globals.owaSettings.ContentDeliveryNetworkEndpoint;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x000452DD File Offset: 0x000434DD
		public static string ErrorReportAddress
		{
			get
			{
				return Globals.owaSettings.ErrorReportAddress;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x000452E9 File Offset: 0x000434E9
		public static int MaximumTemporaryFilteredViewPerUser
		{
			get
			{
				return Globals.owaSettings.MaximumTemporaryFilteredViewPerUser;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x000452F5 File Offset: 0x000434F5
		public static int MaximumFilteredViewInFavoritesPerUser
		{
			get
			{
				return Globals.owaSettings.MaximumFilteredViewInFavoritesPerUser;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00045301 File Offset: 0x00043501
		public static bool SendWatsonReports
		{
			get
			{
				return Globals.owaSettings.SendWatsonReports;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0004530D File Offset: 0x0004350D
		public static bool SendClientWatsonReports
		{
			get
			{
				return Globals.owaSettings.SendClientWatsonReports;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00045319 File Offset: 0x00043519
		public static bool DisableBreadcrumbs
		{
			get
			{
				return Globals.owaSettings.DisableBreadcrumbs;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00045325 File Offset: 0x00043525
		public static bool IsPreCheckinApp
		{
			get
			{
				return Globals.owaSettings.IsPreCheckinApp;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00045331 File Offset: 0x00043531
		public static int ServicePointConnectionLimit
		{
			get
			{
				return Globals.owaSettings.ServicePointConnectionLimit;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0004533D File Offset: 0x0004353D
		public static bool ProxyToLocalHost
		{
			get
			{
				return Globals.owaSettings.ProxyToLocalHost;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00045349 File Offset: 0x00043549
		public static int MaxBreadcrumbs
		{
			get
			{
				return Globals.owaSettings.MaxBreadcrumbs;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00045355 File Offset: 0x00043555
		public static bool StoreTransientExceptionEventLogEnabled
		{
			get
			{
				return Globals.owaSettings.StoreTransientExceptionEventLogEnabled;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00045361 File Offset: 0x00043561
		public static int StoreTransientExceptionEventLogThreshold
		{
			get
			{
				return Globals.owaSettings.StoreTransientExceptionEventLogThreshold;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0004536D File Offset: 0x0004356D
		public static int StoreTransientExceptionEventLogFrequencyInSeconds
		{
			get
			{
				return Globals.owaSettings.StoreTransientExceptionEventLogFrequencyInSeconds;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00045379 File Offset: 0x00043579
		public static int MaxPendingRequestLifeInSeconds
		{
			get
			{
				return Globals.owaSettings.MaxPendingRequestLifeInSeconds;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00045385 File Offset: 0x00043585
		public static int MaxItemsInConversationExpansion
		{
			get
			{
				return Globals.owaSettings.MaxItemsInConversationExpansion;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00045391 File Offset: 0x00043591
		public static int MaxItemsInConversationReadingPane
		{
			get
			{
				return Globals.owaSettings.MaxItemsInConversationReadingPane;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0004539D File Offset: 0x0004359D
		public static long MaxBytesInConversationReadingPane
		{
			get
			{
				return Globals.owaSettings.MaxBytesInConversationReadingPane;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x000453A9 File Offset: 0x000435A9
		public static bool HideDeletedItems
		{
			get
			{
				return Globals.owaSettings.HideDeletedItems;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x000453B5 File Offset: 0x000435B5
		public static string OCSServerName
		{
			get
			{
				return Globals.owaSettings.OCSServerName;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x000453C1 File Offset: 0x000435C1
		public static int ActivityBasedPresenceDuration
		{
			get
			{
				return Globals.owaSettings.ActivityBasedPresenceDuration;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x000453CD File Offset: 0x000435CD
		public static int MailTipsMaxClientCacheSize
		{
			get
			{
				return Globals.owaSettings.MailTipsMaxClientCacheSize;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x000453D9 File Offset: 0x000435D9
		public static int MailTipsMaxMailboxSourcedRecipientSize
		{
			get
			{
				return Globals.owaSettings.MailTipsMaxMailboxSourcedRecipientSize;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x000453E5 File Offset: 0x000435E5
		public static int MailTipsClientCacheEntryExpiryInHours
		{
			get
			{
				return Globals.owaSettings.MailTipsClientCacheEntryExpiryInHours;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x000453F1 File Offset: 0x000435F1
		internal static PhishingLevel MinimumSuspiciousPhishingLevel
		{
			get
			{
				return Globals.owaSettings.MinimumSuspiciousPhishingLevel;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x000453FD File Offset: 0x000435FD
		public static long ApplicationTime
		{
			get
			{
				return Globals.applicationStopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00045409 File Offset: 0x00043609
		public static bool CanaryProtectionRequired
		{
			get
			{
				return Globals.OwaVDirType == OWAVDirType.OWA;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00045413 File Offset: 0x00043613
		internal static int UserContextLockTimeout
		{
			get
			{
				return Globals.owaSettings.UserContextLockTimeout;
			}
		}

		// Token: 0x04000731 RID: 1841
		public const int AutoCompleteCacheVersion = 3;

		// Token: 0x04000732 RID: 1842
		public const int PasswordExpirationNotificationDays = 14;

		// Token: 0x04000733 RID: 1843
		public const string DocumentLibraryNamespace = "DocumentLibrary";

		// Token: 0x04000734 RID: 1844
		public const string SendByEmail = "SendByEmail";

		// Token: 0x04000735 RID: 1845
		public const int MaxSubjectLength = 255;

		// Token: 0x04000736 RID: 1846
		public const int MaxInviteMessageLength = 300;

		// Token: 0x04000737 RID: 1847
		private const int FailoverServerLcid = 1033;

		// Token: 0x04000738 RID: 1848
		private const double MinUserTimeoutMaxPendingLifeRatio = 1.25;

		// Token: 0x04000739 RID: 1849
		private const int DefaultMaximumTemporaryFilteredViewPerUser = 60;

		// Token: 0x0400073A RID: 1850
		private const int DefaultMaximumFilteredViewInFavoritesPerUser = 25;

		// Token: 0x0400073B RID: 1851
		internal static readonly string HtmlDirectionCharacterString = new string('‎', 1);

		// Token: 0x0400073C RID: 1852
		public static readonly string CopyrightMessage = "Copyright (c) 2006 Microsoft Corporation.  All rights reserved.";

		// Token: 0x0400073D RID: 1853
		public static readonly string SupportedBrowserHelpUrl = "http://go.microsoft.com/fwlink/?LinkID=129362";

		// Token: 0x0400073E RID: 1854
		public static readonly string VirtualRootName = "owa";

		// Token: 0x0400073F RID: 1855
		public static readonly string RealmParameter = "realm";

		// Token: 0x04000740 RID: 1856
		private static Stopwatch applicationStopwatch = new Stopwatch();

		// Token: 0x04000741 RID: 1857
		private static bool isInitialized;

		// Token: 0x04000742 RID: 1858
		private static Exception initializationError;

		// Token: 0x04000743 RID: 1859
		private static OwaSettingsLoaderBase owaSettings;
	}
}
