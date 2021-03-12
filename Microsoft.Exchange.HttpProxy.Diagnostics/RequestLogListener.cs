using System;
using System.Collections;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000D RID: 13
	internal class RequestLogListener
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002C60 File Offset: 0x00000E60
		static RequestLogListener()
		{
			RequestLogListener.log = new Log(DiagnosticsConfiguration.LogFileNamePrefix.Value, new LogHeaderFormatter(RequestLogListener.logSchema, true), "LogComponent");
			RequestLogListener.log.Configure(DiagnosticsConfiguration.LogFolderPath.Value, TimeSpan.FromDays((double)DiagnosticsConfiguration.MaxLogRetentionInDays.Value), (long)(DiagnosticsConfiguration.MaxLogDirectorySizeInGB.Value * 1024 * 1024 * 1024), (long)(DiagnosticsConfiguration.MaxLogFileSizeInMB.Value * 1024 * 1024), true);
			ThreadPool.QueueUserWorkItem(new WaitCallback(RequestLogListener.CommitLogLines));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D34 File Offset: 0x00000F34
		public static void AppendLog(LogData logData)
		{
			RequestLogListener.logQueue.Enqueue(logData);
			RequestLogListener.logCommitSignal.Set();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D4C File Offset: 0x00000F4C
		public static void FlushLogLines()
		{
			RequestLogListener.logCommitSignal.Set();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D5C File Offset: 0x00000F5C
		private static void CommitLog(LogData logData)
		{
			if (logData == null)
			{
				throw new ArgumentNullException("logData");
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(RequestLogListener.logSchema);
			foreach (LogKey logKey in LogData.LogKeys)
			{
				object obj = logData[logKey];
				if (obj != null)
				{
					int index = (int)logKey;
					logRowFormatter[index] = obj;
				}
			}
			RequestLogListener.log.Append(logRowFormatter, -1);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DC4 File Offset: 0x00000FC4
		private static void CommitLogLines(object state)
		{
			for (;;)
			{
				if (RequestLogListener.logQueue.Count <= 0)
				{
					RequestLogListener.logCommitSignal.WaitOne();
				}
				else
				{
					LogData logData = RequestLogListener.logQueue.Dequeue() as LogData;
					RequestLogListener.CommitLog(logData);
				}
			}
		}

		// Token: 0x04000063 RID: 99
		private static Log log;

		// Token: 0x04000064 RID: 100
		private static Queue logQueue = Queue.Synchronized(new Queue());

		// Token: 0x04000065 RID: 101
		private static AutoResetEvent logCommitSignal = new AutoResetEvent(false);

		// Token: 0x04000066 RID: 102
		private static LogSchema logSchema = new LogSchema("Microsoft Exchange Server", "15.00.1497.010", "ProxyLogs", LogData.LogColumnNames);
	}
}
