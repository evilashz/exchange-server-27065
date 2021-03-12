using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x020005A1 RID: 1441
	internal class MessageLatencyReportSchema : TransportReportSchema
	{
		// Token: 0x04002379 RID: 9081
		public static readonly SimpleProviderPropertyDefinition PercentOfMessageInGivenSla = new SimpleProviderPropertyDefinition("PercentOfMessageInGivenSla", ExchangeObjectVersion.Exchange2010, typeof(decimal), PropertyDefinitionFlags.PersistDefaultValue, 0m, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400237A RID: 9082
		public static readonly SimpleProviderPropertyDefinition SlaTargetInSeconds = new SimpleProviderPropertyDefinition("SlaTargetInSeconds", ExchangeObjectVersion.Exchange2010, typeof(short), PropertyDefinitionFlags.WriteOnce, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
