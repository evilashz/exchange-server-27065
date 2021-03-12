using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200002E RID: 46
	public class RopeList<T>
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000D458 File Offset: 0x0000B658
		public RopeList(int contiguousCapacity = 1024)
		{
			this.contiguousCapacity = contiguousCapacity;
			this.items.Add(new T[this.contiguousCapacity]);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000D488 File Offset: 0x0000B688
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000082 RID: 130
		public T this[int index]
		{
			get
			{
				return this.items[index / this.contiguousCapacity][index % this.contiguousCapacity];
			}
			set
			{
				this.items[index / this.contiguousCapacity][index % this.contiguousCapacity] = value;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		public void Add(T item)
		{
			if ((this.count + 1) / this.contiguousCapacity >= this.items.Count)
			{
				this.items.Add(new T[this.contiguousCapacity]);
			}
			this[this.count] = item;
			this.count++;
		}

		// Token: 0x04000107 RID: 263
		private readonly int contiguousCapacity;

		// Token: 0x04000108 RID: 264
		private List<T[]> items = new List<T[]>();

		// Token: 0x04000109 RID: 265
		private int count;
	}
}
