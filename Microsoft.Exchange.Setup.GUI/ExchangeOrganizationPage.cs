using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000004 RID: 4
	internal class ExchangeOrganizationPage : SetupWizardPage
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000043C4 File Offset: 0x000025C4
		public ExchangeOrganizationPage(InstallModeDataHandler installModeDataHandler)
		{
			this.installOrgCfgDataHandler = installModeDataHandler.InstallOrgCfgDataHandler;
			this.InitializeComponent();
			base.PageTitle = Strings.ExchangeOrganizationPageTitle;
			this.exchangeOrgNameLabel.Text = Strings.ExchangeOrganizationName;
			this.adSplitPermissionCheckBox.Text = Strings.ActiveDirectorySplitPermissions;
			this.adSplitPermissionsDescripitionLabel.Text = Strings.ActiveDirectorySplitPermissionsDescription;
			IOrganizationName organizationName = this.installOrgCfgDataHandler.OrganizationName;
			this.exchangeOrgNameTextBox.Text = ((organizationName == null) ? string.Empty : organizationName.EscapedName);
			this.adSplitPermissionCheckBox.CheckedChanged += delegate(object param0, EventArgs param1)
			{
				this.OnIsADSplitPermissionCheckedChanged(EventArgs.Empty);
			};
			this.IsADSplitPermissionCheckedChanged += this.ADSplitPermission_Checked;
			this.exchangeOrgNameTextBox.Leave += this.ExchangeOrgName_Leave;
			this.exchangeOrgNameTextBox.Enter += this.ExchangeOrgName_Enter;
			base.WizardCancel += this.ExchangeOrganizationPage_WizardCancel;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600002C RID: 44 RVA: 0x000044D0 File Offset: 0x000026D0
		// (remove) Token: 0x0600002D RID: 45 RVA: 0x00004508 File Offset: 0x00002708
		internal event EventHandler IsADSplitPermissionCheckedChanged;

		// Token: 0x0600002E RID: 46 RVA: 0x0000453D File Offset: 0x0000273D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000455C File Offset: 0x0000275C
		private void ExchangeOrganizationPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			base.SetVisibleWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			this.adSplitPermissionCheckBox.Checked = false;
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000458F File Offset: 0x0000278F
		private void ExchangeOrganizationPage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004598 File Offset: 0x00002798
		private void ExchangeOrganizationPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.adSplitPermissionsDescripitionLabel.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000045E0 File Offset: 0x000027E0
		private void ADSplitPermission_Checked(object sender, EventArgs e)
		{
			if (this.adSplitPermissionCheckBox.Checked)
			{
				this.installOrgCfgDataHandler.ActiveDirectorySplitPermissions = new bool?(true);
			}
			else
			{
				this.installOrgCfgDataHandler.ActiveDirectorySplitPermissions = new bool?(false);
			}
			if (this.newOrgName == null)
			{
				MessageBoxHelper.ShowError(Strings.ExchangeOrganizationNameError);
				base.SetWizardButtons(WizardButtons.None);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000463D File Offset: 0x0000283D
		private void ExchangeOrgName_Leave(object sender, EventArgs e)
		{
			this.newOrgName = this.SetOrganizationName();
			if (this.newOrgName == null)
			{
				MessageBoxHelper.ShowError(Strings.ExchangeOrganizationNameError);
				base.SetWizardButtons(WizardButtons.None);
				return;
			}
			this.installOrgCfgDataHandler.OrganizationName = this.newOrgName;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000467C File Offset: 0x0000287C
		private NewOrganizationName SetOrganizationName()
		{
			NewOrganizationName result = null;
			if (!string.IsNullOrEmpty(this.exchangeOrgNameTextBox.Text))
			{
				try
				{
					result = new NewOrganizationName(this.exchangeOrgNameTextBox.Text);
				}
				catch (FormatException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000046C4 File Offset: 0x000028C4
		private void ExchangeOrgName_Enter(object sender, EventArgs e)
		{
			base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000046CD File Offset: 0x000028CD
		private void OnIsADSplitPermissionCheckedChanged(EventArgs e)
		{
			if (this.IsADSplitPermissionCheckedChanged != null)
			{
				this.IsADSplitPermissionCheckedChanged(this, e);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000046E4 File Offset: 0x000028E4
		private void InitializeComponent()
		{
			this.exchangeOrgNameLabel = new Label();
			this.exchangeOrgNameTextBox = new TextBox();
			this.adSplitPermissionCheckBox = new CustomCheckbox();
			this.adSplitPermissionsDescripitionLabel = new Label();
			base.SuspendLayout();
			this.exchangeOrgNameLabel.AutoSize = true;
			this.exchangeOrgNameLabel.BackColor = Color.Transparent;
			this.exchangeOrgNameLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.exchangeOrgNameLabel.Location = new Point(0, 0);
			this.exchangeOrgNameLabel.Margin = new Padding(3);
			this.exchangeOrgNameLabel.MaximumSize = new Size(720, 0);
			this.exchangeOrgNameLabel.Name = "exchangeOrgNameLabel";
			this.exchangeOrgNameLabel.Size = new Size(187, 15);
			this.exchangeOrgNameLabel.TabIndex = 19;
			this.exchangeOrgNameLabel.Text = "[ExchangeOrganizationNameText]";
			this.exchangeOrgNameTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.exchangeOrgNameTextBox.BorderStyle = BorderStyle.FixedSingle;
			this.exchangeOrgNameTextBox.ForeColor = Color.FromArgb(51, 51, 51);
			this.exchangeOrgNameTextBox.Location = new Point(3, 21);
			this.exchangeOrgNameTextBox.Name = "exchangeOrgNameTextBox";
			this.exchangeOrgNameTextBox.Size = new Size(670, 23);
			this.exchangeOrgNameTextBox.TabIndex = 20;
			this.adSplitPermissionCheckBox.BackColor = Color.Transparent;
			this.adSplitPermissionCheckBox.Checked = false;
			this.adSplitPermissionCheckBox.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.adSplitPermissionCheckBox.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.adSplitPermissionCheckBox.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.adSplitPermissionCheckBox.Highligted = false;
			this.adSplitPermissionCheckBox.Location = new Point(3, 73);
			this.adSplitPermissionCheckBox.Name = "adSplitPermissionCheckBox";
			this.adSplitPermissionCheckBox.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.adSplitPermissionCheckBox.Size = new Size(670, 19);
			this.adSplitPermissionCheckBox.TabIndex = 23;
			this.adSplitPermissionCheckBox.Text = "[ADSplitPermissionCheckBox]";
			this.adSplitPermissionCheckBox.TextGap = 10;
			this.adSplitPermissionsDescripitionLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.adSplitPermissionsDescripitionLabel.AutoSize = true;
			this.adSplitPermissionsDescripitionLabel.BackColor = Color.Transparent;
			this.adSplitPermissionsDescripitionLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.adSplitPermissionsDescripitionLabel.Location = new Point(0, 102);
			this.adSplitPermissionsDescripitionLabel.MaximumSize = new Size(720, 0);
			this.adSplitPermissionsDescripitionLabel.Name = "adSplitPermissionsDescripitionLabel";
			this.adSplitPermissionsDescripitionLabel.Size = new Size(208, 15);
			this.adSplitPermissionsDescripitionLabel.TabIndex = 22;
			this.adSplitPermissionsDescripitionLabel.Text = "[ADSplitPermissionsDescripitionLabel]";
			base.Controls.Add(this.adSplitPermissionsDescripitionLabel);
			base.Controls.Add(this.adSplitPermissionCheckBox);
			base.Controls.Add(this.exchangeOrgNameTextBox);
			base.Controls.Add(this.exchangeOrgNameLabel);
			base.Name = "ExchangeOrganizationPage";
			base.SetActive += this.ExchangeOrganizationPage_SetActive;
			base.CheckLoaded += this.ExchangeOrganizationPage_CheckLoaded;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000022 RID: 34
		private Label exchangeOrgNameLabel;

		// Token: 0x04000023 RID: 35
		private TextBox exchangeOrgNameTextBox;

		// Token: 0x04000024 RID: 36
		private CustomCheckbox adSplitPermissionCheckBox;

		// Token: 0x04000025 RID: 37
		private Label adSplitPermissionsDescripitionLabel;

		// Token: 0x04000026 RID: 38
		private InstallOrgCfgDataHandler installOrgCfgDataHandler;

		// Token: 0x04000027 RID: 39
		private IContainer components;

		// Token: 0x04000028 RID: 40
		private NewOrganizationName newOrgName;
	}
}
