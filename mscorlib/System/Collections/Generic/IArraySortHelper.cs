using System;

namespace System.Collections.Generic
{
	// Token: 0x020004B0 RID: 1200
	internal interface IArraySortHelper<TKey>
	{
		// Token: 0x06003A15 RID: 14869
		void Sort(TKey[] keys, int index, int length, IComparer<TKey> comparer);

		// Token: 0x06003A16 RID: 14870
		int BinarySearch(TKey[] keys, int index, int length, TKey value, IComparer<TKey> comparer);
	}
}
