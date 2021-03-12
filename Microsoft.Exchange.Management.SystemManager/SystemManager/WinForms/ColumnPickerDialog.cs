using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001B5 RID: 437
	public class ColumnPickerDialog : ExchangePropertyPageControl
	{
		// Token: 0x060011AB RID: 4523 RVA: 0x00045EDC File Offset: 0x000440DC
		public ColumnPickerDialog()
		{
			this.InitializeComponent();
			base.HelpTopic = HelpId.ColumnPickerDialog.ToString();
			this.tableLayoutPanel.SuspendLayout();
			this.availableColumnLabel.Text = Strings.AvailableColumnsLabel;
			this.displayedColumnsLabel.Text = Strings.DisplayedColumnsLabel;
			this.addButton.Text = Strings.AddButton;
			this.removeButton.Text = Strings.RemoveButton;
			this.addAllButton.Text = Strings.AddAllButton;
			this.moveUpButton.Text = Strings.MoveUpButton;
			this.moveDownButton.Text = Strings.MoveDownButton;
			this.restoreDefaultsButton.Text = Strings.RestoreDefaultsButton;
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00045FD1 File Offset: 0x000441D1
		public ColumnPickerDialog(DataListView owner) : this()
		{
			this.list = owner;
			this.InitializeLists();
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x00045FE6 File Offset: 0x000441E6
		protected override Size DefaultMaximumSize
		{
			get
			{
				return new Size(570, 316);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00045FF7 File Offset: 0x000441F7
		protected override Size DefaultMinimumSize
		{
			get
			{
				return new Size(570, 316);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00046008 File Offset: 0x00044208
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00046010 File Offset: 0x00044210
		[DefaultValue(true)]
		public new bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00046019 File Offset: 0x00044219
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x00046021 File Offset: 0x00044221
		[DefaultValue(AutoSizeMode.GrowAndShrink)]
		public new AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.AutoSizeMode;
			}
			set
			{
				base.AutoSizeMode = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0004602C File Offset: 0x0004422C
		public StringCollection DisplayedColumnNames
		{
			get
			{
				StringCollection stringCollection = new StringCollection();
				for (int i = 0; i < this.displayedColumnsListView.Items.Count; i++)
				{
					stringCollection.Add(this.displayedColumnsListView.Items[i].SubItems[1].Text);
				}
				return stringCollection;
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00046084 File Offset: 0x00044284
		private void InitializeLists()
		{
			this.availableColumnsListView.BeginUpdate();
			this.displayedColumnsListView.BeginUpdate();
			List<ListViewItem> list = new List<ListViewItem>();
			foreach (ExchangeColumnHeader exchangeColumnHeader in this.list.AvailableColumns)
			{
				ListViewItem listViewItem = new ListViewItem(exchangeColumnHeader.Text);
				listViewItem.Name = exchangeColumnHeader.Name;
				listViewItem.SubItems.Add(exchangeColumnHeader.Name);
				listViewItem.SubItems.Add(exchangeColumnHeader.DisplayIndex.ToString());
				if (exchangeColumnHeader.Visible)
				{
					list.Add(listViewItem);
				}
				else
				{
					this.availableColumnsListView.Items.Add(listViewItem);
				}
			}
			list.Sort(new ColumnPickerDialog.DisplayIndexItemComparer());
			this.displayedColumnsListView.Items.AddRange(list.ToArray());
			this.ResizeLists();
			this.displayedColumnsListView.EndUpdate();
			this.availableColumnsListView.EndUpdate();
			this.SelectFirstItemInBothLists();
			this.UpdateButtonStates();
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004619C File Offset: 0x0004439C
		private ExchangeColumnHeader GetColumnFromListItem(ListViewItem item)
		{
			return this.list.AvailableColumns[item.SubItems[1].Text];
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000461BF File Offset: 0x000443BF
		private void addButton_Click(object sender, EventArgs e)
		{
			this.AddColumns(this.availableColumnsListView.SelectedItems);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000461D2 File Offset: 0x000443D2
		private void addAllButton_Click(object sender, EventArgs e)
		{
			this.AddColumns(this.availableColumnsListView.Items);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000461E8 File Offset: 0x000443E8
		private void AddColumns(IList itemCollection)
		{
			this.displayedColumnsListView.SelectedItems.Clear();
			int index = ((ListViewItem)itemCollection[0]).Index;
			foreach (object obj in itemCollection)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				this.availableColumnsListView.Items.Remove(listViewItem);
				this.displayedColumnsListView.Items.Add(listViewItem);
				listViewItem.Selected = true;
			}
			this.PreserveSelectionAfterAddRemove(index, this.availableColumnsListView, this.displayedColumnsListView);
			this.ResizeLists();
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0004629C File Offset: 0x0004449C
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			this.UpdateRightToLeftLayout();
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x000462AB File Offset: 0x000444AB
		private void UpdateRightToLeftLayout()
		{
			if (base.IsHandleCreated)
			{
				this.availableColumnsListView.RightToLeftLayout = LayoutHelper.IsRightToLeft(this);
				this.displayedColumnsListView.RightToLeftLayout = LayoutHelper.IsRightToLeft(this);
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000462D7 File Offset: 0x000444D7
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.UpdateRightToLeftLayout();
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000462E8 File Offset: 0x000444E8
		private void PreserveSelectionAfterAddRemove(int selectionIndexAfterAdd, ListView fromList, ListView toList)
		{
			if (fromList.Items.Count > 0)
			{
				selectionIndexAfterAdd = ((fromList.Items.Count > selectionIndexAfterAdd) ? selectionIndexAfterAdd : (fromList.Items.Count - 1));
				fromList.Items[selectionIndexAfterAdd].Selected = true;
			}
			toList.SelectedItems[0].Focused = true;
			toList.Focus();
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00046350 File Offset: 0x00044550
		private void removeButton_Click(object sender, EventArgs e)
		{
			this.availableColumnsListView.SelectedItems.Clear();
			int index = this.displayedColumnsListView.SelectedItems[0].Index;
			foreach (object obj in this.displayedColumnsListView.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				this.displayedColumnsListView.Items.Remove(listViewItem);
				this.availableColumnsListView.Items.Add(listViewItem);
				listViewItem.Selected = true;
			}
			this.PreserveSelectionAfterAddRemove(index, this.displayedColumnsListView, this.availableColumnsListView);
			this.ResizeLists();
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00046414 File Offset: 0x00044614
		private void moveUpButton_Click(object sender, EventArgs e)
		{
			this.MoveItem(true);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004641D File Offset: 0x0004461D
		private void moveDownButton_Click(object sender, EventArgs e)
		{
			this.MoveItem(false);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00046428 File Offset: 0x00044628
		private void MoveItem(bool moveUp)
		{
			ListViewItem listViewItem = this.displayedColumnsListView.SelectedItems[0];
			int index = moveUp ? (listViewItem.Index - 1) : (listViewItem.Index + 1);
			this.displayedColumnsListView.Items.Remove(listViewItem);
			this.displayedColumnsListView.Items.Insert(index, listViewItem);
			this.displayedColumnsListView.Focus();
			this.displayedColumnsListView.SelectedItems[0].Focused = true;
			this.UpdateButtonStates();
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x000464AC File Offset: 0x000446AC
		private void restoreDefaultsButton_Click(object sender, EventArgs e)
		{
			this.availableColumnsListView.BeginUpdate();
			this.displayedColumnsListView.BeginUpdate();
			List<ListViewItem> list = new List<ListViewItem>();
			foreach (ExchangeColumnHeader exchangeColumnHeader in this.list.AvailableColumns)
			{
				if (!exchangeColumnHeader.Default && this.displayedColumnsListView.Items.ContainsKey(exchangeColumnHeader.Name))
				{
					ListViewItem listViewItem = this.displayedColumnsListView.Items[exchangeColumnHeader.Name];
					this.displayedColumnsListView.Items.Remove(listViewItem);
					this.availableColumnsListView.Items.Add(listViewItem);
				}
				else if (exchangeColumnHeader.Default)
				{
					ListViewItem listViewItem2;
					if (!this.displayedColumnsListView.Items.ContainsKey(exchangeColumnHeader.Name))
					{
						listViewItem2 = this.availableColumnsListView.Items[exchangeColumnHeader.Name];
						this.availableColumnsListView.Items.Remove(listViewItem2);
					}
					else
					{
						listViewItem2 = this.displayedColumnsListView.Items[exchangeColumnHeader.Name];
						this.displayedColumnsListView.Items.Remove(listViewItem2);
					}
					listViewItem2.SubItems[2].Text = exchangeColumnHeader.DefaultDisplayIndex.ToString();
					list.Add(listViewItem2);
				}
			}
			list.Sort(new ColumnPickerDialog.DisplayIndexItemComparer());
			this.displayedColumnsListView.Items.AddRange(list.ToArray());
			this.SelectFirstItemInBothLists();
			this.ResizeLists();
			this.displayedColumnsListView.EndUpdate();
			this.availableColumnsListView.EndUpdate();
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00046668 File Offset: 0x00044868
		private void ResizeLists()
		{
			this.displayedColumnsListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			this.availableColumnsListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00046698 File Offset: 0x00044898
		private void SelectFirstItemInBothLists()
		{
			this.SelectFirstItem(this.availableColumnsListView);
			this.SelectFirstItem(this.displayedColumnsListView);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000466B2 File Offset: 0x000448B2
		private void SelectFirstItem(ListView list)
		{
			if (list.Items.Count > 0)
			{
				list.SelectedItems.Clear();
				list.Items[0].Selected = true;
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x000466DF File Offset: 0x000448DF
		private void availableColumnsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateButtonStates();
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x000466E7 File Offset: 0x000448E7
		private void displayedColumnsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateButtonStates();
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000466F0 File Offset: 0x000448F0
		private void UpdateButtonStates()
		{
			bool flag = this.displayedColumnsListView.SelectedItems.Count == 1;
			bool flag2 = this.displayedColumnsListView.SelectedItems.Count != 0;
			bool flag3 = false;
			if (flag2)
			{
				foreach (object obj in this.displayedColumnsListView.SelectedItems)
				{
					ListViewItem item = (ListViewItem)obj;
					ExchangeColumnHeader columnFromListItem = this.GetColumnFromListItem(item);
					if (columnFromListItem.Name == this.list.SelectionNameProperty || (columnFromListItem.Name.Equals("Name") && columnFromListItem.Default))
					{
						flag3 = true;
						break;
					}
				}
			}
			bool flag4 = true;
			foreach (ExchangeColumnHeader exchangeColumnHeader in this.list.AvailableColumns)
			{
				if (exchangeColumnHeader.Default != this.displayedColumnsListView.Items.ContainsKey(exchangeColumnHeader.Name) || (exchangeColumnHeader.Default && exchangeColumnHeader.DefaultDisplayIndex != this.displayedColumnsListView.Items.IndexOfKey(exchangeColumnHeader.Name)))
				{
					flag4 = false;
					break;
				}
			}
			bool enabled = flag && this.list.AllowColumnReorder && this.displayedColumnsListView.SelectedIndices[0] != 0 && this.GetColumnFromListItem(this.displayedColumnsListView.SelectedItems[0]).IsReorderable && this.GetColumnFromListItem(this.displayedColumnsListView.Items[this.displayedColumnsListView.SelectedIndices[0] - 1]).IsReorderable;
			bool enabled2 = flag && this.list.AllowColumnReorder && this.displayedColumnsListView.SelectedIndices[0] != this.displayedColumnsListView.Items.Count - 1 && this.GetColumnFromListItem(this.displayedColumnsListView.SelectedItems[0]).IsReorderable && this.GetColumnFromListItem(this.displayedColumnsListView.Items[this.displayedColumnsListView.SelectedIndices[0] + 1]).IsReorderable;
			this.restoreDefaultsButton.Enabled = !flag4;
			this.moveUpButton.Enabled = enabled;
			this.moveDownButton.Enabled = enabled2;
			this.removeButton.Enabled = (flag2 && !flag3);
			this.addButton.Enabled = (this.availableColumnsListView.SelectedItems.Count != 0);
			this.addAllButton.Enabled = (this.availableColumnsListView.Items.Count != 0);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x000469D4 File Offset: 0x00044BD4
		private void availableColumnsListView_ItemActivate(object sender, EventArgs e)
		{
			this.addButton.PerformClick();
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000469E1 File Offset: 0x00044BE1
		private void displayedColumnsListView_ItemActivate(object sender, EventArgs e)
		{
			if (this.removeButton.Enabled)
			{
				this.removeButton.PerformClick();
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x000469FB File Offset: 0x00044BFB
		public override Size GetPreferredSize(Size proposedSize)
		{
			proposedSize.Width = base.Width;
			return this.tableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00046A16 File Offset: 0x00044C16
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00046A38 File Offset: 0x00044C38
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.availableColumnLabel = new Label();
			this.displayedColumnsLabel = new Label();
			this.availableColumnsListView = new ListView();
			this.availableColumnsHeader = new ColumnHeader();
			this.displayedColumnsListView = new ListView();
			this.displayedColumnsHeader = new ColumnHeader();
			this.addButton = new ExchangeButton();
			this.addAllButton = new ExchangeButton();
			this.moveUpButton = new ExchangeButton();
			this.moveDownButton = new ExchangeButton();
			this.restoreDefaultsButton = new ExchangeButton();
			this.removeButton = new ExchangeButton();
			((ISupportInitialize)base.BindingSource).BeginInit();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			base.InputValidationProvider.SetEnabled(base.BindingSource, true);
			this.tableLayoutPanel.AutoLayout = false;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 7;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel.ContainerType = ContainerType.Control;
			this.tableLayoutPanel.Controls.Add(this.availableColumnLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.displayedColumnsLabel, 4, 0);
			this.tableLayoutPanel.Controls.Add(this.availableColumnsListView, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.displayedColumnsListView, 4, 1);
			this.tableLayoutPanel.Controls.Add(this.addButton, 2, 1);
			this.tableLayoutPanel.Controls.Add(this.addAllButton, 2, 3);
			this.tableLayoutPanel.Controls.Add(this.moveUpButton, 6, 1);
			this.tableLayoutPanel.Controls.Add(this.moveDownButton, 6, 2);
			this.tableLayoutPanel.Controls.Add(this.restoreDefaultsButton, 6, 3);
			this.tableLayoutPanel.Controls.Add(this.removeButton, 2, 2);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new Padding(13, 12, 16, 12);
			this.tableLayoutPanel.RowCount = 5;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(570, 312);
			this.tableLayoutPanel.TabIndex = 0;
			this.availableColumnLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.availableColumnLabel.AutoSize = true;
			this.availableColumnLabel.Location = new Point(13, 12);
			this.availableColumnLabel.Margin = new Padding(0);
			this.availableColumnLabel.Name = "availableColumnLabel";
			this.availableColumnLabel.Size = new Size(155, 13);
			this.availableColumnLabel.TabIndex = 0;
			this.availableColumnLabel.Text = "labelAvailableColumns";
			this.displayedColumnsLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.displayedColumnsLabel.AutoSize = true;
			this.displayedColumnsLabel.Location = new Point(268, 12);
			this.displayedColumnsLabel.Margin = new Padding(0);
			this.displayedColumnsLabel.Name = "displayedColumnsLabel";
			this.displayedColumnsLabel.Size = new Size(155, 13);
			this.displayedColumnsLabel.TabIndex = 5;
			this.displayedColumnsLabel.Text = "labelDisplayedColumns";
			this.availableColumnsListView.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.availableColumnsListView.Columns.AddRange(new ColumnHeader[]
			{
				this.availableColumnsHeader
			});
			this.availableColumnsListView.HeaderStyle = ColumnHeaderStyle.None;
			this.availableColumnsListView.HideSelection = false;
			this.availableColumnsListView.Location = new Point(16, 28);
			this.availableColumnsListView.Margin = new Padding(3, 3, 0, 0);
			this.availableColumnsListView.Name = "availableColumnsListView";
			this.tableLayoutPanel.SetRowSpan(this.availableColumnsListView, 4);
			this.availableColumnsListView.Size = new Size(152, 272);
			this.availableColumnsListView.Sorting = SortOrder.Ascending;
			this.availableColumnsListView.TabIndex = 1;
			this.availableColumnsListView.UseCompatibleStateImageBehavior = false;
			this.availableColumnsListView.View = View.Details;
			this.availableColumnsListView.ItemActivate += this.availableColumnsListView_ItemActivate;
			this.availableColumnsListView.SelectedIndexChanged += this.availableColumnsListView_SelectedIndexChanged;
			this.availableColumnsHeader.Text = "DisplayName";
			this.availableColumnsHeader.Width = 148;
			this.displayedColumnsListView.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.displayedColumnsListView.Columns.AddRange(new ColumnHeader[]
			{
				this.displayedColumnsHeader
			});
			this.displayedColumnsListView.HeaderStyle = ColumnHeaderStyle.None;
			this.displayedColumnsListView.HideSelection = false;
			this.displayedColumnsListView.Location = new Point(271, 28);
			this.displayedColumnsListView.Margin = new Padding(3, 3, 0, 0);
			this.displayedColumnsListView.Name = "displayedColumnsListView";
			this.tableLayoutPanel.SetRowSpan(this.displayedColumnsListView, 4);
			this.displayedColumnsListView.Size = new Size(152, 272);
			this.displayedColumnsListView.TabIndex = 6;
			this.displayedColumnsListView.UseCompatibleStateImageBehavior = false;
			this.displayedColumnsListView.View = View.Details;
			this.displayedColumnsListView.ItemActivate += this.displayedColumnsListView_ItemActivate;
			this.displayedColumnsListView.SelectedIndexChanged += this.displayedColumnsListView_SelectedIndexChanged;
			this.displayedColumnsHeader.Text = "DisplayName";
			this.displayedColumnsHeader.Width = 148;
			this.addButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.addButton.AutoSize = true;
			this.addButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.addButton.Enabled = false;
			this.addButton.Location = new Point(176, 28);
			this.addButton.Margin = new Padding(3, 3, 0, 0);
			this.addButton.MinimumSize = new Size(75, 23);
			this.addButton.Name = "addButton";
			this.addButton.Size = new Size(87, 23);
			this.addButton.TabIndex = 2;
			this.addButton.Text = "buttonAdd";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += this.addButton_Click;
			this.addAllButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.addAllButton.AutoSize = true;
			this.addAllButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.addAllButton.Location = new Point(176, 98);
			this.addAllButton.Margin = new Padding(3, 12, 0, 0);
			this.addAllButton.MinimumSize = new Size(75, 23);
			this.addAllButton.Name = "addAllButton";
			this.addAllButton.Size = new Size(87, 23);
			this.addAllButton.TabIndex = 4;
			this.addAllButton.Text = "buttonAddAll";
			this.addAllButton.UseVisualStyleBackColor = true;
			this.addAllButton.Click += this.addAllButton_Click;
			this.moveUpButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.moveUpButton.AutoSize = true;
			this.moveUpButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.moveUpButton.Enabled = false;
			this.moveUpButton.Location = new Point(431, 28);
			this.moveUpButton.Margin = new Padding(3, 3, 0, 0);
			this.moveUpButton.Name = "moveUpButton";
			this.moveUpButton.Size = new Size(123, 23);
			this.moveUpButton.TabIndex = 7;
			this.moveUpButton.Text = "buttonMoveUp";
			this.moveUpButton.UseVisualStyleBackColor = true;
			this.moveUpButton.Click += this.moveUpButton_Click;
			this.moveDownButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.moveDownButton.AutoSize = true;
			this.moveDownButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.moveDownButton.Enabled = false;
			this.moveDownButton.Location = new Point(431, 63);
			this.moveDownButton.Margin = new Padding(3, 12, 0, 0);
			this.moveDownButton.Name = "moveDownButton";
			this.moveDownButton.Size = new Size(123, 23);
			this.moveDownButton.TabIndex = 8;
			this.moveDownButton.Text = "buttonMoveDown";
			this.moveDownButton.UseVisualStyleBackColor = true;
			this.moveDownButton.Click += this.moveDownButton_Click;
			this.restoreDefaultsButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.restoreDefaultsButton.AutoSize = true;
			this.restoreDefaultsButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.restoreDefaultsButton.Location = new Point(431, 98);
			this.restoreDefaultsButton.Margin = new Padding(3, 12, 0, 0);
			this.restoreDefaultsButton.Name = "restoreDefaultsButton";
			this.restoreDefaultsButton.Size = new Size(123, 23);
			this.restoreDefaultsButton.TabIndex = 9;
			this.restoreDefaultsButton.Text = "buttonRestoreDefaults";
			this.restoreDefaultsButton.UseVisualStyleBackColor = true;
			this.restoreDefaultsButton.Click += this.restoreDefaultsButton_Click;
			this.removeButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.removeButton.AutoSize = true;
			this.removeButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.removeButton.Enabled = false;
			this.removeButton.Location = new Point(176, 63);
			this.removeButton.Margin = new Padding(3, 12, 0, 0);
			this.removeButton.MinimumSize = new Size(75, 23);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new Size(87, 23);
			this.removeButton.TabIndex = 3;
			this.removeButton.Text = "buttonRemove";
			this.removeButton.UseVisualStyleBackColor = true;
			this.removeButton.Click += this.removeButton_Click;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.tableLayoutPanel);
			this.MinimumSize = new Size(570, 316);
			base.Name = "ColumnPickerDialog";
			base.Size = new Size(570, 316);
			((ISupportInitialize)base.BindingSource).EndInit();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040006C2 RID: 1730
		private const int columnNameSubItem = 1;

		// Token: 0x040006C3 RID: 1731
		private const int columnDisplayIndexSubItem = 2;

		// Token: 0x040006C4 RID: 1732
		private DataListView list;

		// Token: 0x040006C5 RID: 1733
		private IContainer components;

		// Token: 0x040006C6 RID: 1734
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x040006C7 RID: 1735
		private Label availableColumnLabel;

		// Token: 0x040006C8 RID: 1736
		private Label displayedColumnsLabel;

		// Token: 0x040006C9 RID: 1737
		private ListView availableColumnsListView;

		// Token: 0x040006CA RID: 1738
		private ListView displayedColumnsListView;

		// Token: 0x040006CB RID: 1739
		private ExchangeButton addButton;

		// Token: 0x040006CC RID: 1740
		private ExchangeButton removeButton;

		// Token: 0x040006CD RID: 1741
		private ExchangeButton addAllButton;

		// Token: 0x040006CE RID: 1742
		private ExchangeButton moveUpButton;

		// Token: 0x040006CF RID: 1743
		private ExchangeButton moveDownButton;

		// Token: 0x040006D0 RID: 1744
		private ExchangeButton restoreDefaultsButton;

		// Token: 0x040006D1 RID: 1745
		private ColumnHeader availableColumnsHeader;

		// Token: 0x040006D2 RID: 1746
		private ColumnHeader displayedColumnsHeader;

		// Token: 0x020001B6 RID: 438
		private class DisplayIndexItemComparer : IComparer<ListViewItem>
		{
			// Token: 0x060011CD RID: 4557 RVA: 0x0004766C File Offset: 0x0004586C
			public int Compare(ListViewItem x, ListViewItem y)
			{
				int result = 0;
				if (x == null && y != null)
				{
					result = -1;
				}
				else if (x != null && y == null)
				{
					result = 1;
				}
				else if (x != null && y != null)
				{
					int num = int.Parse(x.SubItems[2].Text);
					int num2 = int.Parse(y.SubItems[2].Text);
					result = num - num2;
				}
				return result;
			}
		}
	}
}
