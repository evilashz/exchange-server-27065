using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200054F RID: 1359
	internal class ReplicationCheckResultSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04002278 RID: 8824
		public static SimpleProviderPropertyDefinition Value = new SimpleProviderPropertyDefinition("Value", ExchangeObjectVersion.Exchange2010, typeof(ReplicationCheckResultEnum), PropertyDefinitionFlags.None, ReplicationCheckResultEnum.Undefined, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
