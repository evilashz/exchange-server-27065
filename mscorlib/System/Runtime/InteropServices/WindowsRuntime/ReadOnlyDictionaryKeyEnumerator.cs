using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009B9 RID: 2489
	[Serializable]
	internal sealed class ReadOnlyDictionaryKeyEnumerator<TKey, TValue> : IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x06006372 RID: 25458 RVA: 0x00152146 File Offset: 0x00150346
		public ReadOnlyDictionaryKeyEnumerator(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x0015216F File Offset: 0x0015036F
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x0015217C File Offset: 0x0015037C
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06006375 RID: 25461 RVA: 0x00152189 File Offset: 0x00150389
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<!0>)this).Current;
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06006376 RID: 25462 RVA: 0x00152198 File Offset: 0x00150398
		public TKey Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x001521B8 File Offset: 0x001503B8
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002C38 RID: 11320
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;

		// Token: 0x04002C39 RID: 11321
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
