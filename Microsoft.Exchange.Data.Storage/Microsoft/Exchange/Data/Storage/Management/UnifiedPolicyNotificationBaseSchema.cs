using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A53 RID: 2643
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnifiedPolicyNotificationBaseSchema : XsoMailboxConfigurationObjectSchema
	{
		// Token: 0x040036F5 RID: 14069
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(UnifiedPolicySyncNotificationId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036F6 RID: 14070
		public static readonly XsoDriverPropertyDefinition UnifiedPolicyNotificationData = new XsoDriverPropertyDefinition(MessageItemSchema.UnifiedPolicyNotificationData, "UnifiedPolicyNotificationData ", ExchangeObjectVersion.Exchange2010, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036F7 RID: 14071
		public static readonly SimpleProviderPropertyDefinition Version = new SimpleProviderPropertyDefinition("Version", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
