using System;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007FB RID: 2043
	internal class TenantRelocationSyncPartitionData
	{
		// Token: 0x170023BC RID: 9148
		// (get) Token: 0x060064CF RID: 25807 RVA: 0x0015FBEE File Offset: 0x0015DDEE
		// (set) Token: 0x060064D0 RID: 25808 RVA: 0x0015FBF6 File Offset: 0x0015DDF6
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x170023BD RID: 9149
		// (get) Token: 0x060064D1 RID: 25809 RVA: 0x0015FBFF File Offset: 0x0015DDFF
		public ADObjectId TenantOrganizationUnit
		{
			get
			{
				return this.OrganizationId.OrganizationalUnit;
			}
		}

		// Token: 0x170023BE RID: 9150
		// (get) Token: 0x060064D2 RID: 25810 RVA: 0x0015FC0C File Offset: 0x0015DE0C
		public ADObjectId TenantConfigurationUnit
		{
			get
			{
				return this.OrganizationId.ConfigurationUnit;
			}
		}

		// Token: 0x170023BF RID: 9151
		// (get) Token: 0x060064D3 RID: 25811 RVA: 0x0015FC19 File Offset: 0x0015DE19
		// (set) Token: 0x060064D4 RID: 25812 RVA: 0x0015FC21 File Offset: 0x0015DE21
		public ADObjectId TenantConfigurationUnitRoot { get; private set; }

		// Token: 0x170023C0 RID: 9152
		// (get) Token: 0x060064D5 RID: 25813 RVA: 0x0015FC2A File Offset: 0x0015DE2A
		// (set) Token: 0x060064D6 RID: 25814 RVA: 0x0015FC32 File Offset: 0x0015DE32
		public ADObjectId PartitionRoot { get; private set; }

		// Token: 0x170023C1 RID: 9153
		// (get) Token: 0x060064D7 RID: 25815 RVA: 0x0015FC3B File Offset: 0x0015DE3B
		// (set) Token: 0x060064D8 RID: 25816 RVA: 0x0015FC43 File Offset: 0x0015DE43
		public ADObjectId PartitionConfigNcRoot { get; private set; }

		// Token: 0x170023C2 RID: 9154
		// (get) Token: 0x060064D9 RID: 25817 RVA: 0x0015FC4C File Offset: 0x0015DE4C
		// (set) Token: 0x060064DA RID: 25818 RVA: 0x0015FC54 File Offset: 0x0015DE54
		public PartitionId PartitionId { get; private set; }

		// Token: 0x170023C3 RID: 9155
		// (get) Token: 0x060064DB RID: 25819 RVA: 0x0015FC5D File Offset: 0x0015DE5D
		// (set) Token: 0x060064DC RID: 25820 RVA: 0x0015FC65 File Offset: 0x0015DE65
		public bool IsConfigurationUnitUnderConfigNC { get; private set; }

		// Token: 0x060064DD RID: 25821 RVA: 0x0015FC70 File Offset: 0x0015DE70
		internal TenantRelocationSyncPartitionData(OrganizationId sourceTenantOrganizationId, ADObjectId sourcePartitionRoot, PartitionId sourcePartionId)
		{
			this.OrganizationId = sourceTenantOrganizationId;
			this.PartitionRoot = sourcePartitionRoot;
			this.PartitionId = sourcePartionId;
			this.PartitionConfigNcRoot = this.PartitionRoot.GetChildId("Configuration");
			this.IsConfigurationUnitUnderConfigNC = this.TenantConfigurationUnit.IsDescendantOf(this.PartitionConfigNcRoot);
			this.TenantConfigurationUnitRoot = this.TenantConfigurationUnit.Parent;
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x0015FCD6 File Offset: 0x0015DED6
		internal bool IsTenantRootObject(ADObjectId id)
		{
			return this.TenantConfigurationUnit.Equals(id) || this.TenantConfigurationUnitRoot.Equals(id) || this.TenantOrganizationUnit.Equals(id);
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x0015FD02 File Offset: 0x0015DF02
		internal bool IsUnderTenantScope(ADObjectId value)
		{
			if (string.IsNullOrEmpty(value.DistinguishedName))
			{
				throw new ArgumentException("value.DistinguishedName must not be null");
			}
			return value.IsDescendantOf(this.TenantConfigurationUnitRoot) || value.IsDescendantOf(this.TenantOrganizationUnit);
		}
	}
}
