using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x02000009 RID: 9
	internal class DiagnosticsAggregationServiceletConfig
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000037C4 File Offset: 0x000019C4
		public DiagnosticsAggregationServiceletConfig()
		{
			this.Enabled = AppConfigLoader.GetConfigBoolValue("DiagnosticsAggregationServiceletEnabled", true);
			this.TimeSpanForQueueDataBeingCurrent = AppConfigLoader.GetConfigTimeSpanValue("TimeSpanForQueueDataBeingCurrent", TimeSpan.FromMinutes(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(11.0));
			this.TimeSpanForQueueDataBeingStale = AppConfigLoader.GetConfigTimeSpanValue("TimeSpanForQueueDataBeingStale", TimeSpan.FromMinutes(1.0), TimeSpan.FromHours(10.0), TimeSpan.FromHours(1.0));
			this.LoggingEnabled = AppConfigLoader.GetConfigBoolValue("DiagnosticsAggregationLoggingEnabled", false);
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string path = Path.Combine(Directory.GetParent(Path.GetDirectoryName(executingAssembly.Location)).FullName, "TransportRoles\\Logs\\DiagnosticsAggregation\\");
			this.LogFileDirectoryPath = AppConfigLoader.GetConfigStringValue("DiagnosticsAggregationLogFileDirectoryPath", Path.GetFullPath(path));
			int configIntValue = AppConfigLoader.GetConfigIntValue("DiagnosticsAggregationLogFileMaxSizeInMB", 1, 10000, 2);
			this.LogFileMaxSize = ByteQuantifiedSize.FromMB((ulong)((long)configIntValue));
			int configIntValue2 = AppConfigLoader.GetConfigIntValue("DiagnosticsAggregationLogFileMaxDirectorySizeInMB", 1, 10000, 10);
			this.LogFileMaxDirectorySize = ByteQuantifiedSize.FromMB((ulong)((long)configIntValue2));
			this.LogFileMaxAge = AppConfigLoader.GetConfigTimeSpanValue("DiagnosticsAggregationLogFileMaxAge", TimeSpan.FromMinutes(1.0), TimeSpan.FromDays(365.0), TimeSpan.FromDays(15.0));
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003922 File Offset: 0x00001B22
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000392A File Offset: 0x00001B2A
		public bool Enabled { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003933 File Offset: 0x00001B33
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000393B File Offset: 0x00001B3B
		public TimeSpan TimeSpanForQueueDataBeingCurrent { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003944 File Offset: 0x00001B44
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000394C File Offset: 0x00001B4C
		public TimeSpan TimeSpanForQueueDataBeingStale { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003955 File Offset: 0x00001B55
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000395D File Offset: 0x00001B5D
		public bool LoggingEnabled { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003966 File Offset: 0x00001B66
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000396E File Offset: 0x00001B6E
		public string LogFileDirectoryPath { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003977 File Offset: 0x00001B77
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000397F File Offset: 0x00001B7F
		public ByteQuantifiedSize LogFileMaxSize { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003988 File Offset: 0x00001B88
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00003990 File Offset: 0x00001B90
		public ByteQuantifiedSize LogFileMaxDirectorySize { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003999 File Offset: 0x00001B99
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000039A1 File Offset: 0x00001BA1
		public TimeSpan LogFileMaxAge { get; private set; }

		// Token: 0x04000037 RID: 55
		private const string DiagnosticsAggregationServiceletEnabledString = "DiagnosticsAggregationServiceletEnabled";

		// Token: 0x04000038 RID: 56
		private const string TimeSpanForQueueDataBeingCurrentString = "TimeSpanForQueueDataBeingCurrent";

		// Token: 0x04000039 RID: 57
		private const string TimeSpanForQueueDataBeingStaleString = "TimeSpanForQueueDataBeingStale";

		// Token: 0x0400003A RID: 58
		private const string LoggingEnabledString = "DiagnosticsAggregationLoggingEnabled";

		// Token: 0x0400003B RID: 59
		private const string LogFileDirectoryPathString = "DiagnosticsAggregationLogFileDirectoryPath";

		// Token: 0x0400003C RID: 60
		private const string LogFileMaxSizeString = "DiagnosticsAggregationLogFileMaxSizeInMB";

		// Token: 0x0400003D RID: 61
		private const string LogFileMaxDirectorySizeString = "DiagnosticsAggregationLogFileMaxDirectorySizeInMB";

		// Token: 0x0400003E RID: 62
		private const string LogFileMaxAgeString = "DiagnosticsAggregationLogFileMaxAge";
	}
}
