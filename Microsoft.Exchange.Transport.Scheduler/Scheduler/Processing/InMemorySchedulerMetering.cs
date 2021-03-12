using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000010 RID: 16
	internal class InMemorySchedulerMetering : RefreshableComponent, ISchedulerMetering
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002B18 File Offset: 0x00000D18
		public InMemorySchedulerMetering(TimeSpan usageDataTtl, TimeSpan historyLength, TimeSpan historyBucketSize, TimeSpan updateInterval, Func<DateTime> timeProvider) : base(updateInterval, timeProvider)
		{
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("usageDataTtl", usageDataTtl, TimeSpan.Zero.Add(TimeSpan.FromTicks(1L)), TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("historyLength", historyLength, TimeSpan.Zero.Add(TimeSpan.FromTicks(1L)), TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("historyBucketSize", historyBucketSize, TimeSpan.Zero.Add(TimeSpan.FromTicks(1L)), TimeSpan.MaxValue);
			this.usageDataTtl = usageDataTtl;
			this.historyLength = historyLength;
			this.historyBucketSize = historyBucketSize;
			this.totalUsageData = new MeteringData(this.historyLength, this.historyBucketSize, timeProvider);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BDB File Offset: 0x00000DDB
		public InMemorySchedulerMetering(TimeSpan usageDataTtl, TimeSpan historyLength, TimeSpan historyBucketSize, TimeSpan updateInterval) : this(usageDataTtl, historyLength, historyBucketSize, updateInterval, () => DateTime.UtcNow)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C05 File Offset: 0x00000E05
		public UsageData GetTotalUsage()
		{
			return this.totalUsageData.GetUsageData();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C1C File Offset: 0x00000E1C
		public void RecordStart(IEnumerable<IMessageScope> scopes, long memorySize)
		{
			ArgumentValidator.ThrowIfNull("scopes", scopes);
			ArgumentValidator.ThrowIfInvalidValue<long>("memorySize", memorySize, (long l) => l >= 0L);
			this.totalUsageData.IncrementJobCount();
			this.totalUsageData.AddMemory(memorySize);
			foreach (IMessageScope scope in scopes)
			{
				this.RecordStart(scope, memorySize);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public void RecordEnd(IEnumerable<IMessageScope> scopes, TimeSpan duration)
		{
			ArgumentValidator.ThrowIfNull("scopes", scopes);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("duration", duration, TimeSpan.Zero, TimeSpan.MaxValue);
			this.totalUsageData.DecrementJobCount();
			this.totalUsageData.AddProcessing(duration.Ticks);
			foreach (IMessageScope scope in scopes)
			{
				this.RecordEnd(scope, duration);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D38 File Offset: 0x00000F38
		public bool TryGetUsage(IMessageScope scope, out UsageData data)
		{
			MeteringData meteringData;
			if (this.perScopeData.TryGetValue(scope, out meteringData))
			{
				data = meteringData.GetUsageData();
				return true;
			}
			data = null;
			return false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D64 File Offset: 0x00000F64
		protected override void Refresh(DateTime timestamp)
		{
			DateTime t = timestamp - this.usageDataTtl;
			List<IMessageScope> list = new List<IMessageScope>();
			foreach (KeyValuePair<IMessageScope, MeteringData> keyValuePair in this.perScopeData)
			{
				if (keyValuePair.Value.IsEmpty() && keyValuePair.Value.LastUpdated < t)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (IMessageScope key in list)
			{
				this.perScopeData.Remove(key);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002E38 File Offset: 0x00001038
		private void RecordStart(IMessageScope scope, long memorySize)
		{
			MeteringData meteringData;
			if (!this.perScopeData.TryGetValue(scope, out meteringData))
			{
				meteringData = new MeteringData(this.historyLength, this.historyBucketSize, base.TimeProvider);
				this.perScopeData.Add(scope, meteringData);
			}
			meteringData.IncrementJobCount();
			meteringData.AddMemory(memorySize);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E88 File Offset: 0x00001088
		private void RecordEnd(IMessageScope scope, TimeSpan duration)
		{
			MeteringData meteringData;
			if (this.perScopeData.TryGetValue(scope, out meteringData))
			{
				meteringData.DecrementJobCount();
				meteringData.AddProcessing(duration.Ticks);
				return;
			}
			throw new InvalidOperationException(string.Format("No metering data found for expected scope {0}", scope));
		}

		// Token: 0x04000028 RID: 40
		private readonly MeteringData totalUsageData;

		// Token: 0x04000029 RID: 41
		private readonly TimeSpan historyLength;

		// Token: 0x0400002A RID: 42
		private readonly TimeSpan historyBucketSize;

		// Token: 0x0400002B RID: 43
		private readonly TimeSpan usageDataTtl;

		// Token: 0x0400002C RID: 44
		private readonly IDictionary<IMessageScope, MeteringData> perScopeData = new Dictionary<IMessageScope, MeteringData>();
	}
}
