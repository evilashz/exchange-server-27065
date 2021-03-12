using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009B1 RID: 2481
	internal sealed class ListToBindableVectorAdapter
	{
		// Token: 0x06006342 RID: 25410 RVA: 0x001517D1 File Offset: 0x0014F9D1
		private ListToBindableVectorAdapter()
		{
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x001517DC File Offset: 0x0014F9DC
		[SecurityCritical]
		internal object GetAt(uint index)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
			object result;
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

		// Token: 0x06006344 RID: 25412 RVA: 0x0015182C File Offset: 0x0014FA2C
		[SecurityCritical]
		internal uint Size()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			return (uint)list.Count;
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x00151848 File Offset: 0x0014FA48
		[SecurityCritical]
		internal IBindableVectorView GetView()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			return new ListToBindableVectorViewAdapter(list);
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x00151864 File Offset: 0x0014FA64
		[SecurityCritical]
		internal bool IndexOf(object value, out uint index)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			int num = list.IndexOf(value);
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x06006347 RID: 25415 RVA: 0x00151890 File Offset: 0x0014FA90
		[SecurityCritical]
		internal void SetAt(uint index, object value)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
			try
			{
				list[(int)index] = value;
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, innerException, "ArgumentOutOfRange_IndexOutOfRange");
			}
		}

		// Token: 0x06006348 RID: 25416 RVA: 0x001518DC File Offset: 0x0014FADC
		[SecurityCritical]
		internal void InsertAt(uint index, object value)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count + 1);
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

		// Token: 0x06006349 RID: 25417 RVA: 0x00151928 File Offset: 0x0014FB28
		[SecurityCritical]
		internal void RemoveAt(uint index)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
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

		// Token: 0x0600634A RID: 25418 RVA: 0x00151970 File Offset: 0x0014FB70
		[SecurityCritical]
		internal void Append(object value)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			list.Add(value);
		}

		// Token: 0x0600634B RID: 25419 RVA: 0x0015198C File Offset: 0x0014FB8C
		[SecurityCritical]
		internal void RemoveAtEnd()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			if (list.Count == 0)
			{
				Exception ex = new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRemoveLastFromEmptyCollection"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			uint count = (uint)list.Count;
			this.RemoveAt(count - 1U);
		}

		// Token: 0x0600634C RID: 25420 RVA: 0x001519D8 File Offset: 0x0014FBD8
		[SecurityCritical]
		internal void Clear()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			list.Clear();
		}

		// Token: 0x0600634D RID: 25421 RVA: 0x001519F4 File Offset: 0x0014FBF4
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
