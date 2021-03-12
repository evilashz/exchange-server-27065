using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009A7 RID: 2471
	internal sealed class VectorToListAdapter
	{
		// Token: 0x060062ED RID: 25325 RVA: 0x001506C5 File Offset: 0x0014E8C5
		private VectorToListAdapter()
		{
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x001506D0 File Offset: 0x0014E8D0
		[SecurityCritical]
		internal T Indexer_Get<T>(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> this2 = JitHelpers.UnsafeCast<IVector<T>>(this);
			return VectorToListAdapter.GetAt<T>(this2, (uint)index);
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x001506FC File Offset: 0x0014E8FC
		[SecurityCritical]
		internal void Indexer_Set<T>(int index, T value)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> this2 = JitHelpers.UnsafeCast<IVector<T>>(this);
			VectorToListAdapter.SetAt<T>(this2, (uint)index, value);
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x00150728 File Offset: 0x0014E928
		[SecurityCritical]
		internal int IndexOf<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint num;
			if (!vector.IndexOf(item, out num))
			{
				return -1;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)num;
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x00150764 File Offset: 0x0014E964
		[SecurityCritical]
		internal void Insert<T>(int index, T item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> this2 = JitHelpers.UnsafeCast<IVector<T>>(this);
			VectorToListAdapter.InsertAtHelper<T>(this2, (uint)index, item);
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x00150790 File Offset: 0x0014E990
		[SecurityCritical]
		internal void RemoveAt<T>(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> this2 = JitHelpers.UnsafeCast<IVector<T>>(this);
			VectorToListAdapter.RemoveAtHelper<T>(this2, (uint)index);
		}

		// Token: 0x060062F3 RID: 25331 RVA: 0x001507BC File Offset: 0x0014E9BC
		internal static T GetAt<T>(IVector<T> _this, uint index)
		{
			T at;
			try
			{
				at = _this.GetAt(index);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
			return at;
		}

		// Token: 0x060062F4 RID: 25332 RVA: 0x00150800 File Offset: 0x0014EA00
		private static void SetAt<T>(IVector<T> _this, uint index, T value)
		{
			try
			{
				_this.SetAt(index, value);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
		}

		// Token: 0x060062F5 RID: 25333 RVA: 0x00150844 File Offset: 0x0014EA44
		private static void InsertAtHelper<T>(IVector<T> _this, uint index, T item)
		{
			try
			{
				_this.InsertAt(index, item);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
		}

		// Token: 0x060062F6 RID: 25334 RVA: 0x00150888 File Offset: 0x0014EA88
		internal static void RemoveAtHelper<T>(IVector<T> _this, uint index)
		{
			try
			{
				_this.RemoveAt(index);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
		}
	}
}
