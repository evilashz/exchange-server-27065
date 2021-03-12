using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x0200049F RID: 1183
	internal sealed class Mscorlib_DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600399C RID: 14748 RVA: 0x000DAF08 File Offset: 0x000D9108
		public Mscorlib_DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000DAF20 File Offset: 0x000D9120
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001899 RID: 6297
		private ICollection<TKey> collection;
	}
}
