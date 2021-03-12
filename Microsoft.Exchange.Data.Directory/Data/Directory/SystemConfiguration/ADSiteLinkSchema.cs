using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200038F RID: 911
	internal sealed class ADSiteLinkSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001975 RID: 6517
		public static readonly ADPropertyDefinition ADCost = new ADPropertyDefinition("ADCost", ExchangeObjectVersion.Exchange2003, typeof(int), "cost", ADPropertyDefinitionFlags.PersistDefaultValue, 100, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001976 RID: 6518
		public static readonly ADPropertyDefinition ExchangeCost = new ADPropertyDefinition("ExchangeCost", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchCost", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(0, int.MaxValue)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(1, 99999)
		}, null, null);

		// Token: 0x04001977 RID: 6519
		public static readonly ADPropertyDefinition Sites = new ADPropertyDefinition("Sites", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "siteList", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001978 RID: 6520
		public static readonly ADPropertyDefinition Cost = new ADPropertyDefinition("Cost", ExchangeObjectVersion.Exchange2007, typeof(int), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADSiteLinkSchema.ADCost,
			ADSiteLinkSchema.ExchangeCost
		}, null, new GetterDelegate(ADSiteLink.CostGetter), null, null, null);

		// Token: 0x04001979 RID: 6521
		public static readonly ADPropertyDefinition MaxMessageSize = new ADPropertyDefinition("MaxMessageSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "delivContLength", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
