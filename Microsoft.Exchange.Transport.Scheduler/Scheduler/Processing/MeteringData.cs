using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200001D RID: 29
	internal class MeteringData
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00003774 File Offset: 0x00001974
		public MeteringData(TimeSpan historyLength, TimeSpan historyBucketSize, Func<DateTime> timeProvider)
		{
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("historyLength", historyLength, TimeSpan.Zero.Add(TimeSpan.FromTicks(1L)), TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("historyBucketSize", historyBucketSize, TimeSpan.Zero.Add(TimeSpan.FromTicks(1L)), TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfNull("timeProvider", timeProvider);
			this.totalMemoryCounter = new SlidingTotalCounter(historyLength, historyBucketSize, timeProvider);
			this.totalProcessingCounter = new SlidingTotalCounter(historyLength, historyBucketSize, timeProvider);
			this.timeProvider = timeProvider;
			this.UpdateLastActivityTimestamp();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003803 File Offset: 0x00001A03
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000380B File Offset: 0x00001A0B
		public DateTime LastUpdated { get; set; }

		// Token: 0x06000081 RID: 129 RVA: 0x00003814 File Offset: 0x00001A14
		public bool IsEmpty()
		{
			return this.jobCount == 0;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000381F File Offset: 0x00001A1F
		public UsageData GetUsageData()
		{
			return new UsageData(this.jobCount, this.totalMemoryCounter.Sum, this.totalProcessingCounter.Sum);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003842 File Offset: 0x00001A42
		public void AddMemory(long size)
		{
			this.totalMemoryCounter.AddValue(size);
			this.UpdateLastActivityTimestamp();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003857 File Offset: 0x00001A57
		public void AddProcessing(long ticks)
		{
			this.totalProcessingCounter.AddValue(ticks);
			this.UpdateLastActivityTimestamp();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000386C File Offset: 0x00001A6C
		public void IncrementJobCount()
		{
			this.jobCount++;
			this.UpdateLastActivityTimestamp();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003882 File Offset: 0x00001A82
		public void DecrementJobCount()
		{
			this.jobCount--;
			this.UpdateLastActivityTimestamp();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003898 File Offset: 0x00001A98
		private void UpdateLastActivityTimestamp()
		{
			this.LastUpdated = this.timeProvider();
		}

		// Token: 0x0400004B RID: 75
		private readonly SlidingTotalCounter totalMemoryCounter;

		// Token: 0x0400004C RID: 76
		private readonly SlidingTotalCounter totalProcessingCounter;

		// Token: 0x0400004D RID: 77
		private readonly Func<DateTime> timeProvider;

		// Token: 0x0400004E RID: 78
		private int jobCount;
	}
}
