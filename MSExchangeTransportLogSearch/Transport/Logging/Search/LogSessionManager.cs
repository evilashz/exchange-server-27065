using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.Rpc.LogSearch;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000030 RID: 48
	internal class LogSessionManager
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00007104 File Offset: 0x00005304
		public void Start()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSessionManager Start");
			this.idleTimer = new GuardedTimer(new TimerCallback(this.CloseIdleSessions), null, LogSessionManager.IdleSessionScanInterval, LogSessionManager.Infinite);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007140 File Offset: 0x00005340
		public void Stop()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSessionManager Stop");
			lock (this.sessionLock)
			{
				this.stopping = true;
				foreach (KeyValuePair<Guid, LogSession> keyValuePair in this.sessions)
				{
					keyValuePair.Value.BeginClose();
				}
				foreach (KeyValuePair<Guid, LogSession> keyValuePair2 in this.sessions)
				{
					keyValuePair2.Value.EndClose();
				}
				this.sessions.Clear();
				this.logs.Clear();
				this.idleTimer.Dispose(false);
				this.idleTimer = null;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007250 File Offset: 0x00005450
		private void CloseIdleSessions(object state)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSessionManager CloseIdleSessions");
			List<LogSession> list = new List<LogSession>();
			lock (this.sessionLock)
			{
				foreach (KeyValuePair<Guid, LogSession> keyValuePair in this.sessions)
				{
					if (DateTime.UtcNow - keyValuePair.Value.LastActivity > LogSessionManager.IdleSessionScanInterval)
					{
						list.Add(keyValuePair.Value);
					}
				}
				foreach (LogSession logSession in list)
				{
					this.sessions.Remove(logSession.Id);
				}
			}
			foreach (LogSession logSession2 in list)
			{
				logSession2.BeginClose();
			}
			foreach (LogSession logSession3 in list)
			{
				PerfCounters.SearchesTimedOut.Increment();
				logSession3.EndClose();
			}
			if (!this.stopping)
			{
				this.idleTimer.Change(LogSessionManager.IdleSessionScanInterval, LogSessionManager.Infinite);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007404 File Offset: 0x00005604
		public LogSession CreateSession(string name, Version schemaVersion)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSessionManager CreateSession");
			LogSession result;
			lock (this.sessionLock)
			{
				if (this.stopping)
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_SESSION_CANCELED);
				}
				Log log;
				if (!this.logs.TryGetValue(name, out log))
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_LOG_UNKNOWN_LOG);
				}
				if (this.sessions.Count == 100)
				{
					LogSearchException ex = new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_SERVER_TOO_BUSY);
					StringBuilder stringBuilder = new StringBuilder(5300);
					foreach (KeyValuePair<Guid, LogSession> keyValuePair in this.sessions)
					{
						stringBuilder.Append(keyValuePair.Key.ToString("N"));
						stringBuilder.Append(":");
						stringBuilder.Append(keyValuePair.Value.LastActivity.ToString("y/M/d:H:m:s"));
						stringBuilder.Append(",");
					}
					ExWatson.AddExtraData(stringBuilder.ToString());
					DiagnosticWatson.SendWatsonWithoutCrash(ex, "MaxSessionExceeded", TimeSpan.FromDays(1.0));
					throw ex;
				}
				LogSession logSession = new LogSession(Guid.NewGuid(), log, schemaVersion);
				this.sessions[logSession.Id] = logSession;
				result = logSession;
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000075A8 File Offset: 0x000057A8
		public LogSession GetSession(Guid id)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSessionManager GetSession");
			LogSession result;
			lock (this.sessionLock)
			{
				if (this.stopping)
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_SESSION_CANCELED);
				}
				LogSession logSession;
				if (!this.sessions.TryGetValue(id, out logSession))
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_UNKNOWN_SESSION_ID);
				}
				result = logSession;
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000762C File Offset: 0x0000582C
		public void CloseSession(LogSession session)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSessionManager CloseSession");
			lock (this.sessionLock)
			{
				if (this.stopping)
				{
					return;
				}
				this.sessions.Remove(session.Id);
			}
			session.BeginClose();
			session.EndClose();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000076A4 File Offset: 0x000058A4
		public void RegisterLog(string name, Log log)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSessionManager RegisterLog");
			lock (this.sessionLock)
			{
				this.logs.Add(name, log);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007704 File Offset: 0x00005904
		public void ShowSessions()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSessionManager ShowSessions");
			lock (this.sessionLock)
			{
				foreach (KeyValuePair<Guid, LogSession> keyValuePair in this.sessions)
				{
					Console.WriteLine("{0}, idle time {1}", keyValuePair.Key, DateTime.UtcNow - keyValuePair.Value.LastActivity);
				}
			}
		}

		// Token: 0x0400009D RID: 157
		private const int MaxSessions = 100;

		// Token: 0x0400009E RID: 158
		private const int SessionEntryStringLength = 53;

		// Token: 0x0400009F RID: 159
		private static readonly TimeSpan IdleSessionScanInterval = new TimeSpan(0, 3, 0);

		// Token: 0x040000A0 RID: 160
		private static readonly TimeSpan Infinite = new TimeSpan(0, 0, 0, 0, -1);

		// Token: 0x040000A1 RID: 161
		private Dictionary<Guid, LogSession> sessions = new Dictionary<Guid, LogSession>();

		// Token: 0x040000A2 RID: 162
		private Dictionary<string, Log> logs = new Dictionary<string, Log>();

		// Token: 0x040000A3 RID: 163
		private GuardedTimer idleTimer;

		// Token: 0x040000A4 RID: 164
		private bool stopping;

		// Token: 0x040000A5 RID: 165
		private readonly object sessionLock = new object();
	}
}
