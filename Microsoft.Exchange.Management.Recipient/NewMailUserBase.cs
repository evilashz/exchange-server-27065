using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000088 RID: 136
	public abstract class NewMailUserBase : NewUserBase
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0002800C File Offset: 0x0002620C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("WindowsLiveID" == base.ParameterSetName || "MicrosoftOnlineServicesID" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailuserWithWindowsLiveId(base.Name.ToString(), base.WindowsLiveID.SmtpAddress.ToString(), base.RecipientContainerId.ToString());
				}
				return Strings.ConfirmationMessageNewMailUser(base.Name.ToString(), this.ExternalEmailAddress.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x000280A3 File Offset: 0x000262A3
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x000280AB File Offset: 0x000262AB
		[Parameter(Mandatory = true, ParameterSetName = "EnabledUser")]
		[Parameter(Mandatory = true, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = true, ParameterSetName = "WindowsLiveID")]
		public override SecureString Password
		{
			get
			{
				return base.Password;
			}
			set
			{
				base.Password = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x000280B4 File Offset: 0x000262B4
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x000280BC File Offset: 0x000262BC
		[Parameter(Mandatory = true, ParameterSetName = "EnabledUser")]
		public override string UserPrincipalName
		{
			get
			{
				return base.UserPrincipalName;
			}
			set
			{
				base.UserPrincipalName = value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x000280C5 File Offset: 0x000262C5
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x000280D2 File Offset: 0x000262D2
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = true, ParameterSetName = "EnabledUser")]
		[Parameter(Mandatory = true, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return this.DataObject.ExternalEmailAddress;
			}
			set
			{
				this.DataObject.ExternalEmailAddress = value;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x000280E0 File Offset: 0x000262E0
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x000280ED File Offset: 0x000262ED
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		public virtual bool UsePreferMessageFormat
		{
			get
			{
				return this.DataObject.UsePreferMessageFormat;
			}
			set
			{
				this.DataObject.UsePreferMessageFormat = value;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x000280FB File Offset: 0x000262FB
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00028108 File Offset: 0x00026308
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		public virtual MessageFormat MessageFormat
		{
			get
			{
				return this.DataObject.MessageFormat;
			}
			set
			{
				this.DataObject.MessageFormat = value;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00028116 File Offset: 0x00026316
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00028123 File Offset: 0x00026323
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		public virtual MessageBodyFormat MessageBodyFormat
		{
			get
			{
				return this.DataObject.MessageBodyFormat;
			}
			set
			{
				this.DataObject.MessageBodyFormat = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00028131 File Offset: 0x00026331
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0002813E File Offset: 0x0002633E
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		public virtual MacAttachmentFormat MacAttachmentFormat
		{
			get
			{
				return this.DataObject.MacAttachmentFormat;
			}
			set
			{
				this.DataObject.MacAttachmentFormat = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0002814C File Offset: 0x0002634C
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00028163 File Offset: 0x00026363
		[Parameter]
		public bool RemotePowerShellEnabled
		{
			get
			{
				return (bool)base.Fields[MailUserSchema.RemotePowerShellEnabled];
			}
			set
			{
				base.Fields[MailUserSchema.RemotePowerShellEnabled] = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0002817B File Offset: 0x0002637B
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00028188 File Offset: 0x00026388
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		public CountryInfo UsageLocation
		{
			get
			{
				return this.DataObject.UsageLocation;
			}
			set
			{
				this.DataObject.UsageLocation = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00028196 File Offset: 0x00026396
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x000281AD File Offset: 0x000263AD
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public MailboxProvisioningConstraint MailboxProvisioningConstraint
		{
			get
			{
				return (MailboxProvisioningConstraint)base.Fields[ADRecipientSchema.MailboxProvisioningConstraint];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailboxProvisioningConstraint] = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x000281C0 File Offset: 0x000263C0
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x000281D7 File Offset: 0x000263D7
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
		{
			get
			{
				return (MultiValuedProperty<MailboxProvisioningConstraint>)base.Fields[ADRecipientSchema.MailboxProvisioningPreferences];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailboxProvisioningPreferences] = value;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000281EA File Offset: 0x000263EA
		public NewMailUserBase()
		{
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000281F2 File Offset: 0x000263F2
		protected override void StampDefaultValues(ADUser dataObject)
		{
			dataObject.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
			base.StampDefaultValues(dataObject);
			dataObject.StampDefaultValues(RecipientType.MailUser);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002822C File Offset: 0x0002642C
		protected override void PrepareUserObject(ADUser user)
		{
			TaskLogger.LogEnter();
			if (base.WindowsLiveID == null && base.SoftDeletedObject == null && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.WindowsLiveID.Enabled && !RecipientTaskHelper.SMTPAddressCheckWithAcceptedDomain(this.ConfigurationSession, user.OrganizationId, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorWindowsLiveIdRequired(user.Name)), ExchangeErrorCategory.Client, null);
			}
			if (base.WindowsLiveID != null && base.WindowsLiveID.SmtpAddress != SmtpAddress.Empty)
			{
				if (this.ExternalEmailAddress == null)
				{
					user.ExternalEmailAddress = ProxyAddress.Parse(base.WindowsLiveID.SmtpAddress.ToString());
				}
				user.UserPrincipalName = base.WindowsLiveID.SmtpAddress.ToString();
				base.IsSetRandomPassword = (base.SoftDeletedObject == null || base.IsSetRandomPassword);
			}
			if (string.IsNullOrEmpty(user.UserPrincipalName))
			{
				user.UserPrincipalName = RecipientTaskHelper.GenerateUniqueUserPrincipalName(base.TenantGlobalCatalogSession, user.Name, this.ConfigurationSession.GetDefaultAcceptedDomain().DomainName.Domain, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			if (base.SoftDeletedObject == null)
			{
				if (base.Fields.IsModified(MailUserSchema.RemotePowerShellEnabled))
				{
					user.RemotePowerShellEnabled = this.RemotePowerShellEnabled;
				}
				else
				{
					user.RemotePowerShellEnabled = true;
				}
				MailUserTaskHelper.ValidateExternalEmailAddress(user, this.ConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
			}
			if (base.Fields.IsChanged(ADRecipientSchema.MailboxProvisioningConstraint))
			{
				user.MailboxProvisioningConstraint = this.MailboxProvisioningConstraint;
			}
			if (base.Fields.IsChanged(ADRecipientSchema.MailboxProvisioningPreferences))
			{
				user.MailboxProvisioningPreferences = this.MailboxProvisioningPreferences;
			}
			if (user.MailboxProvisioningConstraint != null)
			{
				MailboxTaskHelper.ValidateMailboxProvisioningConstraintEntries(new MailboxProvisioningConstraint[]
				{
					user.MailboxProvisioningConstraint
				}, base.DomainController, delegate(string message)
				{
					base.WriteVerbose(new LocalizedString(message));
				}, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (user.MailboxProvisioningPreferences != null)
			{
				MailboxTaskHelper.ValidateMailboxProvisioningConstraintEntries(user.MailboxProvisioningPreferences, base.DomainController, delegate(string message)
				{
					base.WriteVerbose(new LocalizedString(message));
				}, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			base.PrepareUserObject(user);
			TaskLogger.LogExit();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002849C File Offset: 0x0002669C
		protected override void StampChangesAfterSettingPassword()
		{
			base.StampChangesAfterSettingPassword();
			if (base.ParameterSetName == "DisabledUser")
			{
				this.DataObject.UserAccountControl = (UserAccountControlFlags.AccountDisabled | UserAccountControlFlags.NormalAccount);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x000284C6 File Offset: 0x000266C6
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(MailUser).FullName;
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000284D7 File Offset: 0x000266D7
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000284E4 File Offset: 0x000266E4
		protected override void InternalProcessRecord()
		{
			if (this.DataObject.IsInLitigationHoldOrInplaceHold)
			{
				RecoverableItemsQuotaHelper.IncreaseRecoverableItemsQuotaIfNeeded(this.DataObject);
			}
			base.InternalProcessRecord();
		}
	}
}
