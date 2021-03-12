using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000515 RID: 1301
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PeopleAggregationExtension : AggregationExtension
	{
		// Token: 0x060037EC RID: 14316 RVA: 0x000E2314 File Offset: 0x000E0514
		public PeopleAggregationExtension(MailboxSession mailboxSession)
		{
			this.contactFolders = mailboxSession.ContactFolders;
			this.clientInfoString = mailboxSession.ClientInfoString;
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000E2334 File Offset: 0x000E0534
		public override PropertyAggregationContext GetPropertyAggregationContext(IList<IStorePropertyBag> sources)
		{
			return new PersonPropertyAggregationContext(sources, this.contactFolders, this.clientInfoString);
		}

		// Token: 0x04001DCA RID: 7626
		private readonly ContactFolders contactFolders;

		// Token: 0x04001DCB RID: 7627
		private readonly string clientInfoString;
	}
}
