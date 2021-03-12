using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.CalculatedCounters
{
	// Token: 0x02000006 RID: 6
	public class DiskFreeSpaceCalculatedCounter : ICalculatedCounter
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002F9A File Offset: 0x0000119A
		public DiskFreeSpaceCalculatedCounter()
		{
			this.percentFreeSpaceValues = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase);
			this.freeMegabyteValues = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase);
			this.IsValidRole = ServerRole.IsRole("Mailbox");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002FD2 File Offset: 0x000011D2
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002FDA File Offset: 0x000011DA
		internal bool IsValidRole { get; set; }

		// Token: 0x0600001D RID: 29 RVA: 0x00002FE3 File Offset: 0x000011E3
		public void OnLogHeader(List<KeyValuePair<int, DiagnosticMeasurement>> currentInputCounters)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002FE8 File Offset: 0x000011E8
		public void OnLogLine(Dictionary<DiagnosticMeasurement, float?> countersAndValues, DateTime? timestamp = null)
		{
			if (this.IsValidRole)
			{
				foreach (KeyValuePair<DiagnosticMeasurement, float?> keyValuePair in countersAndValues)
				{
					if (keyValuePair.Key.ObjectName.Equals("LogicalDisk", StringComparison.OrdinalIgnoreCase) && (keyValuePair.Key.InstanceName.IndexOf("ExchangeVolumes", StringComparison.OrdinalIgnoreCase) >= 0 || keyValuePair.Key.InstanceName.IndexOf("ExchangeDBs", StringComparison.OrdinalIgnoreCase) >= 0))
					{
						Dictionary<string, float> dictionary = null;
						if (keyValuePair.Key.CounterName.Equals("Free Megabytes", StringComparison.OrdinalIgnoreCase))
						{
							dictionary = this.freeMegabyteValues;
						}
						else if (keyValuePair.Key.CounterName.Equals("% Free Space", StringComparison.OrdinalIgnoreCase))
						{
							dictionary = this.percentFreeSpaceValues;
						}
						if (dictionary != null && keyValuePair.Value != null)
						{
							dictionary[keyValuePair.Key.InstanceName] = keyValuePair.Value.Value;
						}
					}
				}
				float num = 0f;
				float num2 = 0f;
				foreach (KeyValuePair<string, float> keyValuePair2 in this.freeMegabyteValues)
				{
					float num3;
					if (this.percentFreeSpaceValues.TryGetValue(keyValuePair2.Key, out num3) && num3 > 0f)
					{
						num2 += keyValuePair2.Value;
						num += keyValuePair2.Value / (num3 / 100f);
					}
				}
				float value = (num > 0f) ? ((float)Math.Round((double)(num2 / num * 100f))) : 0f;
				countersAndValues.Add(DiskFreeSpaceCalculatedCounter.SyntheticPercentFreeSpaceMeasure, new float?(value));
				countersAndValues.Add(DiskFreeSpaceCalculatedCounter.SyntheticFreeMegabytesMeasure, new float?(num2));
				this.percentFreeSpaceValues.Clear();
				this.freeMegabyteValues.Clear();
			}
		}

		// Token: 0x04000026 RID: 38
		public const string SyntheticDiskObjectName = "SyntheticDisk";

		// Token: 0x04000027 RID: 39
		public const string LogicalDiskObjectName = "LogicalDisk";

		// Token: 0x04000028 RID: 40
		public const string PercentFreeSpaceCounterName = "% Free Space";

		// Token: 0x04000029 RID: 41
		public const string FreeMegabytesCounterName = "Free Megabytes";

		// Token: 0x0400002A RID: 42
		private static readonly DiagnosticMeasurement SyntheticPercentFreeSpaceMeasure = DiagnosticMeasurement.GetMeasure(Environment.MachineName, "SyntheticDisk", "% Free Space", string.Empty);

		// Token: 0x0400002B RID: 43
		private static readonly DiagnosticMeasurement SyntheticFreeMegabytesMeasure = DiagnosticMeasurement.GetMeasure(Environment.MachineName, "SyntheticDisk", "Free Megabytes", string.Empty);

		// Token: 0x0400002C RID: 44
		private readonly Dictionary<string, float> percentFreeSpaceValues;

		// Token: 0x0400002D RID: 45
		private readonly Dictionary<string, float> freeMegabyteValues;
	}
}
