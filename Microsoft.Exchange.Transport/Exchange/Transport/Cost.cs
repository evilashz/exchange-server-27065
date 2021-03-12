using System;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200032B RID: 811
	internal class Cost : IComparable, IComparable<Cost>
	{
		// Token: 0x0600231B RID: 8987 RVA: 0x00085771 File Offset: 0x00083971
		internal Cost(WaitCondition condition, CostConfiguration config, Trace tracer)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			this.condition = condition;
			this.config = config;
			this.tracer = tracer;
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000857A7 File Offset: 0x000839A7
		internal Cost(CostConfiguration config, int threads, Trace tracer) : this(null, config, tracer)
		{
			if (threads < 0)
			{
				throw new ArgumentException("threads has to be positive", "threads");
			}
			this.usedThreads = threads;
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x000857CD File Offset: 0x000839CD
		internal bool IsEmpty
		{
			get
			{
				return this.usedThreads == 0 && this.accessTokensCount == 0 && this.ProcessingTotal == 0L && this.startedProcessingCount == 0 && this.MemoryTotal == 0L;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x000857FD File Offset: 0x000839FD
		internal WaitCondition Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x00085805 File Offset: 0x00083A05
		internal int UsedThreads
		{
			get
			{
				if (this.config.ReversedCost)
				{
					return this.config.MaxThreads - this.usedThreads - this.accessTokensCount;
				}
				return this.usedThreads + this.accessTokensCount;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x0008583B File Offset: 0x00083A3B
		internal CostObjectState ObjectState
		{
			get
			{
				return this.objectState;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x00085844 File Offset: 0x00083A44
		internal long ProcessingTotal
		{
			get
			{
				if (!this.config.ProcessingHistoryEnabled || this.processingHistory == null)
				{
					if (this.config.ReversedCost)
					{
						return this.config.ProcessingCapacity;
					}
					return 0L;
				}
				else
				{
					long sum;
					lock (this.syncRoot)
					{
						sum = this.processingHistory.Sum;
					}
					if (this.config.ReversedCost)
					{
						return Math.Max(0L, this.config.ProcessingCapacity - sum);
					}
					return sum;
				}
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x000858E0 File Offset: 0x00083AE0
		internal long MemoryTotal
		{
			get
			{
				if (!this.config.MemoryCollectionEnabled)
				{
					return 0L;
				}
				if (this.config.ReversedCost)
				{
					return Convert.ToInt64(this.config.MemoryThreshold.ToBytes());
				}
				if (this.memoryUsed == null)
				{
					return 0L;
				}
				long sum;
				lock (this.syncRoot)
				{
					sum = this.memoryUsed.Sum;
				}
				return sum;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x00085968 File Offset: 0x00083B68
		public double LastThrottleFactor
		{
			get
			{
				return this.lastThrottleFactor;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x00085970 File Offset: 0x00083B70
		public bool HasOverride
		{
			get
			{
				return this.LastThrottleFactor >= 0.0;
			}
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00085986 File Offset: 0x00083B86
		public static bool operator <(Cost a, Cost b)
		{
			return a.CompareTo(b) == -1;
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x00085992 File Offset: 0x00083B92
		public static bool operator >(Cost a, Cost b)
		{
			return a.CompareTo(b) == 1;
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0008599E File Offset: 0x00083B9E
		public static bool operator >=(Cost a, Cost b)
		{
			return a.CompareTo(b) != -1;
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000859AD File Offset: 0x00083BAD
		public static bool operator <=(Cost a, Cost b)
		{
			return a.CompareTo(b) != 1;
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000859BC File Offset: 0x00083BBC
		public static bool operator ==(Cost a, Cost b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000859C5 File Offset: 0x00083BC5
		public static bool operator !=(Cost a, Cost b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000859D4 File Offset: 0x00083BD4
		public int CompareTo(object obj)
		{
			Cost cost = (Cost)obj;
			if (cost == null)
			{
				throw new ArgumentException("obj is not of type Cost");
			}
			return this.CompareTo(cost);
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00085A03 File Offset: 0x00083C03
		public int CompareTo(Cost obj)
		{
			return this.CompareTo(this.UsedThreads, obj, obj.UsedThreads);
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x00085A18 File Offset: 0x00083C18
		public int CompareWithoutAccessToken(Cost cost)
		{
			int num = this.UsedThreads;
			int otherCostUsedThreads = cost.UsedThreads;
			if (this.config.ReversedCost)
			{
				num = this.config.MaxThreads - this.usedThreads;
			}
			if (cost.config.ReversedCost)
			{
				otherCostUsedThreads = cost.config.MaxThreads - cost.usedThreads;
			}
			return this.CompareTo(num, cost, otherCostUsedThreads);
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00085A7C File Offset: 0x00083C7C
		private int CompareTo(int usedThreads, Cost otherCost, int otherCostUsedThreads)
		{
			if (this.config.ReversedCost == otherCost.config.ReversedCost)
			{
				throw new InvalidOperationException("Cannot compare two reversed Cost objects or two normal Cost objects");
			}
			double num = (double)otherCostUsedThreads;
			double num2 = (double)usedThreads;
			if (otherCost.config.ReversedCost)
			{
				num = this.ApplyOverride(this, otherCostUsedThreads);
			}
			else if (this.config.ReversedCost)
			{
				num2 = this.ApplyOverride(otherCost, usedThreads);
			}
			long num3 = otherCost.config.ProcessingHistoryEnabled ? otherCost.ProcessingTotal : 0L;
			if (num2 < num && (!this.config.ProcessingHistoryEnabled || this.ProcessingTotal < num3 || (this.ProcessingTotal == 0L && this.ProcessingTotal == num3)) && (!this.config.MemoryCollectionEnabled || this.MemoryTotal < (otherCost.config.MemoryCollectionEnabled ? otherCost.MemoryTotal : 0L)))
			{
				return -1;
			}
			if (num2 == num && this.ProcessingTotal == otherCost.ProcessingTotal && this.MemoryTotal == otherCost.MemoryTotal)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00085B78 File Offset: 0x00083D78
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			Cost cost = obj as Cost;
			return !(cost == null) && (this.config.ReversedCost == cost.config.ReversedCost && this.usedThreads == cost.usedThreads && this.accessTokensCount == cost.accessTokensCount && this.startedProcessingCount == cost.startedProcessingCount && ((this.processingHistory == null && cost.processingHistory == null) || (this.processingHistory != null && cost.processingHistory != null && this.processingHistory.Equals(cost.processingHistory)))) && ((this.memoryUsed == null && cost.memoryUsed == null) || (this.memoryUsed != null && cost.memoryUsed != null && this.memoryUsed.Equals(cost.memoryUsed)));
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00085C59 File Offset: 0x00083E59
		public override int GetHashCode()
		{
			return this.usedThreads.GetHashCode() ^ this.accessTokensCount.GetHashCode() ^ this.startedProcessingCount;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x00085C7C File Offset: 0x00083E7C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Used Threads: {0}, Access Tokens: {1}", this.config.ReversedCost ? this.UsedThreads : this.usedThreads, this.accessTokensCount);
			if (this.config.ProcessingHistoryEnabled)
			{
				stringBuilder.AppendFormat(", Processing Total: {0}", this.ProcessingTotal);
			}
			if (this.config.MemoryCollectionEnabled)
			{
				stringBuilder.AppendFormat(", Memory: {0}", this.MemoryTotal);
			}
			if ((this.config.OverrideEnabled || this.config.TestOverrideEnabled) && this.LastThrottleFactor != -1.0)
			{
				stringBuilder.AppendFormat(", ThrottleFactor: {0}", this.LastThrottleFactor);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00085D58 File Offset: 0x00083F58
		internal bool HasCapacity(bool allowAboveThreshold, int inFlightUnlocks)
		{
			if (!this.config.ReversedCost)
			{
				throw new InvalidOperationException("HasCapacity is supported on ReversedCost only");
			}
			int maxThreads = this.config.MaxThreads;
			if (allowAboveThreshold)
			{
				int maxAllowedCapacity = this.config.MaxAllowedCapacity;
				double num = (double)(this.usedThreads + this.accessTokensCount + inFlightUnlocks);
				return num / (double)maxThreads * 100.0 < (double)maxAllowedCapacity;
			}
			return this.usedThreads + this.accessTokensCount < maxThreads;
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00085DCC File Offset: 0x00083FCC
		internal int GetIndexOf()
		{
			return this.usedThreads + this.accessTokensCount;
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x00085DDB File Offset: 0x00083FDB
		internal void RecordThreadStart()
		{
			Interlocked.Increment(ref this.usedThreads);
			this.objectState = CostObjectState.Live;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00085DF0 File Offset: 0x00083FF0
		internal void RecordThreadEnd()
		{
			int num = Interlocked.Decrement(ref this.usedThreads);
			if (num < 0)
			{
				throw new InvalidOperationException("Cannot decrement used threads below zero");
			}
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00085E18 File Offset: 0x00084018
		internal bool AddMemoryCost(ByteQuantifiedSize bytesUsed)
		{
			if (!this.config.MemoryCollectionEnabled)
			{
				return false;
			}
			if (this.config.ReversedCost)
			{
				return true;
			}
			if (bytesUsed > this.config.MinInterestingMemorySize)
			{
				lock (this.syncRoot)
				{
					if (this.memoryUsed == null)
					{
						this.memoryUsed = new SlidingTotalCounter(this.config.HistoryInterval, this.config.BucketSize, new Func<DateTime>(this.TimeProvider));
					}
					this.memoryUsed.AddValue((long)bytesUsed.ToBytes());
					this.objectState = CostObjectState.Live;
				}
			}
			return true;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x00085ED4 File Offset: 0x000840D4
		internal bool MarkEmptyCostForDeletion()
		{
			if (this.objectState != CostObjectState.Init && this.IsEmpty)
			{
				this.objectState = CostObjectState.MarkedForDeletion;
				return true;
			}
			return false;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x00085EF0 File Offset: 0x000840F0
		internal void MarkObjectDeleted()
		{
			this.objectState = CostObjectState.Deleted;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x00085EF9 File Offset: 0x000840F9
		internal void AddToken()
		{
			Interlocked.Increment(ref this.accessTokensCount);
			this.objectState = CostObjectState.Live;
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x00085F10 File Offset: 0x00084110
		internal void ReturnToken()
		{
			Interlocked.Increment(ref this.usedThreads);
			int num = Interlocked.Decrement(ref this.accessTokensCount);
			if (num < 0)
			{
				throw new InvalidOperationException("Cannot decrement access tokens below zero");
			}
			this.objectState = CostObjectState.Live;
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x00085F4C File Offset: 0x0008414C
		internal void FailToken()
		{
			int num = Interlocked.Decrement(ref this.accessTokensCount);
			if (num < 0)
			{
				throw new InvalidOperationException("Cannot decrement access tokens below zero");
			}
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x00085F74 File Offset: 0x00084174
		internal void StartProcessing()
		{
			if (!this.config.ProcessingHistoryEnabled)
			{
				return;
			}
			Interlocked.Increment(ref this.startedProcessingCount);
			this.objectState = CostObjectState.Live;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x00085F98 File Offset: 0x00084198
		internal void CompleteProcessing(DateTime startTime)
		{
			if (!this.config.ProcessingHistoryEnabled)
			{
				return;
			}
			int num = Interlocked.Decrement(ref this.startedProcessingCount);
			if (num < 0)
			{
				throw new InvalidOperationException("Cannot complete processing something that wasn't started");
			}
			DateTime d = this.TimeProvider();
			TimeSpan t = d - startTime;
			if (t > this.config.MinInterestingProcessingInterval || this.config.ReversedCost)
			{
				lock (this.syncRoot)
				{
					if (this.processingHistory == null)
					{
						this.processingHistory = new SlidingTotalCounter(this.config.HistoryInterval, this.config.BucketSize, new Func<DateTime>(this.TimeProvider));
					}
					long value = (long)Math.Min(this.config.HistoryInterval.TotalMilliseconds, t.TotalMilliseconds);
					this.processingHistory.AddValue(value);
				}
			}
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x00086098 File Offset: 0x00084298
		internal XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement(this.config.ReversedCost ? "FreeCost" : "Cost", new object[]
			{
				new XElement("UsedThreads", this.config.ReversedCost ? this.UsedThreads : this.usedThreads),
				new XElement("AccessTokens", this.accessTokensCount),
				new XElement("ProcessingTotal", this.ProcessingTotal),
				new XElement("MemoryTotal", this.MemoryTotal)
			});
			if (this.LastThrottleFactor != -1.0)
			{
				xelement.Add(new XElement("LastOverrideFactor", this.LastThrottleFactor));
			}
			return xelement;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0008618C File Offset: 0x0008438C
		private double ApplyOverride(Cost cost, int currentLimit)
		{
			if (cost.config.QuotaOverride == null || (!cost.config.OverrideEnabled && !cost.config.TestOverrideEnabled))
			{
				cost.lastThrottleFactor = -1.0;
				return (double)currentLimit;
			}
			double num = (double)currentLimit;
			ProcessingQuotaComponent.ProcessingData quotaOverride = cost.config.QuotaOverride.GetQuotaOverride(cost.condition);
			if (quotaOverride != null && quotaOverride.ThrottlingFactor < 1.0)
			{
				if (cost.config.OverrideEnabled)
				{
					num = (double)currentLimit * quotaOverride.ThrottlingFactor;
					cost.lastThrottleFactor = quotaOverride.ThrottlingFactor;
				}
				if (cost.config.TestOverrideEnabled)
				{
					this.tracer.TraceDebug<WaitCondition, double, double>((long)cost.GetHashCode(), "Quota for tenant {0} would be adjusted to {1}% and new limit is {2}", cost.condition, quotaOverride.ThrottlingFactor * 100.0, num);
				}
			}
			else
			{
				cost.lastThrottleFactor = -1.0;
			}
			return num;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x00086270 File Offset: 0x00084470
		private DateTime TimeProvider()
		{
			if (this.config.TimeProvider == null)
			{
				return DateTime.UtcNow;
			}
			return this.config.TimeProvider();
		}

		// Token: 0x04001247 RID: 4679
		private readonly WaitCondition condition;

		// Token: 0x04001248 RID: 4680
		private readonly CostConfiguration config;

		// Token: 0x04001249 RID: 4681
		private int usedThreads;

		// Token: 0x0400124A RID: 4682
		private int accessTokensCount;

		// Token: 0x0400124B RID: 4683
		private int startedProcessingCount;

		// Token: 0x0400124C RID: 4684
		private SlidingTotalCounter processingHistory;

		// Token: 0x0400124D RID: 4685
		private SlidingTotalCounter memoryUsed;

		// Token: 0x0400124E RID: 4686
		private double lastThrottleFactor;

		// Token: 0x0400124F RID: 4687
		private object syncRoot = new object();

		// Token: 0x04001250 RID: 4688
		private CostObjectState objectState;

		// Token: 0x04001251 RID: 4689
		private Trace tracer;
	}
}
