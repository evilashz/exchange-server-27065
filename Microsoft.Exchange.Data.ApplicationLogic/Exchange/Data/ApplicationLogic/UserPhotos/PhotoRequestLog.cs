using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000200 RID: 512
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoRequestLog
	{
		// Token: 0x060012AF RID: 4783 RVA: 0x0004E188 File Offset: 0x0004C388
		public PhotoRequestLog(PhotosConfiguration configuration, string logFileNamePrefix, string build)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNullOrEmpty("logFileNamePrefix", logFileNamePrefix);
			ArgumentValidator.ThrowIfNullOrEmpty("build", build);
			this.build = build;
			this.log = new Log(logFileNamePrefix, new LogHeaderFormatter(PhotoRequestLog.Schema), "photos");
			this.log.Configure(configuration.PhotoRequestLoggingPath, configuration.PhotoRequestLogFileMaxAge, configuration.PhotoRequestLogDirectoryMaxSize, configuration.PhotoRequestLogFileMaxSize);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0004E204 File Offset: 0x0004C404
		public void Log(DateTime timestamp, string requestId, string eventType, string message)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(PhotoRequestLog.Schema, true);
			logRowFormatter[1] = this.build;
			logRowFormatter[2] = Environment.MachineName;
			logRowFormatter[3] = requestId;
			logRowFormatter[4] = eventType;
			logRowFormatter[5] = message;
			this.log.Append(logRowFormatter, 0, timestamp);
		}

		// Token: 0x04000A46 RID: 2630
		private const string LogComponentName = "photos";

		// Token: 0x04000A47 RID: 2631
		private static readonly string[] LogColumns = new string[]
		{
			"Time",
			"Build",
			"Server",
			"RequestId",
			"EventType",
			"Message"
		};

		// Token: 0x04000A48 RID: 2632
		private static readonly LogSchema Schema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Photo Request log", PhotoRequestLog.LogColumns);

		// Token: 0x04000A49 RID: 2633
		private readonly string build;

		// Token: 0x04000A4A RID: 2634
		private Log log;

		// Token: 0x02000201 RID: 513
		private static class LogColumnIndices
		{
			// Token: 0x04000A4B RID: 2635
			public const int Time = 0;

			// Token: 0x04000A4C RID: 2636
			public const int Build = 1;

			// Token: 0x04000A4D RID: 2637
			public const int Server = 2;

			// Token: 0x04000A4E RID: 2638
			public const int RequestId = 3;

			// Token: 0x04000A4F RID: 2639
			public const int EventType = 4;

			// Token: 0x04000A50 RID: 2640
			public const int Message = 5;
		}
	}
}
