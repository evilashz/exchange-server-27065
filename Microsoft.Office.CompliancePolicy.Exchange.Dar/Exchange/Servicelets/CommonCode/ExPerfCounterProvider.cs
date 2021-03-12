using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.Monitor;

namespace Microsoft.Exchange.Servicelets.CommonCode
{
	// Token: 0x02000021 RID: 33
	public sealed class ExPerfCounterProvider : PerfCounterProvider
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000656C File Offset: 0x0000476C
		public ExPerfCounterProvider(string categoryName, IEnumerable<ExPerformanceCounter> perfCounters) : base(categoryName)
		{
			if (perfCounters == null || !perfCounters.Any<ExPerformanceCounter>())
			{
				throw new ArgumentException("The perf counter collection is set to null or empty", "perfCounters");
			}
			this.perfCounters = perfCounters.ToDictionary((ExPerformanceCounter p) => p.CounterName);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000065C4 File Offset: 0x000047C4
		public override void Increment(string counterName)
		{
			ExPerformanceCounter exPerformanceCounter = this.perfCounters[counterName];
			exPerformanceCounter.Increment();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000065E8 File Offset: 0x000047E8
		public override void Increment(string counterName, string baseCounterName)
		{
			ExPerformanceCounter exPerformanceCounter = this.perfCounters[counterName];
			ExPerformanceCounter exPerformanceCounter2 = this.perfCounters[baseCounterName];
			exPerformanceCounter.Increment();
			exPerformanceCounter2.Increment();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006620 File Offset: 0x00004820
		public override void IncrementBy(string counterName, long incrementValue)
		{
			ExPerformanceCounter exPerformanceCounter = this.perfCounters[counterName];
			exPerformanceCounter.IncrementBy(incrementValue);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006644 File Offset: 0x00004844
		public override void IncrementBy(string counterName, long incrementValue, string baseCounterName)
		{
			ExPerformanceCounter exPerformanceCounter = this.perfCounters[counterName];
			ExPerformanceCounter exPerformanceCounter2 = this.perfCounters[baseCounterName];
			exPerformanceCounter.IncrementBy(incrementValue);
			exPerformanceCounter2.Increment();
		}

		// Token: 0x040000A8 RID: 168
		private readonly Dictionary<string, ExPerformanceCounter> perfCounters;
	}
}
