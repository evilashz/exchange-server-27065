using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000190 RID: 400
	internal static class RatePerfCounters
	{
		// Token: 0x06000F51 RID: 3921 RVA: 0x0003E7DC File Offset: 0x0003C9DC
		public static void UpdatePerfCounterValues(object status)
		{
			lock (RatePerfCounters.lockObject)
			{
				TimeSpan threshold = new TimeSpan(3000L);
				if (ExDateTime.Compare(RatePerfCounters.lockObject.LastUpdateTime, ExDateTime.Now, threshold) != 0)
				{
					if (RatePerfCounters.exceptionPerfSamples != null)
					{
						for (int i = 0; i < RatePerfCounters.exceptionPerfSamples.Length; i++)
						{
							lock (RatePerfCounters.exceptionPerfSamples[i])
							{
								ExDateTime now = ExDateTime.Now;
								int j = RatePerfCounters.exceptionPerfSamples[i].Count;
								foreach (ExDateTime dt in RatePerfCounters.exceptionPerfSamples[i])
								{
									if (ExDateTime.Compare(dt, now, RatePerfCounters.ExceptionComputationInterval) == 0)
									{
										break;
									}
									j--;
								}
								RatePerfCounters.exceptionPerfCounters[i].RawValue = (long)j;
								while (j < RatePerfCounters.exceptionPerfSamples[i].Count)
								{
									RatePerfCounters.exceptionPerfSamples[i].Dequeue();
								}
							}
						}
					}
					if (RatePerfCounters.latencyPerfSamples != null)
					{
						for (int k = 0; k < RatePerfCounters.latencyPerfSamples.Length; k++)
						{
							lock (RatePerfCounters.latencyPerfSamples[k])
							{
								int l = 0;
								long num = 0L;
								ExDateTime now2 = ExDateTime.Now;
								foreach (RatePerfCounters.LatencySample latencySample in RatePerfCounters.latencyPerfSamples[k])
								{
									if (ExDateTime.Compare(latencySample.Timestamp, now2, RatePerfCounters.LatencyComputationInterval) == 0)
									{
										num += latencySample.Latency;
										l++;
									}
								}
								if (l > 0)
								{
									RatePerfCounters.latencyPerfCounters[k].RawValue = num / (long)l;
								}
								while (l < RatePerfCounters.latencyPerfSamples[k].Count)
								{
									RatePerfCounters.latencyPerfSamples[k].Dequeue();
								}
							}
						}
					}
					RatePerfCounters.lockObject.LastUpdateTime = ExDateTime.Now;
				}
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003EA70 File Offset: 0x0003CC70
		internal static bool Initialize(ExPerformanceCounter[] exceptionPerfCounters, ExPerformanceCounter[] latencyPerfCounters)
		{
			if (!RatePerfCounters.initialized)
			{
				if (exceptionPerfCounters != null)
				{
					RatePerfCounters.exceptionPerfCounters = exceptionPerfCounters;
					RatePerfCounters.exceptionPerfSamples = new Queue<ExDateTime>[exceptionPerfCounters.Length];
					for (int i = 0; i < exceptionPerfCounters.Length; i++)
					{
						RatePerfCounters.exceptionPerfSamples[i] = new Queue<ExDateTime>(100);
					}
				}
				if (latencyPerfCounters != null)
				{
					RatePerfCounters.latencyPerfCounters = latencyPerfCounters;
					RatePerfCounters.latencyPerfSamples = new Queue<RatePerfCounters.LatencySample>[latencyPerfCounters.Length];
					for (int j = 0; j < RatePerfCounters.latencyPerfSamples.Length; j++)
					{
						RatePerfCounters.latencyPerfSamples[j] = new Queue<RatePerfCounters.LatencySample>(6000);
					}
				}
				try
				{
					RatePerfCounters.timer = new Timer(new TimerCallback(RatePerfCounters.UpdatePerfCounterValues), null, 10000, 3000);
				}
				catch (Exception ex)
				{
					if (ex is ArgumentOutOfRangeException || ex is NotSupportedException)
					{
						return false;
					}
					throw;
				}
				RatePerfCounters.initialized = (RatePerfCounters.timer != null);
			}
			return RatePerfCounters.initialized;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0003EB54 File Offset: 0x0003CD54
		internal static void IncrementExceptionPerfCounter(int perfCounterIndex)
		{
			if (perfCounterIndex < 0 || perfCounterIndex >= RatePerfCounters.exceptionPerfSamples.Length)
			{
				return;
			}
			lock (RatePerfCounters.exceptionPerfSamples[perfCounterIndex])
			{
				ExDateTime now = ExDateTime.Now;
				if (RatePerfCounters.exceptionPerfSamples[perfCounterIndex].Count == 100)
				{
					RatePerfCounters.exceptionPerfSamples[perfCounterIndex].Dequeue();
				}
				RatePerfCounters.exceptionPerfSamples[perfCounterIndex].Enqueue(now);
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0003EBD0 File Offset: 0x0003CDD0
		internal static void IncrementLatencyPerfCounter(int latencyCounterIndex, long latency)
		{
			if (latencyCounterIndex < 0 || latencyCounterIndex >= RatePerfCounters.latencyPerfSamples.Length)
			{
				return;
			}
			lock (RatePerfCounters.latencyPerfSamples[latencyCounterIndex])
			{
				RatePerfCounters.LatencySample item = new RatePerfCounters.LatencySample(latency);
				if (RatePerfCounters.latencyPerfSamples[latencyCounterIndex].Count == 6000)
				{
					RatePerfCounters.latencyPerfSamples[latencyCounterIndex].Dequeue();
				}
				RatePerfCounters.latencyPerfSamples[latencyCounterIndex].Enqueue(item);
			}
		}

		// Token: 0x04000827 RID: 2087
		private const int TimerUpdateInterval = 3000;

		// Token: 0x04000828 RID: 2088
		private const int TimerStartDelay = 10000;

		// Token: 0x04000829 RID: 2089
		private const int MaxExceptionQueueSize = 100;

		// Token: 0x0400082A RID: 2090
		private const int MaxLatencyQueueSize = 6000;

		// Token: 0x0400082B RID: 2091
		private static readonly TimeSpan ExceptionComputationInterval = new TimeSpan(0, 1, 0);

		// Token: 0x0400082C RID: 2092
		private static readonly TimeSpan LatencyComputationInterval = new TimeSpan(0, 5, 0);

		// Token: 0x0400082D RID: 2093
		private static ExPerformanceCounter[] exceptionPerfCounters;

		// Token: 0x0400082E RID: 2094
		private static Queue<ExDateTime>[] exceptionPerfSamples;

		// Token: 0x0400082F RID: 2095
		private static ExPerformanceCounter[] latencyPerfCounters;

		// Token: 0x04000830 RID: 2096
		private static Queue<RatePerfCounters.LatencySample>[] latencyPerfSamples;

		// Token: 0x04000831 RID: 2097
		private static Timer timer;

		// Token: 0x04000832 RID: 2098
		private static bool initialized = false;

		// Token: 0x04000833 RID: 2099
		private static RatePerfCounters.LockObject lockObject = new RatePerfCounters.LockObject();

		// Token: 0x02000191 RID: 401
		private struct LatencySample
		{
			// Token: 0x06000F56 RID: 3926 RVA: 0x0003EC7C File Offset: 0x0003CE7C
			public LatencySample(long latency)
			{
				this.Latency = latency;
				this.Timestamp = ExDateTime.Now;
			}

			// Token: 0x04000834 RID: 2100
			public ExDateTime Timestamp;

			// Token: 0x04000835 RID: 2101
			public long Latency;
		}

		// Token: 0x02000192 RID: 402
		private class LockObject
		{
			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003EC90 File Offset: 0x0003CE90
			// (set) Token: 0x06000F58 RID: 3928 RVA: 0x0003EC98 File Offset: 0x0003CE98
			public ExDateTime LastUpdateTime { get; set; }
		}
	}
}
