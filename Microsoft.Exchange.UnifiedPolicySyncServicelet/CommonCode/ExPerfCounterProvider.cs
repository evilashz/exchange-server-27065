using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.Monitor;

namespace Microsoft.Exchange.Servicelets.CommonCode
{
	// Token: 0x02000008 RID: 8
	public sealed class ExPerfCounterProvider : PerfCounterProvider
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002F00 File Offset: 0x00001100
		public ExPerfCounterProvider(string categoryName, IEnumerable<ExPerformanceCounter> perfCounters) : base(categoryName)
		{
			if (perfCounters == null || !perfCounters.Any<ExPerformanceCounter>())
			{
				throw new ArgumentException("The perf counter collection is set to null or empty", "perfCounters");
			}
			this.perfCounters = perfCounters.ToDictionary((ExPerformanceCounter p) => p.CounterName);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002F58 File Offset: 0x00001158
		public override void Increment(string counterName)
		{
			ExPerformanceCounter exPerformanceCounter = this.perfCounters[counterName];
			exPerformanceCounter.Increment();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002F7C File Offset: 0x0000117C
		public override void Increment(string counterName, string baseCounterName)
		{
			ExPerformanceCounter exPerformanceCounter = this.perfCounters[counterName];
			ExPerformanceCounter exPerformanceCounter2 = this.perfCounters[baseCounterName];
			exPerformanceCounter.Increment();
			exPerformanceCounter2.Increment();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002FB4 File Offset: 0x000011B4
		public override void IncrementBy(string counterName, long incrementValue)
		{
			ExPerformanceCounter exPerformanceCounter = this.perfCounters[counterName];
			exPerformanceCounter.IncrementBy(incrementValue);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002FD8 File Offset: 0x000011D8
		public override void IncrementBy(string counterName, long incrementValue, string baseCounterName)
		{
			ExPerformanceCounter exPerformanceCounter = this.perfCounters[counterName];
			ExPerformanceCounter exPerformanceCounter2 = this.perfCounters[baseCounterName];
			exPerformanceCounter.IncrementBy(incrementValue);
			exPerformanceCounter2.Increment();
		}

		// Token: 0x0400005A RID: 90
		private readonly Dictionary<string, ExPerformanceCounter> perfCounters;
	}
}
