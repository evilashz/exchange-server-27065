using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.CalculatedCounters
{
	// Token: 0x02000005 RID: 5
	public class DatabaseMountedBitmapCalculatedCounter : MultiInstanceSingleObjectCalculatedCounter
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002D9C File Offset: 0x00000F9C
		public DatabaseMountedBitmapCalculatedCounter() : base("MSExchange Active Manager", "Database Mounted Bitmap", new string[]
		{
			"Database Mounted"
		})
		{
			this.stepDuration = Configuration.GetConfigTimeSpan("PerfLogAggregatorAnalyzerStepDuration", TimeSpan.FromMinutes(1.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(5.0));
			this.previousTimestamp = DateTime.MinValue;
			this.isEnabled = (this.stepDuration.Ticks != 0L && ServerRole.IsRole("Mailbox"));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002E40 File Offset: 0x00001040
		public override void OnLogLine(Dictionary<DiagnosticMeasurement, float?> countersAndValues, DateTime? timestamp)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (timestamp == null)
			{
				return;
			}
			foreach (KeyValuePair<string, DiagnosticMeasurement[]> keyValuePair in base.Instances)
			{
				float? num = countersAndValues[keyValuePair.Value[0]];
				DiagnosticMeasurement key = keyValuePair.Value[1];
				if (num != null && !"_total".Equals(keyValuePair.Key, StringComparison.OrdinalIgnoreCase) && Math.Abs((timestamp.Value - this.previousTimestamp).TotalMilliseconds) > 900.0)
				{
					float? value = new float?((float)((int)num.Value << (int)(timestamp.Value.Ticks % this.stepDuration.Ticks / this.perfSampleIntervalTickes)));
					countersAndValues.Add(key, value);
				}
			}
			this.previousTimestamp = timestamp.Value;
		}

		// Token: 0x0400001C RID: 28
		public const string PerfObjectName = "MSExchange Active Manager";

		// Token: 0x0400001D RID: 29
		public const string DatabaseMountedBitmapCounterName = "Database Mounted Bitmap";

		// Token: 0x0400001E RID: 30
		public const string DatabaseMountedCounterName = "Database Mounted";

		// Token: 0x0400001F RID: 31
		public const string StepDurationConfigName = "PerfLogAggregatorAnalyzerStepDuration";

		// Token: 0x04000020 RID: 32
		public const string TotalInstanceName = "_total";

		// Token: 0x04000021 RID: 33
		private const int MinimumIntervalBetweenSamples = 900;

		// Token: 0x04000022 RID: 34
		private readonly long perfSampleIntervalTickes = new TimeSpan(0, 0, 15).Ticks;

		// Token: 0x04000023 RID: 35
		private readonly TimeSpan stepDuration;

		// Token: 0x04000024 RID: 36
		private readonly bool isEnabled;

		// Token: 0x04000025 RID: 37
		private DateTime previousTimestamp;
	}
}
