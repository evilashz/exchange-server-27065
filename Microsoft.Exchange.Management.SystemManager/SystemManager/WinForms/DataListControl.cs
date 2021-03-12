using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000115 RID: 277
	public class DataListControl : BindableUserControl
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x000243AC File Offset: 0x000225AC
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x000243B4 File Offset: 0x000225B4
		[DefaultValue(false)]
		public bool SuspendDuplicateErrorMessage
		{
			get
			{
				return this.suspendDuplicateErrorMessage;
			}
			set
			{
				this.suspendDuplicateErrorMessage = value;
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00024448 File Offset: 0x00022648
		public DataListControl()
		{
			this.InitializeComponent();
			base.BindingSource.DataSourceChanged += delegate(object param0, EventArgs param1)
			{
				this.OnDataSourceChanged(EventArgs.Empty);
			};
			base.BindingSource.ListChanged += delegate(object sender, ListChangedEventArgs e)
			{
				if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemDeleted || (base.BindingSource.DataSource is SortedDataList && e.ListChangedType == ListChangedType.ItemMoved))
				{
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			};
			this.DataListView.ContextMenu = this.dataListViewContextMenu;
			base.HandleCreated += delegate(object param0, EventArgs param1)
			{
				this.SetVisibilityForListLabel();
				this.SetVisibilityForPageLabel();
			};
			this.DataListView.HandleCreated += delegate(object param0, EventArgs param1)
			{
				this.AdjustColumWidthForDataListView();
			};
			this.DataListView.SizeChanged += delegate(object param0, EventArgs param1)
			{
				if (this.DataListView.IsHandleCreated)
				{
					this.AdjustColumWidthForDataListView();
				}
			};
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00024507 File Offset: 0x00022707
		protected override Size DefaultSize
		{
			get
			{
				return new Size(389, 372);
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00024518 File Offset: 0x00022718
		private void AdjustColumWidthForDataListView()
		{
			if (1 == this.DataListView.Columns.Count && this.DataListView.HeaderStyle != ColumnHeaderStyle.None && View.Details == this.DataListView.View && this.previousWidthOfDataListView != this.DataListView.Width)
			{
				this.previousWidthOfDataListView = this.DataListView.Width;
				if (this.DataListView.Width - this.DataListView.ClientRectangle.Width >= SystemInformation.VerticalScrollBarWidth)
				{
					this.DataListView.Columns[0].Width = this.DataListView.ClientRectangle.Width;
					return;
				}
				this.DataListView.Columns[0].Width = this.DataListView.ClientRectangle.Width - SystemInformation.VerticalScrollBarWidth;
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00024604 File Offset: 0x00022804
		private void toolStrip_ItemAdded(object sender, ToolStripItemEventArgs e)
		{
			ToolStripDropDownItem toolStripDropDownItem = e.Item as ToolStripDropDownItem;
			if (toolStripDropDownItem != null)
			{
				ToolStripDropDownMenu toolStripDropDownMenu = toolStripDropDownItem.DropDown as ToolStripDropDownMenu;
				if (toolStripDropDownMenu != null)
				{
					toolStripDropDownMenu.ShowImageMargin = false;
				}
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00024636 File Offset: 0x00022836
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ExchangeTextBox EditTextBox
		{
			get
			{
				return this.exchangeTextBoxEdit;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002463E File Offset: 0x0002283E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataListView DataListView
		{
			get
			{
				return this.dataListView;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00024646 File Offset: 0x00022846
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IBindingList DataList
		{
			get
			{
				return base.BindingSource;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0002464E File Offset: 0x0002284E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected Type ItemType
		{
			get
			{
				return ListBindingHelper.GetListItemType(this.DataSource);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0002465B File Offset: 0x0002285B
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x00024663 File Offset: 0x00022863
		[DefaultValue(null)]
		public object DataSource
		{
			get
			{
				return this.originDataSource;
			}
			set
			{
				if (value != this.DataSource)
				{
					this.originDataSource = value;
					base.BindingSource.DataSource = this.CreateWrappedDataSource(value);
				}
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00024687 File Offset: 0x00022887
		protected virtual object CreateWrappedDataSource(object value)
		{
			if (!this.IsSimpleList(value))
			{
				return value;
			}
			return new SortedDataList((IList)value);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000246A0 File Offset: 0x000228A0
		private bool IsSimpleList(object list)
		{
			bool result = false;
			if (list is IList)
			{
				if (list is Array)
				{
					result = true;
				}
				else
				{
					Type type = list.GetType();
					if (type.IsGenericType)
					{
						Type genericTypeDefinition = type.GetGenericTypeDefinition();
						result = (Array.IndexOf<Type>(DataListControl.SimpleGenericListTypes, genericTypeDefinition) >= 0);
					}
				}
			}
			return result;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000246EC File Offset: 0x000228EC
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataListControl.EventDataSourceChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000A65 RID: 2661 RVA: 0x0002471A File Offset: 0x0002291A
		// (remove) Token: 0x06000A66 RID: 2662 RVA: 0x0002472D File Offset: 0x0002292D
		public event EventHandler DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(DataListControl.EventDataSourceChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataListControl.EventDataSourceChanged, value);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00024740 File Offset: 0x00022940
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0002474D File Offset: 0x0002294D
		[DefaultValue("")]
		public string ListLabelText
		{
			get
			{
				return this.listLabel.Text;
			}
			set
			{
				this.listLabel.Text = value;
				if (base.IsHandleCreated)
				{
					this.SetVisibilityForListLabel();
				}
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00024769 File Offset: 0x00022969
		private void SetVisibilityForListLabel()
		{
			this.listLabel.Visible = !string.IsNullOrEmpty(this.listLabel.Text);
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00024789 File Offset: 0x00022989
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00024796 File Offset: 0x00022996
		[DefaultValue("")]
		public string PageLabelText
		{
			get
			{
				return this.pageLabel.Text;
			}
			set
			{
				this.pageLabel.Text = value;
				if (base.IsHandleCreated)
				{
					this.SetVisibilityForPageLabel();
				}
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000247B2 File Offset: 0x000229B2
		private void SetVisibilityForPageLabel()
		{
			this.pageLabel.Visible = !string.IsNullOrEmpty(this.pageLabel.Text);
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x000247D2 File Offset: 0x000229D2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripItemCollection ToolStripItems
		{
			get
			{
				return this.toolStrip.Items;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x000247DF File Offset: 0x000229DF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected TabbableToolStrip ToolStrip
		{
			get
			{
				return this.toolStrip;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x000247E7 File Offset: 0x000229E7
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x000247EF File Offset: 0x000229EF
		[DefaultValue(null)]
		public string IdentityProperty
		{
			get
			{
				return this.identityProperty;
			}
			set
			{
				this.identityProperty = value;
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000247F8 File Offset: 0x000229F8
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			this.DataListView.Focus();
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002480D File Offset: 0x00022A0D
		public override void Refresh()
		{
			base.BindingSource.ResetBindings(false);
			base.Refresh();
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00024824 File Offset: 0x00022A24
		public void ShowErrorAsync(string message)
		{
			base.BeginInvoke(new DataListControl.ShowErrorDelegate(base.ShowError), new object[]
			{
				message
			});
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00024850 File Offset: 0x00022A50
		private void control_VisibleChanged(object sender, EventArgs e)
		{
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.listLabel.Margin = (this.pageLabel.Visible ? new Padding(0, 12, 0, 0) : new Padding(0));
			this.exchangeTextBoxEdit.Margin = ((this.pageLabel.Visible || this.listLabel.Visible) ? new Padding(3, 3, 0, 0) : new Padding(3, 0, 0, 0));
			this.toolStrip.Margin = ((!this.pageLabel.Visible && !this.listLabel.Visible && !this.exchangeTextBoxEdit.Visible) ? new Padding(3, 0, 0, 0) : new Padding(3, 3, 0, 0));
			if (!this.toolStrip.Visible)
			{
				this.dataListView.Margin = ((!this.pageLabel.Visible && !this.listLabel.Visible && !this.exchangeTextBoxEdit.Visible) ? new Padding(3, 0, 0, 0) : new Padding(3, 3, 0, 0));
			}
			else
			{
				this.dataListView.Margin = new Padding(3, 0, 0, 0);
			}
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000249A4 File Offset: 0x00022BA4
		protected void InternalAddRange(ICollection collection)
		{
			int num = 0;
			try
			{
				foreach (object obj in collection)
				{
					int num2 = -1;
					if (!string.IsNullOrEmpty(this.IdentityProperty))
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj)[this.IdentityProperty];
						object value = propertyDescriptor.GetValue(obj);
						for (int i = 0; i < this.DataList.Count; i++)
						{
							object component = this.DataList[i];
							object value2 = propertyDescriptor.GetValue(component);
							if (value.Equals(value2))
							{
								num2 = i;
								break;
							}
						}
					}
					else
					{
						num2 = this.DataList.IndexOf(obj);
					}
					num++;
					this.suppressFocusOnRow = (num != collection.Count);
					if (num2 < 0)
					{
						this.InternalAddValue(obj);
					}
					else
					{
						this.InternalEditValue(num2, obj);
					}
				}
			}
			finally
			{
				this.suppressFocusOnRow = false;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00024ABC File Offset: 0x00022CBC
		protected virtual bool InternalAddValue(object value)
		{
			bool result = false;
			try
			{
				base.NotifyExposedPropertyIsModified();
				this.AddDataItemToDataSource(value);
				result = true;
				if (!this.suppressFocusOnRow)
				{
					this.DataListView.FocusOnDataRow(value);
				}
			}
			catch (InvalidOperationException ex)
			{
				if (!(this.DataSource is MultiValuedPropertyBase) && !(this.DataSource is DataTable) && !(this.DataSource is DataView))
				{
					throw;
				}
				if (!this.SuspendDuplicateErrorMessage)
				{
					base.ShowError(ex.Message);
				}
			}
			catch (DataValidationException ex2)
			{
				base.ShowError(ex2.Message);
			}
			return result;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00024B60 File Offset: 0x00022D60
		private void AddDataItemToDataSource(object item)
		{
			DataRow dataRow = item as DataRow;
			if (dataRow != null && this.Table != null)
			{
				try
				{
					this.Table.Rows.Add(dataRow);
					return;
				}
				catch (ConstraintException)
				{
					if (this.Table.Constraints.Count == 1 && this.Table.Constraints[0] is UniqueConstraint && (this.Table.Constraints[0] as UniqueConstraint).Columns != null && (this.Table.Constraints[0] as UniqueConstraint).Columns.Length > 0)
					{
						throw new InvalidOperationException(DataStrings.ErrorValueAlreadyPresent(dataRow[(this.Table.Constraints[0] as UniqueConstraint).Columns[0]].ToString()));
					}
					throw;
				}
			}
			this.DataList.Add(item);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00024C60 File Offset: 0x00022E60
		protected bool InternalEditValue(int index, object value, bool isShowErrorAsync)
		{
			bool result = false;
			DataListControl.ShowErrorDelegate showErrorDelegate = isShowErrorAsync ? new DataListControl.ShowErrorDelegate(this.ShowErrorAsync) : new DataListControl.ShowErrorDelegate(base.ShowError);
			try
			{
				this.EditDataItemInDataSource(index, value);
				result = true;
				if (!this.suppressFocusOnRow)
				{
					this.DataListView.FocusOnDataRow(value);
				}
			}
			catch (InvalidOperationException ex)
			{
				if (!(this.DataSource is MultiValuedPropertyBase) && !(this.DataSource is DataTable) && !(this.DataSource is DataView))
				{
					throw ex;
				}
				showErrorDelegate(ex.Message);
			}
			catch (DataValidationException ex2)
			{
				showErrorDelegate(ex2.Message);
			}
			return result;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00024D14 File Offset: 0x00022F14
		private DataTable Table
		{
			get
			{
				DataTable dataTable = this.DataSource as DataTable;
				if (dataTable == null && this.DataSource is DataView)
				{
					dataTable = (this.DataSource as DataView).Table;
				}
				return dataTable;
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00024D50 File Offset: 0x00022F50
		private void EditDataItemInDataSource(int index, object value)
		{
			if (value is DataRow && this.Table != null)
			{
				DataRow dataRow = value as DataRow;
				DataRow dest = this.Table.Rows[index];
				try
				{
					DataListControl.CopyDataRow(dataRow, dest);
					return;
				}
				catch (ConstraintException)
				{
					if (this.Table.Constraints.Count == 1 && this.Table.Constraints[0] is UniqueConstraint && (this.Table.Constraints[0] as UniqueConstraint).Columns != null && (this.Table.Constraints[0] as UniqueConstraint).Columns.Length > 0)
					{
						throw new InvalidOperationException(DataStrings.ErrorValueAlreadyPresent(dataRow[(this.Table.Constraints[0] as UniqueConstraint).Columns[0]].ToString()));
					}
					throw;
				}
			}
			this.DataList[index] = value;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00024E5C File Offset: 0x0002305C
		protected virtual bool InternalEditValue(int index, object value)
		{
			base.NotifyExposedPropertyIsModified();
			return this.InternalEditValue(index, value, false);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00024E70 File Offset: 0x00023070
		protected static void CopyDataRow(DataRow source, DataRow dest)
		{
			dest.BeginEdit();
			for (int i = 0; i < source.Table.Columns.Count; i++)
			{
				if (!dest.Table.Columns[i].ReadOnly)
				{
					dest[i] = source[i];
				}
			}
			dest.EndEdit();
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00024ECA File Offset: 0x000230CA
		protected override string ExposedPropertyName
		{
			get
			{
				return "DataSource";
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00024ED1 File Offset: 0x000230D1
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00024EF0 File Offset: 0x000230F0
		private void InitializeComponent()
		{
			this.components = new Container();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.pageLabel = new Label();
			this.listLabel = new Label();
			this.exchangeTextBoxEdit = new ExchangeTextBox();
			this.toolStrip = new TabbableToolStrip();
			this.dataListView = new DataListView();
			this.dataListViewContextMenu = new ContextMenu();
			((ISupportInitialize)base.BindingSource).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.pageLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.listLabel, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.exchangeTextBoxEdit, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.toolStrip, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.dataListView, 0, 4);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Margin = new Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new Size(418, 396);
			this.tableLayoutPanel1.TabIndex = 4;
			this.pageLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.pageLabel.AutoSize = true;
			this.pageLabel.Location = new Point(0, 0);
			this.pageLabel.Margin = new Padding(0);
			this.pageLabel.Name = "pageLabel";
			this.pageLabel.Size = new Size(418, 13);
			this.pageLabel.TabIndex = 0;
			this.pageLabel.Tag = "";
			this.pageLabel.Visible = false;
			this.pageLabel.VisibleChanged += this.control_VisibleChanged;
			this.listLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.listLabel.AutoSize = true;
			this.listLabel.Location = new Point(0, 25);
			this.listLabel.Margin = new Padding(0, 12, 0, 0);
			this.listLabel.Name = "listLabel";
			this.listLabel.Size = new Size(418, 13);
			this.listLabel.TabIndex = 3;
			this.listLabel.Visible = false;
			this.listLabel.VisibleChanged += this.control_VisibleChanged;
			this.exchangeTextBoxEdit.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.exchangeTextBoxEdit.Location = new Point(3, 41);
			this.exchangeTextBoxEdit.Margin = new Padding(3, 3, 0, 0);
			this.exchangeTextBoxEdit.Name = "exchangeTextBoxEdit";
			this.exchangeTextBoxEdit.Size = new Size(415, 20);
			this.exchangeTextBoxEdit.TabIndex = 1;
			this.exchangeTextBoxEdit.Visible = false;
			this.exchangeTextBoxEdit.VisibleChanged += this.control_VisibleChanged;
			this.toolStrip.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.toolStrip.BackColor = Color.Transparent;
			this.toolStrip.Dock = DockStyle.None;
			this.toolStrip.LayoutStyle = ToolStripLayoutStyle.Flow;
			this.toolStrip.Location = new Point(3, 64);
			this.toolStrip.Margin = new Padding(3, 3, 0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new Size(415, 19);
			this.toolStrip.Stretch = true;
			this.toolStrip.TabIndex = 2;
			this.toolStrip.TabStop = true;
			this.toolStrip.Text = "toolStrip";
			this.toolStrip.ItemAdded += this.toolStrip_ItemAdded;
			this.toolStrip.VisibleChanged += this.control_VisibleChanged;
			this.dataListView.ContextMenu = this.dataListViewContextMenu;
			this.dataListView.DataSource = base.BindingSource;
			this.dataListView.Dock = DockStyle.Fill;
			this.dataListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.dataListView.Location = new Point(3, 83);
			this.dataListView.Margin = new Padding(3, 0, 0, 0);
			this.dataListView.Name = "dataListView";
			this.dataListView.Size = new Size(415, 313);
			this.dataListView.TabIndex = 4;
			this.dataListView.UseCompatibleStateImageBehavior = false;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "DataListControl";
			((ISupportInitialize)base.BindingSource).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000457 RID: 1111
		private static Type[] SimpleGenericListTypes = new Type[]
		{
			typeof(DagNetMultiValuedProperty<>),
			typeof(MultiValuedProperty<>),
			typeof(BindingList<>),
			typeof(List<>)
		};

		// Token: 0x04000458 RID: 1112
		private bool suppressFocusOnRow;

		// Token: 0x04000459 RID: 1113
		private bool suspendDuplicateErrorMessage;

		// Token: 0x0400045A RID: 1114
		private int previousWidthOfDataListView;

		// Token: 0x0400045B RID: 1115
		private object originDataSource;

		// Token: 0x0400045C RID: 1116
		private static readonly object EventDataSourceChanged = new object();

		// Token: 0x0400045D RID: 1117
		private string identityProperty;

		// Token: 0x0400045E RID: 1118
		private IContainer components;

		// Token: 0x0400045F RID: 1119
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000460 RID: 1120
		private Label pageLabel;

		// Token: 0x04000461 RID: 1121
		private Label listLabel;

		// Token: 0x04000462 RID: 1122
		private TabbableToolStrip toolStrip;

		// Token: 0x04000463 RID: 1123
		private DataListView dataListView;

		// Token: 0x04000464 RID: 1124
		private ContextMenu dataListViewContextMenu;

		// Token: 0x04000465 RID: 1125
		private ExchangeTextBox exchangeTextBoxEdit;

		// Token: 0x02000116 RID: 278
		// (Invoke) Token: 0x06000A87 RID: 2695
		private delegate void ShowErrorDelegate(string message);
	}
}
