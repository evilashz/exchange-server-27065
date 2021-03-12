using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000091 RID: 145
	[Cmdlet("Remove", "MicrosoftExchangeRecipient", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMicrosoftExchangeRecipient : DataAccessTask<ADMicrosoftExchangeRecipient>
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0002981E File Offset: 0x00027A1E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMicrosoftExchangeRecipient;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00029825 File Offset: 0x00027A25
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x0002982D File Offset: 0x00027A2D
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00029840 File Offset: 0x00027A40
		protected override IConfigDataProvider CreateSession()
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, false);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, adsessionSettings, 82, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\MicrosoftExchangeRecipient\\RemoveMicrosoftExchangeRecipient.cs");
			tenantOrRootOrgRecipientSession.LinkResolutionServer = ADSession.GetCurrentConfigDC(adsessionSettings.GetAccountOrResourceForestFqdn());
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = false;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000298A4 File Offset: 0x00027AA4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.recipient = MailboxTaskHelper.FindMicrosoftExchangeRecipient((IRecipientSession)base.DataSession, DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.CurrentOrganizationId), 114, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\MicrosoftExchangeRecipient\\RemoveMicrosoftExchangeRecipient.cs"));
			if (this.recipient == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorMicrosoftExchangeRecipientNotFound), ErrorCategory.InvalidOperation, null);
			}
			IVersionable versionable = this.recipient;
			if (versionable != null && versionable.MaximumSupportedExchangeObjectVersion.IsOlderThan(versionable.ExchangeVersion))
			{
				base.WriteError(new TaskException(Strings.ErrorRemoveNewerObject(this.recipient.Identity.ToString(), versionable.ExchangeVersion.ExchangeBuild.ToString())), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002997C File Offset: 0x00027B7C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.WriteVerbose(TaskVerboseStringHelper.GetDeleteObjectVerboseString(this.recipient.Identity, base.DataSession, typeof(ADMicrosoftExchangeRecipient)));
			base.DataSession.Delete(this.recipient);
			TaskLogger.LogExit();
		}

		// Token: 0x040001F9 RID: 505
		private ADRecipient recipient;
	}
}
