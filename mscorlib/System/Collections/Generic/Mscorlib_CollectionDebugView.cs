using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x0200049E RID: 1182
	internal sealed class Mscorlib_CollectionDebugView<T>
	{
		// Token: 0x0600399A RID: 14746 RVA: 0x000DAEC2 File Offset: 0x000D90C2
		public Mscorlib_CollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x000DAEDC File Offset: 0x000D90DC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001898 RID: 6296
		private ICollection<T> collection;
	}
}
