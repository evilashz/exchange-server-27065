using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000413 RID: 1043
	internal sealed class SystemMessageSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001FB0 RID: 8112
		public static readonly ADPropertyDefinition Text = new ADPropertyDefinition("Text", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchDSNText", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001FB1 RID: 8113
		public static readonly ADPropertyDefinition Internal = new ADPropertyDefinition("Internal", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(SystemMessage.InternalGetter), null, null, null);

		// Token: 0x04001FB2 RID: 8114
		public static readonly ADPropertyDefinition Language = new ADPropertyDefinition("Language", ExchangeObjectVersion.Exchange2007, typeof(CultureInfo), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(SystemMessage.LanguageGetter), null, null, null);

		// Token: 0x04001FB3 RID: 8115
		public static readonly ADPropertyDefinition DsnCode = new ADPropertyDefinition("DsnCode", ExchangeObjectVersion.Exchange2007, typeof(EnhancedStatusCode), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, EnhancedStatusCode.Parse("5.0.0"), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(SystemMessage.CodeGetter), null, null, null);

		// Token: 0x04001FB4 RID: 8116
		public static readonly ADPropertyDefinition QuotaMessageType = new ADPropertyDefinition("QuotaMessageType", ExchangeObjectVersion.Exchange2007, typeof(QuotaMessageType?), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(SystemMessage.QuotaMessageTypeGetter), null, null, null);
	}
}
