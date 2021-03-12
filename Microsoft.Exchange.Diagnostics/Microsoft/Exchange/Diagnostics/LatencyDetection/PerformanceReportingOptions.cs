using System;
using System.Threading;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x0200017E RID: 382
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PerformanceReportingOptions : IThresholdInitializer
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x00027F65 File Offset: 0x00026165
		private PerformanceReportingOptions()
		{
			this.Refresh();
			this.LoadTime = DateTime.UtcNow;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00027F89 File Offset: 0x00026189
		internal static PerformanceReportingOptions Instance
		{
			get
			{
				return PerformanceReportingOptions.singletonInstance;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00027F90 File Offset: 0x00026190
		internal bool LatencyDetectionEnabled
		{
			get
			{
				this.RefreshIfExpired();
				return this.latencyDetectionEnabled && this.LoadTime + this.InitialWait < DateTime.UtcNow;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00027FBD File Offset: 0x000261BD
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00027FC5 File Offset: 0x000261C5
		internal TimeSpan WatsonThrottle { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00027FCE File Offset: 0x000261CE
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x00027FD6 File Offset: 0x000261D6
		internal TimeSpan InitialWait { get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00027FDF File Offset: 0x000261DF
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00027FE7 File Offset: 0x000261E7
		internal DateTime LoadTime { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00027FF0 File Offset: 0x000261F0
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00027FF8 File Offset: 0x000261F8
		internal TimeSpan RefreshOptionsInterval { get; private set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00028001 File Offset: 0x00026201
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x00028009 File Offset: 0x00026209
		internal TimeSpan BacklogRetirementAge { get; private set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00028012 File Offset: 0x00026212
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x0002801A File Offset: 0x0002621A
		internal uint MaximumBacklogSize { get; private set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00028023 File Offset: 0x00026223
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0002802B File Offset: 0x0002622B
		public bool EnableLatencyEventLogging { get; private set; }

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00028034 File Offset: 0x00026234
		void IThresholdInitializer.SetThresholdFromConfiguration(LatencyDetectionLocation location, LoggingType type)
		{
			TimeSpan threshold = location.DefaultThreshold;
			if (LoggingType.WindowsErrorReporting == type)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\LatencyDetectionOptions"))
				{
					if (registryKey != null)
					{
						uint registryUInt = PerformanceReportingOptions.GetRegistryUInt32(registryKey, location.Identity + "ThresholdMilliseconds", PerformanceReportingOptions.GetTotalMilliseconds(location.DefaultThreshold), PerformanceReportingOptions.GetTotalMilliseconds(location.MinimumThreshold));
						threshold = TimeSpan.FromMilliseconds(registryUInt);
					}
				}
			}
			location.SetThreshold(type, threshold);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000280BC File Offset: 0x000262BC
		internal void Refresh()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\LatencyDetectionOptions"))
			{
				if (registryKey == null)
				{
					this.latencyDetectionEnabled = false;
					this.WatsonThrottle = TimeSpan.FromMinutes(10.0);
					this.InitialWait = TimeSpan.FromMinutes(3.0);
					this.RefreshOptionsInterval = TimeSpan.FromSeconds(30.0);
					this.BacklogRetirementAge = TimeSpan.FromMinutes(10.0);
					this.MaximumBacklogSize = 32U;
					this.EnableLatencyEventLogging = PerformanceReportingOptions.IsLatencyEventLoggingEnabled();
				}
				else
				{
					this.latencyDetectionEnabled = PerformanceReportingOptions.GetRegistryBoolean(registryKey, "Enabled", false);
					this.WatsonThrottle = TimeSpan.FromMinutes(PerformanceReportingOptions.GetRegistryUInt32(registryKey, "WatsonThrottleMinutes", 10U, 10U));
					this.InitialWait = TimeSpan.FromMinutes(PerformanceReportingOptions.GetRegistryUInt32(registryKey, "InitialWaitMinutes", 3U, 0U, 120U));
					this.RefreshOptionsInterval = TimeSpan.FromSeconds(PerformanceReportingOptions.GetRegistryUInt32(registryKey, "RefreshOptionsIntervalSeconds", 30U, 30U, 3600U));
					this.BacklogRetirementAge = TimeSpan.FromMinutes(PerformanceReportingOptions.GetRegistryUInt32(registryKey, "BacklogRetirementAgeMinutes", 10U, 1U, 480U));
					this.MaximumBacklogSize = PerformanceReportingOptions.GetRegistryUInt32(registryKey, "BacklogSizeLimit", 32U, 0U, 256U);
					this.EnableLatencyEventLogging = PerformanceReportingOptions.GetRegistryBoolean(registryKey, "EnableLatencyEventLogging", false);
				}
				lock (this.refreshLockObject)
				{
					this.nextRefresh = DateTime.UtcNow + this.RefreshOptionsInterval;
				}
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00028274 File Offset: 0x00026474
		private static uint GetTotalMilliseconds(TimeSpan duration)
		{
			double totalMilliseconds = duration.TotalMilliseconds;
			uint result = 2147483647U;
			if (totalMilliseconds < 2147483647.0)
			{
				result = (uint)totalMilliseconds;
			}
			return result;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000282A0 File Offset: 0x000264A0
		private static uint GetRegistryUInt32(RegistryKey key, string name, uint defaultValue, uint minimum, uint maximum)
		{
			uint num = defaultValue;
			int? num2 = key.GetValue(name, defaultValue) as int?;
			if (num2 != null && num2 >= 0)
			{
				num = Math.Max(minimum, (uint)num2.Value);
				num = Math.Min(maximum, num);
			}
			return num;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00028300 File Offset: 0x00026500
		private static uint GetRegistryUInt32(RegistryKey key, string name, uint defaultValue, uint minimum)
		{
			return PerformanceReportingOptions.GetRegistryUInt32(key, name, defaultValue, minimum, 2147483647U);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00028310 File Offset: 0x00026510
		private static bool GetRegistryBoolean(RegistryKey key, string name, bool defaultValue)
		{
			int? num = key.GetValue(name, defaultValue) as int?;
			if (num == null)
			{
				return defaultValue;
			}
			return num != 0;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00028357 File Offset: 0x00026557
		private static bool IsLatencyEventLoggingEnabled()
		{
			return DatacenterRegistry.IsMicrosoftHostedOnly() || DatacenterRegistry.IsDatacenterDedicated();
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00028367 File Offset: 0x00026567
		private void RefreshWorker(object ignored)
		{
			this.Refresh();
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00028370 File Offset: 0x00026570
		private void RefreshIfExpired()
		{
			if (this.nextRefresh <= DateTime.UtcNow)
			{
				lock (this.refreshLockObject)
				{
					DateTime utcNow = DateTime.UtcNow;
					if (this.nextRefresh <= utcNow)
					{
						this.nextRefresh = utcNow + this.RefreshOptionsInterval;
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.RefreshWorker));
					}
				}
			}
		}

		// Token: 0x04000761 RID: 1889
		internal const bool DefaultLatencyDetectionEnabled = false;

		// Token: 0x04000762 RID: 1890
		internal const uint DefaultMinWatsonThrottleMinutes = 10U;

		// Token: 0x04000763 RID: 1891
		internal const uint DefaultInitialWaitMinutes = 3U;

		// Token: 0x04000764 RID: 1892
		internal const uint MaxInitialWaitMinutes = 120U;

		// Token: 0x04000765 RID: 1893
		internal const uint DefaultMinRefreshOptionsIntervalSeconds = 30U;

		// Token: 0x04000766 RID: 1894
		internal const uint MaximumRefreshOptionsIntervalSeconds = 3600U;

		// Token: 0x04000767 RID: 1895
		internal const uint DefaultBacklogRetirementAgeMinutes = 10U;

		// Token: 0x04000768 RID: 1896
		internal const uint MinBacklogRetirementAgeMinutes = 1U;

		// Token: 0x04000769 RID: 1897
		internal const uint DefaultBacklogSizeLimit = 32U;

		// Token: 0x0400076A RID: 1898
		internal const uint MinimumBacklogSizeLimit = 0U;

		// Token: 0x0400076B RID: 1899
		internal const uint MaximumBacklogSizeLimit = 256U;

		// Token: 0x0400076C RID: 1900
		internal const uint MaxBacklogRetirementAgeMinutes = 480U;

		// Token: 0x0400076D RID: 1901
		internal const string ConfigRegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\LatencyDetectionOptions";

		// Token: 0x0400076E RID: 1902
		internal const string ThresholdValueAppend = "ThresholdMilliseconds";

		// Token: 0x0400076F RID: 1903
		internal const string Enabled = "Enabled";

		// Token: 0x04000770 RID: 1904
		internal const string WatsonThrottleMinutes = "WatsonThrottleMinutes";

		// Token: 0x04000771 RID: 1905
		internal const string InitialWaitMinutes = "InitialWaitMinutes";

		// Token: 0x04000772 RID: 1906
		internal const string RefreshOptionsIntervalSeconds = "RefreshOptionsIntervalSeconds";

		// Token: 0x04000773 RID: 1907
		internal const string BacklogRetirementAgeMinutes = "BacklogRetirementAgeMinutes";

		// Token: 0x04000774 RID: 1908
		internal const string BacklogSizeLimit = "BacklogSizeLimit";

		// Token: 0x04000775 RID: 1909
		private const string EnableLatencyEventLoggingFlag = "EnableLatencyEventLogging";

		// Token: 0x04000776 RID: 1910
		private static PerformanceReportingOptions singletonInstance = new PerformanceReportingOptions();

		// Token: 0x04000777 RID: 1911
		private bool latencyDetectionEnabled;

		// Token: 0x04000778 RID: 1912
		private DateTime nextRefresh;

		// Token: 0x04000779 RID: 1913
		private object refreshLockObject = new object();
	}
}
