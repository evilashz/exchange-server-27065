using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	public sealed class DevicePolicy : PsCompliancePolicyBase
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001EC37 File Offset: 0x0001CE37
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001EC3F File Offset: 0x0001CE3F
		private new MultiValuedProperty<BindingMetadata> ExchangeBinding { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001EC48 File Offset: 0x0001CE48
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0001EC50 File Offset: 0x0001CE50
		private new MultiValuedProperty<BindingMetadata> SharePointBinding { get; set; }

		// Token: 0x06000746 RID: 1862 RVA: 0x0001EC59 File Offset: 0x0001CE59
		public DevicePolicy()
		{
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001EC61 File Offset: 0x0001CE61
		public DevicePolicy(PolicyStorage policyStorage) : base(policyStorage)
		{
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001EC6A File Offset: 0x0001CE6A
		public PolicyScenario Type
		{
			get
			{
				return PolicyScenario.DeviceSettings;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001EC6D File Offset: 0x0001CE6D
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return DevicePolicy.policySchema;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001EC74 File Offset: 0x0001CE74
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x040002B1 RID: 689
		private static readonly PsCompliancePolicyBaseSchema policySchema = ObjectSchema.GetInstance<PsCompliancePolicyBaseSchema>();
	}
}
