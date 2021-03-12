using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000126 RID: 294
	public class DataListPropertyPage : ExchangePropertyPageControl
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x0002A00B File Offset: 0x0002820B
		public DataListPropertyPage()
		{
			this.InitializeComponent();
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0002A019 File Offset: 0x00028219
		public DataListControl DataListControl
		{
			get
			{
				return this.dataListControl;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0002A021 File Offset: 0x00028221
		protected DataListView DataListView
		{
			get
			{
				return this.DataListControl.DataListView;
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002A02E File Offset: 0x0002822E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002A050 File Offset: 0x00028250
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.dataListControl = new DataListControl();
			((ISupportInitialize)base.BindingSource).BeginInit();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.ContainerType = ContainerType.PropertyPage;
			this.tableLayoutPanel.Controls.Add(this.dataListControl, 0, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new Padding(13, 12, 16, 12);
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(418, 396);
			this.tableLayoutPanel.TabIndex = 0;
			this.dataListControl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.dataListControl.AutoSize = true;
			this.dataListControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.dataListControl.DataListView.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.dataListControl.DataListView.DataSourceRefresher = null;
			this.dataListControl.DataListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.dataListControl.DataListView.Location = new Point(3, 0);
			this.dataListControl.DataListView.Margin = new Padding(3, 0, 0, 0);
			this.dataListControl.DataListView.Name = "dataListView";
			this.dataListControl.DataListView.Size = new Size(386, 372);
			this.dataListControl.DataListView.TabIndex = 4;
			this.dataListControl.DataListView.UseCompatibleStateImageBehavior = false;
			this.dataListControl.Location = new Point(13, 12);
			this.dataListControl.Margin = new Padding(0);
			this.dataListControl.Name = "dataListControl";
			this.dataListControl.Size = new Size(389, 372);
			this.dataListControl.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "DataListPropertyPage";
			((ISupportInitialize)base.BindingSource).EndInit();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040004C7 RID: 1223
		private IContainer components;

		// Token: 0x040004C8 RID: 1224
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x040004C9 RID: 1225
		private DataListControl dataListControl;
	}
}
