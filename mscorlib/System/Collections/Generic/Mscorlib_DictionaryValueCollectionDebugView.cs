using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004A0 RID: 1184
	internal sealed class Mscorlib_DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600399E RID: 14750 RVA: 0x000DAF4C File Offset: 0x000D914C
		public Mscorlib_DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x000DAF64 File Offset: 0x000D9164
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400189A RID: 6298
		private ICollection<TValue> collection;
	}
}
