using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000166 RID: 358
	public class SimpleListEditor : DataListControl
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x00037D58 File Offset: 0x00035F58
		public SimpleListEditor()
		{
			this.addCommand = new Command();
			this.addCommand.Name = "addCommand";
			this.addCommand.Text = Strings.AddObject;
			this.addCommand.Description = Strings.AddCommandDescription;
			this.addCommand.Enabled = false;
			this.addCommand.Icon = Icons.Add;
			this.addCommand.Visible = true;
			this.addCommand.Execute += this.AddCommand_Execute;
			ToolStripItem[] toolStripItems = new ToolStripItem[]
			{
				new CommandToolStripButton(this.addCommand),
				new CommandToolStripButton(base.DataListView.RemoveCommand)
			};
			base.ToolStripItems.AddRange(toolStripItems);
			base.DataListView.ContextMenu.MenuItems.Add(new CommandMenuItem(base.DataListView.RemoveCommand, base.Components));
			base.DataListView.AllowRemove = true;
			base.DataListView.AutoGenerateColumns = false;
			base.DataListView.HeaderStyle = ColumnHeaderStyle.None;
			base.DataListView.AvailableColumns.Add("ToString()", string.Empty, true);
			base.DataListView.DeleteSelectionCommand = base.DataListView.RemoveCommand;
			base.Name = "SimpleListEditor";
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00037EBD File Offset: 0x000360BD
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x00037EC5 File Offset: 0x000360C5
		[DefaultValue("Name")]
		public string DataTableColumnName
		{
			get
			{
				return this.dataTableColumnName;
			}
			set
			{
				this.dataTableColumnName = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00037ECE File Offset: 0x000360CE
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x00037EE6 File Offset: 0x000360E6
		[DefaultValue("")]
		public string DataListViewColumnText
		{
			get
			{
				return base.DataListView.Columns[0].Text;
			}
			set
			{
				base.DataListView.Columns[0].Text = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00037EFF File Offset: 0x000360FF
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x00037F0C File Offset: 0x0003610C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string AddCommandText
		{
			get
			{
				return this.addCommand.Text;
			}
			set
			{
				this.addCommand.Text = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00037F1A File Offset: 0x0003611A
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x00037F27 File Offset: 0x00036127
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string AddCommandDescription
		{
			get
			{
				return this.addCommand.Description;
			}
			set
			{
				this.addCommand.Description = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00037F35 File Offset: 0x00036135
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x00037F3D File Offset: 0x0003613D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ObjectPicker Picker
		{
			get
			{
				return this.picker;
			}
			set
			{
				if (this.picker != value)
				{
					this.picker = value;
					if (this.picker != null)
					{
						this.picker.AllowMultiSelect = true;
					}
					this.addCommand.Enabled = (value != null);
				}
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00037F78 File Offset: 0x00036178
		private void AddCommand_Execute(object sender, EventArgs e)
		{
			if (DialogResult.OK == this.Picker.ShowDialog(this))
			{
				DataTable selectedObjects = this.Picker.SelectedObjects;
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < selectedObjects.Rows.Count; i++)
				{
					arrayList.Add(selectedObjects.Rows[i][this.DataTableColumnName]);
				}
				base.InternalAddRange(arrayList);
			}
		}

		// Token: 0x040005E2 RID: 1506
		private ObjectPicker picker;

		// Token: 0x040005E3 RID: 1507
		private Command addCommand;

		// Token: 0x040005E4 RID: 1508
		private string dataTableColumnName = ObjectPicker.ObjectName;
	}
}
