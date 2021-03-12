using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000147 RID: 327
	public class CounterEntryAligned : CounterEntry
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x000232B7 File Offset: 0x000214B7
		public CounterEntryAligned(IntPtr handle, int offset) : this(CounterEntryAligned.GetInternalCounterEntry(handle, offset), handle)
		{
			this.offset = offset;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000232CE File Offset: 0x000214CE
		[CLSCompliant(false)]
		public unsafe CounterEntryAligned(InternalCounterEntryAligned* internalCounterEntry, IntPtr handle)
		{
			if (null == internalCounterEntry)
			{
				throw new ArgumentNullException("internalCategoryEntry");
			}
			this.internalCounterEntry = internalCounterEntry;
			this.Initialize(handle);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x000232F4 File Offset: 0x000214F4
		public unsafe override long Value
		{
			get
			{
				return this.internalCounterEntry->Value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00023301 File Offset: 0x00021501
		public unsafe override int SpinLock
		{
			get
			{
				return this.internalCounterEntry->SpinLock;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0002330E File Offset: 0x0002150E
		public override int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00023316 File Offset: 0x00021516
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x00023323 File Offset: 0x00021523
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

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00023331 File Offset: 0x00021531
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0002333E File Offset: 0x0002153E
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

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0002334C File Offset: 0x0002154C
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x00023359 File Offset: 0x00021559
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

		// Token: 0x06000945 RID: 2373 RVA: 0x00023367 File Offset: 0x00021567
		private unsafe static InternalCounterEntryAligned* GetInternalCounterEntry(IntPtr handle, int offset)
		{
			return (long)handle / (long)sizeof(InternalCounterEntryAligned) + offset;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00023374 File Offset: 0x00021574
		private unsafe void Initialize(IntPtr handle)
		{
			if (this.internalCounterEntry->LifetimeOffset != 0)
			{
				this.lifetimeEntry = new LifetimeEntry(handle, this.internalCounterEntry->LifetimeOffset);
			}
			this.name = new string((long)handle / 2L + this.internalCounterEntry->CounterNameOffset);
		}

		// Token: 0x04000660 RID: 1632
		private unsafe InternalCounterEntryAligned* internalCounterEntry;

		// Token: 0x04000661 RID: 1633
		private int offset;
	}
}
