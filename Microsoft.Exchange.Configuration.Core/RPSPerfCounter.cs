using System;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x0200001C RID: 28
	public static class RPSPerfCounter
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x000051EC File Offset: 0x000033EC
		public static void UpdateAverageRTCounter(long requestTime)
		{
			if (RPSPerfCounter.currentAvgRT == null)
			{
				RPSPerfCounter.currentAvgRT = new AverageResponseTimeCounter();
			}
			long averageResponseTime = RPSPerfCounter.currentAvgRT.GetAverageResponseTime(requestTime, RPSPerfCounterHelper.CurrentPerfCounter.AverageResponseTime.RawValue);
			if (averageResponseTime != 0L)
			{
				RPSPerfCounterHelper.CurrentPerfCounter.AverageResponseTime.RawValue = averageResponseTime;
			}
		}

		// Token: 0x0400007D RID: 125
		private static AverageResponseTimeCounter currentAvgRT;
	}
}
