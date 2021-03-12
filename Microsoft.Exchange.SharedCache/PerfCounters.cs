using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x02000016 RID: 22
	internal class PerfCounters : IDisposable
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003B40 File Offset: 0x00001D40
		public PerfCounters(CacheSettings cacheSettings)
		{
			ArgumentValidator.ThrowIfNull("cacheSettings", cacheSettings);
			this.countersInstance = SharedCachePerfCounters.GetInstance(cacheSettings.Name);
			this.countersInstance.TotalRequests.RawValue = 0L;
			this.countersInstance.CacheSize.RawValue = 0L;
			this.countersInstance.CacheHitRate.RawValue = 0L;
			this.countersInstance.AverageLatency.RawValue = 0L;
			this.countersInstance.FailedRequests.RawValue = 0L;
			this.countersInstance.CorruptEntries.RawValue = 0L;
			this.cacheHitRate = new SlidingPercentageCounter(cacheSettings.PerfCounterHitRateSlidingWindow, cacheSettings.PerfCounterHitRateGranularity);
			this.averageLatency = new RunningAverageFloat((ushort)cacheSettings.PerfCounterAverageLatencySampleCount);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003C04 File Offset: 0x00001E04
		public long AverageLatencyValue
		{
			get
			{
				return this.countersInstance.AverageLatency.RawValue;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003C16 File Offset: 0x00001E16
		public long DiskSpace
		{
			get
			{
				return this.countersInstance.DiskSpace.RawValue;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003C28 File Offset: 0x00001E28
		public void IncrementTotalRequests()
		{
			this.countersInstance.TotalRequests.Increment();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003C3B File Offset: 0x00001E3B
		public void IncrementFailedRequests()
		{
			this.countersInstance.FailedRequests.Increment();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003C4E File Offset: 0x00001E4E
		public void IncrementCorruptEntries()
		{
			this.countersInstance.FailedRequests.Increment();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003C61 File Offset: 0x00001E61
		public void UpdateCacheSize(long newSize)
		{
			this.countersInstance.CacheSize.RawValue = newSize;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003C74 File Offset: 0x00001E74
		public void UpdateCacheHitRate(bool wasCacheHit)
		{
			double value = this.cacheHitRate.Add(wasCacheHit ? 1L : 0L, 1L);
			this.countersInstance.CacheHitRate.RawValue = Convert.ToInt64(value);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public void UpdateAverageLatency(long latency)
		{
			long rawValue = (long)this.averageLatency.Update((float)latency);
			this.countersInstance.AverageLatency.RawValue = rawValue;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003CDD File Offset: 0x00001EDD
		public void UpdateDiskSpace(long newSize)
		{
			this.countersInstance.DiskSpace.RawValue = newSize;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public void Dispose()
		{
			this.cacheHitRate = null;
			this.countersInstance = null;
			this.averageLatency = null;
		}

		// Token: 0x04000049 RID: 73
		private SharedCachePerfCountersInstance countersInstance;

		// Token: 0x0400004A RID: 74
		private SlidingPercentageCounter cacheHitRate;

		// Token: 0x0400004B RID: 75
		private RunningAverageFloat averageLatency;
	}
}
