using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001B8 RID: 440
	public class CommandToolStripButton : ToolStripButton
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x00047F05 File Offset: 0x00046105
		public CommandToolStripButton()
		{
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00047F0D File Offset: 0x0004610D
		public CommandToolStripButton(Command command)
		{
			this.Command = command;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00047F1C File Offset: 0x0004611C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Command = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00047F2F File Offset: 0x0004612F
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x00047F38 File Offset: 0x00046138
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
						this.Command.StyleChanged -= this.Command_StyleChanged;
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
						return;
					}
					this.DisposeImage();
				}
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0004812E File Offset: 0x0004632E
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			this.Command.Invoke();
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00048142 File Offset: 0x00046342
		private void Command_TextChanged(object sender, EventArgs e)
		{
			this.Text = this.Command.Text;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00048155 File Offset: 0x00046355
		private void Command_DescriptionChanged(object sender, EventArgs e)
		{
			base.ToolTipText = this.Command.Description;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00048168 File Offset: 0x00046368
		private void Command_EnabledChanged(object sender, EventArgs e)
		{
			this.Enabled = this.Command.Enabled;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0004817B File Offset: 0x0004637B
		private void Command_VisibleChanged(object sender, EventArgs e)
		{
			base.Visible = this.Command.Visible;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004818E File Offset: 0x0004638E
		private void Command_CheckedChanged(object sender, EventArgs e)
		{
			base.Checked = this.Command.Checked;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x000481A1 File Offset: 0x000463A1
		private void Command_IconChanged(object sender, EventArgs e)
		{
			this.DisposeImage();
			if (this.Command.Icon != null)
			{
				this.Image = IconLibrary.ToSmallBitmap(this.Command.Icon);
			}
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x000481CC File Offset: 0x000463CC
		private void DisposeImage()
		{
			if (this.Image != null)
			{
				this.Image.Dispose();
				this.Image = null;
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x000481E8 File Offset: 0x000463E8
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

		// Token: 0x040006DA RID: 1754
		private Command command;
	}
}
