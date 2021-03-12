using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000AE RID: 174
	internal class MailboxListRestrictionFinalCleanup : IMailboxListRestriction
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x00023E7F File Offset: 0x0002207F
		public SearchCriteria Filter(Context context)
		{
			return Factory.CreateSearchCriteriaCompare(DatabaseSchema.MailboxTable(context.Database).Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(5));
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00023EA2 File Offset: 0x000220A2
		public Index Index(MailboxTable mailboxTable)
		{
			return mailboxTable.MailboxTablePK;
		}
	}
}
