using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000696 RID: 1686
	[Serializable]
	internal class ReadOnlySet<T> : ICollection<!0>, IEnumerable<!0>, ICollection, IEnumerable
	{
		// Token: 0x06001EB7 RID: 7863 RVA: 0x000399BB File Offset: 0x00037BBB
		public ReadOnlySet(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.collection = collection;
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x000399D8 File Offset: 0x00037BD8
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x000399DB File Offset: 0x00037BDB
		public int Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x000399E8 File Offset: 0x00037BE8
		public bool IsSynchronized
		{
			get
			{
				ICollection collection = this.collection as ICollection;
				return collection != null && collection.IsSynchronized;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x00039A0C File Offset: 0x00037C0C
		public object SyncRoot
		{
			get
			{
				ICollection collection = this.collection as ICollection;
				if (collection != null)
				{
					return collection.SyncRoot;
				}
				throw new NotSupportedException("Internal collection does not implement ICollection.");
			}
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00039A39 File Offset: 0x00037C39
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00039A45 File Offset: 0x00037C45
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00039A51 File Offset: 0x00037C51
		public bool Contains(T item)
		{
			return this.collection.Contains(item);
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00039A5F File Offset: 0x00037C5F
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.collection.CopyTo(array, arrayIndex);
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00039A6E File Offset: 0x00037C6E
		public void CopyTo(Array array, int index)
		{
			this.CopyTo(array as T[], index);
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00039A7D File Offset: 0x00037C7D
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.collection.GetEnumerator();
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00039A8A File Offset: 0x00037C8A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.collection.GetEnumerator();
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00039A97 File Offset: 0x00037C97
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x04001E6F RID: 7791
		private ICollection<T> collection;
	}
}
