using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000067 RID: 103
	public class InferenceLogViewTable : ViewTable
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x00045CF8 File Offset: 0x00043EF8
		public InferenceLogViewTable(Context context, Mailbox mailbox) : base(mailbox, DatabaseSchema.InferenceLogTable(mailbox.Database).Table)
		{
			InferenceLogTable inferenceLogTable = DatabaseSchema.InferenceLogTable(mailbox.Database);
			base.SetImplicitCriteria(Factory.CreateSearchCriteriaCompare(inferenceLogTable.MailboxPartitionNumber, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(mailbox.MailboxPartitionNumber, inferenceLogTable.MailboxPartitionNumber)));
			Column[] columns;
			if (UnifiedMailbox.IsReady(context, context.Database))
			{
				columns = new Column[]
				{
					inferenceLogTable.MailboxPartitionNumber,
					inferenceLogTable.MailboxNumber,
					inferenceLogTable.RowId,
					inferenceLogTable.CreateTime,
					inferenceLogTable.PropertyBlob
				};
			}
			else
			{
				columns = new Column[]
				{
					inferenceLogTable.MailboxPartitionNumber,
					inferenceLogTable.RowId,
					inferenceLogTable.CreateTime,
					inferenceLogTable.PropertyBlob
				};
			}
			base.SetColumns(context, columns);
			this.SortTable(SortOrder.Empty);
		}
	}
}
