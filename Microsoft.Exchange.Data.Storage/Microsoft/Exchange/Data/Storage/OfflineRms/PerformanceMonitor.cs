using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000ACA RID: 2762
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PerformanceMonitor
	{
		// Token: 0x06006475 RID: 25717 RVA: 0x001A9ED4 File Offset: 0x001A80D4
		public PerformanceMonitor(string name, ServerManagerLog.Subcomponent component, int minimumRequiredSampleCount, TimeSpan minimumRequiredSamplePeriod)
		{
			this.name = name;
			this.component = component;
			this.minimumRequiredSampleCount = minimumRequiredSampleCount;
			this.minimumRequiredSamplePeriod = minimumRequiredSamplePeriod;
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x001A9F29 File Offset: 0x001A8129
		public void Record(Stopwatch watch)
		{
			if (watch == null)
			{
				throw new ArgumentNullException("watch");
			}
			watch.Stop();
			this.Record(watch.ElapsedMilliseconds);
			watch.Reset();
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x001A9F54 File Offset: 0x001A8154
		internal void Record(long elapsedMilliseconds)
		{
			bool flag = false;
			long num = 0L;
			long num2 = 0L;
			long num3 = 0L;
			int num4 = 0;
			TimeSpan timeSpan = TimeSpan.MinValue;
			lock (this.syncObject)
			{
				this.totalCostMilliseconds += elapsedMilliseconds;
				this.sampleCount++;
				if (elapsedMilliseconds > this.highestCost)
				{
					this.highestCost = elapsedMilliseconds;
				}
				if (elapsedMilliseconds < this.lowestCost)
				{
					this.lowestCost = elapsedMilliseconds;
				}
				timeSpan = DateTime.UtcNow - this.lastReset;
				num4 = this.sampleCount;
				if (num4 >= this.minimumRequiredSampleCount && timeSpan >= this.minimumRequiredSamplePeriod)
				{
					num = this.lowestCost;
					num2 = this.highestCost;
					num3 = this.totalCostMilliseconds / (long)this.sampleCount;
					flag = true;
					this.lowestCost = long.MaxValue;
					this.highestCost = 0L;
					this.totalCostMilliseconds = 0L;
					this.sampleCount = 0;
					this.lastReset = DateTime.UtcNow;
				}
			}
			if (flag)
			{
				ServerManagerLog.LogEvent(this.component, ServerManagerLog.EventType.Statistics, null, string.Format("Performance for {0}: AverageCost {1} ms, HighestCost {2} ms, LowestCost {3} ms, SampleCount {4}, SamplePeriod {5}", new object[]
				{
					this.name,
					num3,
					num2,
					num,
					num4,
					timeSpan
				}));
			}
		}

		// Token: 0x0400391D RID: 14621
		private readonly object syncObject = new object();

		// Token: 0x0400391E RID: 14622
		private readonly int minimumRequiredSampleCount;

		// Token: 0x0400391F RID: 14623
		private readonly TimeSpan minimumRequiredSamplePeriod;

		// Token: 0x04003920 RID: 14624
		private readonly string name;

		// Token: 0x04003921 RID: 14625
		private readonly ServerManagerLog.Subcomponent component;

		// Token: 0x04003922 RID: 14626
		private long totalCostMilliseconds;

		// Token: 0x04003923 RID: 14627
		private int sampleCount;

		// Token: 0x04003924 RID: 14628
		private long highestCost;

		// Token: 0x04003925 RID: 14629
		private long lowestCost = long.MaxValue;

		// Token: 0x04003926 RID: 14630
		private DateTime lastReset = DateTime.UtcNow;
	}
}
