using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000100 RID: 256
	public sealed class VariantConfigurationCompliancePolicyComponent : VariantConfigurationComponent
	{
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0001B820 File Offset: 0x00019A20
		internal VariantConfigurationCompliancePolicyComponent() : base("CompliancePolicy")
		{
			base.Add(new VariantConfigurationSection("CompliancePolicy.settings.ini", "ProcessForestWideOrgEtrs", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CompliancePolicy.settings.ini", "ShowSupervisionPredicate", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CompliancePolicy.settings.ini", "ValidateTenantOutboundConnector", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CompliancePolicy.settings.ini", "RuleConfigurationAdChangeNotifications", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CompliancePolicy.settings.ini", "QuarantineAction", typeof(IFeature), false));
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0001B8D8 File Offset: 0x00019AD8
		public VariantConfigurationSection ProcessForestWideOrgEtrs
		{
			get
			{
				return base["ProcessForestWideOrgEtrs"];
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0001B8E5 File Offset: 0x00019AE5
		public VariantConfigurationSection ShowSupervisionPredicate
		{
			get
			{
				return base["ShowSupervisionPredicate"];
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0001B8F2 File Offset: 0x00019AF2
		public VariantConfigurationSection ValidateTenantOutboundConnector
		{
			get
			{
				return base["ValidateTenantOutboundConnector"];
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0001B8FF File Offset: 0x00019AFF
		public VariantConfigurationSection RuleConfigurationAdChangeNotifications
		{
			get
			{
				return base["RuleConfigurationAdChangeNotifications"];
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0001B90C File Offset: 0x00019B0C
		public VariantConfigurationSection QuarantineAction
		{
			get
			{
				return base["QuarantineAction"];
			}
		}
	}
}
