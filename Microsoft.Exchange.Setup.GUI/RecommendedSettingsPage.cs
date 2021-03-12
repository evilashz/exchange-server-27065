using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x0200000F RID: 15
	internal class RecommendedSettingsPage : SetupWizardPage
	{
		// Token: 0x06000089 RID: 137 RVA: 0x0000767C File Offset: 0x0000587C
		public RecommendedSettingsPage(InstallModeDataHandler installModeDataHandler)
		{
			this.installModeDataHandler = installModeDataHandler;
			this.InitializeComponent();
			base.PageTitle = Strings.RecommendedSettingsTitle;
			this.useRecommendedSettingsDescriptionLabel.Text = Strings.UseRecommendedSettingsDescription;
			this.notUseRecommendedSettingDescriptionLabel.Text = Strings.NotUseRecommendedSettingsDescription;
			this.useSettingsRadioButton.Text = Strings.UseSettingsRadioButtonText;
			this.doNotUseSettingsRadioButton.Text = Strings.DoNotUseSettingsRadioButtonText;
			this.checkForErrorSolutionsLinkLabelLinkLabel.Text = Strings.ReadMoreAboutCheckingForErrorSolutionsLinkText;
			this.checkForErrorSolutionsLinkLabelLinkLabel.Links.Add(0, 0, "http://go.microsoft.com/fwlink/p/?LinkId=260761");
			this.provideUsageFeedbackLinkLabel.Text = Strings.ReadMoreAboutUsageLinkText;
			this.provideUsageFeedbackLinkLabel.Links.Add(0, 0, "http://go.microsoft.com/fwlink/?LinkID=64471");
			this.useSettingsRadioButton.Checked = true;
			base.WizardCancel += this.RecommendedSettingsPage_WizardCancel;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00007778 File Offset: 0x00005978
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00007798 File Offset: 0x00005998
		private void RecommendedSettingsPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.Previous);
			base.SetVisibleWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			if (this.useSettingsRadioButton.Checked || this.doNotUseSettingsRadioButton.Checked)
			{
				base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			}
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000077EC File Offset: 0x000059EC
		private void RecommendedSettingsPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.useSettingsRadioButton.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00007834 File Offset: 0x00005A34
		private void RadioButtonCheckChanged(object sender, EventArgs e)
		{
			CustomRadiobutton customRadiobutton = (CustomRadiobutton)sender;
			if (customRadiobutton.Checked)
			{
				if (customRadiobutton == this.useSettingsRadioButton)
				{
					this.installModeDataHandler.WatsonEnabled = true;
					this.installModeDataHandler.CustomerFeedbackEnabled = new bool?(true);
					this.doNotUseSettingsRadioButton.Checked = false;
					return;
				}
				this.installModeDataHandler.WatsonEnabled = false;
				this.installModeDataHandler.CustomerFeedbackEnabled = new bool?(false);
				this.useSettingsRadioButton.Checked = false;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000078AC File Offset: 0x00005AAC
		private void ErrorReportingLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.checkForErrorSolutionsLinkLabelLinkLabel.Links[this.checkForErrorSolutionsLinkLabelLinkLabel.Links.IndexOf(e.Link)].Visited = true;
			Process.Start(e.Link.LinkData.ToString());
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000078FC File Offset: 0x00005AFC
		private void CustomerExperienceProgramLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.provideUsageFeedbackLinkLabel.Links[this.provideUsageFeedbackLinkLabel.Links.IndexOf(e.Link)].Visited = true;
			Process.Start(e.Link.LinkData.ToString());
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000794B File Offset: 0x00005B4B
		private void RecommendedSettingsPage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00007954 File Offset: 0x00005B54
		private void InitializeComponent()
		{
			this.useChoicePanel = new Panel();
			this.useChoiseDescriptionPanel = new Panel();
			this.useRecommendedSettingsDescriptionLabel = new Label();
			this.useSettingsRadioButton = new CustomRadiobutton();
			this.notUseChoicePanel = new Panel();
			this.notUseChoiseDescriptionPanel = new Panel();
			this.notUseRecommendedSettingDescriptionLabel = new Label();
			this.doNotUseSettingsRadioButton = new CustomRadiobutton();
			this.linkPanel = new Panel();
			this.checkForErrorSolutionsLinkLabelLinkLabel = new LinkLabel();
			this.provideUsageFeedbackLinkLabel = new LinkLabel();
			this.useChoicePanel.SuspendLayout();
			this.useChoiseDescriptionPanel.SuspendLayout();
			this.notUseChoicePanel.SuspendLayout();
			this.notUseChoiseDescriptionPanel.SuspendLayout();
			this.linkPanel.SuspendLayout();
			base.SuspendLayout();
			this.useChoicePanel.Controls.Add(this.useChoiseDescriptionPanel);
			this.useChoicePanel.Controls.Add(this.useSettingsRadioButton);
			this.useChoicePanel.Dock = DockStyle.Top;
			this.useChoicePanel.Location = new Point(0, 0);
			this.useChoicePanel.Name = "useChoicePanel";
			this.useChoicePanel.Size = new Size(721, 100);
			this.useChoicePanel.TabIndex = 30;
			this.useChoiseDescriptionPanel.Controls.Add(this.useRecommendedSettingsDescriptionLabel);
			this.useChoiseDescriptionPanel.Location = new Point(25, 27);
			this.useChoiseDescriptionPanel.Name = "useChoiseDescriptionPanel";
			this.useChoiseDescriptionPanel.Size = new Size(696, 65);
			this.useChoiseDescriptionPanel.TabIndex = 31;
			this.useRecommendedSettingsDescriptionLabel.AutoSize = true;
			this.useRecommendedSettingsDescriptionLabel.BackColor = Color.Transparent;
			this.useRecommendedSettingsDescriptionLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.useRecommendedSettingsDescriptionLabel.Location = new Point(0, 0);
			this.useRecommendedSettingsDescriptionLabel.MaximumSize = new Size(700, 125);
			this.useRecommendedSettingsDescriptionLabel.Name = "useRecommendedSettingsDescriptionLabel";
			this.useRecommendedSettingsDescriptionLabel.Size = new Size(269, 17);
			this.useRecommendedSettingsDescriptionLabel.TabIndex = 30;
			this.useRecommendedSettingsDescriptionLabel.Text = "[UseRecommendedSettingsDescriptionLabel]";
			this.useSettingsRadioButton.AutoSize = true;
			this.useSettingsRadioButton.BackColor = Color.Transparent;
			this.useSettingsRadioButton.Checked = false;
			this.useSettingsRadioButton.DisabledColor = Color.FromArgb(221, 221, 221);
			this.useSettingsRadioButton.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.useSettingsRadioButton.ForeColor = Color.FromArgb(80, 80, 80);
			this.useSettingsRadioButton.HighlightedColor = Color.FromArgb(51, 51, 51);
			this.useSettingsRadioButton.Highligted = false;
			this.useSettingsRadioButton.Location = new Point(0, 0);
			this.useSettingsRadioButton.Name = "useSettingsRadioButton";
			this.useSettingsRadioButton.NormalColor = Color.FromArgb(80, 80, 80);
			this.useSettingsRadioButton.Size = new Size(1738, 19);
			this.useSettingsRadioButton.TabIndex = 27;
			this.useSettingsRadioButton.Text = "[UseSettingsRadioButton]";
			this.useSettingsRadioButton.TextGap = 10;
			this.useSettingsRadioButton.CheckedChanged += this.RadioButtonCheckChanged;
			this.notUseChoicePanel.Controls.Add(this.notUseChoiseDescriptionPanel);
			this.notUseChoicePanel.Controls.Add(this.doNotUseSettingsRadioButton);
			this.notUseChoicePanel.Dock = DockStyle.Top;
			this.notUseChoicePanel.Location = new Point(0, 100);
			this.notUseChoicePanel.Name = "notUseChoicePanel";
			this.notUseChoicePanel.Size = new Size(721, 100);
			this.notUseChoicePanel.TabIndex = 32;
			this.notUseChoiseDescriptionPanel.Controls.Add(this.notUseRecommendedSettingDescriptionLabel);
			this.notUseChoiseDescriptionPanel.Location = new Point(25, 27);
			this.notUseChoiseDescriptionPanel.Name = "notUseChoiseDescriptionPanel";
			this.notUseChoiseDescriptionPanel.Size = new Size(696, 65);
			this.notUseChoiseDescriptionPanel.TabIndex = 32;
			this.notUseRecommendedSettingDescriptionLabel.AutoSize = true;
			this.notUseRecommendedSettingDescriptionLabel.BackColor = Color.Transparent;
			this.notUseRecommendedSettingDescriptionLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.notUseRecommendedSettingDescriptionLabel.Location = new Point(0, 0);
			this.notUseRecommendedSettingDescriptionLabel.MaximumSize = new Size(700, 125);
			this.notUseRecommendedSettingDescriptionLabel.Name = "notUseRecommendedSettingDescriptionLabel";
			this.notUseRecommendedSettingDescriptionLabel.Size = new Size(291, 17);
			this.notUseRecommendedSettingDescriptionLabel.TabIndex = 31;
			this.notUseRecommendedSettingDescriptionLabel.Text = "[NotUseRecommendedSettingsDescriptionLabel]";
			this.doNotUseSettingsRadioButton.AutoSize = true;
			this.doNotUseSettingsRadioButton.BackColor = Color.Transparent;
			this.doNotUseSettingsRadioButton.Checked = false;
			this.doNotUseSettingsRadioButton.DisabledColor = Color.FromArgb(221, 221, 221);
			this.doNotUseSettingsRadioButton.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.doNotUseSettingsRadioButton.ForeColor = Color.FromArgb(80, 80, 80);
			this.doNotUseSettingsRadioButton.HighlightedColor = Color.FromArgb(51, 51, 51);
			this.doNotUseSettingsRadioButton.Highligted = false;
			this.doNotUseSettingsRadioButton.Location = new Point(0, 0);
			this.doNotUseSettingsRadioButton.Name = "doNotUseSettingsRadioButton";
			this.doNotUseSettingsRadioButton.NormalColor = Color.FromArgb(80, 80, 80);
			this.doNotUseSettingsRadioButton.Size = new Size(888, 19);
			this.doNotUseSettingsRadioButton.TabIndex = 28;
			this.doNotUseSettingsRadioButton.Text = "[DoNotUseSettingsRadioButton]";
			this.doNotUseSettingsRadioButton.TextGap = 10;
			this.doNotUseSettingsRadioButton.CheckedChanged += this.RadioButtonCheckChanged;
			this.linkPanel.Controls.Add(this.checkForErrorSolutionsLinkLabelLinkLabel);
			this.linkPanel.Controls.Add(this.provideUsageFeedbackLinkLabel);
			this.linkPanel.Dock = DockStyle.Fill;
			this.linkPanel.Location = new Point(0, 200);
			this.linkPanel.Name = "linkPanel";
			this.linkPanel.Size = new Size(721, 235);
			this.linkPanel.TabIndex = 33;
			this.checkForErrorSolutionsLinkLabelLinkLabel.ActiveLinkColor = Color.Red;
			this.checkForErrorSolutionsLinkLabelLinkLabel.AutoSize = true;
			this.checkForErrorSolutionsLinkLabelLinkLabel.BackColor = Color.Transparent;
			this.checkForErrorSolutionsLinkLabelLinkLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.checkForErrorSolutionsLinkLabelLinkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
			this.checkForErrorSolutionsLinkLabelLinkLabel.LinkColor = Color.FromArgb(0, 114, 198);
			this.checkForErrorSolutionsLinkLabelLinkLabel.Location = new Point(0, 25);
			this.checkForErrorSolutionsLinkLabelLinkLabel.Name = "checkForErrorSolutionsLinkLabelLinkLabel";
			this.checkForErrorSolutionsLinkLabelLinkLabel.Size = new Size(258, 17);
			this.checkForErrorSolutionsLinkLabelLinkLabel.TabIndex = 26;
			this.checkForErrorSolutionsLinkLabelLinkLabel.TabStop = true;
			this.checkForErrorSolutionsLinkLabelLinkLabel.Text = "[CheckForErrorSolutionsLinkLabelLinkLabel]";
			this.checkForErrorSolutionsLinkLabelLinkLabel.LinkClicked += this.ErrorReportingLinkLabel_LinkClicked;
			this.provideUsageFeedbackLinkLabel.ActiveLinkColor = Color.Red;
			this.provideUsageFeedbackLinkLabel.AutoSize = true;
			this.provideUsageFeedbackLinkLabel.BackColor = Color.Transparent;
			this.provideUsageFeedbackLinkLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.provideUsageFeedbackLinkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
			this.provideUsageFeedbackLinkLabel.LinkColor = Color.FromArgb(0, 114, 198);
			this.provideUsageFeedbackLinkLabel.Location = new Point(0, 0);
			this.provideUsageFeedbackLinkLabel.Name = "provideUsageFeedbackLinkLabel";
			this.provideUsageFeedbackLinkLabel.Size = new Size(258, 17);
			this.provideUsageFeedbackLinkLabel.TabIndex = 25;
			this.provideUsageFeedbackLinkLabel.TabStop = true;
			this.provideUsageFeedbackLinkLabel.Text = "[ProvideUsageFeedbackLinkLabelLinkLabel]";
			this.provideUsageFeedbackLinkLabel.LinkClicked += this.CustomerExperienceProgramLinkLabel_LinkClicked;
			base.AutoScaleDimensions = new SizeF(7f, 15f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.linkPanel);
			base.Controls.Add(this.notUseChoicePanel);
			base.Controls.Add(this.useChoicePanel);
			base.Name = "RecommendedSettingsPage";
			base.SetActive += this.RecommendedSettingsPage_SetActive;
			base.CheckLoaded += this.RecommendedSettingsPage_CheckLoaded;
			this.useChoicePanel.ResumeLayout(false);
			this.useChoicePanel.PerformLayout();
			this.useChoiseDescriptionPanel.ResumeLayout(false);
			this.useChoiseDescriptionPanel.PerformLayout();
			this.notUseChoicePanel.ResumeLayout(false);
			this.notUseChoicePanel.PerformLayout();
			this.notUseChoiseDescriptionPanel.ResumeLayout(false);
			this.notUseChoiseDescriptionPanel.PerformLayout();
			this.linkPanel.ResumeLayout(false);
			this.linkPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400005E RID: 94
		public const string CeipProgramLink = "http://go.microsoft.com/fwlink/?LinkID=64471";

		// Token: 0x0400005F RID: 95
		public const string ErrorReportingLink = "http://go.microsoft.com/fwlink/p/?LinkId=260761";

		// Token: 0x04000060 RID: 96
		private IContainer components;

		// Token: 0x04000061 RID: 97
		private Panel useChoicePanel;

		// Token: 0x04000062 RID: 98
		private Panel notUseChoicePanel;

		// Token: 0x04000063 RID: 99
		private Panel linkPanel;

		// Token: 0x04000064 RID: 100
		private Label useRecommendedSettingsDescriptionLabel;

		// Token: 0x04000065 RID: 101
		private CustomRadiobutton useSettingsRadioButton;

		// Token: 0x04000066 RID: 102
		private CustomRadiobutton doNotUseSettingsRadioButton;

		// Token: 0x04000067 RID: 103
		private LinkLabel checkForErrorSolutionsLinkLabelLinkLabel;

		// Token: 0x04000068 RID: 104
		private LinkLabel provideUsageFeedbackLinkLabel;

		// Token: 0x04000069 RID: 105
		private Label notUseRecommendedSettingDescriptionLabel;

		// Token: 0x0400006A RID: 106
		private Panel useChoiseDescriptionPanel;

		// Token: 0x0400006B RID: 107
		private Panel notUseChoiseDescriptionPanel;

		// Token: 0x0400006C RID: 108
		private InstallModeDataHandler installModeDataHandler;
	}
}
