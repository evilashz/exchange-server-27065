using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000331 RID: 817
	internal sealed class ADDomainTrustInfoSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x04001717 RID: 5911
		public static readonly ADPropertyDefinition TargetName = new ADPropertyDefinition("TargetName", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.RawName
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ADObject.NameGetter), null, null, null);

		// Token: 0x04001718 RID: 5912
		public static readonly ADPropertyDefinition TrustDirection = new ADPropertyDefinition("TrustDirection", ExchangeObjectVersion.Exchange2003, typeof(TrustDirectionFlag), "trustDirection", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.FilterOnly, TrustDirectionFlag.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001719 RID: 5913
		public static readonly ADPropertyDefinition TrustType = new ADPropertyDefinition("TrustType", ExchangeObjectVersion.Exchange2003, typeof(TrustTypeFlag), "trustType", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.FilterOnly, TrustTypeFlag.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400171A RID: 5914
		public static readonly ADPropertyDefinition TrustAttributes = new ADPropertyDefinition("TrustAttributes", ExchangeObjectVersion.Exchange2003, typeof(TrustAttributeFlag), "trustAttributes", ADPropertyDefinitionFlags.ReadOnly, TrustAttributeFlag.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
