using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.SQM;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200005E RID: 94
	[Cmdlet("set", "InboxRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetInboxRule : SetTenantADTaskBase<InboxRuleIdParameter, InboxRule, InboxRule>
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001A684 File Offset: 0x00018884
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001A68C File Offset: 0x0001888C
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001A695 File Offset: 0x00018895
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0001A69D File Offset: 0x0001889D
		[Parameter(Mandatory = false)]
		public SwitchParameter AlwaysDeleteOutlookRulesBlob { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001A6A6 File Offset: 0x000188A6
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001A6BD File Offset: 0x000188BD
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001A6D0 File Offset: 0x000188D0
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001A6E7 File Offset: 0x000188E7
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] From
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.From];
			}
			set
			{
				base.Fields[InboxRuleSchema.From] = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001A6FA File Offset: 0x000188FA
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001A711 File Offset: 0x00018911
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfFrom
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.ExceptIfFrom];
			}
			set
			{
				base.Fields[InboxRuleSchema.ExceptIfFrom] = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001A724 File Offset: 0x00018924
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0001A73B File Offset: 0x0001893B
		[Parameter(Mandatory = false)]
		public MessageClassificationIdParameter[] HasClassification
		{
			get
			{
				return (MessageClassificationIdParameter[])base.Fields[InboxRuleSchema.HasClassification];
			}
			set
			{
				base.Fields[InboxRuleSchema.HasClassification] = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001A74E File Offset: 0x0001894E
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x0001A765 File Offset: 0x00018965
		[Parameter(Mandatory = false)]
		public MessageClassificationIdParameter[] ExceptIfHasClassification
		{
			get
			{
				return (MessageClassificationIdParameter[])base.Fields[InboxRuleSchema.ExceptIfHasClassification];
			}
			set
			{
				base.Fields[InboxRuleSchema.ExceptIfHasClassification] = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001A778 File Offset: 0x00018978
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0001A78F File Offset: 0x0001898F
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] SentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.SentTo];
			}
			set
			{
				base.Fields[InboxRuleSchema.SentTo] = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001A7A2 File Offset: 0x000189A2
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0001A7B9 File Offset: 0x000189B9
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfSentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.ExceptIfSentTo];
			}
			set
			{
				base.Fields[InboxRuleSchema.ExceptIfSentTo] = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001A7CC File Offset: 0x000189CC
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0001A7E3 File Offset: 0x000189E3
		[Parameter(Mandatory = false)]
		public MailboxFolderIdParameter CopyToFolder
		{
			get
			{
				return (MailboxFolderIdParameter)base.Fields[InboxRuleSchema.CopyToFolder];
			}
			set
			{
				base.Fields[InboxRuleSchema.CopyToFolder] = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001A7F6 File Offset: 0x000189F6
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x0001A80D File Offset: 0x00018A0D
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ForwardAsAttachmentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.ForwardAsAttachmentTo];
			}
			set
			{
				base.Fields[InboxRuleSchema.ForwardAsAttachmentTo] = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001A820 File Offset: 0x00018A20
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0001A837 File Offset: 0x00018A37
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ForwardTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.ForwardTo];
			}
			set
			{
				base.Fields[InboxRuleSchema.ForwardTo] = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001A84A File Offset: 0x00018A4A
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x0001A861 File Offset: 0x00018A61
		[Parameter(Mandatory = false)]
		public MailboxFolderIdParameter MoveToFolder
		{
			get
			{
				return (MailboxFolderIdParameter)base.Fields[InboxRuleSchema.MoveToFolder];
			}
			set
			{
				base.Fields[InboxRuleSchema.MoveToFolder] = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001A874 File Offset: 0x00018A74
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0001A88B File Offset: 0x00018A8B
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] RedirectTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.RedirectTo];
			}
			set
			{
				base.Fields[InboxRuleSchema.RedirectTo] = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001A89E File Offset: 0x00018A9E
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x0001A8B5 File Offset: 0x00018AB5
		[Parameter(Mandatory = false)]
		public AggregationSubscriptionIdentity[] FromSubscription
		{
			get
			{
				return (AggregationSubscriptionIdentity[])base.Fields[InboxRuleSchema.FromSubscription];
			}
			set
			{
				base.Fields[InboxRuleSchema.FromSubscription] = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001A8C8 File Offset: 0x00018AC8
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0001A8DF File Offset: 0x00018ADF
		[Parameter(Mandatory = false)]
		public AggregationSubscriptionIdentity[] ExceptIfFromSubscription
		{
			get
			{
				return (AggregationSubscriptionIdentity[])base.Fields[InboxRuleSchema.ExceptIfFromSubscription];
			}
			set
			{
				base.Fields[InboxRuleSchema.ExceptIfFromSubscription] = value;
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001A8FA File Offset: 0x00018AFA
		protected override void InternalValidate()
		{
			base.InternalValidate();
			InboxRuleDataProvider.ValidateInboxRuleProperties(this.DataObject, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001A928 File Offset: 0x00018B28
		protected override void InternalProcessRecord()
		{
			if (this.AlwaysDeleteOutlookRulesBlob.IsPresent)
			{
				InboxRuleDataProvider inboxRuleDataProvider = (InboxRuleDataProvider)base.DataSession;
				inboxRuleDataProvider.SetAlwaysDeleteOutlookRulesBlob(new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			else if (!this.Force.IsPresent)
			{
				InboxRuleDataProvider inboxRuleDataProvider = (InboxRuleDataProvider)base.DataSession;
				inboxRuleDataProvider.ConfirmDeleteOutlookBlob = (() => base.ShouldContinue(Strings.WarningInboxRuleOutlookBlobExists));
			}
			if (this.DataObject.FlaggedForAction != null)
			{
				InboxRuleDataProvider.CheckFlaggedAction(this.DataObject.FlaggedForAction, InboxRuleSchema.FlaggedForAction.Name, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (this.DataObject.ExceptIfFlaggedForAction != null)
			{
				InboxRuleDataProvider.CheckFlaggedAction(this.DataObject.ExceptIfFlaggedForAction, InboxRuleSchema.ExceptIfFlaggedForAction.Name, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (this.DataObject.SendTextMessageNotificationTo.Count > 0)
			{
				SmsSqmDataPointHelper.AddNotificationConfigDataPoint(SmsSqmSession.Instance, this.adUser.Id, this.adUser.LegacyExchangeDN, SMSNotificationType.Email);
			}
			ManageInboxRule.ProcessRecord(new Action(base.InternalProcessRecord), new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError), this.Identity);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001AA58 File Offset: 0x00018C58
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
			this.adUser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
			if (this.Identity != null && this.Identity.InternalInboxRuleId == null)
			{
				this.Identity.InternalInboxRuleId = new InboxRuleId(this.adUser.Id, this.Identity.RawRuleName, this.Identity.RawRuleId);
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 323, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\InboxRule\\SetInboxRule.cs");
			base.VerifyIsWithinScopes(TaskHelper.UnderscopeSessionToOrganization(tenantOrRootOrgRecipientSession, this.adUser.OrganizationId, true), this.adUser, true, new DataAccessTask<InboxRule>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			InboxRuleDataProvider inboxRuleDataProvider = new InboxRuleDataProvider(base.SessionSettings, this.adUser, "Set-InboxRule");
			this.mailboxOwner = inboxRuleDataProvider.MailboxSession.MailboxOwner.ObjectId.ToString();
			return inboxRuleDataProvider;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001AC08 File Offset: 0x00018E08
		protected override IConfigurable PrepareDataObject()
		{
			InboxRule inboxRule = this.LoadInboxRule();
			this.PrepareDataObjectFromParameters(inboxRule);
			inboxRule.ValidateInterdependentParameters(new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			return inboxRule;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001AC38 File Offset: 0x00018E38
		private InboxRule LoadInboxRule()
		{
			InboxRule result;
			try
			{
				InboxRule inboxRule = (InboxRule)base.PrepareDataObject();
				inboxRule.Provider = (XsoMailboxDataProviderBase)base.DataSession;
				result = inboxRule;
			}
			catch (ObjectNotFoundException)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorInboxRuleDoesNotExist), ErrorCategory.InvalidOperation, this.Identity);
				result = null;
			}
			return result;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001AC98 File Offset: 0x00018E98
		private void PrepareDataObjectFromParameters(InboxRule inboxRule)
		{
			if (base.Fields.IsModified(InboxRuleSchema.From))
			{
				inboxRule.From = ManageInboxRule.ResolveRecipients(this.From, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ExceptIfFrom))
			{
				inboxRule.ExceptIfFrom = ManageInboxRule.ResolveRecipients(this.ExceptIfFrom, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.HasClassification))
			{
				inboxRule.HasClassification = ManageInboxRule.ResolveMessageClassifications(this.HasClassification, this.ConfigurationSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ExceptIfHasClassification))
			{
				inboxRule.ExceptIfHasClassification = ManageInboxRule.ResolveMessageClassifications(this.ExceptIfHasClassification, this.ConfigurationSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.SentTo))
			{
				inboxRule.SentTo = ManageInboxRule.ResolveRecipients(this.SentTo, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ExceptIfSentTo))
			{
				inboxRule.ExceptIfSentTo = ManageInboxRule.ResolveRecipients(this.ExceptIfSentTo, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.CopyToFolder))
			{
				inboxRule.CopyToFolder = ManageInboxRule.ResolveMailboxFolder(this.CopyToFolder, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADUser>), new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<MailboxFolder>), base.TenantGlobalCatalogSession, base.SessionSettings, this.adUser, (base.ExchangeRunspaceConfig == null) ? null : base.ExchangeRunspaceConfig.SecurityAccessToken, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ForwardAsAttachmentTo))
			{
				inboxRule.ForwardAsAttachmentTo = ManageInboxRule.ResolveRecipients(this.ForwardAsAttachmentTo, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ForwardTo))
			{
				inboxRule.ForwardTo = ManageInboxRule.ResolveRecipients(this.ForwardTo, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.MoveToFolder))
			{
				inboxRule.MoveToFolder = ManageInboxRule.ResolveMailboxFolder(this.MoveToFolder, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADUser>), new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<MailboxFolder>), base.TenantGlobalCatalogSession, base.SessionSettings, this.adUser, (base.ExchangeRunspaceConfig == null) ? null : base.ExchangeRunspaceConfig.SecurityAccessToken, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.RedirectTo))
			{
				inboxRule.RedirectTo = ManageInboxRule.ResolveRecipients(this.RedirectTo, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.FromSubscription))
			{
				inboxRule.FromSubscription = ManageInboxRule.ResolveSubscriptions(this.FromSubscription, this.adUser, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ExceptIfFromSubscription))
			{
				inboxRule.ExceptIfFromSubscription = ManageInboxRule.ResolveSubscriptions(this.ExceptIfFromSubscription, this.adUser, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001B01E File Offset: 0x0001921E
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001B031 File Offset: 0x00019231
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetInboxRule(this.Identity.ToString(), this.mailboxOwner);
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001B049 File Offset: 0x00019249
		protected override void InternalStateReset()
		{
			ManageInboxRule.CleanupInboxRuleDataProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001B05C File Offset: 0x0001925C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			ManageInboxRule.CleanupInboxRuleDataProvider(base.DataSession);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000184 RID: 388
		private string mailboxOwner;

		// Token: 0x04000185 RID: 389
		private ADUser adUser;
	}
}
