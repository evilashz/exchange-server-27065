namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001FE RID: 510
	public partial class PromptDialog : global::Microsoft.Exchange.Management.SystemManager.WinForms.PropertyPageDialog
	{
		// Token: 0x06001750 RID: 5968 RVA: 0x0006227F File Offset: 0x0006047F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000622A0 File Offset: 0x000604A0
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.panelAll = new global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangePropertyPageControl();
			this.tableLayoutPanel = new global::Microsoft.Exchange.Management.SystemManager.WinForms.AutoTableLayoutPanel();
			this.panelIcon = new global::Microsoft.Exchange.Management.SystemManager.WinForms.AutoTableLayoutPanel();
			this.pictureBoxIcon = new global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangePictureBox();
			this.textBoxContent = new global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangeTextBox();
			this.labelContentLabel = new global::System.Windows.Forms.Label();
			this.labelMessage = new global::System.Windows.Forms.Label();
			this.exampleLabel = new global::System.Windows.Forms.Label();
			this.panelAll.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.panelIcon.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxIcon).BeginInit();
			base.SuspendLayout();
			this.panelAll.AutoSize = true;
			this.panelAll.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelAll.Controls.Add(this.tableLayoutPanel);
			this.panelAll.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panelAll.Location = new global::System.Drawing.Point(0, 0);
			this.panelAll.Margin = new global::System.Windows.Forms.Padding(0);
			this.panelAll.Name = "panelAll";
			this.panelAll.Size = new global::System.Drawing.Size(421, 85);
			this.panelAll.TabIndex = 0;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.panelIcon, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.labelContentLabel, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.labelMessage, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.exampleLabel, 1, 3);
			this.tableLayoutPanel.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.AutoLayout = false;
			this.tableLayoutPanel.Location = new global::System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Margin = new global::System.Windows.Forms.Padding(0);
			this.tableLayoutPanel.MaximumSize = new global::System.Drawing.Size(421, 418);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new global::System.Windows.Forms.Padding(12, 12, 16, 12);
			this.tableLayoutPanel.RowCount = 4;
			this.tableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new global::System.Drawing.Size(421, 85);
			this.tableLayoutPanel.TabIndex = 0;
			this.panelIcon.AutoSize = true;
			this.panelIcon.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelIcon.BackColor = global::System.Drawing.SystemColors.Control;
			this.panelIcon.ColumnCount = 1;
			this.panelIcon.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.panelIcon.Controls.Add(this.pictureBoxIcon, 0, 0);
			this.panelIcon.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panelIcon.AutoLayout = true;
			this.panelIcon.Location = new global::System.Drawing.Point(12, 12);
			this.panelIcon.Margin = new global::System.Windows.Forms.Padding(0);
			this.panelIcon.Name = "panelIcon";
			this.panelIcon.Padding = new global::System.Windows.Forms.Padding(0, 0, 16, 0);
			this.panelIcon.RowCount = 1;
			this.tableLayoutPanel.SetRowSpan(this.panelIcon, 3);
			this.panelIcon.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.panelIcon.Size = new global::System.Drawing.Size(51, 32);
			this.panelIcon.TabIndex = 0;
			this.panelIcon.Visible = false;
			this.pictureBoxIcon.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.pictureBoxIcon.Location = new global::System.Drawing.Point(3, 0);
			this.pictureBoxIcon.Margin = new global::System.Windows.Forms.Padding(3, 0, 0, 0);
			this.pictureBoxIcon.Name = "pictureBoxIcon";
			this.pictureBoxIcon.Size = new global::System.Drawing.Size(32, 32);
			this.pictureBoxIcon.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxIcon.TabIndex = 3;
			this.pictureBoxIcon.TabStop = false;
			this.labelContentLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.labelContentLabel.AutoSize = true;
			this.labelContentLabel.BackColor = global::System.Drawing.SystemColors.Control;
			this.labelContentLabel.Location = new global::System.Drawing.Point(63, 37);
			this.labelContentLabel.Margin = new global::System.Windows.Forms.Padding(0);
			this.labelContentLabel.Name = "labelContentLabel";
			this.labelContentLabel.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 3);
			this.labelContentLabel.Size = new global::System.Drawing.Size(349, 16);
			this.labelContentLabel.TabIndex = 2;
			this.labelMessage.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.labelMessage.AutoSize = true;
			this.labelMessage.BackColor = global::System.Drawing.SystemColors.Control;
			this.labelMessage.Location = new global::System.Drawing.Point(63, 12);
			this.labelMessage.Margin = new global::System.Windows.Forms.Padding(0);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 12);
			this.labelMessage.Size = new global::System.Drawing.Size(349, 25);
			this.labelMessage.TabIndex = 1;
			this.labelMessage.Visible = false;
			this.exampleLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.exampleLabel.AutoSize = true;
			this.exampleLabel.BackColor = global::System.Drawing.SystemColors.Control;
			this.exampleLabel.Location = new global::System.Drawing.Point(63, 37);
			this.exampleLabel.Margin = new global::System.Windows.Forms.Padding(0);
			this.exampleLabel.Name = "exampleLabel";
			this.exampleLabel.Visible = false;
			this.exampleLabel.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 3);
			this.exampleLabel.Size = new global::System.Drawing.Size(349, 16);
			this.exampleLabel.TabIndex = 4;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.CancelVisible = true;
			base.ClientSize = new global::System.Drawing.Size(421, 124);
			base.Controls.Add(this.panelAll);
			this.MaximumSize = new global::System.Drawing.Size(500, 496);
			base.Name = "PromptDialog";
			this.Text = "PromptDialog";
			base.Controls.SetChildIndex(this.panelAll, 0);
			this.panelAll.ResumeLayout(false);
			this.panelAll.PerformLayout();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.panelIcon.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxIcon).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040008B0 RID: 2224
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.AutoTableLayoutPanel panelIcon;

		// Token: 0x040008B1 RID: 2225
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangePictureBox pictureBoxIcon;

		// Token: 0x040008B3 RID: 2227
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040008B4 RID: 2228
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangePropertyPageControl panelAll;

		// Token: 0x040008B5 RID: 2229
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x040008B6 RID: 2230
		private global::System.Windows.Forms.Label labelMessage;

		// Token: 0x040008B7 RID: 2231
		private global::System.Windows.Forms.Label labelContentLabel;

		// Token: 0x040008B8 RID: 2232
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangeTextBox textBoxContent;

		// Token: 0x040008BA RID: 2234
		private global::System.Windows.Forms.Label exampleLabel;
	}
}
