using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x0200000B RID: 11
	internal sealed class JournalPerfCountersWrapper : IDisposable
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002F4C File Offset: 0x0000114C
		public JournalPerfCountersWrapper(IEnumerable<Tuple<ExPerformanceCounter, ExPerformanceCounter>> perfCounters)
		{
			this.perfCounterCollection = new Dictionary<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>>();
			foreach (Tuple<ExPerformanceCounter, ExPerformanceCounter> tuple in perfCounters)
			{
				this.perfCounterCollection.Add(tuple.Item1, new Tuple<ExPerformanceCounter, SlidingTotalCounter>(tuple.Item2, new SlidingTotalCounter(JournalPerfCountersWrapper.SlidingCounterInterval, JournalPerfCountersWrapper.SlidingCounterPrecision)));
			}
			this.refreshTimer = new GuardedTimer(new TimerCallback(this.RefreshOnTimer), null, JournalPerfCountersWrapper.RefreshInterval);
			this.ResetCounters();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002FEC File Offset: 0x000011EC
		public void Increment(ExPerformanceCounter perfCounter, long incrementValue = 1L)
		{
			perfCounter.IncrementBy(incrementValue);
			this.perfCounterCollection[perfCounter].Item2.AddValue(incrementValue);
			this.perfCounterCollection[perfCounter].Item1.RawValue = this.perfCounterCollection[perfCounter].Item2.Sum;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003045 File Offset: 0x00001245
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003054 File Offset: 0x00001254
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.refreshTimer.Change(-1, -1);
					this.refreshTimer.Dispose(true);
					this.ResetCounters();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003088 File Offset: 0x00001288
		private void ResetCounters()
		{
			foreach (KeyValuePair<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>> keyValuePair in this.perfCounterCollection)
			{
				keyValuePair.Value.Item1.RawValue = 0L;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000030E8 File Offset: 0x000012E8
		private void RefreshOnTimer(object state)
		{
			foreach (KeyValuePair<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>> keyValuePair in this.perfCounterCollection)
			{
				keyValuePair.Value.Item1.RawValue = keyValuePair.Value.Item2.Sum;
			}
		}

		// Token: 0x0400005E RID: 94
		private static readonly TimeSpan SlidingCounterInterval = TimeSpan.FromHours(1.0);

		// Token: 0x0400005F RID: 95
		private static readonly TimeSpan SlidingCounterPrecision = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000060 RID: 96
		private static readonly TimeSpan RefreshInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000061 RID: 97
		private readonly Dictionary<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>> perfCounterCollection;

		// Token: 0x04000062 RID: 98
		private GuardedTimer refreshTimer;

		// Token: 0x04000063 RID: 99
		private bool disposed;
	}
}
