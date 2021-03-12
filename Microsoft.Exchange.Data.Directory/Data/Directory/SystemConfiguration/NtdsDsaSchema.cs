using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200050D RID: 1293
	internal sealed class NtdsDsaSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x04002715 RID: 10005
		public static readonly ADPropertyDefinition Options = new ADPropertyDefinition("Options", ExchangeObjectVersion.Exchange2003, typeof(NtdsdsaOptions), "options", ADPropertyDefinitionFlags.ReadOnly, NtdsdsaOptions.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002716 RID: 10006
		public static readonly ADPropertyDefinition MasterNCs = new ADPropertyDefinition("MasterNCs", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "hasMasterNCs", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002717 RID: 10007
		public static readonly ADPropertyDefinition FullReplicaNCs = new ADPropertyDefinition("FullReplicaNCs", ExchangeObjectVersion.Exchange2003, typeof(string), "msDS-hasFullReplicaNCs", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002718 RID: 10008
		public static readonly ADPropertyDefinition DsIsRodc = new ADPropertyDefinition("DsIsRodc", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.ObjectCategory
		}, new CustomFilterBuilderDelegate(NtdsDsa.DsIsRodcFilterBuilder), new GetterDelegate(NtdsDsa.DsIsRodcGetter), null, null, null);

		// Token: 0x04002719 RID: 10009
		public static readonly ADPropertyDefinition InvocationId = new ADPropertyDefinition("InvocationId", ExchangeObjectVersion.Exchange2003, typeof(Guid), "invocationId", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
	}
}
