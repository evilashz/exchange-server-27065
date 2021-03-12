using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006A6 RID: 1702
	internal sealed class PolicyTipMessageConfigSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040035B2 RID: 13746
		internal static readonly ADPropertyDefinition Locale = new ADPropertyDefinition("Locale", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchPolicyTipMessageConfigLocale", ADPropertyDefinitionFlags.WriteOnce, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040035B3 RID: 13747
		internal static readonly ADPropertyDefinition Action = new ADPropertyDefinition("Action", ExchangeObjectVersion.Exchange2010, typeof(PolicyTipMessageConfigAction), "msExchPolicyTipMessageConfigAction", ADPropertyDefinitionFlags.WriteOnce, PolicyTipMessageConfigAction.NotifyOnly, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040035B4 RID: 13748
		public static readonly ADPropertyDefinition Value = new ADPropertyDefinition("Value", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchPolicyTipMessageConfigMessage", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, null, null);
	}
}
