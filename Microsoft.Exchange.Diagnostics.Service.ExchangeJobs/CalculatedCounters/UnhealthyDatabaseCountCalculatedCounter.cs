using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.CalculatedCounters
{
	// Token: 0x02000009 RID: 9
	public class UnhealthyDatabaseCountCalculatedCounter : ICalculatedCounter
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00003CDC File Offset: 0x00001EDC
		public UnhealthyDatabaseCountCalculatedCounter()
		{
			this.databases = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.IsValidRole = ServerRole.IsRole("Mailbox");
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003D04 File Offset: 0x00001F04
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00003D0C File Offset: 0x00001F0C
		internal bool IsValidRole { get; set; }

		// Token: 0x0600003C RID: 60 RVA: 0x00003D15 File Offset: 0x00001F15
		public void OnLogHeader(List<KeyValuePair<int, DiagnosticMeasurement>> currentInputCounters)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003D18 File Offset: 0x00001F18
		public void OnLogLine(Dictionary<DiagnosticMeasurement, float?> countersAndValues, DateTime? timestamp = null)
		{
			if (this.IsValidRole)
			{
				foreach (KeyValuePair<DiagnosticMeasurement, float?> keyValuePair in countersAndValues)
				{
					if (keyValuePair.Key.ObjectName.Equals("MSExchange Replication", StringComparison.OrdinalIgnoreCase) && UnhealthyDatabaseCountCalculatedCounter.ReplicationCounters.Contains(keyValuePair.Key.CounterName) && !keyValuePair.Key.InstanceName.Equals("_Total", StringComparison.OrdinalIgnoreCase) && keyValuePair.Key.InstanceName.IndexOf("Mailbox Database", StringComparison.OrdinalIgnoreCase) < 0 && keyValuePair.Value != null && keyValuePair.Value.Value > 0f)
					{
						this.databases.Add(keyValuePair.Key.InstanceName);
					}
				}
				countersAndValues.Add(UnhealthyDatabaseCountCalculatedCounter.Measure, new float?((float)this.databases.Count));
				this.databases.Clear();
			}
		}

		// Token: 0x04000052 RID: 82
		public const string ReplicationObjectName = "MSExchange Replication";

		// Token: 0x04000053 RID: 83
		public const string UnhealthyCounterName = "Unhealthy";

		// Token: 0x04000054 RID: 84
		private static readonly HashSet<string> ReplicationCounters = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"Suspended",
			"Seeding",
			"Failed",
			"FailedSuspended"
		};

		// Token: 0x04000055 RID: 85
		private static readonly DiagnosticMeasurement Measure = DiagnosticMeasurement.GetMeasure(Environment.MachineName, "MSExchange Replication", "Unhealthy", string.Empty);

		// Token: 0x04000056 RID: 86
		private readonly HashSet<string> databases;
	}
}
