using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000002 RID: 2
	internal class AddRemoveServerRolePage : SetupWizardPage
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002114 File Offset: 0x00000314
		public AddRemoveServerRolePage(RootDataHandler rootDataHandler)
		{
			this.previousSelection = false;
			this.modeDataHandler = rootDataHandler.ModeDatahandler;
			this.InitializeComponent();
			base.PageTitle = ((rootDataHandler.Mode == InstallationModes.Install) ? Strings.AddRemoveServerRolePageTitle : Strings.UninstallPageTitle);
			this.roleSelectionLabel.Text = string.Empty;
			this.installWindowsPrereqCheckBox.Text = Strings.InstallWindowsPrereqCheckBoxText;
			this.mailboxRoleCheckBox.Text = Strings.MailboxRole;
			this.clientAccessRoleCheckBox.Text = Strings.ClientAccessRole;
			this.edgeRoleCheckBox.Text = Strings.EdgeRole;
			this.managementToolsCheckBox.Text = Strings.AdminToolsRole;
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
			this.installWindowsPrereqCheckBox.CheckedChanged += delegate(object param0, EventArgs param1)
			{
				this.OnIsInstallWindowsPrereqCheckedChanged(EventArgs.Empty);
			};
			this.IsInstallWindowsPrereqCheckedChanged += this.InstallWindowsPrereq_Checked;
			base.WizardCancel += this.AddRemoveServerRolePage_WizardCancel;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000002 RID: 2 RVA: 0x000022C8 File Offset: 0x000004C8
		// (remove) Token: 0x06000003 RID: 3 RVA: 0x00002300 File Offset: 0x00000500
		internal event EventHandler IsMailboxCheckedChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000004 RID: 4 RVA: 0x00002338 File Offset: 0x00000538
		// (remove) Token: 0x06000005 RID: 5 RVA: 0x00002370 File Offset: 0x00000570
		internal event EventHandler IsClientAccessCheckedChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000006 RID: 6 RVA: 0x000023A8 File Offset: 0x000005A8
		// (remove) Token: 0x06000007 RID: 7 RVA: 0x000023E0 File Offset: 0x000005E0
		internal event EventHandler IsEdgeCheckedChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000008 RID: 8 RVA: 0x00002418 File Offset: 0x00000618
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x00002450 File Offset: 0x00000650
		internal event EventHandler IsManagementToolsCheckedChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600000A RID: 10 RVA: 0x00002488 File Offset: 0x00000688
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x000024C0 File Offset: 0x000006C0
		internal event EventHandler IsInstallWindowsPrereqCheckedChanged;

		// Token: 0x0600000C RID: 12 RVA: 0x000024F5 File Offset: 0x000006F5
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002514 File Offset: 0x00000714
		private void AddRemoveServerRolePage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			this.roleSelectionLabel.Text = this.modeDataHandler.RoleSelectionDescription;
			base.SetVisibleWizardButtons(WizardButtons.Next);
			this.managementToolsCheckBox.Checked = true;
			if (this.previousSelection)
			{
				this.GetPreviousSelection();
			}
			else
			{
				this.GetInstalledRoles();
				if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
				{
					this.modeDataHandler.IsAdminToolsChecked = true;
					if (this.isMailBoxRoleEnabled)
					{
						this.modeDataHandler.IsMailboxChecked = true;
						this.modeDataHandler.IsClientAccessChecked = true;
						this.modeDataHandler.IsBridgeheadChecked = true;
						this.modeDataHandler.IsUnifiedMessagingChecked = true;
					}
					if (this.isClientAccessRoleEnabled)
					{
						this.modeDataHandler.IsCafeChecked = true;
						this.modeDataHandler.IsFrontendTransportChecked = true;
					}
				}
				this.mailboxRoleCheckBox.Checked = false;
				this.mailboxRoleCheckBox.Enabled = true;
				if (this.isMailBoxRoleEnabled)
				{
					this.mailboxRoleCheckBox.Checked = true;
					this.mailboxRoleCheckBox.Text = Strings.MailboxRole + " " + Strings.AlreadyInstalled;
					if (this.modeDataHandler.Mode == InstallationModes.Install)
					{
						this.mailboxRoleCheckBox.Enabled = false;
						this.modeDataHandler.IsMailboxChecked = false;
						this.modeDataHandler.IsClientAccessChecked = false;
						this.modeDataHandler.IsBridgeheadChecked = false;
						this.modeDataHandler.IsUnifiedMessagingChecked = false;
					}
				}
				else if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
				{
					this.mailboxRoleCheckBox.Enabled = false;
				}
				this.clientAccessRoleCheckBox.Checked = false;
				this.clientAccessRoleCheckBox.Enabled = true;
				if (this.isClientAccessRoleEnabled)
				{
					this.clientAccessRoleCheckBox.Checked = true;
					this.clientAccessRoleCheckBox.Text = Strings.ClientAccessRole + " " + Strings.AlreadyInstalled;
					if (this.modeDataHandler.Mode == InstallationModes.Install)
					{
						this.clientAccessRoleCheckBox.Enabled = false;
						this.modeDataHandler.IsCafeChecked = false;
						this.modeDataHandler.IsFrontendTransportChecked = false;
					}
				}
				else if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
				{
					this.clientAccessRoleCheckBox.Enabled = false;
				}
				if (!this.isClientAccessRoleEnabled && !this.isMailBoxRoleEnabled && this.modeDataHandler.Mode == InstallationModes.Uninstall)
				{
					this.managementToolsCheckBox.Enabled = true;
				}
				else
				{
					this.managementToolsCheckBox.Enabled = false;
				}
				this.edgeRoleCheckBox.Checked = false;
				this.edgeRoleCheckBox.Enabled = (this.modeDataHandler.Mode != InstallationModes.Install);
				if (this.isEdgeRoleEnabled)
				{
					this.edgeRoleCheckBox.Checked = true;
					this.edgeRoleCheckBox.Text = Strings.EdgeRole + " " + Strings.AlreadyInstalled;
					if (this.modeDataHandler.Mode == InstallationModes.Install)
					{
						this.modeDataHandler.IsGatewayChecked = false;
						this.mailboxRoleCheckBox.Enabled = false;
						this.clientAccessRoleCheckBox.Enabled = false;
					}
				}
				if (this.isManagementToolEnabled)
				{
					this.managementToolsCheckBox.Text = Strings.AdminToolsRole + " " + Strings.AlreadyInstalled;
					this.managementToolsCheckBox.Checked = true;
				}
				if (this.modeDataHandler.Mode == InstallationModes.Install)
				{
					if (SetupHelper.IsClientVersionOS())
					{
						base.DisableCustomCheckbox(this.installWindowsPrereqCheckBox, true);
					}
					else
					{
						this.installWindowsPrereqCheckBox.Enabled = true;
						this.installWindowsPrereqCheckBox.Visible = true;
						this.installWindowsPrereqCheckBox.Checked = true;
					}
				}
				else
				{
					this.installWindowsPrereqCheckBox.Enabled = false;
					this.installWindowsPrereqCheckBox.Visible = false;
				}
				base.SetWizardButtons(WizardButtons.None);
			}
			this.IsMailboxCheckedChanged += this.MailboxRole_Checked;
			this.IsClientAccessCheckedChanged += this.ClientAccessRole_Checked;
			this.IsEdgeCheckedChanged += this.EdgeRole_Checked;
			this.IsManagementToolsCheckedChanged += this.ManagementTools_Checked;
			this.previousSelection = true;
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002914 File Offset: 0x00000B14
		private void GetInstalledRoles()
		{
			this.isEdgeRoleEnabled = this.modeDataHandler.IsGatewayChecked;
			this.isMailBoxRoleEnabled = (this.modeDataHandler.IsMailboxChecked && this.modeDataHandler.IsBridgeheadChecked && this.modeDataHandler.IsUnifiedMessagingChecked);
			if (this.modeDataHandler.Mode == InstallationModes.Uninstall && !this.isMailBoxRoleEnabled)
			{
				this.isMailBoxRoleEnabled = ((this.modeDataHandler.IsMailboxChecked && !this.modeDataHandler.IsMailboxChecked) || (this.modeDataHandler.IsBridgeheadEnabled && !this.modeDataHandler.IsBridgeheadChecked) || (this.modeDataHandler.IsUnifiedMessagingEnabled && !this.modeDataHandler.IsUnifiedMessagingChecked));
			}
			this.isClientAccessRoleEnabled = (this.modeDataHandler.IsCafeChecked && this.modeDataHandler.IsFrontendTransportChecked);
			if (this.modeDataHandler.Mode == InstallationModes.Uninstall && !this.isClientAccessRoleEnabled)
			{
				this.isClientAccessRoleEnabled = ((this.modeDataHandler.IsCafeEnabled && !this.modeDataHandler.IsCafeChecked) || (this.modeDataHandler.IsFrontendTransportEnabled && !this.modeDataHandler.IsFrontendTransportChecked));
			}
			this.isManagementToolEnabled = this.modeDataHandler.IsAdminToolsChecked;
			if (this.modeDataHandler.Mode == InstallationModes.Uninstall && !this.isManagementToolEnabled)
			{
				this.isManagementToolEnabled = (this.modeDataHandler.IsAdminToolsEnabled && !this.modeDataHandler.IsAdminToolsChecked);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002A94 File Offset: 0x00000C94
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

		// Token: 0x06000010 RID: 16 RVA: 0x00002AEE File Offset: 0x00000CEE
		private void RemoveProtectionSettingPage()
		{
			this.protectionSettingPage = (ProtectionSettingsPage)base.FindPage("ProtectionSettingsPage");
			if (this.protectionSettingPage != null)
			{
				base.RemovePage(this.protectionSettingPage);
				this.protectionSettingPage = null;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002B24 File Offset: 0x00000D24
		private void GetPreviousSelection()
		{
			if (this.modeDataHandler.IsMailboxChecked && this.modeDataHandler.IsBridgeheadChecked && this.modeDataHandler.IsUnifiedMessagingChecked)
			{
				this.mailboxRoleCheckBox.Checked = true;
				if (this.modeDataHandler.Mode == InstallationModes.Install)
				{
					this.InsertProtectionSettingPage();
					base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
					base.SetVisibleWizardButtons(WizardButtons.Next);
				}
			}
			else if (this.isMailBoxRoleEnabled)
			{
				base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
				base.SetVisibleWizardButtons(WizardButtons.Next);
			}
			else
			{
				this.mailboxRoleCheckBox.Checked = false;
				if (this.modeDataHandler.Mode == InstallationModes.Install)
				{
					this.RemoveProtectionSettingPage();
				}
			}
			if (this.modeDataHandler.IsCafeChecked && this.modeDataHandler.IsFrontendTransportChecked)
			{
				this.clientAccessRoleCheckBox.Checked = true;
				if (this.modeDataHandler.Mode == InstallationModes.Install)
				{
					base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
					base.SetVisibleWizardButtons(WizardButtons.Next);
				}
			}
			else if (this.isClientAccessRoleEnabled)
			{
				base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
				base.SetVisibleWizardButtons(WizardButtons.Next);
			}
			else
			{
				this.clientAccessRoleCheckBox.Checked = false;
			}
			if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
			{
				this.installWindowsPrereqCheckBox.Enabled = false;
				this.installWindowsPrereqCheckBox.Visible = false;
				if (this.modeDataHandler.IsAdminToolsChecked)
				{
					this.managementToolsCheckBox.Checked = true;
					return;
				}
				this.managementToolsCheckBox.Checked = false;
				base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
				base.SetVisibleWizardButtons(WizardButtons.Next);
				return;
			}
			else
			{
				if (SetupHelper.IsClientVersionOS())
				{
					base.DisableCustomCheckbox(this.installWindowsPrereqCheckBox, true);
					return;
				}
				this.installWindowsPrereqCheckBox.Visible = true;
				this.installWindowsPrereqCheckBox.Enabled = true;
				if (this.modeDataHandler.InstallWindowsComponents)
				{
					this.installWindowsPrereqCheckBox.Checked = true;
					return;
				}
				this.installWindowsPrereqCheckBox.Checked = false;
				return;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002CD6 File Offset: 0x00000ED6
		private void AddRemoveServerRolePage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002CE0 File Offset: 0x00000EE0
		private void AddRemoveServerRolePage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.installWindowsPrereqCheckBox.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002D26 File Offset: 0x00000F26
		private void OnIsMailboxCheckedChanged(EventArgs e)
		{
			if (this.IsMailboxCheckedChanged != null)
			{
				this.IsMailboxCheckedChanged(this, e);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002D3D File Offset: 0x00000F3D
		private void OnIsClientAccessCheckedChanged(EventArgs e)
		{
			if (this.IsClientAccessCheckedChanged != null)
			{
				this.IsClientAccessCheckedChanged(this, e);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002D54 File Offset: 0x00000F54
		private void OnIsEdgeRoleCheckedChanged(EventArgs e)
		{
			if (this.IsEdgeCheckedChanged != null)
			{
				this.IsEdgeCheckedChanged(this, e);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002D6B File Offset: 0x00000F6B
		private void OnIsManagementToolsCheckedChanged(EventArgs e)
		{
			if (this.IsManagementToolsCheckedChanged != null)
			{
				this.IsManagementToolsCheckedChanged(this, e);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002D82 File Offset: 0x00000F82
		private void OnIsInstallWindowsPrereqCheckedChanged(EventArgs e)
		{
			if (this.IsInstallWindowsPrereqCheckedChanged != null)
			{
				this.IsInstallWindowsPrereqCheckedChanged(this, e);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002D9C File Offset: 0x00000F9C
		private void MailboxRole_Checked(object sender, EventArgs e)
		{
			if (this.mailboxRoleCheckBox.Checked)
			{
				if (this.modeDataHandler.Mode == InstallationModes.Install && !this.isMailBoxRoleEnabled)
				{
					this.modeDataHandler.IsMailboxChecked = true;
					this.modeDataHandler.IsClientAccessChecked = true;
					this.modeDataHandler.IsBridgeheadChecked = true;
					this.modeDataHandler.IsUnifiedMessagingChecked = true;
					this.modeDataHandler.IsGatewayChecked = false;
					this.edgeRoleCheckBox.Enabled = false;
					this.InsertProtectionSettingPage();
					base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
					base.SetVisibleWizardButtons(WizardButtons.Next);
				}
				if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
				{
					this.managementToolsCheckBox.Enabled = false;
					this.managementToolsCheckBox.Checked = true;
					this.modeDataHandler.IsMailboxChecked = true;
					this.modeDataHandler.IsClientAccessChecked = true;
					this.modeDataHandler.IsBridgeheadChecked = true;
					this.modeDataHandler.IsUnifiedMessagingChecked = true;
					this.RemoveProtectionSettingPage();
					if ((this.isClientAccessRoleEnabled && this.clientAccessRoleCheckBox.Checked) || !this.isClientAccessRoleEnabled)
					{
						base.SetWizardButtons(WizardButtons.None);
						return;
					}
				}
			}
			else
			{
				if (this.modeDataHandler.Mode == InstallationModes.Install)
				{
					this.modeDataHandler.IsMailboxChecked = false;
					this.modeDataHandler.IsClientAccessChecked = false;
					this.modeDataHandler.IsBridgeheadChecked = false;
					this.modeDataHandler.IsUnifiedMessagingChecked = false;
					this.RemoveProtectionSettingPage();
					if ((!this.isClientAccessRoleEnabled && !this.clientAccessRoleCheckBox.Checked) || this.isClientAccessRoleEnabled)
					{
						base.SetWizardButtons(WizardButtons.None);
						this.edgeRoleCheckBox.Enabled = true;
					}
				}
				if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
				{
					if ((this.isClientAccessRoleEnabled && !this.clientAccessRoleCheckBox.Checked) || !this.isClientAccessRoleEnabled)
					{
						this.managementToolsCheckBox.Enabled = true;
					}
					this.modeDataHandler.IsMailboxChecked = false;
					this.modeDataHandler.IsClientAccessChecked = false;
					this.modeDataHandler.IsBridgeheadChecked = false;
					this.modeDataHandler.IsUnifiedMessagingChecked = false;
					this.RemoveProtectionSettingPage();
					base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
					base.SetVisibleWizardButtons(WizardButtons.Next);
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002FA4 File Offset: 0x000011A4
		private void ClientAccessRole_Checked(object sender, EventArgs e)
		{
			if (this.clientAccessRoleCheckBox.Checked)
			{
				if (this.modeDataHandler.Mode == InstallationModes.Install && !this.isClientAccessRoleEnabled)
				{
					this.modeDataHandler.IsCafeChecked = true;
					this.modeDataHandler.IsFrontendTransportChecked = true;
					base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
					base.SetVisibleWizardButtons(WizardButtons.Next);
					this.modeDataHandler.IsGatewayChecked = false;
					this.edgeRoleCheckBox.Enabled = false;
				}
				if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
				{
					this.managementToolsCheckBox.Enabled = false;
					this.managementToolsCheckBox.Checked = true;
					this.modeDataHandler.IsCafeChecked = true;
					this.modeDataHandler.IsFrontendTransportChecked = true;
					if ((this.isMailBoxRoleEnabled && this.mailboxRoleCheckBox.Checked) || !this.isMailBoxRoleEnabled)
					{
						base.SetWizardButtons(WizardButtons.None);
						return;
					}
				}
			}
			else
			{
				if (this.modeDataHandler.Mode == InstallationModes.Install)
				{
					this.modeDataHandler.IsCafeChecked = false;
					this.modeDataHandler.IsFrontendTransportChecked = false;
					if ((!this.isMailBoxRoleEnabled && !this.mailboxRoleCheckBox.Checked) || this.isMailBoxRoleEnabled)
					{
						base.SetWizardButtons(WizardButtons.None);
						this.edgeRoleCheckBox.Enabled = true;
					}
				}
				if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
				{
					if ((this.isMailBoxRoleEnabled && !this.mailboxRoleCheckBox.Checked) || !this.isMailBoxRoleEnabled)
					{
						this.managementToolsCheckBox.Enabled = true;
					}
					this.modeDataHandler.IsCafeChecked = false;
					this.modeDataHandler.IsFrontendTransportChecked = false;
					base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
					base.SetVisibleWizardButtons(WizardButtons.Next);
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003134 File Offset: 0x00001334
		private void EdgeRole_Checked(object sender, EventArgs e)
		{
			if (this.edgeRoleCheckBox.Checked)
			{
				if (this.modeDataHandler.Mode == InstallationModes.Install && !this.isEdgeRoleEnabled)
				{
					this.modeDataHandler.IsGatewayChecked = true;
					this.modeDataHandler.IsMailboxChecked = false;
					this.modeDataHandler.IsClientAccessChecked = false;
					this.modeDataHandler.IsBridgeheadChecked = false;
					this.modeDataHandler.IsUnifiedMessagingChecked = false;
					this.modeDataHandler.IsCafeChecked = false;
					this.modeDataHandler.IsFrontendTransportChecked = false;
					base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
					base.SetVisibleWizardButtons(WizardButtons.Next);
					this.mailboxRoleCheckBox.Enabled = false;
					this.clientAccessRoleCheckBox.Enabled = false;
				}
				if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
				{
					this.managementToolsCheckBox.Enabled = false;
					this.managementToolsCheckBox.Checked = true;
					this.modeDataHandler.IsGatewayChecked = true;
					this.modeDataHandler.IsMailboxChecked = false;
					this.modeDataHandler.IsClientAccessChecked = false;
					this.modeDataHandler.IsBridgeheadChecked = false;
					this.modeDataHandler.IsUnifiedMessagingChecked = false;
					this.modeDataHandler.IsCafeChecked = false;
					this.modeDataHandler.IsFrontendTransportChecked = false;
					return;
				}
			}
			else
			{
				if (this.modeDataHandler.Mode == InstallationModes.Install)
				{
					this.modeDataHandler.IsGatewayChecked = false;
					this.mailboxRoleCheckBox.Enabled = true;
					this.clientAccessRoleCheckBox.Enabled = true;
				}
				if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
				{
					base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
					base.SetVisibleWizardButtons(WizardButtons.Next);
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000032B0 File Offset: 0x000014B0
		private void ManagementTools_Checked(object sender, EventArgs e)
		{
			if (this.modeDataHandler.Mode == InstallationModes.Install && this.isManagementToolEnabled)
			{
				this.managementToolsCheckBox.Enabled = false;
			}
			if (this.modeDataHandler.Mode == InstallationModes.Uninstall)
			{
				if (this.managementToolsCheckBox.Checked)
				{
					this.modeDataHandler.IsAdminToolsChecked = true;
					if (!this.isMailBoxRoleEnabled && !this.isClientAccessRoleEnabled)
					{
						base.SetWizardButtons(WizardButtons.None);
						return;
					}
				}
				else if (!this.mailboxRoleCheckBox.Checked && !this.clientAccessRoleCheckBox.Checked)
				{
					this.modeDataHandler.IsAdminToolsChecked = false;
					if (!this.isMailBoxRoleEnabled && !this.isClientAccessRoleEnabled)
					{
						base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
						base.SetVisibleWizardButtons(WizardButtons.Next);
					}
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003362 File Offset: 0x00001562
		private void InstallWindowsPrereq_Checked(object sender, EventArgs e)
		{
			if (this.installWindowsPrereqCheckBox.Checked)
			{
				this.modeDataHandler.InstallWindowsComponents = true;
				return;
			}
			this.modeDataHandler.InstallWindowsComponents = false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000338C File Offset: 0x0000158C
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
			base.Name = "AddRemoveServerRolePage";
			base.SetActive += this.AddRemoveServerRolePage_SetActive;
			base.CheckLoaded += this.AddRemoveServerRolePage_CheckLoaded;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000001 RID: 1
		private readonly ModeDataHandler modeDataHandler;

		// Token: 0x04000002 RID: 2
		private IContainer components;

		// Token: 0x04000003 RID: 3
		private CustomCheckbox clientAccessRoleCheckBox;

		// Token: 0x04000004 RID: 4
		private CustomCheckbox mailboxRoleCheckBox;

		// Token: 0x04000005 RID: 5
		private CustomCheckbox edgeRoleCheckBox;

		// Token: 0x04000006 RID: 6
		private CustomCheckbox managementToolsCheckBox;

		// Token: 0x04000007 RID: 7
		private CustomCheckbox installWindowsPrereqCheckBox;

		// Token: 0x04000008 RID: 8
		private Label roleSelectionLabel;

		// Token: 0x04000009 RID: 9
		private bool previousSelection;

		// Token: 0x0400000A RID: 10
		private bool isMailBoxRoleEnabled;

		// Token: 0x0400000B RID: 11
		private bool isClientAccessRoleEnabled;

		// Token: 0x0400000C RID: 12
		private bool isEdgeRoleEnabled;

		// Token: 0x0400000D RID: 13
		private bool isManagementToolEnabled;

		// Token: 0x0400000E RID: 14
		private ProtectionSettingsPage protectionSettingPage;
	}
}
