using System;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security;
using System.Security.AccessControl;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001FC RID: 508
	[Cmdlet("Install", "UserAccount", SupportsShouldProcess = true)]
	public sealed class InstallUserAccount : NewADTaskBase<ADUser>
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0004C5FA File Offset: 0x0004A7FA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewUser(this.Name.ToString());
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0004C60C File Offset: 0x0004A80C
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x0004C619 File Offset: 0x0004A819
		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return this.DataObject.Name;
			}
			set
			{
				this.DataObject.Name = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0004C627 File Offset: 0x0004A827
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x0004C634 File Offset: 0x0004A834
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string LastName
		{
			get
			{
				return this.DataObject.LastName;
			}
			set
			{
				this.DataObject.LastName = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0004C642 File Offset: 0x0004A842
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x0004C659 File Offset: 0x0004A859
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string Domain
		{
			get
			{
				return (string)base.Fields["Domain"];
			}
			set
			{
				base.Fields["Domain"] = value;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0004C66C File Offset: 0x0004A86C
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x0004C674 File Offset: 0x0004A874
		[Parameter(Mandatory = false)]
		public SwitchParameter LogonEnabled { get; set; }

		// Token: 0x0600115C RID: 4444 RVA: 0x0004C680 File Offset: 0x0004A880
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, ConfigScopes.TenantSubTree, 101, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\InstallUserAccount.cs");
			tenantOrRootOrgRecipientSession.LinkResolutionServer = ADSession.GetCurrentConfigDC(tenantOrRootOrgRecipientSession.SessionSettings.GetAccountOrResourceForestFqdn());
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = false;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0004C6D8 File Offset: 0x0004A8D8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			ADForest localForest = ADForest.GetLocalForest();
			if (base.Fields.IsModified("Domain"))
			{
				this.adDomain = localForest.FindDomainByFqdn(this.Domain);
				if (this.adDomain == null)
				{
					base.WriteError(new DomainNotFoundException(this.Domain), ErrorCategory.InvalidArgument, null);
				}
			}
			else
			{
				this.adDomain = localForest.FindLocalDomain();
			}
			string defaultOUForRecipient = RecipientTaskHelper.GetDefaultOUForRecipient(this.adDomain.Id);
			if (string.IsNullOrEmpty(defaultOUForRecipient))
			{
				base.WriteError(new ArgumentException(Strings.UsersContainerNotFound(this.adDomain.Fqdn, WellKnownGuid.UsersWkGuid)), ErrorCategory.InvalidArgument, null);
			}
			this.containerId = new ADObjectId(NativeHelpers.DistinguishedNameFromCanonicalName(defaultOUForRecipient));
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0004C79C File Offset: 0x0004A99C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			aduser.SetId(this.containerId.GetChildId(this.Name));
			if (string.IsNullOrEmpty(aduser.UserPrincipalName))
			{
				aduser.UserPrincipalName = aduser.Name + "@" + this.adDomain.Fqdn;
			}
			if (string.IsNullOrEmpty(aduser.SamAccountName))
			{
				aduser.SamAccountName = "SM_" + Guid.NewGuid().ToString("N").Substring(0, 17);
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0004C83C File Offset: 0x0004AA3C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ADUser dataObject = this.DataObject;
				IRecipientSession recipientSession = (IRecipientSession)base.DataSession;
				recipientSession.Save(dataObject);
				ADUser aduser = (ADUser)base.DataSession.Read<ADUser>(dataObject.Identity);
				if (aduser == null)
				{
					throw new LocalizedException(Strings.ErrorReadingUpdatedUserFromAD(dataObject.OriginatingServer, recipientSession.LastUsedDc));
				}
				aduser.UserAccountControl = UserAccountControlFlags.None;
				if (this.LogonEnabled)
				{
					using (SecureString randomPassword = MailboxTaskUtilities.GetRandomPassword(this.Name, aduser.SamAccountName))
					{
						recipientSession.SetPassword(aduser, randomPassword);
						goto IL_98;
					}
				}
				aduser.UserAccountControl |= UserAccountControlFlags.AccountDisabled;
				IL_98:
				aduser.UserAccountControl |= UserAccountControlFlags.NormalAccount;
				this.DataObject = aduser;
				base.InternalProcessRecord();
			}
			catch (ADObjectAlreadyExistsException ex)
			{
				base.WriteVerbose(Strings.UserCreateFailed(this.Name, ex.Message.ToString()));
			}
			LocalizedString localizedString = LocalizedString.Empty;
			try
			{
				base.WriteVerbose(Strings.VerboseGrantingEoaFullAccessOnMailbox(this.DataObject.Identity.ToString()));
				ADGroup adgroup = base.RootOrgGlobalCatalogSession.ResolveWellKnownGuid<ADGroup>(WellKnownGuid.EoaWkGuid, base.GlobalConfigSession.ConfigurationNamingContext.ToDNString());
				if (adgroup == null)
				{
					localizedString = Strings.ErrorGroupNotFound(WellKnownGuid.EoaWkGuid.ToString());
				}
				else
				{
					DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, (IDirectorySession)base.DataSession, this.DataObject.Id, new ActiveDirectoryAccessRule[]
					{
						new ActiveDirectoryAccessRule(adgroup.Sid, ActiveDirectoryRights.GenericAll, AccessControlType.Allow, ActiveDirectorySecurityInheritance.All)
					});
				}
			}
			catch (ADTransientException ex2)
			{
				localizedString = ex2.LocalizedString;
			}
			catch (ADOperationException ex3)
			{
				localizedString = ex3.LocalizedString;
			}
			catch (SecurityDescriptorAccessDeniedException ex4)
			{
				localizedString = ex4.LocalizedString;
			}
			if (LocalizedString.Empty != localizedString)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorGrantingEraFullAccessOnMailbox(this.DataObject.Identity.ToString(), localizedString)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000793 RID: 1939
		private const string paramDomain = "Domain";

		// Token: 0x04000794 RID: 1940
		private ADObjectId containerId;

		// Token: 0x04000795 RID: 1941
		private ADDomain adDomain;
	}
}
