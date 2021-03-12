using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000E1 RID: 225
	[Serializable]
	public class AuditConfigurationPolicy : PsCompliancePolicyBase
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x00025A2A File Offset: 0x00023C2A
		public AuditConfigurationPolicy()
		{
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00025A32 File Offset: 0x00023C32
		public AuditConfigurationPolicy(PolicyStorage policyStorage) : base(policyStorage)
		{
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00025A3B File Offset: 0x00023C3B
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return AuditConfigurationPolicy.schema;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00025A42 File Offset: 0x00023C42
		public PolicyScenario Type
		{
			get
			{
				return PolicyScenario.AuditSettings;
			}
		}

		// Token: 0x040003D3 RID: 979
		private static readonly AuditConfigurationPolicySchema schema = ObjectSchema.GetInstance<AuditConfigurationPolicySchema>();
	}
}
