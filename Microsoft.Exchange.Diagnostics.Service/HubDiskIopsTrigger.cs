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
	// Token: 0x0200000A RID: 10
	public class HubDiskIopsTrigger : PerInstanceTrigger
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00003C08 File Offset: 0x00001E08
		public HubDiskIopsTrigger(IJob job) : base(job, Regex.Escape("LogicalDisk(_Total)\\Disk Transfers/sec"), new PerfLogCounterTrigger.TriggerConfiguration("HubDiskIopsTrigger", 360.0, double.MaxValue, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromHours(1.0), 0), HubDiskIopsTrigger.additionalCounters, new HashSet<string>())
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003C98 File Offset: 0x00001E98
		protected override string CollectAdditionalInformation(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			PerfLogCounterTrigger.AddFollowUpSteps(stringBuilder, "HUB DiskIOPS Troubleshooting Guide", null);
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

		// Token: 0x04000027 RID: 39
		private static readonly HashSet<DiagnosticMeasurement> additionalCounters = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			DiagnosticMeasurement.GetMeasure("LogicalDisk", "Avg. Disk sec/Read"),
			DiagnosticMeasurement.GetMeasure("LogicalDisk", "Avg. Disk sec/Write"),
			DiagnosticMeasurement.GetMeasure("LogicalDisk", "Current Disk Queue Length"),
			DiagnosticMeasurement.GetMeasure("LogicalDisk", "Disk Reads/sec"),
			DiagnosticMeasurement.GetMeasure("LogicalDisk", "Disk Writes/sec"),
			DiagnosticMeasurement.GetMeasure("MSExchangeTransport SmtpSend", "Messages Sent/sec"),
			DiagnosticMeasurement.GetMeasure("MSExchangeTransport SMTPReceive", "Messages Received/sec"),
			DiagnosticMeasurement.GetMeasure("MSExchangeTransport Queues", "Aggregate Delivery Queue Length (All Queues)")
		};
	}
}
