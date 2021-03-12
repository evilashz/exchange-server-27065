using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Assistants.Logging
{
	// Token: 0x020000C6 RID: 198
	internal static class MailboxAssistantsSlaReportLogFactory
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x0001B950 File Offset: 0x00019B50
		public static MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog GetLogInstance(string logName, SlaLogType logType)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("logName", logName);
			logName = Regex.Replace(logName, "\\s+", string.Empty);
			switch (logType)
			{
			case SlaLogType.MailboxSlaLog:
				return MailboxAssistantsSlaReportLogFactory.GetLogInstance(logName, logType, MailboxAssistantsSlaReportLogFactory.mailboxSlaLogs);
			case SlaLogType.DatabaseSlaLog:
				return MailboxAssistantsSlaReportLogFactory.GetLogInstance(logName, logType, MailboxAssistantsSlaReportLogFactory.databaseSlaLogs);
			default:
				return null;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001B9A7 File Offset: 0x00019BA7
		public static void StopAll()
		{
			MailboxAssistantsSlaReportLogFactory.Stop(MailboxAssistantsSlaReportLogFactory.mailboxSlaLogs);
			MailboxAssistantsSlaReportLogFactory.Stop(MailboxAssistantsSlaReportLogFactory.databaseSlaLogs);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001B9C0 File Offset: 0x00019BC0
		private static MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog GetLogInstance(string logName, SlaLogType logType, Dictionary<string, MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog> logs)
		{
			MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog result;
			lock (logs)
			{
				if (!logs.ContainsKey(logName))
				{
					switch (logType)
					{
					case SlaLogType.MailboxSlaLog:
						logs[logName] = new MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog(logName);
						break;
					case SlaLogType.DatabaseSlaLog:
						logs[logName] = new MailboxAssistantsSlaReportLogFactory.MailboxAssistantsDatabaseSlaLog(logName);
						break;
					}
				}
				result = logs[logName];
			}
			return result;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001BA38 File Offset: 0x00019C38
		private static void Stop(Dictionary<string, MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog> logs)
		{
			lock (logs)
			{
				foreach (KeyValuePair<string, MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog> keyValuePair in logs)
				{
					if (keyValuePair.Value != null)
					{
						keyValuePair.Value.FlushAndDispose();
					}
				}
				logs.Clear();
			}
		}

		// Token: 0x0400039B RID: 923
		private static readonly Dictionary<string, MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog> mailboxSlaLogs = new Dictionary<string, MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog>();

		// Token: 0x0400039C RID: 924
		private static readonly Dictionary<string, MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog> databaseSlaLogs = new Dictionary<string, MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog>();

		// Token: 0x020000C7 RID: 199
		internal class MailboxAssistantsSlaReportLog : ActivityContextLogger
		{
			// Token: 0x060005A6 RID: 1446 RVA: 0x0001BAD6 File Offset: 0x00019CD6
			public MailboxAssistantsSlaReportLog(string logName) : base(logName)
			{
				ArgumentValidator.ThrowIfNullOrWhiteSpace("logName", logName);
				this.logName = logName;
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001BAF1 File Offset: 0x00019CF1
			protected override string FileNamePrefix
			{
				get
				{
					return this.logName;
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001BAF9 File Offset: 0x00019CF9
			protected override string LogComponentName
			{
				get
				{
					return "MailboxAssistantsSlaReportLog";
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001BB00 File Offset: 0x00019D00
			protected override string LogTypeName
			{
				get
				{
					return "MailboxAssistantsSlaReportLog";
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001BB07 File Offset: 0x00019D07
			protected override int TimestampField
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001BB0A File Offset: 0x00019D0A
			protected override Trace Tracer
			{
				get
				{
					return ExTraceGlobals.AssistantBaseTracer;
				}
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x0001BB11 File Offset: 0x00019D11
			public void FlushAndDispose()
			{
				base.FlushLog();
				this.Dispose();
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x0001BB20 File Offset: 0x00019D20
			internal void LogMailboxEvent(string assistantName, string databaseName, string jobId, MailboxSlaRequestType requestType, Guid mailboxGuid, string mailboxDisplayNameTracingOnlyUsage, MailboxSlaEventType eventType, MailboxSlaFilterReasonType reason = MailboxSlaFilterReasonType.None, Exception exception = null)
			{
				if (!this.ShouldLog())
				{
					return;
				}
				LogRowFormatter logRowFormatter = new LogRowFormatter(base.LogSchema);
				logRowFormatter[1] = this.ServerName;
				logRowFormatter[2] = (assistantName ?? string.Empty);
				logRowFormatter[3] = (databaseName ?? string.Empty);
				logRowFormatter[4] = (jobId ?? string.Empty);
				logRowFormatter[5] = ((requestType == MailboxSlaRequestType.Unknown) ? string.Empty : requestType.ToString());
				logRowFormatter[6] = ((mailboxGuid == Guid.Empty) ? string.Empty : mailboxGuid.ToString("D"));
				logRowFormatter[7] = eventType.ToString();
				logRowFormatter[8] = ((reason == MailboxSlaFilterReasonType.None) ? string.Empty : reason.ToString());
				logRowFormatter[9] = ((exception != null) ? exception.GetType().ToString() : string.Empty);
				logRowFormatter[10] = ((exception != null && exception.InnerException != null) ? exception.InnerException.GetType().ToString() : string.Empty);
				base.AppendLog(logRowFormatter);
				if (base.IsDebugTraceEnabled)
				{
					string message = string.Format("Assistant: {0}, Server: {1}, Database: {2}, WindowJob: {3}, Request: {4}, Mailbox: {5}, Event: {6}, Reason: {7}, Exception: {8}", new object[]
					{
						assistantName,
						this.ServerName,
						databaseName ?? string.Empty,
						jobId ?? string.Empty,
						requestType,
						string.IsNullOrEmpty(mailboxDisplayNameTracingOnlyUsage) ? mailboxGuid.ToString("D") : mailboxDisplayNameTracingOnlyUsage,
						eventType,
						(reason == MailboxSlaFilterReasonType.None) ? string.Empty : reason.ToString(),
						(exception != null) ? exception.Message : string.Empty
					});
					base.SafeTraceDebug((long)this.GetHashCode(), message, new object[0]);
				}
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x0001BD05 File Offset: 0x00019F05
			protected override string[] GetLogFields()
			{
				return Enum.GetNames(typeof(MailboxSlaReportLogFields));
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x0001BD16 File Offset: 0x00019F16
			protected override ActivityContextLogFileSettings GetLogFileSettings()
			{
				return new MailboxAssistantsSlaReportLogFileSettings();
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x0001BD1D File Offset: 0x00019F1D
			protected override void InternalLogActivityEvent(IActivityScope activityScope, ActivityEventType eventType)
			{
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x0001BD1F File Offset: 0x00019F1F
			protected bool ShouldLog()
			{
				if (base.Enabled)
				{
					return true;
				}
				base.SafeTraceDebug((long)this.GetHashCode(), "MailboxAssistantsSlaReportLog is disabled, skip writing to the log file.", new object[0]);
				return false;
			}

			// Token: 0x0400039D RID: 925
			private readonly string logName;
		}

		// Token: 0x020000C8 RID: 200
		internal class MailboxAssistantsDatabaseSlaLog : MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog
		{
			// Token: 0x060005B2 RID: 1458 RVA: 0x0001BD44 File Offset: 0x00019F44
			public MailboxAssistantsDatabaseSlaLog(string logName) : base(logName)
			{
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001BD4D File Offset: 0x00019F4D
			protected override string LogComponentName
			{
				get
				{
					return "MailboxAssistantsDatabaseSlaLog";
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001BD54 File Offset: 0x00019F54
			protected override string LogTypeName
			{
				get
				{
					return "MailboxAssistantsDatabaseSlaLog";
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001BD5B File Offset: 0x00019F5B
			protected override int TimestampField
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x0001BD60 File Offset: 0x00019F60
			internal void LogDatabaseEvent(string assistantName, string databaseName, DatabaseSlaEventType eventType, Exception exception = null)
			{
				if (!base.ShouldLog())
				{
					return;
				}
				LogRowFormatter logRowFormatter = new LogRowFormatter(base.LogSchema);
				logRowFormatter[1] = this.ServerName;
				logRowFormatter[2] = (assistantName ?? string.Empty);
				logRowFormatter[3] = (databaseName ?? string.Empty);
				logRowFormatter[4] = eventType.ToString();
				logRowFormatter[5] = ((exception != null) ? exception.GetType().ToString() : string.Empty);
				logRowFormatter[6] = ((exception != null && exception.InnerException != null) ? exception.InnerException.GetType().ToString() : string.Empty);
				base.AppendLog(logRowFormatter);
				if (base.IsDebugTraceEnabled)
				{
					string message = string.Format("Assistant: {0}, Server: {1}, Database: {2}, Event: {3}, Exception: {4}", new object[]
					{
						assistantName,
						this.ServerName,
						databaseName ?? string.Empty,
						eventType,
						(exception != null) ? exception.Message : string.Empty
					});
					base.SafeTraceDebug((long)this.GetHashCode(), message, new object[0]);
				}
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x0001BE7B File Offset: 0x0001A07B
			protected override string[] GetLogFields()
			{
				return Enum.GetNames(typeof(DatabaseSlaLogFields));
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0001BE8C File Offset: 0x0001A08C
			protected override ActivityContextLogFileSettings GetLogFileSettings()
			{
				return new MailboxAssistantsDatabaseSlaLogFileSettings();
			}
		}
	}
}
