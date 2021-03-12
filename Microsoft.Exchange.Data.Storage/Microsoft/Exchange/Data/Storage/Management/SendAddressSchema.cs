using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A52 RID: 2642
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SendAddressSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040036F3 RID: 14067
		public static readonly ProviderPropertyDefinition AddressId = new SimpleProviderPropertyDefinition("AddressId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036F4 RID: 14068
		public static readonly ProviderPropertyDefinition DisplayName = new SimpleProviderPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
