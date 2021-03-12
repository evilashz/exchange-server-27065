using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009AD RID: 2477
	internal sealed class ListToVectorAdapter
	{
		// Token: 0x06006316 RID: 25366 RVA: 0x00150F95 File Offset: 0x0014F195
		private ListToVectorAdapter()
		{
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x00150FA0 File Offset: 0x0014F1A0
		[SecurityCritical]
		internal T GetAt<T>(uint index)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count);
			T result;
			try
			{
				result = list[(int)index];
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, innerException, "ArgumentOutOfRange_IndexOutOfRange");
			}
			return result;
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x00150FF0 File Offset: 0x0014F1F0
		[SecurityCritical]
		internal uint Size<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			return (uint)list.Count;
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x0015100C File Offset: 0x0014F20C
		[SecurityCritical]
		internal IReadOnlyList<T> GetView<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			IReadOnlyList<T> readOnlyList = list as IReadOnlyList<T>;
			if (readOnlyList == null)
			{
				readOnlyList = new ReadOnlyCollection<T>(list);
			}
			return readOnlyList;
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x00151034 File Offset: 0x0014F234
		[SecurityCritical]
		internal bool IndexOf<T>(T value, out uint index)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			int num = list.IndexOf(value);
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x00151060 File Offset: 0x0014F260
		[SecurityCritical]
		internal void SetAt<T>(uint index, T value)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count);
			try
			{
				list[(int)index] = value;
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, innerException, "ArgumentOutOfRange_IndexOutOfRange");
			}
		}

		// Token: 0x0600631C RID: 25372 RVA: 0x001510AC File Offset: 0x0014F2AC
		[SecurityCritical]
		internal void InsertAt<T>(uint index, T value)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count + 1);
			try
			{
				list.Insert((int)index, value);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				ex.SetErrorCode(-2147483637);
				throw;
			}
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x001510F8 File Offset: 0x0014F2F8
		[SecurityCritical]
		internal void RemoveAt<T>(uint index)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count);
			try
			{
				list.RemoveAt((int)index);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				ex.SetErrorCode(-2147483637);
				throw;
			}
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x00151140 File Offset: 0x0014F340
		[SecurityCritical]
		internal void Append<T>(T value)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			list.Add(value);
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x0015115C File Offset: 0x0014F35C
		[SecurityCritical]
		internal void RemoveAtEnd<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			if (list.Count == 0)
			{
				Exception ex = new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRemoveLastFromEmptyCollection"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			uint count = (uint)list.Count;
			this.RemoveAt<T>(count - 1U);
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x001511A8 File Offset: 0x0014F3A8
		[SecurityCritical]
		internal void Clear<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			list.Clear();
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x001511C4 File Offset: 0x0014F3C4
		[SecurityCritical]
		internal uint GetMany<T>(uint startIndex, T[] items)
		{
			IList<T> sourceList = JitHelpers.UnsafeCast<IList<T>>(this);
			return ListToVectorAdapter.GetManyHelper<T>(sourceList, startIndex, items);
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x001511E0 File Offset: 0x0014F3E0
		[SecurityCritical]
		internal void ReplaceAll<T>(T[] items)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			list.Clear();
			if (items != null)
			{
				foreach (T item in items)
				{
					list.Add(item);
				}
			}
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x0015121C File Offset: 0x0014F41C
		private static void EnsureIndexInt32(uint index, int listCapacity)
		{
			if (2147483647U <= index || index >= (uint)listCapacity)
			{
				Exception ex = new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x00151258 File Offset: 0x0014F458
		private static uint GetManyHelper<T>(IList<T> sourceList, uint startIndex, T[] items)
		{
			if ((ulong)startIndex == (ulong)((long)sourceList.Count))
			{
				return 0U;
			}
			ListToVectorAdapter.EnsureIndexInt32(startIndex, sourceList.Count);
			if (items == null)
			{
				return 0U;
			}
			uint num = Math.Min((uint)items.Length, (uint)(sourceList.Count - (int)startIndex));
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				items[(int)num2] = sourceList[(int)(num2 + startIndex)];
			}
			if (typeof(T) == typeof(string))
			{
				string[] array = items as string[];
				uint num3 = num;
				while ((ulong)num3 < (ulong)((long)items.Length))
				{
					array[(int)num3] = string.Empty;
					num3 += 1U;
				}
			}
			return num;
		}
	}
}
