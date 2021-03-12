using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009A3 RID: 2467
	[Serializable]
	internal sealed class DictionaryValueEnumerator<TKey, TValue> : IEnumerator<TValue>, IDisposable, IEnumerator
	{
		// Token: 0x060062DD RID: 25309 RVA: 0x001504BA File Offset: 0x0014E6BA
		public DictionaryValueEnumerator(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x060062DE RID: 25310 RVA: 0x001504E3 File Offset: 0x0014E6E3
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x060062DF RID: 25311 RVA: 0x001504F0 File Offset: 0x0014E6F0
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x060062E0 RID: 25312 RVA: 0x001504FD File Offset: 0x0014E6FD
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TValue>)this).Current;
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x060062E1 RID: 25313 RVA: 0x0015050C File Offset: 0x0014E70C
		public TValue Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x060062E2 RID: 25314 RVA: 0x0015052C File Offset: 0x0014E72C
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002C2E RID: 11310
		private readonly IDictionary<TKey, TValue> dictionary;

		// Token: 0x04002C2F RID: 11311
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
