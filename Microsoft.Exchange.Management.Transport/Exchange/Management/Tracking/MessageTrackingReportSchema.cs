using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x020000A2 RID: 162
	internal class MessageTrackingReportSchema : MessageTrackingSharedResultSchema
	{
		// Token: 0x04000209 RID: 521
		public static readonly SimpleProviderPropertyDefinition DeliveryCount = new SimpleProviderPropertyDefinition("DeliveryCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400020A RID: 522
		public static readonly SimpleProviderPropertyDefinition UnsuccessfulCount = new SimpleProviderPropertyDefinition("UnsuccessfulCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400020B RID: 523
		public static readonly SimpleProviderPropertyDefinition PendingCount = new SimpleProviderPropertyDefinition("PendingCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400020C RID: 524
		public static readonly SimpleProviderPropertyDefinition TransferredCount = new SimpleProviderPropertyDefinition("TransferredCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400020D RID: 525
		public static readonly SimpleProviderPropertyDefinition RecipientTrackingEvents = new SimpleProviderPropertyDefinition("RecipientTrackingEvents", ExchangeObjectVersion.Exchange2010, typeof(RecipientTrackingEvent[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
