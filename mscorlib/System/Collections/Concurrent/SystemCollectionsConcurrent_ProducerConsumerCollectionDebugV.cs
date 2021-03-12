using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x0200047F RID: 1151
	internal sealed class SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<T>
	{
		// Token: 0x06003807 RID: 14343 RVA: 0x000D615C File Offset: 0x000D435C
		public SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.m_collection = collection;
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x000D6179 File Offset: 0x000D4379
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.m_collection.ToArray();
			}
		}

		// Token: 0x04001858 RID: 6232
		private IProducerConsumerCollection<T> m_collection;
	}
}
