using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001F0 RID: 496
	public class FilterControl : ExchangeUserControl
	{
		// Token: 0x06001648 RID: 5704 RVA: 0x0005C384 File Offset: 0x0005A584
		public FilterControl()
		{
			this.InitializeComponent();
			base.Name = "FilterControl";
			this.progressTimer = new Timer();
			this.progressTimer.Interval = 500;
			this.progressTimer.Tick += this.ProgressTimer_Tick;
			this.propertiesToFilter = new BindingList<FilterablePropertyDescription>();
			this.propertiesToFilter.ListChanged += this.propertiesToFilter_ListChanged;
			this.filterNodes = new BindingList<FilterNode>();
			this.filterNodes.ListChanged += this.filterNodes_ListChanged;
			this.SupportsOrOperator = true;
			this.AutoValidate = AutoValidate.EnableAllowFocusChange;
			if (!FilterControl.ShowProgressIndicator)
			{
				this.buttons.Items.Remove(this.progressIndicator);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0005C470 File Offset: 0x0005A670
		// (set) Token: 0x0600164A RID: 5706 RVA: 0x0005C478 File Offset: 0x0005A678
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0005C484 File Offset: 0x0005A684
		private void propertiesToFilter_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				List<FilterablePropertyDescription> list = new List<FilterablePropertyDescription>(this.propertiesToFilter);
				list.Sort();
				this.propertiesToFilter.RaiseListChangedEvents = false;
				this.propertiesToFilter.Clear();
				foreach (FilterablePropertyDescription item in list)
				{
					this.propertiesToFilter.Add(item);
				}
				this.propertiesToFilter.RaiseListChangedEvents = true;
				this.UpdateVisibile();
			}
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0005C51C File Offset: 0x0005A71C
		private void UpdateVisibile()
		{
			if (this.DataSource != null && this.DataSource.SupportsFiltering)
			{
				foreach (object obj in this.FilterItems)
				{
					FilterItem filterItem = (FilterItem)obj;
					filterItem.BeginInit();
				}
				base.Visible = (this.PropertiesToFilter.Count > 0);
				foreach (object obj2 in this.FilterItems)
				{
					FilterItem filterItem2 = (FilterItem)obj2;
					filterItem2.EndInit();
				}
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0005C5F4 File Offset: 0x0005A7F4
		private void InitializeComponent()
		{
			this.incompleteItems = new ArrayList();
			this.items = new Panel();
			this.buttons = new TabbableToolStrip();
			this.createFilterButton = new ToolStripButton(Strings.CreateFilter.ToString());
			this.createFilterButton.ToolTipText = ExchangeUserControl.RemoveAccelerator(Strings.CreateFilter);
			this.addNewFilterButton = new ToolStripButton(Strings.AddFilter.ToString());
			this.addNewFilterButton.ToolTipText = ExchangeUserControl.RemoveAccelerator(Strings.AddFilter);
			this.removeFilterButton = new ToolStripButton(Strings.RemoveFilter.ToString());
			this.removeFilterButton.ToolTipText = ExchangeUserControl.RemoveAccelerator(Strings.RemoveFilter);
			this.applyFilterButton = new ToolStripButton(Strings.ApplyFilter.ToString());
			this.applyFilterButton.ToolTipText = ExchangeUserControl.RemoveAccelerator(Strings.ApplyFilter);
			this.stopFilterButton = new ToolStripButton(Strings.StopFilter.ToString());
			this.stopFilterButton.ToolTipText = ExchangeUserControl.RemoveAccelerator(Strings.StopFilter);
			this.progressIndicator = new ToolStripLabel();
			this.buttons.SuspendLayout();
			base.SuspendLayout();
			this.items.Name = "filterItemsPanel";
			this.items.AutoScroll = true;
			this.items.AutoSize = true;
			this.items.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.items.ControlRemoved += this.FilterItemRemoved;
			this.items.Dock = DockStyle.Top;
			this.items.Margin = new Padding(0);
			this.items.TabIndex = 0;
			this.items.TabStop = true;
			this.buttons.Items.AddRange(new ToolStripItem[]
			{
				this.createFilterButton,
				this.addNewFilterButton,
				this.removeFilterButton,
				this.applyFilterButton,
				this.stopFilterButton,
				this.progressIndicator
			});
			this.buttons.GripStyle = ToolStripGripStyle.Hidden;
			this.buttons.Name = "toolstripButtons";
			this.buttons.Dock = DockStyle.Bottom;
			this.buttons.TabStop = true;
			this.buttons.TabIndex = 1;
			this.iconLibrary = new IconLibrary();
			this.iconLibrary.Icons.Add("Create", Icons.CreateFilter);
			this.iconLibrary.Icons.Add("Add", Icons.Add);
			this.iconLibrary.Icons.Add("Remove", Icons.RemoveFilter);
			this.iconLibrary.Icons.Add("Move", Icons.Move);
			this.buttons.ImageList = this.iconLibrary.SmallImageList;
			this.createFilterButton.ImageIndex = 0;
			this.createFilterButton.Name = "createFilterButton";
			this.createFilterButton.Click += this.createFilterButton_Click;
			this.addNewFilterButton.ImageIndex = 1;
			this.addNewFilterButton.Name = "addNewFilterButton";
			this.addNewFilterButton.Click += this.addNewFilter_Click;
			this.removeFilterButton.ImageIndex = 2;
			this.removeFilterButton.Name = "removeFilterButton";
			this.removeFilterButton.Click += this.removeFilterButton_Click;
			this.applyFilterButton.ImageIndex = 3;
			this.applyFilterButton.Name = "applyFilterButton";
			this.applyFilterButton.Click += this.applyFilterButton_Click;
			this.applyFilterButton.Alignment = ToolStripItemAlignment.Right;
			this.stopFilterButton.Name = "stopFilterButton";
			this.stopFilterButton.Click += this.stopFilterButton_Click;
			this.stopFilterButton.Alignment = ToolStripItemAlignment.Right;
			this.progressIndicator.Name = "progressIndicator";
			this.progressIndicator.Alignment = ToolStripItemAlignment.Right;
			this.progressIndicator.Image = Icons.Progress;
			base.Controls.Add(this.buttons);
			base.Controls.Add(this.items);
			this.AutoSize = true;
			this.Dock = DockStyle.Top;
			base.DockPadding.Top = 2;
			base.DockPadding.Bottom = 6;
			this.TabStop = true;
			this.buttons.ResumeLayout(false);
			this.buttons.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0005CAA0 File Offset: 0x0005ACA0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.progressTimer.Dispose();
				this.DataSource = null;
			}
			this.IsFiltering = false;
			base.Dispose(disposing);
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0005CAC5 File Offset: 0x0005ACC5
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x0005CACD File Offset: 0x0005ACCD
		[DefaultValue(true)]
		public override bool AutoSize
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

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0005CAD6 File Offset: 0x0005ACD6
		// (set) Token: 0x06001652 RID: 5714 RVA: 0x0005CADE File Offset: 0x0005ACDE
		[DefaultValue(DockStyle.Top)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0005CAE7 File Offset: 0x0005ACE7
		// (set) Token: 0x06001654 RID: 5716 RVA: 0x0005CAEF File Offset: 0x0005ACEF
		[DefaultValue(true)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x0005CAF8 File Offset: 0x0005ACF8
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(0, 2, 0, 6);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0005CB03 File Offset: 0x0005AD03
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal IList<FilterablePropertyDescription> PropertiesToFilter
		{
			get
			{
				return this.propertiesToFilter;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0005CB0B File Offset: 0x0005AD0B
		private Control.ControlCollection FilterItems
		{
			get
			{
				return this.items.Controls;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0005CB18 File Offset: 0x0005AD18
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x0005CB20 File Offset: 0x0005AD20
		public bool IsApplied
		{
			get
			{
				return this.isApplied;
			}
			protected set
			{
				if (this.isApplied != value)
				{
					this.isApplied = value;
					this.OnIsAppliedChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0005CB40 File Offset: 0x0005AD40
		protected virtual void OnIsAppliedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[FilterControl.EventIsAppliedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x0600165B RID: 5723 RVA: 0x0005CB6E File Offset: 0x0005AD6E
		// (remove) Token: 0x0600165C RID: 5724 RVA: 0x0005CB81 File Offset: 0x0005AD81
		public event EventHandler IsAppliedChanged
		{
			add
			{
				base.Events.AddHandler(FilterControl.EventIsAppliedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(FilterControl.EventIsAppliedChanged, value);
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x0005CB94 File Offset: 0x0005AD94
		protected QueryFilter FilterExpressionTree
		{
			get
			{
				if (this.isFilterTreeStale)
				{
					if (this.filterNodes.Count > 0)
					{
						Hashtable hashtable = new Hashtable();
						foreach (FilterNode filterNode in this.filterNodes)
						{
							List<QueryFilter> list = hashtable[filterNode.PropertyDefinition] as List<QueryFilter>;
							if (list != null)
							{
								list.Add(filterNode.QueryFilter);
							}
							else
							{
								list = new List<QueryFilter>();
								list.Add(filterNode.QueryFilter);
								hashtable.Add(filterNode.PropertyDefinition, list);
							}
						}
						List<QueryFilter> list2 = new List<QueryFilter>(hashtable.Keys.Count);
						foreach (object obj in hashtable.Values)
						{
							List<QueryFilter> list3 = obj as List<QueryFilter>;
							if (list3.Count > 1 && this.SupportsOrOperator)
							{
								list2.Add(new OrFilter(list3.ToArray()));
							}
							else
							{
								list2.AddRange(list3);
							}
						}
						this.filterExpressionTree = new AndFilter(list2.ToArray());
					}
					else
					{
						this.filterExpressionTree = null;
					}
					this.isFilterTreeStale = false;
				}
				return this.filterExpressionTree;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0005CCF8 File Offset: 0x0005AEF8
		[DefaultValue("")]
		public string Expression
		{
			get
			{
				if (this.isFilterStringStale)
				{
					if (this.FilterExpressionTree != null)
					{
						this.filterExpressionString = this.FilterExpressionTree.GenerateInfixString(FilterLanguage.Monad);
					}
					else
					{
						this.filterExpressionString = string.Empty;
					}
					this.isFilterStringStale = false;
				}
				return this.filterExpressionString;
			}
		}

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x0600165F RID: 5727 RVA: 0x0005CD38 File Offset: 0x0005AF38
		// (remove) Token: 0x06001660 RID: 5728 RVA: 0x0005CD70 File Offset: 0x0005AF70
		public event EventHandler ExpressionChanged;

		// Token: 0x06001661 RID: 5729 RVA: 0x0005CDA8 File Offset: 0x0005AFA8
		private void OnExpressionChanged()
		{
			this.isFilterTreeStale = true;
			if (this.isInitializing)
			{
				return;
			}
			try
			{
				this.isFilterStringStale = true;
				if (this.DataSource != null)
				{
					this.DataSource.ApplyFilter(this.FilterExpressionTree);
				}
				this.IsApplied = true;
				this.persistedExpression = ((this.FilterExpressionTree != null) ? WinformsHelper.Serialize(this.FilterExpressionTree) : null);
				this.OnExpressionChanged(EventArgs.Empty);
			}
			catch (InvalidExpressionException ex)
			{
				base.ShowError(ex.Message);
			}
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0005CE34 File Offset: 0x0005B034
		protected virtual void OnExpressionChanged(EventArgs e)
		{
			if (this.ExpressionChanged != null && this.notificationsActive)
			{
				this.ExpressionChanged(this, e);
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0005CE53 File Offset: 0x0005B053
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x0005CE5C File Offset: 0x0005B05C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public byte[] PersistedExpression
		{
			get
			{
				return this.persistedExpression;
			}
			set
			{
				try
				{
					base.SuspendLayout();
					this.notificationsActive = false;
					this.isInitializing = true;
					Control[] array = new Control[this.items.Controls.Count];
					this.items.Controls.CopyTo(array, 0);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] is FilterItem)
						{
							array[i].Dispose();
						}
					}
					List<FilterNode> nodesFromSerializedQueryFilter = FilterNode.GetNodesFromSerializedQueryFilter(value, this.PropertiesToFilter, this.ObjectSchema);
					foreach (FilterNode item in nodesFromSerializedQueryFilter)
					{
						this.filterNodes.Add(item);
					}
				}
				finally
				{
					this.isInitializing = false;
					this.notificationsActive = true;
					this.OnExpressionChanged();
					base.ResumeLayout();
				}
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0005CF48 File Offset: 0x0005B148
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x0005CF50 File Offset: 0x0005B150
		internal ObjectSchema ObjectSchema
		{
			get
			{
				return this.objectSchema;
			}
			set
			{
				this.objectSchema = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0005CF59 File Offset: 0x0005B159
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x0005CF64 File Offset: 0x0005B164
		[DefaultValue(null)]
		public IAdvancedBindingListView DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value != this.DataSource)
				{
					if (this.DataSource != null)
					{
						this.DataSource.FilteringChanged -= this.DataSource_FilteringChanged;
						this.IsFiltering = false;
						this.useFilterWithProgress = false;
					}
					this.dataSource = value;
					if (this.DataSource != null)
					{
						this.DataSource.FilteringChanged += this.DataSource_FilteringChanged;
						this.useFilterWithProgress = this.DataSource.SupportCancelFiltering;
						this.IsFiltering = this.DataSource.Filtering;
					}
					this.UpdateVisibile();
					if (this.dataSource != null && this.PersistedExpression != null)
					{
						this.OnExpressionChanged();
					}
				}
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0005D00E File Offset: 0x0005B20E
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0005D016 File Offset: 0x0005B216
		[DefaultValue(true)]
		public bool SupportsOrOperator
		{
			get
			{
				return this.supportsOrOperator;
			}
			set
			{
				this.supportsOrOperator = value;
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0005D020 File Offset: 0x0005B220
		private void addNewFilter_Click(object sender, EventArgs e)
		{
			FilterNode filterNode = new FilterNode();
			filterNode.FilterablePropertyDescription = this.PropertiesToFilter[0];
			filterNode.Operator = (PropertyFilterOperator)this.PropertiesToFilter[0].SupportedOperators.Values.GetValue(0);
			this.incompleteItems.Add(filterNode);
			this.filterNodes.Add(filterNode);
			this.filterNode_PropertyChanged(filterNode, EventArgs.Empty);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0005D094 File Offset: 0x0005B294
		private void filterNodes_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				this.BindToNodePropertyChanges(this.filterNodes[e.NewIndex]);
				this.InsertFilterItem(this.CreateItemForNode(this.filterNodes[e.NewIndex], this.propertiesToFilter));
			}
			this.IsApplied = false;
			this.SetButtonsVisibility();
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0005D0F1 File Offset: 0x0005B2F1
		internal virtual FilterItem CreateItemForNode(FilterNode node, IList<FilterablePropertyDescription> propertiesToFilter)
		{
			return new FilterItem(node, propertiesToFilter);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0005D0FA File Offset: 0x0005B2FA
		private void BindToNodePropertyChanges(FilterNode node)
		{
			node.FilterablePropertyDescriptionChanged += this.filterNode_PropertyChanged;
			node.OperatorChanged += this.filterNode_PropertyChanged;
			node.ValueChanged += this.filterNode_PropertyChanged;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0005D132 File Offset: 0x0005B332
		private void UnbindFromNodePropertyChanges(FilterNode node)
		{
			node.FilterablePropertyDescriptionChanged -= this.filterNode_PropertyChanged;
			node.OperatorChanged -= this.filterNode_PropertyChanged;
			node.ValueChanged -= this.filterNode_PropertyChanged;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0005D16C File Offset: 0x0005B36C
		private void filterNode_PropertyChanged(object sender, EventArgs e)
		{
			FilterNode filterNode = sender as FilterNode;
			if (filterNode.IsComplete)
			{
				this.incompleteItems.Remove(filterNode);
			}
			else if (!this.incompleteItems.Contains(filterNode))
			{
				this.incompleteItems.Add(filterNode);
			}
			this.IsApplied = false;
			this.SetButtonsVisibility();
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0005D1C0 File Offset: 0x0005B3C0
		private void removeFilterButton_Click(object sender, EventArgs e)
		{
			while (this.FilterItems.Count > 0)
			{
				Control control = this.FilterItems[0];
				this.FilterItems.RemoveAt(0);
				control.Dispose();
			}
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0005D1FC File Offset: 0x0005B3FC
		private void applyFilterButton_Click(object sender, EventArgs e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (FilterNode filterNode in this.filterNodes)
			{
				string value = filterNode.Validate();
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.AppendLine(value);
				}
			}
			if (stringBuilder.Length == 0)
			{
				this.OnExpressionChanged();
				return;
			}
			base.ShowError(stringBuilder.ToString());
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0005D27C File Offset: 0x0005B47C
		private void createFilterButton_Click(object sender, EventArgs e)
		{
			this.addNewFilter_Click(this.addNewFilterButton, EventArgs.Empty);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0005D28F File Offset: 0x0005B48F
		private void stopFilterButton_Click(object sender, EventArgs e)
		{
			if (this.DataSource.SupportCancelFiltering)
			{
				this.stopFilterButton.Enabled = false;
				this.DataSource.CancelFiltering();
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0005D2B8 File Offset: 0x0005B4B8
		protected void InsertFilterItem(FilterItem item)
		{
			item.BeginInit();
			item.Name = string.Format(CultureInfo.InvariantCulture, "Expression{0}", new object[]
			{
				this.FilterItems.Count.ToString(CultureInfo.InvariantCulture)
			});
			this.FilterItems.Add(item);
			this.FilterItems.SetChildIndex(item, 0);
			item.EndInit();
			base.Focus();
			base.SelectNextControl(item, true, true, true, false);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0005D338 File Offset: 0x0005B538
		private void FilterItemRemoved(object sender, ControlEventArgs e)
		{
			FilterItem filterItem = e.Control as FilterItem;
			if (filterItem != null)
			{
				this.UnbindFromNodePropertyChanges(filterItem.FilterNode);
				if (this.incompleteItems.Contains(filterItem.FilterNode))
				{
					this.incompleteItems.Remove(filterItem.FilterNode);
				}
				this.filterNodes.Remove(filterItem.FilterNode);
			}
			if (this.FilterItems.Count == 0)
			{
				this.OnExpressionChanged();
			}
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0005D3A9 File Offset: 0x0005B5A9
		protected override void OnHandleCreated(EventArgs e)
		{
			this.SetButtonsVisibility();
			this.items.MaximumSize = new Size(0, (this.buttons.Height + 2) * 5);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0005D3D4 File Offset: 0x0005B5D4
		private void SetButtonsVisibility()
		{
			base.SuspendLayout();
			bool flag = this.FilterItems.Count > 0;
			this.createFilterButton.Visible = !flag;
			this.removeFilterButton.Visible = flag;
			this.removeFilterButton.Enabled = (!this.IsFiltering || (this.FilterItems.Count == 1 && this.incompleteItems.Count == 1));
			this.applyFilterButton.Visible = (flag && !this.IsFiltering);
			this.applyFilterButton.Enabled = (this.incompleteItems.Count == 0);
			this.addNewFilterButton.Visible = flag;
			this.addNewFilterButton.Enabled = (!this.IsFiltering && this.incompleteItems.Count == 0 && this.FilterItems.Count < 10);
			this.progressIndicator.Visible = this.IsFiltering;
			this.stopFilterButton.Visible = this.IsFiltering;
			this.items.Enabled = !this.IsFiltering;
			base.ResumeLayout();
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0005D4F3 File Offset: 0x0005B6F3
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0005D4FB File Offset: 0x0005B6FB
		public bool IsFiltering
		{
			get
			{
				return this.isFiltering;
			}
			private set
			{
				if (value != this.isFiltering)
				{
					this.isFiltering = value;
					if (this.useFilterWithProgress)
					{
						this.SetButtonsVisibility();
					}
				}
			}
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0005D51C File Offset: 0x0005B71C
		private void DataSource_FilteringChanged(object sender, EventArgs e)
		{
			if (this.DataSource.Filtering)
			{
				this.stopFilterButton.Enabled = this.DataSource.SupportCancelFiltering;
				this.IsFiltering = true;
				this.progressTimer.Stop();
				return;
			}
			if (this.stopFilterButton.Enabled)
			{
				this.stopFilterButton.Enabled = false;
				this.progressTimer.Start();
				return;
			}
			this.IsFiltering = false;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0005D58B File Offset: 0x0005B78B
		private void ProgressTimer_Tick(object sender, EventArgs e)
		{
			this.progressTimer.Stop();
			this.IsFiltering = false;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0005D5A0 File Offset: 0x0005B7A0
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			bool result;
			if (keyData == Keys.Return)
			{
				this.applyFilterButton.PerformClick();
				result = true;
			}
			else
			{
				result = base.ProcessDialogKey(keyData);
			}
			return result;
		}

		// Token: 0x0400081F RID: 2079
		private const int maxExpressionsBeforeScroll = 5;

		// Token: 0x04000820 RID: 2080
		private const int maxExpressionsTotal = 10;

		// Token: 0x04000821 RID: 2081
		private ToolStripButton addNewFilterButton;

		// Token: 0x04000822 RID: 2082
		private ToolStripButton applyFilterButton;

		// Token: 0x04000823 RID: 2083
		private ToolStripButton removeFilterButton;

		// Token: 0x04000824 RID: 2084
		private ToolStripButton createFilterButton;

		// Token: 0x04000825 RID: 2085
		private ToolStripButton stopFilterButton;

		// Token: 0x04000826 RID: 2086
		private ToolStripLabel progressIndicator;

		// Token: 0x04000827 RID: 2087
		private TabbableToolStrip buttons;

		// Token: 0x04000828 RID: 2088
		private Panel items;

		// Token: 0x04000829 RID: 2089
		private IconLibrary iconLibrary;

		// Token: 0x0400082A RID: 2090
		private bool isInitializing;

		// Token: 0x0400082B RID: 2091
		private bool useFilterWithProgress;

		// Token: 0x0400082C RID: 2092
		private bool isFiltering;

		// Token: 0x0400082D RID: 2093
		private BindingList<FilterNode> filterNodes;

		// Token: 0x0400082E RID: 2094
		private Timer progressTimer;

		// Token: 0x0400082F RID: 2095
		private ArrayList incompleteItems;

		// Token: 0x04000830 RID: 2096
		internal static bool ShowProgressIndicator = true;

		// Token: 0x04000831 RID: 2097
		private BindingList<FilterablePropertyDescription> propertiesToFilter;

		// Token: 0x04000832 RID: 2098
		private bool isApplied = true;

		// Token: 0x04000833 RID: 2099
		private static readonly object EventIsAppliedChanged = new object();

		// Token: 0x04000834 RID: 2100
		private QueryFilter filterExpressionTree;

		// Token: 0x04000835 RID: 2101
		private bool isFilterTreeStale = true;

		// Token: 0x04000836 RID: 2102
		private bool isFilterStringStale = true;

		// Token: 0x04000837 RID: 2103
		private string filterExpressionString = string.Empty;

		// Token: 0x04000839 RID: 2105
		private byte[] persistedExpression;

		// Token: 0x0400083A RID: 2106
		private ObjectSchema objectSchema;

		// Token: 0x0400083B RID: 2107
		private bool notificationsActive = true;

		// Token: 0x0400083C RID: 2108
		private IAdvancedBindingListView dataSource;

		// Token: 0x0400083D RID: 2109
		private bool supportsOrOperator;
	}
}
