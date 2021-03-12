using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x02000008 RID: 8
	[Cmdlet("Get", "App", DefaultParameterSetName = "Identity")]
	public sealed class GetApp : GetTenantADObjectWithIdentityTaskBase<AppIdParameter, App>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002C8A File Offset: 0x00000E8A
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002CA1 File Offset: 0x00000EA1
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002CB4 File Offset: 0x00000EB4
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002CCB File Offset: 0x00000ECB
		[Parameter(Mandatory = false)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002CDE File Offset: 0x00000EDE
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002D04 File Offset: 0x00000F04
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

		// Token: 0x06000049 RID: 73 RVA: 0x00002D1C File Offset: 0x00000F1C
		internal static OWAExtensionDataProvider CreateOwaExtensionDataProvider(OrganizationIdParameter organizationIdParameter, IRecipientSession tenantGlobalCatalogSession, ADSessionSettings sessionSettings, bool isUserScope, ADUser adUser, string taskName, bool isDebugOn, Task.ErrorLoggerDelegate writeErrorDelegate)
		{
			OWAExtensionDataProvider result = null;
			LocalizedException ex = null;
			try
			{
				result = new OWAExtensionDataProvider((organizationIdParameter == null) ? null : organizationIdParameter.RawIdentity, tenantGlobalCatalogSession, sessionSettings, isUserScope, adUser, taskName, isDebugOn);
			}
			catch (StorageTransientException ex2)
			{
				ex = ex2;
			}
			catch (StoragePermanentException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				writeErrorDelegate(ex, ExchangeErrorCategory.Client, adUser.Identity);
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D88 File Offset: 0x00000F88
		protected override IConfigDataProvider CreateSession()
		{
			MailboxIdParameter mailboxIdParameter = null;
			ADUser aduser = null;
			if (this.Identity != null)
			{
				if (this.Identity.InternalOWAExtensionId != null)
				{
					mailboxIdParameter = new MailboxIdParameter(this.Identity.InternalOWAExtensionId.MailboxOwnerId);
				}
				else
				{
					mailboxIdParameter = this.Identity.RawMailbox;
				}
			}
			if (mailboxIdParameter != null && this.Mailbox != null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorConflictingMailboxes), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (mailboxIdParameter == null)
			{
				ADObjectId adobjectId;
				base.TryGetExecutingUserId(out adobjectId);
				if (adobjectId == null && this.Mailbox == null)
				{
					return this.CreateDataProviderForNonMailboxUser();
				}
				mailboxIdParameter = (this.Mailbox ?? MailboxTaskHelper.ResolveMailboxIdentity(adobjectId, new Task.ErrorLoggerDelegate(base.WriteError)));
			}
			try
			{
				aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
			}
			catch (ManagementObjectNotFoundException)
			{
				return this.CreateDataProviderForNonMailboxUser();
			}
			if (this.Identity != null && this.Identity.InternalOWAExtensionId == null)
			{
				this.Identity.InternalOWAExtensionId = OWAExtensionHelper.CreateOWAExtensionId(this, aduser.Id, null, this.Identity.RawExtensionName);
			}
			ADScopeException ex;
			if (!base.TenantGlobalCatalogSession.TryVerifyIsWithinScopes(aduser, true, out ex))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotChangeMailboxOutOfWriteScope(aduser.Identity.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ErrorCategory.InvalidOperation, aduser.Identity);
			}
			if (this.Organization != null)
			{
				this.SetCurrentOrganizationId();
			}
			return GetApp.CreateOwaExtensionDataProvider(this.Organization, base.TenantGlobalCatalogSession, base.SessionSettings, !this.OrganizationApp, aduser, "Get-App", base.IsDebugOn, new Task.ErrorLoggerDelegate(base.WriteError));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F6C File Offset: 0x0000116C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			base.CheckExclusiveParameters(new object[]
			{
				"Mailbox",
				"OrganizationApp"
			});
			if (this.Organization != null && !this.OrganizationApp)
			{
				base.WriteError(new LocalizedException(Strings.ErrorParameterRequired("OrganizationApp")), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002FCC File Offset: 0x000011CC
		protected override void InternalProcessRecord()
		{
			OWAExtensionHelper.ProcessRecord(new Action(base.InternalProcessRecord), new Task.TaskErrorLoggingDelegate(base.WriteError), this.Identity);
			if (base.IsDebugOn)
			{
				base.WriteDebug(((OWAExtensionDataProvider)base.DataSession).RawMasterTableXml);
				base.WriteDebug(((OWAExtensionDataProvider)base.DataSession).RawOrgMasterTableXml);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003030 File Offset: 0x00001230
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			OWAExtensionHelper.CleanupOWAExtensionDataProvider(base.DataSession);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000304C File Offset: 0x0000124C
		private IConfigDataProvider CreateDataProviderForNonMailboxUser()
		{
			if (base.IsDebugOn)
			{
				base.WriteDebug("Creating data provider for non mailbox user.");
			}
			IConfigDataProvider result = new OWAAppDataProviderForNonMailboxUser((this.Organization == null) ? null : this.Organization.RawIdentity, base.TenantGlobalCatalogSession, base.SessionSettings, !this.OrganizationApp, "Get-App");
			if (this.Identity != null && this.Identity.InternalOWAExtensionId == null)
			{
				this.Identity.InternalOWAExtensionId = OWAExtensionHelper.CreateOWAExtensionId(this, new ADObjectId(), null, this.Identity.RawExtensionName);
			}
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030E8 File Offset: 0x000012E8
		private void SetCurrentOrganizationId()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 313, "SetCurrentOrganizationId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Extension\\getapp.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
			base.CurrentOrganizationId = adorganizationalUnit.OrganizationId;
		}
	}
}
