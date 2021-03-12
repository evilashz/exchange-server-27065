using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000020 RID: 32
	internal class TimePeriodObjectPool<TValue> where TValue : IInitialize, new()
	{
		// Token: 0x06000146 RID: 326 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		public TimePeriodObjectPool()
		{
			this.internalPools = new ConcurrentBag<TValue>[2];
			this.internalPools[0] = new ConcurrentBag<TValue>();
			this.internalPools[1] = new ConcurrentBag<TValue>();
			this.TimeStamps = new DateTime[2];
			this.TimeStamps[0] = DateTime.UtcNow;
			this.TimeStamps[1] = DateTime.UtcNow;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000C061 File Offset: 0x0000A261
		// (set) Token: 0x06000148 RID: 328 RVA: 0x0000C069 File Offset: 0x0000A269
		public DateTime[] TimeStamps { get; set; }

		// Token: 0x06000149 RID: 329 RVA: 0x0000C074 File Offset: 0x0000A274
		public void Clear(int i, DateTime dateTime)
		{
			if (i != 0 && i != 1)
			{
				throw new ArgumentException("Parameter i allow only 0 or 1.", "i");
			}
			ConcurrentBag<TValue> value = new ConcurrentBag<TValue>();
			Interlocked.CompareExchange<ConcurrentBag<TValue>>(ref this.internalPools[i], value, this.internalPools[i]);
			this.TimeStamps[i] = dateTime;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		public TValue GetObject(int i)
		{
			ConcurrentBag<TValue> concurrentBag = this.internalPools[i];
			ConcurrentBag<TValue> concurrentBag2 = this.internalPools[1 - i];
			TValue tvalue;
			if (concurrentBag2.TryTake(out tvalue))
			{
				tvalue.Initialize();
				concurrentBag.Add(tvalue);
				return tvalue;
			}
			if (default(TValue) != null)
			{
				return default(TValue);
			}
			return Activator.CreateInstance<TValue>();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000C12D File Offset: 0x0000A32D
		public void Add(int i, TValue value)
		{
			if (i != 0 && i != 1)
			{
				throw new ArgumentException("Parameter i allow only 0 or 1.", "i");
			}
			this.internalPools[i].Add(value);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000C154 File Offset: 0x0000A354
		internal int GetInternalPoolSize(int i)
		{
			return this.internalPools[i].Count;
		}

		// Token: 0x040000D9 RID: 217
		private ConcurrentBag<TValue>[] internalPools;
	}
}
