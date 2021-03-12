using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000541 RID: 1345
	internal sealed class ProvisioningReconciliationConfigSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040028C8 RID: 10440
		internal static readonly ADPropertyDefinition ReconciliationCookies = new ADPropertyDefinition("ReconciliationCookies", ExchangeObjectVersion.Exchange2007, typeof(ReconciliationCookie), "msExchReconciliationCookies", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040028C9 RID: 10441
		public static readonly ADPropertyDefinition ReconciliationCookiesForNextCycle = new ADPropertyDefinition("ReconciliationCookiesForNextCycle", ExchangeObjectVersion.Exchange2007, typeof(ReconciliationCookie), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040028CA RID: 10442
		public static readonly ADPropertyDefinition ReconciliationCookieForCurrentCycle = new ADPropertyDefinition("ReconciliationCookieForCurrentCycle", ExchangeObjectVersion.Exchange2007, typeof(ReconciliationCookie), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
