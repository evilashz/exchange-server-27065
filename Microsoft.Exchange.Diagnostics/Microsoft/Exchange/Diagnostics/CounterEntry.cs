using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000146 RID: 326
	public abstract class CounterEntry
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x000231B4 File Offset: 0x000213B4
		public CounterEntry Next
		{
			get
			{
				return this.nextCounterEntry;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x000231BC File Offset: 0x000213BC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x000231C4 File Offset: 0x000213C4
		public LifetimeEntry Lifetime
		{
			get
			{
				return this.lifetimeEntry;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600092C RID: 2348
		public abstract long Value { get; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600092D RID: 2349
		public abstract int SpinLock { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600092E RID: 2350
		public abstract int Offset { get; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600092F RID: 2351
		// (set) Token: 0x06000930 RID: 2352
		public abstract int NameOffset { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000931 RID: 2353
		// (set) Token: 0x06000932 RID: 2354
		public abstract int NextCounterOffset { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000933 RID: 2355
		// (set) Token: 0x06000934 RID: 2356
		public abstract int LifetimeOffset { get; set; }

		// Token: 0x06000935 RID: 2357 RVA: 0x000231CC File Offset: 0x000213CC
		public static CounterEntry GetCounterEntry(IntPtr handle, int offset)
		{
			CounterEntry counterEntry = CounterEntry.InternalGetCounterEntry(handle, offset);
			counterEntry.InitializeNextCounter(handle);
			return counterEntry;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000231EC File Offset: 0x000213EC
		public override string ToString()
		{
			if (this.Lifetime == null)
			{
				return string.Format("{0} Value={1} SpinLock={2}", this.Name, this.Value, this.SpinLock);
			}
			return string.Format("{0} Value={1} SpinLock={2} Lifetime={3}", new object[]
			{
				this.Name,
				this.Value,
				this.SpinLock,
				this.Lifetime
			});
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00023268 File Offset: 0x00021468
		private static CounterEntry InternalGetCounterEntry(IntPtr handle, int offset)
		{
			if (offset % 8 == 0)
			{
				return new CounterEntryAligned(handle, offset);
			}
			return new CounterEntryMisaligned(handle, offset);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00023280 File Offset: 0x00021480
		private void InitializeNextCounter(IntPtr handle)
		{
			CounterEntry counterEntry = this;
			while (counterEntry.NextCounterOffset != 0)
			{
				CounterEntry counterEntry2 = CounterEntry.InternalGetCounterEntry(handle, counterEntry.NextCounterOffset);
				counterEntry.nextCounterEntry = counterEntry2;
				counterEntry = counterEntry2;
			}
		}

		// Token: 0x0400065D RID: 1629
		[CLSCompliant(false)]
		protected CounterEntry nextCounterEntry;

		// Token: 0x0400065E RID: 1630
		[CLSCompliant(false)]
		protected string name;

		// Token: 0x0400065F RID: 1631
		[CLSCompliant(false)]
		protected LifetimeEntry lifetimeEntry;
	}
}
