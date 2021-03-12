using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;

namespace Microsoft.Exchange.EdgeSync.Datacenter
{
	// Token: 0x02000004 RID: 4
	internal class EdgeSyncDiag
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000026FA File Offset: 0x000008FA
		public EdgeSyncDiag(EdgeSyncLogSession logSession, Trace tracer)
		{
			this.logSession = logSession;
			this.tracer = tracer;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002710 File Offset: 0x00000910
		public ExEventLog EventLog
		{
			get
			{
				return EdgeSyncEvents.Log;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002717 File Offset: 0x00000917
		public EdgeSyncLogSession LogSession
		{
			get
			{
				return this.logSession;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000271F File Offset: 0x0000091F
		public Trace Tracer
		{
			get
			{
				return this.tracer;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002727 File Offset: 0x00000927
		public static string GetDiagString(string messageFormat, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, messageFormat, args);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002738 File Offset: 0x00000938
		public string LogAndTraceError(string messageFormat, params object[] args)
		{
			string diagString = EdgeSyncDiag.GetDiagString(messageFormat, args);
			this.LogSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, null, diagString);
			this.Tracer.TraceError<string>((long)this.GetHashCode(), "{0}", diagString);
			return diagString;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002778 File Offset: 0x00000978
		public void LogAndTraceException(Exception exception, string messageFormat, params object[] args)
		{
			string diagString = EdgeSyncDiag.GetDiagString(messageFormat, args);
			this.LogSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, exception, diagString);
			this.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "{0}; Exception: {1}", diagString, exception);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027B8 File Offset: 0x000009B8
		public void LogAndTraceInfo(EdgeSyncLoggingLevel level, string messageFormat, params object[] args)
		{
			string diagString = EdgeSyncDiag.GetDiagString(messageFormat, args);
			this.LogSession.LogEvent(level, EdgeSyncEvent.TargetConnection, null, diagString);
			this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}", diagString);
		}

		// Token: 0x04000008 RID: 8
		private EdgeSyncLogSession logSession;

		// Token: 0x04000009 RID: 9
		private Trace tracer;
	}
}
