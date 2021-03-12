using System;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000117 RID: 279
	public class CustomToolStripList : DataListControl
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x0002551C File Offset: 0x0002371C
		public CustomToolStripList()
		{
			base.Name = "CustomToolStripList";
			this.addCommand = new Command();
			this.editCommand = new Command();
			this.addCommand.Name = "add";
			this.addCommand.Text = Strings.AddButtonText;
			this.addCommand.Description = Strings.AddButtonDescription;
			this.addCommand.Enabled = true;
			this.addCommand.Icon = Icons.Add;
			this.addCommand.Visible = true;
			this.editCommand.Name = "edit";
			this.editCommand.Text = base.DataListView.InlineEditCommand.Text;
			this.editCommand.Description = base.DataListView.InlineEditCommand.Description;
			this.editCommand.Enabled = false;
			this.editCommand.Icon = base.DataListView.InlineEditCommand.Icon;
			this.editCommand.Visible = true;
			base.DataListView.ShowSelectionPropertiesCommand = this.editCommand;
			ToolStripItem[] toolStripItems = new ToolStripItem[]
			{
				new CommandToolStripButton(this.AddCommand),
				new CommandToolStripButton(base.DataListView.ShowSelectionPropertiesCommand),
				new CommandToolStripButton(this.RemoveCommand)
			};
			MenuItem[] items = new MenuItem[]
			{
				new CommandMenuItem(base.DataListView.ShowSelectionPropertiesCommand, base.Components),
				new CommandMenuItem(this.RemoveCommand, base.Components)
			};
			base.DataListView.AllowRemove = true;
			base.DataListView.ContextMenu.MenuItems.AddRange(items);
			base.ToolStripItems.AddRange(toolStripItems);
			base.DataListView.SelectionChanged += delegate(object param0, EventArgs param1)
			{
				this.editCommand.Enabled = (base.DataListView.SelectedIndices.Count == 1);
			};
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000256FA File Offset: 0x000238FA
		public Command AddCommand
		{
			get
			{
				return this.addCommand;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00025702 File Offset: 0x00023902
		public Command EditCommand
		{
			get
			{
				return this.editCommand;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0002570A File Offset: 0x0002390A
		public virtual Command RemoveCommand
		{
			get
			{
				return base.DataListView.RemoveCommand;
			}
		}

		// Token: 0x04000466 RID: 1126
		private Command addCommand;

		// Token: 0x04000467 RID: 1127
		private Command editCommand;
	}
}
