namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000149 RID: 329
	public partial class MessageBoxDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x000313DC File Offset: 0x0002F5DC
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.captionLabel = new global::System.Windows.Forms.Label();
			this.messagePanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.autoScrollPanel = new global::System.Windows.Forms.Panel();
			this.messageScrollLabelAutoSizePanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.messageScrollLabel = new global::System.Windows.Forms.Label();
			this.messageLabel = new global::System.Windows.Forms.Label();
			this.buttonsPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.noButton = new global::Microsoft.ManagementGUI.WinForms.ExchangeButton();
			this.yesButton = new global::Microsoft.ManagementGUI.WinForms.ExchangeButton();
			this.cancelButton = new global::Microsoft.ManagementGUI.WinForms.ExchangeButton();
			this.okButton = new global::Microsoft.ManagementGUI.WinForms.ExchangeButton();
			this.iconPictureBox = new global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangePictureBox();
			this.tableLayoutPanel.SuspendLayout();
			this.messagePanel.SuspendLayout();
			this.autoScrollPanel.SuspendLayout();
			this.messageScrollLabelAutoSizePanel.SuspendLayout();
			this.buttonsPanel.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.iconPictureBox).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 32f));
			this.tableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 16f));
			this.tableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.captionLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.messagePanel, 2, 1);
			this.tableLayoutPanel.Controls.Add(this.buttonsPanel, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.iconPictureBox, 0, 1);
			this.tableLayoutPanel.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new global::System.Drawing.Point(12, 12);
			this.tableLayoutPanel.Margin = new global::System.Windows.Forms.Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 4;
			this.tableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 0f));
			this.tableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 16f));
			this.tableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new global::System.Drawing.Size(610, 424);
			this.tableLayoutPanel.TabIndex = 0;
			this.captionLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.captionLabel.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan(this.captionLabel, 3);
			this.captionLabel.ForeColor = this.captionLabel.BackColor;
			this.captionLabel.Location = new global::System.Drawing.Point(3, 0);
			this.captionLabel.Name = "captionLabel";
			this.captionLabel.Size = new global::System.Drawing.Size(604, 1);
			this.captionLabel.TabIndex = 3;
			this.captionLabel.Text = "captionLabel";
			this.messagePanel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.messagePanel.AutoSize = true;
			this.messagePanel.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.messagePanel.ColumnCount = 2;
			this.messagePanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.messagePanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.messagePanel.Controls.Add(this.autoScrollPanel, 1, 0);
			this.messagePanel.Controls.Add(this.messageLabel, 0, 0);
			this.messagePanel.Location = new global::System.Drawing.Point(48, 3);
			this.messagePanel.Margin = new global::System.Windows.Forms.Padding(0, 3, 0, 0);
			this.messagePanel.Name = "messagePanel";
			this.messagePanel.RowCount = 1;
			this.messagePanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.messagePanel.Size = new global::System.Drawing.Size(562, 382);
			this.messagePanel.TabIndex = 4;
			this.autoScrollPanel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.autoScrollPanel.AutoScroll = true;
			this.autoScrollPanel.Controls.Add(this.messageScrollLabelAutoSizePanel);
			this.autoScrollPanel.Location = new global::System.Drawing.Point(284, 3);
			this.autoScrollPanel.Name = "autoScrollPanel";
			this.autoScrollPanel.Size = new global::System.Drawing.Size(275, 376);
			this.autoScrollPanel.TabIndex = 1;
			this.messageScrollLabelAutoSizePanel.AutoSize = true;
			this.messageScrollLabelAutoSizePanel.ColumnCount = 1;
			this.messageScrollLabelAutoSizePanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.messageScrollLabelAutoSizePanel.Controls.Add(this.messageScrollLabel, 0, 0);
			this.messageScrollLabelAutoSizePanel.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.messageScrollLabelAutoSizePanel.Location = new global::System.Drawing.Point(0, 0);
			this.messageScrollLabelAutoSizePanel.Name = "messageScrollLabelAutoSizePanel";
			this.messageScrollLabelAutoSizePanel.RowCount = 1;
			this.messageScrollLabelAutoSizePanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.messageScrollLabelAutoSizePanel.Size = new global::System.Drawing.Size(275, 13);
			this.messageScrollLabelAutoSizePanel.TabIndex = 2;
			this.messageScrollLabel.AutoSize = true;
			this.messageScrollLabel.Location = new global::System.Drawing.Point(3, 0);
			this.messageScrollLabel.Name = "messageScrollLabel";
			this.messageScrollLabel.Size = new global::System.Drawing.Size(101, 13);
			this.messageScrollLabel.TabIndex = 0;
			this.messageScrollLabel.Text = "messageScrollLabel";
			this.messageScrollLabel.UseMnemonic = false;
			this.messageLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.messageLabel.AutoSize = true;
			this.messageLabel.Location = new global::System.Drawing.Point(0, 0);
			this.messageLabel.Margin = new global::System.Windows.Forms.Padding(0);
			this.messageLabel.MaximumSize = new global::System.Drawing.Size(562, 385);
			this.messageLabel.Name = "messageLabel";
			this.messageLabel.Size = new global::System.Drawing.Size(281, 382);
			this.messageLabel.TabIndex = 0;
			this.messageLabel.Text = "messageLabel";
			this.messageLabel.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.messageLabel.UseMnemonic = false;
			this.buttonsPanel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonsPanel.AutoSize = true;
			this.buttonsPanel.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonsPanel.ColumnCount = 7;
			this.tableLayoutPanel.SetColumnSpan(this.buttonsPanel, 3);
			this.buttonsPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.buttonsPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 8f));
			this.buttonsPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.buttonsPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 8f));
			this.buttonsPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.buttonsPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 8f));
			this.buttonsPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.buttonsPanel.Controls.Add(this.noButton, 4, 0);
			this.buttonsPanel.Controls.Add(this.yesButton, 2, 0);
			this.buttonsPanel.Controls.Add(this.cancelButton, 6, 0);
			this.buttonsPanel.Controls.Add(this.okButton, 0, 0);
			this.buttonsPanel.Location = new global::System.Drawing.Point(266, 401);
			this.buttonsPanel.Margin = new global::System.Windows.Forms.Padding(0);
			this.buttonsPanel.MinimumSize = new global::System.Drawing.Size(0, 23);
			this.buttonsPanel.Name = "buttonsPanel";
			this.buttonsPanel.RowCount = 1;
			this.buttonsPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.buttonsPanel.Size = new global::System.Drawing.Size(344, 23);
			this.buttonsPanel.TabIndex = 2;
			this.noButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.noButton.AutoSize = true;
			this.noButton.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.noButton.DialogResult = global::System.Windows.Forms.DialogResult.No;
			this.noButton.Location = new global::System.Drawing.Point(176, 0);
			this.noButton.Margin = new global::System.Windows.Forms.Padding(0);
			this.noButton.MinimumSize = new global::System.Drawing.Size(75, 23);
			this.noButton.Name = "noButton";
			this.noButton.Size = new global::System.Drawing.Size(80, 23);
			this.noButton.TabIndex = 2;
			this.noButton.Text = "noButton";
			this.yesButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.yesButton.AutoSize = true;
			this.yesButton.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.yesButton.DialogResult = global::System.Windows.Forms.DialogResult.Yes;
			this.yesButton.Location = new global::System.Drawing.Point(88, 0);
			this.yesButton.Margin = new global::System.Windows.Forms.Padding(0);
			this.yesButton.MinimumSize = new global::System.Drawing.Size(75, 23);
			this.yesButton.Name = "yesButton";
			this.yesButton.Size = new global::System.Drawing.Size(80, 23);
			this.yesButton.TabIndex = 1;
			this.yesButton.Text = "yesButton";
			this.cancelButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.cancelButton.AutoSize = true;
			this.cancelButton.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new global::System.Drawing.Point(264, 0);
			this.cancelButton.Margin = new global::System.Windows.Forms.Padding(0);
			this.cancelButton.MinimumSize = new global::System.Drawing.Size(75, 23);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new global::System.Drawing.Size(80, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "cancelButton";
			this.okButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.okButton.AutoSize = true;
			this.okButton.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new global::System.Drawing.Point(0, 0);
			this.okButton.Margin = new global::System.Windows.Forms.Padding(0);
			this.okButton.MinimumSize = new global::System.Drawing.Size(75, 23);
			this.okButton.Name = "okButton";
			this.okButton.Size = new global::System.Drawing.Size(80, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "okButton";
			this.iconPictureBox.Location = new global::System.Drawing.Point(0, 3);
			this.iconPictureBox.Margin = new global::System.Windows.Forms.Padding(0, 3, 0, 0);
			this.iconPictureBox.MinimumSize = new global::System.Drawing.Size(32, 32);
			this.iconPictureBox.Name = "iconPictureBox";
			this.iconPictureBox.Size = new global::System.Drawing.Size(32, 32);
			this.iconPictureBox.TabIndex = 1;
			this.iconPictureBox.TabStop = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.CancelButton = this.cancelButton;
			base.ClientSize = new global::System.Drawing.Size(634, 448);
			base.Controls.Add(this.tableLayoutPanel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			this.MaximumSize = new global::System.Drawing.Size(640, 480);
			base.MinimizeBox = false;
			base.Name = "MessageBoxDialog";
			base.Padding = new global::System.Windows.Forms.Padding(12);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.messagePanel.ResumeLayout(false);
			this.messagePanel.PerformLayout();
			this.autoScrollPanel.ResumeLayout(false);
			this.autoScrollPanel.PerformLayout();
			this.messageScrollLabelAutoSizePanel.ResumeLayout(false);
			this.messageScrollLabelAutoSizePanel.PerformLayout();
			this.buttonsPanel.ResumeLayout(false);
			this.buttonsPanel.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.iconPictureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400054C RID: 1356
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel;

		// Token: 0x0400054D RID: 1357
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangePictureBox iconPictureBox;

		// Token: 0x0400054E RID: 1358
		private global::System.Windows.Forms.TableLayoutPanel buttonsPanel;

		// Token: 0x0400054F RID: 1359
		private global::Microsoft.ManagementGUI.WinForms.ExchangeButton okButton;

		// Token: 0x04000550 RID: 1360
		private global::Microsoft.ManagementGUI.WinForms.ExchangeButton cancelButton;

		// Token: 0x04000551 RID: 1361
		private global::Microsoft.ManagementGUI.WinForms.ExchangeButton noButton;

		// Token: 0x04000552 RID: 1362
		private global::Microsoft.ManagementGUI.WinForms.ExchangeButton yesButton;

		// Token: 0x04000553 RID: 1363
		private global::System.Windows.Forms.Label captionLabel;

		// Token: 0x04000554 RID: 1364
		private global::System.Windows.Forms.TableLayoutPanel messagePanel;

		// Token: 0x04000555 RID: 1365
		private global::System.Windows.Forms.Label messageLabel;

		// Token: 0x04000556 RID: 1366
		private global::System.Windows.Forms.Panel autoScrollPanel;

		// Token: 0x04000557 RID: 1367
		private global::System.Windows.Forms.TableLayoutPanel messageScrollLabelAutoSizePanel;

		// Token: 0x04000558 RID: 1368
		private global::System.Windows.Forms.Label messageScrollLabel;
	}
}
