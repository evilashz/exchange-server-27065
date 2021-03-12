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
	// Token: 0x02000058 RID: 88
	public abstract class EnableDisableInboxRuleBase : ObjectActionTenantADTask<InboxRuleIdParameter, InboxRule>
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x00018B56 File Offset: 0x00016D56
		public EnableDisableInboxRuleBase(bool enabled)
		{
			this.enabled = enabled;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00018B65 File Offset: 0x00016D65
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00018B6D File Offset: 0x00016D6D
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00018B76 File Offset: 0x00016D76
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00018B7E File Offset: 0x00016D7E
		[Parameter(Mandatory = false)]
		public SwitchParameter AlwaysDeleteOutlookRulesBlob { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00018B87 File Offset: 0x00016D87
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00018B9E File Offset: 0x00016D9E
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

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00018BB1 File Offset: 0x00016DB1
		internal string MailboxOwner
		{
			get
			{
				return this.mailboxOwner;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00018BBC File Offset: 0x00016DBC
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
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 139, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\InboxRule\\EnableDisableInboxRuleBase.cs");
			base.VerifyIsWithinScopes(TaskHelper.UnderscopeSessionToOrganization(tenantOrRootOrgRecipientSession, aduser.OrganizationId, true), aduser, true, new DataAccessTask<InboxRule>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			InboxRuleDataProvider inboxRuleDataProvider = new InboxRuleDataProvider(base.SessionSettings, aduser, "EnableDisable-InboxRule");
			this.mailboxOwner = inboxRuleDataProvider.MailboxSession.MailboxOwner.ObjectId.ToString();
			return inboxRuleDataProvider;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00018D58 File Offset: 0x00016F58
		protected override IConfigurable PrepareDataObject()
		{
			InboxRule inboxRule = (InboxRule)base.PrepareDataObject();
			inboxRule.Provider = (InboxRuleDataProvider)base.DataSession;
			inboxRule.Enabled = this.enabled;
			return inboxRule;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00018D8F File Offset: 0x00016F8F
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00018DA2 File Offset: 0x00016FA2
		protected override void InternalStateReset()
		{
			ManageInboxRule.CleanupInboxRuleDataProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00018DC4 File Offset: 0x00016FC4
		protected override void InternalProcessRecord()
		{
			InboxRuleDataProvider inboxRuleDataProvider = (InboxRuleDataProvider)base.DataSession;
			if (this.AlwaysDeleteOutlookRulesBlob.IsPresent)
			{
				inboxRuleDataProvider.SetAlwaysDeleteOutlookRulesBlob(new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			else if (!inboxRuleDataProvider.IsAlwaysDeleteOutlookRulesBlob() && !inboxRuleDataProvider.HandleOutlookBlob(this.Force, () => base.ShouldContinue(Strings.WarningInboxRuleOutlookBlobExists)))
			{
				return;
			}
			ManageInboxRule.ProcessRecord(new Action(base.InternalProcessRecord), new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError), this.Identity);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00018E47 File Offset: 0x00017047
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			ManageInboxRule.CleanupInboxRuleDataProvider(base.DataSession);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000178 RID: 376
		private string mailboxOwner;

		// Token: 0x04000179 RID: 377
		private readonly bool enabled;
	}
}
