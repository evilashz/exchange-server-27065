using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.BackSync
{
	// Token: 0x020000C3 RID: 195
	internal sealed class SyncDataSchema : ObjectSchema
	{
		// Token: 0x040002F5 RID: 757
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040002F6 RID: 758
		public static readonly SimpleProviderPropertyDefinition Data = new SimpleProviderPropertyDefinition("Data", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
