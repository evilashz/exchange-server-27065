using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001E5 RID: 485
	public class ExchangeSummaryControl : ExchangeUserControl
	{
		// Token: 0x060015C7 RID: 5575 RVA: 0x00059708 File Offset: 0x00057908
		public ExchangeSummaryControl(SummaryControlStyle style, DescriptionControl descriptionControl)
		{
			this.InitializeComponent();
			switch (style)
			{
			case SummaryControlStyle.VariableDescriptionSize:
				this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
				this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
				break;
			case SummaryControlStyle.Percentage:
				this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
				this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
				break;
			case SummaryControlStyle.Percentage40:
				this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
				this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
				break;
			}
			this.summaryControlStyle = style;
			this.descriptionControl = descriptionControl;
			this.objectInfoCollection.ListChanged += this.objectInfoCollection_ListChanged;
			this.objectInfoCollection.ListChanging += this.objectInfoCollection_ListChanging;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00059846 File Offset: 0x00057A46
		public ExchangeSummaryControl() : this(SummaryControlStyle.VariableDescriptionSize, DescriptionControl.TextBox)
		{
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x00059850 File Offset: 0x00057A50
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ChangeNotifyingCollection<GeneralPageSummaryInfo> SummaryInfoCollection
		{
			get
			{
				return this.objectInfoCollection;
			}
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00059858 File Offset: 0x00057A58
		private void objectInfoCollection_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				this.summaryPanel_InsertItem(e.NewIndex, this.SummaryInfoCollection[e.NewIndex]);
				return;
			}
			if (e.ListChangedType == ListChangedType.ItemChanged)
			{
				this.summaryPanel_RemoveItem(e.NewIndex, this.SummaryInfoCollection[e.NewIndex]);
				this.summaryPanel_InsertItem(e.NewIndex, this.SummaryInfoCollection[e.NewIndex]);
				return;
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				for (int i = 0; i < this.SummaryInfoCollection.Count; i++)
				{
					this.summaryPanel_InsertItem(i, this.SummaryInfoCollection[i]);
				}
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00059904 File Offset: 0x00057B04
		private void objectInfoCollection_ListChanging(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				this.summaryPanel_RemoveItem(e.NewIndex, this.SummaryInfoCollection[e.NewIndex]);
				return;
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				for (int i = this.SummaryInfoCollection.Count - 1; i >= 0; i--)
				{
					this.summaryPanel_RemoveItem(i, this.SummaryInfoCollection[i]);
				}
			}
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0005996C File Offset: 0x00057B6C
		private void summaryPanel_RemoveItem(int rowIndex, GeneralPageSummaryInfo objectInfo)
		{
			Control controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(0, rowIndex);
			Control controlFromPosition2 = this.tableLayoutPanel.GetControlFromPosition(1, rowIndex);
			this.tableLayoutPanel.Controls.Remove(controlFromPosition);
			this.tableLayoutPanel.Controls.Remove(controlFromPosition2);
			for (int i = rowIndex + 1; i < this.tableLayoutPanel.RowCount; i++)
			{
				Control controlFromPosition3 = this.tableLayoutPanel.GetControlFromPosition(0, i);
				if (controlFromPosition3 != null)
				{
					this.tableLayoutPanel.SetRow(controlFromPosition3, i - 1);
				}
				controlFromPosition3 = this.tableLayoutPanel.GetControlFromPosition(1, i);
				if (controlFromPosition3 != null)
				{
					this.tableLayoutPanel.SetRow(controlFromPosition3, i - 1);
				}
			}
			this.tableLayoutPanel.RowStyles.RemoveAt(this.tableLayoutPanel.RowCount - 1);
			this.tableLayoutPanel.RowCount--;
			if (rowIndex == 0 && this.GetControlFromPosition(0, 0) != null)
			{
				this.GetControlFromPosition(0, 0).Margin = this.firstRowMargin;
				this.GetControlFromPosition(1, 0).Margin = this.firstRowMargin;
			}
			objectInfo.BindingSourceChanged -= this.objectInfo_BindingSourceChanged;
			objectInfo.DataSourceChanged -= this.objectInfo_DataSourceChanged;
			objectInfo.PropertyNameChanged -= this.objectInfo_PropertyNameChanged;
			objectInfo.PropertyEmptyValueChanged -= this.objectInfo_PropertyEmptyValueChanged;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00059ABC File Offset: 0x00057CBC
		private void summaryPanel_InsertItem(int rowIndex, GeneralPageSummaryInfo objectInfo)
		{
			Label label = new Label();
			label.Name = objectInfo.PropertyName.Replace('.', '_') + "Label";
			label.Text = objectInfo.Text;
			label.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			label.AutoSize = true;
			if (this.summaryControlStyle == SummaryControlStyle.VariableDescriptionSize)
			{
				int width = (int)Math.Floor(0.7 * (double)(this.DefaultSize.Width - base.Padding.Horizontal));
				label.MaximumSize = new Size(width, 0);
			}
			Control control = this.GetDescriptionControl(objectInfo);
			this.tableLayoutPanel.RowCount++;
			this.tableLayoutPanel.RowStyles.Insert(rowIndex, new RowStyle());
			for (int i = this.tableLayoutPanel.RowCount - 2; i >= rowIndex; i--)
			{
				Control controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(0, i);
				if (controlFromPosition != null)
				{
					this.tableLayoutPanel.SetRow(controlFromPosition, i + 1);
				}
				controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(1, i);
				if (controlFromPosition != null)
				{
					this.tableLayoutPanel.SetRow(controlFromPosition, i + 1);
				}
			}
			label.Margin = ((rowIndex == 0) ? this.firstRowMargin : this.nonFirstRowMargin);
			control.Margin = ((rowIndex == 0) ? this.firstRowMargin : this.nonFirstRowMargin);
			if (rowIndex == 0 && this.GetControlFromPosition(0, 0) != null)
			{
				this.GetControlFromPosition(0, 0).Margin = this.nonFirstRowMargin;
				this.GetControlFromPosition(1, 0).Margin = this.nonFirstRowMargin;
			}
			this.tableLayoutPanel.Controls.Add(label, 0, rowIndex);
			this.tableLayoutPanel.Controls.Add(control, 1, rowIndex);
			label.DataBindings.Add(new Binding("Text", objectInfo, "Text"));
			if (objectInfo.BindingSource != null && objectInfo.BindingSource.DataSource != null)
			{
				this.objectInfo_UpdateBinding(objectInfo);
			}
			objectInfo.BindingSourceChanged += this.objectInfo_BindingSourceChanged;
			objectInfo.DataSourceChanged += this.objectInfo_DataSourceChanged;
			objectInfo.PropertyNameChanged += this.objectInfo_PropertyNameChanged;
			objectInfo.PropertyEmptyValueChanged += this.objectInfo_PropertyEmptyValueChanged;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00059CE4 File Offset: 0x00057EE4
		private Control GetDescriptionControl(GeneralPageSummaryInfo objectInfo)
		{
			Control result = null;
			switch (this.descriptionControl)
			{
			case DescriptionControl.Label:
				result = new Label
				{
					Name = objectInfo.PropertyName.Replace('.', '_') + "TextBox",
					Dock = DockStyle.Top,
					AutoSize = true
				};
				break;
			case DescriptionControl.TextBox:
				result = new ExchangeTextBox
				{
					Name = objectInfo.PropertyName.Replace('.', '_') + "TextBox",
					ReadOnly = true,
					BorderStyle = BorderStyle.None,
					Dock = DockStyle.Top,
					FormatMode = objectInfo.FormatMode
				};
				break;
			}
			return result;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00059D8A File Offset: 0x00057F8A
		private void objectInfo_DataSourceChanged(object sender, EventArgs e)
		{
			this.objectInfo_UpdateBinding((GeneralPageSummaryInfo)sender);
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00059D98 File Offset: 0x00057F98
		private void objectInfo_PropertyNameChanged(object sender, EventArgs e)
		{
			this.objectInfo_UpdateBinding((GeneralPageSummaryInfo)sender);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00059DA6 File Offset: 0x00057FA6
		private void objectInfo_PropertyEmptyValueChanged(object sender, EventArgs e)
		{
			this.objectInfo_UpdateBinding((GeneralPageSummaryInfo)sender);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00059DB4 File Offset: 0x00057FB4
		private void objectInfo_BindingSourceChanged(object sender, EventArgs e)
		{
			this.objectInfo_UpdateBinding((GeneralPageSummaryInfo)sender);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00059E14 File Offset: 0x00058014
		private void objectInfo_UpdateBinding(GeneralPageSummaryInfo objectInfo)
		{
			Control controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(1, this.SummaryInfoCollection.IndexOf(objectInfo));
			if (controlFromPosition != null)
			{
				Binding binding = controlFromPosition.DataBindings["Text"];
				if (binding != null)
				{
					controlFromPosition.DataBindings.Remove(binding);
				}
				if (objectInfo.BindingSource != null && objectInfo.BindingSource.DataSource != null)
				{
					binding = new Binding("Text", objectInfo.BindingSource, objectInfo.PropertyName, true, DataSourceUpdateMode.Never, string.Empty);
					controlFromPosition.DataBindings.Add(binding);
					binding.Format += delegate(object sender, ConvertEventArgs e)
					{
						if (binding.DataSourceUpdateMode == DataSourceUpdateMode.Never && (e.Value == null || string.IsNullOrEmpty(e.Value.ToString())))
						{
							e.Value = objectInfo.PropertyEmptyValue;
						}
					};
					if (objectInfo.BindingSource.DataSource is Type)
					{
						controlFromPosition.Text = objectInfo.PropertyEmptyValue;
						return;
					}
					binding.ReadValue();
				}
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00059F43 File Offset: 0x00058143
		public override Size GetPreferredSize(Size proposedSize)
		{
			return this.tableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00059F54 File Offset: 0x00058154
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new TableLayoutPanel();
			base.SuspendLayout();
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.TabIndex = 0;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "ExchangeSummaryControl";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00059FF9 File Offset: 0x000581F9
		protected override Size DefaultSize
		{
			get
			{
				return new Size(443, 496);
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0005A00A File Offset: 0x0005820A
		public int RowCount
		{
			get
			{
				return this.tableLayoutPanel.RowCount;
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0005A017 File Offset: 0x00058217
		public Control GetControlFromPosition(int columnIndex, int rowIndex)
		{
			return this.tableLayoutPanel.GetControlFromPosition(columnIndex, rowIndex);
		}

		// Token: 0x040007E1 RID: 2017
		private SummaryControlStyle summaryControlStyle;

		// Token: 0x040007E2 RID: 2018
		private DescriptionControl descriptionControl;

		// Token: 0x040007E3 RID: 2019
		private TableLayoutPanel tableLayoutPanel;

		// Token: 0x040007E4 RID: 2020
		private ChangeNotifyingCollection<GeneralPageSummaryInfo> objectInfoCollection = new ChangeNotifyingCollection<GeneralPageSummaryInfo>();

		// Token: 0x040007E5 RID: 2021
		private Padding firstRowMargin = new Padding(0, 0, 0, 0);

		// Token: 0x040007E6 RID: 2022
		private Padding nonFirstRowMargin = new Padding(0, 12, 0, 0);
	}
}
