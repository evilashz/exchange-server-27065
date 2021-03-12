using System;
using System.Linq;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x02000191 RID: 401
	internal static class HighAvailabilityConstants
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00049F40 File Offset: 0x00048140
		public static int ReqdDataProtectionInfrastructureDetection
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.ReqdDataProtectionInfrastructureDetection);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00049F49 File Offset: 0x00048149
		public static int ReplayRestartWindow
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.ReplayRestartWindow);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00049F55 File Offset: 0x00048155
		public static int ReqdDataProtectionInfrastructureBugcheck
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.ReqdDataProtectionInfrastructureBugcheck);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00049F61 File Offset: 0x00048161
		public static int ReqdDataProtectionInfrastructureEscalate2
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.ReqdDataProtectionInfrastructureEscalate2);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00049F6D File Offset: 0x0004816D
		public static int NonHealthThreatingDataProtectionInfraDetection
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.ReplayRestartWindow);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00049F79 File Offset: 0x00048179
		public static int TransientNonHealthThreatingDataProtectionInfraDetection
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.TransientNonHealthThreatingDataProtectionInfraDetection);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00049F85 File Offset: 0x00048185
		public static int NonHealthThreatingDataProtectionInfraRepair1
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.TransientNonHealthThreatingDataProtectionInfraDetection);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00049F91 File Offset: 0x00048191
		public static int NonHealthThreatingDataProtectionInfraRepair2
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.NonHealthThreatingDataProtectionInfraRepair2);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00049F9D File Offset: 0x0004819D
		public static int ReqdDataProtectionInfrastructureEscalate1
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.ReqdDataProtectionInfrastructureBugcheck);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00049FA9 File Offset: 0x000481A9
		public static int ReqdDataProtectionInfrastructureRecoveryFailure
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.ReqdDataProtectionInfrastructureBugcheck);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x00049FB5 File Offset: 0x000481B5
		public static int TransientSuppressedLoadDetectionWindow
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.TransientSuppressedLoadDetectionWindow);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00049FC1 File Offset: 0x000481C1
		public static int AdministrativelyDerivedFailureDetection
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.AdministrativelyDerivedFailureDetection);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00049FCD File Offset: 0x000481CD
		public static int EstimatedReseedTime
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.AdministrativelyDerivedFailureDetection);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00049FD9 File Offset: 0x000481D9
		public static int ServerInMaintenanceModeTurnaroundTime
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.ServerInMaintenanceModeTurnaroundTime);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00049FE5 File Offset: 0x000481E5
		public static int ProbeFailureDuration
		{
			get
			{
				return HighAvailabilityConstants.GetTimings(HighAvailabilityConstants.Timings.TransientNonHealthThreatingDataProtectionInfraDetection);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00049FF4 File Offset: 0x000481F4
		public static bool DisableResponders
		{
			get
			{
				int value = HighAvailabilityUtility.RegReader.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\Parameters", "BigRedButton", 0);
				return value != 0;
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0004A024 File Offset: 0x00048224
		private static int GetTimings(HighAvailabilityConstants.Timings timing)
		{
			int result = (int)timing;
			if (HighAvailabilityConstants.overrideAllowedTimings.Contains(timing))
			{
				int value = HighAvailabilityUtility.RegReader.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\Parameters", timing.ToString(), 0);
				if (value > 0)
				{
					result = value;
				}
			}
			return result;
		}

		// Token: 0x040008D0 RID: 2256
		public const string ParameterRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\Parameters";

		// Token: 0x040008D1 RID: 2257
		public const string StateRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\States";

		// Token: 0x040008D2 RID: 2258
		public const string DbCopyStateRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\States\\DbCopyStates";

		// Token: 0x040008D3 RID: 2259
		public const string ServerComponentStateRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\States\\ServerComponentStates";

		// Token: 0x040008D4 RID: 2260
		public const char StrikeHistoryFieldSeperator = '|';

		// Token: 0x040008D5 RID: 2261
		public const char StrikeHistoryEntrySeperator = ';';

		// Token: 0x040008D6 RID: 2262
		public const string StrikeHistoryRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\StrikeHistory";

		// Token: 0x040008D7 RID: 2263
		public const string DiskLatencyWatermarkValueName = "DiskLatencyWatermark";

		// Token: 0x040008D8 RID: 2264
		public const string OneCopyMonitorLastRunValueName = "OneCopyMonitorLastRun";

		// Token: 0x040008D9 RID: 2265
		public const string OneCopyMonitorStaleAlertValueName = "OneCopyMonitorStaleAlertInMins";

		// Token: 0x040008DA RID: 2266
		public const string WorkitemEnrollmentLog = "WorkItemEnrollmentLogPath";

		// Token: 0x040008DB RID: 2267
		public const string AdCacheExpirationValueName = "AdCacheExpirationInSeconds";

		// Token: 0x040008DC RID: 2268
		public const string RpcCacheExpirationValueName = "RpcCacheExpirationInSeconds";

		// Token: 0x040008DD RID: 2269
		public const string RegCacheExpirationValueName = "RegCacheExpirationInSeconds";

		// Token: 0x040008DE RID: 2270
		public const string EscalationTeam = "High Availability";

		// Token: 0x040008DF RID: 2271
		public const string EseEscalationTeam = "ESE";

		// Token: 0x040008E0 RID: 2272
		public const string ReplServiceName = "MSExchangeRepl";

		// Token: 0x040008E1 RID: 2273
		public const string DagMgmtServiceName = "MSExchangeDagMgmt";

		// Token: 0x040008E2 RID: 2274
		public const string StoreServiceName = "MSExchangeIS";

		// Token: 0x040008E3 RID: 2275
		public const string NetworkServiceName = "Network";

		// Token: 0x040008E4 RID: 2276
		public const int ProbeMaxRetry = 3;

		// Token: 0x040008E5 RID: 2277
		public const int DefaultMonitoringInterval = 300;

		// Token: 0x040008E6 RID: 2278
		private const string ResponderDisableMasterSwitch = "BigRedButton";

		// Token: 0x040008E7 RID: 2279
		public static readonly string ServiceName = ExchangeComponent.DataProtection.Name;

		// Token: 0x040008E8 RID: 2280
		public static readonly string ControllerServiceName = ExchangeComponent.DiskController.Name;

		// Token: 0x040008E9 RID: 2281
		public static readonly string ClusteringServiceName = ExchangeComponent.Clustering.Name;

		// Token: 0x040008EA RID: 2282
		public static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040008EB RID: 2283
		private static readonly HighAvailabilityConstants.Timings[] overrideAllowedTimings = new HighAvailabilityConstants.Timings[]
		{
			HighAvailabilityConstants.Timings.ReqdDataProtectionInfrastructureBugcheck,
			HighAvailabilityConstants.Timings.ReqdDataProtectionInfrastructureEscalate2,
			HighAvailabilityConstants.Timings.ReqdDataProtectionInfrastructureBugcheck,
			HighAvailabilityConstants.Timings.ReqdDataProtectionInfrastructureBugcheck,
			HighAvailabilityConstants.Timings.AdministrativelyDerivedFailureDetection,
			HighAvailabilityConstants.Timings.AdministrativelyDerivedFailureDetection,
			HighAvailabilityConstants.Timings.ServerInMaintenanceModeTurnaroundTime
		};

		// Token: 0x02000192 RID: 402
		private enum Timings
		{
			// Token: 0x040008ED RID: 2285
			ReqdDataProtectionInfrastructureDetection = 120,
			// Token: 0x040008EE RID: 2286
			ReplayRestartWindow = 300,
			// Token: 0x040008EF RID: 2287
			ReqdDataProtectionInfrastructureBugcheck = 600,
			// Token: 0x040008F0 RID: 2288
			ReqdDataProtectionInfrastructureEscalate2 = 1200,
			// Token: 0x040008F1 RID: 2289
			NonHealthThreatingDataProtectionInfraDetection = 300,
			// Token: 0x040008F2 RID: 2290
			TransientNonHealthThreatingDataProtectionInfraDetection = 3600,
			// Token: 0x040008F3 RID: 2291
			NonHealthThreatingDataProtectionInfraRepair1 = 3600,
			// Token: 0x040008F4 RID: 2292
			NonHealthThreatingDataProtectionInfraRepair2 = 18000,
			// Token: 0x040008F5 RID: 2293
			ReqdDataProtectionInfrastructureEscalate1 = 600,
			// Token: 0x040008F6 RID: 2294
			ReqdDataProtectionInfrastructureRecoveryFailure = 600,
			// Token: 0x040008F7 RID: 2295
			TransientSuppressedLoadDetectionWindow = 7200,
			// Token: 0x040008F8 RID: 2296
			AdministrativelyDerivedFailureDetection = 28800,
			// Token: 0x040008F9 RID: 2297
			EstimatedReseedTime = 28800,
			// Token: 0x040008FA RID: 2298
			ServerInMaintenanceModeTurnaroundTime = 259200,
			// Token: 0x040008FB RID: 2299
			ProbeFailureDuration = 3600
		}
	}
}
