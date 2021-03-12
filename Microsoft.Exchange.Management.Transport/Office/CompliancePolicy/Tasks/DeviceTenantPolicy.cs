using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000C8 RID: 200
	[Serializable]
	public sealed class DeviceTenantPolicy : PsCompliancePolicyBase
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001ECD7 File Offset: 0x0001CED7
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x0001ECDF File Offset: 0x0001CEDF
		private new MultiValuedProperty<BindingMetadata> ExchangeBinding { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001ECE8 File Offset: 0x0001CEE8
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0001ECF0 File Offset: 0x0001CEF0
		private new MultiValuedProperty<BindingMetadata> SharePointBinding { get; set; }

		// Token: 0x0600075A RID: 1882 RVA: 0x0001ECF9 File Offset: 0x0001CEF9
		public DeviceTenantPolicy()
		{
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001ED01 File Offset: 0x0001CF01
		public DeviceTenantPolicy(PolicyStorage policyStorage) : base(policyStorage)
		{
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001ED0A File Offset: 0x0001CF0A
		public PolicyScenario Type
		{
			get
			{
				return PolicyScenario.DeviceTenantConditionalAccess;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001ED0D File Offset: 0x0001CF0D
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return DeviceTenantPolicy.policySchema;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001ED14 File Offset: 0x0001CF14
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x040002B7 RID: 695
		private static readonly PsCompliancePolicyBaseSchema policySchema = ObjectSchema.GetInstance<PsCompliancePolicyBaseSchema>();
	}
}
