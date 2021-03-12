using System;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009A6 RID: 2470
	internal sealed class EnumeratorToIteratorAdapter<T> : IIterator<T>, IBindableIterator
	{
		// Token: 0x060062E7 RID: 25319 RVA: 0x00150594 File Offset: 0x0014E794
		internal EnumeratorToIteratorAdapter(IEnumerator<T> enumerator)
		{
			this.m_enumerator = enumerator;
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x060062E8 RID: 25320 RVA: 0x001505AA File Offset: 0x0014E7AA
		public T Current
		{
			get
			{
				if (this.m_firstItem)
				{
					this.m_firstItem = false;
					this.MoveNext();
				}
				if (!this.m_hasCurrent)
				{
					throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, null);
				}
				return this.m_enumerator.Current;
			}
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x060062E9 RID: 25321 RVA: 0x001505E1 File Offset: 0x0014E7E1
		object IBindableIterator.Current
		{
			get
			{
				return ((IIterator<T>)this).Current;
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x060062EA RID: 25322 RVA: 0x001505EE File Offset: 0x0014E7EE
		public bool HasCurrent
		{
			get
			{
				if (this.m_firstItem)
				{
					this.m_firstItem = false;
					this.MoveNext();
				}
				return this.m_hasCurrent;
			}
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x0015060C File Offset: 0x0014E80C
		public bool MoveNext()
		{
			try
			{
				this.m_hasCurrent = this.m_enumerator.MoveNext();
			}
			catch (InvalidOperationException innerException)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483636, innerException);
			}
			return this.m_hasCurrent;
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x00150650 File Offset: 0x0014E850
		public int GetMany(T[] items)
		{
			if (items == null)
			{
				return 0;
			}
			int num = 0;
			while (num < items.Length && this.HasCurrent)
			{
				items[num] = this.Current;
				this.MoveNext();
				num++;
			}
			if (typeof(T) == typeof(string))
			{
				string[] array = items as string[];
				for (int i = num; i < items.Length; i++)
				{
					array[i] = string.Empty;
				}
			}
			return num;
		}

		// Token: 0x04002C30 RID: 11312
		private IEnumerator<T> m_enumerator;

		// Token: 0x04002C31 RID: 11313
		private bool m_firstItem = true;

		// Token: 0x04002C32 RID: 11314
		private bool m_hasCurrent;
	}
}
