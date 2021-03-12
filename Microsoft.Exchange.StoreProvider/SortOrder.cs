using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200024D RID: 589
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SortOrder : IEquatable<SortOrder>
	{
		// Token: 0x06000A70 RID: 2672 RVA: 0x00032DD7 File Offset: 0x00030FD7
		public SortOrder()
		{
			this.sorts = new List<SortOrder.Sort>();
			this.expandCount = 0;
			this.categoriesCount = 0;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00032DF8 File Offset: 0x00030FF8
		public SortOrder(PropTag propTag, SortFlags sortFlag) : this()
		{
			this.Add(propTag, sortFlag);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00032E08 File Offset: 0x00031008
		public unsafe SortOrder(byte[] sortosBlob) : this()
		{
			if (sortosBlob.Length < SSortOrderSet.SizeOf)
			{
				MapiExceptionHelper.ThrowIfError("Invalid SORTOS blob", -2147024809);
			}
			fixed (byte* ptr = sortosBlob)
			{
				SSortOrderSet* ptr2 = (SSortOrderSet*)ptr;
				if (sortosBlob.Length < (int)Marshal.OffsetOf(typeof(SSortOrderSet), "aSorts") + ptr2->cSorts * SSortOrder.SizeOf)
				{
					MapiExceptionHelper.ThrowIfError("Invalid SORTOS blob", -2147024809);
				}
				SSortOrder* ptr3 = &ptr2->aSorts;
				for (int i = 0; i < ptr2->cSorts; i++)
				{
					if (this.categoriesCount < ptr2->cCategories)
					{
						this.AddCategory((PropTag)ptr3[i].ulPropTag, (SortFlags)ptr3[i].ulOrder);
						if (i + 1 < ptr2->cSorts && (ptr3[i + 1].ulOrder == 4 || ptr3[i + 1].ulOrder == 8))
						{
							this.Add((PropTag)ptr3[i + 1].ulPropTag, (SortFlags)ptr3[i + 1].ulOrder);
							i++;
						}
					}
					else
					{
						this.Add((PropTag)ptr3[i].ulPropTag, (SortFlags)ptr3[i].ulOrder);
					}
				}
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00032F72 File Offset: 0x00031172
		public int CategoriesCount
		{
			get
			{
				return this.categoriesCount;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00032F7A File Offset: 0x0003117A
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x00032F82 File Offset: 0x00031182
		public int ExpandCount
		{
			get
			{
				return this.expandCount;
			}
			set
			{
				this.expandCount = value;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00032F8B File Offset: 0x0003118B
		public void Add(PropTag propTag, SortFlags sortFlag)
		{
			this.sorts.Add(new SortOrder.Sort(propTag, sortFlag));
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00032F9F File Offset: 0x0003119F
		public void AddCategory(PropTag propTag, SortFlags sortFlag)
		{
			this.sorts.Add(new SortOrder.Sort(propTag, sortFlag));
			this.categoriesCount++;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00032FC1 File Offset: 0x000311C1
		public int GetSortCount()
		{
			return this.sorts.Count;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00032FD0 File Offset: 0x000311D0
		public void EnumerateSortOrder(SortOrder.EnumSortOrderDelegate del, object ctx)
		{
			int num = 0;
			for (int i = 0; i < this.sorts.Count; i++)
			{
				if (num < this.categoriesCount)
				{
					del(this.sorts[i].PropertyTag, this.sorts[i].Direction, true, ctx);
					if (i + 1 < this.sorts.Count && (this.sorts[i + 1].Direction == SortFlags.CategoryMax || this.sorts[i + 1].Direction == SortFlags.CategoryMin))
					{
						del(this.sorts[i + 1].PropertyTag, this.sorts[i + 1].Direction, false, ctx);
						i++;
					}
					num++;
				}
				else
				{
					del(this.sorts[i].PropertyTag, this.sorts[i].Direction, false, ctx);
				}
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000330ED File Offset: 0x000312ED
		public override bool Equals(object comparand)
		{
			return comparand is SortOrder && this.Equals((SortOrder)comparand);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00033105 File Offset: 0x00031305
		public bool Equals(SortOrder comparand)
		{
			return this.IsEqualTo(comparand);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0003310E File Offset: 0x0003130E
		public static bool Equals(SortOrder v1, SortOrder v2)
		{
			return v1.Equals(v2);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00033117 File Offset: 0x00031317
		public override int GetHashCode()
		{
			return this.sorts.GetHashCode() + this.categoriesCount;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0003312C File Offset: 0x0003132C
		internal bool IsEqualTo(SortOrder other)
		{
			if (this.categoriesCount != other.categoriesCount || this.ExpandCount != other.ExpandCount)
			{
				return false;
			}
			if (this.sorts.Count != other.sorts.Count)
			{
				return false;
			}
			for (int i = 0; i < this.sorts.Count; i++)
			{
				if (this.sorts[i].PropertyTag != other.sorts[i].PropertyTag || this.sorts[i].Direction != other.sorts[i].Direction)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000331DF File Offset: 0x000313DF
		internal int GetBytesToMarshal()
		{
			return SSortOrderSet.SizeOf + this.GetSortCount() * SSortOrder.SizeOf;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000331F4 File Offset: 0x000313F4
		internal unsafe void MarshalToNative(SSortOrderSet* pssortset)
		{
			pssortset->cSorts = this.sorts.Count;
			pssortset->cCategories = this.categoriesCount;
			pssortset->cExpanded = this.ExpandCount;
			SSortOrder* ptr = &pssortset->aSorts;
			int i = 0;
			while (i < this.sorts.Count)
			{
				ptr->ulPropTag = (int)this.sorts[i].PropertyTag;
				ptr->ulOrder = (int)this.sorts[i].Direction;
				i++;
				ptr++;
			}
		}

		// Token: 0x04001052 RID: 4178
		private readonly List<SortOrder.Sort> sorts;

		// Token: 0x04001053 RID: 4179
		private int expandCount;

		// Token: 0x04001054 RID: 4180
		private int categoriesCount;

		// Token: 0x0200024E RID: 590
		private struct Sort
		{
			// Token: 0x06000A81 RID: 2689 RVA: 0x00033285 File Offset: 0x00031485
			public Sort(PropTag propertyTag, SortFlags direction)
			{
				this.propertyTag = propertyTag;
				this.direction = direction;
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00033295 File Offset: 0x00031495
			public PropTag PropertyTag
			{
				get
				{
					return this.propertyTag;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0003329D File Offset: 0x0003149D
			public SortFlags Direction
			{
				get
				{
					return this.direction;
				}
			}

			// Token: 0x04001055 RID: 4181
			private readonly PropTag propertyTag;

			// Token: 0x04001056 RID: 4182
			private readonly SortFlags direction;
		}

		// Token: 0x0200024F RID: 591
		// (Invoke) Token: 0x06000A85 RID: 2693
		public delegate void EnumSortOrderDelegate(PropTag ptag, SortFlags flags, bool isCategory, object ctx);
	}
}
