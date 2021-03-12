using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x020000A5 RID: 165
	internal class RecipientTrackingEventSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000211 RID: 529
		public static readonly SimpleProviderPropertyDefinition Date = new SimpleProviderPropertyDefinition("Date", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.UtcNow, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000212 RID: 530
		public static readonly SimpleProviderPropertyDefinition RecipientAddress = new SimpleProviderPropertyDefinition("RecipientAddress", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), PropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000213 RID: 531
		public static readonly SimpleProviderPropertyDefinition RecipientDisplayName = new SimpleProviderPropertyDefinition("RecipientDisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000214 RID: 532
		public static readonly SimpleProviderPropertyDefinition DeliveryStatus = new SimpleProviderPropertyDefinition("DeliveryStatus", ExchangeObjectVersion.Exchange2010, typeof(_DeliveryStatus), PropertyDefinitionFlags.None, _DeliveryStatus.Pending, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000215 RID: 533
		public static readonly SimpleProviderPropertyDefinition EventTypeValue = new SimpleProviderPropertyDefinition("EventType", ExchangeObjectVersion.Exchange2010, typeof(EventType), PropertyDefinitionFlags.None, EventType.Submit, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000216 RID: 534
		public static readonly SimpleProviderPropertyDefinition EventDescription = new SimpleProviderPropertyDefinition("EventDescription", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000217 RID: 535
		public static readonly SimpleProviderPropertyDefinition EventData = new SimpleProviderPropertyDefinition("EventData", ExchangeObjectVersion.Exchange2010, typeof(string[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000218 RID: 536
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
