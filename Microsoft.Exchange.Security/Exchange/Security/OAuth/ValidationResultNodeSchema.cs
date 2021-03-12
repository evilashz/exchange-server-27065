using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x02000106 RID: 262
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ValidationResultNodeSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040007EB RID: 2027
		public static readonly ProviderPropertyDefinition Task = new SimpleProviderPropertyDefinition("Task", ExchangeObjectVersion.Exchange2010, typeof(LocalizedString), PropertyDefinitionFlags.None, LocalizedString.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040007EC RID: 2028
		public static readonly ProviderPropertyDefinition Detail = new SimpleProviderPropertyDefinition("Detail", ExchangeObjectVersion.Exchange2010, typeof(LocalizedString), PropertyDefinitionFlags.None, LocalizedString.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040007ED RID: 2029
		public static readonly ProviderPropertyDefinition ResultType = new SimpleProviderPropertyDefinition("ResultType", ExchangeObjectVersion.Exchange2010, typeof(ResultType), PropertyDefinitionFlags.None, Microsoft.Exchange.Security.OAuth.ResultType.Success, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
