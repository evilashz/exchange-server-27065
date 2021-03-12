using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200072C RID: 1836
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TestDataCenterDKMAccessResultSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04002933 RID: 10547
		public static readonly SimpleProviderPropertyDefinition AclStateIsGood = new SimpleProviderPropertyDefinition("AclStateIsGood", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002934 RID: 10548
		public static readonly SimpleProviderPropertyDefinition AclStateDetails = new SimpleProviderPropertyDefinition("AclStateDetails", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
