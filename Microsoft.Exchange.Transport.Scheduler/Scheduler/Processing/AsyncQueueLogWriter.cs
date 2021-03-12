using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000007 RID: 7
	internal sealed class AsyncQueueLogWriter : IQueueLogWriter
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000026CC File Offset: 0x000008CC
		public AsyncQueueLogWriter(string software, string logName, string logComponentName, string logFileNamePrefix, string logDirectory, TimeSpan maxAge, TimeSpan streamFlushInterval, TimeSpan backgroundWriteInterval, long maxDirectorySize = 0L, long maxLogFileSize = 0L, int bufferSize = 0)
		{
			ArgumentValidator.ThrowIfNull("software", software);
			ArgumentValidator.ThrowIfNull("logName", logName);
			ArgumentValidator.ThrowIfNull("logComponentName", logComponentName);
			ArgumentValidator.ThrowIfNull("logFileNamePrefix", logFileNamePrefix);
			ArgumentValidator.ThrowIfNull("logDirectory", logDirectory);
			ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("maxAge", maxAge, (TimeSpan interval) => interval > TimeSpan.Zero);
			ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("streamFlushInterval", streamFlushInterval, (TimeSpan interval) => interval > TimeSpan.Zero);
			ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("backgroundWriteInterval", backgroundWriteInterval, (TimeSpan interval) => interval > TimeSpan.Zero);
			ArgumentValidator.ThrowIfInvalidValue<long>("maxDirectorySize", maxDirectorySize, (long number) => number >= 0L);
			ArgumentValidator.ThrowIfInvalidValue<long>("maxLogFileSize", maxLogFileSize, (long number) => number >= 0L);
			ArgumentValidator.ThrowIfNegative("bufferSize", bufferSize);
			this.logSchema = new LogSchema(software, Assembly.GetExecutingAssembly().GetName().Version.ToString(), logName, AsyncQueueLogWriter.CreateHeaders());
			this.asyncLog = new AsyncLog(logFileNamePrefix, new LogHeaderFormatter(this.logSchema), logComponentName);
			this.asyncLog.Configure(logDirectory, maxAge, maxDirectorySize, maxLogFileSize, bufferSize, streamFlushInterval, backgroundWriteInterval);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000284C File Offset: 0x00000A4C
		public void Write(QueueLogInfo logInfo)
		{
			ArgumentValidator.ThrowIfNull("logInfo", logInfo);
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			logRowFormatter[0] = logInfo.TimeStamp;
			logRowFormatter[1] = logInfo.Display;
			logRowFormatter[2] = logInfo.Enqueues;
			logRowFormatter[3] = logInfo.Dequeues;
			logRowFormatter[4] = logInfo.Count;
			logRowFormatter[5] = logInfo.UsageData.ProcessingTicks;
			logRowFormatter[6] = logInfo.UsageData.MemoryUsed;
			logRowFormatter[7] = logInfo.UsageData.ProcessingTicks;
			this.asyncLog.Append(logRowFormatter, -1);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002917 File Offset: 0x00000B17
		internal void Flush()
		{
			this.asyncLog.Flush();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002924 File Offset: 0x00000B24
		private static string[] CreateHeaders()
		{
			string[] array = new string[Enum.GetValues(typeof(AsyncQueueLogWriter.Fields)).Length];
			array[0] = "Timestamp";
			array[1] = "Display";
			array[2] = "Enqueues";
			array[3] = "Dequeues";
			array[4] = "Count";
			array[5] = "TotalProcessingTicks";
			array[6] = "TotalMemoryUsed";
			array[7] = "TotalLockDuration";
			array[8] = "CustomData";
			return array;
		}

		// Token: 0x04000012 RID: 18
		private readonly LogSchema logSchema;

		// Token: 0x04000013 RID: 19
		private readonly AsyncLog asyncLog;

		// Token: 0x02000008 RID: 8
		internal enum Fields
		{
			// Token: 0x0400001A RID: 26
			Timestamp,
			// Token: 0x0400001B RID: 27
			Display,
			// Token: 0x0400001C RID: 28
			Enqueues,
			// Token: 0x0400001D RID: 29
			Dequeues,
			// Token: 0x0400001E RID: 30
			Count,
			// Token: 0x0400001F RID: 31
			TotalProcessingTicks,
			// Token: 0x04000020 RID: 32
			TotalMemoryUsed,
			// Token: 0x04000021 RID: 33
			TotalLockDuration,
			// Token: 0x04000022 RID: 34
			CustomData
		}
	}
}
