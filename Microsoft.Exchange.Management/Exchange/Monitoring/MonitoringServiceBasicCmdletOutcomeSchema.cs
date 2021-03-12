using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000579 RID: 1401
	internal class MonitoringServiceBasicCmdletOutcomeSchema : ObjectSchema
	{
		// Token: 0x040022F0 RID: 8944
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040022F1 RID: 8945
		public static readonly SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040022F2 RID: 8946
		public static readonly SimpleProviderPropertyDefinition Result = new SimpleProviderPropertyDefinition("Result", ExchangeObjectVersion.Exchange2003, typeof(MonitoringServiceBasicCmdletResult), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
