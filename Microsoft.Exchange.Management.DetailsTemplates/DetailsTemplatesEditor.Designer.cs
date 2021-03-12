namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000012 RID: 18
	internal partial class DetailsTemplatesEditor : global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangeForm
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000044BE File Offset: 0x000026BE
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000044E0 File Offset: 0x000026E0
		private void InitializeComponent()
		{
			this.mainMenu = new global::System.Windows.Forms.MainMenu();
			this.fileMenuItem = new global::System.Windows.Forms.MenuItem();
			this.saveMenuItem = new global::System.Windows.Forms.MenuItem();
			this.menuItemSeparator = new global::System.Windows.Forms.MenuItem();
			this.exitMenuItem = new global::System.Windows.Forms.MenuItem();
			this.editMenuItem = new global::System.Windows.Forms.MenuItem();
			this.undoMenuItem = new global::System.Windows.Forms.MenuItem();
			this.redoMenuItem = new global::System.Windows.Forms.MenuItem();
			this.menuItemSeparatorBetweenRedoAndCut = new global::System.Windows.Forms.MenuItem();
			this.cutMenuItem = new global::System.Windows.Forms.MenuItem();
			this.copyMenuItem = new global::System.Windows.Forms.MenuItem();
			this.pasteMenuItem = new global::System.Windows.Forms.MenuItem();
			this.deleteMenuItem = new global::System.Windows.Forms.MenuItem();
			this.selectAllMenuItemSeparator = new global::System.Windows.Forms.MenuItem();
			this.selectAllMenuItem = new global::System.Windows.Forms.MenuItem();
			this.menuItemSeparatorBetweenDelAndRemove = new global::System.Windows.Forms.MenuItem();
			this.addTabPageMenuItem = new global::System.Windows.Forms.MenuItem();
			this.removeTabPageMenuItem = new global::System.Windows.Forms.MenuItem();
			this.editorContextMenu = new global::System.Windows.Forms.ContextMenu();
			this.cutContextMenuItem = new global::System.Windows.Forms.MenuItem();
			this.copyContextMenuItem = new global::System.Windows.Forms.MenuItem();
			this.pasteContextMenuItem = new global::System.Windows.Forms.MenuItem();
			this.deleteContextMenuItem = new global::System.Windows.Forms.MenuItem();
			this.selectAllContextMenuItemSeparator = new global::System.Windows.Forms.MenuItem();
			this.selectAllContextMenuItem = new global::System.Windows.Forms.MenuItem();
			this.contextMenuItemSeparatorBetweenDelAndRemove = new global::System.Windows.Forms.MenuItem();
			this.addTabPageContextMenuItem = new global::System.Windows.Forms.MenuItem();
			this.removeTabPageContextMenuItem = new global::System.Windows.Forms.MenuItem();
			this.helpMenuItem = new global::System.Windows.Forms.MenuItem();
			this.splitContainer1 = new global::System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new global::System.Windows.Forms.SplitContainer();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			base.SuspendLayout();
			this.mainMenu.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.fileMenuItem,
				this.editMenuItem,
				this.helpMenuItem
			});
			this.fileMenuItem.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.saveMenuItem,
				this.menuItemSeparator,
				this.exitMenuItem,
				this.undoMenuItem
			});
			this.fileMenuItem.Name = "fileMenuItem";
			this.fileMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.File;
			this.saveMenuItem.Name = "saveMenuItem";
			this.saveMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Save;
			this.saveMenuItem.Click += new global::System.EventHandler(this.saveToolStripMenuItem_Click);
			this.menuItemSeparator.Name = "menuItemSeparator";
			this.menuItemSeparator.Text = "-";
			this.exitMenuItem.Name = "exitMenuItem";
			this.exitMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Exit;
			this.exitMenuItem.Click += new global::System.EventHandler(this.exitToolStripMenuItem_Click);
			this.editMenuItem.Name = "editMenuItem";
			this.editMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.EditText;
			this.editMenuItem.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.addTabPageMenuItem,
				this.removeTabPageMenuItem,
				this.menuItemSeparatorBetweenDelAndRemove,
				this.undoMenuItem,
				this.redoMenuItem,
				this.menuItemSeparatorBetweenRedoAndCut,
				this.cutMenuItem,
				this.copyMenuItem,
				this.pasteMenuItem,
				this.deleteMenuItem,
				this.selectAllMenuItemSeparator,
				this.selectAllMenuItem
			});
			this.undoMenuItem.Name = "undoMenuItem";
			this.undoMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Undo;
			this.undoMenuItem.Tag = global::System.ComponentModel.Design.StandardCommands.Undo;
			this.undoMenuItem.Shortcut = global::System.Windows.Forms.Shortcut.CtrlZ;
			this.undoMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.redoMenuItem.Name = "redoMenuItem";
			this.redoMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Redo;
			this.redoMenuItem.Tag = global::System.ComponentModel.Design.StandardCommands.Redo;
			this.redoMenuItem.Shortcut = global::System.Windows.Forms.Shortcut.CtrlY;
			this.redoMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.menuItemSeparatorBetweenRedoAndCut.Name = "menuItemSeparatorBetweenRedoAndCut";
			this.menuItemSeparatorBetweenRedoAndCut.Text = "-";
			this.cutMenuItem.Name = "cutMenuItem";
			this.cutMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Cut;
			this.cutMenuItem.Tag = global::System.ComponentModel.Design.StandardCommands.Cut;
			this.cutMenuItem.Shortcut = global::System.Windows.Forms.Shortcut.CtrlX;
			this.cutMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.copyMenuItem.Name = "copyMenuItem";
			this.copyMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Copy;
			this.copyMenuItem.Tag = global::System.ComponentModel.Design.StandardCommands.Copy;
			this.copyMenuItem.Shortcut = global::System.Windows.Forms.Shortcut.CtrlC;
			this.copyMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.pasteMenuItem.Name = "pasteMenuItem";
			this.pasteMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Paste;
			this.pasteMenuItem.Tag = global::System.ComponentModel.Design.StandardCommands.Paste;
			this.pasteMenuItem.Shortcut = global::System.Windows.Forms.Shortcut.CtrlV;
			this.pasteMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.deleteMenuItem.Name = "deleteMenuItem";
			this.deleteMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Delete;
			this.deleteMenuItem.Tag = global::System.ComponentModel.Design.StandardCommands.Delete;
			this.deleteMenuItem.Shortcut = global::System.Windows.Forms.Shortcut.Del;
			this.deleteMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.selectAllMenuItemSeparator.Name = "selectAllMenuItemSeparator";
			this.selectAllMenuItemSeparator.Text = "-";
			this.selectAllMenuItem.Name = "selectAllMenuItem";
			this.selectAllMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.SelectAll;
			this.selectAllMenuItem.Tag = global::Microsoft.Exchange.Management.DetailsTemplates.DetailsTemplatesMenuService.SelectAllCommandId;
			this.selectAllMenuItem.Shortcut = global::System.Windows.Forms.Shortcut.CtrlA;
			this.selectAllMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.menuItemSeparatorBetweenDelAndRemove.Name = "menuItemSeparatorBetweenDelAndRemove";
			this.menuItemSeparatorBetweenDelAndRemove.Text = "-";
			this.addTabPageMenuItem.Name = "addTabPageMenuItem";
			this.addTabPageMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.AddTab;
			this.addTabPageMenuItem.Tag = global::Microsoft.Exchange.Management.DetailsTemplates.DetailsTemplatesMenuService.AddTabPageCommandId;
			this.addTabPageMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.removeTabPageMenuItem.Name = "removeTabPageMenuItem";
			this.removeTabPageMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.RemoveTab;
			this.removeTabPageMenuItem.Tag = global::Microsoft.Exchange.Management.DetailsTemplates.DetailsTemplatesMenuService.RemoveTabPageCommandId;
			this.removeTabPageMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.editorContextMenu.Name = "editorContextMenu";
			this.editorContextMenu.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.addTabPageContextMenuItem,
				this.removeTabPageContextMenuItem,
				this.contextMenuItemSeparatorBetweenDelAndRemove,
				this.cutContextMenuItem,
				this.copyContextMenuItem,
				this.pasteContextMenuItem,
				this.deleteContextMenuItem,
				this.selectAllContextMenuItemSeparator,
				this.selectAllContextMenuItem
			});
			this.cutMenuItem.Name = "cutContextMenuItem";
			this.cutContextMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Cut;
			this.cutContextMenuItem.Tag = global::System.ComponentModel.Design.StandardCommands.Cut;
			this.cutContextMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.copyContextMenuItem.Name = "copyContextMenuItem";
			this.copyContextMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Copy;
			this.copyContextMenuItem.Tag = global::System.ComponentModel.Design.StandardCommands.Copy;
			this.copyContextMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.pasteContextMenuItem.Name = "pasteContextMenuItem";
			this.pasteContextMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Paste;
			this.pasteContextMenuItem.Tag = global::System.ComponentModel.Design.StandardCommands.Paste;
			this.pasteContextMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.deleteContextMenuItem.Name = "deleteContextMenuItem";
			this.deleteContextMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Delete;
			this.deleteContextMenuItem.Tag = global::System.ComponentModel.Design.StandardCommands.Delete;
			this.deleteContextMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.selectAllContextMenuItemSeparator.Name = "selectAllContextMenuItemSeparator";
			this.selectAllContextMenuItemSeparator.Text = "-";
			this.selectAllContextMenuItem.Name = "selectAllContextMenuItem";
			this.selectAllContextMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.SelectAll;
			this.selectAllContextMenuItem.Tag = global::Microsoft.Exchange.Management.DetailsTemplates.DetailsTemplatesMenuService.SelectAllCommandId;
			this.selectAllContextMenuItem.Shortcut = global::System.Windows.Forms.Shortcut.CtrlA;
			this.selectAllContextMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.contextMenuItemSeparatorBetweenDelAndRemove.Name = "contextMenuItemSeparatorBetweenDelAndRemove";
			this.contextMenuItemSeparatorBetweenDelAndRemove.Text = "-";
			this.addTabPageContextMenuItem.Name = "addTabPageContextMenuItem";
			this.addTabPageContextMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.AddTab;
			this.addTabPageContextMenuItem.Tag = global::Microsoft.Exchange.Management.DetailsTemplates.DetailsTemplatesMenuService.AddTabPageCommandId;
			this.addTabPageContextMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.removeTabPageContextMenuItem.Name = "removeTabPageContextMenuItem";
			this.removeTabPageContextMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.RemoveTab;
			this.removeTabPageContextMenuItem.Tag = global::Microsoft.Exchange.Management.DetailsTemplates.DetailsTemplatesMenuService.RemoveTabPageCommandId;
			this.removeTabPageContextMenuItem.Click += new global::System.EventHandler(this.editMenuItems_Click);
			this.helpMenuItem.Name = "helpMenuItem";
			this.helpMenuItem.Text = global::Microsoft.Exchange.ManagementGUI.Resources.Strings.Help;
			this.helpMenuItem.Click += new global::System.EventHandler(this.helpToolStripMenuItem_Click);
			this.splitContainer1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new global::System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new global::System.Drawing.Size(792, 392);
			this.splitContainer1.SplitterDistance = 662;
			this.splitContainer1.TabIndex = 1;
			this.splitContainer2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new global::System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Size = new global::System.Drawing.Size(662, 392);
			this.splitContainer2.SplitterDistance = 140;
			this.splitContainer2.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(792, 416);
			base.Controls.Add(this.splitContainer1);
			base.Menu = this.mainMenu;
			base.Name = "DetailsTemplatesEditor";
			this.Text = "DetailsTemplatesEditor";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400001F RID: 31
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000020 RID: 32
		private global::System.Windows.Forms.MainMenu mainMenu;

		// Token: 0x04000021 RID: 33
		private global::System.Windows.Forms.MenuItem fileMenuItem;

		// Token: 0x04000022 RID: 34
		private global::System.Windows.Forms.MenuItem saveMenuItem;

		// Token: 0x04000023 RID: 35
		private global::System.Windows.Forms.MenuItem menuItemSeparator;

		// Token: 0x04000024 RID: 36
		private global::System.Windows.Forms.MenuItem exitMenuItem;

		// Token: 0x04000025 RID: 37
		private global::System.Windows.Forms.MenuItem helpMenuItem;

		// Token: 0x04000026 RID: 38
		private global::System.Windows.Forms.MenuItem editMenuItem;

		// Token: 0x04000027 RID: 39
		private global::System.Windows.Forms.MenuItem undoMenuItem;

		// Token: 0x04000028 RID: 40
		private global::System.Windows.Forms.MenuItem redoMenuItem;

		// Token: 0x04000029 RID: 41
		private global::System.Windows.Forms.MenuItem menuItemSeparatorBetweenRedoAndCut;

		// Token: 0x0400002A RID: 42
		private global::System.Windows.Forms.MenuItem cutMenuItem;

		// Token: 0x0400002B RID: 43
		private global::System.Windows.Forms.MenuItem copyMenuItem;

		// Token: 0x0400002C RID: 44
		private global::System.Windows.Forms.MenuItem pasteMenuItem;

		// Token: 0x0400002D RID: 45
		private global::System.Windows.Forms.MenuItem deleteMenuItem;

		// Token: 0x0400002E RID: 46
		private global::System.Windows.Forms.MenuItem selectAllMenuItemSeparator;

		// Token: 0x0400002F RID: 47
		private global::System.Windows.Forms.MenuItem selectAllMenuItem;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.MenuItem menuItemSeparatorBetweenDelAndRemove;

		// Token: 0x04000031 RID: 49
		private global::System.Windows.Forms.MenuItem removeTabPageMenuItem;

		// Token: 0x04000032 RID: 50
		private global::System.Windows.Forms.MenuItem addTabPageMenuItem;

		// Token: 0x04000033 RID: 51
		private global::System.Windows.Forms.MenuItem cutContextMenuItem;

		// Token: 0x04000034 RID: 52
		private global::System.Windows.Forms.MenuItem copyContextMenuItem;

		// Token: 0x04000035 RID: 53
		private global::System.Windows.Forms.MenuItem pasteContextMenuItem;

		// Token: 0x04000036 RID: 54
		private global::System.Windows.Forms.MenuItem deleteContextMenuItem;

		// Token: 0x04000037 RID: 55
		private global::System.Windows.Forms.MenuItem selectAllContextMenuItemSeparator;

		// Token: 0x04000038 RID: 56
		private global::System.Windows.Forms.MenuItem selectAllContextMenuItem;

		// Token: 0x04000039 RID: 57
		private global::System.Windows.Forms.MenuItem contextMenuItemSeparatorBetweenDelAndRemove;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.MenuItem removeTabPageContextMenuItem;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.MenuItem addTabPageContextMenuItem;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.ContextMenu editorContextMenu;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.SplitContainer splitContainer1;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.SplitContainer splitContainer2;
	}
}
