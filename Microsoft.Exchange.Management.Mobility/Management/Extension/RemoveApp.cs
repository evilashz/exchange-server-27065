using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x02000010 RID: 16
	[Cmdlet("Remove", "App", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveApp : RemoveTenantADTaskBase<AppIdParameter, App>
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00005A5A File Offset: 0x00003C5A
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00005A71 File Offset: 0x00003C71
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00005A84 File Offset: 0x00003C84
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00005AAA File Offset: 0x00003CAA
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

		// Token: 0x060000AF RID: 175 RVA: 0x00005AC4 File Offset: 0x00003CC4
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
				ADObjectId executingUserId;
				if (!base.TryGetExecutingUserId(out executingUserId) && this.Mailbox == null)
				{
					return this.CreateDataProviderForNonMailboxUser();
				}
				mailboxIdParameter = (this.Mailbox ?? MailboxTaskHelper.ResolveMailboxIdentity(executingUserId, new Task.ErrorLoggerDelegate(base.WriteError)));
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
			if (!TaskHelper.UnderscopeSessionToOrganization(base.TenantGlobalCatalogSession, aduser.OrganizationId, true).TryVerifyIsWithinScopes(aduser, true, out ex))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotChangeMailboxOutOfWriteScope(aduser.Identity.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ErrorCategory.InvalidOperation, aduser.Identity);
			}
			IConfigDataProvider configDataProvider = GetApp.CreateOwaExtensionDataProvider(null, base.TenantGlobalCatalogSession, base.SessionSettings, !this.OrganizationApp, aduser, "Remove-App", false, new Task.ErrorLoggerDelegate(base.WriteError));
			this.mailboxOwner = ((OWAExtensionDataProvider)configDataProvider).MailboxSession.MailboxOwner.ObjectId.ToString();
			return configDataProvider;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005CB8 File Offset: 0x00003EB8
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			base.CheckExclusiveParameters(new object[]
			{
				"Mailbox",
				"OrganizationApp"
			});
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005CE9 File Offset: 0x00003EE9
		protected override void InternalProcessRecord()
		{
			OWAExtensionHelper.ProcessRecord(new Action(base.InternalProcessRecord), new Task.TaskErrorLoggingDelegate(base.WriteError), this.Identity);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005D2C File Offset: 0x00003F2C
		protected override IConfigurable ResolveDataObject()
		{
			IConfigurable configurable = null;
			OWAExtensionHelper.ProcessRecord(delegate
			{
				configurable = this.<>n__FabricatedMethod3();
			}, new Task.TaskErrorLoggingDelegate(base.WriteError), this.Identity);
			return configurable;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005D76 File Offset: 0x00003F76
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			OWAExtensionHelper.CleanupOWAExtensionDataProvider(base.DataSession);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00005D90 File Offset: 0x00003F90
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.OrganizationApp)
				{
					return Strings.ConfirmationMessageUninstallOwaOrgExtension(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageUninstallOwaExtension(this.Identity.ToString(), this.mailboxOwner);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005DC8 File Offset: 0x00003FC8
		private IConfigDataProvider CreateDataProviderForNonMailboxUser()
		{
			if (!this.OrganizationApp)
			{
				base.WriteError(new LocalizedException(Strings.ErrorAppTargetMailboxNotFound("OrganizationApp", "Mailbox")), ErrorCategory.InvalidArgument, null);
			}
			if (base.IsDebugOn)
			{
				base.WriteDebug("Creating data provider for non mailbox user.");
			}
			IConfigDataProvider result = new OWAAppDataProviderForNonMailboxUser(null, base.TenantGlobalCatalogSession, base.SessionSettings, !this.OrganizationApp, "Remove-App");
			if (this.Identity != null && this.Identity.InternalOWAExtensionId == null)
			{
				this.Identity.InternalOWAExtensionId = OWAExtensionHelper.CreateOWAExtensionId(this, new ADObjectId(), null, this.Identity.RawExtensionName);
			}
			return result;
		}

		// Token: 0x04000043 RID: 67
		private string mailboxOwner;
	}
}
