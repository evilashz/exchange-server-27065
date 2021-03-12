using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000E8 RID: 232
	[DataContract]
	public sealed class AssociationConfiguration : PolicyConfigurationBase
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x00013EED File Offset: 0x000120ED
		public AssociationConfiguration() : base(ConfigurationObjectType.Association)
		{
			this.PolicyIds = Enumerable.Empty<Guid>();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00013F01 File Offset: 0x00012101
		public AssociationConfiguration(Guid tenantId, Guid objectId) : base(ConfigurationObjectType.Association, tenantId, objectId)
		{
			this.PolicyIds = Enumerable.Empty<Guid>();
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00013F17 File Offset: 0x00012117
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00013F1F File Offset: 0x0001211F
		[DataMember]
		public IncrementalAttribute<string> Comments { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00013F28 File Offset: 0x00012128
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00013F30 File Offset: 0x00012130
		[DataMember]
		public IncrementalAttribute<string> Description { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00013F39 File Offset: 0x00012139
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00013F41 File Offset: 0x00012141
		[DataMember]
		public IncrementalAttribute<AssociationType> AssociationType { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00013F4A File Offset: 0x0001214A
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00013F52 File Offset: 0x00012152
		[DataMember]
		public IncrementalAttribute<string> Scope { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00013F5B File Offset: 0x0001215B
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00013F63 File Offset: 0x00012163
		[DataMember]
		public IEnumerable<Guid> PolicyIds { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00013F6C File Offset: 0x0001216C
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00013F74 File Offset: 0x00012174
		[DataMember]
		public IncrementalAttribute<Guid?> DefaultPolicyId { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00013F7D File Offset: 0x0001217D
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00013F85 File Offset: 0x00012185
		[DataMember]
		public IncrementalAttribute<bool> AllowOverride { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00013F8E File Offset: 0x0001218E
		protected override IDictionary<string, string> PropertyNameMapping
		{
			get
			{
				return AssociationConfiguration.propertyNameMapping;
			}
		}

		// Token: 0x040003B8 RID: 952
		private static IDictionary<string, string> propertyNameMapping = new Dictionary<string, string>
		{
			{
				"Description",
				PolicyAssociationConfigSchema.Description
			},
			{
				"Comments",
				PolicyAssociationConfigSchema.Comment
			},
			{
				"PolicyIds",
				PolicyAssociationConfigSchema.PolicyDefinitionConfigIds
			},
			{
				"DefaultPolicyId",
				PolicyAssociationConfigSchema.DefaultPolicyDefinitionConfigId
			},
			{
				"AllowOverride",
				PolicyAssociationConfigSchema.AllowOverride
			},
			{
				"Scope",
				PolicyAssociationConfigSchema.Scope
			}
		}.Merge(PolicyConfigurationBase.BasePropertyNameMapping);
	}
}
