using System;
using System.Collections;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009B2 RID: 2482
	internal sealed class ListToBindableVectorViewAdapter : IBindableVectorView, IBindableIterable
	{
		// Token: 0x0600634E RID: 25422 RVA: 0x00151A2F File Offset: 0x0014FC2F
		internal ListToBindableVectorViewAdapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			this.list = list;
		}

		// Token: 0x0600634F RID: 25423 RVA: 0x00151A4C File Offset: 0x0014FC4C
		private static void EnsureIndexInt32(uint index, int listCapacity)
		{
			if (2147483647U <= index || index >= (uint)listCapacity)
			{
				Exception ex = new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}

		// Token: 0x06006350 RID: 25424 RVA: 0x00151A88 File Offset: 0x0014FC88
		public IBindableIterator First()
		{
			IEnumerator enumerator = this.list.GetEnumerator();
			return new EnumeratorToIteratorAdapter<object>(new EnumerableToBindableIterableAdapter.NonGenericToGenericEnumerator(enumerator));
		}

		// Token: 0x06006351 RID: 25425 RVA: 0x00151AAC File Offset: 0x0014FCAC
		public object GetAt(uint index)
		{
			ListToBindableVectorViewAdapter.EnsureIndexInt32(index, this.list.Count);
			object result;
			try
			{
				result = this.list[(int)index];
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, innerException, "ArgumentOutOfRange_IndexOutOfRange");
			}
			return result;
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06006352 RID: 25426 RVA: 0x00151AFC File Offset: 0x0014FCFC
		public uint Size
		{
			get
			{
				return (uint)this.list.Count;
			}
		}

		// Token: 0x06006353 RID: 25427 RVA: 0x00151B0C File Offset: 0x0014FD0C
		public bool IndexOf(object value, out uint index)
		{
			int num = this.list.IndexOf(value);
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x04002C33 RID: 11315
		private readonly IList list;
	}
}
