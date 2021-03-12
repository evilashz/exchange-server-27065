using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000796 RID: 1942
	[Serializable]
	public class RecipientTemplateProvisioningPolicy : TemplateProvisioningPolicy
	{
		// Token: 0x17002295 RID: 8853
		// (get) Token: 0x060060A4 RID: 24740 RVA: 0x00148578 File Offset: 0x00146778
		internal override ADObjectSchema Schema
		{
			get
			{
				return RecipientTemplateProvisioningPolicy.schema;
			}
		}

		// Token: 0x17002296 RID: 8854
		// (get) Token: 0x060060A5 RID: 24741 RVA: 0x0014857F File Offset: 0x0014677F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RecipientTemplateProvisioningPolicy.MostDerivedClass;
			}
		}

		// Token: 0x17002297 RID: 8855
		// (get) Token: 0x060060A6 RID: 24742 RVA: 0x00148586 File Offset: 0x00146786
		internal override ICollection<Type> SupportedPresentationObjectTypes
		{
			get
			{
				return ProvisioningHelper.AllSupportedRecipientTypes;
			}
		}

		// Token: 0x17002298 RID: 8856
		// (get) Token: 0x060060A7 RID: 24743 RVA: 0x0014858D File Offset: 0x0014678D
		internal override IEnumerable<IProvisioningTemplate> ProvisioningTemplateRules
		{
			get
			{
				return RecipientTemplateProvisioningPolicy.provisioningTemplates;
			}
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x00148594 File Offset: 0x00146794
		public RecipientTemplateProvisioningPolicy()
		{
			base.Name = "Recipient Template Policy";
		}

		// Token: 0x17002299 RID: 8857
		// (get) Token: 0x060060A9 RID: 24745 RVA: 0x001485A7 File Offset: 0x001467A7
		// (set) Token: 0x060060AA RID: 24746 RVA: 0x001485B9 File Offset: 0x001467B9
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> DefaultMaxSendSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[RecipientTemplateProvisioningPolicySchema.DefaultMaxSendSize];
			}
			set
			{
				this[RecipientTemplateProvisioningPolicySchema.DefaultMaxSendSize] = value;
			}
		}

		// Token: 0x1700229A RID: 8858
		// (get) Token: 0x060060AB RID: 24747 RVA: 0x001485CC File Offset: 0x001467CC
		// (set) Token: 0x060060AC RID: 24748 RVA: 0x001485DE File Offset: 0x001467DE
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> DefaultMaxReceiveSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[RecipientTemplateProvisioningPolicySchema.DefaultMaxReceiveSize];
			}
			set
			{
				this[RecipientTemplateProvisioningPolicySchema.DefaultMaxReceiveSize] = value;
			}
		}

		// Token: 0x1700229B RID: 8859
		// (get) Token: 0x060060AD RID: 24749 RVA: 0x001485F1 File Offset: 0x001467F1
		// (set) Token: 0x060060AE RID: 24750 RVA: 0x00148603 File Offset: 0x00146803
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> DefaultProhibitSendQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[RecipientTemplateProvisioningPolicySchema.DefaultProhibitSendQuota];
			}
			set
			{
				this[RecipientTemplateProvisioningPolicySchema.DefaultProhibitSendQuota] = value;
			}
		}

		// Token: 0x1700229C RID: 8860
		// (get) Token: 0x060060AF RID: 24751 RVA: 0x00148616 File Offset: 0x00146816
		// (set) Token: 0x060060B0 RID: 24752 RVA: 0x00148628 File Offset: 0x00146828
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> DefaultProhibitSendReceiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[RecipientTemplateProvisioningPolicySchema.DefaultProhibitSendReceiveQuota];
			}
			set
			{
				this[RecipientTemplateProvisioningPolicySchema.DefaultProhibitSendReceiveQuota] = value;
			}
		}

		// Token: 0x1700229D RID: 8861
		// (get) Token: 0x060060B1 RID: 24753 RVA: 0x0014863B File Offset: 0x0014683B
		// (set) Token: 0x060060B2 RID: 24754 RVA: 0x0014864D File Offset: 0x0014684D
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> DefaultIssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[RecipientTemplateProvisioningPolicySchema.DefaultIssueWarningQuota];
			}
			set
			{
				this[RecipientTemplateProvisioningPolicySchema.DefaultIssueWarningQuota] = value;
			}
		}

		// Token: 0x1700229E RID: 8862
		// (get) Token: 0x060060B3 RID: 24755 RVA: 0x00148660 File Offset: 0x00146860
		// (set) Token: 0x060060B4 RID: 24756 RVA: 0x00148672 File Offset: 0x00146872
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize? DefaultRulesQuota
		{
			get
			{
				return (ByteQuantifiedSize?)this[RecipientTemplateProvisioningPolicySchema.DefaultRulesQuota];
			}
			set
			{
				this[RecipientTemplateProvisioningPolicySchema.DefaultRulesQuota] = value;
			}
		}

		// Token: 0x1700229F RID: 8863
		// (get) Token: 0x060060B5 RID: 24757 RVA: 0x00148685 File Offset: 0x00146885
		// (set) Token: 0x060060B6 RID: 24758 RVA: 0x00148697 File Offset: 0x00146897
		public ADObjectId DefaultDistributionListOU
		{
			get
			{
				return (ADObjectId)this[RecipientTemplateProvisioningPolicySchema.DefaultDistributionListOU];
			}
			set
			{
				this[RecipientTemplateProvisioningPolicySchema.DefaultDistributionListOU] = value;
			}
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x001486A8 File Offset: 0x001468A8
		internal override void ProvisionCustomDefaultProperties(IConfigurable provisionedDefault)
		{
			base.ProvisionCustomDefaultProperties(provisionedDefault);
			Mailbox mailbox = provisionedDefault as Mailbox;
			if (mailbox != null && (this.DefaultProhibitSendQuota != Unlimited<ByteQuantifiedSize>.UnlimitedValue || this.DefaultProhibitSendReceiveQuota != Unlimited<ByteQuantifiedSize>.UnlimitedValue || this.DefaultIssueWarningQuota != Unlimited<ByteQuantifiedSize>.UnlimitedValue))
			{
				mailbox.UseDatabaseQuotaDefaults = new bool?(false);
			}
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x00148708 File Offset: 0x00146908
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			errors.AddRange(Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
			{
				RecipientTemplateProvisioningPolicySchema.DefaultIssueWarningQuota,
				RecipientTemplateProvisioningPolicySchema.DefaultProhibitSendQuota,
				RecipientTemplateProvisioningPolicySchema.DefaultProhibitSendReceiveQuota
			}, this.Identity));
		}

		// Token: 0x040040E0 RID: 16608
		internal const string PolicyName = "Recipient Template Policy";

		// Token: 0x040040E1 RID: 16609
		private static RecipientTemplateProvisioningPolicySchema schema = ObjectSchema.GetInstance<RecipientTemplateProvisioningPolicySchema>();

		// Token: 0x040040E2 RID: 16610
		private static IProvisioningTemplate[] provisioningTemplates = new IProvisioningTemplate[]
		{
			new ProvisioningPropertyTemplate(RecipientTemplateProvisioningPolicySchema.DefaultMaxSendSize, MailEnabledRecipientSchema.MaxSendSize, null, ProvisioningHelper.AllSupportedRecipientTypes),
			new ProvisioningPropertyTemplate(RecipientTemplateProvisioningPolicySchema.DefaultMaxReceiveSize, MailEnabledRecipientSchema.MaxReceiveSize, null, ProvisioningHelper.AllSupportedRecipientTypes),
			new ProvisioningPropertyTemplate(RecipientTemplateProvisioningPolicySchema.DefaultProhibitSendQuota, MailboxSchema.ProhibitSendQuota, null, typeof(Mailbox)),
			new ProvisioningPropertyTemplate(RecipientTemplateProvisioningPolicySchema.DefaultProhibitSendReceiveQuota, MailboxSchema.ProhibitSendReceiveQuota, null, typeof(Mailbox)),
			new ProvisioningPropertyTemplate(RecipientTemplateProvisioningPolicySchema.DefaultIssueWarningQuota, MailboxSchema.IssueWarningQuota, null, typeof(Mailbox)),
			new ProvisioningPropertyTemplate(RecipientTemplateProvisioningPolicySchema.DefaultRulesQuota, MailboxSchema.RulesQuota, null, typeof(Mailbox)),
			new ProvisioningPropertyTemplate(RecipientTemplateProvisioningPolicySchema.DefaultDistributionListOU, DistributionGroupBaseSchema.DefaultDistributionListOU, null, new Type[]
			{
				typeof(DistributionGroup),
				typeof(DynamicDistributionGroup)
			})
		};

		// Token: 0x040040E3 RID: 16611
		internal new static string MostDerivedClass = "msExchRecipientTemplatePolicy";
	}
}
