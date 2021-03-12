using System;
using System.Collections.Generic;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200000A RID: 10
	internal static class AppConfigReader
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00003AE8 File Offset: 0x00001CE8
		static AppConfigReader()
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			AppConfigReader.LogConfigurationSection = (LogConfiguration)configuration.GetSection("LogUploaderConfiguration");
			if (AppConfigReader.LogConfigurationSection == null)
			{
				throw new ConfigurationErrorsException("LogUploaderConfiguration section does not exist in app.config.");
			}
			AppConfigReader.DalStubSettings = (DalStubConfig)configuration.GetSection("DalStubConfig");
			if (AppConfigReader.DalStubSettings != null)
			{
				AppConfigReader.DalStubSettings.Partitions.BuildSearchList();
			}
			TimeSpan nonExistentDirectoryCheckInterval;
			if (TimeSpan.TryParse(AppConfigReader.GetAppSettingStringValue("NonExistentDirectoryCheckInterval"), out nonExistentDirectoryCheckInterval))
			{
				AppConfigReader.NonExistentDirectoryCheckInterval = nonExistentDirectoryCheckInterval;
			}
			else
			{
				AppConfigReader.NonExistentDirectoryCheckInterval = TimeSpan.FromMinutes(5.0);
			}
			bool skipLinesFromSprodMsit;
			if (bool.TryParse(AppConfigReader.GetAppSettingStringValue("SkipLinesFromSprodMSIT"), out skipLinesFromSprodMsit))
			{
				AppConfigReader.SkipLinesFromSprodMsit = skipLinesFromSprodMsit;
			}
			else
			{
				AppConfigReader.SkipLinesFromSprodMsit = false;
			}
			bool useDefaultRegionTag;
			if (bool.TryParse(AppConfigReader.GetAppSettingStringValue("UseDefaultRegionTag"), out useDefaultRegionTag))
			{
				AppConfigReader.UseDefaultRegionTag = useDefaultRegionTag;
			}
			else
			{
				AppConfigReader.UseDefaultRegionTag = false;
			}
			TimeSpan persistentStoreDetailsRecheckTimerInterval;
			if (TimeSpan.TryParse(AppConfigReader.GetAppSettingStringValue("PersistentStoreDetailsRecheckTimerInterval"), out persistentStoreDetailsRecheckTimerInterval))
			{
				AppConfigReader.PersistentStoreDetailsRecheckTimerInterval = persistentStoreDetailsRecheckTimerInterval;
			}
			else
			{
				AppConfigReader.PersistentStoreDetailsRecheckTimerInterval = TimeSpan.FromHours(1.0);
			}
			int num;
			if (int.TryParse(AppConfigReader.GetAppSettingStringValue("LogProcessingCutOffDays"), out num) && num > 0)
			{
				AppConfigReader.LogProcessingCutOffDays = TimeSpan.FromDays((double)num);
			}
			else
			{
				AppConfigReader.LogProcessingCutOffDays = TimeSpan.FromDays(7.0);
			}
			TimeSpan newerLogCutOffTimeSpan;
			if (TimeSpan.TryParse(AppConfigReader.GetAppSettingStringValue("NewerLogCutOffTimeSpan"), out newerLogCutOffTimeSpan))
			{
				AppConfigReader.NewerLogCutOffTimeSpan = newerLogCutOffTimeSpan;
			}
			else
			{
				AppConfigReader.NewerLogCutOffTimeSpan = TimeSpan.FromHours(2.0);
			}
			int num2;
			if (int.TryParse(AppConfigReader.GetAppSettingStringValue("MaxNumberOfMessagesInBatch"), out num2) && num2 > 0)
			{
				AppConfigReader.MaxNumberOfMessagesInBatch = num2;
			}
			else
			{
				AppConfigReader.MaxNumberOfMessagesInBatch = 200;
			}
			if (!int.TryParse(AppConfigReader.GetAppSettingStringValue("MaxNumberOfEventsInBatch"), out AppConfigReader.MaxNumberOfEventsInBatch) || AppConfigReader.MaxNumberOfEventsInBatch <= 0)
			{
				AppConfigReader.MaxNumberOfEventsInBatch = 1000;
			}
			if (!int.TryParse(AppConfigReader.GetAppSettingStringValue("MaxNumberOfRecipientsInBatch"), out AppConfigReader.MaxNumberOfRecipientsInBatch) || AppConfigReader.MaxNumberOfRecipientsInBatch <= 0)
			{
				AppConfigReader.MaxNumberOfRecipientsInBatch = 1000;
			}
			TimeSpan maxWaitTimeBeforeAlertOnBackLog;
			if (TimeSpan.TryParse(AppConfigReader.GetAppSettingStringValue("MaxWaitTimeBeforeAlertOnBackLog"), out maxWaitTimeBeforeAlertOnBackLog))
			{
				AppConfigReader.MaxWaitTimeBeforeAlertOnBackLog = maxWaitTimeBeforeAlertOnBackLog;
			}
			else
			{
				AppConfigReader.MaxWaitTimeBeforeAlertOnBackLog = TimeSpan.FromMinutes(5.0);
			}
			TimeSpan timeSpan;
			if (TimeSpan.TryParse(AppConfigReader.GetAppSettingStringValue("WriterFailureSampleExpirationTime"), out timeSpan))
			{
				AppConfigReader.WriterFailureSampleExpirationTimeSpan = timeSpan;
			}
			else
			{
				AppConfigReader.WriterFailureSampleExpirationTimeSpan = TimeSpan.FromMinutes(15.0);
			}
			if (TimeSpan.TryParse(AppConfigReader.GetAppSettingStringValue("WriterLongLatencyFailureThreshold"), out timeSpan))
			{
				AppConfigReader.WriterLongLatencyFailureThresholdTimeSpan = timeSpan;
			}
			else
			{
				AppConfigReader.WriterLongLatencyFailureThresholdTimeSpan = TimeSpan.FromSeconds(120.0);
			}
			int num3;
			if (int.TryParse(AppConfigReader.GetAppSettingStringValue("WriterConsecutiveFailureHealthyThreshold"), out num3) && num3 > 0)
			{
				AppConfigReader.WriterConsecutiveFailureHealthyThresholdCount = num3;
				return;
			}
			AppConfigReader.WriterConsecutiveFailureHealthyThresholdCount = 10;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003D88 File Offset: 0x00001F88
		internal static IEnumerable<LogTypeInstance> GetLogTypeInstancesInEnvironment(string envName, string region = "")
		{
			ProcessingEnvironmentCollection environments = AppConfigReader.LogConfigurationSection.Environments;
			List<LogTypeInstance> list = new List<LogTypeInstance>();
			foreach (object obj in environments)
			{
				ProcessingEnvironment processingEnvironment = (ProcessingEnvironment)obj;
				if (string.Compare(processingEnvironment.EnvironmentName, envName, true) == 0 && (string.IsNullOrWhiteSpace(region) || string.IsNullOrWhiteSpace(processingEnvironment.Region) || string.Compare(processingEnvironment.Region, region, true) == 0))
				{
					foreach (object obj2 in processingEnvironment.Logs)
					{
						LogTypeInstance item = (LogTypeInstance)obj2;
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003E78 File Offset: 0x00002078
		internal static ConfigInstance GetConfigurationByName(string name)
		{
			return AppConfigReader.LogConfigurationSection.ConfigSettings.Get(name);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003E8A File Offset: 0x0000208A
		internal static ConfigInstance GetDefaultConfiguration()
		{
			return AppConfigReader.LogConfigurationSection.ConfigSettings.Get("Default");
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003EA0 File Offset: 0x000020A0
		internal static string GetAppSettingStringValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		// Token: 0x04000046 RID: 70
		private const string DefaultConfigSettingName = "Default";

		// Token: 0x04000047 RID: 71
		private const string SkipLinesFromSprodMsitKey = "SkipLinesFromSprodMSIT";

		// Token: 0x04000048 RID: 72
		private const string PersistentStoreDetailsRecheckTimerIntervalKey = "PersistentStoreDetailsRecheckTimerInterval";

		// Token: 0x04000049 RID: 73
		private const string LogProcessingCutOffDaysKey = "LogProcessingCutOffDays";

		// Token: 0x0400004A RID: 74
		private const string NewerLogCutOffTimeSpanKey = "NewerLogCutOffTimeSpan";

		// Token: 0x0400004B RID: 75
		private const string NonExistentDirectoryCheckIntervalKey = "NonExistentDirectoryCheckInterval";

		// Token: 0x0400004C RID: 76
		private const string UseDefaultRegionTagKey = "UseDefaultRegionTag";

		// Token: 0x0400004D RID: 77
		private const string MaxNumberOfMessagesInBatchKey = "MaxNumberOfMessagesInBatch";

		// Token: 0x0400004E RID: 78
		private const string MaxWaitTimeBeforeAlertOnBackLogKey = "MaxWaitTimeBeforeAlertOnBackLog";

		// Token: 0x0400004F RID: 79
		internal static readonly LogConfiguration LogConfigurationSection;

		// Token: 0x04000050 RID: 80
		internal static readonly DalStubConfig DalStubSettings;

		// Token: 0x04000051 RID: 81
		internal static readonly bool SkipLinesFromSprodMsit;

		// Token: 0x04000052 RID: 82
		internal static readonly TimeSpan PersistentStoreDetailsRecheckTimerInterval;

		// Token: 0x04000053 RID: 83
		internal static readonly TimeSpan LogProcessingCutOffDays;

		// Token: 0x04000054 RID: 84
		internal static readonly TimeSpan NewerLogCutOffTimeSpan;

		// Token: 0x04000055 RID: 85
		internal static readonly TimeSpan NonExistentDirectoryCheckInterval;

		// Token: 0x04000056 RID: 86
		internal static readonly bool UseDefaultRegionTag;

		// Token: 0x04000057 RID: 87
		internal static readonly int MaxNumberOfMessagesInBatch;

		// Token: 0x04000058 RID: 88
		internal static readonly int MaxNumberOfEventsInBatch;

		// Token: 0x04000059 RID: 89
		internal static readonly int MaxNumberOfRecipientsInBatch;

		// Token: 0x0400005A RID: 90
		internal static readonly TimeSpan MaxWaitTimeBeforeAlertOnBackLog;

		// Token: 0x0400005B RID: 91
		internal static readonly TimeSpan WriterFailureSampleExpirationTimeSpan;

		// Token: 0x0400005C RID: 92
		internal static readonly TimeSpan WriterLongLatencyFailureThresholdTimeSpan;

		// Token: 0x0400005D RID: 93
		internal static readonly int WriterConsecutiveFailureHealthyThresholdCount;
	}
}
