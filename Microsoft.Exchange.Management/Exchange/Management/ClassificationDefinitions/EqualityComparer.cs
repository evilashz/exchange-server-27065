using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000821 RID: 2081
	internal class EqualityComparer<T> : IEqualityComparer<!0>
	{
		// Token: 0x06004801 RID: 18433 RVA: 0x00127D17 File Offset: 0x00125F17
		internal EqualityComparer(Func<T, T, bool> equalityComparerFn, Func<T, int> hasherFn)
		{
			this.equalityComparerFunc = equalityComparerFn;
			this.hashCodeFunc = hasherFn;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x00127D2D File Offset: 0x00125F2D
		public bool Equals(T x, T y)
		{
			return this.equalityComparerFunc(x, y);
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x00127D3C File Offset: 0x00125F3C
		public int GetHashCode(T obj)
		{
			return this.hashCodeFunc(obj);
		}

		// Token: 0x04002BE0 RID: 11232
		private readonly Func<T, T, bool> equalityComparerFunc;

		// Token: 0x04002BE1 RID: 11233
		private readonly Func<T, int> hashCodeFunc;
	}
}
