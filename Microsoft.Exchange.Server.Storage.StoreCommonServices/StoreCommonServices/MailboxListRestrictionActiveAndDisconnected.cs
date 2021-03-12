using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000AC RID: 172
	internal class MailboxListRestrictionActiveAndDisconnected : IMailboxListRestriction
	{
		// Token: 0x060006DC RID: 1756 RVA: 0x00023DC8 File Offset: 0x00021FC8
		public SearchCriteria Filter(Context context)
		{
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(context.Database);
			return Factory.CreateSearchCriteriaOr(new SearchCriteria[]
			{
				Factory.CreateSearchCriteriaCompare(mailboxTable.Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(2)),
				Factory.CreateSearchCriteriaCompare(mailboxTable.Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(3)),
				Factory.CreateSearchCriteriaCompare(mailboxTable.Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(4))
			});
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00023E3C File Offset: 0x0002203C
		public Index Index(MailboxTable mailboxTable)
		{
			return mailboxTable.MailboxTablePK;
		}
	}
}
