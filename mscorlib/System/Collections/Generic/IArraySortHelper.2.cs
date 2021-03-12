using System;

namespace System.Collections.Generic
{
	// Token: 0x020004B4 RID: 1204
	internal interface IArraySortHelper<TKey, TValue>
	{
		// Token: 0x06003A35 RID: 14901
		void Sort(TKey[] keys, TValue[] values, int index, int length, IComparer<TKey> comparer);
	}
}
