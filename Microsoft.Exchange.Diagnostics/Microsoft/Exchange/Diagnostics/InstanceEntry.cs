using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000145 RID: 325
	public class InstanceEntry
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x00022FC3 File Offset: 0x000211C3
		public InstanceEntry(IntPtr handle, int offset) : this(InstanceEntry.GetInternalInstanceEntry(handle, offset), handle)
		{
			this.offset = offset;
			this.InitializeNextInstance(handle);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00022FE1 File Offset: 0x000211E1
		private unsafe InstanceEntry(InternalInstanceEntry* internalIntancesEntry, IntPtr handle)
		{
			if (null == internalIntancesEntry)
			{
				throw new ArgumentNullException("internalCategoryEntry");
			}
			this.internalInstanceEntry = internalIntancesEntry;
			this.Initialize(handle);
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00023007 File Offset: 0x00021207
		public InstanceEntry Next
		{
			get
			{
				return this.nextInstanceEntry;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002300F File Offset: 0x0002120F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00023017 File Offset: 0x00021217
		public CounterEntry FirstCounter
		{
			get
			{
				return this.counterEntry;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0002301F File Offset: 0x0002121F
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0002302C File Offset: 0x0002122C
		public unsafe int RefCount
		{
			get
			{
				return this.internalInstanceEntry->RefCount;
			}
			set
			{
				this.internalInstanceEntry->RefCount = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0002303A File Offset: 0x0002123A
		public unsafe int SpinLock
		{
			get
			{
				return this.internalInstanceEntry->SpinLock;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00023047 File Offset: 0x00021247
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0002304F File Offset: 0x0002124F
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x0002305C File Offset: 0x0002125C
		public unsafe int NameOffset
		{
			get
			{
				return this.internalInstanceEntry->InstanceNameOffset;
			}
			set
			{
				this.internalInstanceEntry->InstanceNameOffset = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0002306A File Offset: 0x0002126A
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x00023077 File Offset: 0x00021277
		public unsafe int FirstCounterOffset
		{
			get
			{
				return this.internalInstanceEntry->FirstCounterOffset;
			}
			set
			{
				this.internalInstanceEntry->FirstCounterOffset = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00023085 File Offset: 0x00021285
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x00023092 File Offset: 0x00021292
		public unsafe int NextInstanceOffset
		{
			get
			{
				return this.internalInstanceEntry->NextInstanceOffset;
			}
			set
			{
				this.internalInstanceEntry->NextInstanceOffset = value;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000230A0 File Offset: 0x000212A0
		public unsafe override string ToString()
		{
			return string.Format("{0}({1:X}) RefCount={2} SpinLock={3} Offset={4}", new object[]
			{
				this.Name,
				this.internalInstanceEntry->InstanceNameHashCode,
				this.RefCount,
				this.SpinLock,
				this.Offset
			});
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00023105 File Offset: 0x00021305
		private unsafe static InternalInstanceEntry* GetInternalInstanceEntry(IntPtr handle, int offset)
		{
			return (long)handle / (long)sizeof(InternalInstanceEntry) + offset;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00023114 File Offset: 0x00021314
		private unsafe void Initialize(IntPtr handle)
		{
			if (this.internalInstanceEntry->FirstCounterOffset != 0)
			{
				this.counterEntry = CounterEntry.GetCounterEntry(handle, this.internalInstanceEntry->FirstCounterOffset);
			}
			this.name = new string((long)handle / 2L + this.internalInstanceEntry->InstanceNameOffset);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00023164 File Offset: 0x00021364
		private unsafe void InitializeNextInstance(IntPtr handle)
		{
			InstanceEntry instanceEntry = this;
			while (instanceEntry.internalInstanceEntry->NextInstanceOffset != 0)
			{
				InstanceEntry instanceEntry2 = new InstanceEntry(InstanceEntry.GetInternalInstanceEntry(handle, instanceEntry.internalInstanceEntry->NextInstanceOffset), handle);
				instanceEntry2.offset = instanceEntry.internalInstanceEntry->NextInstanceOffset;
				instanceEntry.nextInstanceEntry = instanceEntry2;
				instanceEntry = instanceEntry2;
			}
		}

		// Token: 0x04000658 RID: 1624
		private unsafe InternalInstanceEntry* internalInstanceEntry;

		// Token: 0x04000659 RID: 1625
		private InstanceEntry nextInstanceEntry;

		// Token: 0x0400065A RID: 1626
		private string name;

		// Token: 0x0400065B RID: 1627
		private CounterEntry counterEntry;

		// Token: 0x0400065C RID: 1628
		private int offset;
	}
}
