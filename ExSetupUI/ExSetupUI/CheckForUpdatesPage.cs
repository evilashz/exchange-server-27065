using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Exchange.Setup.AcquireLanguagePack;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Win32;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000003 RID: 3
	internal class CheckForUpdatesPage : SetupWizardPage
	{
		// Token: 0x06000040 RID: 64 RVA: 0x0000296C File Offset: 0x00000B6C
		public CheckForUpdatesPage()
		{
			this.InitializeComponent();
			base.PageTitle = Strings.CheckForUpdatesPageTitle;
			this.checkForUpdateLabel.Text = Strings.CheckForUpdateLabelText;
			this.checkForUpdateYesNoLabel.Text = Strings.CheckForUpdateYesNoLabelText;
			this.checkForUpdateYesRadioButton.Text = Strings.CheckForUpdateYesRadioButtonText;
			this.checkForUpdateNoRadioButton.Text = Strings.CheckForUpdateNoRadioButtonText;
			this.previousDownloadedUpdatesRadioButton.Text = Strings.PreviousDownloadedUpdatesRadioButtonText;
			base.WizardCancel += this.CheckForUpdatesPage_WizardCancel;
			this.checkForUpdateYesRadioButton.Checked = true;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A1C File Offset: 0x00000C1C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A3B File Offset: 0x00000C3B
		public override void OnWizardNext(WizardPageEventArgs e)
		{
			this.SetNextPageVisible();
			base.OnWizardNext(e);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A4C File Offset: 0x00000C4C
		private void CheckForUpdatesPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			this.previousDownloadedUpdatesRadioButton.Visible = false;
			base.SetVisibleWizardButtons(WizardButtons.Next);
			this.CheckRegistry();
			if (this.checkForUpdateYesRadioButton.Checked || this.previousDownloadedUpdatesRadioButton.Checked)
			{
				base.SetWizardButtons(WizardButtons.Next);
				base.SetPageVisibleControl(base.Name, "UpdatesDownloadsPage", true);
			}
			else if (this.checkForUpdateNoRadioButton.Checked)
			{
				base.SetWizardButtons(WizardButtons.Next);
			}
			else
			{
				base.SetWizardButtons(WizardButtons.None);
			}
			UpdatesDownloadsPage updatesDownloadsPage = base.FindPage("UpdatesDownloadsPage") as UpdatesDownloadsPage;
			if (updatesDownloadsPage != null)
			{
				if (this.previousDownloadedUpdatesRadioButton.Checked)
				{
					updatesDownloadsPage.UsePreviousDownloadedUpdates = true;
					this.previousDownloadedUpdatesRadioButton.Visible = true;
				}
				else
				{
					updatesDownloadsPage.UsePreviousDownloadedUpdates = false;
				}
			}
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B18 File Offset: 0x00000D18
		private void CheckForUpdatesPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.checkForUpdateYesRadioButton.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B5E File Offset: 0x00000D5E
		private void CheckForUpdatesPage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B68 File Offset: 0x00000D68
		private void CheckRegistry()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SetupChecksRegistryConstant.RegistryPathForLanguagePack, true))
			{
				bool flag = true;
				if (registryKey != null)
				{
					string text = (string)registryKey.GetValue("LanguagePackBundlePath");
					if (!string.IsNullOrEmpty(text) && Directory.Exists(text) && LanguagePackXmlHelper.ContainsOnlyDownloadedFiles(text))
					{
						Logger.LoggerMessage(Strings.RegistryKeyForLanguagePackFound);
						flag = false;
						this.previousDownloadedUpdatesRadioButton.Visible = true;
						this.checkForUpdateYesRadioButton.Checked = false;
						this.checkForUpdateNoRadioButton.Checked = false;
						this.previousDownloadedUpdatesRadioButton.Checked = true;
					}
					if (flag)
					{
						registryKey.DeleteValue("LanguagePackBundlePath", false);
						this.previousDownloadedUpdatesRadioButton.Visible = false;
						this.previousDownloadedUpdatesRadioButton.Checked = false;
					}
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C3C File Offset: 0x00000E3C
		private void RadioButtonCheckChanged(object sender, EventArgs e)
		{
			CustomRadiobutton customRadiobutton = (CustomRadiobutton)sender;
			if (customRadiobutton.Checked)
			{
				if (customRadiobutton == this.checkForUpdateYesRadioButton)
				{
					this.checkForUpdateNoRadioButton.Checked = false;
					this.previousDownloadedUpdatesRadioButton.Checked = false;
					return;
				}
				if (customRadiobutton == this.checkForUpdateNoRadioButton)
				{
					this.checkForUpdateYesRadioButton.Checked = false;
					this.previousDownloadedUpdatesRadioButton.Checked = false;
					return;
				}
				this.checkForUpdateYesRadioButton.Checked = false;
				this.checkForUpdateNoRadioButton.Checked = false;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CB4 File Offset: 0x00000EB4
		private void SetNextPageVisible()
		{
			bool visible = this.checkForUpdateYesRadioButton.Checked || this.previousDownloadedUpdatesRadioButton.Checked;
			UpdatesDownloadsPage updatesDownloadsPage = base.FindPage("UpdatesDownloadsPage") as UpdatesDownloadsPage;
			updatesDownloadsPage.UsePreviousDownloadedUpdates = this.previousDownloadedUpdatesRadioButton.Checked;
			base.SetPageVisibleControl(base.Name, "UpdatesDownloadsPage", visible);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D14 File Offset: 0x00000F14
		private void InitializeComponent()
		{
			this.checkForUpdateLabel = new Label();
			this.checkForUpdateNoRadioButton = new CustomRadiobutton();
			this.checkForUpdateYesRadioButton = new CustomRadiobutton();
			this.checkForUpdateYesNoLabel = new Label();
			this.previousDownloadedUpdatesRadioButton = new CustomRadiobutton();
			this.descriptionPanel = new Panel();
			this.optionsPanel = new Panel();
			this.spacerPanel = new Panel();
			this.descriptionPanel.SuspendLayout();
			this.optionsPanel.SuspendLayout();
			base.SuspendLayout();
			this.checkForUpdateLabel.AutoSize = true;
			this.checkForUpdateLabel.BackColor = Color.Transparent;
			this.checkForUpdateLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.checkForUpdateLabel.ForeColor = Color.FromArgb(51, 51, 51);
			this.checkForUpdateLabel.Location = new Point(0, 0);
			this.checkForUpdateLabel.MaximumSize = new Size(720, 125);
			this.checkForUpdateLabel.Name = "checkForUpdateLabel";
			this.checkForUpdateLabel.Size = new Size(136, 17);
			this.checkForUpdateLabel.TabIndex = 17;
			this.checkForUpdateLabel.Text = "[CheckForUpdateText]";
			this.checkForUpdateNoRadioButton.AutoSize = true;
			this.checkForUpdateNoRadioButton.BackColor = Color.Transparent;
			this.checkForUpdateNoRadioButton.Checked = false;
			this.checkForUpdateNoRadioButton.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.checkForUpdateNoRadioButton.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.checkForUpdateNoRadioButton.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.checkForUpdateNoRadioButton.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.checkForUpdateNoRadioButton.Highligted = false;
			this.checkForUpdateNoRadioButton.Location = new Point(0, 54);
			this.checkForUpdateNoRadioButton.Margin = new Padding(2);
			this.checkForUpdateNoRadioButton.Name = "checkForUpdateNoRadioButton";
			this.checkForUpdateNoRadioButton.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.checkForUpdateNoRadioButton.Size = new Size(646, 19);
			this.checkForUpdateNoRadioButton.TabIndex = 21;
			this.checkForUpdateNoRadioButton.Text = "[CheckForUpdateNoRadioButton]";
			this.checkForUpdateNoRadioButton.TextGap = 10;
			this.checkForUpdateNoRadioButton.CheckedChanged += this.RadioButtonCheckChanged;
			this.checkForUpdateYesRadioButton.AutoSize = true;
			this.checkForUpdateYesRadioButton.BackColor = Color.Transparent;
			this.checkForUpdateYesRadioButton.Checked = false;
			this.checkForUpdateYesRadioButton.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.checkForUpdateYesRadioButton.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.checkForUpdateYesRadioButton.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.checkForUpdateYesRadioButton.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.checkForUpdateYesRadioButton.Highligted = false;
			this.checkForUpdateYesRadioButton.Location = new Point(0, 25);
			this.checkForUpdateYesRadioButton.Margin = new Padding(2);
			this.checkForUpdateYesRadioButton.Name = "checkForUpdateYesRadioButton";
			this.checkForUpdateYesRadioButton.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.checkForUpdateYesRadioButton.Size = new Size(646, 19);
			this.checkForUpdateYesRadioButton.TabIndex = 20;
			this.checkForUpdateYesRadioButton.Text = "[CheckForUpdateYesRadioButton]";
			this.checkForUpdateYesRadioButton.TextGap = 10;
			this.checkForUpdateYesRadioButton.CheckedChanged += this.RadioButtonCheckChanged;
			this.checkForUpdateYesNoLabel.AutoSize = true;
			this.checkForUpdateYesNoLabel.BackColor = Color.Transparent;
			this.checkForUpdateYesNoLabel.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Pixel, 0);
			this.checkForUpdateYesNoLabel.ForeColor = Color.FromArgb(51, 51, 51);
			this.checkForUpdateYesNoLabel.Location = new Point(0, 0);
			this.checkForUpdateYesNoLabel.Margin = new Padding(2, 0, 2, 0);
			this.checkForUpdateYesNoLabel.Name = "checkForUpdateYesNoLabel";
			this.checkForUpdateYesNoLabel.Size = new Size(212, 17);
			this.checkForUpdateYesNoLabel.TabIndex = 18;
			this.checkForUpdateYesNoLabel.Text = "[CheckForUpdateYesNoLabelText]";
			this.previousDownloadedUpdatesRadioButton.AutoSize = true;
			this.previousDownloadedUpdatesRadioButton.BackColor = Color.Transparent;
			this.previousDownloadedUpdatesRadioButton.Checked = false;
			this.previousDownloadedUpdatesRadioButton.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.previousDownloadedUpdatesRadioButton.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.previousDownloadedUpdatesRadioButton.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.previousDownloadedUpdatesRadioButton.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.previousDownloadedUpdatesRadioButton.Highligted = false;
			this.previousDownloadedUpdatesRadioButton.Location = new Point(0, 83);
			this.previousDownloadedUpdatesRadioButton.Margin = new Padding(2);
			this.previousDownloadedUpdatesRadioButton.Name = "previousDownloadedUpdatesRadioButton";
			this.previousDownloadedUpdatesRadioButton.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.previousDownloadedUpdatesRadioButton.Size = new Size(646, 19);
			this.previousDownloadedUpdatesRadioButton.TabIndex = 22;
			this.previousDownloadedUpdatesRadioButton.Text = "[UsePreviouslyDownloadedRadioButton]";
			this.previousDownloadedUpdatesRadioButton.TextGap = 10;
			this.previousDownloadedUpdatesRadioButton.Visible = false;
			this.previousDownloadedUpdatesRadioButton.CheckedChanged += this.RadioButtonCheckChanged;
			this.descriptionPanel.AutoSize = true;
			this.descriptionPanel.Controls.Add(this.checkForUpdateLabel);
			this.descriptionPanel.Dock = DockStyle.Top;
			this.descriptionPanel.Location = new Point(0, 0);
			this.descriptionPanel.Name = "descriptionPanel";
			this.descriptionPanel.Size = new Size(561, 17);
			this.descriptionPanel.TabIndex = 23;
			this.optionsPanel.Controls.Add(this.checkForUpdateYesNoLabel);
			this.optionsPanel.Controls.Add(this.previousDownloadedUpdatesRadioButton);
			this.optionsPanel.Controls.Add(this.checkForUpdateNoRadioButton);
			this.optionsPanel.Controls.Add(this.checkForUpdateYesRadioButton);
			this.optionsPanel.Dock = DockStyle.Fill;
			this.optionsPanel.Location = new Point(0, 57);
			this.optionsPanel.Name = "optionsPanel";
			this.optionsPanel.Size = new Size(561, 254);
			this.optionsPanel.TabIndex = 24;
			this.spacerPanel.Dock = DockStyle.Top;
			this.spacerPanel.Location = new Point(0, 17);
			this.spacerPanel.Name = "spacerPanel";
			this.spacerPanel.Size = new Size(561, 40);
			this.spacerPanel.TabIndex = 25;
			base.AutoScaleDimensions = new SizeF(7f, 15f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.Controls.Add(this.optionsPanel);
			base.Controls.Add(this.spacerPanel);
			base.Controls.Add(this.descriptionPanel);
			base.Margin = new Padding(2);
			base.Name = "CheckForUpdatesPage";
			base.Size = new Size(561, 311);
			base.SetActive += this.CheckForUpdatesPage_SetActive;
			base.CheckLoaded += this.CheckForUpdatesPage_CheckLoaded;
			this.descriptionPanel.ResumeLayout(false);
			this.descriptionPanel.PerformLayout();
			this.optionsPanel.ResumeLayout(false);
			this.optionsPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000012 RID: 18
		private const string UpdatesDownloadsPageName = "UpdatesDownloadsPage";

		// Token: 0x04000013 RID: 19
		private Label checkForUpdateLabel;

		// Token: 0x04000014 RID: 20
		private CustomRadiobutton previousDownloadedUpdatesRadioButton;

		// Token: 0x04000015 RID: 21
		private CustomRadiobutton checkForUpdateNoRadioButton;

		// Token: 0x04000016 RID: 22
		private CustomRadiobutton checkForUpdateYesRadioButton;

		// Token: 0x04000017 RID: 23
		private Label checkForUpdateYesNoLabel;

		// Token: 0x04000018 RID: 24
		private Panel descriptionPanel;

		// Token: 0x04000019 RID: 25
		private Panel optionsPanel;

		// Token: 0x0400001A RID: 26
		private Panel spacerPanel;

		// Token: 0x0400001B RID: 27
		private IContainer components;
	}
}
