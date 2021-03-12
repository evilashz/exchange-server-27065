using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D18 RID: 3352
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxDiscoverySearchRequestExtendedStoreSchema : ObjectSchema
	{
		// Token: 0x040050EC RID: 20716
		public static readonly ExtendedPropertyDefinition AsynchronousActionRequest = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "AsynchronousActionRequest", 14);

		// Token: 0x040050ED RID: 20717
		public static readonly ExtendedPropertyDefinition AsynchronousActionRbacContext = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "AsynchronousActionRbacContext", 25);
	}
}
