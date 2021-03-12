using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009BA RID: 2490
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class ReadOnlyDictionaryValueCollection<TKey, TValue> : IEnumerable<!1>, IEnumerable
	{
		// Token: 0x06006378 RID: 25464 RVA: 0x001521CB File Offset: 0x001503CB
		public ReadOnlyDictionaryValueCollection(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x06006379 RID: 25465 RVA: 0x001521E8 File Offset: 0x001503E8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!1>)this).GetEnumerator();
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x001521F0 File Offset: 0x001503F0
		public IEnumerator<TValue> GetEnumerator()
		{
			return new ReadOnlyDictionaryValueEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002C3A RID: 11322
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;
	}
}
