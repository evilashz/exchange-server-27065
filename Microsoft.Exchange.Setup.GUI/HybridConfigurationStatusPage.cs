using System;
using System.ComponentModel;
using System.Drawing;
using System.Management.Automation;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000006 RID: 6
	internal class HybridConfigurationStatusPage : SetupWizardPage
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00005238 File Offset: 0x00003438
		public HybridConfigurationStatusPage(IHybridConfigurationDetection hybridConfigurationDetectionProvider)
		{
			this.hybridConfigurationDetectionProvider = (HybridConfigurationDetection)hybridConfigurationDetectionProvider;
			this.InitializeComponent();
			base.Name = "HybridConfigurationStatusPage";
			base.PageTitle = Strings.HybridConfigurationStatusPageTitle;
			base.SetActive += this.HybridConfigurationStatusPage_SetActive;
			base.CheckLoaded += this.HybridConfigurationStatusPage_CheckLoaded;
			base.WizardCancel += this.HybridConfigurationStatusPage_WizardCancel;
			base.WizardFailed += this.HybridConfigurationStatusPage_WizardFailed;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000052C0 File Offset: 0x000034C0
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000052C8 File Offset: 0x000034C8
		internal PSCredential Credential { get; set; }

		// Token: 0x06000046 RID: 70 RVA: 0x000052D1 File Offset: 0x000034D1
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000052F0 File Offset: 0x000034F0
		private void StartHybridTest()
		{
			try
			{
				this.UpdateHybridTestStatus(Strings.HybridConfigurationCredentialsChecks);
				if (this.Credential != null)
				{
					bool flag = this.hybridConfigurationDetectionProvider.RunTenantHybridTest(this.Credential, string.Empty);
					if (flag)
					{
						this.UpdateHybridTestStatus(Strings.HybridConfigurationCredentialsFinished);
						base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
						base.SetExitFlag(false);
						return;
					}
					this.UpdateHybridTestStatus(Strings.HybridConfigurationCredentialsFailed);
				}
				else
				{
					this.UpdateHybridTestStatus(Strings.InvalidCredentials);
				}
				this.SetExitButton();
			}
			catch (Exception message)
			{
				this.UpdateHybridTestStatus(message);
				this.SetExitButton();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000539C File Offset: 0x0000359C
		private void SetExitButton()
		{
			base.SetExitFlag(true);
			base.SetBtnNextText(Strings.btnExit);
			base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000053BC File Offset: 0x000035BC
		private void UpdateHybridTestStatus(object message)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new HybridConfigurationStatusPage.UpdateStatusBox(this.UpdateHybridTestStatus), new object[]
				{
					message
				});
				return;
			}
			string text = null;
			if (message is LocalizedException)
			{
				LocalizedException ex = (LocalizedException)message;
				text = ex.LocalizedString.ToString();
				SetupLogger.Log(new LocalizedString(message.ToString()));
			}
			else if (message is ILocalizedString)
			{
				text = message.ToString();
				SetupLogger.Log(new LocalizedString(text));
			}
			else if (message is HybridConfigurationDetectionException)
			{
				HybridConfigurationDetectionException ex2 = (HybridConfigurationDetectionException)message;
				SetupLogger.LogError(ex2);
				text = ex2.Message;
			}
			else if (message is Exception)
			{
				Exception e = (Exception)message;
				text = Strings.HybridConfigurationSystemExceptionText;
				SetupLogger.LogError(e);
			}
			this.progressLabel.Text = text;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005498 File Offset: 0x00003698
		private void HybridConfigurationStatusPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.None);
			base.SetVisibleWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			base.SetExitFlag(false);
			base.SetBtnNextText(Strings.btnNext);
			base.EnableCheckLoadedTimer(200);
			this.StartHybridTest();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000054E8 File Offset: 0x000036E8
		private void HybridConfigurationStatusPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.progressLabel.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000552E File Offset: 0x0000372E
		private void HybridConfigurationStatusPage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005536 File Offset: 0x00003736
		private void HybridConfigurationStatusPage_WizardFailed(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Error);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00005540 File Offset: 0x00003740
		private void InitializeComponent()
		{
			this.progressLabel = new Label();
			base.SuspendLayout();
			this.progressLabel.AutoSize = true;
			this.progressLabel.FlatStyle = FlatStyle.Flat;
			this.progressLabel.Location = new Point(0, 0);
			this.progressLabel.MaximumSize = new Size(720, 0);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new Size(123, 17);
			this.progressLabel.TabIndex = 0;
			this.progressLabel.Text = "[ProgressLabelText]";
			base.Controls.Add(this.progressLabel);
			base.Name = "HybridConfigurationStatusPage";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000031 RID: 49
		private IContainer components;

		// Token: 0x04000032 RID: 50
		private Label progressLabel;

		// Token: 0x04000033 RID: 51
		private HybridConfigurationDetection hybridConfigurationDetectionProvider;

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x06000050 RID: 80
		private delegate void UpdateStatusBox(object msg);
	}
}
