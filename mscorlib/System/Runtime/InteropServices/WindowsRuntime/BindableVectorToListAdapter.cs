using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009AF RID: 2479
	internal sealed class BindableVectorToListAdapter
	{
		// Token: 0x0600632D RID: 25389 RVA: 0x00151412 File Offset: 0x0014F612
		private BindableVectorToListAdapter()
		{
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x0015141C File Offset: 0x0014F61C
		[SecurityCritical]
		internal object Indexer_Get(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector this2 = JitHelpers.UnsafeCast<IBindableVector>(this);
			return BindableVectorToListAdapter.GetAt(this2, (uint)index);
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x00151448 File Offset: 0x0014F648
		[SecurityCritical]
		internal void Indexer_Set(int index, object value)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector this2 = JitHelpers.UnsafeCast<IBindableVector>(this);
			BindableVectorToListAdapter.SetAt(this2, (uint)index, value);
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x00151474 File Offset: 0x0014F674
		[SecurityCritical]
		internal int Add(object value)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			bindableVector.Append(value);
			uint size = bindableVector.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)(size - 1U);
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x001514B4 File Offset: 0x0014F6B4
		[SecurityCritical]
		internal bool Contains(object item)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num;
			return bindableVector.IndexOf(item, out num);
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x001514D4 File Offset: 0x0014F6D4
		[SecurityCritical]
		internal void Clear()
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			bindableVector.Clear();
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x001514EE File Offset: 0x0014F6EE
		[SecurityCritical]
		internal bool IsFixedSize()
		{
			return false;
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x001514F1 File Offset: 0x0014F6F1
		[SecurityCritical]
		internal bool IsReadOnly()
		{
			return false;
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x001514F4 File Offset: 0x0014F6F4
		[SecurityCritical]
		internal int IndexOf(object item)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num;
			if (!bindableVector.IndexOf(item, out num))
			{
				return -1;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)num;
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x00151530 File Offset: 0x0014F730
		[SecurityCritical]
		internal void Insert(int index, object item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector this2 = JitHelpers.UnsafeCast<IBindableVector>(this);
			BindableVectorToListAdapter.InsertAtHelper(this2, (uint)index, item);
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x0015155C File Offset: 0x0014F75C
		[SecurityCritical]
		internal void Remove(object item)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num;
			bool flag = bindableVector.IndexOf(item, out num);
			if (flag)
			{
				if (2147483647U < num)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
				}
				BindableVectorToListAdapter.RemoveAtHelper(bindableVector, num);
			}
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x0015159C File Offset: 0x0014F79C
		[SecurityCritical]
		internal void RemoveAt(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector this2 = JitHelpers.UnsafeCast<IBindableVector>(this);
			BindableVectorToListAdapter.RemoveAtHelper(this2, (uint)index);
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x001515C8 File Offset: 0x0014F7C8
		private static object GetAt(IBindableVector _this, uint index)
		{
			object at;
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

		// Token: 0x0600633A RID: 25402 RVA: 0x0015160C File Offset: 0x0014F80C
		private static void SetAt(IBindableVector _this, uint index, object value)
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

		// Token: 0x0600633B RID: 25403 RVA: 0x00151650 File Offset: 0x0014F850
		private static void InsertAtHelper(IBindableVector _this, uint index, object item)
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

		// Token: 0x0600633C RID: 25404 RVA: 0x00151694 File Offset: 0x0014F894
		private static void RemoveAtHelper(IBindableVector _this, uint index)
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
