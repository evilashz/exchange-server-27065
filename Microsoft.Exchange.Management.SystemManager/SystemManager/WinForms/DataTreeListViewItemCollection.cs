using System;
using System.Collections;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001D8 RID: 472
	public sealed class DataTreeListViewItemCollection : CollectionBase
	{
		// Token: 0x06001528 RID: 5416 RVA: 0x00057428 File Offset: 0x00055628
		public DataTreeListViewItemCollection(DataTreeListViewItem owner)
		{
			if (owner == null)
			{
				throw new NotSupportedException();
			}
			this.owner = owner;
			this.listView = owner.ListView;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0005744C File Offset: 0x0005564C
		public DataTreeListViewItemCollection(DataTreeListView listView)
		{
			if (listView == null)
			{
				throw new NotSupportedException();
			}
			this.listView = listView;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00057464 File Offset: 0x00055664
		protected override void OnClear()
		{
			foreach (object obj in this)
			{
				DataTreeListViewItem dataTreeListViewItem = (DataTreeListViewItem)obj;
				dataTreeListViewItem.RemoveFromListView();
				dataTreeListViewItem.ClearChildrenDataSources();
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x000574C0 File Offset: 0x000556C0
		protected override void OnRemove(int index, object value)
		{
			this[index].ClearChildrenDataSources();
			this[index].RemoveFromListView();
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x000574DA File Offset: 0x000556DA
		public void Add(DataTreeListViewItem item)
		{
			this.Insert(base.Count, item);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x000574EC File Offset: 0x000556EC
		public void Insert(int index, DataTreeListViewItem item)
		{
			if (index < 0 && index > base.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (!this.Contains(item))
			{
				base.List.Insert(index, item);
				item.Parent = this.owner;
				if (item.Parent != null)
				{
					this.owner.IsLeaf = false;
					item.IndentCount = this.owner.IndentCount + 1;
					if (item.Parent.IsExpanded && item.Parent.IsInListView)
					{
						item.AddToListView(this.ListView);
						return;
					}
				}
				else
				{
					item.IndentCount = 1;
					item.AddToListView(this.ListView);
				}
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x000575A4 File Offset: 0x000557A4
		public void Remove(DataTreeListViewItem item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				base.RemoveAt(num);
			}
		}

		// Token: 0x170004F1 RID: 1265
		public DataTreeListViewItem this[int index]
		{
			get
			{
				return (DataTreeListViewItem)base.List[index];
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x000575D7 File Offset: 0x000557D7
		public int IndexOf(object value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x000575E5 File Offset: 0x000557E5
		public bool Contains(object value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x000575F3 File Offset: 0x000557F3
		public DataTreeListView ListView
		{
			get
			{
				if (this.owner != null && this.owner.ListView != this.listView)
				{
					this.listView = this.owner.ListView;
				}
				return this.listView;
			}
		}

		// Token: 0x040007B1 RID: 1969
		private DataTreeListViewItem owner;

		// Token: 0x040007B2 RID: 1970
		private DataTreeListView listView;
	}
}
