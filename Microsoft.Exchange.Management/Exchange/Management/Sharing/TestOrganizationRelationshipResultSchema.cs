using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000A02 RID: 2562
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TestOrganizationRelationshipResultSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04003443 RID: 13379
		public static readonly SimpleProviderPropertyDefinition Id = new SimpleProviderPropertyDefinition("Id", ExchangeObjectVersion.Exchange2007, typeof(TestOrganizationRelationshipResultId), PropertyDefinitionFlags.None, TestOrganizationRelationshipResultId.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003444 RID: 13380
		public static readonly SimpleProviderPropertyDefinition Status = new SimpleProviderPropertyDefinition("Status", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003445 RID: 13381
		public static readonly SimpleProviderPropertyDefinition Description = new SimpleProviderPropertyDefinition("Description", ExchangeObjectVersion.Exchange2007, typeof(LocalizedString), PropertyDefinitionFlags.None, LocalizedString.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
