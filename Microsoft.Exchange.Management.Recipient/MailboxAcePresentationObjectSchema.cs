using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000A4 RID: 164
	internal class MailboxAcePresentationObjectSchema : AcePresentationObjectSchema
	{
		// Token: 0x04000243 RID: 579
		public static readonly SimpleProviderPropertyDefinition AccessRights = new SimpleProviderPropertyDefinition("AccessRights", ExchangeObjectVersion.Exchange2003, typeof(MailboxRights[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
