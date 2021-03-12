using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000056 RID: 86
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFolder : IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000661 RID: 1633
		// (set) Token: 0x06000662 RID: 1634
		string DisplayName { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000663 RID: 1635
		int ItemCount { get; }

		// Token: 0x06000664 RID: 1636
		IQueryResult IFolderQuery(FolderQueryFlags queryFlags, QueryFilter queryFilter, SortBy[] sortColumns, params PropertyDefinition[] dataColumns);

		// Token: 0x06000665 RID: 1637
		IQueryResult IItemQuery(ItemQueryType queryFlags, QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns);

		// Token: 0x06000666 RID: 1638
		IQueryResult IConversationItemQuery(QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns);

		// Token: 0x06000667 RID: 1639
		IQueryResult IConversationMembersQuery(QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns);

		// Token: 0x06000668 RID: 1640
		QueryResult GroupedItemQuery(QueryFilter queryFilter, ItemQueryType itemQueryType, GroupByAndOrder[] groupBys, int expandCount, SortBy[] sortColumns, params PropertyDefinition[] dataColumns);

		// Token: 0x06000669 RID: 1641
		AggregateOperationResult DeleteObjects(DeleteItemFlags deleteFlags, params StoreId[] ids);

		// Token: 0x0600066A RID: 1642
		FolderSaveResult Save();

		// Token: 0x0600066B RID: 1643
		FolderSaveResult Save(SaveMode saveMode);

		// Token: 0x0600066C RID: 1644
		IQueryResult PersonItemQuery(QueryFilter queryFilter, QueryFilter aggregationFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns, AggregationExtension aggregationExtension);

		// Token: 0x0600066D RID: 1645
		IQueryResult PersonItemQuery(QueryFilter queryFilter, QueryFilter aggregationFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns);
	}
}
