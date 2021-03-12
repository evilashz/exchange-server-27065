using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxBuilder<T> where T : ILocatableMailbox
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600025C RID: 604
		T Mailbox { get; }

		// Token: 0x0600025D RID: 605
		IMailboxBuilder<T> BuildFromAssociation(MailboxAssociation rawEntry);

		// Token: 0x0600025E RID: 606
		IMailboxBuilder<T> BuildFromDirectory(ADRawEntry rawEntry);
	}
}
