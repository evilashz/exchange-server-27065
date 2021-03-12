using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004A2 RID: 1186
	internal sealed class Mscorlib_KeyedCollectionDebugView<K, T>
	{
		// Token: 0x060039A2 RID: 14754 RVA: 0x000DAFD4 File Offset: 0x000D91D4
		public Mscorlib_KeyedCollectionDebugView(KeyedCollection<K, T> keyedCollection)
		{
			if (keyedCollection == null)
			{
				throw new ArgumentNullException("keyedCollection");
			}
			this.kc = keyedCollection;
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x000DAFF4 File Offset: 0x000D91F4
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this.kc.Count];
				this.kc.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400189C RID: 6300
		private KeyedCollection<K, T> kc;
	}
}
