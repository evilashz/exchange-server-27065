using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A0F RID: 2575
	[Serializable]
	public abstract class UnifiedPolicyStorageBase : ADConfigurationObject
	{
		// Token: 0x06007719 RID: 30489 RVA: 0x001884BF File Offset: 0x001866BF
		public UnifiedPolicyStorageBase()
		{
		}

		// Token: 0x17002A84 RID: 10884
		// (get) Token: 0x0600771A RID: 30490 RVA: 0x001884C7 File Offset: 0x001866C7
		// (set) Token: 0x0600771B RID: 30491 RVA: 0x001884D9 File Offset: 0x001866D9
		public Workload Workload
		{
			get
			{
				return (Workload)this[UnifiedPolicyStorageBaseSchema.WorkloadProp];
			}
			set
			{
				this[UnifiedPolicyStorageBaseSchema.WorkloadProp] = value;
			}
		}

		// Token: 0x17002A85 RID: 10885
		// (get) Token: 0x0600771C RID: 30492 RVA: 0x001884EC File Offset: 0x001866EC
		// (set) Token: 0x0600771D RID: 30493 RVA: 0x001884FE File Offset: 0x001866FE
		public Guid PolicyVersion
		{
			get
			{
				return (Guid)this[UnifiedPolicyStorageBaseSchema.PolicyVersion];
			}
			set
			{
				this[UnifiedPolicyStorageBaseSchema.PolicyVersion] = value;
			}
		}

		// Token: 0x17002A86 RID: 10886
		// (get) Token: 0x0600771E RID: 30494 RVA: 0x00188511 File Offset: 0x00186711
		// (set) Token: 0x0600771F RID: 30495 RVA: 0x00188523 File Offset: 0x00186723
		public Guid MasterIdentity
		{
			get
			{
				return (Guid)this[UnifiedPolicyStorageBaseSchema.MasterIdentity];
			}
			set
			{
				this[UnifiedPolicyStorageBaseSchema.MasterIdentity] = value;
			}
		}

		// Token: 0x17002A87 RID: 10887
		// (get) Token: 0x06007720 RID: 30496 RVA: 0x00188536 File Offset: 0x00186736
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}
	}
}
