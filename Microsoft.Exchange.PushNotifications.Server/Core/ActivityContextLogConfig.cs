using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Server.Core
{
	// Token: 0x02000002 RID: 2
	internal sealed class ActivityContextLogConfig : ILogConfiguration
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public ActivityContextLogConfig()
		{
			this.IsLoggingEnabled = ActivityContextLogConfig.IsActivityContextLogEnabled();
			this.LogPath = AppConfigLoader.GetConfigStringValue("ActivityContextLogPath", ActivityContextLogConfig.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("ActivityContextMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(7.0));
			this.MaxLogDirectorySizeInBytes = (long)ActivityContextLogConfig.GetConfigByteQuantifiedSizeValue("ActivityContextMaxDirectorySize", ByteQuantifiedSize.FromGB(1UL)).ToBytes();
			this.MaxLogFileSizeInBytes = (long)ActivityContextLogConfig.GetConfigByteQuantifiedSizeValue("ActivityContextMaxLogSize", ByteQuantifiedSize.FromMB(10UL)).ToBytes();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000216A File Offset: 0x0000036A
		public static string DefaultLogPath
		{
			get
			{
				if (ActivityContextLogConfig.defaultLogPath == null)
				{
					ActivityContextLogConfig.defaultLogPath = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\PushNotifications");
				}
				return ActivityContextLogConfig.defaultLogPath;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000218C File Offset: 0x0000038C
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002194 File Offset: 0x00000394
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000219D File Offset: 0x0000039D
		public bool IsActivityEventHandler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021A0 File Offset: 0x000003A0
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000021A8 File Offset: 0x000003A8
		public string LogPath { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021B1 File Offset: 0x000003B1
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021B9 File Offset: 0x000003B9
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021C2 File Offset: 0x000003C2
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021CA File Offset: 0x000003CA
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021D3 File Offset: 0x000003D3
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021DB File Offset: 0x000003DB
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021E4 File Offset: 0x000003E4
		public string LogComponent
		{
			get
			{
				return "PushNotificationLog";
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021EB File Offset: 0x000003EB
		public string LogPrefix
		{
			get
			{
				return "PushNotification";
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021F2 File Offset: 0x000003F2
		public string LogType
		{
			get
			{
				return "PushNotification Server Log";
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F9 File Offset: 0x000003F9
		internal static bool IsActivityContextLogEnabled()
		{
			return AppConfigLoader.GetConfigBoolValue("ActivityContextLoggingEnabled", false);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002208 File Offset: 0x00000408
		private static ByteQuantifiedSize GetConfigByteQuantifiedSizeValue(string configName, ByteQuantifiedSize defaultValue)
		{
			string expression = null;
			ByteQuantifiedSize result;
			if (AppConfigLoader.TryGetConfigRawValue(configName, out expression) && ByteQuantifiedSize.TryParse(expression, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x04000001 RID: 1
		public const string ActivityContextLoggingEnabled = "ActivityContextLoggingEnabled";

		// Token: 0x04000002 RID: 2
		public const string ActivityContextLogPath = "ActivityContextLogPath";

		// Token: 0x04000003 RID: 3
		public const string ActivityContextMaxLogAge = "ActivityContextMaxLogAge";

		// Token: 0x04000004 RID: 4
		public const string ActivityContextMaxDirectorySize = "ActivityContextMaxDirectorySize";

		// Token: 0x04000005 RID: 5
		public const string ActivityContextMaxLogSize = "ActivityContextMaxLogSize";

		// Token: 0x04000006 RID: 6
		public const string LogPrefixValue = "PushNotification";

		// Token: 0x04000007 RID: 7
		private const string LogTypeValue = "PushNotification Server Log";

		// Token: 0x04000008 RID: 8
		private const string LogComponentValue = "PushNotificationLog";

		// Token: 0x04000009 RID: 9
		private const string DefaultRelativeFilePath = "Logging\\PushNotifications";

		// Token: 0x0400000A RID: 10
		private static string defaultLogPath;
	}
}
