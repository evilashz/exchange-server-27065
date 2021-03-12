using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009BF RID: 2495
	[DebuggerDisplay("Size = {Size}")]
	internal sealed class IReadOnlyListToIVectorViewAdapter
	{
		// Token: 0x0600638D RID: 25485 RVA: 0x001523F9 File Offset: 0x001505F9
		private IReadOnlyListToIVectorViewAdapter()
		{
		}

		// Token: 0x0600638E RID: 25486 RVA: 0x00152404 File Offset: 0x00150604
		[SecurityCritical]
		internal T GetAt<T>(uint index)
		{
			IReadOnlyList<T> readOnlyList = JitHelpers.UnsafeCast<IReadOnlyList<T>>(this);
			IReadOnlyListToIVectorViewAdapter.EnsureIndexInt32(index, readOnlyList.Count);
			T result;
			try
			{
				result = readOnlyList[(int)index];
			}
			catch (ArgumentOutOfRangeException ex)
			{
				ex.SetErrorCode(-2147483637);
				throw;
			}
			return result;
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x00152450 File Offset: 0x00150650
		[SecurityCritical]
		internal uint Size<T>()
		{
			IReadOnlyList<T> readOnlyList = JitHelpers.UnsafeCast<IReadOnlyList<T>>(this);
			return (uint)readOnlyList.Count;
		}

		// Token: 0x06006390 RID: 25488 RVA: 0x0015246C File Offset: 0x0015066C
		[SecurityCritical]
		internal bool IndexOf<T>(T value, out uint index)
		{
			IReadOnlyList<T> readOnlyList = JitHelpers.UnsafeCast<IReadOnlyList<T>>(this);
			int num = -1;
			int count = readOnlyList.Count;
			for (int i = 0; i < count; i++)
			{
				if (EqualityComparer<T>.Default.Equals(value, readOnlyList[i]))
				{
					num = i;
					break;
				}
			}
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x06006391 RID: 25489 RVA: 0x001524BC File Offset: 0x001506BC
		[SecurityCritical]
		internal uint GetMany<T>(uint startIndex, T[] items)
		{
			IReadOnlyList<T> readOnlyList = JitHelpers.UnsafeCast<IReadOnlyList<T>>(this);
			if ((ulong)startIndex == (ulong)((long)readOnlyList.Count))
			{
				return 0U;
			}
			IReadOnlyListToIVectorViewAdapter.EnsureIndexInt32(startIndex, readOnlyList.Count);
			if (items == null)
			{
				return 0U;
			}
			uint num = Math.Min((uint)items.Length, (uint)(readOnlyList.Count - (int)startIndex));
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				items[(int)num2] = readOnlyList[(int)(num2 + startIndex)];
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

		// Token: 0x06006392 RID: 25490 RVA: 0x0015255C File Offset: 0x0015075C
		private static void EnsureIndexInt32(uint index, int listCapacity)
		{
			if (2147483647U <= index || index >= (uint)listCapacity)
			{
				Exception ex = new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}
	}
}
