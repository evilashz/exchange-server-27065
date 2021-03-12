using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x020005A4 RID: 1444
	internal class MessageStatisticsReportSchema : TransportReportSchema
	{
		// Token: 0x0400237C RID: 9084
		public static readonly SimpleProviderPropertyDefinition TotalMessagesSent = new SimpleProviderPropertyDefinition("TotalMessagesSent", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400237D RID: 9085
		public static readonly SimpleProviderPropertyDefinition TotalMessagesReceived = new SimpleProviderPropertyDefinition("TotalMessagesReceived", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400237E RID: 9086
		public static readonly SimpleProviderPropertyDefinition TotalMessagesSentToForeign = new SimpleProviderPropertyDefinition("TotalMessagesSentToForeign", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400237F RID: 9087
		public static readonly SimpleProviderPropertyDefinition TotalMessagesReceivedFromForeign = new SimpleProviderPropertyDefinition("TotalMessagesReceivedFromForeign", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
