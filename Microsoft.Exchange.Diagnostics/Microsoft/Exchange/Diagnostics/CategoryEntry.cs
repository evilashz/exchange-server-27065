using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000144 RID: 324
	public class CategoryEntry
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x00022DF6 File Offset: 0x00020FF6
		public CategoryEntry(IntPtr handle, int offset) : this(CategoryEntry.GetInternalCategoryEntry(handle, offset), handle)
		{
			this.offset = offset;
			this.InitializeNextCategory(handle);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00022E14 File Offset: 0x00021014
		private unsafe CategoryEntry(InternalCategoryEntry* internalCategoryEntry, IntPtr handle)
		{
			if (null == internalCategoryEntry)
			{
				throw new ArgumentNullException("internalCategoryEntry");
			}
			this.internalCategoryEntry = internalCategoryEntry;
			this.Initialize(handle);
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00022E3A File Offset: 0x0002103A
		public CategoryEntry Next
		{
			get
			{
				return this.nextCategoryEntry;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00022E42 File Offset: 0x00021042
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00022E4A File Offset: 0x0002104A
		public InstanceEntry FirstInstance
		{
			get
			{
				return this.instanceEntry;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00022E52 File Offset: 0x00021052
		public unsafe int SpinLock
		{
			get
			{
				return this.internalCategoryEntry->SpinLock;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00022E5F File Offset: 0x0002105F
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00022E67 File Offset: 0x00021067
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x00022E74 File Offset: 0x00021074
		public unsafe int NameOffset
		{
			get
			{
				return this.internalCategoryEntry->CategoryNameOffset;
			}
			set
			{
				this.internalCategoryEntry->CategoryNameOffset = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00022E82 File Offset: 0x00021082
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x00022E8F File Offset: 0x0002108F
		public unsafe int FirstInstanceOffset
		{
			get
			{
				return this.internalCategoryEntry->FirstInstanceOffset;
			}
			set
			{
				this.internalCategoryEntry->FirstInstanceOffset = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00022E9D File Offset: 0x0002109D
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x00022EAA File Offset: 0x000210AA
		public unsafe int NextCategoryOffset
		{
			get
			{
				return this.internalCategoryEntry->NextCategoryOffset;
			}
			set
			{
				this.internalCategoryEntry->NextCategoryOffset = value;
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00022EB8 File Offset: 0x000210B8
		public unsafe void Refresh()
		{
			this.Initialize((IntPtr)((void*)(this.internalCategoryEntry - this.offset / sizeof(InternalCategoryEntry))));
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00022ED2 File Offset: 0x000210D2
		public unsafe void RefreshInstances(IntPtr handle)
		{
			if (this.internalCategoryEntry->FirstInstanceOffset != 0)
			{
				this.instanceEntry = new InstanceEntry(handle, this.internalCategoryEntry->FirstInstanceOffset);
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00022EF8 File Offset: 0x000210F8
		public unsafe override string ToString()
		{
			return string.Format("{0}({1:X}) SpinLock={2}", this.Name, this.internalCategoryEntry->CategoryNameHashCode, this.SpinLock);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00022F25 File Offset: 0x00021125
		private unsafe static InternalCategoryEntry* GetInternalCategoryEntry(IntPtr handle, int offset)
		{
			return (long)handle / (long)sizeof(InternalCategoryEntry) + offset;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00022F34 File Offset: 0x00021134
		private unsafe void Initialize(IntPtr handle)
		{
			if (this.internalCategoryEntry->FirstInstanceOffset != 0)
			{
				this.instanceEntry = new InstanceEntry(handle, this.internalCategoryEntry->FirstInstanceOffset);
			}
			this.name = new string((long)handle / 2L + this.internalCategoryEntry->CategoryNameOffset);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00022F84 File Offset: 0x00021184
		private unsafe void InitializeNextCategory(IntPtr handle)
		{
			CategoryEntry categoryEntry = this;
			while (categoryEntry.internalCategoryEntry->NextCategoryOffset != 0)
			{
				CategoryEntry categoryEntry2 = new CategoryEntry(CategoryEntry.GetInternalCategoryEntry(handle, categoryEntry.internalCategoryEntry->NextCategoryOffset), handle);
				categoryEntry.nextCategoryEntry = categoryEntry2;
				categoryEntry = categoryEntry2;
			}
		}

		// Token: 0x04000653 RID: 1619
		private unsafe InternalCategoryEntry* internalCategoryEntry;

		// Token: 0x04000654 RID: 1620
		private CategoryEntry nextCategoryEntry;

		// Token: 0x04000655 RID: 1621
		private string name;

		// Token: 0x04000656 RID: 1622
		private InstanceEntry instanceEntry;

		// Token: 0x04000657 RID: 1623
		private int offset;
	}
}
