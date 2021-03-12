using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000136 RID: 310
	[Serializable]
	public sealed class MultiGranularityPercentileCounter : IPercentileCounter
	{
		// Token: 0x060008C9 RID: 2249 RVA: 0x00022578 File Offset: 0x00020778
		public MultiGranularityPercentileCounter(TimeSpan expiryInterval, TimeSpan granularityInterval, MultiGranularityPercentileCounter.Param[] parameters, CurrentTimeProvider currentTimeProvider)
		{
			this.count = parameters.Length;
			ExAssert.RetailAssert(this.count > 0, "Number of parameters: {0} must be greater than zero.", new object[]
			{
				this.count
			});
			for (int i = 0; i < this.count - 1; i++)
			{
				ExAssert.RetailAssert(parameters[i].Granularity < parameters[i + 1].Granularity, "The granularities must be sorted.");
				ExAssert.RetailAssert(parameters[i].Range < parameters[i + 1].Range, "The ranges  must be sorted.");
				ExAssert.RetailAssert(parameters[i].Range % parameters[i + 1].Granularity == 0L, "The range[i] MOD granularity[i + 1] must be zero.");
			}
			this.percentileCounters = new PercentileCounter[this.count];
			this.borderBuckets = new long[this.count];
			for (int j = 0; j < this.count; j++)
			{
				this.percentileCounters[j] = new PercentileCounter(expiryInterval, granularityInterval, parameters[j].Granularity, parameters[j].Range, currentTimeProvider);
				this.borderBuckets[j] = parameters[j].Range - parameters[j].Granularity;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000226C5 File Offset: 0x000208C5
		public MultiGranularityPercentileCounter(TimeSpan expiryInterval, TimeSpan granularityInterval, MultiGranularityPercentileCounter.Param[] parameters) : this(expiryInterval, granularityInterval, parameters, null)
		{
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000226D4 File Offset: 0x000208D4
		public void AddValue(long value)
		{
			foreach (PercentileCounter percentileCounter in this.percentileCounters)
			{
				percentileCounter.AddValue(value);
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00022704 File Offset: 0x00020904
		public long PercentileQuery(double percentage)
		{
			long num;
			return this.PercentileQuery(percentage, out num);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002271C File Offset: 0x0002091C
		public long PercentileQuery(double percentage, out long samples)
		{
			long num = 0L;
			samples = 0L;
			int i = 0;
			while (i < this.count)
			{
				if (percentage <= 100.0 - this.percentileCounters[i].InfiniteBucketPercentage || i == this.count - 1)
				{
					num = this.percentileCounters[i].PercentileQuery(percentage, out samples);
					if (i > 0 && num < this.borderBuckets[i - 1])
					{
						num = this.borderBuckets[i - 1];
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			return num;
		}

		// Token: 0x04000612 RID: 1554
		private PercentileCounter[] percentileCounters;

		// Token: 0x04000613 RID: 1555
		private long[] borderBuckets;

		// Token: 0x04000614 RID: 1556
		private int count;

		// Token: 0x02000137 RID: 311
		public struct Param
		{
			// Token: 0x060008CE RID: 2254 RVA: 0x00022795 File Offset: 0x00020995
			public Param(long granularity, long range)
			{
				ExAssert.RetailAssert(range % granularity == 0L, "The range MOD granularity must be zero.");
				this.granularity = granularity;
				this.range = range;
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x060008CF RID: 2255 RVA: 0x000227B6 File Offset: 0x000209B6
			public long Granularity
			{
				get
				{
					return this.granularity;
				}
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x060008D0 RID: 2256 RVA: 0x000227BE File Offset: 0x000209BE
			public long Range
			{
				get
				{
					return this.range;
				}
			}

			// Token: 0x04000615 RID: 1557
			private long granularity;

			// Token: 0x04000616 RID: 1558
			private long range;
		}
	}
}
