﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004B5 RID: 1205
	[TypeDependency("System.Collections.Generic.GenericArraySortHelper`2")]
	internal class ArraySortHelper<TKey, TValue> : IArraySortHelper<TKey, TValue>
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000DCC94 File Offset: 0x000DAE94
		public static IArraySortHelper<TKey, TValue> Default
		{
			get
			{
				IArraySortHelper<TKey, TValue> arraySortHelper = ArraySortHelper<TKey, TValue>.defaultArraySortHelper;
				if (arraySortHelper == null)
				{
					arraySortHelper = ArraySortHelper<TKey, TValue>.CreateArraySortHelper();
				}
				return arraySortHelper;
			}
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000DCCB4 File Offset: 0x000DAEB4
		[SecuritySafeCritical]
		private static IArraySortHelper<TKey, TValue> CreateArraySortHelper()
		{
			if (typeof(IComparable<TKey>).IsAssignableFrom(typeof(TKey)))
			{
				ArraySortHelper<TKey, TValue>.defaultArraySortHelper = (IArraySortHelper<TKey, TValue>)RuntimeTypeHandle.Allocate(typeof(GenericArraySortHelper<string, string>).TypeHandle.Instantiate(new Type[]
				{
					typeof(TKey),
					typeof(TValue)
				}));
			}
			else
			{
				ArraySortHelper<TKey, TValue>.defaultArraySortHelper = new ArraySortHelper<TKey, TValue>();
			}
			return ArraySortHelper<TKey, TValue>.defaultArraySortHelper;
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x000DCD3C File Offset: 0x000DAF3C
		public void Sort(TKey[] keys, TValue[] values, int index, int length, IComparer<TKey> comparer)
		{
			try
			{
				if (comparer == null || comparer == Comparer<TKey>.Default)
				{
					comparer = Comparer<TKey>.Default;
				}
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					ArraySortHelper<TKey, TValue>.IntrospectiveSort(keys, values, index, length, comparer);
				}
				else
				{
					ArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, index, length + index - 1, comparer, 32);
				}
			}
			catch (IndexOutOfRangeException)
			{
				IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(comparer);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException);
			}
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000DCDC0 File Offset: 0x000DAFC0
		private static void SwapIfGreaterWithItems(TKey[] keys, TValue[] values, IComparer<TKey> comparer, int a, int b)
		{
			if (a != b && comparer.Compare(keys[a], keys[b]) > 0)
			{
				TKey tkey = keys[a];
				keys[a] = keys[b];
				keys[b] = tkey;
				if (values != null)
				{
					TValue tvalue = values[a];
					values[a] = values[b];
					values[b] = tvalue;
				}
			}
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000DCE30 File Offset: 0x000DB030
		private static void Swap(TKey[] keys, TValue[] values, int i, int j)
		{
			if (i != j)
			{
				TKey tkey = keys[i];
				keys[i] = keys[j];
				keys[j] = tkey;
				if (values != null)
				{
					TValue tvalue = values[i];
					values[i] = values[j];
					values[j] = tvalue;
				}
			}
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000DCE80 File Offset: 0x000DB080
		internal static void DepthLimitedQuickSort(TKey[] keys, TValue[] values, int left, int right, IComparer<TKey> comparer, int depthLimit)
		{
			while (depthLimit != 0)
			{
				int num = left;
				int num2 = right;
				int num3 = num + (num2 - num >> 1);
				ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, num, num3);
				ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, num, num2);
				ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, num3, num2);
				TKey tkey = keys[num3];
				for (;;)
				{
					if (comparer.Compare(keys[num], tkey) >= 0)
					{
						while (comparer.Compare(tkey, keys[num2]) < 0)
						{
							num2--;
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							TKey tkey2 = keys[num];
							keys[num] = keys[num2];
							keys[num2] = tkey2;
							if (values != null)
							{
								TValue tvalue = values[num];
								values[num] = values[num2];
								values[num2] = tvalue;
							}
						}
						num++;
						num2--;
						if (num > num2)
						{
							break;
						}
					}
					else
					{
						num++;
					}
				}
				depthLimit--;
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						ArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, left, num2, comparer, depthLimit);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						ArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, num, right, comparer, depthLimit);
					}
					right = num2;
				}
				if (left >= right)
				{
					return;
				}
			}
			ArraySortHelper<TKey, TValue>.Heapsort(keys, values, left, right, comparer);
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000DCF9B File Offset: 0x000DB19B
		internal static void IntrospectiveSort(TKey[] keys, TValue[] values, int left, int length, IComparer<TKey> comparer)
		{
			if (length < 2)
			{
				return;
			}
			ArraySortHelper<TKey, TValue>.IntroSort(keys, values, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(keys.Length), comparer);
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000DCFBC File Offset: 0x000DB1BC
		private static void IntroSort(TKey[] keys, TValue[] values, int lo, int hi, int depthLimit, IComparer<TKey> comparer)
		{
			while (hi > lo)
			{
				int num = hi - lo + 1;
				if (num <= 16)
				{
					if (num == 1)
					{
						return;
					}
					if (num == 2)
					{
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi);
						return;
					}
					if (num == 3)
					{
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi - 1);
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi);
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, hi - 1, hi);
						return;
					}
					ArraySortHelper<TKey, TValue>.InsertionSort(keys, values, lo, hi, comparer);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						ArraySortHelper<TKey, TValue>.Heapsort(keys, values, lo, hi, comparer);
						return;
					}
					depthLimit--;
					int num2 = ArraySortHelper<TKey, TValue>.PickPivotAndPartition(keys, values, lo, hi, comparer);
					ArraySortHelper<TKey, TValue>.IntroSort(keys, values, num2 + 1, hi, depthLimit, comparer);
					hi = num2 - 1;
				}
			}
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000DD064 File Offset: 0x000DB264
		private static int PickPivotAndPartition(TKey[] keys, TValue[] values, int lo, int hi, IComparer<TKey> comparer)
		{
			int num = lo + (hi - lo) / 2;
			ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, num);
			ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi);
			ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, num, hi);
			TKey tkey = keys[num];
			ArraySortHelper<TKey, TValue>.Swap(keys, values, num, hi - 1);
			int i = lo;
			int num2 = hi - 1;
			while (i < num2)
			{
				while (comparer.Compare(keys[++i], tkey) < 0)
				{
				}
				while (comparer.Compare(tkey, keys[--num2]) < 0)
				{
				}
				if (i >= num2)
				{
					break;
				}
				ArraySortHelper<TKey, TValue>.Swap(keys, values, i, num2);
			}
			ArraySortHelper<TKey, TValue>.Swap(keys, values, i, hi - 1);
			return i;
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000DD100 File Offset: 0x000DB300
		private static void Heapsort(TKey[] keys, TValue[] values, int lo, int hi, IComparer<TKey> comparer)
		{
			int num = hi - lo + 1;
			for (int i = num / 2; i >= 1; i--)
			{
				ArraySortHelper<TKey, TValue>.DownHeap(keys, values, i, num, lo, comparer);
			}
			for (int j = num; j > 1; j--)
			{
				ArraySortHelper<TKey, TValue>.Swap(keys, values, lo, lo + j - 1);
				ArraySortHelper<TKey, TValue>.DownHeap(keys, values, 1, j - 1, lo, comparer);
			}
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x000DD154 File Offset: 0x000DB354
		private static void DownHeap(TKey[] keys, TValue[] values, int i, int n, int lo, IComparer<TKey> comparer)
		{
			TKey tkey = keys[lo + i - 1];
			TValue tvalue = (values != null) ? values[lo + i - 1] : default(TValue);
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n && comparer.Compare(keys[lo + num - 1], keys[lo + num]) < 0)
				{
					num++;
				}
				if (comparer.Compare(tkey, keys[lo + num - 1]) >= 0)
				{
					break;
				}
				keys[lo + i - 1] = keys[lo + num - 1];
				if (values != null)
				{
					values[lo + i - 1] = values[lo + num - 1];
				}
				i = num;
			}
			keys[lo + i - 1] = tkey;
			if (values != null)
			{
				values[lo + i - 1] = tvalue;
			}
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000DD228 File Offset: 0x000DB428
		private static void InsertionSort(TKey[] keys, TValue[] values, int lo, int hi, IComparer<TKey> comparer)
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				TKey tkey = keys[i + 1];
				TValue tvalue = (values != null) ? values[i + 1] : default(TValue);
				while (num >= lo && comparer.Compare(tkey, keys[num]) < 0)
				{
					keys[num + 1] = keys[num];
					if (values != null)
					{
						values[num + 1] = values[num];
					}
					num--;
				}
				keys[num + 1] = tkey;
				if (values != null)
				{
					values[num + 1] = tvalue;
				}
			}
		}

		// Token: 0x040018A8 RID: 6312
		private static volatile IArraySortHelper<TKey, TValue> defaultArraySortHelper;
	}
}
