using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200068D RID: 1677
	internal class FixedTimeSumBase
	{
		// Token: 0x06001E7B RID: 7803 RVA: 0x00038BB4 File Offset: 0x00036DB4
		protected FixedTimeSumBase(uint windowBucketLength, ushort numberOfBuckets, uint? limit)
		{
			ArgumentValidator.ThrowIfOutOfRange<uint>("windowBucketLength", windowBucketLength, 1U, uint.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<int>("numberOfBuckets", (int)numberOfBuckets, 1, 65535);
			this.waitingBuckets = new Queue<FixedTimeSumBase.WindowBucket>((int)numberOfBuckets);
			this.windowBucketLength = windowBucketLength;
			this.lastCall = TimeProvider.UtcNow;
			this.limit = limit;
			this.totalWindowLength = TimeSpan.FromMilliseconds(windowBucketLength * (uint)numberOfBuckets);
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x00038C25 File Offset: 0x00036E25
		internal uint GetValue()
		{
			this.Update();
			return this.value;
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x00038C35 File Offset: 0x00036E35
		internal bool IsEmpty
		{
			get
			{
				return this.BucketCount == 0 && this.currentBucket == null;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00038C4A File Offset: 0x00036E4A
		internal int BucketCount
		{
			get
			{
				return this.waitingBuckets.Count;
			}
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x00038C58 File Offset: 0x00036E58
		internal void Clear()
		{
			lock (this.instanceLock)
			{
				this.waitingBuckets.Clear();
				this.currentBucket = null;
				this.value = 0U;
				this.lastCall = TimeProvider.UtcNow;
			}
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00038CB8 File Offset: 0x00036EB8
		internal void Update()
		{
			lock (this.instanceLock)
			{
				DateTime utcNow = this.SetLastCall(TimeProvider.UtcNow);
				this.InternalUpdate(utcNow);
			}
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00038D08 File Offset: 0x00036F08
		protected bool TryAdd(uint addend)
		{
			bool result = false;
			DateTime utcNow = this.SetLastCall(TimeProvider.UtcNow);
			lock (this.instanceLock)
			{
				this.InternalUpdate(utcNow);
				if (addend > 0U)
				{
					if (this.limit == null || this.IsUnderLimit(addend))
					{
						this.value += addend;
						result = this.GetCurrentBucket(utcNow).Add(utcNow, addend);
					}
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00038D9C File Offset: 0x00036F9C
		private void InternalUpdate(DateTime utcNow)
		{
			if (this.currentBucket != null && utcNow >= this.currentBucket.ExpireAt)
			{
				if (this.currentBucket.Value != 0U)
				{
					this.waitingBuckets.Enqueue(this.currentBucket);
				}
				this.currentBucket = null;
			}
			while (this.waitingBuckets.Count > 0)
			{
				FixedTimeSumBase.WindowBucket windowBucket = this.waitingBuckets.Peek();
				if (!(utcNow - windowBucket.ExpireAt >= this.totalWindowLength))
				{
					break;
				}
				this.waitingBuckets.Dequeue();
				this.value -= windowBucket.Value;
			}
			if (this.currentBucket == null)
			{
				int count = this.waitingBuckets.Count;
			}
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00038E58 File Offset: 0x00037058
		private FixedTimeSumBase.WindowBucket GetCurrentBucket(DateTime utcNow)
		{
			FixedTimeSumBase.WindowBucket result;
			lock (this.instanceLock)
			{
				if (this.currentBucket == null)
				{
					this.currentBucket = new FixedTimeSumBase.WindowBucket(utcNow, this.windowBucketLength);
				}
				result = this.currentBucket;
			}
			return result;
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00038EB4 File Offset: 0x000370B4
		private DateTime SetLastCall(DateTime utcNow)
		{
			if (this.lastCall < utcNow)
			{
				this.lastCall = utcNow;
			}
			return this.lastCall;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x00038ED4 File Offset: 0x000370D4
		private bool IsUnderLimit(uint addend)
		{
			ulong num = (ulong)this.value + (ulong)addend;
			if (num <= (ulong)-1)
			{
				ulong num2 = num;
				uint? num3 = this.limit;
				return num2 <= (ulong)num3.GetValueOrDefault() && num3 != null;
			}
			return false;
		}

		// Token: 0x04001E53 RID: 7763
		protected readonly uint windowBucketLength;

		// Token: 0x04001E54 RID: 7764
		private readonly TimeSpan totalWindowLength;

		// Token: 0x04001E55 RID: 7765
		private readonly uint? limit;

		// Token: 0x04001E56 RID: 7766
		protected Queue<FixedTimeSumBase.WindowBucket> waitingBuckets;

		// Token: 0x04001E57 RID: 7767
		protected FixedTimeSumBase.WindowBucket currentBucket;

		// Token: 0x04001E58 RID: 7768
		protected volatile uint value;

		// Token: 0x04001E59 RID: 7769
		private DateTime lastCall;

		// Token: 0x04001E5A RID: 7770
		private object instanceLock = new object();

		// Token: 0x0200068E RID: 1678
		protected class WindowBucket
		{
			// Token: 0x06001E86 RID: 7814 RVA: 0x00038F12 File Offset: 0x00037112
			public WindowBucket(DateTime utcNow, uint windowSizeMsec)
			{
				this.ExpireAt = utcNow.AddMilliseconds(windowSizeMsec);
				this.WindowTimeSpan = TimeSpan.FromMilliseconds(windowSizeMsec);
			}

			// Token: 0x17000818 RID: 2072
			// (get) Token: 0x06001E87 RID: 7815 RVA: 0x00038F38 File Offset: 0x00037138
			// (set) Token: 0x06001E88 RID: 7816 RVA: 0x00038F40 File Offset: 0x00037140
			internal DateTime ExpireAt { get; private set; }

			// Token: 0x17000819 RID: 2073
			// (get) Token: 0x06001E89 RID: 7817 RVA: 0x00038F49 File Offset: 0x00037149
			// (set) Token: 0x06001E8A RID: 7818 RVA: 0x00038F51 File Offset: 0x00037151
			internal TimeSpan WindowTimeSpan { get; private set; }

			// Token: 0x06001E8B RID: 7819 RVA: 0x00038F5A File Offset: 0x0003715A
			internal bool Add(DateTime utcNow, uint value)
			{
				if (utcNow < this.ExpireAt)
				{
					this.value += value;
					return true;
				}
				return false;
			}

			// Token: 0x1700081A RID: 2074
			// (get) Token: 0x06001E8C RID: 7820 RVA: 0x00038F7F File Offset: 0x0003717F
			public uint Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04001E5B RID: 7771
			private volatile uint value;
		}
	}
}
