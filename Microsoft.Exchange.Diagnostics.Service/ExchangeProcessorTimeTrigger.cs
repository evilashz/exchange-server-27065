using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000005 RID: 5
	public class ExchangeProcessorTimeTrigger : ProcessorTimeTrigger
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00003308 File Offset: 0x00001508
		public ExchangeProcessorTimeTrigger(IJob job) : base(job)
		{
			base.AdditionalCounters.Add(ExchangeProcessorTimeTrigger.totalKernelCPUCounter);
			if (ExchangeProcessorTimeTrigger.isCafeServer || ExchangeProcessorTimeTrigger.isMailboxServer)
			{
				base.AdditionalCounters.Add(ExchangeProcessorTimeTrigger.aspNetRequestRateCounter);
			}
			if (ExchangeProcessorTimeTrigger.isMailboxServer)
			{
				foreach (DiagnosticMeasurement item in ExchangeProcessorTimeTrigger.totalMBXPerfCounters)
				{
					base.AdditionalCounters.Add(item);
				}
				base.AdditionalCounters.Add(ExchangeProcessorTimeTrigger.rpcOperationRateCounter);
				base.AdditionalCounters.Add(ExchangeProcessorTimeTrigger.rpcAverageLatencyCounter);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000033C0 File Offset: 0x000015C0
		protected override bool ShouldTrigger(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			return this.HasMailboxDatabaseMounted(context) && base.ShouldTrigger(context);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000033D4 File Offset: 0x000015D4
		protected override string CollectAdditionalInformation(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append(base.CollectAdditionalInformation(context));
			ValueStatistics valueStatistics;
			if (context.AdditionalData.TryGetValue(ExchangeProcessorTimeTrigger.totalKernelCPUCounter, out valueStatistics))
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat("The total CPU in Kernel : {0}", valueStatistics.Mean.ToString());
				stringBuilder.AppendLine();
			}
			if (ExchangeProcessorTimeTrigger.isMailboxServer)
			{
				foreach (DiagnosticMeasurement diagnosticMeasurement in ExchangeProcessorTimeTrigger.totalMBXPerfCounters)
				{
					if (context.AdditionalData.TryGetValue(diagnosticMeasurement, out valueStatistics))
					{
						stringBuilder.AppendLine();
						stringBuilder.AppendFormat("{0} : {1}", diagnosticMeasurement.ToString(), valueStatistics.Mean.ToString());
						stringBuilder.AppendLine();
					}
				}
			}
			Dictionary<DiagnosticMeasurement, ValueStatistics> dictionary = new Dictionary<DiagnosticMeasurement, ValueStatistics>(30);
			Dictionary<DiagnosticMeasurement, ValueStatistics> dictionary2 = new Dictionary<DiagnosticMeasurement, ValueStatistics>(30);
			Dictionary<DiagnosticMeasurement, ValueStatistics> dictionary3 = new Dictionary<DiagnosticMeasurement, ValueStatistics>(20);
			foreach (KeyValuePair<DiagnosticMeasurement, ValueStatistics> keyValuePair in context.AdditionalData)
			{
				if (DiagnosticMeasurement.CounterFilterComparer.Comparer.Equals(keyValuePair.Key, ExchangeProcessorTimeTrigger.rpcOperationRateCounter) && keyValuePair.Value.SampleCount > 0)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
				if (DiagnosticMeasurement.CounterFilterComparer.Comparer.Equals(keyValuePair.Key, ExchangeProcessorTimeTrigger.rpcAverageLatencyCounter) && keyValuePair.Value.SampleCount > 0)
				{
					dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
				}
				if (DiagnosticMeasurement.CounterFilterComparer.Comparer.Equals(keyValuePair.Key, ExchangeProcessorTimeTrigger.aspNetRequestRateCounter) && keyValuePair.Value.SampleCount > 0)
				{
					string text = keyValuePair.Key.InstanceName.Substring(keyValuePair.Key.InstanceName.IndexOf("_") + 1);
					DiagnosticMeasurement measure = DiagnosticMeasurement.GetMeasure(keyValuePair.Key.ObjectName, keyValuePair.Key.CounterName, text);
					ValueStatistics valueStatistics2;
					if (!dictionary3.TryGetValue(measure, out valueStatistics2))
					{
						valueStatistics2 = new ValueStatistics(null);
						dictionary3.Add(measure, valueStatistics2);
					}
					valueStatistics2.AddPoint(keyValuePair.Value);
				}
			}
			if (ExchangeProcessorTimeTrigger.isCafeServer || ExchangeProcessorTimeTrigger.isMailboxServer)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Top 15 W3WP AppPools with the highest request rate:");
				stringBuilder.AppendLine("Requests/sec: \t W3WP AppPool");
				this.OutputValuesInDescendingOrder(dictionary3, stringBuilder, 15);
			}
			if (ExchangeProcessorTimeTrigger.isMailboxServer)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Top 15 components with the highest ROPs rate:");
				stringBuilder.AppendLine("RPC Operations/sec: \t Component");
				this.OutputValuesInDescendingOrder(dictionary, stringBuilder, 15);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Top 15 components with the highest RPC average latency:");
				stringBuilder.AppendLine("RPC Average Latency: \t Component");
				this.OutputValuesInDescendingOrder(dictionary2, stringBuilder, 15);
			}
			if (PerfLogCounterTrigger.IsDatacenter)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(string.Format("Additional trace files can be found at \\\\{0}\\EDSLogs\\Dumps", Environment.MachineName));
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003738 File Offset: 0x00001938
		private void OutputValuesInDescendingOrder(Dictionary<DiagnosticMeasurement, ValueStatistics> values, StringBuilder builder, int top)
		{
			if (values != null && values.Count > 0)
			{
				IEnumerable<KeyValuePair<DiagnosticMeasurement, ValueStatistics>> enumerable = (from x in values
				orderby x.Value.Mean descending
				select x).Take(top);
				using (IEnumerator<KeyValuePair<DiagnosticMeasurement, ValueStatistics>> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<DiagnosticMeasurement, ValueStatistics> keyValuePair = enumerator.Current;
						string arg = keyValuePair.Value.Mean.Value.ToString("N1").PadLeft(5);
						builder.AppendFormat("{0}\t{1}", arg, keyValuePair.Key.InstanceName);
						builder.AppendLine();
					}
					return;
				}
			}
			builder.AppendLine("No Data Available.");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003810 File Offset: 0x00001A10
		private bool HasMailboxDatabaseMounted(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			ValueStatistics valueStatistics = new ValueStatistics(null);
			if (context.AdditionalData.TryGetValue(ExchangeProcessorTimeTrigger.totalDatabaseMountedCounter, out valueStatistics))
			{
				float? mean = valueStatistics.Mean;
				if (mean.GetValueOrDefault() == 0f && mean != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000017 RID: 23
		private static readonly bool isMailboxServer = ServerRole.IsRole("Mailbox");

		// Token: 0x04000018 RID: 24
		private static readonly bool isCafeServer = ServerRole.IsRole("Cafe");

		// Token: 0x04000019 RID: 25
		private static readonly DiagnosticMeasurement totalKernelCPUCounter = DiagnosticMeasurement.GetMeasure(Environment.MachineName, "Processor", "% Privileged Time", "_Total");

		// Token: 0x0400001A RID: 26
		private static readonly DiagnosticMeasurement totalDatabaseMountedCounter = DiagnosticMeasurement.GetMeasure(Environment.MachineName, "MSExchange Active Manager", "Database Mounted", "_Total");

		// Token: 0x0400001B RID: 27
		private static readonly List<DiagnosticMeasurement> totalMBXPerfCounters = new List<DiagnosticMeasurement>
		{
			ExchangeProcessorTimeTrigger.totalDatabaseMountedCounter,
			DiagnosticMeasurement.GetMeasure(Environment.MachineName, "MSExchange Active Manager", "Database Copy Role Active", "_Total"),
			DiagnosticMeasurement.GetMeasure(Environment.MachineName, "MSExchange Search Indexes", "Crawler: Mailboxes Remaining", "_Total"),
			DiagnosticMeasurement.GetMeasure(Environment.MachineName, "MSExchange Search Indexes", "Notifications: Awaiting Processing", "_Total"),
			DiagnosticMeasurement.GetMeasure(Environment.MachineName, "MSExchange Search Indexes", "Retry: Retriable Items", "_Total")
		};

		// Token: 0x0400001C RID: 28
		private static readonly DiagnosticMeasurement rpcOperationRateCounter = DiagnosticMeasurement.GetMeasure("MSExchangeIS Client Type", "RPC Operations/sec");

		// Token: 0x0400001D RID: 29
		private static readonly DiagnosticMeasurement rpcAverageLatencyCounter = DiagnosticMeasurement.GetMeasure("MSExchangeIS Client Type", "RPC Average Latency");

		// Token: 0x0400001E RID: 30
		private static readonly DiagnosticMeasurement aspNetRequestRateCounter = DiagnosticMeasurement.GetMeasure("W3SVC_W3WP", "Requests / Sec");
	}
}
