using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000015 RID: 21
	internal class UninstallSummaryPage : SetupWizardPage
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x0000AA68 File Offset: 0x00008C68
		public UninstallSummaryPage()
		{
			this.InitializeComponent();
			base.PageTitle = Strings.UninstallWelcomeTitle;
			this.uninstallSummaryLabel.Text = Strings.UninstallWelcomeDiscription;
			base.WizardCancel += this.UninstallSummaryPage_WizardCancel;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000AAD7 File Offset: 0x00008CD7
		private void UninstallSummaryPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.Next);
			base.SetVisibleWizardButtons(WizardButtons.Next);
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000AB00 File Offset: 0x00008D00
		private void UninstallSummaryPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.uninstallSummaryLabel.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000AB46 File Offset: 0x00008D46
		private void UninstallSummaryPage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000AB50 File Offset: 0x00008D50
		private void InitializeComponent()
		{
			this.uninstallSummaryLabel = new Label();
			base.SuspendLayout();
			this.uninstallSummaryLabel.AutoSize = true;
			this.uninstallSummaryLabel.BackColor = Color.Transparent;
			this.uninstallSummaryLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.uninstallSummaryLabel.Location = new Point(0, 0);
			this.uninstallSummaryLabel.MaximumSize = new Size(720, 125);
			this.uninstallSummaryLabel.Name = "uninstallSummaryLabel";
			this.uninstallSummaryLabel.Size = new Size(143, 17);
			this.uninstallSummaryLabel.TabIndex = 26;
			this.uninstallSummaryLabel.Text = "[UninstallSummaryText]";
			base.Controls.Add(this.uninstallSummaryLabel);
			base.Name = "UninstallSummaryPage";
			base.SetActive += this.UninstallSummaryPage_SetActive;
			base.CheckLoaded += this.UninstallSummaryPage_CheckLoaded;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000097 RID: 151
		private Label uninstallSummaryLabel;

		// Token: 0x04000098 RID: 152
		private IContainer components;
	}
}
