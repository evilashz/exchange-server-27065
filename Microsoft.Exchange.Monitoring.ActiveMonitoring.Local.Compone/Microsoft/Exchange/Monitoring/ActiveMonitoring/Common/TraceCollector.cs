using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000A6 RID: 166
	internal class TraceCollector
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x000224C0 File Offset: 0x000206C0
		internal void FlushToFile(string logInstanceName, MonitoringLogConfiguration logConfiguration)
		{
			MonitoringLogger loggerInstance = this.GetLoggerInstance(logInstanceName, logConfiguration);
			lock (this.traceTimes)
			{
				loggerInstance.LogEvent(DateTime.UtcNow, "---------------------------------------------------------------------------------------------", null);
				loggerInstance.LogEvents(this.traceTimes, this.traceMessages, this.traceParameters);
				loggerInstance.LogEvent(DateTime.UtcNow, "---------------------------------------------------------------------------------------------", null);
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00022540 File Offset: 0x00020740
		internal void TraceInformation(Trace tracer, TracingContext tracingContext, string message, params object[] parameters)
		{
			string message2 = message;
			if (parameters != null && parameters.Length > 0)
			{
				message2 = string.Format(message, parameters);
			}
			WTFDiagnostics.TraceInformation(tracer, tracingContext, message2, null, "TraceInformation", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Logging\\TraceCollector.cs", 79);
			lock (this.traceTimes)
			{
				this.traceTimes.Add(DateTime.UtcNow);
				this.traceMessages.Add(message);
				this.traceParameters.Add(parameters);
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000225D0 File Offset: 0x000207D0
		internal void DisposeLoggerInstance(string instanceName)
		{
			lock (TraceCollector.syncLogCache)
			{
				if (TraceCollector.syncLogCache.ContainsKey(instanceName))
				{
					MonitoringLogger monitoringLogger = TraceCollector.syncLogCache[instanceName];
					TraceCollector.syncLogCache.Remove(instanceName);
					monitoringLogger.Dispose();
				}
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00022634 File Offset: 0x00020834
		private MonitoringLogger GetLoggerInstance(string instanceName, MonitoringLogConfiguration logConfiguration)
		{
			MonitoringLogger result;
			lock (TraceCollector.syncLogCache)
			{
				if (TraceCollector.syncLogCache.ContainsKey(instanceName))
				{
					result = TraceCollector.syncLogCache[instanceName];
				}
				else
				{
					MonitoringLogger monitoringLogger = new MonitoringLogger(logConfiguration);
					TraceCollector.syncLogCache.Add(instanceName, monitoringLogger);
					result = monitoringLogger;
				}
			}
			return result;
		}

		// Token: 0x040003B1 RID: 945
		private static readonly Dictionary<string, MonitoringLogger> syncLogCache = new Dictionary<string, MonitoringLogger>();

		// Token: 0x040003B2 RID: 946
		private List<DateTime> traceTimes = new List<DateTime>();

		// Token: 0x040003B3 RID: 947
		private List<string> traceMessages = new List<string>();

		// Token: 0x040003B4 RID: 948
		private List<object[]> traceParameters = new List<object[]>();
	}
}
