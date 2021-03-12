using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.Rpc.LogSearch;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000032 RID: 50
	internal sealed class LogSearchServer : LogSearchRpcServer
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00007940 File Offset: 0x00005B40
		private static LogQuery DeserializeQuery(byte[] query)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "MsExchangeLogSearch server is entering DeserializeQuery");
			MemoryStream stream = new MemoryStream(query);
			StreamReader textReader = new StreamReader(stream, Encoding.UTF8);
			LogQuery result;
			try
			{
				LogQuery logQuery = (LogQuery)LogSearchServer.logQuerySerializer.Deserialize(textReader);
				logQuery.Beginning = LogSearchServer.GetNearestValidDate(logQuery.Beginning);
				logQuery.End = LogSearchServer.GetNearestValidDate(logQuery.End);
				result = logQuery;
			}
			catch (InvalidOperationException)
			{
				throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_BAD_QUERY_XML);
			}
			return result;
		}

		// Token: 0x17000026 RID: 38
		// (set) Token: 0x06000103 RID: 259 RVA: 0x000079C8 File Offset: 0x00005BC8
		public LogSessionManager SessionManager
		{
			set
			{
				lock (this.syncSessionManager)
				{
					this.sessionManager = value;
					Monitor.PulseAll(this.syncSessionManager);
				}
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007A14 File Offset: 0x00005C14
		public override byte[] SearchExtensibleSchema(string clientVersion, string logName, byte[] query, bool continueInBackground, int resultLimit, ref int resultSize, ref Guid sessionId, ref bool more, ref int progress, string clientName)
		{
			byte[] result;
			try
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch server is entering Search using extensible result schema.");
				PerfCounters.SearchesProcessed.Increment();
				this.WaitForSessionManager();
				resultSize = 0;
				sessionId = Guid.Empty;
				more = false;
				progress = 0;
				if (query.Length > 65536)
				{
					ExTraceGlobals.ServiceTracer.TraceError<int>((long)this.GetHashCode(), "Search rejected because the query was too complex. Query length: {0}", query.Length);
					PerfCounters.SearchesRejected.Increment();
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_QUERY_TOO_COMPLEX);
				}
				Version schemaVersion = new Version(clientVersion);
				LogSession logSession = this.sessionManager.CreateSession(logName, schemaVersion);
				try
				{
					LogQuery query2 = LogSearchServer.DeserializeQuery(query);
					logSession.SetQuery(query2);
					if (LogSearchServer.logger.IsEventCategoryEnabled(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchClientQuery.CategoryId, MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchClientQuery.Level))
					{
						LogSearchServer.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchClientQuery, null, new object[]
						{
							clientName,
							Encoding.UTF8.GetString(query)
						});
					}
					byte[] array = new byte[Math.Min(resultLimit, 1048576)];
					resultSize = logSession.Read(array, 0, array.Length, out more, out progress, !continueInBackground);
					if (more && continueInBackground)
					{
						sessionId = logSession.Id;
						logSession = null;
					}
					result = array;
				}
				catch (LogSearchException ex)
				{
					PerfCounters.SearchesRejected.Increment();
					ExTraceGlobals.ServiceTracer.TraceDebug<LogSearchException>((long)logSession.GetHashCode(), "Search was interrupted, Exception: {0}", ex);
					throw ex;
				}
				finally
				{
					if (logSession != null)
					{
						ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogSearchServer session {0} Search failed", logSession.Id.ToString());
						this.sessionManager.CloseSession(logSession);
					}
				}
			}
			catch (LogSearchException)
			{
				throw;
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
				result = null;
			}
			return result;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007C34 File Offset: 0x00005E34
		public override byte[] Search(string logName, byte[] query, bool continueInBackground, int resultLimit, ref int resultSize, ref Guid sessionId, ref bool more, ref int progress, string clientName)
		{
			byte[] result;
			try
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch server is entering Search");
				PerfCounters.SearchesProcessed.Increment();
				this.WaitForSessionManager();
				resultSize = 0;
				sessionId = Guid.Empty;
				more = false;
				progress = 0;
				if (query.Length > 65536)
				{
					ExTraceGlobals.ServiceTracer.TraceError<int>((long)this.GetHashCode(), "Search rejected because the query was too complex. Query length: {0}", query.Length);
					PerfCounters.SearchesRejected.Increment();
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_QUERY_TOO_COMPLEX);
				}
				LogSession logSession = this.sessionManager.CreateSession(logName, LogSearchServer.DefaultLogSchemaVersion);
				try
				{
					LogQuery query2 = LogSearchServer.DeserializeQuery(query);
					logSession.SetQuery(query2);
					if (LogSearchServer.logger.IsEventCategoryEnabled(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchClientQuery.CategoryId, MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchClientQuery.Level))
					{
						LogSearchServer.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchClientQuery, null, new object[]
						{
							clientName,
							Encoding.UTF8.GetString(query)
						});
					}
					byte[] array = new byte[Math.Min(resultLimit, 1048576)];
					resultSize = logSession.Read(array, 0, array.Length, out more, out progress, !continueInBackground);
					if (more && continueInBackground)
					{
						sessionId = logSession.Id;
						logSession = null;
					}
					result = array;
				}
				catch (LogSearchException ex)
				{
					PerfCounters.SearchesRejected.Increment();
					ExTraceGlobals.ServiceTracer.TraceDebug<LogSearchException>((long)logSession.GetHashCode(), "Search was interrupted, Exception: {0}", ex);
					throw ex;
				}
				finally
				{
					if (logSession != null)
					{
						ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogSearchServer session {0} Search failed", logSession.Id.ToString());
						this.sessionManager.CloseSession(logSession);
					}
				}
			}
			catch (LogSearchException)
			{
				throw;
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
				result = null;
			}
			return result;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007E4C File Offset: 0x0000604C
		public override byte[] Continue(Guid sessionId, bool continueInBackground, int resultLimit, ref int resultSize, ref bool more, ref int progress)
		{
			byte[] result;
			try
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch server is entering Continue");
				this.WaitForSessionManager();
				resultSize = 0;
				more = false;
				progress = 0;
				LogSession logSession = this.sessionManager.GetSession(sessionId);
				try
				{
					byte[] array = new byte[Math.Min(resultLimit, 1048576)];
					resultSize = logSession.Read(array, 0, array.Length, out more, out progress, !continueInBackground);
					if (more && continueInBackground)
					{
						ExTraceGlobals.ServiceTracer.TraceDebug((long)logSession.GetHashCode(), "Session continuing in background");
						logSession = null;
					}
					result = array;
				}
				catch (LogSearchException ex)
				{
					PerfCounters.SearchesRejected.Increment();
					ExTraceGlobals.ServiceTracer.TraceDebug<LogSearchException>((long)logSession.GetHashCode(), "Continue was interrupted, Exception: {0}", ex);
					throw ex;
				}
				finally
				{
					if (logSession != null)
					{
						ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogSearchServer session {0} Continue failed", logSession.Id.ToString());
						this.sessionManager.CloseSession(logSession);
					}
				}
			}
			catch (LogSearchException)
			{
				throw;
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
				result = null;
			}
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007F80 File Offset: 0x00006180
		public override void Cancel(Guid sessionId)
		{
			try
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch server is entering Cancel");
				this.WaitForSessionManager();
				try
				{
					this.sessionManager.CloseSession(this.sessionManager.GetSession(sessionId));
				}
				catch (LogSearchException)
				{
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogSearchServer Cancel session {0} failed with LogSearchException", sessionId.ToString());
				}
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008010 File Offset: 0x00006210
		private static DateTime GetNearestValidDate(DateTime date)
		{
			if (date < CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime)
			{
				return CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime;
			}
			if (date > CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime)
			{
				return CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime;
			}
			return date;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000806C File Offset: 0x0000626C
		private void WaitForSessionManager()
		{
			if (this.sessionManager == null)
			{
				lock (this.syncSessionManager)
				{
					while (this.sessionManager == null)
					{
						Monitor.Wait(this.syncSessionManager);
					}
				}
			}
		}

		// Token: 0x040000AC RID: 172
		private const int ResultSizeLimit = 1048576;

		// Token: 0x040000AD RID: 173
		private const int QuerySizeLimit = 65536;

		// Token: 0x040000AE RID: 174
		private const string LogSearchServiceEventSource = "MSExchangeTransportLogSearch";

		// Token: 0x040000AF RID: 175
		private static readonly Version DefaultLogSchemaVersion = new Version("12.00.0000.000");

		// Token: 0x040000B0 RID: 176
		private static LogQuerySerializer logQuerySerializer = new LogQuerySerializer();

		// Token: 0x040000B1 RID: 177
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, "MSExchangeTransportLogSearch");

		// Token: 0x040000B2 RID: 178
		private LogSessionManager sessionManager;

		// Token: 0x040000B3 RID: 179
		private object syncSessionManager = new object();
	}
}
