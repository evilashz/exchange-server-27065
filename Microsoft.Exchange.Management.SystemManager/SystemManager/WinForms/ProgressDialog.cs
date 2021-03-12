using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000194 RID: 404
	public partial class ProgressDialog : ExchangeDialog
	{
		// Token: 0x06001029 RID: 4137 RVA: 0x0003F750 File Offset: 0x0003D950
		public ProgressDialog()
		{
			this.InitializeComponent();
			Application.Idle += this.Application_Idle;
			base.OkVisible = false;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0003F789 File Offset: 0x0003D989
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			base.HelpVisible = false;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0003F7B8 File Offset: 0x0003D9B8
		private void Application_Idle(object sender, EventArgs e)
		{
			if (this.updateControls)
			{
				this.updateControls = false;
				if (this.UseMarquee)
				{
					this.progressBar.Style = ProgressBarStyle.Marquee;
				}
				else
				{
					this.progressBar.Style = ProgressBarStyle.Continuous;
				}
				this.progressBar.Maximum = this.Maximum;
				this.progressBar.Value = this.Value;
				this.statusLabel.Text = this.StatusText;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0003FB0E File Offset: 0x0003DD0E
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x0003FB16 File Offset: 0x0003DD16
		[DefaultValue(0)]
		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (value != this.Value)
				{
					this.value = value;
					this.updateControls = true;
					base.Invalidate();
				}
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0003FB35 File Offset: 0x0003DD35
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x0003FB3D File Offset: 0x0003DD3D
		[DefaultValue(100)]
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				if (value != this.Maximum)
				{
					this.maximum = value;
					this.updateControls = true;
					base.Invalidate();
				}
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0003FB5C File Offset: 0x0003DD5C
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x0003FB64 File Offset: 0x0003DD64
		[DefaultValue(false)]
		public bool UseMarquee
		{
			get
			{
				return this.useMarquee;
			}
			set
			{
				if (value != this.UseMarquee)
				{
					this.useMarquee = value;
					this.updateControls = true;
					base.Invalidate();
				}
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0003FB83 File Offset: 0x0003DD83
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x0003FB8B File Offset: 0x0003DD8B
		[DefaultValue("")]
		public string StatusText
		{
			get
			{
				return this.statusText;
			}
			set
			{
				value = (value ?? "");
				if (value != this.StatusText)
				{
					this.statusText = value;
					this.updateControls = true;
					base.Invalidate();
				}
			}
		}

		// Token: 0x04000651 RID: 1617
		private bool updateControls;

		// Token: 0x04000652 RID: 1618
		private int value;

		// Token: 0x04000653 RID: 1619
		private int maximum = 100;

		// Token: 0x04000654 RID: 1620
		private bool useMarquee;

		// Token: 0x04000655 RID: 1621
		private string statusText = "";
	}
}
