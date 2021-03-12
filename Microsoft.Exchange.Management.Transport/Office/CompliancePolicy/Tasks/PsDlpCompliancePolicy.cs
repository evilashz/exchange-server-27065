using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000DA RID: 218
	[Serializable]
	public sealed class PsDlpCompliancePolicy : PsCompliancePolicyBase
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x000253EE File Offset: 0x000235EE
		public PsDlpCompliancePolicy()
		{
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000253F6 File Offset: 0x000235F6
		public PsDlpCompliancePolicy(PolicyStorage policyStorage) : base(policyStorage)
		{
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x000253FF File Offset: 0x000235FF
		public PolicyScenario Type
		{
			get
			{
				return PolicyScenario.Dlp;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00025402 File Offset: 0x00023602
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return PsDlpCompliancePolicy.schema;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00025409 File Offset: 0x00023609
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x040003BC RID: 956
		private static readonly PsCompliancePolicyBaseSchema schema = ObjectSchema.GetInstance<PsCompliancePolicyBaseSchema>();
	}
}
