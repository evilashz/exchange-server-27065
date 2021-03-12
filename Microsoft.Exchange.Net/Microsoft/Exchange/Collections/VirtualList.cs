using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x020006A8 RID: 1704
	[Serializable]
	internal sealed class VirtualList<T> : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06001F80 RID: 8064 RVA: 0x0003B87B File Offset: 0x00039A7B
		public VirtualList(bool readOnly)
		{
			if (!readOnly)
			{
				throw new NotImplementedException("VirtualList<T> does not yet support writes.");
			}
			this.list = null;
			this.start = -1;
			this.length = -1;
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x0003B8A6 File Offset: 0x00039AA6
		public bool IsReadOnly
		{
			get
			{
				this.ThrowIfNotInitialized();
				return true;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0003B8AF File Offset: 0x00039AAF
		public int Count
		{
			get
			{
				this.ThrowIfNotInitialized();
				return this.length;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x0003B8C0 File Offset: 0x00039AC0
		public bool IsSynchronized
		{
			get
			{
				ICollection collection = this.list as ICollection;
				return collection != null && collection.IsSynchronized;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0003B8E4 File Offset: 0x00039AE4
		public object SyncRoot
		{
			get
			{
				ICollection collection = this.list as ICollection;
				if (collection != null)
				{
					return collection.SyncRoot;
				}
				throw new NotSupportedException("Internal collection does not implement ICollection.");
			}
		}

		// Token: 0x1700084B RID: 2123
		public T this[int index]
		{
			get
			{
				this.ThrowIfNotInitialized();
				return this.list[index + this.start];
			}
		}

		// Token: 0x1700084C RID: 2124
		T IList<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotImplementedException("VirtualList<T> does not yet support writes.");
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x0003B944 File Offset: 0x00039B44
		public void SetRange(IList<T> list, int start, int length)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (start >= list.Count)
			{
				throw new ArgumentException("start must be less than the size of the list.");
			}
			if (start + length > list.Count)
			{
				throw new ArgumentException("'start + length' must be less than or equal to the size of the list.");
			}
			this.list = list;
			this.start = start;
			this.length = length;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0003B99E File Offset: 0x00039B9E
		public void Add(T value)
		{
			throw new NotImplementedException("VirtualList<T> does not yet support writes.");
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0003B9AA File Offset: 0x00039BAA
		public void Clear()
		{
			throw new NotImplementedException("VirtualList<T> does not yet support writes.");
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0003B9B6 File Offset: 0x00039BB6
		public bool Contains(T item)
		{
			throw new NotImplementedException("VirtualList.Contains has not been implemented.");
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0003B9C4 File Offset: 0x00039BC4
		public void CopyTo(T[] array, int arrayIndex)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[i + arrayIndex] = this[i];
			}
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0003B9F2 File Offset: 0x00039BF2
		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException("VirtualList.CopyTo has not been implemented.");
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0003B9FE File Offset: 0x00039BFE
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException("VirtualList.GetEnumerator has not been implemented.");
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0003BA0A File Offset: 0x00039C0A
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException("VirtualList.GetEnumerator has not been implemented.");
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x0003BA16 File Offset: 0x00039C16
		public int IndexOf(T item)
		{
			throw new NotImplementedException("VirtualList.IndexOf has not been implemented.");
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0003BA22 File Offset: 0x00039C22
		public void Insert(int index, T item)
		{
			throw new NotImplementedException("VirtualList.Insert has not been implemented.");
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0003BA2E File Offset: 0x00039C2E
		public bool Remove(T item)
		{
			throw new NotSupportedException("VirtualList<T> does not yet support writes.");
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0003BA3A File Offset: 0x00039C3A
		public void RemoveAt(int index)
		{
			throw new NotSupportedException("VirtualList<T> does not yet support writes.");
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0003BA46 File Offset: 0x00039C46
		private void ThrowIfNotInitialized()
		{
			if (this.list == null || this.start == -1 || this.length == -1)
			{
				throw new InvalidOperationException("Call VirtualList.SetRange first.");
			}
		}

		// Token: 0x04001EA7 RID: 7847
		private const string ListSerializationName = "list";

		// Token: 0x04001EA8 RID: 7848
		private const string VirtualListIsReadOnly = "VirtualList<T> does not yet support writes.";

		// Token: 0x04001EA9 RID: 7849
		private IList<T> list;

		// Token: 0x04001EAA RID: 7850
		private int start;

		// Token: 0x04001EAB RID: 7851
		private int length;
	}
}
