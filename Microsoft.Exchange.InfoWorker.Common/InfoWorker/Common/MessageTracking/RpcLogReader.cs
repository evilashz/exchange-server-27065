using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.LogSearch;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002DE RID: 734
	internal class RpcLogReader : ILogReader
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x00061FF8 File Offset: 0x000601F8
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00062000 File Offset: 0x00060200
		public ServerVersion ServerVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x00062008 File Offset: 0x00060208
		public MtrSchemaVersion MtrSchemaVersion
		{
			get
			{
				return VersionConverter.GetMtrSchemaVersion(this.version);
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00062018 File Offset: 0x00060218
		public static ILogReader GetLogReader(string server, DirectoryContext context)
		{
			ServerInfo serverInfo = ServerCache.Instance.FindMailboxOrHubServer(server, 34UL);
			if (!serverInfo.IsSearchable)
			{
				return null;
			}
			return new RpcLogReader(serverInfo.Key, serverInfo.AdminDisplayVersion, context);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00062055 File Offset: 0x00060255
		private RpcLogReader(string server, ServerVersion version, DirectoryContext context)
		{
			this.server = server;
			this.version = version;
			this.context = context;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00062072 File Offset: 0x00060272
		public List<MessageTrackingLogEntry> ReadLogs(RpcReason rpcReason, string logFilePrefix, string messageId, DateTime startTime, DateTime endTime, TrackingEventBudget eventBudget)
		{
			return this.ReadLogs(rpcReason, logFilePrefix, null, messageId, startTime, endTime, eventBudget);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00062084 File Offset: 0x00060284
		public List<MessageTrackingLogEntry> ReadLogs(RpcReason rpcReason, string logFilePrefix, ProxyAddressCollection senderProxyAddresses, DateTime startTime, DateTime endTime, TrackingEventBudget eventBudget)
		{
			return this.ReadLogs(rpcReason, logFilePrefix, senderProxyAddresses, null, startTime, endTime, eventBudget);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00062098 File Offset: 0x00060298
		private List<MessageTrackingLogEntry> ReadLogs(RpcReason rpcReason, string logFilePrefix, ProxyAddressCollection senderProxyAddresses, string messageId, DateTime startTime, DateTime endTime, TrackingEventBudget eventBudget)
		{
			Exception ex = null;
			int num = 0;
			List<MessageTrackingLogEntry> list = null;
			bool flag = !string.IsNullOrEmpty(messageId);
			bool flag2 = senderProxyAddresses != null;
			if (flag && flag2)
			{
				throw new InvalidOperationException("Cannot get logs with both message id and sender address criteria");
			}
			if (rpcReason == RpcReason.None)
			{
				InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportQueries.Increment();
			}
			else
			{
				InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportQueries.Increment();
			}
			try
			{
				this.context.DiagnosticsContext.LogRpcStart(this.server, rpcReason);
				using (LogSearchCursor cursor = TrackingSearch.GetCursor(this.server, this.version, logFilePrefix, messageId, senderProxyAddresses, startTime, endTime))
				{
					list = this.ReadLogsFromCursor(cursor, eventBudget);
				}
			}
			catch (LogSearchException ex2)
			{
				ex = ex2;
				num = ex2.ErrorCode;
			}
			catch (RpcException ex3)
			{
				ex = ex3;
				num = ex3.ErrorCode;
			}
			finally
			{
				this.context.DiagnosticsContext.LogRpcEnd(ex, (list == null) ? 0 : list.Count);
			}
			if (ex != null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Error reading from log-search RPC server for {0}: {1}, ErrorCode: {2}, Exception: {3}", new object[]
				{
					flag ? "message id" : "sender",
					flag ? messageId : senderProxyAddresses[0].ToString(),
					num,
					ex
				});
				TrackingTransientException.AddAndRaiseETX(this.context.Errors, ErrorCode.LogSearchConnection, this.server, ex.ToString());
			}
			return list;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00062228 File Offset: 0x00060428
		private List<MessageTrackingLogEntry> ReadLogsFromCursor(LogSearchCursor cursor, TrackingEventBudget eventBudget)
		{
			List<MessageTrackingLogEntry> list = new List<MessageTrackingLogEntry>();
			while (cursor.MoveNext())
			{
				MessageTrackingLogEntry messageTrackingLogEntry;
				if (MessageTrackingLogEntry.TryCreateFromCursor(cursor, this.server, this.context.Errors, out messageTrackingLogEntry))
				{
					if (list.Count % ServerCache.Instance.RowsBeforeTimeBudgetCheck == 0)
					{
						eventBudget.TestDelayOperation("GetRpcsBeforeDelay");
						eventBudget.CheckTimeBudget();
					}
					list.Add(messageTrackingLogEntry);
					if (messageTrackingLogEntry.RecipientAddresses != null)
					{
						eventBudget.IncrementBy((uint)messageTrackingLogEntry.RecipientAddresses.Length);
					}
					else
					{
						eventBudget.IncrementBy(1U);
					}
				}
			}
			return list;
		}

		// Token: 0x04000DC1 RID: 3521
		private string server;

		// Token: 0x04000DC2 RID: 3522
		private DirectoryContext context;

		// Token: 0x04000DC3 RID: 3523
		private ServerVersion version;
	}
}
