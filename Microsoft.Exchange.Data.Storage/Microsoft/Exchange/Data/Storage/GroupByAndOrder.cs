using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000060 RID: 96
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct GroupByAndOrder
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x000384EC File Offset: 0x000366EC
		public GroupByAndOrder(PropertyDefinition groupByColumn, GroupSort groupSortColumn)
		{
			this.GroupByColumn = groupByColumn;
			this.GroupSortColumn = groupSortColumn;
		}

		// Token: 0x040001E8 RID: 488
		public readonly PropertyDefinition GroupByColumn;

		// Token: 0x040001E9 RID: 489
		public readonly GroupSort GroupSortColumn;
	}
}
