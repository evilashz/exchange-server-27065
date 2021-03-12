using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001C4 RID: 452
	public class CustomDataListControl : InlineEditDataListControl
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x0004D154 File Offset: 0x0004B354
		public CustomDataListControl()
		{
			base.DataListView.LabelEdit = false;
			foreach (object obj in base.ToolStripItems)
			{
				CommandToolStripButton commandToolStripButton = (CommandToolStripButton)obj;
				if (commandToolStripButton.Command.Equals(base.DataListView.InlineEditCommand))
				{
					commandToolStripButton.Visible = false;
				}
			}
			foreach (object obj2 in base.DataListView.ContextMenu.MenuItems)
			{
				CommandMenuItem commandMenuItem = (CommandMenuItem)obj2;
				if (commandMenuItem.Command.Equals(base.DataListView.InlineEditCommand))
				{
					commandMenuItem.Visible = false;
				}
			}
			base.DataListView.BeforeLabelEdit += this.DataListView_BeforeLabelEdit;
			base.Name = "CustomDataListControl";
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0004D26C File Offset: 0x0004B46C
		[DefaultValue(null)]
		[Browsable(false)]
		public Command RemoveCommand
		{
			get
			{
				return base.DataListView.RemoveCommand;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0004D279 File Offset: 0x0004B479
		private void DataListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
		{
			e.CancelEdit = true;
		}
	}
}
