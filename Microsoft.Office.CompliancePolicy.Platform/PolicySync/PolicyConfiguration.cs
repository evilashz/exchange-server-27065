using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000FB RID: 251
	[DataContract]
	public sealed class PolicyConfiguration : PolicyConfigurationBase
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x000149A5 File Offset: 0x00012BA5
		public PolicyConfiguration() : base(ConfigurationObjectType.Policy)
		{
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000149AE File Offset: 0x00012BAE
		public PolicyConfiguration(Guid tenantId, Guid objectId) : base(ConfigurationObjectType.Policy, tenantId, objectId)
		{
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x000149B9 File Offset: 0x00012BB9
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x000149C1 File Offset: 0x00012BC1
		[DataMember]
		public IncrementalAttribute<string> Comments { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x000149CA File Offset: 0x00012BCA
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x000149D2 File Offset: 0x00012BD2
		[DataMember]
		public IncrementalAttribute<string> Description { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x000149DB File Offset: 0x00012BDB
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x000149E3 File Offset: 0x00012BE3
		[DataMember]
		public IncrementalAttribute<Mode> Mode { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x000149EC File Offset: 0x00012BEC
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x000149F4 File Offset: 0x00012BF4
		[DataMember]
		public PolicyScenario PolicyScenario { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x000149FD File Offset: 0x00012BFD
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00014A05 File Offset: 0x00012C05
		[DataMember]
		public IncrementalAttribute<Guid?> DefaultRuleId { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00014A0E File Offset: 0x00012C0E
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00014A16 File Offset: 0x00012C16
		[DataMember]
		public IncrementalAttribute<bool> IsEnabled { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00014A1F File Offset: 0x00012C1F
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x00014A27 File Offset: 0x00012C27
		[DataMember]
		public IncrementalAttribute<string> CreatedBy { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00014A30 File Offset: 0x00012C30
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00014A38 File Offset: 0x00012C38
		[DataMember]
		public string LastModifiedBy { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00014A41 File Offset: 0x00012C41
		protected override IDictionary<string, string> PropertyNameMapping
		{
			get
			{
				return PolicyConfiguration.propertyNameMapping;
			}
		}

		// Token: 0x040003D9 RID: 985
		private static IDictionary<string, string> propertyNameMapping = new Dictionary<string, string>
		{
			{
				"Description",
				PolicyDefinitionConfigSchema.Description
			},
			{
				"Comments",
				PolicyDefinitionConfigSchema.Comment
			},
			{
				"DefaultRuleId",
				PolicyDefinitionConfigSchema.DefaultPolicyRuleConfigId
			},
			{
				"Mode",
				PolicyDefinitionConfigSchema.Mode
			},
			{
				"PolicyScenario",
				PolicyDefinitionConfigSchema.Scenario
			},
			{
				"IsEnabled",
				PolicyDefinitionConfigSchema.Enabled
			},
			{
				"CreatedBy",
				PolicyDefinitionConfigSchema.CreatedBy
			},
			{
				"LastModifiedBy",
				PolicyDefinitionConfigSchema.LastModifiedBy
			}
		}.Merge(PolicyConfigurationBase.BasePropertyNameMapping);
	}
}
