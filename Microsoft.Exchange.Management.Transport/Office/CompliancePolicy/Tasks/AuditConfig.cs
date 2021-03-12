using System;
using Microsoft.Exchange.Data;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000DE RID: 222
	[Serializable]
	public class AuditConfig : IConfigurable
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x0002590C File Offset: 0x00023B0C
		public AuditConfig(Workload wl = Workload.None)
		{
			this.Identity = new ConfigObjectId(Guid.NewGuid().ToString());
			this.Workload = wl;
			this.Setting = AuditSwitchStatus.None;
			this.PolicyDistributionStatus = PolicyApplyStatus.Error;
			this.DistributionResults = new MultiValuedProperty<PolicyDistributionErrorDetails>();
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002595D File Offset: 0x00023B5D
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00025965 File Offset: 0x00023B65
		public Workload Workload { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0002596E File Offset: 0x00023B6E
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00025976 File Offset: 0x00023B76
		public AuditSwitchStatus Setting { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0002597F File Offset: 0x00023B7F
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00025987 File Offset: 0x00023B87
		public PolicyApplyStatus PolicyDistributionStatus { get; set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00025990 File Offset: 0x00023B90
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x00025998 File Offset: 0x00023B98
		public MultiValuedProperty<PolicyDistributionErrorDetails> DistributionResults { get; set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x000259A1 File Offset: 0x00023BA1
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x000259A9 File Offset: 0x00023BA9
		public ObjectId Identity { get; private set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x000259B2 File Offset: 0x00023BB2
		public ObjectState ObjectState
		{
			get
			{
				return ObjectState.New;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x000259B5 File Offset: 0x00023BB5
		public bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000259B8 File Offset: 0x00023BB8
		public ValidationError[] Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000259C0 File Offset: 0x00023BC0
		public void CopyChangesFrom(IConfigurable changedObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000259C7 File Offset: 0x00023BC7
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}
	}
}
