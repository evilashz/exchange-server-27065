using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x0200001B RID: 27
	internal class RequestMonitorLogger
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000050B9 File Offset: 0x000032B9
		internal RequestMonitorLogger(string logFolderPath = null)
		{
			this.LogFolderPath = logFolderPath;
			this.InitializeLogger();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000050CE File Offset: 0x000032CE
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000050D6 File Offset: 0x000032D6
		internal string LogFolderPath { get; set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x000050E0 File Offset: 0x000032E0
		internal void InitializeLogger()
		{
			string[] names = Enum.GetNames(typeof(RequestMonitorMetadata));
			this.logSchema = new LogSchema("Microsoft Exchange Server", "15.00.1497.010", "Request Monitor Logs", names);
			this.log = new Log("Request_Monitor_", new LogHeaderFormatter(this.logSchema, true), "RequestMonitor");
			string logFolderPath = this.LogFolderPath;
			this.log.Configure(logFolderPath, RequestMonitorLogger.maxAge, (long)(RequestMonitorLogger.maxDirectorySize * 1024 * 1024 * 1024), (long)(RequestMonitorLogger.maxLogFileSize * 1024 * 1024), true);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000517C File Offset: 0x0000337C
		internal void Commit(RequestMonitorContext context)
		{
			if (context == null)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			for (int i = 0; i < context.Fields.Length; i++)
			{
				logRowFormatter[i] = context[i];
			}
			this.log.Append(logRowFormatter, -1);
		}

		// Token: 0x04000074 RID: 116
		private const string LogType = "Request Monitor Logs";

		// Token: 0x04000075 RID: 117
		private const string LogFilePrefix = "Request_Monitor_";

		// Token: 0x04000076 RID: 118
		private const string LogComponent = "RequestMonitor";

		// Token: 0x04000077 RID: 119
		private static TimeSpan maxAge = TimeSpan.FromDays(30.0);

		// Token: 0x04000078 RID: 120
		private static int maxDirectorySize = 1;

		// Token: 0x04000079 RID: 121
		private static int maxLogFileSize = 10;

		// Token: 0x0400007A RID: 122
		private Log log;

		// Token: 0x0400007B RID: 123
		private LogSchema logSchema;
	}
}
