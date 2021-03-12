using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004B2 RID: 1202
	[TypeDependency("System.Collections.Generic.GenericArraySortHelper`1")]
	internal class ArraySortHelper<T> : IArraySortHelper<T>
	{
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06003A19 RID: 14873 RVA: 0x000DC124 File Offset: 0x000DA324
		public static IArraySortHelper<T> Default
		{
			get
			{
				IArraySortHelper<T> arraySortHelper = ArraySortHelper<T>.defaultArraySortHelper;
				if (arraySortHelper == null)
				{
					arraySortHelper = ArraySortHelper<T>.CreateArraySortHelper();
				}
				return arraySortHelper;
			}
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000DC144 File Offset: 0x000DA344
		[SecuritySafeCritical]
		private static IArraySortHelper<T> CreateArraySortHelper()
		{
			if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
			{
				ArraySortHelper<T>.defaultArraySortHelper = (IArraySortHelper<T>)RuntimeTypeHandle.Allocate(typeof(GenericArraySortHelper<string>).TypeHandle.Instantiate(new Type[]
				{
					typeof(T)
				}));
			}
			else
			{
				ArraySortHelper<T>.defaultArraySortHelper = new ArraySortHelper<T>();
			}
			return ArraySortHelper<T>.defaultArraySortHelper;
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000DC1BC File Offset: 0x000DA3BC
		public void Sort(T[] keys, int index, int length, IComparer<T> comparer)
		{
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					ArraySortHelper<T>.IntrospectiveSort(keys, index, length, comparer);
				}
				else
				{
					ArraySortHelper<T>.DepthLimitedQuickSort(keys, index, length + index - 1, comparer, 32);
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

		// Token: 0x06003A1C RID: 14876 RVA: 0x000DC234 File Offset: 0x000DA434
		public int BinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int result;
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				result = ArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException);
			}
			return result;
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000DC280 File Offset: 0x000DA480
		internal static int InternalBinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int i = index;
			int num = index + length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3 = comparer.Compare(array[num2], value);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000DC2C8 File Offset: 0x000DA4C8
		private static void SwapIfGreater(T[] keys, IComparer<T> comparer, int a, int b)
		{
			if (a != b && comparer.Compare(keys[a], keys[b]) > 0)
			{
				T t = keys[a];
				keys[a] = keys[b];
				keys[b] = t;
			}
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000DC310 File Offset: 0x000DA510
		private static void Swap(T[] a, int i, int j)
		{
			if (i != j)
			{
				T t = a[i];
				a[i] = a[j];
				a[j] = t;
			}
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000DC340 File Offset: 0x000DA540
		internal static void DepthLimitedQuickSort(T[] keys, int left, int right, IComparer<T> comparer, int depthLimit)
		{
			while (depthLimit != 0)
			{
				int num = left;
				int num2 = right;
				int num3 = num + (num2 - num >> 1);
				ArraySortHelper<T>.SwapIfGreater(keys, comparer, num, num3);
				ArraySortHelper<T>.SwapIfGreater(keys, comparer, num, num2);
				ArraySortHelper<T>.SwapIfGreater(keys, comparer, num3, num2);
				T t = keys[num3];
				for (;;)
				{
					if (comparer.Compare(keys[num], t) >= 0)
					{
						while (comparer.Compare(t, keys[num2]) < 0)
						{
							num2--;
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							T t2 = keys[num];
							keys[num] = keys[num2];
							keys[num2] = t2;
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
						ArraySortHelper<T>.DepthLimitedQuickSort(keys, left, num2, comparer, depthLimit);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						ArraySortHelper<T>.DepthLimitedQuickSort(keys, num, right, comparer, depthLimit);
					}
					right = num2;
				}
				if (left >= right)
				{
					return;
				}
			}
			ArraySortHelper<T>.Heapsort(keys, left, right, comparer);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000DC427 File Offset: 0x000DA627
		internal static void IntrospectiveSort(T[] keys, int left, int length, IComparer<T> comparer)
		{
			if (length < 2)
			{
				return;
			}
			ArraySortHelper<T>.IntroSort(keys, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(keys.Length), comparer);
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000DC448 File Offset: 0x000DA648
		private static void IntroSort(T[] keys, int lo, int hi, int depthLimit, IComparer<T> comparer)
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
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
						return;
					}
					if (num == 3)
					{
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi - 1);
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, hi - 1, hi);
						return;
					}
					ArraySortHelper<T>.InsertionSort(keys, lo, hi, comparer);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						ArraySortHelper<T>.Heapsort(keys, lo, hi, comparer);
						return;
					}
					depthLimit--;
					int num2 = ArraySortHelper<T>.PickPivotAndPartition(keys, lo, hi, comparer);
					ArraySortHelper<T>.IntroSort(keys, num2 + 1, hi, depthLimit, comparer);
					hi = num2 - 1;
				}
			}
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000DC4E4 File Offset: 0x000DA6E4
		private static int PickPivotAndPartition(T[] keys, int lo, int hi, IComparer<T> comparer)
		{
			int num = lo + (hi - lo) / 2;
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, num);
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, num, hi);
			T t = keys[num];
			ArraySortHelper<T>.Swap(keys, num, hi - 1);
			int i = lo;
			int num2 = hi - 1;
			while (i < num2)
			{
				while (comparer.Compare(keys[++i], t) < 0)
				{
				}
				while (comparer.Compare(t, keys[--num2]) < 0)
				{
				}
				if (i >= num2)
				{
					break;
				}
				ArraySortHelper<T>.Swap(keys, i, num2);
			}
			ArraySortHelper<T>.Swap(keys, i, hi - 1);
			return i;
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000DC574 File Offset: 0x000DA774
		private static void Heapsort(T[] keys, int lo, int hi, IComparer<T> comparer)
		{
			int num = hi - lo + 1;
			for (int i = num / 2; i >= 1; i--)
			{
				ArraySortHelper<T>.DownHeap(keys, i, num, lo, comparer);
			}
			for (int j = num; j > 1; j--)
			{
				ArraySortHelper<T>.Swap(keys, lo, lo + j - 1);
				ArraySortHelper<T>.DownHeap(keys, 1, j - 1, lo, comparer);
			}
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000DC5C4 File Offset: 0x000DA7C4
		private static void DownHeap(T[] keys, int i, int n, int lo, IComparer<T> comparer)
		{
			T t = keys[lo + i - 1];
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n && comparer.Compare(keys[lo + num - 1], keys[lo + num]) < 0)
				{
					num++;
				}
				if (comparer.Compare(t, keys[lo + num - 1]) >= 0)
				{
					break;
				}
				keys[lo + i - 1] = keys[lo + num - 1];
				i = num;
			}
			keys[lo + i - 1] = t;
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000DC64C File Offset: 0x000DA84C
		private static void InsertionSort(T[] keys, int lo, int hi, IComparer<T> comparer)
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T t = keys[i + 1];
				while (num >= lo && comparer.Compare(t, keys[num]) < 0)
				{
					keys[num + 1] = keys[num];
					num--;
				}
				keys[num + 1] = t;
			}
		}

		// Token: 0x040018A7 RID: 6311
		private static volatile IArraySortHelper<T> defaultArraySortHelper;
	}
}
