using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001D7 RID: 471
	public class DataTreeListViewItem : ListViewItem
	{
		// Token: 0x06001508 RID: 5384 RVA: 0x000566D5 File Offset: 0x000548D5
		public DataTreeListViewItem() : this(null, null)
		{
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x000566DF File Offset: 0x000548DF
		public DataTreeListViewItem(DataTreeListView listView, object row) : this(listView, row, null)
		{
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000566EC File Offset: 0x000548EC
		public DataTreeListViewItem(DataTreeListView listView, object row, DataTreeListViewItem parentItem)
		{
			this.dataSource = row;
			this.listView = listView;
			this.IsLeaf = true;
			this.Parent = parentItem;
			if (this.Parent != null)
			{
				this.Parent.ChildrenItems.Add(this);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00056760 File Offset: 0x00054960
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTreeListViewItemCollection ChildrenItems
		{
			get
			{
				if (this.childrenItems == null)
				{
					this.childrenItems = new DataTreeListViewItemCollection(this);
				}
				return this.childrenItems;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0005677C File Offset: 0x0005497C
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x00056784 File Offset: 0x00054984
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsLeaf
		{
			get
			{
				return this.isLeaf;
			}
			set
			{
				if (value != this.IsLeaf)
				{
					if (value && this.ChildrenItems.Count > 0)
					{
						throw new InvalidOperationException();
					}
					this.isLeaf = value;
				}
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x000567AD File Offset: 0x000549AD
		// (set) Token: 0x0600150F RID: 5391 RVA: 0x000567B8 File Offset: 0x000549B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTreeListViewItem Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (value != this.Parent)
				{
					if (this.Parent != null)
					{
						this.Parent.ChildrenItems.Remove(this);
					}
					this.parent = value;
					if (this.Parent == null)
					{
						if (this.ListView != null && !this.ListView.TopItems.Contains(this))
						{
							this.ListView.TopItems.Add(this);
							return;
						}
					}
					else if (!this.Parent.ChildrenItems.Contains(value))
					{
						this.Parent.ChildrenItems.Add(this);
					}
				}
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00056847 File Offset: 0x00054A47
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x00056850 File Offset: 0x00054A50
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		[Browsable(false)]
		public bool IsExpanded
		{
			get
			{
				return this.isExpanded;
			}
			set
			{
				if (this.IsExpanded != value)
				{
					this.isExpanded = value;
					if (this.ListView != null && this.ChildrenItems.Count > 0 && this.isInListView)
					{
						this.ListView.BeginUpdate();
						try
						{
							if (value)
							{
								this.Expand();
							}
							else
							{
								this.Collapse();
							}
						}
						finally
						{
							this.ListView.EndUpdate();
						}
						this.ListView.Invalidate(base.Bounds);
					}
				}
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x000568D8 File Offset: 0x00054AD8
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x000568E0 File Offset: 0x00054AE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DataTreeListView ListView
		{
			get
			{
				return (base.ListView as DataTreeListView) ?? this.listView;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x000568F7 File Offset: 0x00054AF7
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x000568FF File Offset: 0x00054AFF
		[Browsable(true)]
		public Color BackColorBegin
		{
			get
			{
				return this.backColorBegin;
			}
			set
			{
				this.backColorBegin = value;
				if (this.ListView != null)
				{
					this.ListView.Invalidate(base.Bounds);
				}
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x00056921 File Offset: 0x00054B21
		// (set) Token: 0x06001517 RID: 5399 RVA: 0x00056929 File Offset: 0x00054B29
		[Browsable(true)]
		public Color BackColorEnd
		{
			get
			{
				return this.backColorEnd;
			}
			set
			{
				this.backColorEnd = value;
				if (this.ListView != null)
				{
					this.ListView.Invalidate(base.Bounds);
				}
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0005694B File Offset: 0x00054B4B
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x00056953 File Offset: 0x00054B53
		private DataTreeListViewColumnMapping ColumnMapping
		{
			get
			{
				return this.columnMapping;
			}
			set
			{
				this.columnMapping = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0005695C File Offset: 0x00054B5C
		internal bool IsInListView
		{
			get
			{
				return this.isInListView;
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00056964 File Offset: 0x00054B64
		internal Rectangle GetPlusMinusButtonBound()
		{
			Rectangle bounds = base.GetBounds(ItemBoundsPortion.Icon);
			bounds.Offset(-SystemInformation.SmallIconSize.Width, 0);
			return bounds;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00056990 File Offset: 0x00054B90
		public override void Remove()
		{
			if (this.Parent != null)
			{
				this.Parent.ChildrenItems.Remove(this);
				return;
			}
			if (this.ListView != null)
			{
				this.ListView.TopItems.Remove(this);
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000569C8 File Offset: 0x00054BC8
		internal int GetNextSiblingListViewIndex()
		{
			int result = -1;
			if (this.IsInListView)
			{
				if (this.IsLeaf || this.ChildrenItems.Count == 0 || !this.IsExpanded)
				{
					result = base.Index + 1;
				}
				else
				{
					result = this.ChildrenItems[this.ChildrenItems.Count - 1].GetNextSiblingListViewIndex();
				}
			}
			return result;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00056A28 File Offset: 0x00054C28
		internal void AddToListView(DataTreeListView listView)
		{
			this.isInListView = true;
			if (base.Index < 0 && listView != null)
			{
				int num;
				if (this.Parent == null)
				{
					num = listView.Items.Count;
				}
				else
				{
					int num2 = this.Parent.ChildrenItems.IndexOf(this);
					if (num2 == 0)
					{
						num = this.Parent.Index + 1;
					}
					else if (this.Parent.ChildrenItems[num2 - 1].Index > 0)
					{
						num = this.Parent.ChildrenItems[num2 - 1].GetNextSiblingListViewIndex();
					}
					else
					{
						num = -1;
					}
				}
				if (num >= 0)
				{
					listView.Items.Insert(num, this);
				}
			}
			this.BindToChildDataSources();
			this.RecreateChildItems();
			listView.RestoreItemStates(this);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00056AEC File Offset: 0x00054CEC
		internal void RemoveFromListView()
		{
			this.isInListView = false;
			if (this.IsExpanded)
			{
				foreach (object obj in this.ChildrenItems)
				{
					DataTreeListViewItem dataTreeListViewItem = (DataTreeListViewItem)obj;
					dataTreeListViewItem.RemoveFromListView();
				}
			}
			if (base.ListView != null)
			{
				base.ListView.Items.Remove(this);
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00056B6C File Offset: 0x00054D6C
		private void ApplySort()
		{
			int num = -1;
			if (!string.IsNullOrEmpty(this.ListView.SortProperty))
			{
				foreach (object obj in this.ListView.Columns)
				{
					ColumnHeader columnHeader = (ColumnHeader)obj;
					if (this.ListView.SortProperty == columnHeader.Name)
					{
						num = columnHeader.Index;
						break;
					}
				}
			}
			foreach (object obj2 in this.childDataSources)
			{
				BindingSource bindingSource = (BindingSource)obj2;
				if (bindingSource.SupportsSorting && bindingSource.Count > 1)
				{
					string text = this.ListView.SortProperty;
					ListSortDirection sortDirection = this.ListView.SortDirection;
					DataTreeListViewColumnMapping dataTreeListViewColumnMapping = (DataTreeListViewColumnMapping)this.dataSource2ColumnMapping[bindingSource];
					if (num >= 0 && dataTreeListViewColumnMapping.PropertyNames.Count > num && !string.IsNullOrEmpty(dataTreeListViewColumnMapping.PropertyNames[num]))
					{
						text = dataTreeListViewColumnMapping.PropertyNames[num];
					}
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(bindingSource[0]);
					if (string.IsNullOrEmpty(text) || properties[text] == null)
					{
						text = dataTreeListViewColumnMapping.SortProperty;
						sortDirection = dataTreeListViewColumnMapping.SortDirection;
					}
					if (!string.IsNullOrEmpty(text) && properties[text] != null && (!bindingSource.IsSorted || bindingSource.SortProperty.Name != text || bindingSource.SortDirection != sortDirection))
					{
						bindingSource.ApplySort(properties[text], sortDirection);
					}
				}
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00056D58 File Offset: 0x00054F58
		private void RecreateChildItems()
		{
			if (this.ListView != null && this.DataSource != null && this.childDataSources.Count > 0)
			{
				this.ApplySort();
				if (this.ListView.InvokeRequired)
				{
					this.ListView.Invoke(new MethodInvoker(this.RecreateChildItems));
					return;
				}
				this.ListView.BeginUpdate();
				try
				{
					if (this.ChildrenItems.Count > 0)
					{
						if (this.ChildrenItems[0].IsInListView)
						{
							this.ListView.BackupItemsStates();
						}
						this.ChildrenItems.Clear();
					}
					foreach (object obj in this.childDataSources)
					{
						BindingSource bindingSource = (BindingSource)obj;
						foreach (object obj2 in bindingSource)
						{
							DataTreeListViewItem dataTreeListViewItem = this.ListView.InternalCreateListViewItemForRow(obj2);
							dataTreeListViewItem.ColumnMapping = (DataTreeListViewColumnMapping)this.dataSource2ColumnMapping[bindingSource];
							dataTreeListViewItem.UpdateItem();
							if (dataTreeListViewItem.ColumnMapping.ImageIndex < 0)
							{
								if (!string.IsNullOrEmpty(this.ListView.ImagePropertyName))
								{
									PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj2);
									PropertyDescriptor propertyDescriptor = properties[this.ListView.ImagePropertyName];
									if (propertyDescriptor != null)
									{
										dataTreeListViewItem.ImageKey = DataListView.GetPropertyValue(obj2, propertyDescriptor).ToString();
									}
								}
							}
							else
							{
								dataTreeListViewItem.ImageIndex = dataTreeListViewItem.ColumnMapping.ImageIndex;
							}
							this.ChildrenItems.Add(dataTreeListViewItem);
						}
					}
					this.ListView.RaiseItemsForRowsCreated(EventArgs.Empty);
				}
				finally
				{
					this.ListView.EndUpdate();
				}
				this.ListView.TrySelectItemBySpecifiedIdentity();
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x00056F84 File Offset: 0x00055184
		private void UpdateItem()
		{
			if (this.ListView != null)
			{
				this.ListView.InternalUpdateItem(this);
			}
			DataTreeListViewColumnMapping dataTreeListViewColumnMapping = this.ColumnMapping;
			if (dataTreeListViewColumnMapping != null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.DataSource);
				for (int i = 0; i < dataTreeListViewColumnMapping.PropertyNames.Count; i++)
				{
					PropertyDescriptor propertyDescriptor;
					if (!string.IsNullOrEmpty(dataTreeListViewColumnMapping.PropertyNames[i]) && (propertyDescriptor = properties[dataTreeListViewColumnMapping.PropertyNames[i]]) != null && i < base.SubItems.Count)
					{
						base.SubItems[i].Text = this.ListView.FormatRawValueByColumn(DataListView.GetPropertyValue(this.DataSource, propertyDescriptor), this.ListView.Columns[i]);
					}
				}
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0005704C File Offset: 0x0005524C
		private void BindToChildDataSources()
		{
			if (this.ListView != null && this.DataSource != null)
			{
				this.ClearChildrenDataSources();
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.DataSource);
				foreach (object obj in this.ListView.ChildrenDataMembers)
				{
					DataTreeListViewColumnMapping dataTreeListViewColumnMapping = (DataTreeListViewColumnMapping)obj;
					if (properties[dataTreeListViewColumnMapping.DataMember] != null)
					{
						BindingSource bindingSource = new BindingSource(this.DataSource, dataTreeListViewColumnMapping.DataMember);
						this.childDataSources.Add(bindingSource);
						bindingSource.ListChanged += this.ChildList_ListChanged;
						this.dataSource2ColumnMapping[bindingSource] = dataTreeListViewColumnMapping;
						this.ColumnMapping = dataTreeListViewColumnMapping;
					}
				}
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x00057124 File Offset: 0x00055324
		internal void ClearChildrenDataSources()
		{
			foreach (object obj in this.childDataSources)
			{
				BindingSource bindingSource = (BindingSource)obj;
				bindingSource.ListChanged -= this.ChildList_ListChanged;
			}
			this.childDataSources.Clear();
			foreach (object obj2 in this.ChildrenItems)
			{
				DataTreeListViewItem dataTreeListViewItem = (DataTreeListViewItem)obj2;
				dataTreeListViewItem.ClearChildrenDataSources();
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x000571E4 File Offset: 0x000553E4
		private void ChildList_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (this.ListView == null)
			{
				this.ChildrenItems.Clear();
				this.IsExpanded = false;
				this.childDataSources.Clear();
				return;
			}
			IList list = sender as IList;
			if (list != null)
			{
				switch (e.ListChangedType)
				{
				case ListChangedType.Reset:
				case ListChangedType.ItemDeleted:
					this.ListView.SelectedIndices.Clear();
					this.ListView.RaiseSelectionChanged();
					this.RecreateChildItems();
					return;
				case ListChangedType.ItemChanged:
				{
					object row = list[e.NewIndex];
					DataTreeListViewItem dataTreeListViewItem = this.ListView.GetItemFromRow(row) as DataTreeListViewItem;
					if (dataTreeListViewItem == null)
					{
						return;
					}
					if (this.ListView.InvokeRequired)
					{
						this.ListView.Invoke(new MethodInvoker(dataTreeListViewItem.UpdateItem));
						return;
					}
					dataTreeListViewItem.UpdateItem();
					return;
				}
				}
				this.RecreateChildItems();
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x000572C4 File Offset: 0x000554C4
		private void Expand()
		{
			if (!this.IsLeaf && this.IsExpanded && this.ListView != null)
			{
				this.ListView.InternalOnExpandItem(this);
				int index = base.Index;
				foreach (object obj in this.ChildrenItems)
				{
					DataTreeListViewItem dataTreeListViewItem = (DataTreeListViewItem)obj;
					dataTreeListViewItem.AddToListView(this.listView);
				}
				foreach (object obj2 in this.ChildrenItems)
				{
					DataTreeListViewItem dataTreeListViewItem2 = (DataTreeListViewItem)obj2;
					if (dataTreeListViewItem2.IsExpanded)
					{
						dataTreeListViewItem2.Expand();
					}
				}
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x000573AC File Offset: 0x000555AC
		private void Collapse()
		{
			if (!this.IsLeaf && !this.IsExpanded && this.ListView != null)
			{
				this.ListView.InternalOnCollapseItem(this);
				foreach (object obj in this.ChildrenItems)
				{
					DataTreeListViewItem dataTreeListViewItem = (DataTreeListViewItem)obj;
					dataTreeListViewItem.RemoveFromListView();
				}
			}
		}

		// Token: 0x040007A5 RID: 1957
		private DataTreeListViewItemCollection childrenItems;

		// Token: 0x040007A6 RID: 1958
		private bool isLeaf;

		// Token: 0x040007A7 RID: 1959
		private DataTreeListViewItem parent;

		// Token: 0x040007A8 RID: 1960
		private bool isExpanded;

		// Token: 0x040007A9 RID: 1961
		private object dataSource;

		// Token: 0x040007AA RID: 1962
		private DataTreeListView listView;

		// Token: 0x040007AB RID: 1963
		private Color backColorBegin = Color.Empty;

		// Token: 0x040007AC RID: 1964
		private Color backColorEnd = Color.Empty;

		// Token: 0x040007AD RID: 1965
		private DataTreeListViewColumnMapping columnMapping;

		// Token: 0x040007AE RID: 1966
		private ArrayList childDataSources = new ArrayList();

		// Token: 0x040007AF RID: 1967
		private bool isInListView;

		// Token: 0x040007B0 RID: 1968
		private Hashtable dataSource2ColumnMapping = new Hashtable();
	}
}
