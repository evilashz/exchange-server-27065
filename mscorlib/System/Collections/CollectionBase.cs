using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200045F RID: 1119
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class CollectionBase : IList, ICollection, IEnumerable
	{
		// Token: 0x06003658 RID: 13912 RVA: 0x000D0C5A File Offset: 0x000CEE5A
		protected CollectionBase()
		{
			this.list = new ArrayList();
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000D0C6D File Offset: 0x000CEE6D
		protected CollectionBase(int capacity)
		{
			this.list = new ArrayList(capacity);
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600365A RID: 13914 RVA: 0x000D0C81 File Offset: 0x000CEE81
		protected ArrayList InnerList
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x000D0C9C File Offset: 0x000CEE9C
		protected IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000D0C9F File Offset: 0x000CEE9F
		// (set) Token: 0x0600365D RID: 13917 RVA: 0x000D0CAC File Offset: 0x000CEEAC
		[ComVisible(false)]
		public int Capacity
		{
			get
			{
				return this.InnerList.Capacity;
			}
			set
			{
				this.InnerList.Capacity = value;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x0600365E RID: 13918 RVA: 0x000D0CBA File Offset: 0x000CEEBA
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.list != null)
				{
					return this.list.Count;
				}
				return 0;
			}
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000D0CD1 File Offset: 0x000CEED1
		[__DynamicallyInvokable]
		public void Clear()
		{
			this.OnClear();
			this.InnerList.Clear();
			this.OnClearComplete();
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000D0CEC File Offset: 0x000CEEEC
		[__DynamicallyInvokable]
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			object value = this.InnerList[index];
			this.OnValidate(value);
			this.OnRemove(index, value);
			this.InnerList.RemoveAt(index);
			try
			{
				this.OnRemoveComplete(index, value);
			}
			catch
			{
				this.InnerList.Insert(index, value);
				throw;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06003661 RID: 13921 RVA: 0x000D0D70 File Offset: 0x000CEF70
		bool IList.IsReadOnly
		{
			get
			{
				return this.InnerList.IsReadOnly;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x000D0D7D File Offset: 0x000CEF7D
		bool IList.IsFixedSize
		{
			get
			{
				return this.InnerList.IsFixedSize;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x000D0D8A File Offset: 0x000CEF8A
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06003664 RID: 13924 RVA: 0x000D0D97 File Offset: 0x000CEF97
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000D0DA4 File Offset: 0x000CEFA4
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		// Token: 0x17000814 RID: 2068
		object IList.this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				return this.InnerList[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this.OnValidate(value);
				object obj = this.InnerList[index];
				this.OnSet(index, obj, value);
				this.InnerList[index] = value;
				try
				{
					this.OnSetComplete(index, obj, value);
				}
				catch
				{
					this.InnerList[index] = obj;
					throw;
				}
			}
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000D0E68 File Offset: 0x000CF068
		bool IList.Contains(object value)
		{
			return this.InnerList.Contains(value);
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000D0E78 File Offset: 0x000CF078
		int IList.Add(object value)
		{
			this.OnValidate(value);
			this.OnInsert(this.InnerList.Count, value);
			int num = this.InnerList.Add(value);
			try
			{
				this.OnInsertComplete(num, value);
			}
			catch
			{
				this.InnerList.RemoveAt(num);
				throw;
			}
			return num;
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000D0ED8 File Offset: 0x000CF0D8
		void IList.Remove(object value)
		{
			this.OnValidate(value);
			int num = this.InnerList.IndexOf(value);
			if (num < 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RemoveArgNotFound"));
			}
			this.OnRemove(num, value);
			this.InnerList.RemoveAt(num);
			try
			{
				this.OnRemoveComplete(num, value);
			}
			catch
			{
				this.InnerList.Insert(num, value);
				throw;
			}
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000D0F4C File Offset: 0x000CF14C
		int IList.IndexOf(object value)
		{
			return this.InnerList.IndexOf(value);
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000D0F5C File Offset: 0x000CF15C
		void IList.Insert(int index, object value)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this.OnValidate(value);
			this.OnInsert(index, value);
			this.InnerList.Insert(index, value);
			try
			{
				this.OnInsertComplete(index, value);
			}
			catch
			{
				this.InnerList.RemoveAt(index);
				throw;
			}
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000D0FD0 File Offset: 0x000CF1D0
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000D0FDD File Offset: 0x000CF1DD
		protected virtual void OnSet(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000D0FDF File Offset: 0x000CF1DF
		protected virtual void OnInsert(int index, object value)
		{
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000D0FE1 File Offset: 0x000CF1E1
		protected virtual void OnClear()
		{
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000D0FE3 File Offset: 0x000CF1E3
		protected virtual void OnRemove(int index, object value)
		{
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000D0FE5 File Offset: 0x000CF1E5
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000D0FF5 File Offset: 0x000CF1F5
		protected virtual void OnSetComplete(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000D0FF7 File Offset: 0x000CF1F7
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000D0FF9 File Offset: 0x000CF1F9
		protected virtual void OnClearComplete()
		{
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000D0FFB File Offset: 0x000CF1FB
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		// Token: 0x040017FD RID: 6141
		private ArrayList list;
	}
}
