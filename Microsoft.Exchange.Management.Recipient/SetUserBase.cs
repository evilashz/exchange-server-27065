using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200000D RID: 13
	public abstract class SetUserBase<TIdentity, TPublicObject> : SetMailEnabledOrgPersonObjectTask<TIdentity, TPublicObject, ADUser> where TIdentity : IIdentityParameter, new() where TPublicObject : MailEnabledOrgPerson, new()
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000046F0 File Offset: 0x000028F0
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000046F8 File Offset: 0x000028F8
		protected bool IsSetRandomPassword
		{
			get
			{
				return this.isSetRandomPassword;
			}
			set
			{
				this.isSetRandomPassword = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004701 File Offset: 0x00002901
		protected bool IsChangingOnPassword
		{
			get
			{
				return this.Password != null && this.Password.Length > 0;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000471C File Offset: 0x0000291C
		protected bool HasSetPasswordPermission
		{
			get
			{
				return (base.CurrentTaskContext.InvocationInfo.IsCmdletInvokedWithoutPSFramework && this.DataObject.RecipientTypeDetails == RecipientTypeDetails.MonitoringMailbox) || (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.SetPasswordWithoutOldPassword.Enabled && base.ExchangeRunspaceConfig != null && base.ExchangeRunspaceConfig.ExecutingUserHasResetPasswordPermission);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000478B File Offset: 0x0000298B
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000047AC File Offset: 0x000029AC
		[Parameter(Mandatory = false)]
		public Capability SKUCapability
		{
			get
			{
				return (Capability)(base.Fields["SKUCapability"] ?? Capability.None);
			}
			set
			{
				base.VerifyValues<Capability>(CapabilityHelper.AllowedSKUCapabilities, value);
				base.Fields["SKUCapability"] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000047D0 File Offset: 0x000029D0
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000047F0 File Offset: 0x000029F0
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return (MultiValuedProperty<Capability>)(base.Fields["AddOnSKUCapability"] ?? new MultiValuedProperty<Capability>());
			}
			set
			{
				if (value != null)
				{
					base.VerifyValues<Capability>(CapabilityHelper.AllowedSKUCapabilities, value.ToArray());
				}
				base.Fields["AddOnSKUCapability"] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004817 File Offset: 0x00002A17
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000483D File Offset: 0x00002A3D
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassLiveId
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassLiveId"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BypassLiveId"] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004855 File Offset: 0x00002A55
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000486C File Offset: 0x00002A6C
		[Parameter(Mandatory = false)]
		public NetID NetID
		{
			get
			{
				return (NetID)base.Fields["NetID"];
			}
			set
			{
				base.Fields["NetID"] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000487F File Offset: 0x00002A7F
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00004896 File Offset: 0x00002A96
		[Parameter(Mandatory = false)]
		public SecureString Password
		{
			get
			{
				return (SecureString)base.Fields["Password"];
			}
			set
			{
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000048A9 File Offset: 0x00002AA9
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000048C0 File Offset: 0x00002AC0
		[Parameter(Mandatory = false)]
		public string FederatedIdentity
		{
			get
			{
				return (string)base.Fields["FederatedIdentity"];
			}
			set
			{
				base.Fields["FederatedIdentity"] = value;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000048D4 File Offset: 0x00002AD4
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.NetID != null && !this.BypassLiveId)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorNetIDWithoutBypassWLIDInSet), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004924 File Offset: 0x00002B24
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (ADUser)base.PrepareDataObject();
			if (this.DataObject.IsChanged(MailboxSchema.WindowsLiveID) && this.DataObject.WindowsLiveID != SmtpAddress.Empty)
			{
				if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.WindowsLiveID.Enabled)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorEnableWindowsLiveIdForEnterpriseMailbox), ExchangeErrorCategory.Client, this.DataObject.Identity);
				}
				if (this.ShouldCheckAcceptedDomains())
				{
					RecipientTaskHelper.ValidateInAcceptedDomain(this.ConfigurationSession, this.DataObject.OrganizationId, this.DataObject.WindowsLiveID.Domain, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
				}
			}
			if (this.DataObject.WindowsLiveID != SmtpAddress.Empty && !this.DataObject.WindowsLiveID.Equals(this.DataObject.UserPrincipalName))
			{
				this.WriteWarning(Strings.WarningChangingUserPrincipalName(this.DataObject.UserPrincipalName, this.DataObject.WindowsLiveID.ToString()));
				this.DataObject.UserPrincipalName = this.DataObject.WindowsLiveID.ToString();
			}
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004A98 File Offset: 0x00002C98
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			this.IsSetRandomPassword = false;
			if (this.DataObject.IsChanged(MailboxSchema.WindowsLiveID))
			{
				this.IsSetRandomPassword = true;
				if (this.DataObject.IsChanged(MailboxSchema.NetID))
				{
					MailboxTaskHelper.IsLiveIdExists((IRecipientSession)base.DataSession, this.DataObject.WindowsLiveID, this.DataObject.NetID, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (base.Fields.IsModified("SKUCapability"))
			{
				this.DataObject.SKUCapability = new Capability?(this.SKUCapability);
			}
			if (base.Fields.IsModified("AddOnSKUCapability"))
			{
				CapabilityHelper.SetAddOnSKUCapabilities(this.AddOnSKUCapability, this.DataObject.PersistedCapabilities);
				RecipientTaskHelper.UpgradeArchiveQuotaOnArchiveAddOnSKU(this.DataObject, this.DataObject.PersistedCapabilities);
			}
			if (this.IsChangingOnPassword && this.HasSetPasswordPermission)
			{
				((IRecipientSession)base.DataSession).SetPassword(this.DataObject, this.Password);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004BBC File Offset: 0x00002DBC
		protected override bool IsObjectStateChanged()
		{
			return base.IsObjectStateChanged() || this.IsChangingOnPassword;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004BD0 File Offset: 0x00002DD0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.FederatedIdentity != null && this.Password != null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorFederatedIdentityandPasswordTogether), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
			if (this.DataObject.IsChanged(MailUserSchema.WindowsLiveID))
			{
				MailboxTaskHelper.IsMemberExists((IRecipientSession)base.DataSession, this.DataObject.WindowsLiveID, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (this.DataObject.IsModified(MailUserSchema.UserPrincipalName))
			{
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.ValidateExternalEmailAddressInAcceptedDomain.Enabled && this.ShouldCheckAcceptedDomains())
				{
					RecipientTaskHelper.ValidateInAcceptedDomain(this.ConfigurationSession, this.DataObject.OrganizationId, RecipientTaskHelper.GetDomainPartOfUserPrincalName(this.DataObject.UserPrincipalName), new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
				}
				RecipientTaskHelper.IsUserPrincipalNameUnique(base.TenantGlobalCatalogSession, this.DataObject, this.DataObject.UserPrincipalName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			if (this.DataObject.IsChanged(MailboxSchema.JournalArchiveAddress) && this.DataObject.JournalArchiveAddress != SmtpAddress.NullReversePath && this.DataObject.JournalArchiveAddress != SmtpAddress.Empty)
			{
				RecipientTaskHelper.IsJournalArchiveAddressUnique(base.TenantGlobalCatalogSession, this.DataObject.OrganizationId, this.DataObject, this.DataObject.JournalArchiveAddress, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			if (this.DataObject.IsModified(MailUserSchema.SamAccountName))
			{
				RecipientTaskHelper.IsSamAccountNameUnique(this.DataObject, this.DataObject.SamAccountName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000010 RID: 16
		private bool isSetRandomPassword;
	}
}
