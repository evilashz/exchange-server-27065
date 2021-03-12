using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000049 RID: 73
	internal class MruEqualityComparer<TKey> : IEqualityComparer<TKey>
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000984E File Offset: 0x00007A4E
		public MruEqualityComparer(IComparer<TKey> comparer)
		{
			this.comparer = comparer;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000985D File Offset: 0x00007A5D
		public bool Equals(TKey x, TKey y)
		{
			return this.comparer.Compare(x, y) == 0;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000986F File Offset: 0x00007A6F
		public int GetHashCode(TKey obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x04000147 RID: 327
		private IComparer<TKey> comparer;
	}
}
