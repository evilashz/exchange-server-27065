using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000004 RID: 4
	internal class ProgressPageBase : SetupWizardPage
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000034C7 File Offset: 0x000016C7
		public ProgressPageBase()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000034D5 File Offset: 0x000016D5
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000034F4 File Offset: 0x000016F4
		private void InitializeComponent()
		{
			this.customProgressBarWithTitle = new CustomProgressBarWithTitle();
			base.SuspendLayout();
			this.customProgressBarWithTitle.BackColor = Color.Transparent;
			this.customProgressBarWithTitle.BarBackColor = Color.FromArgb(198, 198, 198);
			this.customProgressBarWithTitle.BarForeColor = Color.FromArgb(0, 114, 198);
			this.customProgressBarWithTitle.Location = new Point(0, 32);
			this.customProgressBarWithTitle.Margin = new Padding(0);
			this.customProgressBarWithTitle.Name = "customProgressBarWithTitle";
			this.customProgressBarWithTitle.Size = new Size(721, 62);
			this.customProgressBarWithTitle.TabIndex = 0;
			this.customProgressBarWithTitle.Title = "ProgressBar Title";
			this.customProgressBarWithTitle.TitleBarGap = 20;
			this.customProgressBarWithTitle.TitlePercentageGap = 20;
			this.customProgressBarWithTitle.Value = 0;
			base.Controls.Add(this.customProgressBarWithTitle);
			base.Margin = new Padding(0);
			base.Name = "ProgressPageBase";
			base.ResumeLayout(false);
		}

		// Token: 0x0400001C RID: 28
		private IContainer components;

		// Token: 0x0400001D RID: 29
		protected CustomProgressBarWithTitle customProgressBarWithTitle;
	}
}
