using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002D2 RID: 722
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractFolder : AbstractStoreObject, IFolder, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x000861F0 File Offset: 0x000843F0
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x000861F7 File Offset: 0x000843F7
		public virtual string DisplayName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000861FE File Offset: 0x000843FE
		public virtual int ItemCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00086205 File Offset: 0x00084405
		public virtual IQueryResult IFolderQuery(FolderQueryFlags queryFlags, QueryFilter queryFilter, SortBy[] sortColumns, params PropertyDefinition[] dataColumns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x0008620C File Offset: 0x0008440C
		public virtual IQueryResult IItemQuery(ItemQueryType queryFlags, QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00086213 File Offset: 0x00084413
		public IQueryResult IConversationItemQuery(QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x0008621A File Offset: 0x0008441A
		public IQueryResult IConversationMembersQuery(QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00086221 File Offset: 0x00084421
		public QueryResult GroupedItemQuery(QueryFilter queryFilter, ItemQueryType queryFlags, GroupByAndOrder[] groupBy, int expandCount, SortBy[] sortColumns, params PropertyDefinition[] dataColumns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00086228 File Offset: 0x00084428
		public virtual AggregateOperationResult DeleteObjects(DeleteItemFlags deleteFlags, params StoreId[] ids)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0008622F File Offset: 0x0008442F
		public virtual FolderSaveResult Save()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00086236 File Offset: 0x00084436
		public FolderSaveResult Save(SaveMode saveMode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0008623D File Offset: 0x0008443D
		public virtual IQueryResult PersonItemQuery(QueryFilter queryFilter, QueryFilter aggregationFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns, AggregationExtension aggregationExtension)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00086244 File Offset: 0x00084444
		public virtual IQueryResult PersonItemQuery(QueryFilter queryFilter, QueryFilter aggregationFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			throw new NotImplementedException();
		}
	}
}
