using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x0200094A RID: 2378
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PublicFolderMailboxLoggerBase : DisposeTrackableBase, IPublicFolderMailboxLoggerBase
	{
		// Token: 0x0600587E RID: 22654 RVA: 0x0016C050 File Offset: 0x0016A250
		public PublicFolderMailboxLoggerBase(IPublicFolderSession publicFolderSession, Guid? correlationId = null)
		{
			ArgumentValidator.ThrowIfNull("publicFolderSession", publicFolderSession);
			this.TransactionId = (correlationId ?? Guid.NewGuid());
			this.pfSession = publicFolderSession;
			this.organizationId = this.pfSession.OrganizationId;
			this.MailboxGuid = publicFolderSession.MailboxGuid;
		}

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x0600587F RID: 22655 RVA: 0x0016C0B1 File Offset: 0x0016A2B1
		// (set) Token: 0x06005880 RID: 22656 RVA: 0x0016C0B9 File Offset: 0x0016A2B9
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x06005881 RID: 22657 RVA: 0x0016C0C2 File Offset: 0x0016A2C2
		// (set) Token: 0x06005882 RID: 22658 RVA: 0x0016C0CA File Offset: 0x0016A2CA
		public Guid TransactionId { get; private set; }

		// Token: 0x06005883 RID: 22659 RVA: 0x0016C0D4 File Offset: 0x0016A2D4
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

		// Token: 0x06005884 RID: 22660 RVA: 0x0016C12C File Offset: 0x0016A32C
		public static string GetExceptionLogString(Exception e, PublicFolderMailboxLoggerBase.ExceptionLogOption option)
		{
			Exception ex = e;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				stringBuilder.Append("[Message:");
				stringBuilder.Append(ex.Message);
				stringBuilder.Append("]");
				stringBuilder.Append("[Type:");
				stringBuilder.Append(ex.GetType().ToString());
				stringBuilder.Append("]");
				if ((option & PublicFolderMailboxLoggerBase.ExceptionLogOption.IncludeStack) == PublicFolderMailboxLoggerBase.ExceptionLogOption.IncludeStack)
				{
					stringBuilder.Append("[Stack:");
					stringBuilder.Append(string.IsNullOrEmpty(ex.StackTrace) ? string.Empty : ex.StackTrace.Replace("\r\n", string.Empty));
					stringBuilder.Append("]");
				}
				if ((option & PublicFolderMailboxLoggerBase.ExceptionLogOption.IncludeInnerException) != PublicFolderMailboxLoggerBase.ExceptionLogOption.IncludeInnerException || ex.InnerException == null || num > 10)
				{
					break;
				}
				ex = ex.InnerException;
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005885 RID: 22661 RVA: 0x0016C20C File Offset: 0x0016A40C
		internal static void LogOnServer(Exception exception, string logComponent, string logSuffixName)
		{
			PublicFolderMailboxLoggerBase.LogOnServer(PublicFolderMailboxLoggerBase.GetExceptionLogString(exception, PublicFolderMailboxLoggerBase.ExceptionLogOption.All), LogEventType.Error, logComponent, logSuffixName, null);
		}

		// Token: 0x06005886 RID: 22662 RVA: 0x0016C234 File Offset: 0x0016A434
		internal static void LogOnServer(string data, LogEventType eventType, string logComponent, string logSuffixName, Guid? transactionId = null)
		{
			Log log = PublicFolderMailboxLoggerBase.InitializeServerLogging(logComponent, logSuffixName);
			LogRowFormatter logRowFormatter = new LogRowFormatter(PublicFolderMailboxLoggerBase.LogSchema);
			logRowFormatter[2] = eventType;
			logRowFormatter[5] = data;
			if (transactionId != null)
			{
				logRowFormatter[7] = transactionId;
			}
			log.Append(logRowFormatter, 0);
		}

		// Token: 0x06005887 RID: 22663 RVA: 0x0016C288 File Offset: 0x0016A488
		private static Log InitializeServerLogging(string logComponent, string logSuffixName)
		{
			if (!PublicFolderMailboxLoggerBase.initializedLogs.ContainsKey(logSuffixName))
			{
				lock (PublicFolderMailboxLoggerBase.initializeLockObject)
				{
					if (!PublicFolderMailboxLoggerBase.initializedLogs.ContainsKey(logSuffixName))
					{
						Log log = new Log(PublicFolderMailboxLoggerBase.GetLogFileName(logSuffixName), new LogHeaderFormatter(PublicFolderMailboxLoggerBase.LogSchema), logComponent);
						log.Configure(Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\PublicFolder\\"), PublicFolderMailboxLoggerBase.LogMaxAge, 262144000L, 10485760L);
						PublicFolderMailboxLoggerBase.initializedLogs.Add(logSuffixName, log);
					}
				}
			}
			return PublicFolderMailboxLoggerBase.initializedLogs[logSuffixName];
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x0016C334 File Offset: 0x0016A534
		public virtual void ReportError(string errorContextMessage, Exception syncException)
		{
			this.LogEvent(LogEventType.Error, string.Format(CultureInfo.InvariantCulture, "[ErrorContext:{0}] {1}", new object[]
			{
				errorContextMessage,
				PublicFolderMailboxLoggerBase.GetExceptionLogString(syncException, PublicFolderMailboxLoggerBase.ExceptionLogOption.All)
			}));
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x0016C370 File Offset: 0x0016A570
		public virtual void LogEvent(LogEventType eventType, string data)
		{
			LogRowFormatter logRowFormatter = null;
			this.LogEvent(eventType, data, out logRowFormatter);
		}

		// Token: 0x0600588A RID: 22666 RVA: 0x0016C38C File Offset: 0x0016A58C
		public virtual void LogEvent(LogEventType eventType, string data, out LogRowFormatter row)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new ArgumentNullException("data");
			}
			Log log = PublicFolderMailboxLoggerBase.InitializeServerLogging(this.logComponent, this.logSuffixName);
			row = new LogRowFormatter(PublicFolderMailboxLoggerBase.LogSchema);
			row[2] = eventType;
			row[3] = this.organizationId.ToString();
			row[4] = this.MailboxGuid.ToString();
			row[7] = this.TransactionId.ToString();
			row[5] = data;
			log.Append(row, 0);
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x0016C435 File Offset: 0x0016A635
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x0016C437 File Offset: 0x0016A637
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderMailboxLoggerBase>(this);
		}

		// Token: 0x04003041 RID: 12353
		internal const string CRLF = "\r\n";

		// Token: 0x04003042 RID: 12354
		private const string LogType = "PublicFolder Diagnostics Log";

		// Token: 0x04003043 RID: 12355
		private const string DefaultLogPath = "Logging\\PublicFolder\\";

		// Token: 0x04003044 RID: 12356
		private const int MaxLogDirectorySize = 262144000;

		// Token: 0x04003045 RID: 12357
		private const int MaxLogFileSize = 10485760;

		// Token: 0x04003046 RID: 12358
		private static readonly string[] Fields = new string[]
		{
			"date-time",
			"subcomponent",
			"event-type",
			"organization-id",
			"mailbox-guid",
			"data",
			"context",
			"transaction-id"
		};

		// Token: 0x04003047 RID: 12359
		private static readonly LogSchema LogSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "PublicFolder Diagnostics Log", PublicFolderMailboxLoggerBase.Fields);

		// Token: 0x04003048 RID: 12360
		private static readonly EnhancedTimeSpan LogMaxAge = EnhancedTimeSpan.FromDays(30.0);

		// Token: 0x04003049 RID: 12361
		private static readonly object initializeLockObject = new object();

		// Token: 0x0400304A RID: 12362
		private static Dictionary<string, Log> initializedLogs = new Dictionary<string, Log>();

		// Token: 0x0400304B RID: 12363
		protected string logComponent;

		// Token: 0x0400304C RID: 12364
		protected string logSuffixName;

		// Token: 0x0400304D RID: 12365
		protected IPublicFolderSession pfSession;

		// Token: 0x0400304E RID: 12366
		protected OrganizationId organizationId;

		// Token: 0x0200094B RID: 2379
		[Flags]
		internal enum ExceptionLogOption
		{
			// Token: 0x04003052 RID: 12370
			Default = 1,
			// Token: 0x04003053 RID: 12371
			IncludeStack = 2,
			// Token: 0x04003054 RID: 12372
			IncludeInnerException = 4,
			// Token: 0x04003055 RID: 12373
			All = 7
		}

		// Token: 0x0200094C RID: 2380
		private enum Field
		{
			// Token: 0x04003057 RID: 12375
			Time,
			// Token: 0x04003058 RID: 12376
			Subcomponent,
			// Token: 0x04003059 RID: 12377
			EventType,
			// Token: 0x0400305A RID: 12378
			OrganizationId,
			// Token: 0x0400305B RID: 12379
			MailboxGuid,
			// Token: 0x0400305C RID: 12380
			Data,
			// Token: 0x0400305D RID: 12381
			Context,
			// Token: 0x0400305E RID: 12382
			TransactionId
		}
	}
}
