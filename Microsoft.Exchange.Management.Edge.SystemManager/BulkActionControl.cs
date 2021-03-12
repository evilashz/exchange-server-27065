using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x02000002 RID: 2
	public class BulkActionControl : ExchangePropertyPageControl
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public BulkActionControl()
		{
			this.InitializeComponent();
			this.Text = Strings.BulkActionTitleText;
			this.warningIconPictureBox.Image = IconLibrary.ToBitmap(Icons.Warning, this.warningIconPictureBox.Size);
			this.bulkActionLabel.Text = Strings.BulkActionLabelText;
			this.expandScopeCheckBox.Text = Strings.BulkActionCheckBoxText;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002143 File Offset: 0x00000343
		public override Size GetPreferredSize(Size proposedSize)
		{
			proposedSize.Width = base.Width;
			return this.tableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002160 File Offset: 0x00000360
		private void InitializeComponent()
		{
			this.warningIconPictureBox = new ExchangePictureBox();
			this.bulkActionLabel = new Label();
			this.expandScopeCheckBox = new AutoHeightCheckBox();
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			((ISupportInitialize)base.BindingSource).BeginInit();
			((ISupportInitialize)this.warningIconPictureBox).BeginInit();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			base.InputValidationProvider.SetEnabled(base.BindingSource, true);
			this.warningIconPictureBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.warningIconPictureBox.Location = new Point(16, 12);
			this.warningIconPictureBox.Margin = new Padding(3, 0, 0, 0);
			this.warningIconPictureBox.Name = "warningIconPictureBox";
			this.warningIconPictureBox.Size = new Size(32, 32);
			this.warningIconPictureBox.TabIndex = 0;
			this.warningIconPictureBox.TabStop = false;
			this.bulkActionLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.bulkActionLabel.Location = new Point(60, 12);
			this.bulkActionLabel.Margin = new Padding(0);
			this.bulkActionLabel.Name = "bulkActionLabel";
			this.bulkActionLabel.Size = new Size(147, 16);
			this.bulkActionLabel.TabIndex = 1;
			this.bulkActionLabel.Text = "bulkActionLabel";
			this.expandScopeCheckBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.expandScopeCheckBox.Location = new Point(63, 52);
			this.expandScopeCheckBox.Margin = new Padding(3, 8, 0, 0);
			this.expandScopeCheckBox.Name = "expandScopeCheckBox";
			this.expandScopeCheckBox.Size = new Size(144, 17);
			this.expandScopeCheckBox.TabIndex = 0;
			this.expandScopeCheckBox.Text = "expandScopeCheckBox";
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 12f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.ContainerType = ContainerType.Control;
			this.tableLayoutPanel.Controls.Add(this.expandScopeCheckBox, 2, 1);
			this.tableLayoutPanel.Controls.Add(this.bulkActionLabel, 2, 0);
			this.tableLayoutPanel.Controls.Add(this.warningIconPictureBox, 0, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new Padding(13, 12, 16, 12);
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(223, 81);
			this.tableLayoutPanel.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "BulkActionControl";
			base.Size = new Size(418, 96);
			((ISupportInitialize)base.BindingSource).EndInit();
			((ISupportInitialize)this.warningIconPictureBox).EndInit();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002546 File Offset: 0x00000746
		[DefaultValue("false")]
		public bool IsExpandScopeSelected
		{
			get
			{
				return this.expandScopeCheckBox.Checked;
			}
		}

		// Token: 0x04000001 RID: 1
		private ExchangePictureBox warningIconPictureBox;

		// Token: 0x04000002 RID: 2
		private Label bulkActionLabel;

		// Token: 0x04000003 RID: 3
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x04000004 RID: 4
		private AutoHeightCheckBox expandScopeCheckBox;
	}
}
