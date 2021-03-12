using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OWAOrgExtensionSchema : OWAExtensionSchema
	{
		// Token: 0x04000035 RID: 53
		public static readonly SimpleProviderPropertyDefinition ProvidedTo = new SimpleProviderPropertyDefinition("ProvidedTo", ExchangeObjectVersion.Exchange2010, typeof(ClientExtensionProvidedTo), PropertyDefinitionFlags.None, ClientExtensionProvidedTo.Everyone, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000036 RID: 54
		public static readonly SimpleProviderPropertyDefinition UserList = new SimpleProviderPropertyDefinition("UserList", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
