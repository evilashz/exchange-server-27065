using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DiagnosticsAggregation;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x02000003 RID: 3
	internal class DiagnosticsAggregationLog
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public void Start(DiagnosticsAggregationServiceletConfig config)
		{
			if (!config.LoggingEnabled)
			{
				return;
			}
			this.log = new Log("DiagnosticsAggregationLog", new LogHeaderFormatter(DiagnosticsAggregationLog.schema), "DiagnosticsAggregationLog");
			this.log.Configure(config.LogFileDirectoryPath, config.LogFileMaxAge, (long)config.LogFileMaxDirectorySize.ToBytes(), (long)config.LogFileMaxSize.ToBytes());
			this.loggingEnabled = true;
			this.Log(DiagnosticsAggregationEvent.LogStarted, "Webservice started", new object[0]);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002154 File Offset: 0x00000354
		public void Stop()
		{
			if (this.log == null)
			{
				return;
			}
			lock (this)
			{
				this.loggingEnabled = false;
				this.log.Close();
				this.log = null;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021AC File Offset: 0x000003AC
		public void Log(DiagnosticsAggregationEvent evt, string format, params object[] parameters)
		{
			this.Log(evt, null, null, string.Empty, string.Empty, null, string.Empty, string.Format(format, parameters));
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021F4 File Offset: 0x000003F4
		public void LogOperationFromClient(DiagnosticsAggregationEvent evt, ClientInformation clientInfo, TimeSpan? duration = null, string description = "")
		{
			this.Log(evt, new uint?(clientInfo.SessionId), duration, (clientInfo != null) ? clientInfo.ClientMachineName : string.Empty, (clientInfo != null) ? clientInfo.ClientProcessName : string.Empty, (clientInfo != null) ? new int?(clientInfo.ClientProcessId) : null, string.Empty, description);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002254 File Offset: 0x00000454
		public void LogOperationToServer(DiagnosticsAggregationEvent evt, uint sessionId, string serverName, TimeSpan? duration = null, string description = "")
		{
			this.Log(evt, new uint?(sessionId), duration, string.Empty, string.Empty, null, serverName, description);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002288 File Offset: 0x00000488
		private void Log(DiagnosticsAggregationEvent evt, uint? sessionId, TimeSpan? duration, string clientHostName, string clientProcessName, int? clientProcessId, string serverHostName, string description)
		{
			if (!this.loggingEnabled)
			{
				return;
			}
			LogSchema logSchema = DiagnosticsAggregationLog.schema;
			uint? num = sessionId;
			DiagnosticsAggregationLogRow row = new DiagnosticsAggregationLogRow(logSchema, evt, (num != null) ? new long?((long)((ulong)num.GetValueOrDefault())) : null, duration, clientHostName, clientProcessName, clientProcessId, serverHostName, description);
			try
			{
				lock (this)
				{
					if (this.loggingEnabled)
					{
						this.log.Append(row, 0);
					}
				}
			}
			catch (ObjectDisposedException)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError(0L, "Appending to Diagnostics Aggregation log failed with ObjectDisposedException");
			}
		}

		// Token: 0x04000011 RID: 17
		private const string LogComponentName = "DiagnosticsAggregationLog";

		// Token: 0x04000012 RID: 18
		private const string LogFileName = "DiagnosticsAggregationLog";

		// Token: 0x04000013 RID: 19
		private static readonly LogSchema schema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Diagnostics Aggregation Log", DiagnosticsAggregationLogRow.Fields);

		// Token: 0x04000014 RID: 20
		private Log log;

		// Token: 0x04000015 RID: 21
		private bool loggingEnabled;
	}
}
