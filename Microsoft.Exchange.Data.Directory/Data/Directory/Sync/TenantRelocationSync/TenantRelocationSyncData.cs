using System;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007FC RID: 2044
	internal class TenantRelocationSyncData
	{
		// Token: 0x170023C4 RID: 9156
		// (get) Token: 0x060064E0 RID: 25824 RVA: 0x0015FD3B File Offset: 0x0015DF3B
		// (set) Token: 0x060064E1 RID: 25825 RVA: 0x0015FD43 File Offset: 0x0015DF43
		public TenantPartitionHint SourceTenantPartitionHint { get; private set; }

		// Token: 0x170023C5 RID: 9157
		// (get) Token: 0x060064E2 RID: 25826 RVA: 0x0015FD4C File Offset: 0x0015DF4C
		// (set) Token: 0x060064E3 RID: 25827 RVA: 0x0015FD54 File Offset: 0x0015DF54
		public ADObjectId ResourcePartitionRoot { get; private set; }

		// Token: 0x170023C6 RID: 9158
		// (get) Token: 0x060064E4 RID: 25828 RVA: 0x0015FD5D File Offset: 0x0015DF5D
		// (set) Token: 0x060064E5 RID: 25829 RVA: 0x0015FD65 File Offset: 0x0015DF65
		public ADObjectId ResourcePartitionConfigNc { get; private set; }

		// Token: 0x170023C7 RID: 9159
		// (get) Token: 0x060064E6 RID: 25830 RVA: 0x0015FD6E File Offset: 0x0015DF6E
		// (set) Token: 0x060064E7 RID: 25831 RVA: 0x0015FD76 File Offset: 0x0015DF76
		public ADObjectId SourceConfigContainer { get; private set; }

		// Token: 0x170023C8 RID: 9160
		// (get) Token: 0x060064E8 RID: 25832 RVA: 0x0015FD7F File Offset: 0x0015DF7F
		// (set) Token: 0x060064E9 RID: 25833 RVA: 0x0015FD87 File Offset: 0x0015DF87
		public TenantRelocationSyncPartitionData Source { get; private set; }

		// Token: 0x170023C9 RID: 9161
		// (get) Token: 0x060064EA RID: 25834 RVA: 0x0015FD90 File Offset: 0x0015DF90
		// (set) Token: 0x060064EB RID: 25835 RVA: 0x0015FD98 File Offset: 0x0015DF98
		public TenantRelocationSyncPartitionData Target { get; private set; }

		// Token: 0x170023CA RID: 9162
		// (get) Token: 0x060064EC RID: 25836 RVA: 0x0015FDA1 File Offset: 0x0015DFA1
		// (set) Token: 0x060064ED RID: 25837 RVA: 0x0015FDA9 File Offset: 0x0015DFA9
		public bool LargeTenantModeEnabled { get; private set; }

		// Token: 0x170023CB RID: 9163
		// (get) Token: 0x060064EE RID: 25838 RVA: 0x0015FDB2 File Offset: 0x0015DFB2
		public bool IsSourceSoftLinkEnabled
		{
			get
			{
				return !this.ResourcePartitionRoot.Equals(this.Source.PartitionRoot);
			}
		}

		// Token: 0x170023CC RID: 9164
		// (get) Token: 0x060064EF RID: 25839 RVA: 0x0015FDCD File Offset: 0x0015DFCD
		public bool IsTargetSoftLinkEnabled
		{
			get
			{
				return !this.ResourcePartitionRoot.Equals(this.Target.PartitionRoot);
			}
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x0015FDE8 File Offset: 0x0015DFE8
		public TenantRelocationSyncData(OrganizationId sourceTenantOrganizationId, OrganizationId targetTenantOrganizationId, ADObjectId resourcePartitionRoot, TenantPartitionHint partitionHint, bool largeTenantModeEnabled)
		{
			if (targetTenantOrganizationId.ConfigurationUnit == null)
			{
				throw new ArgumentNullException("targetTenantConfigurationUnit");
			}
			ADObjectId domainId = sourceTenantOrganizationId.OrganizationalUnit.DomainId;
			ADObjectId domainId2 = targetTenantOrganizationId.OrganizationalUnit.DomainId;
			PartitionId partitionId = sourceTenantOrganizationId.OrganizationalUnit.GetPartitionId();
			PartitionId partitionId2 = targetTenantOrganizationId.OrganizationalUnit.GetPartitionId();
			this.Source = new TenantRelocationSyncPartitionData(sourceTenantOrganizationId, domainId, partitionId);
			this.Target = new TenantRelocationSyncPartitionData(targetTenantOrganizationId, domainId2, partitionId2);
			this.ResourcePartitionRoot = resourcePartitionRoot;
			this.ResourcePartitionConfigNc = this.ResourcePartitionRoot.GetChildId("Configuration");
			this.SourceTenantPartitionHint = partitionHint;
			this.SourceConfigContainer = this.Source.TenantConfigurationUnit.Parent;
			this.LargeTenantModeEnabled = largeTenantModeEnabled;
		}
	}
}
