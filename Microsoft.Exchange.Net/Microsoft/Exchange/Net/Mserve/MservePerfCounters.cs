using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Mserve.Perf;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x02000895 RID: 2197
	internal class MservePerfCounters
	{
		// Token: 0x06002F05 RID: 12037 RVA: 0x00069788 File Offset: 0x00067988
		static MservePerfCounters()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				MservePerfCounters.processProcessName = currentProcess.ProcessName;
			}
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x0006984C File Offset: 0x00067A4C
		internal void UpdateRequestPerfCountersForMserveWebService(int readCount, int addCount, int delCount)
		{
			this.counters.ReadRequestsInMserveWebService.IncrementBy((long)readCount);
			this.counters.AddRequestsInMserveWebService.IncrementBy((long)addCount);
			this.counters.DeleteRequestsInMserveWebService.IncrementBy((long)delCount);
			int num = readCount + addCount + delCount;
			this.counters.TotalRequestsInMserveWebService.IncrementBy((long)num);
			MservePerfCounters.percentageCounterFailuresInMserveWebService.AddDenominator((long)num);
			this.counters.PercentageFailuresInMserveWebService.RawValue = (long)MservePerfCounters.percentageCounterFailuresInMserveWebService.GetSlidingPercentage();
			MservePerfCounters.percentageCounterRequestsInMserveWebService.AddNumerator((long)num);
			MservePerfCounters.percentageCounterRequestsInMserveWebService.AddDenominator((long)num);
			MservePerfCounters.percentageCounterRequestsInCacheService.AddDenominator((long)num);
			this.counters.PercentageRequestsInMserveWebService.RawValue = (long)MservePerfCounters.percentageCounterRequestsInMserveWebService.GetSlidingPercentage();
			this.counters.PercentageRequestsInMserveCacheService.RawValue = (long)MservePerfCounters.percentageCounterRequestsInCacheService.GetSlidingPercentage();
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x00069930 File Offset: 0x00067B30
		internal void UpdateFailurePerfCountersForMserveWebService(int failureCount)
		{
			this.counters.TotalFailuresInMserveWebService.IncrementBy((long)failureCount);
			MservePerfCounters.percentageCounterFailuresInMserveWebService.AddNumerator((long)failureCount);
			this.counters.PercentageFailuresInMserveWebService.RawValue = (long)MservePerfCounters.percentageCounterFailuresInMserveWebService.GetSlidingPercentage();
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x00069970 File Offset: 0x00067B70
		internal void UpdateRequestPerfCountersForMserveCacheService(int readCount, int addCount, int delCount)
		{
			this.counters.ReadRequestsInMserveCacheService.IncrementBy((long)readCount);
			int num = readCount + addCount + delCount;
			this.counters.TotalRequestsInMserveCacheService.IncrementBy((long)num);
			MservePerfCounters.percentageCounterFailuresInCacheService.AddDenominator((long)num);
			this.counters.PercentageFailuresInMserveCacheService.RawValue = (long)MservePerfCounters.percentageCounterFailuresInCacheService.GetSlidingPercentage();
			MservePerfCounters.percentageCounterRequestsInCacheService.AddNumerator((long)num);
			MservePerfCounters.percentageCounterRequestsInCacheService.AddDenominator((long)num);
			MservePerfCounters.percentageCounterRequestsInMserveWebService.AddDenominator((long)num);
			this.counters.PercentageRequestsInMserveCacheService.RawValue = (long)MservePerfCounters.percentageCounterRequestsInCacheService.GetSlidingPercentage();
			this.counters.PercentageRequestsInMserveWebService.RawValue = (long)MservePerfCounters.percentageCounterRequestsInMserveWebService.GetSlidingPercentage();
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x00069A2E File Offset: 0x00067C2E
		internal void UpdateFailurePerfCountersForMservCacheService(int failureCount)
		{
			this.counters.TotalFailuresInMserveCacheService.IncrementBy((long)failureCount);
			MservePerfCounters.percentageCounterFailuresInCacheService.AddNumerator((long)failureCount);
			this.counters.PercentageFailuresInMserveCacheService.RawValue = (long)MservePerfCounters.percentageCounterFailuresInCacheService.GetSlidingPercentage();
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x00069A6B File Offset: 0x00067C6B
		internal void UpdateTotalFailuresPerfCounters(int failureCount)
		{
			this.counters.TotalFailuresInMserveService.IncrementBy((long)failureCount);
			MservePerfCounters.percentageCounterTotalFailuresInMserveService.AddNumerator((long)failureCount);
			this.counters.PercentageTotalFailuresInMserveService.RawValue = (long)MservePerfCounters.percentageCounterTotalFailuresInMserveService.GetSlidingPercentage();
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x00069AA8 File Offset: 0x00067CA8
		internal void UpdateTotalRequestPerfCounters(int totalCount)
		{
			this.counters.TotalRequestsInMserveService.IncrementBy((long)totalCount);
			MservePerfCounters.percentageCounterTotalFailuresInMserveService.AddDenominator((long)totalCount);
			this.counters.PercentageTotalFailuresInMserveService.RawValue = (long)MservePerfCounters.percentageCounterTotalFailuresInMserveService.GetSlidingPercentage();
		}

		// Token: 0x040028E2 RID: 10466
		private static readonly string processProcessName;

		// Token: 0x040028E3 RID: 10467
		private readonly MserveWebServiceCountersInstance counters = MserveWebServiceCounters.GetInstance(MservePerfCounters.processProcessName);

		// Token: 0x040028E4 RID: 10468
		private static readonly TimeSpan SlidingCounterInterval = TimeSpan.FromMinutes(60.0);

		// Token: 0x040028E5 RID: 10469
		private static readonly TimeSpan SlidingCounterPrecision = TimeSpan.FromSeconds(1.0);

		// Token: 0x040028E6 RID: 10470
		private static SlidingPercentageCounter percentageCounterFailuresInCacheService = new SlidingPercentageCounter(MservePerfCounters.SlidingCounterInterval, MservePerfCounters.SlidingCounterPrecision);

		// Token: 0x040028E7 RID: 10471
		private static SlidingPercentageCounter percentageCounterRequestsInCacheService = new SlidingPercentageCounter(MservePerfCounters.SlidingCounterInterval, MservePerfCounters.SlidingCounterPrecision);

		// Token: 0x040028E8 RID: 10472
		private static SlidingPercentageCounter percentageCounterFailuresInMserveWebService = new SlidingPercentageCounter(MservePerfCounters.SlidingCounterInterval, MservePerfCounters.SlidingCounterPrecision);

		// Token: 0x040028E9 RID: 10473
		private static SlidingPercentageCounter percentageCounterRequestsInMserveWebService = new SlidingPercentageCounter(MservePerfCounters.SlidingCounterInterval, MservePerfCounters.SlidingCounterPrecision);

		// Token: 0x040028EA RID: 10474
		private static SlidingPercentageCounter percentageCounterTotalFailuresInMserveService = new SlidingPercentageCounter(MservePerfCounters.SlidingCounterInterval, MservePerfCounters.SlidingCounterPrecision);
	}
}
