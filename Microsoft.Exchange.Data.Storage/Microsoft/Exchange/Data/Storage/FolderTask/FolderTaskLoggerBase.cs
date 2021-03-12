using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.FolderTask
{
	// Token: 0x0200095B RID: 2395
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderTaskLoggerBase : DisposeTrackableBase, IFolderTaskLogger, IPublicFolderMailboxLoggerBase
	{
		// Token: 0x060058CB RID: 22731 RVA: 0x0016D280 File Offset: 0x0016B480
		public FolderTaskLoggerBase(IStoreSession storeSession, string logComponent, string logSuffixName, Guid? correlationId = null)
		{
			ArgumentValidator.ThrowIfNull("storeSession", storeSession);
			ArgumentValidator.ThrowIfNullOrEmpty("logComponent", logComponent);
			ArgumentValidator.ThrowIfNullOrEmpty("logSuffixName", logSuffixName);
			this.CorrelationId = (correlationId ?? Guid.NewGuid());
			this.storeSession = storeSession;
			this.logComponent = logComponent;
			this.logSuffixName = logSuffixName;
			this.organizationId = this.storeSession.OrganizationId;
			this.MailboxGuid = this.storeSession.MailboxGuid;
		}

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x060058CC RID: 22732 RVA: 0x0016D30B File Offset: 0x0016B50B
		// (set) Token: 0x060058CD RID: 22733 RVA: 0x0016D313 File Offset: 0x0016B513
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x060058CE RID: 22734 RVA: 0x0016D31C File Offset: 0x0016B51C
		// (set) Token: 0x060058CF RID: 22735 RVA: 0x0016D324 File Offset: 0x0016B524
		public Guid CorrelationId { get; private set; }

		// Token: 0x060058D0 RID: 22736 RVA: 0x0016D330 File Offset: 0x0016B530
		private static string GetLogFileName(string logSuffixName)
		{
			string result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
				{
					currentProcess.ProcessName,
					logSuffixName
				});
			}
			return result;
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x0016D388 File Offset: 0x0016B588
		internal static string GetExceptionLogString(Exception exception, FolderTaskLoggerBase.ExceptionLogOption option)
		{
			Exception ex = exception;
			if (ex == null)
			{
				return "[No Exception]";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				stringBuilder.Append("[Message:");
				stringBuilder.Append(ex.Message);
				stringBuilder.Append("]");
				stringBuilder.Append("[Type:");
				stringBuilder.Append(ex.GetType().ToString());
				stringBuilder.Append("]");
				if ((option & FolderTaskLoggerBase.ExceptionLogOption.IncludeStack) == FolderTaskLoggerBase.ExceptionLogOption.IncludeStack)
				{
					stringBuilder.Append("[Stack:");
					stringBuilder.Append(string.IsNullOrEmpty(ex.StackTrace) ? string.Empty : ex.StackTrace.Replace("\r\n", string.Empty));
					stringBuilder.Append("]");
				}
				int num = 0;
				if ((option & FolderTaskLoggerBase.ExceptionLogOption.IncludeInnerException) != FolderTaskLoggerBase.ExceptionLogOption.IncludeInnerException || ex.InnerException == null || num > 10)
				{
					break;
				}
				ex = ex.InnerException;
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x0016D470 File Offset: 0x0016B670
		internal static void LogOnServer(Exception exception, string logComponent, string logSuffixName)
		{
			FolderTaskLoggerBase.LogOnServer(FolderTaskLoggerBase.GetExceptionLogString(exception, FolderTaskLoggerBase.ExceptionLogOption.All), LogEventType.Error, logComponent, logSuffixName, FolderTaskLoggerBase.LogType.Folder, null);
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x0016D498 File Offset: 0x0016B698
		internal static void LogOnServer(string data, LogEventType eventType, string logComponent, string logSuffixName, FolderTaskLoggerBase.LogType logType, Guid? correlationId = null)
		{
			Log log = FolderTaskLoggerBase.InitializeServerLogging(logComponent, logSuffixName, logType);
			LogRowFormatter logRowFormatter = new LogRowFormatter(FolderTaskLoggerBase.GetLogSchema(logType));
			logRowFormatter[2] = eventType;
			logRowFormatter[5] = data;
			if (correlationId != null)
			{
				logRowFormatter[7] = correlationId;
			}
			log.Append(logRowFormatter, 0);
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x0016D4F0 File Offset: 0x0016B6F0
		private static Log InitializeServerLogging(string logComponent, string logSuffixName, FolderTaskLoggerBase.LogType logType)
		{
			if (!FolderTaskLoggerBase.initializedLogs.ContainsKey(logSuffixName))
			{
				lock (FolderTaskLoggerBase.initializeLockObject)
				{
					if (!FolderTaskLoggerBase.initializedLogs.ContainsKey(logSuffixName))
					{
						Log log = new Log(FolderTaskLoggerBase.GetLogFileName(logSuffixName), new LogHeaderFormatter(FolderTaskLoggerBase.GetLogSchema(logType)), logComponent);
						log.Configure(Path.Combine(ExchangeSetupContext.InstallPath, FolderTaskLoggerBase.GetLogPath(logType)), FolderTaskLoggerBase.LogMaxAge, 262144000L, 10485760L);
						FolderTaskLoggerBase.initializedLogs.Add(logSuffixName, log);
					}
				}
			}
			return FolderTaskLoggerBase.initializedLogs[logSuffixName];
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x0016D5A0 File Offset: 0x0016B7A0
		private static LogSchema GetLogSchema(FolderTaskLoggerBase.LogType logType)
		{
			return FolderTaskLoggerBase.FolderLogSchema;
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x0016D5A7 File Offset: 0x0016B7A7
		private static string GetLogPath(FolderTaskLoggerBase.LogType logType)
		{
			return "Logging\\Folder\\";
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x0016D5B0 File Offset: 0x0016B7B0
		public virtual void ReportError(string errorContextMessage, Exception syncException)
		{
			this.LogEvent(LogEventType.Error, string.Format(CultureInfo.InvariantCulture, "[ErrorContext:{0}] {1}", new object[]
			{
				errorContextMessage,
				FolderTaskLoggerBase.GetExceptionLogString(syncException, FolderTaskLoggerBase.ExceptionLogOption.All)
			}));
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x0016D5E9 File Offset: 0x0016B7E9
		public virtual void LogEvent(LogEventType eventType, string data)
		{
			this.LogEvent(eventType, data, FolderTaskLoggerBase.LogType.Folder);
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x0016D5F8 File Offset: 0x0016B7F8
		public virtual LogRowFormatter LogEvent(LogEventType eventType, string data, FolderTaskLoggerBase.LogType logType)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new ArgumentNullException("data");
			}
			Log log = FolderTaskLoggerBase.InitializeServerLogging(this.logComponent, this.logSuffixName, logType);
			LogRowFormatter logRowFormatter = new LogRowFormatter(FolderTaskLoggerBase.GetLogSchema(logType));
			logRowFormatter[2] = eventType;
			logRowFormatter[3] = this.organizationId.ToString();
			logRowFormatter[4] = this.MailboxGuid.ToString();
			logRowFormatter[7] = this.CorrelationId.ToString();
			logRowFormatter[5] = data;
			log.Append(logRowFormatter, 0);
			return logRowFormatter;
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x0016D69D File Offset: 0x0016B89D
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x0016D69F File Offset: 0x0016B89F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FolderTaskLoggerBase>(this);
		}

		// Token: 0x04003096 RID: 12438
		internal const string CRLF = "\r\n";

		// Token: 0x04003097 RID: 12439
		private const string FolderLogType = "Folder Diagnostics Log";

		// Token: 0x04003098 RID: 12440
		private const string FolderLogPath = "Logging\\Folder\\";

		// Token: 0x04003099 RID: 12441
		private const int MaxLogDirectorySize = 262144000;

		// Token: 0x0400309A RID: 12442
		private const int MaxLogFileSize = 10485760;

		// Token: 0x0400309B RID: 12443
		private static readonly string[] Fields = new string[]
		{
			"date-time",
			"subcomponent",
			"event-type",
			"organization-id",
			"mailbox-guid",
			"data",
			"context",
			"correlation-id"
		};

		// Token: 0x0400309C RID: 12444
		private static readonly LogSchema FolderLogSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Folder Diagnostics Log", FolderTaskLoggerBase.Fields);

		// Token: 0x0400309D RID: 12445
		private static readonly EnhancedTimeSpan LogMaxAge = EnhancedTimeSpan.FromDays(30.0);

		// Token: 0x0400309E RID: 12446
		private static readonly object initializeLockObject = new object();

		// Token: 0x0400309F RID: 12447
		private static Dictionary<string, Log> initializedLogs = new Dictionary<string, Log>();

		// Token: 0x040030A0 RID: 12448
		protected string logComponent;

		// Token: 0x040030A1 RID: 12449
		protected string logSuffixName;

		// Token: 0x040030A2 RID: 12450
		protected IStoreSession storeSession;

		// Token: 0x040030A3 RID: 12451
		protected OrganizationId organizationId;

		// Token: 0x0200095C RID: 2396
		internal enum LogType
		{
			// Token: 0x040030A7 RID: 12455
			Folder
		}

		// Token: 0x0200095D RID: 2397
		[Flags]
		internal enum ExceptionLogOption
		{
			// Token: 0x040030A9 RID: 12457
			Default = 1,
			// Token: 0x040030AA RID: 12458
			IncludeStack = 2,
			// Token: 0x040030AB RID: 12459
			IncludeInnerException = 4,
			// Token: 0x040030AC RID: 12460
			All = 7
		}

		// Token: 0x0200095E RID: 2398
		private enum Field
		{
			// Token: 0x040030AE RID: 12462
			Time,
			// Token: 0x040030AF RID: 12463
			Subcomponent,
			// Token: 0x040030B0 RID: 12464
			EventType,
			// Token: 0x040030B1 RID: 12465
			OrganizationId,
			// Token: 0x040030B2 RID: 12466
			MailboxGuid,
			// Token: 0x040030B3 RID: 12467
			Data,
			// Token: 0x040030B4 RID: 12468
			Context,
			// Token: 0x040030B5 RID: 12469
			CorrelationId
		}
	}
}
