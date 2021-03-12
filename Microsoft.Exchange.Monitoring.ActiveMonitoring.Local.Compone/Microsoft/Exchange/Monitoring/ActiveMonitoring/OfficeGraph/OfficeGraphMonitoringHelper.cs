using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OfficeGraph
{
	// Token: 0x02000252 RID: 594
	internal static class OfficeGraphMonitoringHelper
	{
		// Token: 0x060010A8 RID: 4264 RVA: 0x0006EDF0 File Offset: 0x0006CFF0
		static OfficeGraphMonitoringHelper()
		{
			MonitoringLogConfiguration configuration = new MonitoringLogConfiguration(ExchangeComponent.OfficeGraph.Name, "Monitoring");
			OfficeGraphMonitoringHelper.monitoringLogger = new MonitoringLogger(configuration);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0006EE38 File Offset: 0x0006D038
		internal static long GetPerformanceCounterValue(string categoryName, string counterName)
		{
			long result = 0L;
			using (PerformanceCounter performanceCounter = new PerformanceCounter(categoryName, counterName, true))
			{
				result = performanceCounter.RawValue;
			}
			return result;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0006EE78 File Offset: 0x0006D078
		internal static long GetPerformanceCounterValue(string categoryName, string counterName, string instanceName)
		{
			long result = 0L;
			using (PerformanceCounter performanceCounter = new PerformanceCounter(categoryName, counterName, instanceName, true))
			{
				result = performanceCounter.RawValue;
			}
			return result;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0006EEDC File Offset: 0x0006D0DC
		internal static ProbeResult GetLastProbeResult(ProbeWorkItem probe, IProbeWorkBroker broker, CancellationToken cancellationToken)
		{
			ProbeResult lastProbeResult = null;
			if (broker != null)
			{
				IOrderedEnumerable<ProbeResult> query = from r in broker.GetProbeResults(probe.Definition, probe.Result.ExecutionStartTime.AddSeconds((double)(-5 * probe.Definition.RecurrenceIntervalSeconds)))
				orderby r.ExecutionStartTime descending
				select r;
				Task<int> task = broker.AsDataAccessQuery<ProbeResult>(query).ExecuteAsync(delegate(ProbeResult r)
				{
					if (lastProbeResult == null)
					{
						lastProbeResult = r;
					}
				}, cancellationToken, OfficeGraphMonitoringHelper.traceContext);
				task.Wait(cancellationToken);
				return lastProbeResult;
			}
			if (ExEnvironment.IsTest)
			{
				return null;
			}
			throw new ArgumentNullException("broker");
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0006EF8C File Offset: 0x0006D18C
		internal static MailboxDatabaseInfo GetDatabaseInfo(string databaseName)
		{
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				throw new ArgumentException("databaseName");
			}
			lock (OfficeGraphMonitoringHelper.databaseInfoDict)
			{
				if (OfficeGraphMonitoringHelper.databaseInfoDict.ContainsKey(databaseName))
				{
					return OfficeGraphMonitoringHelper.databaseInfoDict[databaseName];
				}
			}
			ICollection<MailboxDatabaseInfo> mailboxDatabaseInfoCollectionForBackend = LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
			MailboxDatabaseInfo result;
			lock (OfficeGraphMonitoringHelper.databaseInfoDict)
			{
				if (!OfficeGraphMonitoringHelper.databaseInfoDict.ContainsKey(databaseName))
				{
					OfficeGraphMonitoringHelper.databaseInfoDict.Clear();
					foreach (MailboxDatabaseInfo mailboxDatabaseInfo in mailboxDatabaseInfoCollectionForBackend)
					{
						OfficeGraphMonitoringHelper.databaseInfoDict.Add(mailboxDatabaseInfo.MailboxDatabaseName, mailboxDatabaseInfo);
					}
				}
				if (OfficeGraphMonitoringHelper.databaseInfoDict.ContainsKey(databaseName))
				{
					result = OfficeGraphMonitoringHelper.databaseInfoDict[databaseName];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0006F0BC File Offset: 0x0006D2BC
		internal static long GetDirectorySize(string directory)
		{
			return new DirectoryInfo(directory).GetFiles("*.*", SearchOption.AllDirectories).Sum((FileInfo file) => file.Length);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0006F0F1 File Offset: 0x0006D2F1
		internal static void LogInfo(string message, params object[] messageArgs)
		{
			OfficeGraphMonitoringHelper.monitoringLogger.LogEvent(DateTime.UtcNow, message, messageArgs);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0006F104 File Offset: 0x0006D304
		internal static void LogInfo(WorkItem workItem, string message, params object[] messageArgs)
		{
			OfficeGraphMonitoringHelper.monitoringLogger.LogEvent(DateTime.UtcNow, string.Format("{0}/{1}: ", workItem.Definition.Name, workItem.Definition.TargetResource) + message, messageArgs);
		}

		// Token: 0x04000C72 RID: 3186
		internal const string MSExchangeMailboxTransportDeliveryServiceName = "MSExchangeDelivery";

		// Token: 0x04000C73 RID: 3187
		internal const string MSMessageTracingClientServiceName = "MSMessageTracingClient";

		// Token: 0x04000C74 RID: 3188
		internal const string MSExchangeDeliveryExtensibilityAgentsCounterCategoryName = "MSExchange Delivery Extensibility Agents";

		// Token: 0x04000C75 RID: 3189
		internal const string AverageAgentProcessingTimeCounterName = "Average Agent Processing Time (sec)";

		// Token: 0x04000C76 RID: 3190
		internal const string OfficeGraphAgentInstanceName = "office graph agent";

		// Token: 0x04000C77 RID: 3191
		internal const string OfficeGraphWriterMessageTracingPluginCounterCategoryName = "Office Graph Writer - Message Tracing Plugin";

		// Token: 0x04000C78 RID: 3192
		internal const string AverageSignalProcessingTimeCounterName = "Average Signal Processing Time";

		// Token: 0x04000C79 RID: 3193
		internal const string MessageTracingPluginLogDirectory = "D:\\OfficeGraph";

		// Token: 0x04000C7A RID: 3194
		private static readonly TracingContext traceContext = TracingContext.Default;

		// Token: 0x04000C7B RID: 3195
		private static MonitoringLogger monitoringLogger;

		// Token: 0x04000C7C RID: 3196
		private static Dictionary<string, MailboxDatabaseInfo> databaseInfoDict = new Dictionary<string, MailboxDatabaseInfo>(StringComparer.OrdinalIgnoreCase);
	}
}
