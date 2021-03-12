using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200021D RID: 541
	public class WorkPanePage : TabPage
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00068412 File Offset: 0x00066612
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x0006841C File Offset: 0x0006661C
		[DefaultValue(null)]
		public AbstractResultPane ResultPane
		{
			get
			{
				return this.resultPane;
			}
			set
			{
				if (this.ResultPane != null)
				{
					this.ResultPane.TextChanged -= this.ResultPane_TextChanged;
					this.Text = string.Empty;
					base.Controls.Remove(this.resultPane);
				}
				this.resultPane = value;
				if (this.ResultPane != null)
				{
					if (this.ResultPane.Dock != DockStyle.Fill)
					{
						this.ResultPane.Dock = DockStyle.Fill;
					}
					base.Name = this.ResultPane.Name;
					base.Controls.Add(this.ResultPane);
					this.ResultPane.TextChanged += this.ResultPane_TextChanged;
					this.ResultPane_TextChanged(this.ResultPane, EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000684D7 File Offset: 0x000666D7
		private void ResultPane_TextChanged(object sender, EventArgs e)
		{
			this.Text = this.ResultPane.Text;
		}

		// Token: 0x04000932 RID: 2354
		private AbstractResultPane resultPane;
	}
}
