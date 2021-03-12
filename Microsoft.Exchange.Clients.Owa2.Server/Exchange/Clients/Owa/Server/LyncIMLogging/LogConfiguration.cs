using System;
using System.Configuration;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Clients.Owa.Server.LyncIMLogging
{
	// Token: 0x0200014B RID: 331
	internal class LogConfiguration
	{
		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002DFC8 File Offset: 0x0002C1C8
		internal static SyncLogConfiguration CreateSyncLogConfiguration()
		{
			return new SyncLogConfiguration
			{
				Enabled = LogConfiguration.GetAppSetting<bool>("LyncIMSyncLogEnabled", true),
				AgeQuotaInHours = LogConfiguration.GetAppSetting<long>("LyncIMSyncLogAgeQuota", 168L),
				DirectorySizeQuota = LogConfiguration.GetAppSetting<long>("LyncIMSyncLogDirectorySizeQuota", 256000L),
				PerFileSizeQuota = LogConfiguration.GetAppSetting<long>("LyncIMSyncLogFileSizeQuota", 10240L),
				LogFilePath = LogConfiguration.GetAppSetting<string>("LyncIMSyncLogFilePath", "Logging\\OWA\\InstantMessaging"),
				SyncLoggingLevel = LogConfiguration.GetAppSetting<SyncLoggingLevel>("LyncIMSyncLogLevel", SyncLoggingLevel.Debugging)
			};
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002E058 File Offset: 0x0002C258
		internal static T GetAppSetting<T>(string key, T defaultValue)
		{
			try
			{
				string value = ConfigurationManager.AppSettings.Get(key);
				if (!string.IsNullOrWhiteSpace(value))
				{
					if (typeof(T).IsEnum)
					{
						return (T)((object)Enum.Parse(typeof(T), value, true));
					}
					return (T)((object)Convert.ChangeType(value, typeof(T)));
				}
			}
			catch (Exception)
			{
				return defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x040007A6 RID: 1958
		private const int DefaultLogFileAgeInHours = 168;

		// Token: 0x040007A7 RID: 1959
		private const int DefaultLogDirectoryQuotaInKB = 256000;

		// Token: 0x040007A8 RID: 1960
		private const int DefaultPerFileQuotaInKB = 10240;
	}
}
