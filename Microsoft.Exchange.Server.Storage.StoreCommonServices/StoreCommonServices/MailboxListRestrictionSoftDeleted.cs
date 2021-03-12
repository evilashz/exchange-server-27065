using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000AD RID: 173
	internal class MailboxListRestrictionSoftDeleted : IMailboxListRestriction
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x00023E4C File Offset: 0x0002204C
		public SearchCriteria Filter(Context context)
		{
			return Factory.CreateSearchCriteriaCompare(DatabaseSchema.MailboxTable(context.Database).Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(4));
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00023E6F File Offset: 0x0002206F
		public Index Index(MailboxTable mailboxTable)
		{
			return mailboxTable.MailboxTablePK;
		}
	}
}
