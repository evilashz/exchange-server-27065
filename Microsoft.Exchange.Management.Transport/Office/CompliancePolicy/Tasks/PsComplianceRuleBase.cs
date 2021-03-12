using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public class PsComplianceRuleBase : ADPresentationObject
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x0001ED27 File Offset: 0x0001CF27
		public PsComplianceRuleBase()
		{
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001ED2F File Offset: 0x0001CF2F
		public PsComplianceRuleBase(RuleStorage ruleStorage) : base(ruleStorage)
		{
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001ED38 File Offset: 0x0001CF38
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return PsComplianceRuleBase.schema;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001ED3F File Offset: 0x0001CF3F
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0001ED51 File Offset: 0x0001CF51
		internal Guid MasterIdentity
		{
			get
			{
				return (Guid)this[PsComplianceRuleBaseSchema.MasterIdentity];
			}
			set
			{
				this[PsComplianceRuleBaseSchema.MasterIdentity] = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001ED64 File Offset: 0x0001CF64
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0001ED6C File Offset: 0x0001CF6C
		public bool ReadOnly { get; internal set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001ED78 File Offset: 0x0001CF78
		public string ExternalIdentity
		{
			get
			{
				if (this.MasterIdentity != Guid.Empty)
				{
					return this.MasterIdentity.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001EDB1 File Offset: 0x0001CFB1
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x0001EDC3 File Offset: 0x0001CFC3
		internal string RuleBlob
		{
			get
			{
				return (string)this[PsComplianceRuleBaseSchema.RuleBlob];
			}
			set
			{
				this[PsComplianceRuleBaseSchema.RuleBlob] = value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001EDD1 File Offset: 0x0001CFD1
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0001EDE3 File Offset: 0x0001CFE3
		public Workload Workload
		{
			get
			{
				return (Workload)this[PsComplianceRuleBaseSchema.Workload];
			}
			set
			{
				this[PsComplianceRuleBaseSchema.Workload] = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001EDF6 File Offset: 0x0001CFF6
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0001EE08 File Offset: 0x0001D008
		public Guid Policy
		{
			get
			{
				return (Guid)this[PsComplianceRuleBaseSchema.Policy];
			}
			set
			{
				this[PsComplianceRuleBaseSchema.Policy] = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001EE1B File Offset: 0x0001D01B
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0001EE2D File Offset: 0x0001D02D
		public string Comment
		{
			get
			{
				return (string)this[PsComplianceRuleBaseSchema.Comment];
			}
			set
			{
				this[PsComplianceRuleBaseSchema.Comment] = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001EE3B File Offset: 0x0001D03B
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0001EE4D File Offset: 0x0001D04D
		protected bool Enabled
		{
			get
			{
				return (bool)this[PsComplianceRuleBaseSchema.Enabled];
			}
			set
			{
				this[PsComplianceRuleBaseSchema.Enabled] = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001EE60 File Offset: 0x0001D060
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0001EE6B File Offset: 0x0001D06B
		public bool Disabled
		{
			get
			{
				return !this.Enabled;
			}
			set
			{
				this.Enabled = !value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001EE77 File Offset: 0x0001D077
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x0001EE89 File Offset: 0x0001D089
		public Mode Mode
		{
			get
			{
				return (Mode)this[PsComplianceRuleBaseSchema.Mode];
			}
			set
			{
				this[PsComplianceRuleBaseSchema.Mode] = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001EE9C File Offset: 0x0001D09C
		public Guid ObjectVersion
		{
			get
			{
				return (Guid)this[PsComplianceRuleBaseSchema.ObjectVersion];
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001EEAE File Offset: 0x0001D0AE
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0001EEC0 File Offset: 0x0001D0C0
		public string ContentMatchQuery
		{
			get
			{
				return (string)this[PsComplianceRuleBaseSchema.ContentMatchQuery];
			}
			set
			{
				this[PsComplianceRuleBaseSchema.ContentMatchQuery] = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0001EECE File Offset: 0x0001D0CE
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001EED5 File Offset: 0x0001D0D5
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0001EEDD File Offset: 0x0001D0DD
		public string CreatedBy { get; protected set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0001EEE6 File Offset: 0x0001D0E6
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0001EEEE File Offset: 0x0001D0EE
		public string LastModifiedBy { get; protected set; }

		// Token: 0x0600077E RID: 1918 RVA: 0x0001EEF7 File Offset: 0x0001D0F7
		internal virtual string GetRuleXmlFromPolicyRule(PolicyRule policyRule)
		{
			return new RuleSerializer().SaveRuleToString(policyRule);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001EF04 File Offset: 0x0001D104
		internal virtual void PopulateTaskProperties(Task task, IConfigurationSession configurationSession)
		{
			RuleStorage ruleStorage = base.DataObject as RuleStorage;
			ADUser userObjectByExternalDirectoryObjectId = Utils.GetUserObjectByExternalDirectoryObjectId(ruleStorage.CreatedBy, configurationSession);
			ADUser userObjectByExternalDirectoryObjectId2 = Utils.GetUserObjectByExternalDirectoryObjectId(ruleStorage.LastModifiedBy, configurationSession);
			this.CreatedBy = ((!Utils.ExecutingUserIsForestWideAdmin(task) && userObjectByExternalDirectoryObjectId != null) ? userObjectByExternalDirectoryObjectId.DisplayName : ruleStorage.CreatedBy);
			this.LastModifiedBy = ((!Utils.ExecutingUserIsForestWideAdmin(task) && userObjectByExternalDirectoryObjectId2 != null) ? userObjectByExternalDirectoryObjectId2.DisplayName : ruleStorage.LastModifiedBy);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001EF78 File Offset: 0x0001D178
		internal virtual void UpdateStorageProperties(Task task, IConfigurationSession configurationSession, bool isNewRule)
		{
			if (!Utils.ExecutingUserIsForestWideAdmin(task))
			{
				ADObjectId objectId;
				task.TryGetExecutingUserId(out objectId);
				ADUser userObjectByObjectId = Utils.GetUserObjectByObjectId(objectId, configurationSession);
				if (userObjectByObjectId != null)
				{
					RuleStorage ruleStorage = base.DataObject as RuleStorage;
					ruleStorage.LastModifiedBy = userObjectByObjectId.ExternalDirectoryObjectId;
					if (isNewRule)
					{
						ruleStorage.CreatedBy = userObjectByObjectId.ExternalDirectoryObjectId;
					}
				}
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001EFC8 File Offset: 0x0001D1C8
		internal virtual PolicyRule GetPolicyRuleFromRuleBlob()
		{
			throw new NotImplementedException("GetPolicyRuleFromRuleBlob must be implemented by the derived class");
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001EFD4 File Offset: 0x0001D1D4
		internal void SuppressPiiData(PiiMap piiMap)
		{
			base.Name = (SuppressingPiiProperty.TryRedact(ADObjectSchema.Name, base.Name, piiMap) as string);
		}

		// Token: 0x040002BA RID: 698
		private static readonly PsComplianceRuleBaseSchema schema = ObjectSchema.GetInstance<PsComplianceRuleBaseSchema>();
	}
}
