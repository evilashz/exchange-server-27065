namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000230 RID: 560
	public partial class TreeViewObjectPickerForm : global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangeForm, global::Microsoft.Exchange.Management.SystemManager.WinForms.ISelectedObjectsProvider
	{
		// Token: 0x060019FC RID: 6652 RVA: 0x00070D94 File Offset: 0x0006EF94
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.components.Dispose();
				if (this.ObjectPicker != null)
				{
					this.ObjectPicker.DataTableLoader.RefreshCompleted -= new global::System.ComponentModel.RunWorkerCompletedEventHandler(this.DataTableLoader_RefreshCompleted);
					this.ObjectPicker.DataTableLoader.ProgressChanged -= new global::Microsoft.Exchange.Management.SystemManager.RefreshProgressChangedEventHandler(this.DataTableLoader_ProgressChanged);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00071300 File Offset: 0x0006F500
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.dialogButtonsPanel = new global::System.Windows.Forms.FlowLayoutPanel();
			this.helpButton = new global::Microsoft.ManagementGUI.WinForms.ExchangeButton();
			this.cancelButton = new global::Microsoft.ManagementGUI.WinForms.ExchangeButton();
			this.okButton = new global::Microsoft.ManagementGUI.WinForms.ExchangeButton();
			this.statusStrip = new global::System.Windows.Forms.StatusStrip();
			this.selectedCountLabel = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.loadStatusLabel = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.loadProgressBar = new global::Microsoft.ManagementGUI.WinForms.ExchangeToolStripProgressBar();
			this.toolStripContainer = new global::System.Windows.Forms.ToolStripContainer();
			this.menuStrip = new global::System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.resultTreeView = new global::Microsoft.ManagementGUI.WinForms.DataTreeView();
			this.dialogButtonsPanel.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.menuStrip.SuspendLayout();
			base.SuspendLayout();
			this.dialogButtonsPanel.AutoSize = true;
			this.dialogButtonsPanel.Controls.Add(this.helpButton);
			this.dialogButtonsPanel.Controls.Add(this.cancelButton);
			this.dialogButtonsPanel.Controls.Add(this.okButton);
			this.dialogButtonsPanel.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.dialogButtonsPanel.FlowDirection = global::System.Windows.Forms.FlowDirection.RightToLeft;
			this.dialogButtonsPanel.Location = new global::System.Drawing.Point(0, 358);
			this.dialogButtonsPanel.Name = "dialogButtonsPanel";
			this.dialogButtonsPanel.Size = new global::System.Drawing.Size(498, 29);
			this.dialogButtonsPanel.TabIndex = 4;
			this.dialogButtonsPanel.TabStop = true;
			this.helpButton.AutoSize = true;
			this.helpButton.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.helpButton.FocusedAlwaysOnClick = false;
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new global::System.Drawing.Size(75, 23);
			this.helpButton.TabIndex = 2;
			this.helpButton.Click += delegate(object param0, global::System.EventArgs param1)
			{
				this.OnHelpRequested(new global::System.Windows.Forms.HelpEventArgs(global::System.Drawing.Point.Empty));
			};
			this.cancelButton.AutoSize = true;
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.FocusedAlwaysOnClick = false;
			this.cancelButton.Location = new global::System.Drawing.Point(420, 3);
			this.cancelButton.MinimumSize = new global::System.Drawing.Size(75, 23);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new global::System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.okButton.AutoSize = true;
			this.okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.okButton.Enabled = false;
			this.okButton.FocusedAlwaysOnClick = false;
			this.okButton.Location = new global::System.Drawing.Point(339, 3);
			this.okButton.MinimumSize = new global::System.Drawing.Size(75, 23);
			this.okButton.Name = "okButton";
			this.okButton.Size = new global::System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.statusStrip.Dock = global::System.Windows.Forms.DockStyle.None;
			this.statusStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.selectedCountLabel,
				this.loadStatusLabel,
				this.loadProgressBar
			});
			this.statusStrip.LayoutStyle = global::System.Windows.Forms.ToolStripLayoutStyle.Table;
			this.statusStrip.Location = new global::System.Drawing.Point(0, 0);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new global::System.Drawing.Size(498, 22);
			this.statusStrip.TabIndex = 4;
			this.selectedCountLabel.Name = "selectedCountLabel";
			this.loadStatusLabel.Name = "loadStatusLabel";
			this.loadStatusLabel.Spring = true;
			this.loadStatusLabel.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.loadProgressBar.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this.loadProgressBar.Name = "loadProgressBar";
			this.loadProgressBar.Size = new global::System.Drawing.Size(100, 16);
			this.loadProgressBar.Style = global::System.Windows.Forms.ProgressBarStyle.Marquee;
			this.loadProgressBar.Visible = false;
			this.toolStripContainer.TopToolStripPanel.Name = "TopToolStripPanel";
			this.toolStripContainer.LeftToolStripPanel.Name = "LeftToolStripPanel";
			this.toolStripContainer.RightToolStripPanel.Name = "RightToolStripPanel";
			this.toolStripContainer.BottomToolStripPanel.Name = "BottomToolStripPanel";
			this.toolStripContainer.ContentPanel.Name = "ContentPanel";
			this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
			this.toolStripContainer.ContentPanel.AutoScroll = true;
			this.toolStripContainer.ContentPanel.Controls.Add(this.resultTreeView);
			this.toolStripContainer.ContentPanel.Controls.Add(this.dialogButtonsPanel);
			this.toolStripContainer.ContentPanel.RenderMode = global::System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripContainer.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.Location = new global::System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new global::System.Drawing.Size(498, 433);
			this.toolStripContainer.TabIndex = 0;
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
			this.menuStrip.Dock = global::System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fileToolStripMenuItem
			});
			this.menuStrip.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.RenderMode = global::System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip.Size = new global::System.Drawing.Size(498, 24);
			this.menuStrip.TabIndex = 2;
			this.menuStrip.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.closeToolStripMenuItem
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)262259;
			this.resultTreeView.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.resultTreeView.Location = new global::System.Drawing.Point(0, 0);
			this.resultTreeView.Name = "resultTreeView";
			this.resultTreeView.Size = new global::System.Drawing.Size(498, 300);
			this.resultTreeView.TabIndex = 3;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.ClientSize = new global::System.Drawing.Size(498, 433);
			base.Controls.Add(this.toolStripContainer);
			this.MinimumSize = new global::System.Drawing.Size(323, 284);
			base.Name = "TreeViewObjectPickerForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.dialogButtonsPanel.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.ContentPanel.PerformLayout();
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040009B2 RID: 2482
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040009B3 RID: 2483
		private global::System.Windows.Forms.FlowLayoutPanel dialogButtonsPanel;

		// Token: 0x040009B4 RID: 2484
		private global::Microsoft.ManagementGUI.WinForms.ExchangeButton helpButton;

		// Token: 0x040009B5 RID: 2485
		private global::Microsoft.ManagementGUI.WinForms.ExchangeButton cancelButton;

		// Token: 0x040009B6 RID: 2486
		private global::Microsoft.ManagementGUI.WinForms.ExchangeButton okButton;

		// Token: 0x040009B7 RID: 2487
		private global::System.Windows.Forms.ToolStripContainer toolStripContainer;

		// Token: 0x040009B8 RID: 2488
		private global::System.Windows.Forms.StatusStrip statusStrip;

		// Token: 0x040009B9 RID: 2489
		private global::System.Windows.Forms.ToolStripStatusLabel selectedCountLabel;

		// Token: 0x040009BA RID: 2490
		private global::System.Windows.Forms.ToolStripStatusLabel loadStatusLabel;

		// Token: 0x040009BB RID: 2491
		private global::Microsoft.ManagementGUI.WinForms.ExchangeToolStripProgressBar loadProgressBar;

		// Token: 0x040009BC RID: 2492
		private global::System.Windows.Forms.MenuStrip menuStrip;

		// Token: 0x040009BD RID: 2493
		private global::System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

		// Token: 0x040009BE RID: 2494
		private global::System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;

		// Token: 0x040009BF RID: 2495
		private global::Microsoft.ManagementGUI.WinForms.DataTreeView resultTreeView;
	}
}
