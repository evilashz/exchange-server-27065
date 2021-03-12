using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000343 RID: 835
	internal class BposPlacementConfigurationSchema : ObjectSchema
	{
		// Token: 0x04001861 RID: 6241
		public static readonly SimpleProviderPropertyDefinition Configuration = new SimpleProviderPropertyDefinition("Configuration", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
