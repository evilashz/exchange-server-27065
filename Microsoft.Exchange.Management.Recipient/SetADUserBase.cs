using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.RbacTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000D4 RID: 212
	public abstract class SetADUserBase<TIdentity, TPublicObject> : SetOrgPersonObjectTask<TIdentity, TPublicObject, ADUser> where TIdentity : IIdentityParameter, new() where TPublicObject : User, new()
	{
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0003C1D8 File Offset: 0x0003A3D8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				TIdentity identity = this.Identity;
				return Strings.ConfirmationMessageSetUser(identity.ToString());
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0003C1FE File Offset: 0x0003A3FE
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			this.orgAdminHelper = new RoleAssignmentsGlobalConstraints(this.ConfigurationSession, base.TenantGlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError));
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0003C22C File Offset: 0x0003A42C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			SetADUserBase<TIdentity, TPublicObject>.ValidateUserParameters(this.DataObject, this.ConfigurationSession, RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(base.DomainController, this.DataObject.Id), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client, this.ShouldCheckAcceptedDomains(), base.ProvisioningCache);
			if (this.DataObject.IsChanged(UserSchema.WindowsLiveID) && this.DataObject.WindowsLiveID != SmtpAddress.Empty)
			{
				if (this.ShouldCheckAcceptedDomains())
				{
					RecipientTaskHelper.ValidateInAcceptedDomain(this.ConfigurationSession, this.DataObject.OrganizationId, this.DataObject.WindowsLiveID.Domain, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
					MailboxTaskHelper.IsLiveIdExists((IRecipientSession)base.DataSession, this.DataObject.WindowsLiveID, this.DataObject.NetID, new Task.ErrorLoggerDelegate(base.WriteError));
				}
				this.DataObject.UserPrincipalName = this.DataObject.WindowsLiveID.ToString();
			}
			if (this.DataObject.IsModified(UserSchema.CertificateSubject))
			{
				NewLinkedUser.ValidateCertificateSubject(this.DataObject.CertificateSubject, OrganizationId.ForestWideOrgId.Equals(this.DataObject.OrganizationId) ? null : this.DataObject.OrganizationId.PartitionId, this.DataObject.Id, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.IsDisablingRemotePowerShell() && this.orgAdminHelper.ShouldPreventLastAdminRemoval(this, this.DataObject.OrganizationId) && this.orgAdminHelper.IsLastAdmin(this.DataObject))
			{
				TIdentity identity = this.Identity;
				base.WriteError(new RecipientTaskException(Strings.ErrorCannotDisableRemotePowershelForLastDelegatingOrgAdmin(identity.ToString())), ErrorCategory.InvalidOperation, this.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0003C430 File Offset: 0x0003A630
		internal static void ValidateUserParameters(ADUser userObject, IConfigurationSession configSession, IRecipientSession globalCatalogSession, Task.TaskVerboseLoggingDelegate verboseLogger, Task.ErrorLoggerDelegate errorLogger, ExchangeErrorCategory errorLoggerCategory, bool shouldCheckAcceptedDomains, ProvisioningCache provisioningCache)
		{
			if (userObject.IsModified(UserSchema.ResetPasswordOnNextLogon) && userObject.ResetPasswordOnNextLogon && (userObject.UserAccountControl & UserAccountControlFlags.DoNotExpirePassword) != UserAccountControlFlags.None)
			{
				errorLogger(new TaskInvalidOperationException(Strings.ErrorUserCannotChangePasswordAtNextLogon(userObject.Identity.ToString())), errorLoggerCategory, userObject.Identity);
			}
			if (userObject.IsModified(UserSchema.UserPrincipalName))
			{
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.ValidateExternalEmailAddressInAcceptedDomain.Enabled && shouldCheckAcceptedDomains)
				{
					RecipientTaskHelper.ValidateInAcceptedDomain(configSession, userObject.OrganizationId, RecipientTaskHelper.GetDomainPartOfUserPrincalName(userObject.UserPrincipalName), errorLogger, provisioningCache);
				}
				RecipientTaskHelper.IsUserPrincipalNameUnique(globalCatalogSession, userObject, userObject.UserPrincipalName, verboseLogger, errorLogger, errorLoggerCategory);
			}
			if (userObject.IsModified(UserSchema.SamAccountName))
			{
				RecipientTaskHelper.IsSamAccountNameUnique(globalCatalogSession, userObject, userObject.SamAccountName, verboseLogger, errorLogger, errorLoggerCategory);
			}
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0003C503 File Offset: 0x0003A703
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return User.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0003C510 File Offset: 0x0003A710
		private bool IsDisablingRemotePowerShell()
		{
			return this.DataObject.IsChanged(ADRecipientSchema.RemotePowerShellEnabled) && !this.DataObject.RemotePowerShellEnabled;
		}

		// Token: 0x040002F7 RID: 759
		private RoleAssignmentsGlobalConstraints orgAdminHelper;
	}
}
