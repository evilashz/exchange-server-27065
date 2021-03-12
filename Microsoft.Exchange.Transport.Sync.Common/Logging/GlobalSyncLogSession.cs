using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Logging
{
	// Token: 0x02000082 RID: 130
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GlobalSyncLogSession : SyncLogSession
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00014B50 File Offset: 0x00012D50
		public GlobalSyncLogSession(SyncLog syncLog, LogRowFormatter row) : base(syncLog, row)
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00014B5C File Offset: 0x00012D5C
		public void LogInformation(TSLID logEntryId, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			base.LogEvent(logEntryId, SyncLoggingLevel.Information, subscriptionGuid, user, null, null, format, arguments);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00014B79 File Offset: 0x00012D79
		public void LogInformation(TSLID logEntryId, Trace trace, long id, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			this.LogInformation(logEntryId, subscriptionGuid, user, format, arguments);
			if (trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				trace.TraceDebug(id, this.MakeGlobalTracerMessage(subscriptionGuid, user, format, arguments));
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00014BA8 File Offset: 0x00012DA8
		public void LogInformation(TSLID logEntryId, Trace trace, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			this.LogInformation(logEntryId, trace, 0L, subscriptionGuid, user, format, arguments);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00014BBC File Offset: 0x00012DBC
		public void LogVerbose(TSLID logEntryId, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			base.LogEvent(logEntryId, SyncLoggingLevel.Verbose, subscriptionGuid, user, null, null, format, arguments);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00014BD9 File Offset: 0x00012DD9
		public void LogVerbose(TSLID logEntryId, Trace trace, long id, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			this.LogVerbose(logEntryId, subscriptionGuid, user, format, arguments);
			if (trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				trace.TraceDebug(id, this.MakeGlobalTracerMessage(subscriptionGuid, user, format, arguments));
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00014C08 File Offset: 0x00012E08
		public void LogVerbose(TSLID logEntryId, Trace trace, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			this.LogVerbose(logEntryId, trace, 0L, subscriptionGuid, user, format, arguments);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00014C1C File Offset: 0x00012E1C
		public void LogError(TSLID logEntryId, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			base.LogEvent(logEntryId, SyncLoggingLevel.Error, subscriptionGuid, user, null, null, format, arguments);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00014C39 File Offset: 0x00012E39
		public void LogError(TSLID logEntryId, Trace trace, long id, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			this.LogError(logEntryId, subscriptionGuid, user, format, arguments);
			if (trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				trace.TraceDebug(id, this.MakeGlobalTracerMessage(subscriptionGuid, user, format, arguments));
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00014C68 File Offset: 0x00012E68
		public void LogError(TSLID logEntryId, Trace trace, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			this.LogError(logEntryId, trace, 0L, subscriptionGuid, user, format, arguments);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00014C7C File Offset: 0x00012E7C
		public void LogRawData(TSLID logEntryId, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			base.LogEvent(logEntryId, SyncLoggingLevel.RawData, subscriptionGuid, user, null, null, format, arguments);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00014C99 File Offset: 0x00012E99
		public void LogRawData(TSLID logEntryId, Trace trace, long id, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			this.LogRawData(logEntryId, subscriptionGuid, user, format, arguments);
			if (trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				trace.TraceDebug(id, this.MakeGlobalTracerMessage(subscriptionGuid, user, format, arguments));
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00014CC8 File Offset: 0x00012EC8
		public void LogRawData(TSLID logEntryId, Trace trace, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			this.LogRawData(logEntryId, trace, 0L, subscriptionGuid, user, format, arguments);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00014CDC File Offset: 0x00012EDC
		public void LogDebugging(TSLID logEntryId, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			base.LogEvent(logEntryId, SyncLoggingLevel.Debugging, subscriptionGuid, user, null, null, format, arguments);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00014CF9 File Offset: 0x00012EF9
		public void LogDebugging(TSLID logEntryId, Trace trace, long id, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			this.LogDebugging(logEntryId, subscriptionGuid, user, format, arguments);
			if (trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				trace.TraceDebug(id, this.MakeGlobalTracerMessage(subscriptionGuid, user, format, arguments));
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00014D28 File Offset: 0x00012F28
		public void LogDebugging(TSLID logEntryId, Trace trace, Guid subscriptionGuid, object user, string format, params object[] arguments)
		{
			this.LogDebugging(logEntryId, trace, 0L, subscriptionGuid, user, format, arguments);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00014D3C File Offset: 0x00012F3C
		private string MakeGlobalTracerMessage(Guid subscriptionGuid, object user, string format, object[] arguments)
		{
			string result;
			try
			{
				string text = string.Format(SyncLogSession.TracerFormatProvider.Instance, format, arguments);
				result = string.Format(SyncLogSession.TracerFormatProvider.Instance, "{0} [Subscription: {1}, User: {2}]", new object[]
				{
					text,
					subscriptionGuid.ToString(),
					user
				});
			}
			catch (FormatException exception)
			{
				base.ReportWatson("Malformed logging format found.", exception);
				result = string.Empty;
			}
			return result;
		}
	}
}
