using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200036A RID: 874
	internal abstract class SearchPeopleStrategy
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00085D07 File Offset: 0x00083F07
		protected BasePagingType Paging
		{
			get
			{
				return this.parameters.Paging;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x00085D14 File Offset: 0x00083F14
		protected string QueryString
		{
			get
			{
				return this.parameters.QueryString;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00085D21 File Offset: 0x00083F21
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x00085D29 File Offset: 0x00083F29
		private protected SortBy[] SortBy { protected get; private set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x00085D32 File Offset: 0x00083F32
		// (set) Token: 0x0600187E RID: 6270 RVA: 0x00085D3A File Offset: 0x00083F3A
		private protected QueryFilter RestrictionFilter { protected get; private set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00085D43 File Offset: 0x00083F43
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x00085D4B File Offset: 0x00083F4B
		private protected StoreId SearchScope { protected get; private set; }

		// Token: 0x06001881 RID: 6273 RVA: 0x00085D54 File Offset: 0x00083F54
		internal SearchPeopleStrategy(FindPeopleParameters parameters, QueryFilter restrictionFilter, StoreId searchScope)
		{
			parameters.Paging.NoRowCountRetrieval = true;
			this.parameters = parameters;
			this.RestrictionFilter = restrictionFilter;
			this.SearchScope = searchScope;
			if (parameters.SortResults == null || parameters.SortResults.Length == 0)
			{
				this.SortBy = SearchPeopleStrategy.GetSortBy("persona:DisplayName");
				return;
			}
			this.SortBy = SearchPeopleStrategy.GetSortBy(parameters.SortResults);
		}

		// Token: 0x06001882 RID: 6274
		public abstract Persona[] Execute();

		// Token: 0x06001883 RID: 6275 RVA: 0x00085DBC File Offset: 0x00083FBC
		protected void Log(FindPeopleMetadata metadata, object value)
		{
			this.parameters.Logger.Set(metadata, value);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00085DD8 File Offset: 0x00083FD8
		private static SortBy[] GetSortBy(string propertyToSortBy)
		{
			PropertyUriEnum uri;
			PropertyUriMapper.TryGetPropertyUriEnum(propertyToSortBy, out uri);
			PropertyPath propertyPath = new PropertyUri(uri);
			StorePropertyDefinition storePropertyDefinition = SearchPeopleStrategy.GetStorePropertyDefinition(propertyPath);
			return new SortBy[]
			{
				new SortBy(storePropertyDefinition, SortOrder.Ascending)
			};
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00085E2B File Offset: 0x0008402B
		private static SortBy[] GetSortBy(SortResults[] sortResults)
		{
			return (from sortResult in sortResults
			select new SortBy(SearchPeopleStrategy.GetStorePropertyDefinition(sortResult.SortByProperty), (SortOrder)sortResult.Order)).ToArray<SortBy>();
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00085E58 File Offset: 0x00084058
		private static StorePropertyDefinition GetStorePropertyDefinition(PropertyPath propertyPath)
		{
			PropertyDefinition propertyDefinition;
			if (!SearchSchemaMap.TryGetPropertyDefinition(propertyPath, out propertyDefinition))
			{
				throw new UnsupportedPathForSortGroupException(propertyPath);
			}
			StorePropertyDefinition storePropertyDefinition = (StorePropertyDefinition)propertyDefinition;
			if ((storePropertyDefinition.Capabilities & StorePropertyCapabilities.CanSortBy) != StorePropertyCapabilities.CanSortBy)
			{
				throw new UnsupportedPathForSortGroupException(propertyPath);
			}
			return storePropertyDefinition;
		}

		// Token: 0x0400106E RID: 4206
		private const string PersonDisplayNamePropertyURI = "persona:DisplayName";

		// Token: 0x0400106F RID: 4207
		internal static readonly HashSet<PropertyPath> AdditionalSupportedProperties = new HashSet<PropertyPath>
		{
			PersonaSchema.ThirdPartyPhotoUrls.PropertyPath,
			PersonaSchema.Attributions.PropertyPath
		};

		// Token: 0x04001070 RID: 4208
		protected FindPeopleParameters parameters;
	}
}
