using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000AA RID: 170
	internal class MailboxListRestrictionActive : IMailboxListRestriction
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x00023D29 File Offset: 0x00021F29
		public SearchCriteria Filter(Context context)
		{
			return Factory.CreateSearchCriteriaCompare(DatabaseSchema.MailboxTable(context.Database).Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(2));
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00023D4C File Offset: 0x00021F4C
		public Index Index(MailboxTable mailboxTable)
		{
			return mailboxTable.MailboxTablePK;
		}
	}
}
