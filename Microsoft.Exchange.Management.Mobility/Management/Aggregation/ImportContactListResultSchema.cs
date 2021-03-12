using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ImportContactListResultSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400007A RID: 122
		public static readonly ProviderPropertyDefinition ContactsImported = new SimpleProviderPropertyDefinition("ContactsImported", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
