using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200005D RID: 93
	[Cmdlet("Remove", "InboxRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveInboxRule : RemoveTenantADTaskBase<InboxRuleIdParameter, InboxRule>
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0001A3AF File Offset: 0x000185AF
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0001A3B7 File Offset: 0x000185B7
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001A3C0 File Offset: 0x000185C0
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0001A3C8 File Offset: 0x000185C8
		[Parameter(Mandatory = false)]
		public SwitchParameter AlwaysDeleteOutlookRulesBlob { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001A3D1 File Offset: 0x000185D1
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0001A3E8 File Offset: 0x000185E8
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

		// Token: 0x06000647 RID: 1607 RVA: 0x0001A3FC File Offset: 0x000185FC
		protected override IConfigDataProvider CreateSession()
		{
			MailboxIdParameter mailboxIdParameter = null;
			if (this.Identity != null)
			{
				if (this.Identity.InternalInboxRuleId != null)
				{
					mailboxIdParameter = new MailboxIdParameter(this.Identity.InternalInboxRuleId.MailboxOwnerId);
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
				base.TryGetExecutingUserId(out executingUserId);
				mailboxIdParameter = (this.Mailbox ?? MailboxTaskHelper.ResolveMailboxIdentity(executingUserId, new Task.ErrorLoggerDelegate(base.WriteError)));
			}
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
			if (this.Identity != null && this.Identity.InternalInboxRuleId == null)
			{
				this.Identity.InternalInboxRuleId = new InboxRuleId(aduser.Id, this.Identity.RawRuleName, this.Identity.RawRuleId);
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 127, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\InboxRule\\RemoveInboxRule.cs");
			base.VerifyIsWithinScopes(TaskHelper.UnderscopeSessionToOrganization(tenantOrRootOrgRecipientSession, aduser.OrganizationId, true), aduser, true, new DataAccessTask<InboxRule>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			InboxRuleDataProvider inboxRuleDataProvider = new InboxRuleDataProvider(base.SessionSettings, aduser, "Remove-InboxRule");
			this.mailboxOwner = inboxRuleDataProvider.MailboxSession.MailboxOwner.ObjectId.ToString();
			inboxRuleDataProvider.IncludeHidden = true;
			return inboxRuleDataProvider;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001A59A File Offset: 0x0001879A
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001A5AD File Offset: 0x000187AD
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveInboxRule(this.Identity.ToString(), this.mailboxOwner);
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001A5C5 File Offset: 0x000187C5
		protected override void InternalStateReset()
		{
			ManageInboxRule.CleanupInboxRuleDataProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001A5E8 File Offset: 0x000187E8
		protected override void InternalProcessRecord()
		{
			InboxRuleDataProvider inboxRuleDataProvider = (InboxRuleDataProvider)base.DataSession;
			if (this.AlwaysDeleteOutlookRulesBlob.IsPresent)
			{
				inboxRuleDataProvider.SetAlwaysDeleteOutlookRulesBlob(new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			else if (!inboxRuleDataProvider.HandleOutlookBlob(this.Force, () => base.ShouldContinue(Strings.WarningInboxRuleOutlookBlobExists)))
			{
				return;
			}
			ManageInboxRule.ProcessRecord(new Action(base.InternalProcessRecord), new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError), this.Identity);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001A66A File Offset: 0x0001886A
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			ManageInboxRule.CleanupInboxRuleDataProvider(base.DataSession);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000181 RID: 385
		private string mailboxOwner;
	}
}
