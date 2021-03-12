using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000105 RID: 261
	[DataContract]
	public sealed class RuleConfiguration : PolicyConfigurationBase
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x00014D8F File Offset: 0x00012F8F
		public RuleConfiguration() : base(ConfigurationObjectType.Rule)
		{
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00014D98 File Offset: 0x00012F98
		public RuleConfiguration(Guid tenantId, Guid objectId) : base(ConfigurationObjectType.Rule, tenantId, objectId)
		{
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00014DA3 File Offset: 0x00012FA3
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x00014DAB File Offset: 0x00012FAB
		[DataMember]
		public IncrementalAttribute<string> Comments { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00014DB4 File Offset: 0x00012FB4
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x00014DBC File Offset: 0x00012FBC
		[DataMember]
		public IncrementalAttribute<string> Description { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00014DC5 File Offset: 0x00012FC5
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00014DCD File Offset: 0x00012FCD
		[DataMember]
		public Guid ParentPolicyId { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00014DD6 File Offset: 0x00012FD6
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x00014DDE File Offset: 0x00012FDE
		[DataMember]
		public IncrementalAttribute<Mode> Mode { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00014DE7 File Offset: 0x00012FE7
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x00014DEF File Offset: 0x00012FEF
		[DataMember]
		public IncrementalAttribute<string> RuleBlob { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00014DF8 File Offset: 0x00012FF8
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x00014E00 File Offset: 0x00013000
		[DataMember]
		public IncrementalAttribute<int> Priority { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00014E09 File Offset: 0x00013009
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x00014E11 File Offset: 0x00013011
		[DataMember]
		public IncrementalAttribute<bool> IsEnabled { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00014E1A File Offset: 0x0001301A
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x00014E22 File Offset: 0x00013022
		[DataMember]
		public IncrementalAttribute<string> CreatedBy { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00014E2B File Offset: 0x0001302B
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x00014E33 File Offset: 0x00013033
		[DataMember]
		public string LastModifiedBy { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00014E3C File Offset: 0x0001303C
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x00014E44 File Offset: 0x00013044
		[DataMember]
		public PolicyScenario PolicyScenario { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00014E4D File Offset: 0x0001304D
		protected override IDictionary<string, string> PropertyNameMapping
		{
			get
			{
				return RuleConfiguration.propertyNameMapping;
			}
		}

		// Token: 0x040003F6 RID: 1014
		private static IDictionary<string, string> propertyNameMapping = new Dictionary<string, string>
		{
			{
				"Mode",
				PolicyRuleConfigSchema.Mode
			},
			{
				"RuleBlob",
				PolicyRuleConfigSchema.RuleBlob
			},
			{
				"Priority",
				PolicyRuleConfigSchema.Priority
			},
			{
				"Description",
				PolicyRuleConfigSchema.Description
			},
			{
				"Comments",
				PolicyRuleConfigSchema.Comment
			},
			{
				"ParentPolicyId",
				PolicyRuleConfigSchema.PolicyDefinitionConfigId
			},
			{
				"IsEnabled",
				PolicyRuleConfigSchema.Enabled
			},
			{
				"CreatedBy",
				PolicyRuleConfigSchema.CreatedBy
			},
			{
				"LastModifiedBy",
				PolicyRuleConfigSchema.LastModifiedBy
			},
			{
				"PolicyScenario",
				PolicyRuleConfigSchema.Scenario
			}
		}.Merge(PolicyConfigurationBase.BasePropertyNameMapping);
	}
}
