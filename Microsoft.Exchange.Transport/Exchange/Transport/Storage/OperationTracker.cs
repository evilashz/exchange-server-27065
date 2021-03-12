using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000D4 RID: 212
	public class OperationTracker<TOperation>
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x0001E79C File Offset: 0x0001C99C
		public OperationTracker(Func<TimeSpan> logThreshold, Action<TOperation, TimeSpan> logAction, TimeSpan percentileValueGranularity, TimeSpan percentileValueMaximum)
		{
			this.logThreshold = logThreshold;
			this.logAction = logAction;
			this.percentileCounter = new PercentileCounter(TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(1.0), percentileValueGranularity.Ticks, percentileValueMaximum.Ticks);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001E808 File Offset: 0x0001CA08
		public long TotalOperationCount
		{
			get
			{
				return this.operationCount;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001E810 File Offset: 0x0001CA10
		public ICollection<OperationTracker<TOperation>.StackCounter> TracedStack
		{
			get
			{
				return this.stackCounters.Values;
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001E81D File Offset: 0x0001CA1D
		public void Enter(TOperation operation)
		{
			this.currentOperations.TryAdd(operation, Tuple.Create<Stopwatch, Thread>(Stopwatch.StartNew(), Thread.CurrentThread));
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001E83B File Offset: 0x0001CA3B
		public void StartTracing(int operationLimit, TimeSpan traceThreshold)
		{
			this.traceLimit = 0;
			this.stackCounters.Clear();
			this.traceCount = 0;
			this.traceThreshold = traceThreshold;
			this.traceLimit = operationLimit;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001E868 File Offset: 0x0001CA68
		public TimeSpan Exit(TOperation operation)
		{
			Tuple<Stopwatch, Thread> tuple;
			if (!this.currentOperations.TryRemove(operation, out tuple))
			{
				return TimeSpan.Zero;
			}
			Interlocked.Increment(ref this.operationCount);
			Stopwatch item = tuple.Item1;
			item.Stop();
			this.percentileCounter.AddValue(item.ElapsedTicks);
			if (this.logThreshold != null && this.logAction != null && item.Elapsed > this.logThreshold())
			{
				this.logAction(operation, item.Elapsed);
			}
			if (this.traceCount < this.traceLimit && item.Elapsed > this.traceThreshold)
			{
				Interlocked.Increment(ref this.traceCount);
				StackTrace stackTrace = new StackTrace(1, true);
				string text = stackTrace.ToString();
				int hashCode = text.GetHashCode();
				OperationTracker<TOperation>.StackCounter stackCounter;
				if (this.stackCounters.TryGetValue(hashCode, out stackCounter))
				{
					stackCounter.Increment();
				}
				else
				{
					this.stackCounters.TryAdd(hashCode, new OperationTracker<TOperation>.StackCounter(text, 1));
				}
			}
			return tuple.Item1.Elapsed;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001E96F File Offset: 0x0001CB6F
		public TimeSpan PercentileQuery(double percentage)
		{
			return TimeSpan.FromTicks(this.percentileCounter.PercentileQuery(percentage));
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001EBB8 File Offset: 0x0001CDB8
		public IEnumerable<Tuple<TOperation, TimeSpan, StackTrace>> GetRunningOperations()
		{
			foreach (KeyValuePair<TOperation, Tuple<Stopwatch, Thread>> operation in this.currentOperations)
			{
				KeyValuePair<TOperation, Tuple<Stopwatch, Thread>> keyValuePair = operation;
				keyValuePair.Value.Item2.Suspend();
				Tuple<TOperation, TimeSpan, StackTrace> result;
				try
				{
					KeyValuePair<TOperation, Tuple<Stopwatch, Thread>> keyValuePair2 = operation;
					TOperation key = keyValuePair2.Key;
					KeyValuePair<TOperation, Tuple<Stopwatch, Thread>> keyValuePair3 = operation;
					TimeSpan elapsed = keyValuePair3.Value.Item1.Elapsed;
					KeyValuePair<TOperation, Tuple<Stopwatch, Thread>> keyValuePair4 = operation;
					result = Tuple.Create<TOperation, TimeSpan, StackTrace>(key, elapsed, new StackTrace(keyValuePair4.Value.Item2, true));
				}
				finally
				{
					KeyValuePair<TOperation, Tuple<Stopwatch, Thread>> keyValuePair5 = operation;
					keyValuePair5.Value.Item2.Resume();
				}
				yield return result;
			}
			yield break;
		}

		// Token: 0x040003CF RID: 975
		private readonly ConcurrentDictionary<TOperation, Tuple<Stopwatch, Thread>> currentOperations = new ConcurrentDictionary<TOperation, Tuple<Stopwatch, Thread>>();

		// Token: 0x040003D0 RID: 976
		private readonly ConcurrentDictionary<int, OperationTracker<TOperation>.StackCounter> stackCounters = new ConcurrentDictionary<int, OperationTracker<TOperation>.StackCounter>();

		// Token: 0x040003D1 RID: 977
		private readonly PercentileCounter percentileCounter;

		// Token: 0x040003D2 RID: 978
		private readonly Func<TimeSpan> logThreshold;

		// Token: 0x040003D3 RID: 979
		private readonly Action<TOperation, TimeSpan> logAction;

		// Token: 0x040003D4 RID: 980
		private TimeSpan traceThreshold;

		// Token: 0x040003D5 RID: 981
		private long operationCount;

		// Token: 0x040003D6 RID: 982
		private int traceCount;

		// Token: 0x040003D7 RID: 983
		private volatile int traceLimit;

		// Token: 0x020000D5 RID: 213
		public class StackCounter
		{
			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0001EBD5 File Offset: 0x0001CDD5
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0001EBDD File Offset: 0x0001CDDD
			// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0001EBE5 File Offset: 0x0001CDE5
			public string StackTrace { get; private set; }

			// Token: 0x060007B4 RID: 1972 RVA: 0x0001EBEE File Offset: 0x0001CDEE
			public StackCounter(string stackTrace, int initialCount)
			{
				this.StackTrace = stackTrace;
				this.count = initialCount;
			}

			// Token: 0x060007B5 RID: 1973 RVA: 0x0001EC04 File Offset: 0x0001CE04
			public void Increment()
			{
				Interlocked.Increment(ref this.count);
			}

			// Token: 0x040003D8 RID: 984
			private int count;
		}
	}
}
