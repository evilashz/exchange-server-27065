using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000048 RID: 72
	internal sealed class ELCPerfCountersWrapper : IDisposable
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000FD64 File Offset: 0x0000DF64
		public ELCPerfCountersWrapper(IEnumerable<Tuple<ExPerformanceCounter, ExPerformanceCounter>> perfCounters)
		{
			this.perfCounterCollection = new Dictionary<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>>();
			foreach (Tuple<ExPerformanceCounter, ExPerformanceCounter> tuple in perfCounters)
			{
				this.perfCounterCollection.Add(tuple.Item1, new Tuple<ExPerformanceCounter, SlidingTotalCounter>(tuple.Item2, new SlidingTotalCounter(ELCPerfCountersWrapper.SlidingCounterInterval, ELCPerfCountersWrapper.SlidingCounterPrecision)));
			}
			this.refreshTimer = new GuardedTimer(new TimerCallback(this.RefreshOnTimer), null, ELCPerfCountersWrapper.RefreshInterval);
			this.ResetCounters();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000FE04 File Offset: 0x0000E004
		public void Increment(ExPerformanceCounter perfCounter, long incrementValue = 1L)
		{
			if (this.perfCounterCollection.ContainsKey(perfCounter))
			{
				perfCounter.IncrementBy(incrementValue);
				this.perfCounterCollection[perfCounter].Item2.AddValue(incrementValue);
				this.perfCounterCollection[perfCounter].Item1.RawValue = this.perfCounterCollection[perfCounter].Item2.Sum;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000FE6B File Offset: 0x0000E06B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000FE7C File Offset: 0x0000E07C
		public void ResetCounters()
		{
			foreach (KeyValuePair<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>> keyValuePair in this.perfCounterCollection)
			{
				keyValuePair.Value.Item1.RawValue = 0L;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000FEDC File Offset: 0x0000E0DC
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

		// Token: 0x060002A7 RID: 679 RVA: 0x0000FF10 File Offset: 0x0000E110
		private void RefreshOnTimer(object state)
		{
			foreach (KeyValuePair<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>> keyValuePair in this.perfCounterCollection)
			{
				keyValuePair.Value.Item1.RawValue = keyValuePair.Value.Item2.Sum;
			}
		}

		// Token: 0x04000240 RID: 576
		private static readonly TimeSpan SlidingCounterInterval = TimeSpan.FromHours(1.0);

		// Token: 0x04000241 RID: 577
		private static readonly TimeSpan SlidingCounterPrecision = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000242 RID: 578
		private static readonly TimeSpan RefreshInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000243 RID: 579
		private readonly Dictionary<ExPerformanceCounter, Tuple<ExPerformanceCounter, SlidingTotalCounter>> perfCounterCollection;

		// Token: 0x04000244 RID: 580
		private GuardedTimer refreshTimer;

		// Token: 0x04000245 RID: 581
		private bool disposed;
	}
}
