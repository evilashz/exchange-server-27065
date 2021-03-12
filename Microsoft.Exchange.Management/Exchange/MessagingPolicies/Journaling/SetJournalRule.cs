using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A23 RID: 2595
	[Cmdlet("Set", "JournalRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetJournalRule : SetSystemConfigurationObjectTask<RuleIdParameter, JournalRuleObject, TransportRule>
	{
		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x06005CE9 RID: 23785 RVA: 0x001877EC File Offset: 0x001859EC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetJournalrule(this.Identity.ToString());
			}
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x00187800 File Offset: 0x00185A00
		public SetJournalRule()
		{
			this.Name = string.Empty;
			this.Recipient = null;
			this.Scope = JournalRuleScope.Global;
			this.JournalEmailAddress = null;
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x06005CEB RID: 23787 RVA: 0x00187846 File Offset: 0x00185A46
		// (set) Token: 0x06005CEC RID: 23788 RVA: 0x0018785D File Offset: 0x00185A5D
		[Parameter(Mandatory = false)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x06005CED RID: 23789 RVA: 0x00187870 File Offset: 0x00185A70
		// (set) Token: 0x06005CEE RID: 23790 RVA: 0x00187887 File Offset: 0x00185A87
		[Parameter(Mandatory = false)]
		public SmtpAddress? Recipient
		{
			get
			{
				return (SmtpAddress?)base.Fields["RecipientProperty"];
			}
			set
			{
				base.Fields["RecipientProperty"] = value;
			}
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x06005CEF RID: 23791 RVA: 0x0018789F File Offset: 0x00185A9F
		// (set) Token: 0x06005CF0 RID: 23792 RVA: 0x001878B6 File Offset: 0x00185AB6
		[Parameter(Mandatory = false)]
		public RecipientIdParameter JournalEmailAddress
		{
			get
			{
				return (RecipientIdParameter)base.Fields["JournalEmailAddress"];
			}
			set
			{
				base.Fields["JournalEmailAddress"] = value;
			}
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x06005CF1 RID: 23793 RVA: 0x001878C9 File Offset: 0x00185AC9
		// (set) Token: 0x06005CF2 RID: 23794 RVA: 0x001878E0 File Offset: 0x00185AE0
		[Parameter(Mandatory = false)]
		public JournalRuleScope Scope
		{
			get
			{
				return (JournalRuleScope)base.Fields["Scope"];
			}
			set
			{
				base.Fields["Scope"] = value;
			}
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x06005CF3 RID: 23795 RVA: 0x001878F8 File Offset: 0x00185AF8
		// (set) Token: 0x06005CF4 RID: 23796 RVA: 0x0018791E File Offset: 0x00185B1E
		[Parameter(Mandatory = false)]
		public SwitchParameter LawfulInterception
		{
			get
			{
				return (SwitchParameter)(base.Fields["LawfulInterception"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["LawfulInterception"] = value;
			}
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x06005CF5 RID: 23797 RVA: 0x00187936 File Offset: 0x00185B36
		// (set) Token: 0x06005CF6 RID: 23798 RVA: 0x0018794D File Offset: 0x00185B4D
		[Parameter(Mandatory = false)]
		public bool FullReport
		{
			get
			{
				return (bool)base.Fields["FullReport"];
			}
			set
			{
				base.Fields["FullReport"] = value;
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x06005CF7 RID: 23799 RVA: 0x00187965 File Offset: 0x00185B65
		// (set) Token: 0x06005CF8 RID: 23800 RVA: 0x0018797C File Offset: 0x00185B7C
		[Parameter(Mandatory = false)]
		public DateTime? ExpiryDate
		{
			get
			{
				return (DateTime?)base.Fields["ExpiryDate"];
			}
			set
			{
				base.Fields["ExpiryDate"] = value;
			}
		}

		// Token: 0x06005CF9 RID: 23801 RVA: 0x00187994 File Offset: 0x00185B94
		internal override IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings.ConfigScopes == ConfigScopes.TenantLocal)
			{
				return base.CreateConfigurationSession(ADSessionSettings.RescopeToSubtree(sessionSettings));
			}
			return base.CreateConfigurationSession(sessionSettings);
		}

		// Token: 0x06005CFA RID: 23802 RVA: 0x001879B3 File Offset: 0x00185BB3
		internal override IRecipientSession CreateTenantGlobalCatalogSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings.ConfigScopes == ConfigScopes.TenantLocal)
			{
				return base.CreateTenantGlobalCatalogSession(ADSessionSettings.RescopeToSubtree(sessionSettings));
			}
			return base.CreateTenantGlobalCatalogSession(sessionSettings);
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x001879D4 File Offset: 0x00185BD4
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn("JournalingVersioned");
			}
			if (!Utils.ValidateGccJournalRuleParameters(this, false))
			{
				return;
			}
			if (this.ExpiryDate != null && this.ExpiryDate.Value.ToUniversalTime().Date < DateTime.UtcNow.Date)
			{
				base.WriteError(new InvalidOperationException(Strings.JournalingExpiryDateAlreadyExpired), ErrorCategory.InvalidOperation, null);
				return;
			}
			this.DataObject = (TransportRule)base.GetDataObject<TransportRule>(this.Identity, base.DataSession, this.RootId, base.OptionalIdentityData, null, null);
			if (!this.DataObject.OrganizationId.Equals(OrganizationId.ForestWideOrgId) && !((IDirectorySession)base.DataSession).SessionSettings.CurrentOrganizationId.Equals(this.DataObject.OrganizationId))
			{
				base.UnderscopeDataSession(this.DataObject.OrganizationId);
			}
			if (base.HasErrors)
			{
				return;
			}
			if (!Utils.IsChildOfRuleContainer(this.Identity, "JournalingVersioned"))
			{
				throw new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(RuleIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
			}
			JournalingRule journalingRule = null;
			try
			{
				journalingRule = (JournalingRule)JournalingRuleParser.Instance.GetRule(this.DataObject.Xml);
			}
			catch (ParserException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
				return;
			}
			if (journalingRule.GccRuleType != GccType.None && !this.LawfulInterception)
			{
				throw new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(RuleIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
			}
			if (journalingRule.IsTooAdvancedToParse)
			{
				base.WriteError(new InvalidOperationException(Strings.CannotModifyRuleDueToVersion(journalingRule.Name)), ErrorCategory.InvalidOperation, this.Name);
			}
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x00187C18 File Offset: 0x00185E18
		protected override void InternalProcessRecord()
		{
			if (base.Fields.IsModified("JournalEmailAddress") && this.JournalEmailAddress == null)
			{
				base.WriteError(new ArgumentException(Strings.NoRecipients, "JournalEmailAddress"), ErrorCategory.InvalidArgument, this.JournalEmailAddress);
				return;
			}
			if (!base.Fields.IsModified("Name") && !base.Fields.IsModified("Scope") && !base.Fields.IsModified("RecipientProperty") && !base.Fields.IsModified("JournalEmailAddress") && !base.Fields.IsModified("ExpiryDate") && !base.Fields.IsModified("FullReport"))
			{
				base.InternalProcessRecord();
				return;
			}
			ADJournalRuleStorageManager adjournalRuleStorageManager = new ADJournalRuleStorageManager("JournalingVersioned", base.DataSession);
			JournalingRule journalingRule;
			try
			{
				journalingRule = (JournalingRule)JournalingRuleParser.Instance.GetRule(this.DataObject.Xml);
			}
			catch (ParserException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
				return;
			}
			JournalRuleObject journalRuleObject = new JournalRuleObject();
			try
			{
				journalRuleObject.Deserialize(journalingRule);
				journalRuleObject.Name = this.DataObject.Name;
			}
			catch (RecipientInvalidException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, this.JournalEmailAddress);
				return;
			}
			catch (JournalRuleCorruptException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields.IsChanged("Name") || base.Fields.IsModified("Name"))
			{
				journalRuleObject.Name = this.Name;
			}
			SmtpAddress journalingReportNdrToSmtpAddress = JournalRuleObject.GetJournalingReportNdrToSmtpAddress(this.DataObject.OrganizationId, this.ConfigurationSession);
			if (base.Fields.IsChanged("RecipientProperty") || base.Fields.IsModified("RecipientProperty"))
			{
				if (this.Recipient != null && this.Recipient != null && this.Recipient.Value == journalingReportNdrToSmtpAddress)
				{
					base.WriteError(new InvalidOperationException(Strings.JournalingReportNdrToSameAsRecipient), ErrorCategory.InvalidOperation, null);
					return;
				}
				journalRuleObject.Recipient = this.Recipient;
			}
			if (!base.Fields.IsChanged("JournalEmailAddress"))
			{
				if (!base.Fields.IsModified("JournalEmailAddress"))
				{
					goto IL_2AE;
				}
			}
			try
			{
				bool isNonGccInDc = Datacenter.IsMultiTenancyEnabled() && !this.LawfulInterception;
				SmtpAddress smtpAddress;
				if (!JournalRuleObject.LookupAndCheckAllowedTypes(this.JournalEmailAddress, base.TenantGlobalCatalogSession, this.DataObject.OrganizationId, isNonGccInDc, out smtpAddress))
				{
					base.WriteError(new InvalidOperationException(Strings.JournalingToExternalOnly), ErrorCategory.InvalidOperation, null);
					return;
				}
				if (smtpAddress == journalingReportNdrToSmtpAddress)
				{
					this.WriteWarning(Strings.JournalingReportNdrToSameAsJournalEmailAddress);
				}
				journalRuleObject.JournalEmailAddress = smtpAddress;
			}
			catch (RecipientInvalidException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidArgument, "JournalEmailAddress");
				return;
			}
			IL_2AE:
			if (base.Fields.IsChanged("Scope") || base.Fields.IsModified("Scope"))
			{
				journalRuleObject.Scope = this.Scope;
			}
			if (base.Fields.IsChanged("ExpiryDate") || base.Fields.IsModified("ExpiryDate"))
			{
				if (this.ExpiryDate != null)
				{
					journalRuleObject.ExpiryDate = new DateTime?(this.ExpiryDate.Value.ToUniversalTime());
				}
				else
				{
					journalRuleObject.ExpiryDate = null;
				}
			}
			if (base.Fields.IsChanged("FullReport") || base.Fields.IsModified("FullReport"))
			{
				GccType gccRuleType = GccType.None;
				if (this.LawfulInterception)
				{
					if (this.FullReport)
					{
						gccRuleType = GccType.Full;
					}
					else
					{
						gccRuleType = GccType.Prtt;
					}
				}
				journalRuleObject.RuleType = JournalRuleObject.ConvertGccTypeToJournalRuleType(gccRuleType);
			}
			journalingRule = journalRuleObject.Serialize();
			this.DataObject.Xml = JournalingRuleSerializer.Instance.SaveRuleToString(journalingRule);
			if (!adjournalRuleStorageManager.CanRename((ADObjectId)this.DataObject.Identity, this.DataObject.Name, journalingRule.Name))
			{
				base.WriteError(new ArgumentException(Strings.RuleNameAlreadyExist, "Name"), ErrorCategory.InvalidArgument, this.Name);
				return;
			}
			if (Utils.Exchange12HubServersExist(this))
			{
				this.WriteWarning(Strings.SetRuleSyncAcrossDifferentVersionsNeeded);
			}
			this.DataObject.Name = journalingRule.Name;
			base.InternalProcessRecord();
			if (journalingReportNdrToSmtpAddress == SmtpAddress.NullReversePath)
			{
				this.WriteWarning(Strings.JournalingReportNdrToNotSet);
			}
			return;
		}

		// Token: 0x0400347A RID: 13434
		internal const string NameProperty = "Name";

		// Token: 0x0400347B RID: 13435
		internal const string ScopeProperty = "Scope";

		// Token: 0x0400347C RID: 13436
		internal const string RecipientProperty = "RecipientProperty";

		// Token: 0x0400347D RID: 13437
		internal const string JournalEmailAddressProperty = "JournalEmailAddress";

		// Token: 0x0400347E RID: 13438
		internal const string LawfulInterceptionProperty = "LawfulInterception";

		// Token: 0x0400347F RID: 13439
		internal const string FullReportProperty = "FullReport";

		// Token: 0x04003480 RID: 13440
		internal const string ExpiryDateProperty = "ExpiryDate";

		// Token: 0x04003481 RID: 13441
		internal const string OrganizationProperty = "Organization";
	}
}
