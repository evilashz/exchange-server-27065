using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x0200071B RID: 1819
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IRMConfigurationValidationResultSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040028F4 RID: 10484
		public static readonly SimpleProviderPropertyDefinition Results = new SimpleProviderPropertyDefinition("Results", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
