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
	// Token: 0x02000004 RID: 4
	public class DomainControllerDiskIopsTrigger : PerInstanceTrigger
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00003020 File Offset: 0x00001220
		public DomainControllerDiskIopsTrigger(IJob job) : base(job, Regex.Escape("LogicalDisk(_Total)\\Disk Transfers/sec"), new PerfLogCounterTrigger.TriggerConfiguration("DomainControllerDiskIopsTrigger", 1600.0, double.MaxValue, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromHours(1.0), 0), DomainControllerDiskIopsTrigger.additionalCounters, new HashSet<string>())
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000030B0 File Offset: 0x000012B0
		protected override string CollectAdditionalInformation(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			PerfLogCounterTrigger.AddFollowUpSteps(stringBuilder, "DomainController DiskIOPS Troubleshooting Guide", null);
			stringBuilder.AppendLine("Machine Metrics:");
			IOrderedEnumerable<KeyValuePair<DiagnosticMeasurement, ValueStatistics>> orderedEnumerable = from x in context.AdditionalData
			where x.Value.SampleCount > 0
			orderby x.Key.ObjectName
			select x;
			foreach (KeyValuePair<DiagnosticMeasurement, ValueStatistics> keyValuePair in orderedEnumerable)
			{
				string value = DiagnosticMeasurement.FormatMeasureName(string.Empty, keyValuePair.Key.ObjectName, keyValuePair.Key.CounterName, keyValuePair.Key.InstanceName);
				stringBuilder.Append(value).Append(':').Append('\t').Append(keyValuePair.Value.Mean.Value.ToString("N1")).AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000014 RID: 20
		private static readonly HashSet<DiagnosticMeasurement> additionalCounters = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			DiagnosticMeasurement.GetMeasure("LogicalDisk", "Avg. Disk sec/Read"),
			DiagnosticMeasurement.GetMeasure("LogicalDisk", "Avg. Disk sec/Write"),
			DiagnosticMeasurement.GetMeasure("LogicalDisk", "Current Disk Queue Length"),
			DiagnosticMeasurement.GetMeasure("LogicalDisk", "Disk Reads/sec"),
			DiagnosticMeasurement.GetMeasure("LogicalDisk", "Disk Writes/sec"),
			DiagnosticMeasurement.GetMeasure("DirectoryServices", "LDAP Searches/sec"),
			DiagnosticMeasurement.GetMeasure("DirectoryServices", "DS Search sub-operations/sec"),
			DiagnosticMeasurement.GetMeasure("DirectoryServices", "ATQ Outstanding Queued Requests"),
			DiagnosticMeasurement.GetMeasure("DirectoryServices", "ATQ Request Latency"),
			DiagnosticMeasurement.GetMeasure("DirectoryServices", "ATQ Threads LDAP"),
			DiagnosticMeasurement.GetMeasure("DirectoryServices", "ATQ Threads Other"),
			DiagnosticMeasurement.GetMeasure("DirectoryServices", "ATQ Threads Total")
		};
	}
}
