using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001D4 RID: 468
	public class DataTreeListView : DataListView
	{
		// Token: 0x060014C1 RID: 5313 RVA: 0x00055774 File Offset: 0x00053974
		public DataTreeListView()
		{
			this.IconLibrary = this.emptyIconLibrary;
			this.topItems = new DataTreeListViewItemCollection(this);
			this.AutoExpandNewItem = false;
			this.DoubleBuffered = true;
			this.OwnerDraw = true;
			this.emptyImage = new Bitmap(1, 1);
			this.emptyImage.SetPixel(0, 0, this.BackColor);
			this.BackgroundImage = this.emptyImage;
			this.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ChildrenDataMembers.CollectionChanged += delegate(object param0, CollectionChangeEventArgs param1)
			{
				this.CreateItems();
			};
			base.AvailableColumns.ListChanged += delegate(object param0, ListChangedEventArgs param1)
			{
				if (base.AvailableColumns.Count > 0 && base.AvailableColumns[0].IsReorderable)
				{
					base.AvailableColumns[0].IsReorderable = false;
				}
				foreach (ExchangeColumnHeader exchangeColumnHeader in base.AvailableColumns)
				{
					exchangeColumnHeader.IsSortable = false;
				}
				base.HideSortArrow = true;
			};
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00055839 File Offset: 0x00053A39
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateTopItemsFontStyle();
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00055848 File Offset: 0x00053A48
		private void UpdateTopItemsFontStyle()
		{
			Font font = new Font(this.Font, this.Font.Style | FontStyle.Bold);
			base.SuspendLayout();
			foreach (object obj in this.TopItems)
			{
				DataTreeListViewItem dataTreeListViewItem = (DataTreeListViewItem)obj;
				dataTreeListViewItem.Font = font;
			}
			base.ResumeLayout(false);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x000558C8 File Offset: 0x00053AC8
		protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x000558D4 File Offset: 0x00053AD4
		protected override void OnDrawItem(DrawListViewItemEventArgs e)
		{
			DataTreeListViewItem dataTreeListViewItem = e.Item as DataTreeListViewItem;
			if (e.State != (ListViewItemStates)0 && dataTreeListViewItem != null)
			{
				Rectangle bounds = dataTreeListViewItem.Bounds;
				bounds.Intersect(base.ClientRectangle);
				if (base.Enabled && !bounds.IsEmpty && !dataTreeListViewItem.Selected && !dataTreeListViewItem.BackColorBegin.IsEmpty && !dataTreeListViewItem.BackColorEnd.IsEmpty)
				{
					using (Brush brush = new LinearGradientBrush(e.Bounds, dataTreeListViewItem.BackColorBegin, dataTreeListViewItem.BackColorEnd, LinearGradientMode.Horizontal))
					{
						e.Graphics.FillRectangle(brush, e.Bounds);
						goto IL_BE;
					}
				}
				if (base.Enabled && this.BackgroundImage == this.emptyImage)
				{
					e.DrawBackground();
				}
				IL_BE:
				e.DrawDefault = true;
				base.OnDrawItem(e);
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x000559C0 File Offset: 0x00053BC0
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 15)
			{
				base.WndProc(ref m);
				this.DrawPlusMinusButtons();
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x000559F0 File Offset: 0x00053BF0
		private void DrawPlusMinusButtons()
		{
			using (Graphics graphics = base.CreateGraphics())
			{
				foreach (object obj in base.Items)
				{
					DataTreeListViewItem dataTreeListViewItem = (DataTreeListViewItem)obj;
					Rectangle bounds = dataTreeListViewItem.Bounds;
					if (bounds.Bottom >= base.ClientRectangle.Top)
					{
						bounds.Width = base.Columns[0].Width;
						bounds.Intersect(base.ClientRectangle);
						if (!dataTreeListViewItem.IsLeaf && !bounds.IsEmpty && dataTreeListViewItem.ChildrenItems.Count > 0)
						{
							Region clip = graphics.Clip;
							graphics.SetClip(LayoutHelper.MirrorRectangle(bounds, this));
							DataTreeListView.DrawPlusMinusButton(graphics, LayoutHelper.MirrorRectangle(dataTreeListViewItem.GetPlusMinusButtonBound(), this), !dataTreeListViewItem.IsExpanded);
							graphics.Clip = clip;
						}
						else if (bounds.Top > base.ClientRectangle.Bottom)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00055B24 File Offset: 0x00053D24
		private static void DrawPlusMinusButton(Graphics graphics, Rectangle bound, bool isPlusButton)
		{
			int num = Math.Min(bound.Width, bound.Height);
			num /= 2;
			if (num % 2 == 0)
			{
				num++;
			}
			Rectangle rectangle = new Rectangle(bound.Location, new Size(num, num));
			rectangle.Offset((bound.Width - num) / 2, (bound.Height - num) / 2);
			graphics.DrawRectangle(SystemPens.GrayText, rectangle.X, rectangle.Y, num - 1, num - 1);
			int num2 = rectangle.Width - 4;
			if (num2 > 0)
			{
				int num3 = rectangle.Y + rectangle.Height / 2;
				graphics.DrawLine(SystemPens.WindowText, rectangle.X + 2, num3, rectangle.X + 2 + num2 - 1, num3);
			}
			if (isPlusButton && num2 > 0)
			{
				int num4 = rectangle.X + rectangle.Width / 2;
				graphics.DrawLine(SystemPens.WindowText, num4, rectangle.Y + 2, num4, rectangle.Y + 2 + num2 - 1);
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00055C25 File Offset: 0x00053E25
		protected override void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
		{
			base.OnColumnWidthChanged(e);
			base.Invalidate();
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00055C34 File Offset: 0x00053E34
		protected override void OnItemActivate(EventArgs e)
		{
			if (Control.MouseButtons == MouseButtons.Left)
			{
				Point pt = base.PointToClient(Control.MousePosition);
				DataTreeListViewItem dataTreeListViewItem = (DataTreeListViewItem)base.GetItemAt(pt.X, pt.Y);
				if (dataTreeListViewItem != null && dataTreeListViewItem.ChildrenItems.Count > 0 && !dataTreeListViewItem.IsLeaf && dataTreeListViewItem.GetPlusMinusButtonBound().Contains(pt))
				{
					return;
				}
			}
			base.OnItemActivate(e);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00055CA8 File Offset: 0x00053EA8
		protected override void OnKeyDown(KeyEventArgs e)
		{
			DataTreeListViewItem dataTreeListViewItem = base.FocusedItem as DataTreeListViewItem;
			if (dataTreeListViewItem != null)
			{
				switch (e.KeyCode)
				{
				case Keys.Left:
					if (dataTreeListViewItem.IsExpanded)
					{
						dataTreeListViewItem.IsExpanded = false;
						e.Handled = true;
					}
					else if (dataTreeListViewItem.Parent != null)
					{
						base.SelectedIndices.Clear();
						dataTreeListViewItem.Parent.Selected = true;
						dataTreeListViewItem.Parent.Focused = true;
						e.Handled = true;
					}
					break;
				case Keys.Right:
					if (dataTreeListViewItem.IsExpanded)
					{
						if (dataTreeListViewItem.ChildrenItems.Count > 0)
						{
							base.SelectedIndices.Clear();
							dataTreeListViewItem.ChildrenItems[0].Selected = true;
							dataTreeListViewItem.ChildrenItems[0].Focused = true;
							e.Handled = true;
						}
					}
					else if (!dataTreeListViewItem.IsLeaf)
					{
						dataTreeListViewItem.IsExpanded = true;
						e.Handled = true;
					}
					break;
				}
			}
			if (!e.Handled)
			{
				base.OnKeyDown(e);
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00055DAC File Offset: 0x00053FAC
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				DataTreeListViewItem dataTreeListViewItem = base.GetItemAt(e.X, e.Y) as DataTreeListViewItem;
				if (dataTreeListViewItem != null && !dataTreeListViewItem.IsLeaf && dataTreeListViewItem.ChildrenItems.Count > 0 && dataTreeListViewItem.GetPlusMinusButtonBound().Contains(e.X, e.Y))
				{
					dataTreeListViewItem.IsExpanded = !dataTreeListViewItem.IsExpanded;
				}
			}
			base.OnMouseDown(e);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00055E2C File Offset: 0x0005402C
		protected override void OnBackColorChanged(EventArgs e)
		{
			bool flag = this.BackgroundImage == this.emptyImage;
			this.emptyImage = new Bitmap(1, 1);
			this.emptyImage.SetPixel(0, 0, this.BackColor);
			if (flag)
			{
				this.BackgroundImage = this.emptyImage;
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00055E77 File Offset: 0x00054077
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.emptyImage.Dispose();
				this.emptyIconLibrary.Dispose();
				this.TopItems.Clear();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00055EA4 File Offset: 0x000540A4
		public void ExpandAll()
		{
			this.SetExpandedStatusOnRootItems(true, true);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00055EAE File Offset: 0x000540AE
		public void ExpandRootItems()
		{
			this.SetExpandedStatusOnRootItems(true, false);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00055EB8 File Offset: 0x000540B8
		public void CollapseAll()
		{
			this.SetExpandedStatusOnRootItems(false, true);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00055EC2 File Offset: 0x000540C2
		public void CollapseRootItems()
		{
			this.SetExpandedStatusOnRootItems(false, false);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00055ECC File Offset: 0x000540CC
		private void SetExpandedStatusOnRootItems(bool isExpanded, bool isRecursive)
		{
			foreach (object obj in this.TopItems)
			{
				DataTreeListViewItem item = (DataTreeListViewItem)obj;
				this.SetExpandedStatusOnSubItem(item, isExpanded, isRecursive);
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00055F28 File Offset: 0x00054128
		private void SetExpandedStatusOnSubItem(DataTreeListViewItem item, bool isExpanded, bool isRecursive)
		{
			item.IsExpanded = isExpanded;
			if (isRecursive)
			{
				foreach (object obj in item.ChildrenItems)
				{
					DataTreeListViewItem item2 = (DataTreeListViewItem)obj;
					this.SetExpandedStatusOnSubItem(item2, isExpanded, isRecursive);
				}
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00055F90 File Offset: 0x00054190
		internal void InternalOnExpandItem(DataTreeListViewItem item)
		{
			this.OnExpandItem(new ItemCheckedEventArgs(item));
			base.Invalidate(item.Bounds);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00055FAA File Offset: 0x000541AA
		internal void InternalOnCollapseItem(DataTreeListViewItem item)
		{
			this.OnCollapseItem(new ItemCheckedEventArgs(item));
			base.Invalidate(item.Bounds);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00055FC4 File Offset: 0x000541C4
		internal bool HasChildDataMember(object row)
		{
			bool result = false;
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(row);
			foreach (object obj in this.ChildrenDataMembers)
			{
				DataTreeListViewColumnMapping dataTreeListViewColumnMapping = (DataTreeListViewColumnMapping)obj;
				if (properties[dataTreeListViewColumnMapping.DataMember] != null)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00056038 File Offset: 0x00054238
		protected virtual void OnExpandItem(ItemCheckedEventArgs e)
		{
			if (this.ExpandingItem != null)
			{
				this.ExpandingItem(this, e);
			}
		}

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x060014D9 RID: 5337 RVA: 0x00056050 File Offset: 0x00054250
		// (remove) Token: 0x060014DA RID: 5338 RVA: 0x00056088 File Offset: 0x00054288
		public event ItemCheckedEventHandler ExpandingItem;

		// Token: 0x060014DB RID: 5339 RVA: 0x000560BD File Offset: 0x000542BD
		protected virtual void OnCollapseItem(ItemCheckedEventArgs e)
		{
			if (this.CollapsingItem != null)
			{
				this.CollapsingItem(this, e);
			}
		}

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x060014DC RID: 5340 RVA: 0x000560D4 File Offset: 0x000542D4
		// (remove) Token: 0x060014DD RID: 5341 RVA: 0x0005610C File Offset: 0x0005430C
		public event ItemCheckedEventHandler CollapsingItem;

		// Token: 0x060014DE RID: 5342 RVA: 0x00056144 File Offset: 0x00054344
		protected override void OnListManagerListChanged(ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.ItemAdded:
			case ListChangedType.ItemDeleted:
			case ListChangedType.ItemMoved:
				base.OnListManagerListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
				return;
			case ListChangedType.ItemChanged:
			{
				DataTreeListViewItem dataTreeListViewItem = this.TopItems[e.NewIndex];
				if (dataTreeListViewItem != null)
				{
					this.InternalUpdateItem(dataTreeListViewItem);
					return;
				}
				break;
			}
			default:
				base.OnListManagerListChanged(e);
				break;
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000561A8 File Offset: 0x000543A8
		protected override void OnItemsForRowsCreated(EventArgs e)
		{
			this.TopItems.Clear();
			base.RestoreItemsStates(false);
			foreach (object obj in base.Items)
			{
				DataTreeListViewItem item = (DataTreeListViewItem)obj;
				this.TopItems.Add(item);
			}
			this.UpdateTopItemsFontStyle();
			base.OnItemsForRowsCreated(e);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00056228 File Offset: 0x00054428
		protected override ListViewItemStates GetItemStates(ListViewItem item)
		{
			ListViewItemStates listViewItemStates = base.GetItemStates(item);
			if ((item as DataTreeListViewItem).IsExpanded)
			{
				listViewItemStates |= ListViewItemStates.Marked;
			}
			return listViewItemStates;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00056253 File Offset: 0x00054453
		protected override void SetItemStates(ListViewItem item, ListViewItemStates itemStates)
		{
			base.SetItemStates(item, itemStates);
			(item as DataTreeListViewItem).IsExpanded = ((itemStates & ListViewItemStates.Marked) != (ListViewItemStates)0);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00056278 File Offset: 0x00054478
		protected override ListViewItem CreateNewListViewItem(object row)
		{
			DataTreeListViewItem dataTreeListViewItem = new DataTreeListViewItem(this, row);
			dataTreeListViewItem.ImageIndex = base.ImageIndex;
			dataTreeListViewItem.IndentCount = 1;
			dataTreeListViewItem.IsLeaf = !this.HasChildDataMember(row);
			if (!dataTreeListViewItem.IsLeaf)
			{
				dataTreeListViewItem.IsExpanded = this.AutoExpandNewItem;
			}
			return dataTreeListViewItem;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x000562C5 File Offset: 0x000544C5
		internal DataTreeListViewItem InternalCreateListViewItemForRow(object row)
		{
			return base.CreateListViewItemForRow(row) as DataTreeListViewItem;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000562D4 File Offset: 0x000544D4
		internal void InternalUpdateItem(DataTreeListViewItem item)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new DataTreeListView.InternalUpdateItemInvoker(this.InternalUpdateItem), new object[]
				{
					item
				});
				return;
			}
			ItemCheckedEventArgs e = new ItemCheckedEventArgs(item);
			this.OnUpdateItem(e);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00056317 File Offset: 0x00054517
		internal void RaiseItemsForRowsCreated(EventArgs e)
		{
			if (!base.IsCreatingItems)
			{
				base.OnItemsForRowsCreated(e);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00056328 File Offset: 0x00054528
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x00056330 File Offset: 0x00054530
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AutoExpandNewItem
		{
			get
			{
				return this.autoExpandGroup;
			}
			set
			{
				this.autoExpandGroup = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00056339 File Offset: 0x00054539
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTreeListViewColumnMappingCollection ChildrenDataMembers
		{
			get
			{
				return this.childrenDataMembers;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00056341 File Offset: 0x00054541
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTreeListViewItemCollection TopItems
		{
			get
			{
				return this.topItems;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0005634C File Offset: 0x0005454C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int TotalItemsCount
		{
			get
			{
				int num = 0;
				Stack stack = new Stack();
				stack.Push(this.TopItems);
				while (stack.Count > 0)
				{
					DataTreeListViewItemCollection dataTreeListViewItemCollection = (DataTreeListViewItemCollection)stack.Pop();
					num += dataTreeListViewItemCollection.Count;
					foreach (object obj in dataTreeListViewItemCollection)
					{
						DataTreeListViewItem dataTreeListViewItem = (DataTreeListViewItem)obj;
						stack.Push(dataTreeListViewItem.ChildrenItems);
					}
				}
				return num;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x000563E4 File Offset: 0x000545E4
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x000563EC File Offset: 0x000545EC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ColumnHeaderStyle HeaderStyle
		{
			get
			{
				return base.HeaderStyle;
			}
			set
			{
				if (value == ColumnHeaderStyle.Clickable)
				{
					throw new NotSupportedException();
				}
				base.HeaderStyle = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x000563FF File Offset: 0x000545FF
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x00056407 File Offset: 0x00054607
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool OwnerDraw
		{
			get
			{
				return base.OwnerDraw;
			}
			set
			{
				base.OwnerDraw = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00056410 File Offset: 0x00054610
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x00056418 File Offset: 0x00054618
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new IconLibrary IconLibrary
		{
			get
			{
				return base.IconLibrary;
			}
			set
			{
				base.IconLibrary = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00056421 File Offset: 0x00054621
		// (set) Token: 0x060014F2 RID: 5362 RVA: 0x00056424 File Offset: 0x00054624
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool ShowGroups
		{
			get
			{
				return false;
			}
			set
			{
				if (value)
				{
					throw new NotSupportedException();
				}
				base.ShowGroups = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00056436 File Offset: 0x00054636
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x0005643E File Offset: 0x0005463E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				if (value == null)
				{
					base.BackgroundImage = this.emptyImage;
					return;
				}
				base.BackgroundImage = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00056457 File Offset: 0x00054657
		[Browsable(false)]
		public override bool SupportsVirtualMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400079B RID: 1947
		private const int textPadding = 5;

		// Token: 0x0400079C RID: 1948
		private const int iconPadding = 3;

		// Token: 0x0400079F RID: 1951
		private bool autoExpandGroup;

		// Token: 0x040007A0 RID: 1952
		private DataTreeListViewColumnMappingCollection childrenDataMembers = new DataTreeListViewColumnMappingCollection();

		// Token: 0x040007A1 RID: 1953
		private DataTreeListViewItemCollection topItems;

		// Token: 0x040007A2 RID: 1954
		private IconLibrary emptyIconLibrary = new IconLibrary();

		// Token: 0x040007A3 RID: 1955
		private Bitmap emptyImage;

		// Token: 0x020001D5 RID: 469
		// (Invoke) Token: 0x060014F9 RID: 5369
		private delegate void InternalUpdateItemInvoker(DataTreeListViewItem item);
	}
}
