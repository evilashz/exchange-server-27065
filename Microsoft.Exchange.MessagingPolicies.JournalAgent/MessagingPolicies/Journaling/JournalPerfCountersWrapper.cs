using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x0200000A RID: 10
	internal sealed class JournalPerfCountersWrapper : IDisposable
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002D44 File Offset: 0x00000F44
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

		// Token: 0x06000018 RID: 24 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public void Increment(ExPerformanceCounter perfCounter, long incrementValue = 1L)
		{
			perfCounter.IncrementBy(incrementValue);
			this.perfCounterCollection[perfCounter].Item2.AddValue(incrementValue);
			this.perfCounterCollection[perfCounter].Item1.RawValue = this.perfCounterCollection[perfCounter].Item2.Sum;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002E3D File Offset: 0x0000103D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002E4C File Offset: 0x0000104C
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

		// Token: 0x0600001B RID: 27 RVA: 0x00002E80 File Offset: 0x00001080
		private void ResetCounters()
		{
			foreach (KeyValuePair<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>> keyValuePair in this.perfCounterCollection)
			{
				keyValuePair.Value.Item1.RawValue = 0L;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002EE0 File Offset: 0x000010E0
		private void RefreshOnTimer(object state)
		{
			foreach (KeyValuePair<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>> keyValuePair in this.perfCounterCollection)
			{
				keyValuePair.Value.Item1.RawValue = keyValuePair.Value.Item2.Sum;
			}
		}

		// Token: 0x04000046 RID: 70
		private static readonly TimeSpan SlidingCounterInterval = TimeSpan.FromHours(1.0);

		// Token: 0x04000047 RID: 71
		private static readonly TimeSpan SlidingCounterPrecision = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000048 RID: 72
		private static readonly TimeSpan RefreshInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000049 RID: 73
		private readonly Dictionary<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>> perfCounterCollection;

		// Token: 0x0400004A RID: 74
		private GuardedTimer refreshTimer;

		// Token: 0x0400004B RID: 75
		private bool disposed;
	}
}
