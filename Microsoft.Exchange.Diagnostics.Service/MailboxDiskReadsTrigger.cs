using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000012 RID: 18
	public class MailboxDiskReadsTrigger : PerfLogCounterTrigger
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00005E18 File Offset: 0x00004018
		public MailboxDiskReadsTrigger(IJob job) : base(job, "LogicalDisk\\(c:\\\\exchangedbs\\\\.*\\)\\\\Disk Reads/sec", new PerfLogCounterTrigger.TriggerConfiguration("MailboxDiskReadsTrigger", 50.0, double.MaxValue, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromHours(1.0), 0))
		{
			this.databaseInstances = new List<string>();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005E84 File Offset: 0x00004084
		protected override void OnThresholdEvent(PerfLogLine line, PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			foreach (KeyValuePair<DiagnosticMeasurement, float?> keyValuePair in line)
			{
				DiagnosticMeasurement key = keyValuePair.Key;
				float? value = keyValuePair.Value;
				if (value != null && MailboxDiskReadsTrigger.CounterFilter.Contains(key))
				{
					ValueStatistics valueStatistics;
					if (!context.AdditionalData.TryGetValue(key, out valueStatistics))
					{
						context.AdditionalData.Add(key, new ValueStatistics(value));
						if (DiagnosticMeasurement.CounterFilterComparer.Comparer.Equals(key, MailboxDiskReadsTrigger.RpcOperationsPerSec) && !this.databaseInstances.Contains(key.InstanceName))
						{
							this.databaseInstances.Add(key.InstanceName);
						}
					}
					else
					{
						valueStatistics.AddPoint(value);
					}
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005F84 File Offset: 0x00004184
		protected override string CollectAdditionalInformation(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			PerfLogCounterTrigger.AddFollowUpSteps(stringBuilder, "Mailbox Disk IOPS Troubleshooting guide", null);
			stringBuilder.AppendLine("Database metrics:");
			foreach (string text in this.databaseInstances)
			{
				stringBuilder.AppendLine().Append("Database ").Append(text).Append(':').AppendLine().Append('\t').Append('\t');
				foreach (DiagnosticMeasurement diagnosticMeasurement in MailboxDiskReadsTrigger.MSExchangeISStore)
				{
					DiagnosticMeasurement measure = DiagnosticMeasurement.GetMeasure(Environment.MachineName, diagnosticMeasurement.ObjectName, diagnosticMeasurement.CounterName, text);
					this.AppendFormattedMean(context, measure, stringBuilder);
				}
				foreach (DiagnosticMeasurement diagnosticMeasurement2 in MailboxDiskReadsTrigger.DatabaseCounters)
				{
					string text2 = string.Format("Microsoft.Exchange.Store.Worker/{0}", text);
					DiagnosticMeasurement measure2 = DiagnosticMeasurement.GetMeasure(Environment.MachineName, diagnosticMeasurement2.ObjectName, diagnosticMeasurement2.CounterName, text2);
					this.AppendFormattedMean(context, measure2, stringBuilder);
				}
			}
			IEnumerable<KeyValuePair<DiagnosticMeasurement, ValueStatistics>> enumerable = (from x in context.AdditionalData
			where DiagnosticMeasurement.CounterFilterComparer.Comparer.Equals(x.Key, MailboxDiskReadsTrigger.MSExchangeISClientRPCOperationsPerSec)
			orderby x.Value.Mean descending
			select x).Take(5);
			stringBuilder.AppendLine().Append(MailboxDiskReadsTrigger.MSExchangeISClientRPCOperationsPerSec.ObjectName).Append('\\').Append(MailboxDiskReadsTrigger.MSExchangeISClientRPCOperationsPerSec.CounterName).Append(" by instances").Append(':');
			stringBuilder.AppendLine();
			foreach (KeyValuePair<DiagnosticMeasurement, ValueStatistics> keyValuePair in enumerable)
			{
				if (keyValuePair.Value.Mean != null)
				{
					stringBuilder.Append(keyValuePair.Key.InstanceName).Append('\t').Append(keyValuePair.Value.Mean.Value.ToString("N1")).AppendLine().Append('\t').Append('\t');
				}
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000623C File Offset: 0x0000443C
		private void AppendFormattedMean(PerfLogCounterTrigger.SurpassedThresholdContext context, DiagnosticMeasurement counter, StringBuilder builder)
		{
			ValueStatistics valueStatistics;
			if (context.AdditionalData.TryGetValue(counter, out valueStatistics) && valueStatistics.Mean != null)
			{
				builder.Append(counter.ObjectName).Append('\\').Append(counter.CounterName).Append('\t').Append(valueStatistics.Mean.Value.ToString("N1")).AppendLine().Append('\t').Append('\t');
			}
		}

		// Token: 0x04000043 RID: 67
		private static readonly DiagnosticMeasurement RpcOperationsPerSec = DiagnosticMeasurement.GetMeasure("MSExchangeIS Store", "RPC Operations/sec");

		// Token: 0x04000044 RID: 68
		private static readonly DiagnosticMeasurement RpcAverageLatency = DiagnosticMeasurement.GetMeasure("MSExchangeIS Store", "RPC Averaged Latency");

		// Token: 0x04000045 RID: 69
		private static readonly DiagnosticMeasurement IODatabaseReadsAttachedPerSec = DiagnosticMeasurement.GetMeasure("MSExchange Database ==> Instances", "I/O Database Reads (Attached)/sec");

		// Token: 0x04000046 RID: 70
		private static readonly DiagnosticMeasurement IODatabaseWritesAttachedPerSec = DiagnosticMeasurement.GetMeasure("MSExchange Database ==> Instances", "I/O Database Writes (Attached)/sec");

		// Token: 0x04000047 RID: 71
		private static readonly DiagnosticMeasurement IOLogReadsSec = DiagnosticMeasurement.GetMeasure("MSExchange Database ==> Instances", "I/O Log Reads/sec");

		// Token: 0x04000048 RID: 72
		private static readonly DiagnosticMeasurement IOLogWritessec = DiagnosticMeasurement.GetMeasure("MSExchange Database ==> Instances", "I/O Log Writes/sec");

		// Token: 0x04000049 RID: 73
		private static readonly DiagnosticMeasurement MSExchangeISClientRPCOperationsPerSec = DiagnosticMeasurement.GetMeasure("MSExchangeIS Client", "RPC Operations/sec");

		// Token: 0x0400004A RID: 74
		private static readonly HashSet<DiagnosticMeasurement> MSExchangeISStore = new HashSet<DiagnosticMeasurement>
		{
			MailboxDiskReadsTrigger.RpcOperationsPerSec,
			MailboxDiskReadsTrigger.RpcAverageLatency
		};

		// Token: 0x0400004B RID: 75
		private static readonly HashSet<DiagnosticMeasurement> DatabaseCounters = new HashSet<DiagnosticMeasurement>
		{
			MailboxDiskReadsTrigger.IODatabaseReadsAttachedPerSec,
			MailboxDiskReadsTrigger.IODatabaseWritesAttachedPerSec,
			MailboxDiskReadsTrigger.IOLogReadsSec,
			MailboxDiskReadsTrigger.IOLogWritessec
		};

		// Token: 0x0400004C RID: 76
		private static readonly HashSet<DiagnosticMeasurement> CounterFilter = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			MailboxDiskReadsTrigger.RpcOperationsPerSec,
			MailboxDiskReadsTrigger.RpcAverageLatency,
			MailboxDiskReadsTrigger.IODatabaseReadsAttachedPerSec,
			MailboxDiskReadsTrigger.IODatabaseWritesAttachedPerSec,
			MailboxDiskReadsTrigger.IOLogReadsSec,
			MailboxDiskReadsTrigger.IOLogWritessec,
			MailboxDiskReadsTrigger.MSExchangeISClientRPCOperationsPerSec
		};

		// Token: 0x0400004D RID: 77
		private readonly List<string> databaseInstances;
	}
}
