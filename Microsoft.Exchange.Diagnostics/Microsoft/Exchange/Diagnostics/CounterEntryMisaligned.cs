using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000148 RID: 328
	public class CounterEntryMisaligned : CounterEntry
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x000233C4 File Offset: 0x000215C4
		public CounterEntryMisaligned(IntPtr handle, int offset) : this(CounterEntryMisaligned.GetInternalCounterEntry(handle, offset), handle)
		{
			this.offset = offset;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000233DB File Offset: 0x000215DB
		private unsafe CounterEntryMisaligned(InternalCounterEntryMisaligned* internalCounterEntry, IntPtr handle)
		{
			if (null == internalCounterEntry)
			{
				throw new ArgumentNullException("internalCategoryEntry");
			}
			this.internalCounterEntry = internalCounterEntry;
			this.Initialize(handle);
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00023404 File Offset: 0x00021604
		public unsafe override long Value
		{
			get
			{
				long num = (long)this.internalCounterEntry->Value_hi << 32;
				return num + (long)this.internalCounterEntry->Value_lo;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00023431 File Offset: 0x00021631
		public unsafe override int SpinLock
		{
			get
			{
				return this.internalCounterEntry->SpinLock;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0002343E File Offset: 0x0002163E
		public override int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00023446 File Offset: 0x00021646
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x00023453 File Offset: 0x00021653
		public unsafe override int NameOffset
		{
			get
			{
				return this.internalCounterEntry->CounterNameOffset;
			}
			set
			{
				this.internalCounterEntry->CounterNameOffset = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00023461 File Offset: 0x00021661
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x0002346E File Offset: 0x0002166E
		public unsafe override int NextCounterOffset
		{
			get
			{
				return this.internalCounterEntry->NextCounterOffset;
			}
			set
			{
				this.internalCounterEntry->NextCounterOffset = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0002347C File Offset: 0x0002167C
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00023489 File Offset: 0x00021689
		public unsafe override int LifetimeOffset
		{
			get
			{
				return this.internalCounterEntry->LifetimeOffset;
			}
			set
			{
				this.internalCounterEntry->LifetimeOffset = value;
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00023497 File Offset: 0x00021697
		private unsafe static InternalCounterEntryMisaligned* GetInternalCounterEntry(IntPtr handle, int offset)
		{
			return (long)handle / (long)sizeof(InternalCounterEntryMisaligned) + offset;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000234A4 File Offset: 0x000216A4
		private unsafe void Initialize(IntPtr handle)
		{
			if (this.internalCounterEntry->LifetimeOffset != 0)
			{
				this.lifetimeEntry = new LifetimeEntry(handle, this.internalCounterEntry->LifetimeOffset);
			}
			this.name = new string((long)handle / 2L + this.internalCounterEntry->CounterNameOffset);
		}

		// Token: 0x04000662 RID: 1634
		private unsafe InternalCounterEntryMisaligned* internalCounterEntry;

		// Token: 0x04000663 RID: 1635
		private int offset;
	}
}
