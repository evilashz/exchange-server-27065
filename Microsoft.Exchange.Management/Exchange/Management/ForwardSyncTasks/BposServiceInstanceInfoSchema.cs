using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000345 RID: 837
	internal class BposServiceInstanceInfoSchema : ObjectSchema
	{
		// Token: 0x04001865 RID: 6245
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(ServiceInstanceId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001866 RID: 6246
		public static readonly SimpleProviderPropertyDefinition BackSyncUrl = new SimpleProviderPropertyDefinition("BackSyncUrl", ExchangeObjectVersion.Exchange2010, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001867 RID: 6247
		public static readonly SimpleProviderPropertyDefinition EndpointName = new SimpleProviderPropertyDefinition("EndpointName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001868 RID: 6248
		public static readonly SimpleProviderPropertyDefinition AuthorityTransferIsSupported = new SimpleProviderPropertyDefinition("AuthorityTransferIsSupported", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
