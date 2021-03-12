using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200002C RID: 44
	internal class LowAllocSet
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000D254 File Offset: 0x0000B454
		internal LowAllocSet(int initialCapacity = 1024)
		{
			this.primaryTable = new List<long>(initialCapacity);
			for (int i = 0; i < initialCapacity; i++)
			{
				this.primaryTable.Add(long.MaxValue);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000D293 File Offset: 0x0000B493
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		public IEnumerable<long> Values
		{
			get
			{
				if (this.secondaryTable != null)
				{
					return (from value in this.primaryTable
					where value != long.MaxValue
					select value).Concat(this.secondaryTable.Keys);
				}
				return from value in this.primaryTable
				where value != long.MaxValue
				select value;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000D338 File Offset: 0x0000B538
		public void Add(long value)
		{
			int hash = this.GetHash(value);
			if (this.primaryTable[hash] == 9223372036854775807L)
			{
				this.primaryTable[hash] = value;
				this.count++;
				return;
			}
			if (this.primaryTable[hash] != value && (this.secondaryTable == null || !this.secondaryTable.ContainsKey(value)))
			{
				if (this.secondaryTable == null)
				{
					this.secondaryTable = new Dictionary<long, bool>(64);
				}
				this.secondaryTable.Add(value, true);
				this.count++;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		public bool Contains(long value)
		{
			int hash = this.GetHash(value);
			return this.primaryTable[hash] != long.MaxValue && (this.primaryTable[hash] == value || (this.secondaryTable != null && this.secondaryTable.ContainsKey(value)));
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000D42D File Offset: 0x0000B62D
		private int GetHash(long value)
		{
			return Math.Abs((int)value ^ (int)(value >> 32)) % this.primaryTable.Count;
		}

		// Token: 0x040000FF RID: 255
		private const long EmptyCellValue = 9223372036854775807L;

		// Token: 0x04000100 RID: 256
		private Dictionary<long, bool> secondaryTable;

		// Token: 0x04000101 RID: 257
		private List<long> primaryTable;

		// Token: 0x04000102 RID: 258
		private int count;
	}
}
