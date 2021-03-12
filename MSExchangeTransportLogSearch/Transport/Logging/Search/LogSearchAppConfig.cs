using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200002C RID: 44
	internal sealed class LogSearchAppConfig
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x000069C9 File Offset: 0x00004BC9
		private LogSearchAppConfig()
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000069D4 File Offset: 0x00004BD4
		public static LogSearchAppConfig Instance
		{
			get
			{
				if (LogSearchAppConfig.instance == null)
				{
					lock (LogSearchAppConfig.initializationLock)
					{
						if (LogSearchAppConfig.instance == null)
						{
							LogSearchAppConfig logSearchAppConfig = new LogSearchAppConfig();
							logSearchAppConfig.logSearchIndexing = LogSearchAppConfig.IndexingAppConfig.Load();
							logSearchAppConfig.healthMonitoringLog = LogSearchAppConfig.HealthMonitoringLogAppConfig.Load();
							Thread.MemoryBarrier();
							LogSearchAppConfig.instance = logSearchAppConfig;
						}
					}
				}
				return LogSearchAppConfig.instance;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006A48 File Offset: 0x00004C48
		public static int GetConfigInt(string label, int min, int max, int defaultValue)
		{
			int result;
			try
			{
				result = TransportAppConfig.GetConfigInt(label, min, max, defaultValue);
			}
			catch (ConfigurationErrorsException ex)
			{
				LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_ErrorReadingAppConfig, DateTime.UtcNow.Hour.ToString(CultureInfo.InvariantCulture), new object[]
				{
					ex.ToString()
				});
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public static bool GetConfigBool(string label, bool defaultValue)
		{
			bool result;
			try
			{
				result = TransportAppConfig.GetConfigValue<bool>(label, defaultValue, new TransportAppConfig.TryParse<bool>(bool.TryParse));
			}
			catch (ConfigurationErrorsException ex)
			{
				LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_ErrorReadingAppConfig, DateTime.UtcNow.Hour.ToString(CultureInfo.InvariantCulture), new object[]
				{
					ex.ToString()
				});
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006B30 File Offset: 0x00004D30
		public static TimeSpan GetConfigTimeSpan(string label, TimeSpan min, TimeSpan max, TimeSpan defaultValue)
		{
			TimeSpan result;
			try
			{
				result = TransportAppConfig.GetConfigTimeSpan(label, min, max, defaultValue);
			}
			catch (ConfigurationErrorsException ex)
			{
				LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_ErrorReadingAppConfig, DateTime.UtcNow.Hour.ToString(CultureInfo.InvariantCulture), new object[]
				{
					ex.ToString()
				});
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public static string GetConfigString(string label, string defaultValue)
		{
			string result;
			try
			{
				result = TransportAppConfig.GetConfigString(label, defaultValue);
			}
			catch (ConfigurationErrorsException ex)
			{
				LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_ErrorReadingAppConfig, DateTime.UtcNow.Hour.ToString(CultureInfo.InvariantCulture), new object[]
				{
					ex.ToString()
				});
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00006C0C File Offset: 0x00004E0C
		public LogSearchAppConfig.IndexingAppConfig LogSearchIndexing
		{
			get
			{
				return this.logSearchIndexing;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00006C14 File Offset: 0x00004E14
		public LogSearchAppConfig.HealthMonitoringLogAppConfig HealthMonitoringLog
		{
			get
			{
				return this.healthMonitoringLog;
			}
		}

		// Token: 0x0400007F RID: 127
		private static object initializationLock = new object();

		// Token: 0x04000080 RID: 128
		private static LogSearchAppConfig instance;

		// Token: 0x04000081 RID: 129
		private LogSearchAppConfig.IndexingAppConfig logSearchIndexing;

		// Token: 0x04000082 RID: 130
		private LogSearchAppConfig.HealthMonitoringLogAppConfig healthMonitoringLog;

		// Token: 0x0200002D RID: 45
		internal sealed class IndexingAppConfig
		{
			// Token: 0x060000E1 RID: 225 RVA: 0x00006C28 File Offset: 0x00004E28
			private IndexingAppConfig()
			{
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x00006C30 File Offset: 0x00004E30
			public static LogSearchAppConfig.IndexingAppConfig Load()
			{
				return new LogSearchAppConfig.IndexingAppConfig
				{
					clusterRange = LogSearchAppConfig.GetConfigInt("ClusterRange", 0, 65536, 4096),
					refreshInterval = TimeSpan.FromSeconds((double)LogSearchAppConfig.GetConfigInt("RefreshIntervalSeconds", 1, 100, 10)),
					indexLimitMemoryPercentage = LogSearchAppConfig.GetConfigInt("IndexLimitMemoryPercentage", 0, 100, 5)
				};
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x00006C8E File Offset: 0x00004E8E
			public int ClusterRange
			{
				get
				{
					return this.clusterRange;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000E4 RID: 228 RVA: 0x00006C96 File Offset: 0x00004E96
			// (set) Token: 0x060000E5 RID: 229 RVA: 0x00006C9E File Offset: 0x00004E9E
			public TimeSpan RefreshInterval
			{
				get
				{
					return this.refreshInterval;
				}
				internal set
				{
					this.refreshInterval = value;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000E6 RID: 230 RVA: 0x00006CA7 File Offset: 0x00004EA7
			public int IndexLimitMemoryPercentage
			{
				get
				{
					return this.indexLimitMemoryPercentage;
				}
			}

			// Token: 0x04000083 RID: 131
			private const string ClusterRangeKey = "ClusterRange";

			// Token: 0x04000084 RID: 132
			private const string IndexLimitMemoryPercentageKey = "IndexLimitMemoryPercentage";

			// Token: 0x04000085 RID: 133
			private const string RefreshIntervalSecondsKey = "RefreshIntervalSeconds";

			// Token: 0x04000086 RID: 134
			private const int DefaultClusterRange = 4096;

			// Token: 0x04000087 RID: 135
			private const int DefaultRefreshIntervalSeconds = 10;

			// Token: 0x04000088 RID: 136
			private const int DefaultIndexLimitMemoryPercentage = 5;

			// Token: 0x04000089 RID: 137
			private int clusterRange;

			// Token: 0x0400008A RID: 138
			private TimeSpan refreshInterval;

			// Token: 0x0400008B RID: 139
			private int indexLimitMemoryPercentage;
		}

		// Token: 0x0200002E RID: 46
		internal sealed class HealthMonitoringLogAppConfig
		{
			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006CAF File Offset: 0x00004EAF
			public string HealthMonitoringLogPath
			{
				get
				{
					return this.healthMonitoringLogPath;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000E8 RID: 232 RVA: 0x00006CB7 File Offset: 0x00004EB7
			public TimeSpan HealthMonitoringLogMaxAge
			{
				get
				{
					return this.healthMonitoringLogMaxAge;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006CBF File Offset: 0x00004EBF
			public long HealthMonitoringLogMaxFileSize
			{
				get
				{
					return this.healthMonitoringLogMaxFile;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000EA RID: 234 RVA: 0x00006CC7 File Offset: 0x00004EC7
			public long HealthMonitoringLogMaxDirectorySize
			{
				get
				{
					return this.healthMonitoringLogMaxDirectorySize;
				}
			}

			// Token: 0x060000EB RID: 235 RVA: 0x00006CD0 File Offset: 0x00004ED0
			public static LogSearchAppConfig.HealthMonitoringLogAppConfig Load()
			{
				LogSearchAppConfig.HealthMonitoringLogAppConfig healthMonitoringLogAppConfig = new LogSearchAppConfig.HealthMonitoringLogAppConfig();
				int configInt = LogSearchAppConfig.GetConfigInt("HealthMonitoringLogMaxAgeDays", 1, 128, 2);
				healthMonitoringLogAppConfig.healthMonitoringLogMaxAge = TimeSpan.FromDays((double)configInt);
				long num = (long)LogSearchAppConfig.GetConfigInt("HealthMonitoringLogMaxFileSizeMB", 0, 50, 10);
				healthMonitoringLogAppConfig.healthMonitoringLogMaxFile = num * 1024L * 1024L;
				long num2 = (long)LogSearchAppConfig.GetConfigInt("HealthMonitoringLogMaxDirectorySizeMB", 0, 102400, 1024);
				healthMonitoringLogAppConfig.healthMonitoringLogMaxDirectorySize = num2 * 1024L * 1024L;
				string value = LogSearchAppConfig.GetConfigString("HealthMonitoringLogPath", string.Empty);
				if (string.IsNullOrEmpty(value))
				{
					string installPath = ConfigurationContext.Setup.InstallPath;
					if (string.IsNullOrEmpty(installPath))
					{
						LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_RegistryExchangeInstallPathNotFound, null, new object[0]);
						return null;
					}
					value = Path.Combine(installPath, "TransportRoles\\Logs\\HealthMonitoring");
				}
				healthMonitoringLogAppConfig.healthMonitoringLogPath = value;
				return healthMonitoringLogAppConfig;
			}

			// Token: 0x0400008C RID: 140
			private const string HealthMonitoringLogPathString = "HealthMonitoringLogPath";

			// Token: 0x0400008D RID: 141
			private const string HealthMonitoringLogMaxAgeDaysString = "HealthMonitoringLogMaxAgeDays";

			// Token: 0x0400008E RID: 142
			private const string HealthMonitoringLogMaxFileSizeString = "HealthMonitoringLogMaxFileSizeMB";

			// Token: 0x0400008F RID: 143
			private const string HealthMonitoringLogMaxDirectorySizeString = "HealthMonitoringLogMaxDirectorySizeMB";

			// Token: 0x04000090 RID: 144
			private string healthMonitoringLogPath;

			// Token: 0x04000091 RID: 145
			private TimeSpan healthMonitoringLogMaxAge;

			// Token: 0x04000092 RID: 146
			private long healthMonitoringLogMaxFile;

			// Token: 0x04000093 RID: 147
			private long healthMonitoringLogMaxDirectorySize;
		}
	}
}
