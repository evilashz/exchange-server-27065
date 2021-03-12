using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200023F RID: 575
	internal static class PolicyTipPerfMon
	{
		// Token: 0x060015AB RID: 5547 RVA: 0x0004D3F7 File Offset: 0x0004B5F7
		static PolicyTipPerfMon()
		{
			PolicyTipPerfMon.InitializePerfMon();
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0004D400 File Offset: 0x0004B600
		private static void InitializePerfMon()
		{
			PolicyTipPerfMon.percentServerFailures = new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0), true);
			PolicyTipPerfMon.percentHighLatency = new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0), true);
			PolicyTipPerfMon.averageLatency = new SlidingAverageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0));
			if (PolicyTipPerfMon.performanceCounterMaintenanceTimer == null)
			{
				PolicyTipPerfMon.performanceCounterMaintenanceTimer = new GuardedTimer(new TimerCallback(PolicyTipPerfMon.RefreshPerformanceCounters), null, TimeSpan.FromSeconds(60.0));
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0004D4AD File Offset: 0x0004B6AD
		internal static void IncrementTotalRequests()
		{
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsTotalRequests.Increment();
			PolicyTipPerfMon.percentServerFailures.AddDenominator(1L);
			PolicyTipPerfMon.percentHighLatency.AddDenominator(1L);
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0004D4D4 File Offset: 0x0004B6D4
		internal static void IncrementAllServerFailures()
		{
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsAllServerFailedRequests.Increment();
			PolicyTipPerfMon.percentServerFailures.AddNumerator(1L);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0004D4EE File Offset: 0x0004B6EE
		internal static void IncrementPercentHighLatency()
		{
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsHighLatencyRequests.Increment();
			PolicyTipPerfMon.percentHighLatency.AddNumerator(1L);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0004D508 File Offset: 0x0004B708
		public static void RecordPerRequestLatency(TimeSpan timeSpan)
		{
			PolicyTipPerfMon.averageLatency.AddValue((long)timeSpan.TotalMilliseconds);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0004D51C File Offset: 0x0004B71C
		internal static void RefreshPerformanceCounters(object state)
		{
			PolicyTipPerfMon.ComputePercentServerFailures();
			PolicyTipPerfMon.ComputePercentHighLatency();
			PolicyTipPerfMon.ComputeAverageLatency();
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0004D530 File Offset: 0x0004B730
		public static void ComputeAverageLatency()
		{
			long num;
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsAverageRequestLatency.RawValue = PolicyTipPerfMon.averageLatency.CalculateAverageAcrossAllSamples(out num);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0004D553 File Offset: 0x0004B753
		private static void ComputePercentServerFailures()
		{
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsPercentServerFailures.RawValue = (long)((int)PolicyTipPerfMon.percentServerFailures.GetSlidingPercentage());
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0004D56B File Offset: 0x0004B76B
		private static void ComputePercentHighLatency()
		{
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsPercentHighLatency.RawValue = (long)((int)PolicyTipPerfMon.percentHighLatency.GetSlidingPercentage());
		}

		// Token: 0x04000BFB RID: 3067
		private static GuardedTimer performanceCounterMaintenanceTimer;

		// Token: 0x04000BFC RID: 3068
		private static SlidingPercentageCounter percentServerFailures;

		// Token: 0x04000BFD RID: 3069
		private static SlidingPercentageCounter percentHighLatency;

		// Token: 0x04000BFE RID: 3070
		private static SlidingAverageCounter averageLatency;
	}
}
