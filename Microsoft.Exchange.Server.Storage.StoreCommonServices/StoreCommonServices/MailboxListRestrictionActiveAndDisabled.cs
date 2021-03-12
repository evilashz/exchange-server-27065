using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000AB RID: 171
	internal class MailboxListRestrictionActiveAndDisabled : IMailboxListRestriction
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x00023D5C File Offset: 0x00021F5C
		public SearchCriteria Filter(Context context)
		{
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(context.Database);
			return Factory.CreateSearchCriteriaOr(new SearchCriteria[]
			{
				Factory.CreateSearchCriteriaCompare(mailboxTable.Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(2)),
				Factory.CreateSearchCriteriaCompare(mailboxTable.Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(3))
			});
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00023DB6 File Offset: 0x00021FB6
		public Index Index(MailboxTable mailboxTable)
		{
			return mailboxTable.MailboxTablePK;
		}
	}
}
