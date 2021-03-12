using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200016F RID: 367
	internal class TraceFileLogger
	{
		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002AC44 File Offset: 0x00028E44
		internal TraceFileLogger(bool forcedLogging)
		{
			this.isForcedLogging = forcedLogging;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.processName = currentProcess.MainModule.ModuleName;
				this.processId = currentProcess.Id;
			}
			this.isProcessAllowedInDatacenter = !this.processName.Contains("MSExchangeMailboxAssistants");
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002AD4C File Offset: 0x00028F4C
		internal void TraceDebug(object context, string callId, string message)
		{
			this.TraceToLog(TraceFileLogger.LogLevel.Debug, context, callId, message);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002AD58 File Offset: 0x00028F58
		internal void TraceError(object context, string callId, string message)
		{
			this.TraceToLog(TraceFileLogger.LogLevel.Error, context, callId, message);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002AD64 File Offset: 0x00028F64
		internal void TracePfd(object context, string callId, string message)
		{
			this.TraceToLog(TraceFileLogger.LogLevel.Pfd, context, callId, message);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002AD70 File Offset: 0x00028F70
		internal void TraceWarning(object context, string callId, string message)
		{
			this.TraceToLog(TraceFileLogger.LogLevel.Warning, context, callId, message);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002AD7C File Offset: 0x00028F7C
		internal void TracePerformance(object context, string callId, string message)
		{
			this.TraceToLog(TraceFileLogger.LogLevel.Performance, context, callId, message);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002AD88 File Offset: 0x00028F88
		internal void Flush()
		{
			if (this.log != null)
			{
				this.log.Flush();
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002ADA0 File Offset: 0x00028FA0
		private void LazyInitialize()
		{
			if (this.log == null)
			{
				lock (this.lockObject)
				{
					if (this.log == null)
					{
						this.logSchema = new LogSchema("Microsoft Exchange Server", CommonConstants.ApplicationVersion, "Unified Messaging Trace", this.logFields);
						this.log = new Log(this.processName + "-", new LogHeaderFormatter(this.logSchema, LogHeaderCsvOption.CsvCompatible), "UnifiedMessaging");
						this.log.Configure(Path.Combine(Utils.GetExchangeDirectory(), "Logging\\UMLogs"), this.LogMaxAge, 5368709120L, 10485760L, 1048576, this.LogFlushInterval);
					}
				}
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002AE9C File Offset: 0x0002909C
		private void TraceToLog(TraceFileLogger.LogLevel level, object context, string callId, string message)
		{
			if ((VariantConfiguration.InvariantNoFlightingSnapshot.UM.AlwaysLogTraces.Enabled && this.isProcessAllowedInDatacenter) || this.isForcedLogging)
			{
				this.LazyInitialize();
				LogRowFormatter row = new LogRowFormatter(this.logSchema);
				row[1] = Interlocked.Increment(ref this.sequenceNumber);
				row[2] = this.processName;
				row[3] = level;
				row[4] = this.processId;
				row[5] = Thread.CurrentThread.ManagedThreadId;
				row[6] = ((context == null) ? "" : string.Concat(context.GetHashCode()));
				row[7] = ((callId == null) ? "" : callId);
				row[8] = message;
				Task.Factory.StartNew(delegate()
				{
					this.log.Append(row, 0);
				}, CancellationToken.None, TaskCreationOptions.PreferFairness, this.logSchedulerPair.ExclusiveScheduler);
			}
		}

		// Token: 0x04000626 RID: 1574
		private const string SoftwareName = "Microsoft Exchange Server";

		// Token: 0x04000627 RID: 1575
		private const string LogTypeName = "Unified Messaging Trace";

		// Token: 0x04000628 RID: 1576
		private const string LogComponent = "UnifiedMessaging";

		// Token: 0x04000629 RID: 1577
		private const string LogPath = "Logging\\UMLogs";

		// Token: 0x0400062A RID: 1578
		private const long LogMaxDirectorySize = 5368709120L;

		// Token: 0x0400062B RID: 1579
		private const long LogMaxFileSize = 10485760L;

		// Token: 0x0400062C RID: 1580
		private const int LogBufferSize = 1048576;

		// Token: 0x0400062D RID: 1581
		private readonly TimeSpan LogMaxAge = TimeSpan.FromDays(14.0);

		// Token: 0x0400062E RID: 1582
		private readonly TimeSpan LogFlushInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x0400062F RID: 1583
		private readonly string[] logFields = new string[]
		{
			"Timestamp",
			"SequenceNumber",
			"ProcessName",
			"Level",
			"ProcessId",
			"ThreadId",
			"Context",
			"CallId",
			"Message"
		};

		// Token: 0x04000630 RID: 1584
		private Log log;

		// Token: 0x04000631 RID: 1585
		private LogSchema logSchema;

		// Token: 0x04000632 RID: 1586
		private readonly string processName;

		// Token: 0x04000633 RID: 1587
		private readonly int processId;

		// Token: 0x04000634 RID: 1588
		private long sequenceNumber;

		// Token: 0x04000635 RID: 1589
		private readonly bool isForcedLogging;

		// Token: 0x04000636 RID: 1590
		private readonly bool isProcessAllowedInDatacenter;

		// Token: 0x04000637 RID: 1591
		private object lockObject = new object();

		// Token: 0x04000638 RID: 1592
		private ConcurrentExclusiveSchedulerPair logSchedulerPair = new ConcurrentExclusiveSchedulerPair();

		// Token: 0x02000170 RID: 368
		private enum LogField
		{
			// Token: 0x0400063A RID: 1594
			Timestamp,
			// Token: 0x0400063B RID: 1595
			SequenceNumber,
			// Token: 0x0400063C RID: 1596
			ProcessName,
			// Token: 0x0400063D RID: 1597
			Level,
			// Token: 0x0400063E RID: 1598
			ProcessId,
			// Token: 0x0400063F RID: 1599
			ThreadId,
			// Token: 0x04000640 RID: 1600
			Context,
			// Token: 0x04000641 RID: 1601
			CallId,
			// Token: 0x04000642 RID: 1602
			Message
		}

		// Token: 0x02000171 RID: 369
		private enum LogLevel
		{
			// Token: 0x04000644 RID: 1604
			Pfd,
			// Token: 0x04000645 RID: 1605
			Debug,
			// Token: 0x04000646 RID: 1606
			Warning,
			// Token: 0x04000647 RID: 1607
			Error,
			// Token: 0x04000648 RID: 1608
			Performance
		}
	}
}
