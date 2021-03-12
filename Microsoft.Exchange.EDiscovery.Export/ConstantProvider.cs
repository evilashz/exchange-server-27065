using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000005 RID: 5
	internal static class ConstantProvider
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000237C File Offset: 0x0000057C
		static ConstantProvider()
		{
			try
			{
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Exchange\\Client\\eDiscovery\\ExportTool"))
					{
						if (registryKey2 != null)
						{
							ConstantProvider.SearchMailboxesPageSize = ConstantProvider.GetValue<int>(registryKey2, "SearchMailboxesPageSize", 500, new ConstantProvider.TryParse<int>(int.TryParse));
							ConstantProvider.ExportBatchItemCountLimit = ConstantProvider.GetValue<int>(registryKey2, "ExportBatchItemCountLimit", 250, new ConstantProvider.TryParse<int>(int.TryParse));
							ConstantProvider.ExportBatchSizeLimit = ConstantProvider.GetValue<int>(registryKey2, "ExportBatchSizeLimit", 5242880, new ConstantProvider.TryParse<int>(int.TryParse));
							ConstantProvider.ItemIdListCacheSize = ConstantProvider.GetValue<int>(registryKey2, "ItemIdListCacheSize", 500, new ConstantProvider.TryParse<int>(int.TryParse));
							ConstantProvider.RetryInterval = ConstantProvider.GetValue<int>(registryKey2, "RetryInterval", 30000, new ConstantProvider.TryParse<int>(int.TryParse));
							ConstantProvider.AutoDiscoverBatchSize = ConstantProvider.GetValue<int>(registryKey2, "AutoDiscoverBatchSize", 50, new ConstantProvider.TryParse<int>(int.TryParse));
							ConstantProvider.maxCSVLogFileSizeInBytes = ConstantProvider.GetValue<int>(registryKey2, "MaxCSVLogFileSizeInBytes", 104857600, new ConstantProvider.TryParse<int>(int.TryParse));
							ConstantProvider.pstSizeLimitInBytes = ConstantProvider.GetValue<long>(registryKey2, "PSTSizeLimitInBytes", 10000000000L, new ConstantProvider.TryParse<long>(long.TryParse));
							ConstantProvider.TotalRetryTimeWindow = ConstantProvider.GetValue<TimeSpan>(registryKey2, "TotalRetryTimeWindow", DiscoveryConstants.DefaultTotalRetryTimeWindow, new ConstantProvider.TryParse<TimeSpan>(TimeSpan.TryParse));
							ConstantProvider.RetrySchedule = ConstantProvider.GetArrayValue<TimeSpan>(registryKey2, "RetrySchedule", DiscoveryConstants.DefaultRetrySchedule, new ConstantProvider.TryParse<TimeSpan>(TimeSpan.TryParse));
							ConstantProvider.RebindWithAutoDiscoveryEnabled = ConstantProvider.GetValue<bool>(registryKey2, "RebindWithAutoDiscoveryEnabled", false, new ConstantProvider.TryParse<bool>(bool.TryParse));
							ConstantProvider.RebindAutoDiscoveryUrl = null;
							ConstantProvider.RebindAutoDiscoveryInternalUrlOnly = true;
							ConstantProvider.SearchStatisticsEnabled = ConstantProvider.GetValue<bool>(registryKey2, "SearchStatisticsEnabled", false, new ConstantProvider.TryParse<bool>(bool.TryParse));
							int value = ConstantProvider.GetValue<int>(registryKey2, "partitionCSVLogFile", 0, new ConstantProvider.TryParse<int>(int.TryParse));
							if (value == 1)
							{
								ConstantProvider.partitionCSVLogFile = true;
							}
						}
						else
						{
							Tracer.TraceInformation("ConstantProvider..Ctor: Registry not found for constants", new object[0]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Tracer.TraceError("ConstantProvider..Ctor: Failed to load registry data. Details: {0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000266C File Offset: 0x0000086C
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002673 File Offset: 0x00000873
		public static int SearchMailboxesPageSize
		{
			get
			{
				return ConstantProvider.searchMailboxesPageSize;
			}
			internal set
			{
				ConstantProvider.searchMailboxesPageSize = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000267B File Offset: 0x0000087B
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002682 File Offset: 0x00000882
		public static int ExportBatchItemCountLimit
		{
			get
			{
				return ConstantProvider.exportBatchItemCountLimit;
			}
			internal set
			{
				ConstantProvider.exportBatchItemCountLimit = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000268A File Offset: 0x0000088A
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002691 File Offset: 0x00000891
		public static int ExportBatchSizeLimit
		{
			get
			{
				return ConstantProvider.exportBatchSizeLimit;
			}
			internal set
			{
				ConstantProvider.exportBatchSizeLimit = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002699 File Offset: 0x00000899
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000026A0 File Offset: 0x000008A0
		public static int ItemIdListCacheSize
		{
			get
			{
				return ConstantProvider.itemIdListCacheSize;
			}
			internal set
			{
				ConstantProvider.itemIdListCacheSize = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000026AF File Offset: 0x000008AF
		public static int RetryInterval
		{
			get
			{
				return ConstantProvider.retryInterval;
			}
			internal set
			{
				ConstantProvider.retryInterval = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000026B7 File Offset: 0x000008B7
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000026BE File Offset: 0x000008BE
		public static TimeSpan TotalRetryTimeWindow
		{
			get
			{
				return ConstantProvider.totalRetryTimeWindow;
			}
			internal set
			{
				ConstantProvider.totalRetryTimeWindow = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000026C6 File Offset: 0x000008C6
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000026CD File Offset: 0x000008CD
		public static TimeSpan[] RetrySchedule
		{
			get
			{
				return ConstantProvider.retrySchedule;
			}
			internal set
			{
				ConstantProvider.retrySchedule = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000026D5 File Offset: 0x000008D5
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000026DC File Offset: 0x000008DC
		public static int AutoDiscoverBatchSize
		{
			get
			{
				return ConstantProvider.autoDiscoverBatchSize;
			}
			internal set
			{
				ConstantProvider.autoDiscoverBatchSize = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000026E4 File Offset: 0x000008E4
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000026EB File Offset: 0x000008EB
		public static int MaxCSVLogFileSizeInBytes
		{
			get
			{
				return ConstantProvider.maxCSVLogFileSizeInBytes;
			}
			internal set
			{
				ConstantProvider.maxCSVLogFileSizeInBytes = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000026F3 File Offset: 0x000008F3
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000026FA File Offset: 0x000008FA
		public static long PSTSizeLimitInBytes
		{
			get
			{
				return ConstantProvider.pstSizeLimitInBytes;
			}
			internal set
			{
				ConstantProvider.pstSizeLimitInBytes = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002702 File Offset: 0x00000902
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002709 File Offset: 0x00000909
		public static bool PartitionCSVLogFile
		{
			get
			{
				return ConstantProvider.partitionCSVLogFile;
			}
			internal set
			{
				ConstantProvider.partitionCSVLogFile = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002711 File Offset: 0x00000911
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002718 File Offset: 0x00000918
		public static bool RebindWithAutoDiscoveryEnabled { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002720 File Offset: 0x00000920
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002727 File Offset: 0x00000927
		public static bool RebindAutoDiscoveryInternalUrlOnly { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000272F File Offset: 0x0000092F
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002736 File Offset: 0x00000936
		public static Uri RebindAutoDiscoveryUrl { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000273E File Offset: 0x0000093E
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002745 File Offset: 0x00000945
		public static bool SearchStatisticsEnabled { get; set; }

		// Token: 0x06000032 RID: 50 RVA: 0x00002750 File Offset: 0x00000950
		private static T GetValue<T>(RegistryKey configuration, string name, T defaultValue, ConstantProvider.TryParse<T> tryParse)
		{
			string text = configuration.GetValue(name) as string;
			T result;
			if (text == null || !tryParse(text, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000277C File Offset: 0x0000097C
		private static T[] GetArrayValue<T>(RegistryKey configuration, string name, T[] defaultValue, ConstantProvider.TryParse<T> tryParse)
		{
			string[] array = configuration.GetValue(name) as string[];
			T[] array2;
			if (array == null || array.Length == 0)
			{
				array2 = defaultValue;
			}
			else
			{
				array2 = new T[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					if (!tryParse(array[i], out array2[i]))
					{
						array2 = defaultValue;
						break;
					}
				}
			}
			return array2;
		}

		// Token: 0x04000007 RID: 7
		private static int searchMailboxesPageSize = 500;

		// Token: 0x04000008 RID: 8
		private static int exportBatchItemCountLimit = 250;

		// Token: 0x04000009 RID: 9
		private static int exportBatchSizeLimit = 5242880;

		// Token: 0x0400000A RID: 10
		private static int itemIdListCacheSize = 500;

		// Token: 0x0400000B RID: 11
		private static int retryInterval = 30000;

		// Token: 0x0400000C RID: 12
		private static TimeSpan totalRetryTimeWindow = DiscoveryConstants.DefaultTotalRetryTimeWindow;

		// Token: 0x0400000D RID: 13
		private static TimeSpan[] retrySchedule = DiscoveryConstants.DefaultRetrySchedule;

		// Token: 0x0400000E RID: 14
		private static int autoDiscoverBatchSize = 50;

		// Token: 0x0400000F RID: 15
		private static int maxCSVLogFileSizeInBytes = 104857600;

		// Token: 0x04000010 RID: 16
		private static long pstSizeLimitInBytes = 10000000000L;

		// Token: 0x04000011 RID: 17
		private static bool partitionCSVLogFile = true;

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x06000035 RID: 53
		private delegate bool TryParse<T>(string config, out T parsedConfig);
	}
}
