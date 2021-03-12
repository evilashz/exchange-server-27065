using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000E3 RID: 227
	[Serializable]
	public sealed class PsHoldCompliancePolicy : PsCompliancePolicyBase
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x00026006 File Offset: 0x00024206
		public PsHoldCompliancePolicy()
		{
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0002600E File Offset: 0x0002420E
		public PsHoldCompliancePolicy(PolicyStorage policyStorage) : base(policyStorage)
		{
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00026017 File Offset: 0x00024217
		public PolicyScenario Type
		{
			get
			{
				return PolicyScenario.Hold;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0002601A File Offset: 0x0002421A
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return PsHoldCompliancePolicy.schema;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00026021 File Offset: 0x00024221
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x040003DD RID: 989
		private static readonly PsCompliancePolicyBaseSchema schema = ObjectSchema.GetInstance<PsCompliancePolicyBaseSchema>();
	}
}
