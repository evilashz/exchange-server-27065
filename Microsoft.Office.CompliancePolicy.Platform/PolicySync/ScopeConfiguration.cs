using System;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000107 RID: 263
	[DataContract]
	public sealed class ScopeConfiguration : PolicyConfigurationBase
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x00014F1F File Offset: 0x0001311F
		public ScopeConfiguration() : base(ConfigurationObjectType.Scope)
		{
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00014F28 File Offset: 0x00013128
		public ScopeConfiguration(Guid tenantId, Guid objectId) : base(ConfigurationObjectType.Scope, tenantId, objectId)
		{
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00014F33 File Offset: 0x00013133
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x00014F3B File Offset: 0x0001313B
		[DataMember]
		public string AppliedScope { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00014F44 File Offset: 0x00013144
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x00014F4C File Offset: 0x0001314C
		[DataMember]
		public IncrementalAttribute<Mode> Mode { get; set; }
	}
}
