using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x0200001A RID: 26
	internal class PercentilePerfCounters
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004E10 File Offset: 0x00003010
		internal static void UpdateRoutingLatencyPerfCounter(string siteName, double value)
		{
			if (PerfCounters.HttpProxyCountersInstance.TotalProxyRequests.RawValue < 5L)
			{
				return;
			}
			value = ((value >= 0.0) ? value : 0.0);
			IPercentileCounter percentileCounter = PercentilePerfCounters.GetPercentileCounter(siteName);
			percentileCounter.AddValue((long)Convert.ToInt32(value));
			HttpProxyPerSiteCountersInstance httpProxyPerSiteCountersInstance = PerfCounters.GetHttpProxyPerSiteCountersInstance(siteName);
			PercentilePerfCounters.UpdateCounterInstance(httpProxyPerSiteCountersInstance.RoutingLatency90thPercentile, percentileCounter, 90U);
			PercentilePerfCounters.UpdateCounterInstance(httpProxyPerSiteCountersInstance.RoutingLatency95thPercentile, percentileCounter, 95U);
			PercentilePerfCounters.UpdateCounterInstance(httpProxyPerSiteCountersInstance.RoutingLatency99thPercentile, percentileCounter, 99U);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004E90 File Offset: 0x00003090
		private static void UpdateCounterInstance(ExPerformanceCounter perfCounter, IPercentileCounter percentileCounter, uint percentile)
		{
			perfCounter.RawValue = percentileCounter.PercentileQuery(percentile);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004EA4 File Offset: 0x000030A4
		private static IPercentileCounter GetPercentileCounter(string siteName)
		{
			if (!PercentilePerfCounters.siteToLatencyPercentileCounters.ContainsKey(siteName))
			{
				lock (PercentilePerfCounters.siteToLatencyPercentileCounters)
				{
					if (!PercentilePerfCounters.siteToLatencyPercentileCounters.ContainsKey(siteName))
					{
						PercentilePerfCounters.siteToLatencyPercentileCounters.Add(siteName, new PercentileCounter(PercentilePerfCounters.ExpiryInternal, PercentilePerfCounters.GranularityInterval, (long)PercentilePerfCounters.ValueGranularity, (long)PercentilePerfCounters.UpperLimit));
					}
				}
			}
			return PercentilePerfCounters.siteToLatencyPercentileCounters[siteName];
		}

		// Token: 0x040000C1 RID: 193
		private static readonly TimeSpan ExpiryInternal = TimeSpan.FromMinutes(30.0);

		// Token: 0x040000C2 RID: 194
		private static readonly TimeSpan GranularityInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x040000C3 RID: 195
		private static readonly int ValueGranularity = 10;

		// Token: 0x040000C4 RID: 196
		private static readonly int UpperLimit = 30000;

		// Token: 0x040000C5 RID: 197
		private static Dictionary<string, IPercentileCounter> siteToLatencyPercentileCounters = new Dictionary<string, IPercentileCounter>();
	}
}
