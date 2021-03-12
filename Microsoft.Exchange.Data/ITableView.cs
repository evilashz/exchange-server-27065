using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000013 RID: 19
	internal interface ITableView : IPagedView
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000058 RID: 88
		int EstimatedRowCount { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000059 RID: 89
		int CurrentRow { get; }

		// Token: 0x0600005A RID: 90
		bool SeekToCondition(SeekReference seekReference, QueryFilter seekFilter);

		// Token: 0x0600005B RID: 91
		int SeekToOffset(SeekReference seekReference, int offset);
	}
}
