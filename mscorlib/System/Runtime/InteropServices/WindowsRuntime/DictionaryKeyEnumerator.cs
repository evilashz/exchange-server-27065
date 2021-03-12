using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009A1 RID: 2465
	[Serializable]
	internal sealed class DictionaryKeyEnumerator<TKey, TValue> : IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x060062CD RID: 25293 RVA: 0x001502AE File Offset: 0x0014E4AE
		public DictionaryKeyEnumerator(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x001502D7 File Offset: 0x0014E4D7
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x001502E4 File Offset: 0x0014E4E4
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x060062D0 RID: 25296 RVA: 0x001502F1 File Offset: 0x0014E4F1
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<!0>)this).Current;
			}
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x060062D1 RID: 25297 RVA: 0x00150300 File Offset: 0x0014E500
		public TKey Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x00150320 File Offset: 0x0014E520
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002C2B RID: 11307
		private readonly IDictionary<TKey, TValue> dictionary;

		// Token: 0x04002C2C RID: 11308
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
