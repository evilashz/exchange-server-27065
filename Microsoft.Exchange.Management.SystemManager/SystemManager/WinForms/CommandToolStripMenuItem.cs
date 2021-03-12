using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.Services;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001B9 RID: 441
	public class CommandToolStripMenuItem : ToolStripMenuItem, IServiceProvider
	{
		// Token: 0x060011E7 RID: 4583 RVA: 0x00048243 File Offset: 0x00046443
		public CommandToolStripMenuItem()
		{
			this.servicedComponents = new ServicedContainer(this);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00048257 File Offset: 0x00046457
		public CommandToolStripMenuItem(IContainer container) : this()
		{
			container.Add(this);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00048266 File Offset: 0x00046466
		public CommandToolStripMenuItem(IContainer container, Command command) : this(container)
		{
			this.Command = command;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00048276 File Offset: 0x00046476
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.Command = null;
				this.servicedComponents.Dispose();
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00048294 File Offset: 0x00046494
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x0004829C File Offset: 0x0004649C
		[DefaultValue(null)]
		public Command Command
		{
			get
			{
				return this.command;
			}
			set
			{
				if (value != this.Command)
				{
					if (this.Command != null)
					{
						this.Command.TextChanged -= this.Command_TextChanged;
						this.Command.DescriptionChanged -= this.Command_DescriptionChanged;
						this.Command.EnabledChanged -= this.Command_EnabledChanged;
						this.Command.VisibleChanged -= this.Command_VisibleChanged;
						this.Command.CheckedChanged -= this.Command_CheckedChanged;
						this.Command.IconChanged -= this.Command_IconChanged;
						this.Command.CommandAdded -= new CommandEventHandler(this.Command_CommandAdded);
						this.Command.CommandRemoved -= new CommandEventHandler(this.Command_CommandRemoved);
						this.Command.StyleChanged -= this.Command_StyleChanged;
						if (this.Command.HasCommands)
						{
							for (int i = base.DropDownItems.Count - 1; i >= 0; i--)
							{
								base.DropDownItems[i].Dispose();
							}
						}
					}
					this.command = value;
					if (this.Command != null)
					{
						this.Command_TextChanged(this.Command, EventArgs.Empty);
						this.Command_DescriptionChanged(this.Command, EventArgs.Empty);
						this.Command_EnabledChanged(this.Command, EventArgs.Empty);
						this.Command_VisibleChanged(this.Command, EventArgs.Empty);
						this.Command_CheckedChanged(this.Command, EventArgs.Empty);
						this.Command_IconChanged(this.Command, EventArgs.Empty);
						this.Command_StyleChanged(this.Command, EventArgs.Empty);
						this.Command.TextChanged += this.Command_TextChanged;
						this.Command.DescriptionChanged += this.Command_DescriptionChanged;
						this.Command.EnabledChanged += this.Command_EnabledChanged;
						this.Command.VisibleChanged += this.Command_VisibleChanged;
						this.Command.CheckedChanged += this.Command_CheckedChanged;
						this.Command.IconChanged += this.Command_IconChanged;
						this.Command.StyleChanged += this.Command_StyleChanged;
						this.Command.CommandAdded += new CommandEventHandler(this.Command_CommandAdded);
						this.Command.CommandRemoved += new CommandEventHandler(this.Command_CommandRemoved);
						if (!this.Command.HasCommands)
						{
							return;
						}
						using (IEnumerator enumerator = this.Command.Commands.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								Command sourceCommand = (Command)obj;
								base.DropDownItems.Add(this.CreateMenuItem(sourceCommand));
							}
							return;
						}
					}
					this.DisposeImage();
				}
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00048590 File Offset: 0x00046790
		protected virtual ToolStripItem CreateMenuItem(Command sourceCommand)
		{
			if (sourceCommand.IsSeparator)
			{
				return new CommandToolStripSeparator(sourceCommand);
			}
			return new CommandToolStripMenuItem(this.servicedComponents, sourceCommand);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000485B0 File Offset: 0x000467B0
		public virtual ToolStripItem FindMenuItem(Command commandToFind)
		{
			foreach (object obj in base.DropDownItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem is CommandToolStripMenuItem && (toolStripItem as CommandToolStripMenuItem).Command == commandToFind)
				{
					return toolStripItem;
				}
				if (toolStripItem is CommandToolStripSeparator && (toolStripItem as CommandToolStripSeparator).Command == commandToFind)
				{
					return toolStripItem;
				}
			}
			return null;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0004863C File Offset: 0x0004683C
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			this.Command.Invoke();
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00048650 File Offset: 0x00046850
		private void Command_TextChanged(object sender, EventArgs e)
		{
			this.Text = this.Command.Text;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00048663 File Offset: 0x00046863
		private void Command_DescriptionChanged(object sender, EventArgs e)
		{
			base.ToolTipText = this.Command.Description;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00048676 File Offset: 0x00046876
		private void Command_EnabledChanged(object sender, EventArgs e)
		{
			this.Enabled = this.Command.Enabled;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00048689 File Offset: 0x00046889
		private void Command_VisibleChanged(object sender, EventArgs e)
		{
			base.Visible = this.Command.Visible;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0004869C File Offset: 0x0004689C
		private void Command_CheckedChanged(object sender, EventArgs e)
		{
			base.Checked = this.Command.Checked;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x000486AF File Offset: 0x000468AF
		private void Command_IconChanged(object sender, EventArgs e)
		{
			this.DisposeImage();
			if (this.Command.Icon != null)
			{
				this.Image = IconLibrary.ToSmallBitmap(this.Command.Icon);
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x000486DC File Offset: 0x000468DC
		private void Command_StyleChanged(object sender, EventArgs e)
		{
			switch (this.Command.Style)
			{
			case 0:
			case 2:
			case 4:
				this.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
				return;
			case 1:
				this.DisplayStyle = ToolStripItemDisplayStyle.Text;
				break;
			case 3:
			case 5:
			case 6:
			case 7:
				break;
			case 8:
				this.DisplayStyle = ToolStripItemDisplayStyle.Image;
				return;
			default:
				return;
			}
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00048737 File Offset: 0x00046937
		private void Command_CommandAdded(object sender, CommandEventArgs e)
		{
			base.DropDownItems.Insert(this.Command.Commands.IndexOf(e.Command), this.CreateMenuItem(e.Command));
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00048768 File Offset: 0x00046968
		private void Command_CommandRemoved(object sender, CommandEventArgs e)
		{
			ToolStripItem toolStripItem = this.FindMenuItem(e.Command);
			if (toolStripItem != null)
			{
				base.DropDownItems.Remove(toolStripItem);
				this.servicedComponents.Remove(toolStripItem);
				toolStripItem.Dispose();
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x000487A3 File Offset: 0x000469A3
		private void DisposeImage()
		{
			if (this.Image != null)
			{
				this.Image.Dispose();
				this.Image = null;
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000487BF File Offset: 0x000469BF
		object IServiceProvider.GetService(Type serviceType)
		{
			return this.GetService(serviceType);
		}

		// Token: 0x040006DB RID: 1755
		private Command command;

		// Token: 0x040006DC RID: 1756
		private ServicedContainer servicedComponents;
	}
}
