using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000DE RID: 222
	internal abstract class DelegatedObjectSchema : ADObjectSchema
	{
		// Token: 0x04000464 RID: 1124
		public static readonly ADPropertyDefinition Identity = new ADPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2003, typeof(DelegatedObjectId), "delegatedIdentity", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
	}
}
