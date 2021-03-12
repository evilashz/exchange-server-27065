using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Common.Diagnostics
{
	// Token: 0x02000006 RID: 6
	public class LogConfig : ILogConfig
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002344 File Offset: 0x00000544
		public LogConfig(bool isLoggingEnabled, string logType, string logPrefix, string logPath, ulong? maxLogDirectorySize, ulong? maxLogFileSize, TimeSpan? maxLogAge, int logSessionLineCount = 4096)
		{
			ArgumentValidator.ThrowIfNull("logType", logType);
			ArgumentValidator.ThrowIfNull("logPrefix", logPrefix);
			ArgumentValidator.ThrowIfNull("logPath", logPath);
			ArgumentValidator.ThrowIfNegative("logSessionLineCount", logSessionLineCount);
			this.IsLoggingEnabled = isLoggingEnabled;
			this.SoftwareName = LogConfig.DefaultSoftwareName;
			this.SoftwareVersion = LogConfig.DefaultSoftwareVersion;
			this.ComponentName = LogConfig.DefaultComponentName;
			this.LogType = logType;
			this.LogPrefix = logPrefix;
			this.LogPath = logPath;
			this.MaxLogDirectorySize = ((maxLogDirectorySize != null) ? maxLogDirectorySize.Value : LogConfig.DefaultMaxLogDirectorySize);
			this.MaxLogFileSize = ((maxLogFileSize != null) ? maxLogFileSize.Value : LogConfig.DefaultMaxLogFileSize);
			this.MaxLogAge = ((maxLogAge != null) ? maxLogAge.Value : LogConfig.DefaultMaxLogAge);
			this.LogSessionLineCount = logSessionLineCount;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002422 File Offset: 0x00000622
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000242A File Offset: 0x0000062A
		public bool IsLoggingEnabled { get; protected set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002433 File Offset: 0x00000633
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000243B File Offset: 0x0000063B
		public string SoftwareName { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002444 File Offset: 0x00000644
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000244C File Offset: 0x0000064C
		public string SoftwareVersion { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002455 File Offset: 0x00000655
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000245D File Offset: 0x0000065D
		public string ComponentName { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002466 File Offset: 0x00000666
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000246E File Offset: 0x0000066E
		public string LogType { get; protected set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002477 File Offset: 0x00000677
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000247F File Offset: 0x0000067F
		public string LogPrefix { get; protected set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002488 File Offset: 0x00000688
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002490 File Offset: 0x00000690
		public string LogPath { get; protected set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002499 File Offset: 0x00000699
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000024A1 File Offset: 0x000006A1
		public ulong MaxLogDirectorySize { get; protected set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000024AA File Offset: 0x000006AA
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000024B2 File Offset: 0x000006B2
		public ulong MaxLogFileSize { get; protected set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000024BB File Offset: 0x000006BB
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000024C3 File Offset: 0x000006C3
		public TimeSpan MaxLogAge { get; protected set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000024CC File Offset: 0x000006CC
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000024D4 File Offset: 0x000006D4
		public int LogSessionLineCount { get; protected set; }

		// Token: 0x04000009 RID: 9
		public static readonly string DefaultSoftwareName = "Microsoft Exchange Server";

		// Token: 0x0400000A RID: 10
		public static readonly string DefaultSoftwareVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x0400000B RID: 11
		public static readonly string DefaultComponentName = "Inference";

		// Token: 0x0400000C RID: 12
		public static readonly ulong DefaultMaxLogDirectorySize = 1073741824UL;

		// Token: 0x0400000D RID: 13
		public static readonly ulong DefaultMaxLogFileSize = 10485760UL;

		// Token: 0x0400000E RID: 14
		public static readonly TimeSpan DefaultMaxLogAge = TimeSpan.FromDays(1.0);
	}
}
