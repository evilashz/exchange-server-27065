using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000008 RID: 8
	internal class InstantSearchQueryParameters
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003F2C File Offset: 0x0000212C
		public InstantSearchQueryParameters(string kqlQuery, QueryFilter additionalFilter, QueryOptions queryOptions)
		{
			this.KqlQuery = kqlQuery;
			this.AdditionalFilter = additionalFilter;
			this.QueryOptions = queryOptions;
			if (kqlQuery == null)
			{
				if (additionalFilter == null)
				{
					throw new ArgumentException("Must specify kqlQuery and/or additionalFilter");
				}
				this.RequestType = InstantSearchQueryParameters.QueryType.PureQueryFilter;
			}
			else
			{
				if (additionalFilter == null)
				{
					this.RequestType = InstantSearchQueryParameters.QueryType.PureKql;
				}
				else
				{
					this.RequestType = InstantSearchQueryParameters.QueryType.KqlWithQueryFilter;
				}
				this.EmptyPrewarmingQuery = (kqlQuery == string.Empty);
			}
			this.MaximumRefinersCount = 5;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003F99 File Offset: 0x00002199
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003FA1 File Offset: 0x000021A1
		public string KqlQuery { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003FAA File Offset: 0x000021AA
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003FB2 File Offset: 0x000021B2
		public QueryFilter AdditionalFilter { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003FBB File Offset: 0x000021BB
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003FC3 File Offset: 0x000021C3
		public RefinementFilter RefinementFilter { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003FCC File Offset: 0x000021CC
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003FD4 File Offset: 0x000021D4
		public QueryOptions QueryOptions { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003FDD File Offset: 0x000021DD
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003FE5 File Offset: 0x000021E5
		public QuerySuggestionSources QuerySuggestionSources { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003FEE File Offset: 0x000021EE
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003FF6 File Offset: 0x000021F6
		public IReadOnlyCollection<PropertyDefinition> Refiners { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003FFF File Offset: 0x000021FF
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004007 File Offset: 0x00002207
		public IReadOnlyCollection<SortBy> SortSpec { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004010 File Offset: 0x00002210
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004018 File Offset: 0x00002218
		public bool EmptyPrewarmingQuery { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004021 File Offset: 0x00002221
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00004029 File Offset: 0x00002229
		public bool DeepTraversal { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004032 File Offset: 0x00002232
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000403A File Offset: 0x0000223A
		public int? MaximumResultCount { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004043 File Offset: 0x00002243
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000404B File Offset: 0x0000224B
		public int MaximumRefinersCount { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004054 File Offset: 0x00002254
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000405C File Offset: 0x0000225C
		public IReadOnlyList<StoreId> FolderScope { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004065 File Offset: 0x00002265
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000406D File Offset: 0x0000226D
		internal InstantSearchQueryParameters.QueryType RequestType { get; private set; }

		// Token: 0x060000C1 RID: 193 RVA: 0x00004076 File Offset: 0x00002276
		public bool HasOption(QueryOptions options)
		{
			return (this.QueryOptions & options) != QueryOptions.None;
		}

		// Token: 0x0400003B RID: 59
		internal const int DefaultMaximumRefinersCount = 5;

		// Token: 0x02000009 RID: 9
		internal enum QueryType
		{
			// Token: 0x0400004A RID: 74
			PureKql,
			// Token: 0x0400004B RID: 75
			KqlWithQueryFilter,
			// Token: 0x0400004C RID: 76
			PureQueryFilter
		}
	}
}
