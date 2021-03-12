using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000008 RID: 8
	internal class IncompleteInstallationDetectedPage : SetupWizardPage
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00005607 File Offset: 0x00003807
		public IncompleteInstallationDetectedPage()
		{
			this.InitializeComponent();
			base.PageTitle = Strings.IncompleteInstallationDetectedPageTitle;
			this.incompleteInstallationDetectedSummaryLabel.Text = string.Empty;
			base.WizardCancel += this.IncompleteInstallationDetectedPage_WizardCancel;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00005647 File Offset: 0x00003847
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00005666 File Offset: 0x00003866
		private void IncompleteInstallationDetectedPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.Next);
			base.SetVisibleWizardButtons(WizardButtons.Next);
			this.incompleteInstallationDetectedSummaryLabel.Text = Strings.IncompleteInstallationDetectedSummaryLabelText;
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000056A4 File Offset: 0x000038A4
		private void IncompleteInstallationDetectedPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.incompleteInstallationDetectedSummaryLabel.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000056EA File Offset: 0x000038EA
		private void IncompleteInstallationDetectedPage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000056F4 File Offset: 0x000038F4
		private void InitializeComponent()
		{
			this.incompleteInstallationDetectedSummaryLabel = new Label();
			base.SuspendLayout();
			this.incompleteInstallationDetectedSummaryLabel.AutoSize = true;
			this.incompleteInstallationDetectedSummaryLabel.BackColor = Color.Transparent;
			this.incompleteInstallationDetectedSummaryLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.incompleteInstallationDetectedSummaryLabel.Location = new Point(0, 0);
			this.incompleteInstallationDetectedSummaryLabel.MaximumSize = new Size(720, 125);
			this.incompleteInstallationDetectedSummaryLabel.Name = "incompleteInstallationDetectedSummaryLabel";
			this.incompleteInstallationDetectedSummaryLabel.Size = new Size(272, 17);
			this.incompleteInstallationDetectedSummaryLabel.TabIndex = 29;
			this.incompleteInstallationDetectedSummaryLabel.Text = "[IncompleteInstallationDetectedSummaryText]";
			base.Controls.Add(this.incompleteInstallationDetectedSummaryLabel);
			base.Name = "IncompleteInstallationDetectedPage";
			base.SetActive += this.IncompleteInstallationDetectedPage_SetActive;
			base.CheckLoaded += this.IncompleteInstallationDetectedPage_CheckLoaded;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000035 RID: 53
		private IContainer components;

		// Token: 0x04000036 RID: 54
		private Label incompleteInstallationDetectedSummaryLabel;
	}
}
