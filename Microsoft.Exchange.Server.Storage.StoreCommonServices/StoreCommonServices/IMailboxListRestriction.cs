using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000A9 RID: 169
	public interface IMailboxListRestriction
	{
		// Token: 0x060006D4 RID: 1748
		SearchCriteria Filter(Context context);

		// Token: 0x060006D5 RID: 1749
		Index Index(MailboxTable mailboxTable);
	}
}
