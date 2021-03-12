using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009FC RID: 2556
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregatedAccountListConfigurationSchema : UserConfigurationObjectSchema
	{
		// Token: 0x04003436 RID: 13366
		public static readonly SimplePropertyDefinition AggregatedAccountList = new SimplePropertyDefinition("AggregatedAccountList", ExchangeObjectVersion.Exchange2012, typeof(List<AggregatedAccountInfo>), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
