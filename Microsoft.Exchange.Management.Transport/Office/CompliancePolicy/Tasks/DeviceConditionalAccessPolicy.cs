using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000C7 RID: 199
	[Serializable]
	public sealed class DeviceConditionalAccessPolicy : PsCompliancePolicyBase
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001EC87 File Offset: 0x0001CE87
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001EC8F File Offset: 0x0001CE8F
		private new MultiValuedProperty<BindingMetadata> ExchangeBinding { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001EC98 File Offset: 0x0001CE98
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0001ECA0 File Offset: 0x0001CEA0
		private new MultiValuedProperty<BindingMetadata> SharePointBinding { get; set; }

		// Token: 0x06000750 RID: 1872 RVA: 0x0001ECA9 File Offset: 0x0001CEA9
		public DeviceConditionalAccessPolicy()
		{
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001ECB1 File Offset: 0x0001CEB1
		public DeviceConditionalAccessPolicy(PolicyStorage policyStorage) : base(policyStorage)
		{
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001ECBA File Offset: 0x0001CEBA
		public PolicyScenario Type
		{
			get
			{
				return PolicyScenario.DeviceConditionalAccess;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001ECBD File Offset: 0x0001CEBD
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return DeviceConditionalAccessPolicy.policySchema;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x040002B4 RID: 692
		private static readonly PsCompliancePolicyBaseSchema policySchema = ObjectSchema.GetInstance<PsCompliancePolicyBaseSchema>();
	}
}
