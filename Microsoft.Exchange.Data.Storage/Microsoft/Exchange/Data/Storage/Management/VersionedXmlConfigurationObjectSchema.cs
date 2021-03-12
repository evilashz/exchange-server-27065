using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A5D RID: 2653
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class VersionedXmlConfigurationObjectSchema : XsoMailboxConfigurationObjectSchema
	{
		// Token: 0x04003709 RID: 14089
		public static readonly SimpleProviderPropertyDefinition Identity = XsoMailboxConfigurationObjectSchema.MailboxOwnerId;
	}
}
