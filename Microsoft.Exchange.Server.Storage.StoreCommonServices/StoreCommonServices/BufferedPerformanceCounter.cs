using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000022 RID: 34
	public class BufferedPerformanceCounter : ExPerformanceCounter
	{
		// Token: 0x06000150 RID: 336 RVA: 0x000107A7 File Offset: 0x0000E9A7
		public BufferedPerformanceCounter(string categoryName, string counterName, string instanceName, ExPerformanceCounter totalInstanceCounter, params ExPerformanceCounter[] autoUpdateCounters) : base(categoryName, counterName, instanceName, totalInstanceCounter, autoUpdateCounters)
		{
			this.accumulatedValue = 0L;
			this.currentValue = base.RawValue;
			BufferedPerformanceCounter.counters.TryAdd(this, null);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000107D7 File Offset: 0x0000E9D7
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000107E6 File Offset: 0x0000E9E6
		public override long RawValue
		{
			get
			{
				return this.currentValue + this.accumulatedValue;
			}
			set
			{
				this.rawValueWasSet = true;
				this.currentValue = value;
				this.accumulatedValue = 0L;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00010800 File Offset: 0x0000EA00
		public override void Close()
		{
			using (LockManager.Lock(BufferedPerformanceCounter.counters))
			{
				object obj;
				BufferedPerformanceCounter.counters.TryRemove(this, out obj);
			}
			base.Close();
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0001084C File Offset: 0x0000EA4C
		public long BaseRawValueForTest
		{
			get
			{
				return base.RawValue;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0001085C File Offset: 0x0000EA5C
		internal static void Initialize()
		{
			BufferedPerformanceCounter.flushTask = new RecurringTask<object>(delegate(TaskExecutionDiagnosticsProxy diagnosticsContext, object context, Func<bool> shouldCallbackContinue)
			{
				BufferedPerformanceCounter.FlushCounters();
			}, null, TimeSpan.FromSeconds(1.0), false);
			BufferedPerformanceCounter.flushTask.Start();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000108AA File Offset: 0x0000EAAA
		internal static void Terminate()
		{
			if (BufferedPerformanceCounter.flushTask != null)
			{
				BufferedPerformanceCounter.flushTask.Stop();
				BufferedPerformanceCounter.flushTask.Dispose();
				BufferedPerformanceCounter.flushTask = null;
			}
			BufferedPerformanceCounter.FlushCounters();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000108D4 File Offset: 0x0000EAD4
		internal static void FlushCounters()
		{
			using (LockManager.Lock(BufferedPerformanceCounter.counters))
			{
				foreach (KeyValuePair<BufferedPerformanceCounter, object> keyValuePair in BufferedPerformanceCounter.counters)
				{
					keyValuePair.Key.Flush();
				}
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0001094C File Offset: 0x0000EB4C
		public override void Reset()
		{
			base.Reset();
			this.currentValue = 0L;
			this.accumulatedValue = 0L;
			this.rawValueWasSet = false;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0001096B File Offset: 0x0000EB6B
		public override long IncrementBy(long incrementValue)
		{
			Interlocked.Add(ref this.accumulatedValue, incrementValue);
			return this.currentValue + this.accumulatedValue;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00010988 File Offset: 0x0000EB88
		internal void Flush()
		{
			long num = Interlocked.Exchange(ref this.accumulatedValue, 0L);
			if (this.rawValueWasSet)
			{
				long rawValue = this.currentValue + num;
				base.RawValue = rawValue;
				this.currentValue = rawValue;
				this.rawValueWasSet = false;
				return;
			}
			if (num != 0L)
			{
				this.currentValue = base.IncrementBy(num);
				return;
			}
			this.currentValue = base.RawValue;
		}

		// Token: 0x040001E7 RID: 487
		private static ConcurrentDictionary<BufferedPerformanceCounter, object> counters = new ConcurrentDictionary<BufferedPerformanceCounter, object>();

		// Token: 0x040001E8 RID: 488
		private static RecurringTask<object> flushTask;

		// Token: 0x040001E9 RID: 489
		private long accumulatedValue;

		// Token: 0x040001EA RID: 490
		private long currentValue;

		// Token: 0x040001EB RID: 491
		private bool rawValueWasSet;
	}
}
