using System;
using System.ServiceModel;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200012C RID: 300
	public static class Globals
	{
		// Token: 0x060009E6 RID: 2534 RVA: 0x00022F18 File Offset: 0x00021118
		public static void TestHook_SetFlag(bool enable)
		{
			Globals.Owa2ServerUnitTestsHook = enable;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00022F20 File Offset: 0x00021120
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x00022F27 File Offset: 0x00021127
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

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00022F2F File Offset: 0x0002112F
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00022F36 File Offset: 0x00021136
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

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00022F3E File Offset: 0x0002113E
		public static string ApplicationVersion
		{
			get
			{
				if (Globals.application == null)
				{
					return string.Empty;
				}
				return Globals.application.ApplicationVersion;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00022F57 File Offset: 0x00021157
		public static long ApplicationTime
		{
			get
			{
				if (Globals.application == null)
				{
					return 0L;
				}
				return Globals.application.ApplicationTime;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00022F6D File Offset: 0x0002116D
		public static bool ArePerfCountersEnabled
		{
			get
			{
				return Globals.application != null && Globals.application.ArePerfCountersEnabled;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x00022F82 File Offset: 0x00021182
		public static int ActivityBasedPresenceDuration
		{
			get
			{
				if (Globals.application == null)
				{
					return 0;
				}
				return Globals.application.ActivityBasedPresenceDuration;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00022F97 File Offset: 0x00021197
		public static bool SendWatsonReports
		{
			get
			{
				return Globals.application != null && Globals.application.SendWatsonReports;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00022FAC File Offset: 0x000211AC
		public static int MaxBreadcrumbs
		{
			get
			{
				if (Globals.application == null)
				{
					return 0;
				}
				return Globals.application.MaxBreadcrumbs;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00022FC1 File Offset: 0x000211C1
		public static bool LogVerboseNotifications
		{
			get
			{
				return Globals.application != null && Globals.application.LogVerboseNotifications;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00022FD6 File Offset: 0x000211D6
		public static bool DisableBreadcrumbs
		{
			get
			{
				return Globals.application == null || Globals.application.DisableBreadcrumbs;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00022FEB File Offset: 0x000211EB
		public static bool CheckForForgottenAttachmentsEnabled
		{
			get
			{
				return Globals.application == null || Globals.application.CheckForForgottenAttachmentsEnabled;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00023000 File Offset: 0x00021200
		public static bool ControlTasksQueueDisabled
		{
			get
			{
				return Globals.application == null || Globals.application.ControlTasksQueueDisabled;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00023015 File Offset: 0x00021215
		public static string[] BlockedQueryStringValues
		{
			get
			{
				return Globals.blockedQueryStringValues;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0002301C File Offset: 0x0002121C
		public static HttpClientCredentialType ServiceAuthenticationType
		{
			get
			{
				if (Globals.application == null)
				{
					return HttpClientCredentialType.None;
				}
				return Globals.application.ServiceAuthenticationType;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00023031 File Offset: 0x00021231
		public static TroubleshootingContext TroubleshootingContext
		{
			get
			{
				if (Globals.application == null)
				{
					return null;
				}
				return Globals.application.TroubleshootingContext;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00023046 File Offset: 0x00021246
		public static bool LogErrorDetails
		{
			get
			{
				return Globals.application != null && Globals.application.LogErrorDetails;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0002305B File Offset: 0x0002125B
		public static bool LogErrorTraces
		{
			get
			{
				return Globals.application != null && Globals.application.LogErrorTraces;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00023070 File Offset: 0x00021270
		public static string ContentDeliveryNetworkEndpoint
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(Globals.testContentDeliveryNetworkEndpoint))
				{
					return Globals.testContentDeliveryNetworkEndpoint;
				}
				if (Globals.application == null)
				{
					return string.Empty;
				}
				return Globals.application.ContentDeliveryNetworkEndpoint;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002309B File Offset: 0x0002129B
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x000230A2 File Offset: 0x000212A2
		public static bool IsAnonymousCalendarApp { get; private set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x000230AA File Offset: 0x000212AA
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x000230B1 File Offset: 0x000212B1
		public static bool IsFirstReleaseFlightingEnabled { get; private set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x000230B9 File Offset: 0x000212B9
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x000230C0 File Offset: 0x000212C0
		public static bool IsPreCheckinApp { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000230C8 File Offset: 0x000212C8
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x000230CF File Offset: 0x000212CF
		public static bool OwaIsNoRecycleEnabled { get; private set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x000230D7 File Offset: 0x000212D7
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x000230DE File Offset: 0x000212DE
		public static double OwaVersionReadingInterval { get; private set; }

		// Token: 0x06000A05 RID: 2565 RVA: 0x000230E6 File Offset: 0x000212E6
		public static void TestHook_SetContentDeliveryNetworkEndpoint(string cdn)
		{
			Globals.testContentDeliveryNetworkEndpoint = cdn;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000230F0 File Offset: 0x000212F0
		internal static void Initialize()
		{
			Globals.application = BaseApplication.CreateInstance();
			Globals.IsAnonymousCalendarApp = (Globals.application is OwaAnonymousApplication);
			Globals.IsPreCheckinApp = Globals.application.IsPreCheckinApp;
			Globals.IsFirstReleaseFlightingEnabled = Globals.application.IsFirstReleaseFlightingEnabled;
			Globals.OwaIsNoRecycleEnabled = Globals.application.OwaIsNoRecycleEnabled;
			Globals.OwaVersionReadingInterval = Globals.application.OwaVersionReadingInterval;
			Globals.application.Initialize();
			if (Globals.application.BlockedQueryStringValues != null)
			{
				Globals.blockedQueryStringValues = Globals.application.BlockedQueryStringValues.Split(new string[]
				{
					";"
				}, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00023190 File Offset: 0x00021390
		internal static void Dispose()
		{
			if (Globals.application != null)
			{
				Globals.application.Dispose();
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000231A3 File Offset: 0x000213A3
		internal static void UpdateErrorTracingConfiguration()
		{
			if (Globals.application != null)
			{
				Globals.application.UpdateErrorTracingConfiguration();
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000231B8 File Offset: 0x000213B8
		internal static string FormatURIForCDN(string relativeUri)
		{
			if (string.IsNullOrEmpty(Globals.ContentDeliveryNetworkEndpoint))
			{
				return relativeUri;
			}
			if (new Uri(relativeUri, UriKind.RelativeOrAbsolute).IsAbsoluteUri)
			{
				return relativeUri;
			}
			StringBuilder stringBuilder = new StringBuilder(Globals.ContentDeliveryNetworkEndpoint);
			if (!Globals.ContentDeliveryNetworkEndpoint.EndsWith("/"))
			{
				stringBuilder.Append("/");
			}
			stringBuilder.Append("owa/");
			stringBuilder.Append(relativeUri);
			return stringBuilder.ToString();
		}

		// Token: 0x040006A4 RID: 1700
		public static bool Owa2ServerUnitTestsHook;

		// Token: 0x040006A5 RID: 1701
		private static BaseApplication application;

		// Token: 0x040006A6 RID: 1702
		private static bool isInitialized;

		// Token: 0x040006A7 RID: 1703
		private static Exception initializationError;

		// Token: 0x040006A8 RID: 1704
		private static string testContentDeliveryNetworkEndpoint = null;

		// Token: 0x040006A9 RID: 1705
		private static string[] blockedQueryStringValues;
	}
}
