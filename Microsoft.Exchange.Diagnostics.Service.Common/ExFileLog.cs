using System;
using System.Threading;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x0200000B RID: 11
	internal class ExFileLog : ILog, IDisposable
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000044A8 File Offset: 0x000026A8
		public ExFileLog(string logDirectory)
		{
			string[] fields = new string[]
			{
				"date-time",
				"log-level",
				"thread-id",
				"message"
			};
			this.logSchema = new LogSchema("Microsoft Exchange Server", "15.0.0.0", "Diagnostics Service Log", fields);
			LogHeaderFormatter headerFormatter = new LogHeaderFormatter(this.logSchema, LogHeaderCsvOption.CsvStrict);
			this.exlog = new Log("DiagnosticsServiceLog", headerFormatter, "Diagnostics Service Log");
			this.exlog.Configure(logDirectory, TimeSpan.Zero, 104857600L, 10485760L, 1024, TimeSpan.FromSeconds(5.0), true);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00004553 File Offset: 0x00002753
		public void Dispose()
		{
			if (this.exlog != null)
			{
				this.exlog.Close();
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00004568 File Offset: 0x00002768
		public void LogInformationMessage(string format, params object[] args)
		{
			this.WriteLine("Information", format, args);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00004577 File Offset: 0x00002777
		public void LogWarningMessage(string format, params object[] args)
		{
			this.WriteLine("Warning", format, args);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00004586 File Offset: 0x00002786
		public void LogErrorMessage(string format, params object[] args)
		{
			this.WriteLine("Error", format, args);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00004598 File Offset: 0x00002798
		private void WriteLine(string entryType, string format, params object[] args)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			logRowFormatter[1] = entryType;
			logRowFormatter[2] = Thread.CurrentThread.ManagedThreadId;
			logRowFormatter[3] = CommonUtils.FoldIntoSingleLine(string.Format(format, args));
			this.exlog.Append(logRowFormatter, 0);
		}

		// Token: 0x040002C7 RID: 711
		private const string ComponentName = "Diagnostics Service Log";

		// Token: 0x040002C8 RID: 712
		private const string LogFilePrefix = "DiagnosticsServiceLog";

		// Token: 0x040002C9 RID: 713
		private readonly Log exlog;

		// Token: 0x040002CA RID: 714
		private readonly LogSchema logSchema;

		// Token: 0x0200000C RID: 12
		private enum LogFields
		{
			// Token: 0x040002CC RID: 716
			DateTime,
			// Token: 0x040002CD RID: 717
			LogLevel,
			// Token: 0x040002CE RID: 718
			ThreadId,
			// Token: 0x040002CF RID: 719
			Message
		}
	}
}
