using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000058 RID: 88
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DiagnosticLogger : DisposeTrackableBase, ILogger, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000309 RID: 777 RVA: 0x000098B6 File Offset: 0x00007AB6
		public DiagnosticLogger(ILogger logger)
		{
			this.logger = logger;
			this.Logs = new Report.ListWithToString<DiagnosticLog>();
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600030A RID: 778 RVA: 0x000098D0 File Offset: 0x00007AD0
		// (set) Token: 0x0600030B RID: 779 RVA: 0x000098D8 File Offset: 0x00007AD8
		public IList<DiagnosticLog> Logs { get; private set; }

		// Token: 0x0600030C RID: 780 RVA: 0x000098E1 File Offset: 0x00007AE1
		public void Log(string source, MigrationEventType eventType, object context, string format, params object[] args)
		{
			this.AddLogEntry(eventType, null, format, args);
			this.logger.Log(source, eventType, context, format, args);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00009901 File Offset: 0x00007B01
		public void LogEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, params string[] args)
		{
			this.logger.LogEvent(eventType, eventId, args);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00009911 File Offset: 0x00007B11
		public void LogEvent(MigrationEventType eventType, Exception ex, params string[] args)
		{
			this.logger.LogEvent(eventType, ex, args);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00009921 File Offset: 0x00007B21
		public void LogEvent(MigrationEventType eventType, params string[] args)
		{
			this.logger.LogEvent(eventType, args);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00009930 File Offset: 0x00007B30
		public void LogEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, Exception ex, params string[] args)
		{
			this.logger.LogEvent(eventType, eventId, ex, args);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009942 File Offset: 0x00007B42
		public void LogTerseEvent(MigrationEventType eventType, params string[] args)
		{
			this.logger.LogTerseEvent(eventType, args);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009951 File Offset: 0x00007B51
		public void LogTerseEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, params string[] args)
		{
			this.logger.LogTerseEvent(eventType, eventId, args);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00009961 File Offset: 0x00007B61
		public void LogTerseEvent(MigrationEventType eventType, Exception ex, params string[] args)
		{
			this.logger.LogTerseEvent(eventType, ex, args);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00009971 File Offset: 0x00007B71
		public void LogTerseEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, Exception ex, params string[] args)
		{
			this.logger.LogTerseEvent(eventType, eventId, ex, args);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00009983 File Offset: 0x00007B83
		public void Log(MigrationEventType eventType, Exception exception, string format, params object[] args)
		{
			this.AddLogEntry(eventType, exception, format, args);
			this.logger.Log(eventType, exception, format, args);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000099A0 File Offset: 0x00007BA0
		public void Log(MigrationEventType logLevel, string entryString, params object[] formatArgs)
		{
			this.AddLogEntry(logLevel, null, entryString, formatArgs);
			this.logger.Log(logLevel, entryString, formatArgs);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000099BA File Offset: 0x00007BBA
		public void LogError(Exception exception, string entryString, params object[] formatArgs)
		{
			this.AddLogEntry(MigrationEventType.Error, exception, entryString, formatArgs);
			this.logger.LogError(exception, entryString, formatArgs);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000099D4 File Offset: 0x00007BD4
		public void LogVerbose(string formatString, params object[] formatArgs)
		{
			this.Log(MigrationEventType.Verbose, formatString, formatArgs);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000099DF File Offset: 0x00007BDF
		public void LogWarning(string formatString, params object[] formatArgs)
		{
			this.Log(MigrationEventType.Warning, formatString, formatArgs);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000099EA File Offset: 0x00007BEA
		public void LogInformation(string formatString, params object[] formatArgs)
		{
			this.Log(MigrationEventType.Information, formatString, formatArgs);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000099F5 File Offset: 0x00007BF5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DiagnosticLogger>(this);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000099FD File Offset: 0x00007BFD
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00009A00 File Offset: 0x00007C00
		private void AddLogEntry(MigrationEventType eventType, Exception exception, string format, object[] args)
		{
			string logEntry;
			if (args == null || args.Length == 0)
			{
				logEntry = format;
			}
			else
			{
				logEntry = string.Format(format, args);
			}
			this.Logs.Add(new DiagnosticLog
			{
				Exception = exception,
				Level = eventType,
				LogEntry = logEntry
			});
		}

		// Token: 0x040000E4 RID: 228
		private readonly ILogger logger;
	}
}
