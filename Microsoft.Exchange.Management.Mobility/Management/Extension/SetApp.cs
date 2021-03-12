using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x02000011 RID: 17
	[Cmdlet("Set", "App", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetApp : SetTenantADTaskBase<AppIdParameter, App, App>
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005E7D File Offset: 0x0000407D
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00005E94 File Offset: 0x00004094
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005EA7 File Offset: 0x000040A7
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00005ECD File Offset: 0x000040CD
		[Parameter(Mandatory = false)]
		public SwitchParameter OrganizationApp
		{
			get
			{
				return (SwitchParameter)(base.Fields["OrganizationApp"] ?? false);
			}
			set
			{
				base.Fields["OrganizationApp"] = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00005EE5 File Offset: 0x000040E5
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00005EFC File Offset: 0x000040FC
		[Parameter(Mandatory = false)]
		public ClientExtensionProvidedTo ProvidedTo
		{
			get
			{
				return (ClientExtensionProvidedTo)base.Fields["ProvidedTo"];
			}
			set
			{
				base.Fields["ProvidedTo"] = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005F14 File Offset: 0x00004114
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00005F2B File Offset: 0x0000412B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> UserList
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>>)base.Fields["UserList"];
			}
			set
			{
				base.Fields["UserList"] = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005F3E File Offset: 0x0000413E
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00005F55 File Offset: 0x00004155
		[Parameter(Mandatory = false)]
		public DefaultStateForUser DefaultStateForUser
		{
			get
			{
				return (DefaultStateForUser)base.Fields["DefaultStateForUser"];
			}
			set
			{
				base.Fields["DefaultStateForUser"] = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005F6D File Offset: 0x0000416D
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00005F84 File Offset: 0x00004184
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields["Enabled"];
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005F9C File Offset: 0x0000419C
		protected override IConfigDataProvider CreateSession()
		{
			ADUser adUser = null;
			ADObjectId executingUserId;
			if (!base.TryGetExecutingUserId(out executingUserId))
			{
				return this.CreateDataProviderForNonMailboxUser();
			}
			MailboxIdParameter mailboxIdParameter = MailboxTaskHelper.ResolveMailboxIdentity(executingUserId, new Task.ErrorLoggerDelegate(base.WriteError));
			try
			{
				adUser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
			}
			catch (ManagementObjectNotFoundException)
			{
				return this.CreateDataProviderForNonMailboxUser();
			}
			if (this.Identity != null && this.Identity.InternalOWAExtensionId == null)
			{
				this.Identity.InternalOWAExtensionId = OWAExtensionHelper.CreateOWAExtensionId(this, new ADObjectId(), null, this.Identity.RawExtensionName);
			}
			if (this.Organization != null)
			{
				this.SetCurrentOrganizationId();
			}
			return GetApp.CreateOwaExtensionDataProvider(this.Organization, base.TenantGlobalCatalogSession, base.SessionSettings, false, adUser, "Set-App", false, new Task.ErrorLoggerDelegate(base.WriteError));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000060A0 File Offset: 0x000042A0
		protected override IConfigurable PrepareDataObject()
		{
			OrgApp orgApp = (OrgApp)base.PrepareDataObject();
			if (base.Fields.IsModified("Enabled"))
			{
				orgApp.Enabled = this.Enabled;
			}
			if (base.Fields.IsModified("DefaultStateForUser"))
			{
				orgApp.DefaultStateForUser = new DefaultStateForUser?(this.DefaultStateForUser);
			}
			if (base.Fields.IsModified("ProvidedTo"))
			{
				orgApp.ProvidedTo = this.ProvidedTo;
			}
			if (base.Fields.IsModified("UserList"))
			{
				if (this.UserList != null && this.UserList.Count > 1000)
				{
					base.WriteError(new LocalizedException(Strings.ErrorTooManyUsersInUserList(1000)), ErrorCategory.InvalidArgument, null);
				}
				orgApp.UserList = OrgApp.ConvertUserListToPresentationFormat(this, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), this.UserList);
			}
			return orgApp;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000617C File Offset: 0x0000437C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!this.OrganizationApp)
			{
				base.WriteError(new LocalizedException(Strings.ErrorParameterRequired("OrganizationApp")), ErrorCategory.InvalidArgument, null);
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.FullyConsistent, sessionSettings, 230, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Extension\\setapp.cs");
			if (!tenantOrTopologyConfigurationSession.GetOrgContainer().AppsForOfficeEnabled)
			{
				this.WriteWarning(Strings.WarningExtensionFeatureDisabled);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000620C File Offset: 0x0000440C
		protected override void InternalProcessRecord()
		{
			OWAExtensionHelper.ProcessRecord(new Action(base.InternalProcessRecord), new Task.TaskErrorLoggingDelegate(base.WriteError), this.Identity);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006231 File Offset: 0x00004431
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			OWAExtensionHelper.CleanupOWAExtensionDataProvider(base.DataSession);
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000624B File Offset: 0x0000444B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageModifyOwaOrgExtension(this.Identity.ToString());
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006260 File Offset: 0x00004460
		private void SetCurrentOrganizationId()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 283, "SetCurrentOrganizationId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Extension\\setapp.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
			base.CurrentOrganizationId = adorganizationalUnit.OrganizationId;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000062F4 File Offset: 0x000044F4
		private IConfigDataProvider CreateDataProviderForNonMailboxUser()
		{
			if (!this.OrganizationApp)
			{
				base.WriteError(new LocalizedException(Strings.ErrorParameterRequired("OrganizationApp")), ErrorCategory.InvalidArgument, null);
			}
			if (base.IsDebugOn)
			{
				base.WriteDebug("Creating data provider for non mailbox user.");
			}
			IConfigDataProvider result = new OWAAppDataProviderForNonMailboxUser((this.Organization == null) ? null : this.Organization.RawIdentity, base.TenantGlobalCatalogSession, base.SessionSettings, !this.OrganizationApp, "Set-App");
			if (this.Identity != null && this.Identity.InternalOWAExtensionId == null)
			{
				this.Identity.InternalOWAExtensionId = OWAExtensionHelper.CreateOWAExtensionId(this, new ADObjectId(), null, this.Identity.RawExtensionName);
			}
			return result;
		}
	}
}
