using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000010 RID: 16
	internal class RoleSelectionPage : SetupWizardPage
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000082FC File Offset: 0x000064FC
		public RoleSelectionPage(RootDataHandler rootDataHandler)
		{
			this.previousSelection = false;
			this.modeDataHandler = rootDataHandler.ModeDatahandler;
			this.InitializeComponent();
			base.PageTitle = Strings.RoleSelectionPageTitle;
			this.roleSelectionLabel.Text = Strings.RoleSelectionLabelText;
			this.installWindowsPrereqCheckBox.Text = Strings.InstallWindowsPrereqCheckBoxText;
			this.mailboxRoleCheckBox.Text = Strings.MailboxRole;
			this.clientAccessRoleCheckBox.Text = Strings.ClientAccessRole;
			this.managementToolsCheckBox.Text = Strings.AdminToolsRole;
			this.edgeRoleCheckBox.Text = Strings.EdgeRole;
			this.mailboxRoleCheckBox.Checked = false;
			this.clientAccessRoleCheckBox.Checked = false;
			this.edgeRoleCheckBox.Checked = false;
			this.installWindowsPrereqCheckBox.Checked = false;
			this.mailboxRoleCheckBox.CheckedChanged += delegate(object param0, EventArgs param1)
			{
				this.OnIsMailboxCheckedChanged(EventArgs.Empty);
			};
			this.clientAccessRoleCheckBox.CheckedChanged += delegate(object param0, EventArgs param1)
			{
				this.OnIsClientAccessCheckedChanged(EventArgs.Empty);
			};
			this.edgeRoleCheckBox.CheckedChanged += delegate(object param0, EventArgs param1)
			{
				this.OnIsEdgeRoleCheckedChanged(EventArgs.Empty);
			};
			this.managementToolsCheckBox.CheckedChanged += delegate(object param0, EventArgs param1)
			{
				this.OnIsManagementToolsCheckedChanged(EventArgs.Empty);
			};
			this.IsMailboxCheckedChanged += this.MailboxRole_Checked;
			this.IsClientAccessCheckedChanged += this.ClientAccessRole_Checked;
			this.IsEdgeCheckedChanged += this.EdgeRole_Checked;
			this.IsManagementToolsCheckedChanged += this.ManagementTools_Checked;
			this.installWindowsPrereqCheckBox.CheckedChanged += delegate(object param0, EventArgs param1)
			{
				this.OnIsInstallWindowsPrereqCheckedChanged(EventArgs.Empty);
			};
			this.IsInstallWindowsPrereqCheckedChanged += this.InstallWindowsPrereq_Checked;
			base.WizardCancel += this.RoleSelectionPage_WizardCancel;
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000093 RID: 147 RVA: 0x000084EC File Offset: 0x000066EC
		// (remove) Token: 0x06000094 RID: 148 RVA: 0x00008524 File Offset: 0x00006724
		internal event EventHandler IsMailboxCheckedChanged;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000095 RID: 149 RVA: 0x0000855C File Offset: 0x0000675C
		// (remove) Token: 0x06000096 RID: 150 RVA: 0x00008594 File Offset: 0x00006794
		internal event EventHandler IsClientAccessCheckedChanged;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000097 RID: 151 RVA: 0x000085CC File Offset: 0x000067CC
		// (remove) Token: 0x06000098 RID: 152 RVA: 0x00008604 File Offset: 0x00006804
		internal event EventHandler IsEdgeCheckedChanged;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000099 RID: 153 RVA: 0x0000863C File Offset: 0x0000683C
		// (remove) Token: 0x0600009A RID: 154 RVA: 0x00008674 File Offset: 0x00006874
		internal event EventHandler IsManagementToolsCheckedChanged;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600009B RID: 155 RVA: 0x000086AC File Offset: 0x000068AC
		// (remove) Token: 0x0600009C RID: 156 RVA: 0x000086E4 File Offset: 0x000068E4
		internal event EventHandler IsInstallWindowsPrereqCheckedChanged;

		// Token: 0x0600009D RID: 157 RVA: 0x00008719 File Offset: 0x00006919
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00008738 File Offset: 0x00006938
		private void RoleSelectionPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			base.SetVisibleWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			this.managementToolsCheckBox.Checked = true;
			if (this.previousSelection)
			{
				this.GetPreviousSelection();
			}
			else
			{
				this.mailboxRoleCheckBox.Checked = false;
				this.clientAccessRoleCheckBox.Checked = false;
				this.edgeRoleCheckBox.Checked = false;
				if (SetupHelper.IsClientVersionOS())
				{
					base.DisableCustomCheckbox(this.installWindowsPrereqCheckBox, true);
				}
				else
				{
					this.installWindowsPrereqCheckBox.Checked = true;
				}
			}
			this.previousSelection = true;
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000087D4 File Offset: 0x000069D4
		private void InsertProtectionSettingPage()
		{
			this.protectionSettingPage = (ProtectionSettingsPage)base.FindPage("ProtectionSettingsPage");
			if (this.protectionSettingPage == null)
			{
				this.protectionSettingPage = new ProtectionSettingsPage((InstallModeDataHandler)this.modeDataHandler);
				string pageName = "PreCheckPage";
				base.InsertPage(this.protectionSettingPage, base.FindPage(pageName));
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000882E File Offset: 0x00006A2E
		private void RemoveProtectionSettingPage()
		{
			this.protectionSettingPage = (ProtectionSettingsPage)base.FindPage("ProtectionSettingsPage");
			if (this.protectionSettingPage != null)
			{
				base.RemovePage(this.protectionSettingPage);
				this.protectionSettingPage = null;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00008864 File Offset: 0x00006A64
		private void InsertExchangeOrganizationPage()
		{
			this.exchangeOrganizationPage = (ExchangeOrganizationPage)base.FindPage("ExchangeOrganizationPage");
			if (this.exchangeOrganizationPage == null)
			{
				this.exchangeOrganizationPage = new ExchangeOrganizationPage((InstallModeDataHandler)this.modeDataHandler);
				string pageName = "PreCheckPage";
				base.InsertPage(this.exchangeOrganizationPage, base.FindPage(pageName));
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000088BE File Offset: 0x00006ABE
		private void RemoveExchangeOrganizationPage()
		{
			this.exchangeOrganizationPage = (ExchangeOrganizationPage)base.FindPage("ExchangeOrganizationPage");
			if (this.exchangeOrganizationPage != null)
			{
				base.RemovePage(this.exchangeOrganizationPage);
				this.exchangeOrganizationPage = null;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000088F4 File Offset: 0x00006AF4
		private void GetPreviousSelection()
		{
			if (this.modeDataHandler.IsMailboxChecked && this.modeDataHandler.IsBridgeheadChecked && this.modeDataHandler.IsUnifiedMessagingChecked)
			{
				this.mailboxRoleCheckBox.Checked = true;
				this.InsertProtectionSettingPage();
			}
			else
			{
				this.mailboxRoleCheckBox.Checked = false;
				this.RemoveProtectionSettingPage();
			}
			if (this.modeDataHandler.IsCafeChecked && this.modeDataHandler.IsFrontendTransportChecked)
			{
				this.clientAccessRoleCheckBox.Checked = true;
			}
			else
			{
				this.clientAccessRoleCheckBox.Checked = false;
			}
			if (SetupHelper.IsClientVersionOS())
			{
				base.DisableCustomCheckbox(this.installWindowsPrereqCheckBox, true);
				return;
			}
			if (this.modeDataHandler.InstallWindowsComponents)
			{
				this.installWindowsPrereqCheckBox.Checked = true;
				return;
			}
			this.installWindowsPrereqCheckBox.Checked = false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000089BD File Offset: 0x00006BBD
		private void RoleSelectionPage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000089C8 File Offset: 0x00006BC8
		private void RoleSelectionPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.installWindowsPrereqCheckBox.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00008A0E File Offset: 0x00006C0E
		private void OnIsMailboxCheckedChanged(EventArgs e)
		{
			if (this.IsMailboxCheckedChanged != null)
			{
				this.IsMailboxCheckedChanged(this, e);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008A25 File Offset: 0x00006C25
		private void OnIsClientAccessCheckedChanged(EventArgs e)
		{
			if (this.IsClientAccessCheckedChanged != null)
			{
				this.IsClientAccessCheckedChanged(this, e);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00008A3C File Offset: 0x00006C3C
		private void OnIsEdgeRoleCheckedChanged(EventArgs e)
		{
			if (this.IsEdgeCheckedChanged != null)
			{
				this.IsEdgeCheckedChanged(this, e);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008A53 File Offset: 0x00006C53
		private void OnIsManagementToolsCheckedChanged(EventArgs e)
		{
			if (this.IsManagementToolsCheckedChanged != null)
			{
				this.IsManagementToolsCheckedChanged(this, e);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00008A6A File Offset: 0x00006C6A
		private void OnIsInstallWindowsPrereqCheckedChanged(EventArgs e)
		{
			if (this.IsInstallWindowsPrereqCheckedChanged != null)
			{
				this.IsInstallWindowsPrereqCheckedChanged(this, e);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00008A84 File Offset: 0x00006C84
		private void MailboxRole_Checked(object sender, EventArgs e)
		{
			if (this.mailboxRoleCheckBox.Checked)
			{
				this.modeDataHandler.IsMailboxChecked = true;
				this.modeDataHandler.IsClientAccessChecked = true;
				this.modeDataHandler.IsBridgeheadChecked = true;
				this.modeDataHandler.IsUnifiedMessagingChecked = true;
				this.modeDataHandler.IsGatewayChecked = false;
				this.edgeRoleCheckBox.Enabled = false;
				this.InsertProtectionSettingPage();
				return;
			}
			this.modeDataHandler.IsMailboxChecked = false;
			this.modeDataHandler.IsClientAccessChecked = false;
			this.modeDataHandler.IsBridgeheadChecked = false;
			this.modeDataHandler.IsUnifiedMessagingChecked = false;
			if (!this.clientAccessRoleCheckBox.Checked)
			{
				this.edgeRoleCheckBox.Enabled = true;
			}
			this.RemoveProtectionSettingPage();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00008B3C File Offset: 0x00006D3C
		private void ClientAccessRole_Checked(object sender, EventArgs e)
		{
			if (this.clientAccessRoleCheckBox.Checked)
			{
				this.modeDataHandler.IsCafeChecked = true;
				this.modeDataHandler.IsFrontendTransportChecked = true;
				this.modeDataHandler.IsGatewayChecked = false;
				this.edgeRoleCheckBox.Enabled = false;
				return;
			}
			this.modeDataHandler.IsCafeChecked = false;
			this.modeDataHandler.IsFrontendTransportChecked = false;
			if (!this.mailboxRoleCheckBox.Checked)
			{
				this.edgeRoleCheckBox.Enabled = true;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00008BB8 File Offset: 0x00006DB8
		private void EdgeRole_Checked(object sender, EventArgs e)
		{
			if (this.edgeRoleCheckBox.Checked)
			{
				this.modeDataHandler.IsGatewayChecked = true;
				this.modeDataHandler.IsMailboxChecked = false;
				this.modeDataHandler.IsClientAccessChecked = false;
				this.modeDataHandler.IsBridgeheadChecked = false;
				this.modeDataHandler.IsUnifiedMessagingChecked = false;
				this.modeDataHandler.IsCafeChecked = false;
				this.modeDataHandler.IsFrontendTransportChecked = false;
				this.mailboxRoleCheckBox.Enabled = false;
				this.clientAccessRoleCheckBox.Enabled = false;
				this.RemoveExchangeOrganizationPage();
				return;
			}
			this.modeDataHandler.IsGatewayChecked = false;
			this.mailboxRoleCheckBox.Enabled = true;
			this.clientAccessRoleCheckBox.Enabled = true;
			this.InsertExchangeOrganizationPage();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00008C6F File Offset: 0x00006E6F
		private void ManagementTools_Checked(object sender, EventArgs e)
		{
			this.managementToolsCheckBox.Enabled = false;
			this.modeDataHandler.IsAdminToolsChecked = true;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00008C89 File Offset: 0x00006E89
		private void InstallWindowsPrereq_Checked(object sender, EventArgs e)
		{
			if (this.installWindowsPrereqCheckBox.Checked)
			{
				this.modeDataHandler.InstallWindowsComponents = true;
				return;
			}
			this.modeDataHandler.InstallWindowsComponents = false;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00008CB4 File Offset: 0x00006EB4
		private void InitializeComponent()
		{
			this.roleSelectionLabel = new Label();
			this.mailboxRoleCheckBox = new CustomCheckbox();
			this.clientAccessRoleCheckBox = new CustomCheckbox();
			this.managementToolsCheckBox = new CustomCheckbox();
			this.installWindowsPrereqCheckBox = new CustomCheckbox();
			this.edgeRoleCheckBox = new CustomCheckbox();
			base.SuspendLayout();
			this.roleSelectionLabel.AutoSize = true;
			this.roleSelectionLabel.BackColor = Color.Transparent;
			this.roleSelectionLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.roleSelectionLabel.Location = new Point(0, 0);
			this.roleSelectionLabel.MaximumSize = new Size(740, 0);
			this.roleSelectionLabel.Name = "roleSelectionLabel";
			this.roleSelectionLabel.Size = new Size(108, 15);
			this.roleSelectionLabel.TabIndex = 18;
			this.roleSelectionLabel.Text = "[RoleSelectionText]";
			this.mailboxRoleCheckBox.BackColor = Color.Transparent;
			this.mailboxRoleCheckBox.Checked = false;
			this.mailboxRoleCheckBox.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.mailboxRoleCheckBox.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.mailboxRoleCheckBox.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.mailboxRoleCheckBox.Highligted = false;
			this.mailboxRoleCheckBox.Location = new Point(0, 25);
			this.mailboxRoleCheckBox.Name = "mailboxRoleCheckBox";
			this.mailboxRoleCheckBox.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.mailboxRoleCheckBox.Size = new Size(700, 19);
			this.mailboxRoleCheckBox.TabIndex = 23;
			this.mailboxRoleCheckBox.Text = "[MailboxRoleCheckBoxText]";
			this.mailboxRoleCheckBox.TextGap = 10;
			this.clientAccessRoleCheckBox.BackColor = Color.Transparent;
			this.clientAccessRoleCheckBox.Checked = false;
			this.clientAccessRoleCheckBox.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.clientAccessRoleCheckBox.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.clientAccessRoleCheckBox.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.clientAccessRoleCheckBox.Highligted = false;
			this.clientAccessRoleCheckBox.Location = new Point(0, 54);
			this.clientAccessRoleCheckBox.Name = "clientAccessRoleCheckBox";
			this.clientAccessRoleCheckBox.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.clientAccessRoleCheckBox.Size = new Size(700, 19);
			this.clientAccessRoleCheckBox.TabIndex = 24;
			this.clientAccessRoleCheckBox.Text = "[ClientAccessRoleCheckBoxText]";
			this.clientAccessRoleCheckBox.TextGap = 10;
			this.managementToolsCheckBox.BackColor = Color.Transparent;
			this.managementToolsCheckBox.Checked = false;
			this.managementToolsCheckBox.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.managementToolsCheckBox.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.managementToolsCheckBox.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.managementToolsCheckBox.Highligted = false;
			this.managementToolsCheckBox.Location = new Point(0, 83);
			this.managementToolsCheckBox.Name = "managementToolsCheckBox";
			this.managementToolsCheckBox.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.managementToolsCheckBox.Size = new Size(700, 19);
			this.managementToolsCheckBox.TabIndex = 25;
			this.managementToolsCheckBox.Text = "[ManagementToolsCheckBoxText]";
			this.managementToolsCheckBox.TextGap = 10;
			this.installWindowsPrereqCheckBox.BackColor = Color.Transparent;
			this.installWindowsPrereqCheckBox.Checked = false;
			this.installWindowsPrereqCheckBox.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.installWindowsPrereqCheckBox.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.installWindowsPrereqCheckBox.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.installWindowsPrereqCheckBox.Highligted = false;
			this.installWindowsPrereqCheckBox.Location = new Point(0, 146);
			this.installWindowsPrereqCheckBox.Name = "installWindowsPrereqCheckBox";
			this.installWindowsPrereqCheckBox.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.installWindowsPrereqCheckBox.Size = new Size(700, 19);
			this.installWindowsPrereqCheckBox.TabIndex = 26;
			this.installWindowsPrereqCheckBox.Text = "[installWindowsPrereqText]";
			this.installWindowsPrereqCheckBox.TextGap = 10;
			this.edgeRoleCheckBox.BackColor = Color.Transparent;
			this.edgeRoleCheckBox.Checked = false;
			this.edgeRoleCheckBox.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.edgeRoleCheckBox.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.edgeRoleCheckBox.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.edgeRoleCheckBox.Highligted = false;
			this.edgeRoleCheckBox.Location = new Point(0, 112);
			this.edgeRoleCheckBox.Name = "edgeRoleCheckBox";
			this.edgeRoleCheckBox.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.edgeRoleCheckBox.Size = new Size(700, 19);
			this.edgeRoleCheckBox.TabIndex = 27;
			this.edgeRoleCheckBox.Text = "[EdgeRoleCheckBoxText]";
			this.edgeRoleCheckBox.TextGap = 10;
			base.Controls.Add(this.edgeRoleCheckBox);
			base.Controls.Add(this.installWindowsPrereqCheckBox);
			base.Controls.Add(this.managementToolsCheckBox);
			base.Controls.Add(this.clientAccessRoleCheckBox);
			base.Controls.Add(this.mailboxRoleCheckBox);
			base.Controls.Add(this.roleSelectionLabel);
			base.Name = "RoleSelectionPage";
			base.SetActive += this.RoleSelectionPage_SetActive;
			base.CheckLoaded += this.RoleSelectionPage_CheckLoaded;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400006D RID: 109
		private readonly ModeDataHandler modeDataHandler;

		// Token: 0x0400006E RID: 110
		private IContainer components;

		// Token: 0x0400006F RID: 111
		private CustomCheckbox clientAccessRoleCheckBox;

		// Token: 0x04000070 RID: 112
		private CustomCheckbox mailboxRoleCheckBox;

		// Token: 0x04000071 RID: 113
		private CustomCheckbox edgeRoleCheckBox;

		// Token: 0x04000072 RID: 114
		private CustomCheckbox managementToolsCheckBox;

		// Token: 0x04000073 RID: 115
		private CustomCheckbox installWindowsPrereqCheckBox;

		// Token: 0x04000074 RID: 116
		private Label roleSelectionLabel;

		// Token: 0x04000075 RID: 117
		private bool previousSelection;

		// Token: 0x04000076 RID: 118
		private ProtectionSettingsPage protectionSettingPage;

		// Token: 0x04000077 RID: 119
		private ExchangeOrganizationPage exchangeOrganizationPage;
	}
}
