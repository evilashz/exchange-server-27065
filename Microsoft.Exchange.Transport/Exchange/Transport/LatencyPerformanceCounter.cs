using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000178 RID: 376
	internal class LatencyPerformanceCounter : ILatencyPerformanceCounter
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00042920 File Offset: 0x00040B20
		public LatencyPerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00042928 File Offset: 0x00040B28
		private LatencyPerformanceCounter(string instanceName, TimeSpan expiryInterval, long infinitySeconds, bool isLongRange, LatencyPerformanceCounterType type)
		{
			this.counterType = type;
			this.isLongRange = isLongRange;
			this.expiryInterval = expiryInterval;
			this.infinitySeconds = infinitySeconds;
			if (type == LatencyPerformanceCounterType.Local)
			{
				this.perfCounters = LatencyTrackerPerfCounters.GetInstance(instanceName);
			}
			else
			{
				this.endToEndPerfCounters = LatencyTrackerEndToEndPerfCounters.GetInstance(instanceName);
			}
			if (isLongRange)
			{
				this.percentileCounter = new MultiGranularityPercentileCounter(expiryInterval, TimeSpan.FromSeconds(5.0), LatencyPerformanceCounter.parameters);
				return;
			}
			this.percentileCounter = new PercentileCounter(expiryInterval, TimeSpan.FromSeconds(5.0), 1L, infinitySeconds);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000429B8 File Offset: 0x00040BB8
		public static ILatencyPerformanceCounter CreateInstance(string instanceName, TimeSpan expiryInterval, long infinitySeconds)
		{
			return LatencyPerformanceCounter.CreateInstance(instanceName, expiryInterval, infinitySeconds, false);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x000429C3 File Offset: 0x00040BC3
		public static ILatencyPerformanceCounter CreateInstance(string instanceName, TimeSpan expiryInterval, long infinitySeconds, bool isLongRange)
		{
			return LatencyPerformanceCounter.CreateInstance(instanceName, expiryInterval, infinitySeconds, isLongRange, LatencyPerformanceCounterType.Local);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000429D0 File Offset: 0x00040BD0
		public static ILatencyPerformanceCounter CreateInstance(string instanceName, TimeSpan expiryInterval, long infinitySeconds, bool isLongRange, LatencyPerformanceCounterType type)
		{
			ILatencyPerformanceCounter result;
			try
			{
				result = new LatencyPerformanceCounter(instanceName, expiryInterval, infinitySeconds, isLongRange, type);
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.GeneralTracer.TraceError<string, InvalidOperationException>(0L, "Failed to initialize performance counters for component '{0}': {1}", instanceName, ex);
				ExEventLog exEventLog = new ExEventLog(ExTraceGlobals.GeneralTracer.Category, TransportEventLog.GetEventSource());
				exEventLog.LogEvent(TransportEventLogConstants.Tuple_PerfCountersLoadFailure, null, new object[]
				{
					LatencyPerformanceCounter.EventTag,
					instanceName,
					ex.ToString()
				});
				result = null;
			}
			return result;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00042A54 File Offset: 0x00040C54
		public static void SetCategoryNames(string localCategoryName, string endToEndCategoryName)
		{
			LatencyTrackerPerfCounters.SetCategoryName(localCategoryName);
			LatencyTrackerEndToEndPerfCounters.SetCategoryName(endToEndCategoryName);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00042A62 File Offset: 0x00040C62
		public void AddValue(long latencySeconds)
		{
			if (latencySeconds >= 0L)
			{
				this.percentileCounter.AddValue(latencySeconds);
				this.Update();
			}
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00042A7B File Offset: 0x00040C7B
		public void AddValue(long latencySeconds, DeliveryPriority priority)
		{
			this.AddValue(latencySeconds);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00042A84 File Offset: 0x00040C84
		public void Update()
		{
			long rawValue;
			if (this.perfCounters != null)
			{
				this.perfCounters.Percentile99.RawValue = this.percentileCounter.PercentileQuery(99.0, out rawValue);
				this.perfCounters.Percentile99Samples.RawValue = rawValue;
				this.perfCounters.Percentile95.RawValue = this.percentileCounter.PercentileQuery(95.0, out rawValue);
				this.perfCounters.Percentile95Samples.RawValue = rawValue;
				this.perfCounters.Percentile90.RawValue = this.percentileCounter.PercentileQuery(90.0, out rawValue);
				this.perfCounters.Percentile90Samples.RawValue = rawValue;
				this.perfCounters.Percentile80.RawValue = this.percentileCounter.PercentileQuery(80.0, out rawValue);
				this.perfCounters.Percentile80Samples.RawValue = rawValue;
				this.perfCounters.Percentile50.RawValue = this.percentileCounter.PercentileQuery(50.0, out rawValue);
				this.perfCounters.Percentile50Samples.RawValue = rawValue;
				return;
			}
			this.endToEndPerfCounters.Percentile99.RawValue = this.percentileCounter.PercentileQuery(99.0, out rawValue);
			this.endToEndPerfCounters.Percentile99Samples.RawValue = rawValue;
			this.endToEndPerfCounters.Percentile95.RawValue = this.percentileCounter.PercentileQuery(95.0, out rawValue);
			this.endToEndPerfCounters.Percentile95Samples.RawValue = rawValue;
			this.endToEndPerfCounters.Percentile90.RawValue = this.percentileCounter.PercentileQuery(90.0, out rawValue);
			this.endToEndPerfCounters.Percentile90Samples.RawValue = rawValue;
			this.endToEndPerfCounters.Percentile80.RawValue = this.percentileCounter.PercentileQuery(80.0, out rawValue);
			this.endToEndPerfCounters.Percentile80Samples.RawValue = rawValue;
			this.endToEndPerfCounters.Percentile50.RawValue = this.percentileCounter.PercentileQuery(50.0, out rawValue);
			this.endToEndPerfCounters.Percentile50Samples.RawValue = rawValue;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00042CC4 File Offset: 0x00040EC4
		public void Reset()
		{
			if (this.isLongRange)
			{
				this.percentileCounter = new MultiGranularityPercentileCounter(this.expiryInterval, TimeSpan.FromSeconds(5.0), LatencyPerformanceCounter.parameters);
			}
			else
			{
				this.percentileCounter = new PercentileCounter(this.expiryInterval, TimeSpan.FromSeconds(5.0), 1L, this.infinitySeconds);
			}
			this.Update();
		}

		// Token: 0x04000851 RID: 2129
		private static readonly string EventTag = "Latency Tracker";

		// Token: 0x04000852 RID: 2130
		private static readonly MultiGranularityPercentileCounter.Param[] parameters = new MultiGranularityPercentileCounter.Param[]
		{
			new MultiGranularityPercentileCounter.Param(1L, 90L),
			new MultiGranularityPercentileCounter.Param(5L, 300L),
			new MultiGranularityPercentileCounter.Param(30L, 1800L),
			new MultiGranularityPercentileCounter.Param(300L, 18000L),
			new MultiGranularityPercentileCounter.Param(1800L, 43200L)
		};

		// Token: 0x04000853 RID: 2131
		private LatencyTrackerPerfCountersInstance perfCounters;

		// Token: 0x04000854 RID: 2132
		private LatencyTrackerEndToEndPerfCountersInstance endToEndPerfCounters;

		// Token: 0x04000855 RID: 2133
		private IPercentileCounter percentileCounter;

		// Token: 0x04000856 RID: 2134
		private LatencyPerformanceCounterType counterType;

		// Token: 0x04000857 RID: 2135
		private readonly bool isLongRange;

		// Token: 0x04000858 RID: 2136
		private readonly TimeSpan expiryInterval;

		// Token: 0x04000859 RID: 2137
		private readonly long infinitySeconds;
	}
}
