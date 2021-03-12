using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004A1 RID: 1185
	internal sealed class Mscorlib_DictionaryDebugView<K, V>
	{
		// Token: 0x060039A0 RID: 14752 RVA: 0x000DAF90 File Offset: 0x000D9190
		public Mscorlib_DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			this.dict = dictionary;
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060039A1 RID: 14753 RVA: 0x000DAFA8 File Offset: 0x000D91A8
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this.dict.Count];
				this.dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400189B RID: 6299
		private IDictionary<K, V> dict;
	}
}
