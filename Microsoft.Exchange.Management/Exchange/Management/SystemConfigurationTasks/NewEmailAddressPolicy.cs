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
	// Token: 0x02000B08 RID: 2824
	[Cmdlet("New", "EmailAddressPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "SMTPTemplateWithPrecannedFilter")]
	public sealed class NewEmailAddressPolicy : NewMultitenancySystemConfigurationObjectTask<EmailAddressPolicy>
	{
		// Token: 0x17001E77 RID: 7799
		// (get) Token: 0x06006458 RID: 25688 RVA: 0x001A2F0C File Offset: 0x001A110C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("AllTemplatesWithPrecannedFilter" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewEmailAddressPolicyAllTemplatesWithPrecannedFilter(base.Name.ToString(), this.IncludedRecipients.ToString(), base.FormatMultiValuedProperty(this.EnabledEmailAddressTemplates));
				}
				if ("AllTemplatesWithCustomFilter" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewEmailAddressPolicyAllTemplatesWithCustomFilter(base.Name.ToString(), this.RecipientFilter.ToString(), base.FormatMultiValuedProperty(this.EnabledEmailAddressTemplates));
				}
				if ("SMTPTemplateWithCustomFilter" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewEmailAddressPolicySMTPTemplateWithCustomFilter(base.Name.ToString(), this.RecipientFilter.ToString(), this.EnabledPrimarySMTPAddressTemplate.ToString());
				}
				return Strings.ConfirmationMessageNewEmailAddressPolicySMTPTemplateWithPrecannedFilter(base.Name.ToString(), this.IncludedRecipients.ToString(), this.EnabledPrimarySMTPAddressTemplate.ToString());
			}
		}

		// Token: 0x17001E78 RID: 7800
		// (get) Token: 0x06006459 RID: 25689 RVA: 0x001A2FFE File Offset: 0x001A11FE
		// (set) Token: 0x0600645A RID: 25690 RVA: 0x001A3018 File Offset: 0x001A1218
		[Parameter(Mandatory = true, ParameterSetName = "SMTPTemplateWithCustomFilter")]
		[Parameter(Mandatory = true, ParameterSetName = "AllTemplatesWithCustomFilter")]
		public string RecipientFilter
		{
			get
			{
				return (string)base.Fields["RecipientFilter"];
			}
			set
			{
				base.Fields["RecipientFilter"] = (value ?? string.Empty);
				MonadFilter monadFilter = new MonadFilter(value ?? string.Empty, this, ObjectSchema.GetInstance<ADRecipientProperties>());
				this.DataObject.SetRecipientFilter(monadFilter.InnerFilter);
			}
		}

		// Token: 0x17001E79 RID: 7801
		// (get) Token: 0x0600645B RID: 25691 RVA: 0x001A3066 File Offset: 0x001A1266
		// (set) Token: 0x0600645C RID: 25692 RVA: 0x001A3073 File Offset: 0x001A1273
		[Parameter(Mandatory = true, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = true, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public WellKnownRecipientType? IncludedRecipients
		{
			get
			{
				return this.DataObject.IncludedRecipients;
			}
			set
			{
				this.DataObject.IncludedRecipients = value;
			}
		}

		// Token: 0x17001E7A RID: 7802
		// (get) Token: 0x0600645D RID: 25693 RVA: 0x001A3081 File Offset: 0x001A1281
		// (set) Token: 0x0600645E RID: 25694 RVA: 0x001A308E File Offset: 0x001A128E
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalDepartment
		{
			get
			{
				return this.DataObject.ConditionalDepartment;
			}
			set
			{
				this.DataObject.ConditionalDepartment = value;
			}
		}

		// Token: 0x17001E7B RID: 7803
		// (get) Token: 0x0600645F RID: 25695 RVA: 0x001A309C File Offset: 0x001A129C
		// (set) Token: 0x06006460 RID: 25696 RVA: 0x001A30A9 File Offset: 0x001A12A9
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCompany
		{
			get
			{
				return this.DataObject.ConditionalCompany;
			}
			set
			{
				this.DataObject.ConditionalCompany = value;
			}
		}

		// Token: 0x17001E7C RID: 7804
		// (get) Token: 0x06006461 RID: 25697 RVA: 0x001A30B7 File Offset: 0x001A12B7
		// (set) Token: 0x06006462 RID: 25698 RVA: 0x001A30C4 File Offset: 0x001A12C4
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalStateOrProvince
		{
			get
			{
				return this.DataObject.ConditionalStateOrProvince;
			}
			set
			{
				this.DataObject.ConditionalStateOrProvince = value;
			}
		}

		// Token: 0x17001E7D RID: 7805
		// (get) Token: 0x06006463 RID: 25699 RVA: 0x001A30D2 File Offset: 0x001A12D2
		// (set) Token: 0x06006464 RID: 25700 RVA: 0x001A30E9 File Offset: 0x001A12E9
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

		// Token: 0x17001E7E RID: 7806
		// (get) Token: 0x06006465 RID: 25701 RVA: 0x001A30FC File Offset: 0x001A12FC
		// (set) Token: 0x06006466 RID: 25702 RVA: 0x001A3109 File Offset: 0x001A1309
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute1
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute1;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute1 = value;
			}
		}

		// Token: 0x17001E7F RID: 7807
		// (get) Token: 0x06006467 RID: 25703 RVA: 0x001A3117 File Offset: 0x001A1317
		// (set) Token: 0x06006468 RID: 25704 RVA: 0x001A3124 File Offset: 0x001A1324
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute2
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute2;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute2 = value;
			}
		}

		// Token: 0x17001E80 RID: 7808
		// (get) Token: 0x06006469 RID: 25705 RVA: 0x001A3132 File Offset: 0x001A1332
		// (set) Token: 0x0600646A RID: 25706 RVA: 0x001A313F File Offset: 0x001A133F
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute3
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute3;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute3 = value;
			}
		}

		// Token: 0x17001E81 RID: 7809
		// (get) Token: 0x0600646B RID: 25707 RVA: 0x001A314D File Offset: 0x001A134D
		// (set) Token: 0x0600646C RID: 25708 RVA: 0x001A315A File Offset: 0x001A135A
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute4
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute4;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute4 = value;
			}
		}

		// Token: 0x17001E82 RID: 7810
		// (get) Token: 0x0600646D RID: 25709 RVA: 0x001A3168 File Offset: 0x001A1368
		// (set) Token: 0x0600646E RID: 25710 RVA: 0x001A3175 File Offset: 0x001A1375
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute5
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute5;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute5 = value;
			}
		}

		// Token: 0x17001E83 RID: 7811
		// (get) Token: 0x0600646F RID: 25711 RVA: 0x001A3183 File Offset: 0x001A1383
		// (set) Token: 0x06006470 RID: 25712 RVA: 0x001A3190 File Offset: 0x001A1390
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute6
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute6;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute6 = value;
			}
		}

		// Token: 0x17001E84 RID: 7812
		// (get) Token: 0x06006471 RID: 25713 RVA: 0x001A319E File Offset: 0x001A139E
		// (set) Token: 0x06006472 RID: 25714 RVA: 0x001A31AB File Offset: 0x001A13AB
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute7
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute7;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute7 = value;
			}
		}

		// Token: 0x17001E85 RID: 7813
		// (get) Token: 0x06006473 RID: 25715 RVA: 0x001A31B9 File Offset: 0x001A13B9
		// (set) Token: 0x06006474 RID: 25716 RVA: 0x001A31C6 File Offset: 0x001A13C6
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute8
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute8;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute8 = value;
			}
		}

		// Token: 0x17001E86 RID: 7814
		// (get) Token: 0x06006475 RID: 25717 RVA: 0x001A31D4 File Offset: 0x001A13D4
		// (set) Token: 0x06006476 RID: 25718 RVA: 0x001A31E1 File Offset: 0x001A13E1
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute9
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute9;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute9 = value;
			}
		}

		// Token: 0x17001E87 RID: 7815
		// (get) Token: 0x06006477 RID: 25719 RVA: 0x001A31EF File Offset: 0x001A13EF
		// (set) Token: 0x06006478 RID: 25720 RVA: 0x001A31FC File Offset: 0x001A13FC
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute10
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute10;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute10 = value;
			}
		}

		// Token: 0x17001E88 RID: 7816
		// (get) Token: 0x06006479 RID: 25721 RVA: 0x001A320A File Offset: 0x001A140A
		// (set) Token: 0x0600647A RID: 25722 RVA: 0x001A3217 File Offset: 0x001A1417
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute11
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute11;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute11 = value;
			}
		}

		// Token: 0x17001E89 RID: 7817
		// (get) Token: 0x0600647B RID: 25723 RVA: 0x001A3225 File Offset: 0x001A1425
		// (set) Token: 0x0600647C RID: 25724 RVA: 0x001A3232 File Offset: 0x001A1432
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute12
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute12;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute12 = value;
			}
		}

		// Token: 0x17001E8A RID: 7818
		// (get) Token: 0x0600647D RID: 25725 RVA: 0x001A3240 File Offset: 0x001A1440
		// (set) Token: 0x0600647E RID: 25726 RVA: 0x001A324D File Offset: 0x001A144D
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute13
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute13;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute13 = value;
			}
		}

		// Token: 0x17001E8B RID: 7819
		// (get) Token: 0x0600647F RID: 25727 RVA: 0x001A325B File Offset: 0x001A145B
		// (set) Token: 0x06006480 RID: 25728 RVA: 0x001A3268 File Offset: 0x001A1468
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute14
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute14;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute14 = value;
			}
		}

		// Token: 0x17001E8C RID: 7820
		// (get) Token: 0x06006481 RID: 25729 RVA: 0x001A3276 File Offset: 0x001A1476
		// (set) Token: 0x06006482 RID: 25730 RVA: 0x001A3283 File Offset: 0x001A1483
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute15
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute15;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute15 = value;
			}
		}

		// Token: 0x17001E8D RID: 7821
		// (get) Token: 0x06006483 RID: 25731 RVA: 0x001A3291 File Offset: 0x001A1491
		// (set) Token: 0x06006484 RID: 25732 RVA: 0x001A329E File Offset: 0x001A149E
		[Parameter(Mandatory = true, ParameterSetName = "SMTPTemplateWithPrecannedFilter")]
		[Parameter(Mandatory = true, ParameterSetName = "SMTPTemplateWithCustomFilter")]
		[ValidateNotNullOrEmpty]
		public string EnabledPrimarySMTPAddressTemplate
		{
			get
			{
				return this.DataObject.EnabledPrimarySMTPAddressTemplate;
			}
			set
			{
				this.DataObject.EnabledPrimarySMTPAddressTemplate = value;
			}
		}

		// Token: 0x17001E8E RID: 7822
		// (get) Token: 0x06006485 RID: 25733 RVA: 0x001A32AC File Offset: 0x001A14AC
		// (set) Token: 0x06006486 RID: 25734 RVA: 0x001A32B9 File Offset: 0x001A14B9
		[Parameter(Mandatory = true, ParameterSetName = "AllTemplatesWithCustomFilter")]
		[Parameter(Mandatory = true, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		public ProxyAddressTemplateCollection EnabledEmailAddressTemplates
		{
			get
			{
				return this.DataObject.EnabledEmailAddressTemplates;
			}
			set
			{
				this.DataObject.EnabledEmailAddressTemplates = value;
			}
		}

		// Token: 0x17001E8F RID: 7823
		// (get) Token: 0x06006487 RID: 25735 RVA: 0x001A32C7 File Offset: 0x001A14C7
		// (set) Token: 0x06006488 RID: 25736 RVA: 0x001A32D4 File Offset: 0x001A14D4
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithPrecannedFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "AllTemplatesWithCustomFilter")]
		public ProxyAddressTemplateCollection DisabledEmailAddressTemplates
		{
			get
			{
				return this.DataObject.DisabledEmailAddressTemplates;
			}
			set
			{
				this.DataObject.DisabledEmailAddressTemplates = value;
			}
		}

		// Token: 0x17001E90 RID: 7824
		// (get) Token: 0x06006489 RID: 25737 RVA: 0x001A32E2 File Offset: 0x001A14E2
		// (set) Token: 0x0600648A RID: 25738 RVA: 0x001A32EF File Offset: 0x001A14EF
		[Parameter]
		public EmailAddressPolicyPriority Priority
		{
			get
			{
				return this.DataObject.Priority;
			}
			set
			{
				this.DataObject.Priority = value;
			}
		}

		// Token: 0x0600648B RID: 25739 RVA: 0x001A32FD File Offset: 0x001A14FD
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.domainValidator = new UpdateEmailAddressPolicy.WritableDomainValidator(base.OrganizationId ?? OrganizationId.ForestWideOrgId, this.ConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x0600648C RID: 25740 RVA: 0x001A333C File Offset: 0x001A153C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			this.containerId = base.CurrentOrgContainerId.GetDescendantId(EmailAddressPolicy.RdnEapContainerToOrganization);
			this.DataObject = (EmailAddressPolicy)base.PrepareDataObject();
			if (!base.HasErrors)
			{
				this.DataObject.SetId(this.containerId.GetChildId(base.Name));
				if (!this.DataObject.IsModified(EmailAddressPolicySchema.Priority))
				{
					this.DataObject.Priority = EmailAddressPolicyPriority.Lowest;
				}
				if (!base.HasErrors && (this.DataObject.Priority != 0 || !this.DataObject.IsModified(EmailAddressPolicySchema.Priority)))
				{
					UpdateEmailAddressPolicy.PreparePriorityOfEapObjects(base.OrganizationId ?? OrganizationId.ForestWideOrgId, this.DataObject, base.DataSession, new TaskExtendedErrorLoggingDelegate(this.WriteError), out this.affectedPolicies, out this.affectedPoliciesOriginalPriority);
				}
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
				else if (this.DataObject.RecipientContainer != null)
				{
					organizationalUnitIdParameter = new OrganizationalUnitIdParameter(this.DataObject.RecipientContainer);
				}
				if (organizationalUnitIdParameter != null)
				{
					if (base.GlobalConfigSession.IsInPreE14InteropMode())
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorCannotSetRecipientContainer), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					}
					this.DataObject.RecipientContainer = NewAddressBookBase.GetRecipientContainer(organizationalUnitIdParameter, (IConfigurationSession)base.DataSession, base.OrganizationId ?? OrganizationId.ForestWideOrgId, new NewAddressBookBase.GetUniqueObject(base.GetDataObject<ExchangeOrganizationalUnit>), new Task.ErrorLoggerDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				}
			}
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x0600648D RID: 25741 RVA: 0x001A350C File Offset: 0x001A170C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			RecipientFilterHelper.StampE2003FilterMetadata(this.DataObject, this.DataObject.LdapRecipientFilter, EmailAddressPolicySchema.PurportedSearchUI);
			this.domainValidator.Validate(this.DataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x001A3560 File Offset: 0x001A1760
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				bool flag = this.affectedPolicies != null && this.affectedPolicies.Length > 0;
				List<EmailAddressPolicy> list = new List<EmailAddressPolicy>();
				try
				{
					for (int i = 0; i < this.affectedPolicies.Length; i++)
					{
						if (flag)
						{
							base.WriteProgress(Strings.ProgressEmailAddressPolicyPreparingPriority, Strings.ProgressEmailAddressPolicyAdjustingPriority(this.affectedPolicies[i].Identity.ToString()), i * 99 / this.affectedPolicies.Length + 1);
						}
						base.DataSession.Save(this.affectedPolicies[i]);
						this.affectedPolicies[i].Priority = this.affectedPoliciesOriginalPriority[i];
						list.Add(this.affectedPolicies[i]);
					}
					list.Clear();
				}
				finally
				{
					if (list.Count != 0)
					{
						try
						{
							base.DataSession.Delete(this.DataObject);
						}
						catch (DataSourceTransientException)
						{
							this.WriteWarning(Strings.VerboseFailedToDeleteEapWhileRollingBack(this.DataObject.Id.ToString()));
						}
					}
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
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003625 RID: 13861
		private ADObjectId containerId;

		// Token: 0x04003626 RID: 13862
		private EmailAddressPolicy[] affectedPolicies;

		// Token: 0x04003627 RID: 13863
		private EmailAddressPolicyPriority[] affectedPoliciesOriginalPriority;

		// Token: 0x04003628 RID: 13864
		private UpdateEmailAddressPolicy.WritableDomainValidator domainValidator;
	}
}
