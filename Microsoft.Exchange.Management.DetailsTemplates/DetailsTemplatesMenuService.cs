using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200001A RID: 26
	internal class DetailsTemplatesMenuService : MenuCommandService
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x0000553C File Offset: 0x0000373C
		internal DetailsTemplatesMenuService(DetailsTemplatesSurface detailsTemplatesDesignSurface) : base(detailsTemplatesDesignSurface)
		{
			this.designSurface = detailsTemplatesDesignSurface;
			this.AddCommand(new MenuCommand(new EventHandler(this.ExecuteUndo), StandardCommands.Undo)
			{
				Enabled = false
			});
			this.AddCommand(new MenuCommand(new EventHandler(this.ExecuteRedo), StandardCommands.Redo)
			{
				Enabled = false
			});
			MenuCommand command = new MenuCommand(new EventHandler(this.AddNewTabPage), DetailsTemplatesMenuService.AddTabPageCommandId);
			this.AddCommand(command);
			MenuCommand command2 = new MenuCommand(new EventHandler(this.RemoveCurrentTabPage), DetailsTemplatesMenuService.RemoveTabPageCommandId);
			this.AddCommand(command2);
			MenuCommand command3 = new MenuCommand(new EventHandler(this.DoSelectNextControl), DetailsTemplatesMenuService.SelectNextControl);
			this.AddCommand(command3);
			this.readOnlyCommandList.Add(DetailsTemplatesMenuService.SelectNextControl);
			MenuCommand command4 = new MenuCommand(new EventHandler(this.DoSelectPreviousControl), DetailsTemplatesMenuService.SelectPreviousControl);
			this.AddCommand(command4);
			this.readOnlyCommandList.Add(DetailsTemplatesMenuService.SelectPreviousControl);
			MenuCommand command5 = new MenuCommand(new EventHandler(this.DoSwitchTabPage), DetailsTemplatesMenuService.SwitchTabPage);
			this.AddCommand(command5);
			this.readOnlyCommandList.Add(DetailsTemplatesMenuService.SwitchTabPage);
			MenuCommand command6 = new MenuCommand(new EventHandler(this.DoSelectAllInCurrentTab), DetailsTemplatesMenuService.SelectAllCommandId);
			this.AddCommand(command6);
			this.readOnlyCommandList.Add(DetailsTemplatesMenuService.SelectAllCommandId);
			this.selectionService = (base.GetService(typeof(ISelectionService)) as ISelectionService);
			if (this.selectionService != null)
			{
				this.selectionService.SelectionChanged += this.selectionService_SelectionChanged;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000056E6 File Offset: 0x000038E6
		private bool IsReadOnlyCommand(CommandID commandID)
		{
			return commandID != null && this.readOnlyCommandList.Contains(commandID);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000056F9 File Offset: 0x000038F9
		private void selectionService_SelectionChanged(object sender, EventArgs e)
		{
			if (this.Enabled)
			{
				this.OnMenuCommandStatusChanged();
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000570C File Offset: 0x0000390C
		public bool IsMenuCommandEnabled(CommandID commandID)
		{
			bool flag = false;
			if (commandID != null && (this.Enabled || this.IsReadOnlyCommand(commandID)))
			{
				object obj = (this.selectionService != null) ? this.selectionService.PrimarySelection : null;
				int num = (this.selectionService != null) ? this.selectionService.SelectionCount : 0;
				MenuCommand menuCommand = base.FindCommand(commandID);
				if (menuCommand != null)
				{
					if (StandardCommands.Undo.Equals(commandID) || StandardCommands.Redo.Equals(commandID) || DetailsTemplatesMenuService.SelectNextControl.Equals(commandID) || DetailsTemplatesMenuService.SelectPreviousControl.Equals(commandID) || DetailsTemplatesMenuService.SwitchTabPage.Equals(commandID))
					{
						flag = true;
					}
					else if (DetailsTemplatesMenuService.AddTabPageCommandId.Equals(commandID) || DetailsTemplatesMenuService.RemoveTabPageCommandId.Equals(commandID))
					{
						flag = (obj is TabControl);
					}
					else if (StandardCommands.Paste.Equals(commandID))
					{
						flag = (obj is TabPage && num == 1);
					}
					else
					{
						flag = (DetailsTemplatesMenuService.SelectAllCommandId.Equals(commandID) || (!this.ControlTypeSelected(typeof(TabControl)) && !this.ControlTypeSelected(typeof(TabPage))));
					}
					flag = (flag && menuCommand.Enabled);
				}
			}
			return flag;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005844 File Offset: 0x00003A44
		private bool ControlTypeSelected(Type type)
		{
			bool result = false;
			ICollection collection = (this.selectionService == null) ? null : this.selectionService.GetSelectedComponents();
			if (collection != null)
			{
				foreach (object obj in collection)
				{
					if (type.IsAssignableFrom(obj.GetType()))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000058C0 File Offset: 0x00003AC0
		public override bool GlobalInvoke(CommandID commandID)
		{
			return this.IsMenuCommandEnabled(commandID) && base.GlobalInvoke(commandID);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000058D4 File Offset: 0x00003AD4
		protected override void OnCommandsChanged(MenuCommandsChangedEventArgs e)
		{
			base.OnCommandsChanged(e);
			this.OnMenuCommandStatusChanged();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000058E4 File Offset: 0x00003AE4
		public override void ShowContextMenu(CommandID menuID, int x, int y)
		{
			ContextMenu contextMenu = (this.designSurface != null) ? this.designSurface.GetContextMenu() : null;
			Control control = (this.selectionService != null) ? (this.selectionService.PrimarySelection as Control) : null;
			if (control != null && contextMenu != null)
			{
				Point point = control.PointToScreen(new Point(0, 0));
				contextMenu.Show(control, new Point(x - point.X, y - point.Y));
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005956 File Offset: 0x00003B56
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000595E File Offset: 0x00003B5E
		[DefaultValue(false)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.Enabled != value)
				{
					this.enabled = value;
					this.OnMenuCommandStatusChanged();
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000BB RID: 187 RVA: 0x00005978 File Offset: 0x00003B78
		// (remove) Token: 0x060000BC RID: 188 RVA: 0x000059B0 File Offset: 0x00003BB0
		public event EventHandler MenuCommandStatusChanged;

		// Token: 0x060000BD RID: 189 RVA: 0x000059E5 File Offset: 0x00003BE5
		private void OnMenuCommandStatusChanged()
		{
			if (this.MenuCommandStatusChanged != null)
			{
				this.MenuCommandStatusChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005A00 File Offset: 0x00003C00
		private void AddNewTabPage(object sender, EventArgs args)
		{
			DetailsTemplatesMenuService.AddTab(this.designSurface);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005A0D File Offset: 0x00003C0D
		private void RemoveCurrentTabPage(object sender, EventArgs args)
		{
			DetailsTemplatesMenuService.RemoveTab(this.designSurface);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005A1C File Offset: 0x00003C1C
		private static void RemoveTab(DetailsTemplatesSurface designSurface)
		{
			UIService uiservice = null;
			IDesignerHost designerHost = designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null)
			{
				uiservice = (designerHost.GetService(typeof(UIService)) as UIService);
			}
			if (uiservice != null)
			{
				if (designSurface.TemplateTab.Controls.Count <= 1)
				{
					uiservice.ShowMessage(Strings.CannotDeletePage);
					return;
				}
				TabPage selectedTab = designSurface.TemplateTab.SelectedTab;
				if (selectedTab != null)
				{
					DialogResult dialogResult = uiservice.ShowMessage(Strings.ConfirmDeleteTab(selectedTab.Text), UIService.DefaultCaption, MessageBoxButtons.OKCancel);
					if (dialogResult == DialogResult.OK)
					{
						DesignerTransaction designerTransaction = null;
						try
						{
							designerTransaction = designerHost.CreateTransaction("TabControlRemoveTabPage" + designSurface.TemplateTab.Site.Name);
							designerHost.DestroyComponent(selectedTab);
						}
						finally
						{
							if (designerTransaction != null)
							{
								designerTransaction.Commit();
							}
						}
						designSurface.DataContext.IsDirty = true;
					}
				}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005B0C File Offset: 0x00003D0C
		internal static void AddTab(DetailsTemplatesSurface designSurface)
		{
			if (designSurface != null)
			{
				IDesignerHost designerHost = designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					DesignerTransaction designerTransaction = null;
					try
					{
						designerTransaction = designerHost.CreateTransaction("TabControlAddTabPage" + designSurface.TemplateTab.Site.Name);
						Hashtable hashtable = new Hashtable();
						hashtable["Parent"] = designSurface.TemplateTab;
						hashtable["TabPageIndex"] = designSurface.TemplateTab.SelectedIndex + 1;
						ToolboxItem toolboxItem = new ToolboxItem(typeof(CustomTabPage));
						toolboxItem.CreateComponents(designerHost, hashtable);
					}
					finally
					{
						if (designerTransaction != null)
						{
							designerTransaction.Commit();
						}
					}
				}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005BC4 File Offset: 0x00003DC4
		private void ExecuteUndo(object sender, EventArgs e)
		{
			DetailsTemplateUndoEngine detailsTemplateUndoEngine = base.GetService(typeof(UndoEngine)) as DetailsTemplateUndoEngine;
			if (detailsTemplateUndoEngine != null)
			{
				detailsTemplateUndoEngine.DoUndo();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005BF0 File Offset: 0x00003DF0
		private void ExecuteRedo(object sender, EventArgs e)
		{
			DetailsTemplateUndoEngine detailsTemplateUndoEngine = base.GetService(typeof(UndoEngine)) as DetailsTemplateUndoEngine;
			if (detailsTemplateUndoEngine != null)
			{
				detailsTemplateUndoEngine.DoRedo();
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005C1C File Offset: 0x00003E1C
		private void DoSelectNextControl(object sender, EventArgs e)
		{
			this.SelectNextControlInCurrentTabPage(true);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005C25 File Offset: 0x00003E25
		private void DoSelectPreviousControl(object sender, EventArgs e)
		{
			this.SelectNextControlInCurrentTabPage(false);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005C30 File Offset: 0x00003E30
		private void SelectNextControlInCurrentTabPage(bool forward)
		{
			if (this.selectionService != null && this.designSurface.TemplateTab != null && this.designSurface.TemplateTab.SelectedTab != null)
			{
				Control control = null;
				Component component = this.selectionService.PrimarySelection as Component;
				if (forward && component == this.designSurface.TemplateTab)
				{
					control = this.designSurface.TemplateTab.SelectedTab;
				}
				else if (component != null)
				{
					control = this.designSurface.TemplateTab.SelectedTab.GetNextControl(component as Control, forward);
				}
				if (!forward)
				{
					if (component == this.designSurface.TemplateTab.SelectedTab)
					{
						control = this.designSurface.TemplateTab;
					}
					else if (component == this.designSurface.TemplateTab)
					{
						control = null;
					}
					else if (control == null)
					{
						control = this.designSurface.TemplateTab.SelectedTab;
					}
				}
				if (control != null)
				{
					this.selectionService.SetSelectedComponents(new Component[]
					{
						control
					}, SelectionTypes.Replace);
					return;
				}
				DetailsTemplatesEditor detailsTemplatesEditor = this.designSurface.ExchangeForm as DetailsTemplatesEditor;
				if (detailsTemplatesEditor != null)
				{
					detailsTemplatesEditor.SelectNextSiblingOfDesignSurface(forward);
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005D48 File Offset: 0x00003F48
		private void DoSwitchTabPage(object sender, EventArgs e)
		{
			if (this.selectionService != null && this.designSurface.TemplateTab != null)
			{
				int num = this.designSurface.TemplateTab.SelectedIndex;
				num++;
				if (this.designSurface.TemplateTab.TabCount > 0)
				{
					num %= this.designSurface.TemplateTab.TabCount;
				}
				else
				{
					num = -1;
				}
				if (num >= 0)
				{
					this.selectionService.SetSelectedComponents(new Component[]
					{
						this.designSurface.TemplateTab.TabPages[num]
					}, SelectionTypes.Replace);
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005DDC File Offset: 0x00003FDC
		private void DoSelectAllInCurrentTab(object sender, EventArgs e)
		{
			if (this.selectionService != null && this.designSurface.TemplateTab != null && this.designSurface.TemplateTab.SelectedTab != null)
			{
				this.selectionService.SetSelectedComponents(this.designSurface.TemplateTab.SelectedTab.Controls, SelectionTypes.Replace);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005E31 File Offset: 0x00004031
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.selectionService != null)
			{
				this.selectionService.SelectionChanged -= this.selectionService_SelectionChanged;
				this.selectionService = null;
				this.designSurface = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000049 RID: 73
		internal static CommandID AddTabPageCommandId = new CommandID(Guid.NewGuid(), 1);

		// Token: 0x0400004A RID: 74
		internal static CommandID RemoveTabPageCommandId = new CommandID(DetailsTemplatesMenuService.AddTabPageCommandId.Guid, 2);

		// Token: 0x0400004B RID: 75
		internal static CommandID SelectNextControl = new CommandID(DetailsTemplatesMenuService.AddTabPageCommandId.Guid, 3);

		// Token: 0x0400004C RID: 76
		internal static CommandID SelectPreviousControl = new CommandID(DetailsTemplatesMenuService.AddTabPageCommandId.Guid, 4);

		// Token: 0x0400004D RID: 77
		internal static CommandID SwitchTabPage = new CommandID(DetailsTemplatesMenuService.AddTabPageCommandId.Guid, 5);

		// Token: 0x0400004E RID: 78
		internal static CommandID SelectAllCommandId = new CommandID(DetailsTemplatesMenuService.AddTabPageCommandId.Guid, 6);

		// Token: 0x0400004F RID: 79
		private DetailsTemplatesSurface designSurface;

		// Token: 0x04000050 RID: 80
		private ISelectionService selectionService;

		// Token: 0x04000051 RID: 81
		private HashSet<CommandID> readOnlyCommandList = new HashSet<CommandID>();

		// Token: 0x04000052 RID: 82
		private bool enabled;
	}
}
