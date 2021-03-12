using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.Rpc.LogSearch;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000003 RID: 3
	internal class Log
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021E8 File Offset: 0x000003E8
		public Log(CsvTable table, string path, string prefix, string server, string extension, Dictionary<string, double> indexPercentageByPrefix)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch constructs Log with path {0}, prefix {1}, server {2}, extension {3}", new object[]
			{
				path,
				prefix,
				server,
				extension
			});
			this.table = table;
			this.prefix = prefix;
			this.server = server;
			this.extension = extension;
			this.indexPercentageByPrefix = indexPercentageByPrefix;
			this.monitor = new LogMonitor(path, prefix, server, extension, this.table, indexPercentageByPrefix);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000226A File Offset: 0x0000046A
		public CsvTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002274 File Offset: 0x00000474
		public void Config(string path)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "MsExchangeLogSearch Log Config with path {0}", path);
			string text = null;
			try
			{
				text = Path.GetFullPath(path);
				if (!Directory.Exists(text))
				{
					Log.CreateLogDirectory(path);
				}
			}
			catch (DirectoryNotFoundException ex)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string, string>((long)this.GetHashCode(), "MsExchangeLogSearch Log Config failed for path {0} with error {1}", text, ex.Message);
				Log.LogCreateDirectoryFailedEvent(text, ex.Message);
			}
			catch (ArgumentException ex2)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string, string>((long)this.GetHashCode(), "MsExchangeLogSearch Log Config failed for path {0} with error {1}", text, ex2.Message);
				Log.LogCreateDirectoryFailedEvent(text, ex2.Message);
			}
			catch (UnauthorizedAccessException ex3)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string, string>((long)this.GetHashCode(), "MsExchangeLogSearch Log Config failed for path {0} with error {1}", text, ex3.Message);
				Log.LogCreateDirectoryFailedEvent(text, ex3.Message);
			}
			catch (IOException ex4)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string, string>((long)this.GetHashCode(), "MsExchangeLogSearch Log Config failed for path {0} with error {1}", text, ex4.Message);
				Log.LogCreateDirectoryFailedEvent(text, ex4.Message);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023A4 File Offset: 0x000005A4
		public void Start()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch Log Start");
			this.monitor.StartAsync();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023C7 File Offset: 0x000005C7
		public void Stop()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch Log Stop");
			this.monitor.Stop();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023EC File Offset: 0x000005EC
		public void ChangePath(string path)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "MsExchangeLogSearch Log ChangePath with new path {0}", path);
			if (string.Compare(this.monitor.Path, path, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return;
			}
			LogMonitor logMonitor = new LogMonitor(path, this.prefix, this.server, this.extension, this.table, this.indexPercentageByPrefix);
			logMonitor.Start();
			LogMonitor logMonitor2 = Interlocked.Exchange<LogMonitor>(ref this.monitor, logMonitor);
			logMonitor2.Stop();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002464 File Offset: 0x00000664
		public LogCursor GetCursor(DateTime begin, DateTime end, Version schemaVersion)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "MsExchangeLogSearch Log GetCursor with begin time {0} and end time {1}", begin.ToString(), end.ToString());
			if (this.monitor.Initializing)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Search service busy initializing, not ready to answer requests yet");
				throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_SERVER_TOO_BUSY);
			}
			List<LogFileInfo> fileInfoForTimeRange = this.monitor.GetFileInfoForTimeRange(begin, end);
			return new LogCursor(this.table, schemaVersion, begin, end, fileInfoForTimeRange);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024EC File Offset: 0x000006EC
		internal void ForceUpdate()
		{
			List<LogFileInfo> fileInfoForTimeRange = this.monitor.GetFileInfoForTimeRange(DateTime.UtcNow.AddHours(-1.0), DateTime.UtcNow.AddHours(1.0));
			foreach (LogFileInfo logFileInfo in fileInfoForTimeRange)
			{
				logFileInfo.ContinueProcessingActiveFile();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002574 File Offset: 0x00000774
		private static void LogCreateDirectoryFailedEvent(string absolutePath, string exceptionMessage)
		{
			LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceCreateDirectoryFailed, null, new object[]
			{
				absolutePath,
				exceptionMessage
			});
			string notificationReason = string.Format("The Microsoft Exchange Transport Log Search service failed to create message tracking log directory: {0} because of error: {1}", absolutePath, exceptionMessage);
			EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportLogSearch", null, notificationReason, ResultSeverityLevel.Warning, false);
		}

		// Token: 0x04000003 RID: 3
		private CsvTable table;

		// Token: 0x04000004 RID: 4
		private LogMonitor monitor;

		// Token: 0x04000005 RID: 5
		private string prefix;

		// Token: 0x04000006 RID: 6
		private string server;

		// Token: 0x04000007 RID: 7
		private string extension;

		// Token: 0x04000008 RID: 8
		private Dictionary<string, double> indexPercentageByPrefix;
	}
}
