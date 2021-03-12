using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000128 RID: 296
	internal static class PerfCounterHelper
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x00024A9C File Offset: 0x00022C9C
		public static void UpdateMailTipsConfigurationResponseTimePerformanceCounter(long newValue)
		{
			PerfCounterHelper.UpdateMovingAveragePerformanceCounter(MailTipsPerfCounters.MailTipsConfigurationAverageResponseTime, newValue, ref PerfCounterHelper.mailTipsConfigurationResponseTimeAverage, PerfCounterHelper.mailTipsConfigurationResponseTimeAverageLock);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00024AB3 File Offset: 0x00022CB3
		public static void UpdateServiceConfigurationResponseTimePerformanceCounter(long newValue)
		{
			PerfCounterHelper.UpdateMovingAveragePerformanceCounter(MailTipsPerfCounters.ServiceConfigurationAverageResponseTime, newValue, ref PerfCounterHelper.serviceConfigurationResponseTimeAverage, PerfCounterHelper.serviceConfigurationResponseTimeAverageLock);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00024ACA File Offset: 0x00022CCA
		public static void UpdateMailTipsResponseTimePerformanceCounter(long newValue)
		{
			PerfCounterHelper.UpdateMovingAveragePerformanceCounter(MailTipsPerfCounters.MailTipsAverageResponseTime, newValue, ref PerfCounterHelper.mailTipsResponseTimeAverage, PerfCounterHelper.mailTipsResponseTimeAverageLock);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00024AE1 File Offset: 0x00022CE1
		public static void UpdateMailTipsRecipientNumberPerformanceCounter(long newValue)
		{
			PerfCounterHelper.UpdateMovingAveragePerformanceCounter(MailTipsPerfCounters.MailTipsAverageRecipientNumber, newValue, ref PerfCounterHelper.mailTipsRecipientNumberAverage, PerfCounterHelper.mailTipsRecipientNumberAverageLock);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00024AF8 File Offset: 0x00022CF8
		public static void UpdateMailTipsAverageActiveDirectoryResponseTimePerformanceCounter(long newValue)
		{
			PerfCounterHelper.UpdateMovingAveragePerformanceCounter(MailTipsPerfCounters.MailTipsAverageActiveDirectoryResponseTime, newValue, ref PerfCounterHelper.mailTipsAverageActiveDirectoryResponseTime, PerfCounterHelper.mailTipsAverageActiveDirectoryResponseTimeLock);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00024B10 File Offset: 0x00022D10
		private static void UpdateMovingAveragePerformanceCounter(ExPerformanceCounter performanceCounter, long newValue, ref double averageValue, object lockObject)
		{
			lock (lockObject)
			{
				averageValue = (1.0 - PerfCounterHelper.averageMultiplier) * averageValue + PerfCounterHelper.averageMultiplier * (double)newValue;
				performanceCounter.RawValue = (long)averageValue;
			}
		}

		// Token: 0x040004DB RID: 1243
		private static double averageMultiplier = 0.04;

		// Token: 0x040004DC RID: 1244
		private static object mailTipsConfigurationResponseTimeAverageLock = new object();

		// Token: 0x040004DD RID: 1245
		private static double mailTipsConfigurationResponseTimeAverage;

		// Token: 0x040004DE RID: 1246
		private static object serviceConfigurationResponseTimeAverageLock = new object();

		// Token: 0x040004DF RID: 1247
		private static double serviceConfigurationResponseTimeAverage;

		// Token: 0x040004E0 RID: 1248
		private static object mailTipsResponseTimeAverageLock = new object();

		// Token: 0x040004E1 RID: 1249
		private static double mailTipsResponseTimeAverage;

		// Token: 0x040004E2 RID: 1250
		private static object mailTipsRecipientNumberAverageLock = new object();

		// Token: 0x040004E3 RID: 1251
		private static double mailTipsRecipientNumberAverage;

		// Token: 0x040004E4 RID: 1252
		private static object mailTipsAverageActiveDirectoryResponseTimeLock = new object();

		// Token: 0x040004E5 RID: 1253
		private static double mailTipsAverageActiveDirectoryResponseTime;
	}
}
