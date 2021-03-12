using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x0200000E RID: 14
	internal class ProtectionSettingsPage : SetupWizardPage
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00006E38 File Offset: 0x00005038
		public ProtectionSettingsPage(InstallModeDataHandler installModeDataHandler)
		{
			this.installBhdCfgDataHandler = installModeDataHandler.InstallBhdCfgDataHandler;
			this.InitializeComponent();
			base.PageTitle = Strings.ProtectionSettingsPageTitle;
			this.protectionSettingsLabel.Text = Strings.ProtectionSettingsLabelText;
			this.protectionSettingsYesNoLabel.Text = Strings.ProtectionSettingsYesNoLabelText;
			this.disableMalwareYesRadioButton.Text = Strings.DisableMalwareYesRadioButtonText;
			this.disableMalwareNoRadioButton.Text = Strings.DisableMalwareNoRadioButtonText;
			this.prereqNoteLabel.Text = Strings.PrereqNoteText;
			base.WizardCancel += this.ProtectionSettingsPage_WizardCancel;
			if (!InstallableUnitConfigurationInfo.SetupContext.DisableAMFiltering)
			{
				this.disableMalwareNoRadioButton.Checked = true;
				return;
			}
			this.disableMalwareYesRadioButton.Checked = true;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00006F0D File Offset: 0x0000510D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00006F2C File Offset: 0x0000512C
		private void ProtectionSettingsPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			base.SetVisibleWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00006F54 File Offset: 0x00005154
		private void ProtectionSettingsPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.disableMalwareYesRadioButton.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00006F9A File Offset: 0x0000519A
		private void ProtectionSettingsPage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00006FA4 File Offset: 0x000051A4
		private void OnCheckChanged(object sender, EventArgs e)
		{
			CustomRadiobutton customRadiobutton = (CustomRadiobutton)sender;
			if (customRadiobutton.Checked)
			{
				if (customRadiobutton == this.disableMalwareYesRadioButton)
				{
					this.disableMalwareNoRadioButton.Checked = false;
					this.installBhdCfgDataHandler.DisableAMFiltering = true;
					return;
				}
				this.disableMalwareYesRadioButton.Checked = false;
				this.installBhdCfgDataHandler.DisableAMFiltering = false;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006FFC File Offset: 0x000051FC
		private void InitializeComponent()
		{
			this.protectionSettingsYesNoLabel = new Label();
			this.protectionSettingsLabel = new Label();
			this.prereqNoteLabel = new Label();
			this.disableMalwareYesRadioButton = new CustomRadiobutton();
			this.disableMalwareNoRadioButton = new CustomRadiobutton();
			this.headerPanel = new Panel();
			this.spacerPanel = new Panel();
			this.optionPanel = new Panel();
			this.headerPanel.SuspendLayout();
			this.optionPanel.SuspendLayout();
			base.SuspendLayout();
			this.protectionSettingsYesNoLabel.AutoSize = true;
			this.protectionSettingsYesNoLabel.BackColor = Color.Transparent;
			this.protectionSettingsYesNoLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.protectionSettingsYesNoLabel.Location = new Point(0, 0);
			this.protectionSettingsYesNoLabel.Name = "protectionSettingsYesNoLabel";
			this.protectionSettingsYesNoLabel.Size = new Size(196, 15);
			this.protectionSettingsYesNoLabel.TabIndex = 18;
			this.protectionSettingsYesNoLabel.Text = "[ProtectionSettingsYesNoLabelText]";
			this.protectionSettingsLabel.AutoSize = true;
			this.protectionSettingsLabel.BackColor = Color.Transparent;
			this.protectionSettingsLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.protectionSettingsLabel.Location = new Point(0, 0);
			this.protectionSettingsLabel.MaximumSize = new Size(720, 0);
			this.protectionSettingsLabel.Name = "protectionSettingsLabel";
			this.protectionSettingsLabel.Size = new Size(134, 15);
			this.protectionSettingsLabel.TabIndex = 24;
			this.protectionSettingsLabel.Text = "[ProtectionSettingsText]";
			this.prereqNoteLabel.AutoSize = true;
			this.prereqNoteLabel.BackColor = Color.Transparent;
			this.prereqNoteLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.prereqNoteLabel.Location = new Point(0, 85);
			this.prereqNoteLabel.MaximumSize = new Size(740, 125);
			this.prereqNoteLabel.Name = "prereqNoteLabel";
			this.prereqNoteLabel.Size = new Size(97, 15);
			this.prereqNoteLabel.TabIndex = 26;
			this.prereqNoteLabel.Text = "[PrereqNoteText]";
			this.disableMalwareYesRadioButton.AutoSize = true;
			this.disableMalwareYesRadioButton.BackColor = Color.Transparent;
			this.disableMalwareYesRadioButton.Checked = false;
			this.disableMalwareYesRadioButton.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.disableMalwareYesRadioButton.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.disableMalwareYesRadioButton.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.disableMalwareYesRadioButton.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.disableMalwareYesRadioButton.Highligted = false;
			this.disableMalwareYesRadioButton.Location = new Point(3, 29);
			this.disableMalwareYesRadioButton.Name = "disableMalwareYesRadioButton";
			this.disableMalwareYesRadioButton.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.disableMalwareYesRadioButton.Size = new Size(540, 19);
			this.disableMalwareYesRadioButton.TabIndex = 27;
			this.disableMalwareYesRadioButton.Text = "[DisableMalwareYesRadioButton]";
			this.disableMalwareYesRadioButton.TextGap = 10;
			this.disableMalwareYesRadioButton.CheckedChanged += this.OnCheckChanged;
			this.disableMalwareNoRadioButton.AutoSize = true;
			this.disableMalwareNoRadioButton.BackColor = Color.Transparent;
			this.disableMalwareNoRadioButton.Checked = false;
			this.disableMalwareNoRadioButton.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.disableMalwareNoRadioButton.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.disableMalwareNoRadioButton.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.disableMalwareNoRadioButton.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.disableMalwareNoRadioButton.Highligted = false;
			this.disableMalwareNoRadioButton.Location = new Point(3, 54);
			this.disableMalwareNoRadioButton.Name = "disableMalwareNoRadioButton";
			this.disableMalwareNoRadioButton.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.disableMalwareNoRadioButton.Size = new Size(512, 19);
			this.disableMalwareNoRadioButton.TabIndex = 28;
			this.disableMalwareNoRadioButton.Text = "[DisableMalwareNoRadioButton]";
			this.disableMalwareNoRadioButton.TextGap = 10;
			this.disableMalwareNoRadioButton.CheckedChanged += this.OnCheckChanged;
			this.headerPanel.AutoSize = true;
			this.headerPanel.Controls.Add(this.protectionSettingsLabel);
			this.headerPanel.Dock = DockStyle.Top;
			this.headerPanel.Location = new Point(0, 0);
			this.headerPanel.Name = "headerPanel";
			this.headerPanel.Size = new Size(721, 15);
			this.headerPanel.TabIndex = 29;
			this.spacerPanel.Dock = DockStyle.Top;
			this.spacerPanel.Location = new Point(0, 15);
			this.spacerPanel.Name = "spacerPanel";
			this.spacerPanel.Size = new Size(721, 20);
			this.spacerPanel.TabIndex = 30;
			this.optionPanel.Controls.Add(this.protectionSettingsYesNoLabel);
			this.optionPanel.Controls.Add(this.prereqNoteLabel);
			this.optionPanel.Controls.Add(this.disableMalwareNoRadioButton);
			this.optionPanel.Controls.Add(this.disableMalwareYesRadioButton);
			this.optionPanel.Dock = DockStyle.Fill;
			this.optionPanel.Location = new Point(0, 35);
			this.optionPanel.Name = "optionPanel";
			this.optionPanel.Size = new Size(721, 400);
			this.optionPanel.TabIndex = 31;
			base.Controls.Add(this.optionPanel);
			base.Controls.Add(this.spacerPanel);
			base.Controls.Add(this.headerPanel);
			base.Name = "ProtectionSettingsPage";
			base.SetActive += this.ProtectionSettingsPage_SetActive;
			base.CheckLoaded += this.ProtectionSettingsPage_CheckLoaded;
			this.headerPanel.ResumeLayout(false);
			this.headerPanel.PerformLayout();
			this.optionPanel.ResumeLayout(false);
			this.optionPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000054 RID: 84
		private Label protectionSettingsYesNoLabel;

		// Token: 0x04000055 RID: 85
		private CustomRadiobutton disableMalwareNoRadioButton;

		// Token: 0x04000056 RID: 86
		private CustomRadiobutton disableMalwareYesRadioButton;

		// Token: 0x04000057 RID: 87
		private Label protectionSettingsLabel;

		// Token: 0x04000058 RID: 88
		private Label prereqNoteLabel;

		// Token: 0x04000059 RID: 89
		private InstallBhdCfgDataHandler installBhdCfgDataHandler;

		// Token: 0x0400005A RID: 90
		private Panel headerPanel;

		// Token: 0x0400005B RID: 91
		private Panel spacerPanel;

		// Token: 0x0400005C RID: 92
		private Panel optionPanel;

		// Token: 0x0400005D RID: 93
		private IContainer components;
	}
}
