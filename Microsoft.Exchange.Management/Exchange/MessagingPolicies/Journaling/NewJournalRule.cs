using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A1A RID: 2586
	[Cmdlet("new", "journalrule", SupportsShouldProcess = true)]
	public class NewJournalRule : NewMultitenancySystemConfigurationObjectTask<TransportRule>
	{
		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x06005CB7 RID: 23735 RVA: 0x00186980 File Offset: 0x00184B80
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewJournalrule(base.Name.ToString(), this.JournalEmailAddress.ToString());
			}
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x001869A0 File Offset: 0x00184BA0
		public NewJournalRule()
		{
			this.Scope = JournalRuleScope.Global;
			this.Recipient = null;
			this.Enabled = false;
			this.ExpiryDate = null;
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x17001BCE RID: 7118
		// (get) Token: 0x06005CB9 RID: 23737 RVA: 0x001869EA File Offset: 0x00184BEA
		// (set) Token: 0x06005CBA RID: 23738 RVA: 0x00186A01 File Offset: 0x00184C01
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

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x06005CBB RID: 23739 RVA: 0x00186A19 File Offset: 0x00184C19
		// (set) Token: 0x06005CBC RID: 23740 RVA: 0x00186A30 File Offset: 0x00184C30
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

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x06005CBD RID: 23741 RVA: 0x00186A48 File Offset: 0x00184C48
		// (set) Token: 0x06005CBE RID: 23742 RVA: 0x00186A5F File Offset: 0x00184C5F
		[Parameter(Mandatory = true)]
		public RecipientIdParameter JournalEmailAddress
		{
			get
			{
				return (RecipientIdParameter)base.Fields[JournalRuleObjectSchema.JournalEmailAddress];
			}
			set
			{
				base.Fields[JournalRuleObjectSchema.JournalEmailAddress] = value;
			}
		}

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x06005CBF RID: 23743 RVA: 0x00186A72 File Offset: 0x00184C72
		// (set) Token: 0x06005CC0 RID: 23744 RVA: 0x00186A89 File Offset: 0x00184C89
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields[JournalRuleObjectSchema.Enabled];
			}
			set
			{
				base.Fields[JournalRuleObjectSchema.Enabled] = value;
			}
		}

		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x06005CC1 RID: 23745 RVA: 0x00186AA1 File Offset: 0x00184CA1
		// (set) Token: 0x06005CC2 RID: 23746 RVA: 0x00186AC7 File Offset: 0x00184CC7
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

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x06005CC3 RID: 23747 RVA: 0x00186ADF File Offset: 0x00184CDF
		// (set) Token: 0x06005CC4 RID: 23748 RVA: 0x00186AF6 File Offset: 0x00184CF6
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

		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x06005CC5 RID: 23749 RVA: 0x00186B0E File Offset: 0x00184D0E
		// (set) Token: 0x06005CC6 RID: 23750 RVA: 0x00186B25 File Offset: 0x00184D25
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

		// Token: 0x06005CC7 RID: 23751 RVA: 0x00186B40 File Offset: 0x00184D40
		protected override void InternalValidate()
		{
			this.DataObject = (TransportRule)this.PrepareDataObject();
			if (!Utils.ValidateGccJournalRuleParameters(this, true))
			{
				return;
			}
			if (this.ExpiryDate != null && this.ExpiryDate.Value.ToUniversalTime().Date < DateTime.UtcNow.Date)
			{
				base.WriteError(new InvalidOperationException(Strings.JournalingExpiryDateAlreadyExpired), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x00186BC4 File Offset: 0x00184DC4
		protected override void InternalProcessRecord()
		{
			ADJournalRuleStorageManager adjournalRuleStorageManager = null;
			try
			{
				adjournalRuleStorageManager = new ADJournalRuleStorageManager("JournalingVersioned", base.DataSession);
			}
			catch (RuleCollectionNotInAdException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
				return;
			}
			int num = 0;
			TransportRule transportRule = null;
			SmtpAddress journalingReportNdrToSmtpAddress = JournalRuleObject.GetJournalingReportNdrToSmtpAddress(this.ResolveCurrentOrganization(), this.ConfigurationSession);
			JournalRuleObject journalRuleObject;
			try
			{
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled && journalingReportNdrToSmtpAddress == SmtpAddress.NullReversePath)
				{
					base.WriteError(new InvalidOperationException(Strings.JournalNdrMailboxCannotBeNull), ErrorCategory.InvalidOperation, null);
				}
				bool flag = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled && !this.LawfulInterception;
				SmtpAddress smtpAddress;
				if (!JournalRuleObject.LookupAndCheckAllowedTypes(this.JournalEmailAddress, base.TenantGlobalCatalogSession, base.SessionSettings.CurrentOrganizationId, flag, out smtpAddress))
				{
					base.WriteError(new InvalidOperationException(Strings.JournalingToExternalOnly), ErrorCategory.InvalidOperation, null);
					return;
				}
				if (smtpAddress == journalingReportNdrToSmtpAddress)
				{
					this.WriteWarning(Strings.JournalingReportNdrToSameAsJournalEmailAddress);
				}
				if (flag)
				{
					adjournalRuleStorageManager.LoadRuleCollection();
					if (adjournalRuleStorageManager.Count >= 10)
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorTooManyJournalRules(10)), ErrorCategory.InvalidOperation, null);
						return;
					}
				}
				if (this.Recipient != null && this.Recipient != null && this.Recipient.Value == journalingReportNdrToSmtpAddress)
				{
					base.WriteError(new InvalidOperationException(Strings.JournalingReportNdrToSameAsRecipient), ErrorCategory.InvalidOperation, null);
					return;
				}
				GccType gccRuleType = GccType.None;
				if (this.LawfulInterception)
				{
					this.ValidateLawfulInterceptionTenantConfiguration();
					if ((base.Fields.IsChanged("FullReport") || base.Fields.IsModified("FullReport")) && this.FullReport)
					{
						gccRuleType = GccType.Full;
					}
					else
					{
						gccRuleType = GccType.Prtt;
					}
				}
				DateTime? expiryDate = null;
				if (this.ExpiryDate != null)
				{
					expiryDate = new DateTime?(this.ExpiryDate.Value.ToUniversalTime());
				}
				journalRuleObject = new JournalRuleObject(base.Name, this.Enabled, this.Recipient, smtpAddress, this.Scope, expiryDate, gccRuleType);
			}
			catch (DataValidationException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, base.Name);
				return;
			}
			catch (RecipientInvalidException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, this.JournalEmailAddress);
				return;
			}
			try
			{
				JournalingRule rule = journalRuleObject.Serialize();
				adjournalRuleStorageManager.NewRule(rule, this.ResolveCurrentOrganization(), ref num, out transportRule);
			}
			catch (RulesValidationException)
			{
				base.WriteError(new ArgumentException(Strings.RuleNameAlreadyExists, "Name"), ErrorCategory.InvalidArgument, base.Name);
				return;
			}
			catch (ParserException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidData, null);
				return;
			}
			if (Utils.Exchange12HubServersExist(this))
			{
				this.WriteWarning(Strings.NewRuleSyncAcrossDifferentVersionsNeeded);
			}
			journalRuleObject.SetTransportRule(transportRule);
			if (journalingReportNdrToSmtpAddress == SmtpAddress.NullReversePath)
			{
				this.WriteWarning(Strings.JournalingReportNdrToNotSet);
			}
			base.WriteObject(journalRuleObject);
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x00186F40 File Offset: 0x00185140
		private void ValidateLawfulInterceptionTenantConfiguration()
		{
			string config = JournalConfigSchema.Configuration.GetConfig<string>("LegalInterceptTenantName");
			if (string.IsNullOrEmpty(config))
			{
				base.WriteError(new InvalidOperationException(Strings.JournalingParameterErrorGccTenantSettingNotExist), ErrorCategory.ObjectNotFound, null);
			}
			Guid lawfulInterceptTenantGuid = ADJournalRuleStorageManager.GetLawfulInterceptTenantGuid(config);
			if (lawfulInterceptTenantGuid == Guid.Empty)
			{
				base.WriteError(new InvalidOperationException(Strings.JournalingParameterErrorGccTenantNotFound(config)), ErrorCategory.ObjectNotFound, null);
			}
		}

		// Token: 0x04003474 RID: 13428
		internal const int DatacenterMaxJournalRules = 10;
	}
}
