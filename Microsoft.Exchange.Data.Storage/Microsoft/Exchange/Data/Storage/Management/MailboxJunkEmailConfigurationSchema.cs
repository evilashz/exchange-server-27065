using System;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A05 RID: 2565
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxJunkEmailConfigurationSchema : XsoMailboxConfigurationObjectSchema
	{
		// Token: 0x04003485 RID: 13445
		public static readonly SimpleProviderPropertyDefinition Identity = XsoMailboxConfigurationObjectSchema.MailboxOwnerId;

		// Token: 0x04003486 RID: 13446
		public static readonly SimplePropertyDefinition Enabled = new SimplePropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003487 RID: 13447
		public static readonly SimplePropertyDefinition TrustedListsOnly = new SimplePropertyDefinition("TrustedListsOnly", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003488 RID: 13448
		public static readonly SimplePropertyDefinition ContactsTrusted = new SimplePropertyDefinition("ContactsTrusted", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003489 RID: 13449
		public static readonly SimplePropertyDefinition TrustedSendersAndDomains = new SimplePropertyDefinition("TrustedSendersAndDomains", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400348A RID: 13450
		public static readonly SimplePropertyDefinition BlockedSendersAndDomains = new SimplePropertyDefinition("BlockedSendersAndDomains", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
