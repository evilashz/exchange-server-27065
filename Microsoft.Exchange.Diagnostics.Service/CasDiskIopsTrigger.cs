using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000002 RID: 2
	public class CasDiskIopsTrigger : PerInstanceTrigger
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CasDiskIopsTrigger(IJob job) : base(job, Regex.Escape("LogicalDisk(_Total)\\Disk Transfers/sec"), new PerfLogCounterTrigger.TriggerConfiguration("CasDiskIopsTrigger", 360.0, double.MaxValue, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromHours(1.0), 0), CasDiskIopsTrigger.additionalCounters, new HashSet<string>())
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000021D4 File Offset: 0x000003D4
		protected override string CollectAdditionalInformation(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			PerfLogCounterTrigger.AddFollowUpSteps(stringBuilder, "CAS DiskIOPS Troubleshooting Guide", null);
			stringBuilder.AppendLine("Machine Metrics:");
			IOrderedEnumerable<KeyValuePair<DiagnosticMeasurement, ValueStatistics>> orderedEnumerable = from x in context.AdditionalData
			where !DiagnosticMeasurement.CounterFilterComparer.Comparer.Equals(x.Key, CasDiskIopsTrigger.workingSetPrivate) && x.Value.SampleCount > 0
			orderby x.Key.ObjectName
			select x;
			foreach (KeyValuePair<DiagnosticMeasurement, ValueStatistics> keyValuePair in orderedEnumerable)
			{
				string value = DiagnosticMeasurement.FormatMeasureName(string.Empty, keyValuePair.Key.ObjectName, keyValuePair.Key.CounterName, keyValuePair.Key.InstanceName);
				stringBuilder.Append(value).Append(':').Append('\t').Append(keyValuePair.Value.Mean.Value.ToString("N1")).AppendLine();
			}
			stringBuilder.AppendLine();
			stringBuilder.Append("Top ").Append(10).AppendLine(" Working Set - Private breakdown:");
			stringBuilder.AppendLine("Working Set - Private\tProcess Name");
			IEnumerable<KeyValuePair<DiagnosticMeasurement, ValueStatistics>> enumerable = (from x in context.AdditionalData
			where DiagnosticMeasurement.CounterFilterComparer.Comparer.Equals(x.Key, CasDiskIopsTrigger.workingSetPrivate) && !CasDiskIopsTrigger.excludedAdditionalDataProcessInstances.Contains(x.Key.InstanceName) && x.Value.SampleCount > 0
			orderby x.Value.Mean descending
			select x).Take(10);
			foreach (KeyValuePair<DiagnosticMeasurement, ValueStatistics> keyValuePair2 in enumerable)
			{
				string format = keyValuePair2.Value.Mean.Value.ToString("N1").PadLeft(5);
				stringBuilder.AppendFormat(format, new object[0]).Append('\t').Append(keyValuePair2.Key.InstanceName).AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000001 RID: 1
		private static readonly DiagnosticMeasurement workingSetPrivate = DiagnosticMeasurement.GetMeasure("Process", "Working Set - Private");

		// Token: 0x04000002 RID: 2
		private static readonly HashSet<DiagnosticMeasurement> additionalCounters = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			CasDiskIopsTrigger.workingSetPrivate,
			DiagnosticMeasurement.GetMeasure("Memory", "Available MBytes"),
			DiagnosticMeasurement.GetMeasure("Memory", "Pages/sec")
		};

		// Token: 0x04000003 RID: 3
		private static readonly HashSet<string> excludedAdditionalDataProcessInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"_Total",
			"_Global_",
			"Idle"
		};
	}
}
