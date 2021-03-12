using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x020004FE RID: 1278
	internal class MonitorPropertyInformationSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040020D6 RID: 8406
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020D7 RID: 8407
		public static readonly SimpleProviderPropertyDefinition PropertyName = new SimpleProviderPropertyDefinition("PropertyName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020D8 RID: 8408
		public static readonly SimpleProviderPropertyDefinition Description = new SimpleProviderPropertyDefinition("Description", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020D9 RID: 8409
		public static readonly SimpleProviderPropertyDefinition IsMandatory = new SimpleProviderPropertyDefinition("IsMandatory", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
