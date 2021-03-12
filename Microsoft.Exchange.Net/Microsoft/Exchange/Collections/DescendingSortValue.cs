using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000035 RID: 53
	public struct DescendingSortValue<T> : ISortKey<DescendingSortValue<T>>, IComparable<DescendingSortValue<T>> where T : IComparable<T>
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00006BB8 File Offset: 0x00004DB8
		public DescendingSortValue(T item)
		{
			this.item = item;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006BC1 File Offset: 0x00004DC1
		public T Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006BC9 File Offset: 0x00004DC9
		public DescendingSortValue<T> SortKey
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006BD1 File Offset: 0x00004DD1
		public int CompareTo(DescendingSortValue<T> otherItem)
		{
			return Comparer<T>.Default.Compare(otherItem.item, this.item);
		}

		// Token: 0x040000F7 RID: 247
		private readonly T item;
	}
}
