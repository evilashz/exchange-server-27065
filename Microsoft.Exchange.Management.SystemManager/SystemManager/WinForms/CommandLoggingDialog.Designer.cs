namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000119 RID: 281
	public partial class CommandLoggingDialog : global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangeForm
	{
		// Token: 0x06000AD4 RID: 2772 RVA: 0x00026BC3 File Offset: 0x00024DC3
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00026BE4 File Offset: 0x00024DE4
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.statusStrip = new global::System.Windows.Forms.StatusStrip();
			this.selectedCountLabel = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.startLoggingDate = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.resultListView = new global::Microsoft.Exchange.Management.SystemManager.WinForms.DataListView();
			this.outputPanel = new global::Microsoft.Exchange.Management.SystemManager.WinForms.AutoSizePanel();
			this.outputTextBox = new global::System.Windows.Forms.TextBox();
			this.menuStrip = new global::System.Windows.Forms.MenuStrip();
			this.fileMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.actionMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.switchCommandLoggingToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.clearLogToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.helpCommandLoggingToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.copyCommandsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.modifyMaximumRecordToLogToolStripMenuItem = new global::Microsoft.Exchange.Management.SystemManager.WinForms.CommandToolStripMenuItem(this.components);
			this.exportListToolStripMenuItem = new global::Microsoft.Exchange.Management.SystemManager.WinForms.CommandToolStripMenuItem(this.components);
			this.addRemoveColumnsToolStripMenuItem = new global::Microsoft.Exchange.Management.SystemManager.WinForms.CommandToolStripMenuItem(this.components);
			this.splitContainer = new global::System.Windows.Forms.SplitContainer();
			this.statusStrip.SuspendLayout();
			this.outputPanel.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			base.SuspendLayout();
			this.statusStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.selectedCountLabel,
				this.startLoggingDate
			});
			this.statusStrip.LayoutStyle = global::System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.statusStrip.Location = new global::System.Drawing.Point(0, 411);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new global::System.Drawing.Size(498, 22);
			this.statusStrip.TabIndex = 0;
			this.selectedCountLabel.Name = "selectedCountLabel";
			this.selectedCountLabel.Size = new global::System.Drawing.Size(0, 17);
			this.selectedCountLabel.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.startLoggingDate.Alignment = global::System.Windows.Forms.ToolStripItemAlignment.Right;
			this.startLoggingDate.Name = "startLoggingDate";
			this.startLoggingDate.Size = new global::System.Drawing.Size(0, 17);
			this.startLoggingDate.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.resultListView.AutoGenerateColumns = false;
			this.resultListView.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.resultListView.DataSourceRefresher = null;
			this.resultListView.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.resultListView.Location = new global::System.Drawing.Point(0, 0);
			this.resultListView.Name = "resultListView";
			this.resultListView.Size = new global::System.Drawing.Size(498, 260);
			this.resultListView.TabIndex = 0;
			this.resultListView.UseCompatibleStateImageBehavior = false;
			this.resultListView.VirtualMode = true;
			this.outputPanel.BackColor = global::System.Drawing.SystemColors.Window;
			this.outputPanel.Controls.Add(this.outputTextBox);
			this.outputPanel.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.outputPanel.Location = new global::System.Drawing.Point(0, 0);
			this.outputPanel.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
			this.outputPanel.Name = "outputPanel";
			this.outputPanel.Size = new global::System.Drawing.Size(498, 123);
			this.outputPanel.TabIndex = 0;
			this.outputTextBox.BackColor = global::System.Drawing.SystemColors.Window;
			this.outputTextBox.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.outputTextBox.Location = new global::System.Drawing.Point(0, 0);
			this.outputTextBox.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
			this.outputTextBox.Multiline = true;
			this.outputTextBox.Name = "outputTextBox";
			this.outputTextBox.ReadOnly = true;
			this.outputTextBox.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.outputTextBox.Size = new global::System.Drawing.Size(498, 123);
			this.outputTextBox.TabIndex = 0;
			this.menuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fileMenuItem,
				this.actionMenuItem
			});
			this.menuStrip.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.RenderMode = global::System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip.Size = new global::System.Drawing.Size(498, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip";
			this.fileMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.closeToolStripMenuItem
			});
			this.fileMenuItem.Name = "fileMenuItem";
			this.fileMenuItem.Size = new global::System.Drawing.Size(12, 20);
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)262259;
			this.closeToolStripMenuItem.Size = new global::System.Drawing.Size(109, 22);
			this.actionMenuItem.Name = "actionMenuItem";
			this.actionMenuItem.Size = new global::System.Drawing.Size(12, 20);
			this.switchCommandLoggingToolStripMenuItem.Name = "switchCommandLoggingToolStripMenuItem";
			this.switchCommandLoggingToolStripMenuItem.Size = new global::System.Drawing.Size(107, 22);
			this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
			this.clearLogToolStripMenuItem.Size = new global::System.Drawing.Size(107, 22);
			this.helpCommandLoggingToolStripMenuItem.Name = "helpCommandLoggingToolStripMenuItem";
			this.helpCommandLoggingToolStripMenuItem.Size = new global::System.Drawing.Size(107, 22);
			this.copyCommandsToolStripMenuItem.Name = "copyCommandsToolStripMenuItem";
			this.copyCommandsToolStripMenuItem.Size = new global::System.Drawing.Size(107, 22);
			this.modifyMaximumRecordToLogToolStripMenuItem.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.modifyMaximumRecordToLogToolStripMenuItem.Name = "modifyMaximumRecordToLogToolStripMenuItem";
			this.modifyMaximumRecordToLogToolStripMenuItem.Size = new global::System.Drawing.Size(32, 19);
			this.exportListToolStripMenuItem.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.exportListToolStripMenuItem.Name = "exportListToolStripMenuItem";
			this.exportListToolStripMenuItem.Size = new global::System.Drawing.Size(32, 19);
			this.addRemoveColumnsToolStripMenuItem.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.addRemoveColumnsToolStripMenuItem.Name = "addRemoveColumnsToolStripMenuItem";
			this.addRemoveColumnsToolStripMenuItem.Size = new global::System.Drawing.Size(32, 19);
			this.splitContainer.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new global::System.Drawing.Point(0, 24);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = global::System.Windows.Forms.Orientation.Horizontal;
			this.splitContainer.Panel1.Controls.Add(this.resultListView);
			this.splitContainer.Panel2.Controls.Add(this.outputPanel);
			this.splitContainer.Size = new global::System.Drawing.Size(498, 387);
			this.splitContainer.SplitterDistance = 260;
			this.splitContainer.TabIndex = 1;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(498, 433);
			base.Controls.Add(this.splitContainer);
			base.Controls.Add(this.menuStrip);
			base.Controls.Add(this.statusStrip);
			base.MainMenuStrip = this.menuStrip;
			this.MinimumSize = new global::System.Drawing.Size(400, 370);
			base.Name = "CommandLoggingDialog";
			this.Text = "CommandLoggingDialog";
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.outputPanel.ResumeLayout(false);
			this.outputPanel.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			this.splitContainer.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400047A RID: 1146
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400047B RID: 1147
		private global::System.Windows.Forms.MenuStrip menuStrip;

		// Token: 0x0400047C RID: 1148
		private global::System.Windows.Forms.StatusStrip statusStrip;

		// Token: 0x0400047D RID: 1149
		private global::System.Windows.Forms.ToolStripStatusLabel selectedCountLabel;

		// Token: 0x0400047E RID: 1150
		private global::System.Windows.Forms.ToolStripStatusLabel startLoggingDate;

		// Token: 0x0400047F RID: 1151
		private global::System.Windows.Forms.ToolStripMenuItem fileMenuItem;

		// Token: 0x04000480 RID: 1152
		private global::System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;

		// Token: 0x04000481 RID: 1153
		private global::System.Windows.Forms.ToolStripMenuItem switchCommandLoggingToolStripMenuItem;

		// Token: 0x04000482 RID: 1154
		private global::System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;

		// Token: 0x04000483 RID: 1155
		private global::System.Windows.Forms.ToolStripMenuItem helpCommandLoggingToolStripMenuItem;

		// Token: 0x04000484 RID: 1156
		private global::System.Windows.Forms.ToolStripMenuItem copyCommandsToolStripMenuItem;

		// Token: 0x04000485 RID: 1157
		private global::System.Windows.Forms.ToolStripMenuItem actionMenuItem;

		// Token: 0x04000486 RID: 1158
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.CommandToolStripMenuItem addRemoveColumnsToolStripMenuItem;

		// Token: 0x04000487 RID: 1159
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.CommandToolStripMenuItem exportListToolStripMenuItem;

		// Token: 0x04000488 RID: 1160
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.CommandToolStripMenuItem modifyMaximumRecordToLogToolStripMenuItem;

		// Token: 0x04000489 RID: 1161
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.AutoSizePanel outputPanel;

		// Token: 0x0400048A RID: 1162
		private global::System.Windows.Forms.TextBox outputTextBox;

		// Token: 0x0400048B RID: 1163
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.DataListView resultListView;

		// Token: 0x0400048C RID: 1164
		private global::System.Windows.Forms.SplitContainer splitContainer;
	}
}
