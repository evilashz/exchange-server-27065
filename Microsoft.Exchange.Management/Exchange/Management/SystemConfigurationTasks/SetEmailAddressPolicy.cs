using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B0A RID: 2826
	[Cmdlet("Set", "EmailAddressPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetEmailAddressPolicy : SetSystemConfigurationObjectTask<EmailAddressPolicyIdParameter, EmailAddressPolicy>
	{
		// Token: 0x17001E92 RID: 7826
		// (get) Token: 0x06006495 RID: 25749 RVA: 0x001A3B50 File Offset: 0x001A1D50
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetEmailAddressPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17001E93 RID: 7827
		// (get) Token: 0x06006496 RID: 25750 RVA: 0x001A3B62 File Offset: 0x001A1D62
		// (set) Token: 0x06006497 RID: 25751 RVA: 0x001A3B7C File Offset: 0x001A1D7C
		[Parameter]
		public string RecipientFilter
		{
			get
			{
				return (string)base.Fields[EmailAddressPolicySchema.RecipientFilter];
			}
			set
			{
				base.Fields[EmailAddressPolicySchema.RecipientFilter] = (value ?? string.Empty);
				MonadFilter monadFilter = new MonadFilter(value ?? string.Empty, this, ObjectSchema.GetInstance<ADRecipientProperties>());
				this.innerFilter = monadFilter.InnerFilter;
			}
		}

		// Token: 0x17001E94 RID: 7828
		// (get) Token: 0x06006498 RID: 25752 RVA: 0x001A3BC5 File Offset: 0x001A1DC5
		// (set) Token: 0x06006499 RID: 25753 RVA: 0x001A3BDC File Offset: 0x001A1DDC
		[Parameter]
		public OrganizationalUnitIdParameter RecipientContainer
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields["RecipientContainer"];
			}
			set
			{
				base.Fields["RecipientContainer"] = value;
			}
		}

		// Token: 0x17001E95 RID: 7829
		// (get) Token: 0x0600649A RID: 25754 RVA: 0x001A3BEF File Offset: 0x001A1DEF
		// (set) Token: 0x0600649B RID: 25755 RVA: 0x001A3C15 File Offset: 0x001A1E15
		[Parameter]
		public SwitchParameter ForceUpgrade
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceUpgrade"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForceUpgrade"] = value;
			}
		}

		// Token: 0x17001E96 RID: 7830
		// (get) Token: 0x0600649C RID: 25756 RVA: 0x001A3C2D File Offset: 0x001A1E2D
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x001A3C30 File Offset: 0x001A1E30
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return base.Fields.IsModified(EmailAddressPolicySchema.RecipientFilter) || RecipientFilterHelper.IsRecipientFilterPropertiesModified(adObject, false);
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x001A3C50 File Offset: 0x001A1E50
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			EmailAddressPolicy emailAddressPolicy = (EmailAddressPolicy)this.GetDynamicParameters();
			if (base.Fields.IsModified(EmailAddressPolicySchema.RecipientFilter) && RecipientFilterHelper.IsRecipientFilterPropertiesModified(emailAddressPolicy, false))
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorBothCustomAndPrecannedFilterSpecified, null), ErrorCategory.InvalidArgument, null);
			}
			if (emailAddressPolicy.IsModified(EmailAddressPolicySchema.EnabledPrimarySMTPAddressTemplate) && emailAddressPolicy.IsModified(EmailAddressPolicySchema.EnabledEmailAddressTemplates))
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorEnabledPrimarySmtpAndEmailAddressTemplatesSpecified, null), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x001A3CE0 File Offset: 0x001A1EE0
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			EmailAddressPolicy emailAddressPolicy = (EmailAddressPolicy)dataObject;
			bool flag = EmailAddressPolicyPriority.Lowest == emailAddressPolicy.Priority;
			if (flag)
			{
				if (this.Instance.IsChanged(ADObjectSchema.RawName) || ((EmailAddressPolicy)this.GetDynamicParameters()).IsChanged(ADObjectSchema.Name) || this.Instance.IsChanged(EmailAddressPolicySchema.Priority) || this.Instance.IsChanged(EmailAddressPolicySchema.RecipientContainer) || ((EmailAddressPolicy)this.GetDynamicParameters()).IsChanged(EmailAddressPolicySchema.RecipientContainer) || base.Fields.IsModified("RecipientContainer"))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorInvalidOperationOnLowestEap(dataObject.Identity.ToString())), ErrorCategory.InvalidOperation, dataObject.Identity);
				}
				if (!emailAddressPolicy.ExchangeVersion.IsOlderThan(EmailAddressPolicySchema.RecipientFilter.VersionAdded) && (base.Fields.IsModified(EmailAddressPolicySchema.RecipientFilter) || RecipientFilterHelper.IsRecipientFilterPropertiesModified((ADObject)this.GetDynamicParameters(), false) || RecipientFilterHelper.IsRecipientFilterPropertiesModified(this.Instance, true)))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorInvalidOperationOnLowestEap(dataObject.Identity.ToString())), ErrorCategory.InvalidOperation, dataObject.Identity);
				}
			}
			base.StampChangesOn(dataObject);
			if (base.Fields.IsModified(EmailAddressPolicySchema.RecipientFilter))
			{
				emailAddressPolicy.SetRecipientFilter(this.innerFilter);
			}
			if (flag && emailAddressPolicy.IsChanged(EmailAddressPolicySchema.LdapRecipientFilter) && !string.Equals(emailAddressPolicy.LdapRecipientFilter, LdapFilterBuilder.LdapFilterFromQueryFilter(EmailAddressPolicy.RecipientFilterForDefaultPolicy), StringComparison.OrdinalIgnoreCase))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorInvalidFilterForLowestEap(this.Identity.ToString(), emailAddressPolicy.RecipientFilter)), ErrorCategory.InvalidOperation, dataObject.Identity);
			}
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x001A3E94 File Offset: 0x001A2094
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			OrganizationId organizationId = this.DataObject.OrganizationId;
			OrganizationalUnitIdParameter organizationalUnitIdParameter = null;
			if (base.Fields.IsModified("RecipientContainer"))
			{
				if (this.RecipientContainer == null)
				{
					this.DataObject.RecipientContainer = null;
				}
				else
				{
					organizationalUnitIdParameter = this.RecipientContainer;
				}
			}
			else if (this.DataObject.IsModified(AddressBookBaseSchema.RecipientContainer) && this.DataObject.RecipientContainer != null)
			{
				organizationalUnitIdParameter = new OrganizationalUnitIdParameter(this.DataObject.RecipientContainer);
			}
			if (organizationalUnitIdParameter != null)
			{
				if (base.GlobalConfigSession.IsInPreE14InteropMode())
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCannotSetRecipientContainer), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				}
				this.DataObject.RecipientContainer = NewAddressBookBase.GetRecipientContainer(organizationalUnitIdParameter, (IConfigurationSession)base.DataSession, organizationId, new NewAddressBookBase.GetUniqueObject(base.GetDataObject<ExchangeOrganizationalUnit>), new Task.ErrorLoggerDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			if (this.IsObjectStateChanged() && this.DataObject.HasMailboxManagerSetting)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCanNotUpgradePolicyWithMailboxSetting(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Id);
			}
			if (this.IsObjectStateChanged() && this.DataObject.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2007))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorObjectNotManagableFromCurrentConsole(this.Identity.ToString(), this.DataObject.ExchangeVersion.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (!base.HasErrors)
			{
				if (this.DataObject.IsChanged(EmailAddressPolicySchema.Priority) && this.DataObject.Priority != 0)
				{
					UpdateEmailAddressPolicy.PreparePriorityOfEapObjects(organizationId, this.DataObject, base.DataSession, new TaskExtendedErrorLoggingDelegate(this.WriteError), out this.affectedPolicies, out this.affectedPoliciesOriginalPriority);
				}
				if (!base.HasErrors && (this.DataObject.IsChanged(EmailAddressPolicySchema.RecipientFilter) || this.DataObject.IsChanged(EmailAddressPolicySchema.Priority) || this.DataObject.IsChanged(EmailAddressPolicySchema.RawEnabledEmailAddressTemplates) || this.DataObject.IsChanged(EmailAddressPolicySchema.DisabledEmailAddressTemplates) || this.DataObject.IsChanged(EmailAddressPolicySchema.NonAuthoritativeDomains) || this.DataObject.IsChanged(EmailAddressPolicySchema.RecipientContainer)))
				{
					this.DataObject[EmailAddressPolicySchema.RecipientFilterApplied] = false;
				}
			}
			if (this.domainValidator == null || !this.domainValidator.OrganizationId.Equals(organizationId))
			{
				this.domainValidator = new UpdateEmailAddressPolicy.WritableDomainValidator(organizationId, this.ConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			this.domainValidator.Validate(this.DataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x001A4160 File Offset: 0x001A2360
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (RecipientFilterHelper.FixExchange12RecipientFilterMetadata(this.DataObject, ADObjectSchema.ExchangeVersion, EmailAddressPolicySchema.PurportedSearchUI, EmailAddressPolicySchema.RecipientFilterMetadata, this.DataObject.LdapRecipientFilter))
			{
				base.WriteVerbose(Strings.WarningFixTheInvalidRecipientFilterMetadata(this.Identity.ToString()));
			}
			bool flag = this.affectedPolicies != null && this.affectedPolicies.Length > 0;
			List<EmailAddressPolicy> list = new List<EmailAddressPolicy>();
			try
			{
				if (!base.IsUpgrading || this.ForceUpgrade || base.ShouldContinue(Strings.ContinueUpgradeObjectVersion(this.DataObject.Name)))
				{
					if (this.DataObject.IsChanged(EmailAddressPolicySchema.Priority))
					{
						for (int i = 0; i < this.affectedPolicies.Length; i++)
						{
							if (flag)
							{
								base.WriteProgress(Strings.ProgressEmailAddressPolicyPreparingPriority, Strings.ProgressEmailAddressPolicyAdjustingPriority(this.affectedPolicies[i].Identity.ToString()), i * 99 / this.affectedPolicies.Length + 1);
							}
							bool recipientFilterApplied = this.affectedPolicies[i].RecipientFilterApplied;
							if (!this.affectedPolicies[i].ExchangeVersion.IsOlderThan(EmailAddressPolicySchema.RecipientFilterApplied.VersionAdded))
							{
								this.affectedPolicies[i][EmailAddressPolicySchema.RecipientFilterApplied] = false;
							}
							base.DataSession.Save(this.affectedPolicies[i]);
							if (!this.affectedPolicies[i].ExchangeVersion.IsOlderThan(EmailAddressPolicySchema.RecipientFilterApplied.VersionAdded))
							{
								this.affectedPolicies[i][EmailAddressPolicySchema.RecipientFilterApplied] = recipientFilterApplied;
							}
							this.affectedPolicies[i].Priority = this.affectedPoliciesOriginalPriority[i];
							list.Add(this.affectedPolicies[i]);
						}
					}
					base.InternalProcessRecord();
					if (!base.HasErrors)
					{
						list.Clear();
					}
				}
			}
			finally
			{
				for (int j = 0; j < list.Count; j++)
				{
					EmailAddressPolicy emailAddressPolicy = list[j];
					try
					{
						if (flag)
						{
							base.WriteProgress(Strings.ProgressEmailAddressPolicyPreparingPriority, Strings.ProgressEmailAddressPolicyRollingBackPriority(emailAddressPolicy.Identity.ToString()), j * 99 / list.Count + 1);
						}
						base.DataSession.Save(emailAddressPolicy);
					}
					catch (DataSourceTransientException)
					{
						this.WriteWarning(Strings.VerboseFailedToRollbackPriority(emailAddressPolicy.Id.ToString()));
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400362B RID: 13867
		private EmailAddressPolicy[] affectedPolicies;

		// Token: 0x0400362C RID: 13868
		private EmailAddressPolicyPriority[] affectedPoliciesOriginalPriority;

		// Token: 0x0400362D RID: 13869
		private QueryFilter innerFilter;

		// Token: 0x0400362E RID: 13870
		private UpdateEmailAddressPolicy.WritableDomainValidator domainValidator;
	}
}
