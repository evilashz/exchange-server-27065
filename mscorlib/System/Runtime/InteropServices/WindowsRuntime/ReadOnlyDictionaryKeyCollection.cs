using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009B8 RID: 2488
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class ReadOnlyDictionaryKeyCollection<TKey, TValue> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600636F RID: 25455 RVA: 0x00152114 File Offset: 0x00150314
		public ReadOnlyDictionaryKeyCollection(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x06006370 RID: 25456 RVA: 0x00152131 File Offset: 0x00150331
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x00152139 File Offset: 0x00150339
		public IEnumerator<TKey> GetEnumerator()
		{
			return new ReadOnlyDictionaryKeyEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002C37 RID: 11319
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;
	}
}
