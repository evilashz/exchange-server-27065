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
	// Token: 0x0200005B RID: 91
	[Cmdlet("Get", "InboxRule", DefaultParameterSetName = "Identity")]
	public sealed class GetInboxRule : GetTenantADObjectWithIdentityTaskBase<InboxRuleIdParameter, InboxRule>
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00018E7B File Offset: 0x0001707B
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00018E92 File Offset: 0x00017092
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

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00018EA5 File Offset: 0x000170A5
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00018EBC File Offset: 0x000170BC
		[Parameter(Mandatory = false)]
		public ExTimeZoneValue DescriptionTimeZone
		{
			get
			{
				return (ExTimeZoneValue)base.Fields["DescriptionTimeZone"];
			}
			set
			{
				base.Fields["DescriptionTimeZone"] = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00018ECF File Offset: 0x000170CF
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00018EE6 File Offset: 0x000170E6
		[Parameter(Mandatory = false)]
		public string DescriptionTimeFormat
		{
			get
			{
				return (string)base.Fields["DescriptionTimeFormat"];
			}
			set
			{
				base.Fields["DescriptionTimeFormat"] = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00018EF9 File Offset: 0x000170F9
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00018F01 File Offset: 0x00017101
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeHidden { get; set; }

		// Token: 0x060005A7 RID: 1447 RVA: 0x00018F0C File Offset: 0x0001710C
		protected override void WriteResult(IConfigurable dataObject)
		{
			InboxRule inboxRule = dataObject as InboxRule;
			if (inboxRule != null && inboxRule.InError)
			{
				this.WriteWarning(Strings.WarningInboxRuleInError(inboxRule.Name));
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00018F44 File Offset: 0x00017144
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
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 150, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\InboxRule\\GetInboxRule.cs");
			base.VerifyIsWithinScopes(TaskHelper.UnderscopeSessionToOrganization(tenantOrRootOrgRecipientSession, aduser.OrganizationId, true), aduser, true, new DataAccessTask<InboxRule>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			InboxRuleDataProvider inboxRuleDataProvider = new InboxRuleDataProvider(base.SessionSettings, aduser, "Get-InboxRule");
			if (this.IncludeHidden)
			{
				inboxRuleDataProvider.IncludeHidden = true;
			}
			if (base.Fields.IsChanged("DescriptionTimeZone"))
			{
				inboxRuleDataProvider.DescriptionTimeZone = this.DescriptionTimeZone;
			}
			if (base.Fields.IsChanged("DescriptionTimeFormat"))
			{
				inboxRuleDataProvider.DescriptionTimeFormat = this.DescriptionTimeFormat;
			}
			return inboxRuleDataProvider;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00019114 File Offset: 0x00017314
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00019127 File Offset: 0x00017327
		protected override void InternalStateReset()
		{
			ManageInboxRule.CleanupInboxRuleDataProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001913A File Offset: 0x0001733A
		protected override void InternalProcessRecord()
		{
			ManageInboxRule.ProcessRecord(new Action(base.InternalProcessRecord), new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError), this.Identity);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001915F File Offset: 0x0001735F
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			ManageInboxRule.CleanupInboxRuleDataProvider(base.DataSession);
			GC.SuppressFinalize(this);
		}
	}
}
