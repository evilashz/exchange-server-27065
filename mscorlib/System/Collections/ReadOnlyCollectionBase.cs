using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000461 RID: 1121
	[ComVisible(true)]
	[Serializable]
	public abstract class ReadOnlyCollectionBase : ICollection, IEnumerable
	{
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06003694 RID: 13972 RVA: 0x000D1263 File Offset: 0x000CF463
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

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06003695 RID: 13973 RVA: 0x000D127E File Offset: 0x000CF47E
		public virtual int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06003696 RID: 13974 RVA: 0x000D128B File Offset: 0x000CF48B
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06003697 RID: 13975 RVA: 0x000D1298 File Offset: 0x000CF498
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000D12A5 File Offset: 0x000CF4A5
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000D12B4 File Offset: 0x000CF4B4
		public virtual IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x040017FF RID: 6143
		private ArrayList list;
	}
}
