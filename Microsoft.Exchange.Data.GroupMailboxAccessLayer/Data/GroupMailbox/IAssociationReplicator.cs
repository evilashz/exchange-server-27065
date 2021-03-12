using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAssociationReplicator
	{
		// Token: 0x06000153 RID: 339
		bool ReplicateAssociation(IAssociationAdaptor masterAdaptor, params MailboxAssociation[] associations);
	}
}
