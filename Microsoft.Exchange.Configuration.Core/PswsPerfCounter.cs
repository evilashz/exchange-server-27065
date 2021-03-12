using System;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000026 RID: 38
	public class PswsPerfCounter
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000669D File Offset: 0x0000489D
		internal static PswshttpRequestPerformanceCountersInstance Instance
		{
			get
			{
				if (PswsPerfCounter.perfCounter == null)
				{
					PswsPerfCounter.InitializePerfCounter();
				}
				return PswsPerfCounter.perfCounter;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000066B0 File Offset: 0x000048B0
		internal static void UpdatePerfCounter(long totalRequestTime)
		{
			if (PswsPerfCounter.perfCounter == null)
			{
				PswsPerfCounter.InitializePerfCounter();
			}
			long averageResponseTime = PswsPerfCounter.averageRT.GetAverageResponseTime(totalRequestTime, PswsPerfCounter.perfCounter.AverageResponseTime.RawValue);
			if (averageResponseTime != 0L)
			{
				PswsPerfCounter.perfCounter.AverageResponseTime.RawValue = averageResponseTime;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000066F9 File Offset: 0x000048F9
		private static void InitializePerfCounter()
		{
			PswsPerfCounter.perfCounter = PswshttpRequestPerformanceCounters.GetInstance("Psws");
			PswsPerfCounter.averageRT = new AverageResponseTimeCounter();
		}

		// Token: 0x0400008A RID: 138
		private const string PowerShellWebServicePerfCounterInstanceName = "Psws";

		// Token: 0x0400008B RID: 139
		private static PswshttpRequestPerformanceCountersInstance perfCounter;

		// Token: 0x0400008C RID: 140
		private static AverageResponseTimeCounter averageRT;
	}
}
