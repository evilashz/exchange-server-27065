using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200002B RID: 43
	public interface IIndex
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000243 RID: 579
		IReadOnlyDictionary<Column, Column> RenameDictionary { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000244 RID: 580
		SortOrder SortOrder { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000245 RID: 581
		SortOrder LogicalSortOrder { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000246 RID: 582
		Table Table { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000247 RID: 583
		Table IndexTable { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000248 RID: 584
		IList<object> IndexKeyPrefix { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000249 RID: 585
		IList<Column> Columns { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600024A RID: 586
		ISet<Column> ConstantColumns { get; }

		// Token: 0x0600024B RID: 587
		bool GetIndexColumn(Column column, bool acceptTruncated, out Column indexColumn);
	}
}
