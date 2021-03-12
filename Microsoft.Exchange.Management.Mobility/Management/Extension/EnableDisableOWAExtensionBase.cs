using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x02000005 RID: 5
	public abstract class EnableDisableOWAExtensionBase : ObjectActionTenantADTask<AppIdParameter, App>
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002949 File Offset: 0x00000B49
		public EnableDisableOWAExtensionBase(bool enabled)
		{
			this.enabled = enabled;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002958 File Offset: 0x00000B58
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000296F File Offset: 0x00000B6F
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

		// Token: 0x06000039 RID: 57 RVA: 0x00002984 File Offset: 0x00000B84
		protected override IConfigDataProvider CreateSession()
		{
			MailboxIdParameter mailboxIdParameter = null;
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
				ADObjectId executingUserId;
				if (!base.TryGetExecutingUserId(out executingUserId) && this.Mailbox == null)
				{
					base.WriteError(new LocalizedException(Strings.ErrorParameterRequired("Mailbox")), ErrorCategory.InvalidArgument, null);
				}
				mailboxIdParameter = (this.Mailbox ?? MailboxTaskHelper.ResolveMailboxIdentity(executingUserId, new Task.ErrorLoggerDelegate(base.WriteError)));
			}
			this.adUser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
			if (this.Identity != null && this.Identity.InternalOWAExtensionId == null)
			{
				this.Identity.InternalOWAExtensionId = OWAExtensionHelper.CreateOWAExtensionId(this, this.adUser.Id, null, this.Identity.RawExtensionName);
			}
			ADScopeException ex;
			if (!base.TenantGlobalCatalogSession.TryVerifyIsWithinScopes(this.adUser, true, out ex))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotChangeMailboxOutOfWriteScope(this.adUser.Identity.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ErrorCategory.InvalidOperation, this.adUser.Identity);
			}
			OWAExtensionDataProvider owaextensionDataProvider = GetApp.CreateOwaExtensionDataProvider(null, base.TenantGlobalCatalogSession, base.SessionSettings, true, this.adUser, "EnableDisable-App", false, new Task.ErrorLoggerDelegate(base.WriteError));
			this.mailboxOwner = owaextensionDataProvider.MailboxSession.MailboxOwner.ObjectId.ToString();
			return owaextensionDataProvider;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B60 File Offset: 0x00000D60
		protected override IConfigurable PrepareDataObject()
		{
			App app = (App)base.PrepareDataObject();
			app.Enabled = this.enabled;
			return app;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B88 File Offset: 0x00000D88
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.enabled)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, this.adUser.OrganizationId, base.ExecutingUserOrganizationId, false);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.FullyConsistent, sessionSettings, 184, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Extension\\EnableDisableOWAExtensionBase.cs");
				if (!tenantOrTopologyConfigurationSession.GetOrgContainer().AppsForOfficeEnabled)
				{
					this.WriteWarning(Strings.WarningExtensionFeatureDisabled);
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C01 File Offset: 0x00000E01
		protected override void InternalProcessRecord()
		{
			OWAExtensionHelper.ProcessRecord(new Action(base.InternalProcessRecord), new Task.TaskErrorLoggingDelegate(base.WriteError), this.Identity);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C26 File Offset: 0x00000E26
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			OWAExtensionHelper.CleanupOWAExtensionDataProvider(base.DataSession);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400001C RID: 28
		private ADUser adUser;

		// Token: 0x0400001D RID: 29
		protected string mailboxOwner;

		// Token: 0x0400001E RID: 30
		private readonly bool enabled;
	}
}
