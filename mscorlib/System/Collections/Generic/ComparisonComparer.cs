using System;

namespace System.Collections.Generic
{
	// Token: 0x02000491 RID: 1169
	[Serializable]
	internal class ComparisonComparer<T> : Comparer<T>
	{
		// Token: 0x06003918 RID: 14616 RVA: 0x000D94B6 File Offset: 0x000D76B6
		public ComparisonComparer(Comparison<T> comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000D94C5 File Offset: 0x000D76C5
		public override int Compare(T x, T y)
		{
			return this._comparison(x, y);
		}

		// Token: 0x04001886 RID: 6278
		private readonly Comparison<T> _comparison;
	}
}
