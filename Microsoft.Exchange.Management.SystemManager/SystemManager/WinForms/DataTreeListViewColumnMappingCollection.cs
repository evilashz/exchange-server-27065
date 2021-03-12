using System;
using System.Collections;
using System.ComponentModel;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001D6 RID: 470
	public sealed class DataTreeListViewColumnMappingCollection : CollectionBase
	{
		// Token: 0x060014FD RID: 5373 RVA: 0x00056462 File Offset: 0x00054662
		public void Add(string dataMember)
		{
			this.Add(dataMember, -1, null);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0005646D File Offset: 0x0005466D
		public void Add(string dataMember, int imageIndex, string[] propertyNames)
		{
			this.Add(dataMember, imageIndex, propertyNames, string.Empty, ListSortDirection.Ascending);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00056480 File Offset: 0x00054680
		public void Add(string dataMember, int imageIndex, string[] propertyNames, string sortProperty, ListSortDirection sortDirection)
		{
			if (string.IsNullOrEmpty(dataMember))
			{
				throw new ArgumentNullException("dataMember");
			}
			if (this.Find(dataMember) != null)
			{
				throw new ArgumentException(Strings.DuplicateDataMember.ToString(), "dataMember");
			}
			DataTreeListViewColumnMapping value = new DataTreeListViewColumnMapping(dataMember, imageIndex, propertyNames, sortProperty, sortDirection);
			base.List.Add(value);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x000564E4 File Offset: 0x000546E4
		public DataTreeListViewColumnMapping Find(string dataMember)
		{
			DataTreeListViewColumnMapping result = null;
			foreach (object obj in this)
			{
				DataTreeListViewColumnMapping dataTreeListViewColumnMapping = (DataTreeListViewColumnMapping)obj;
				if (dataTreeListViewColumnMapping.DataMember == dataMember)
				{
					result = dataTreeListViewColumnMapping;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00056548 File Offset: 0x00054748
		protected override void OnInsert(int index, object value)
		{
			DataTreeListViewColumnMapping dataTreeListViewColumnMapping = value as DataTreeListViewColumnMapping;
			if (dataTreeListViewColumnMapping == null)
			{
				throw new InvalidOperationException();
			}
			if (string.IsNullOrEmpty(dataTreeListViewColumnMapping.DataMember))
			{
				throw new ArgumentNullException("DataMember");
			}
			for (int i = 0; i < base.Count; i++)
			{
				if ((base.List[i] as DataTreeListViewColumnMapping).DataMember == dataTreeListViewColumnMapping.DataMember && i != index)
				{
					throw new ArgumentException(Strings.DuplicateDataMember.ToString(), "DataMember");
				}
			}
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x000565D3 File Offset: 0x000547D3
		protected override void OnClearComplete()
		{
			base.OnClearComplete();
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, null));
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000565F6 File Offset: 0x000547F6
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0005661B File Offset: 0x0005481B
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, value));
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00056640 File Offset: 0x00054840
		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			base.OnSetComplete(index, oldValue, newValue);
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new CollectionChangeEventArgs(CollectionChangeAction.Refresh, newValue));
			}
		}

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06001506 RID: 5382 RVA: 0x00056668 File Offset: 0x00054868
		// (remove) Token: 0x06001507 RID: 5383 RVA: 0x000566A0 File Offset: 0x000548A0
		public event CollectionChangeEventHandler CollectionChanged;
	}
}
