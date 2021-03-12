using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007CC RID: 1996
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SearchFolderCriteria
	{
		// Token: 0x06004AD8 RID: 19160 RVA: 0x00139528 File Offset: 0x00137728
		public SearchFolderCriteria(QueryFilter searchQuery, StoreId[] folderScope) : this(searchQuery, folderScope, Microsoft.Mapi.SearchState.None)
		{
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00139533 File Offset: 0x00137733
		internal SearchFolderCriteria(QueryFilter searchQuery, StoreId[] folderScope, SearchState searchState)
		{
			SearchFolderCriteria.CheckFolderScope(folderScope);
			this.FolderScope = folderScope;
			this.SearchQuery = searchQuery;
			this.searchState = searchState;
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06004ADA RID: 19162 RVA: 0x00139562 File Offset: 0x00137762
		// (set) Token: 0x06004ADB RID: 19163 RVA: 0x0013956F File Offset: 0x0013776F
		public bool DeepTraversal
		{
			get
			{
				return (this.searchState & Microsoft.Mapi.SearchState.Recursive) == Microsoft.Mapi.SearchState.Recursive;
			}
			set
			{
				if (value)
				{
					this.searchState |= Microsoft.Mapi.SearchState.Recursive;
					return;
				}
				this.searchState &= ~Microsoft.Mapi.SearchState.Recursive;
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06004ADC RID: 19164 RVA: 0x00139592 File Offset: 0x00137792
		// (set) Token: 0x06004ADD RID: 19165 RVA: 0x001395A7 File Offset: 0x001377A7
		public bool UseCiForComplexQueries
		{
			get
			{
				return (this.searchState & Microsoft.Mapi.SearchState.UseCiForComplexQueries) == Microsoft.Mapi.SearchState.UseCiForComplexQueries;
			}
			set
			{
				if (value)
				{
					this.searchState |= Microsoft.Mapi.SearchState.UseCiForComplexQueries;
					return;
				}
				this.searchState &= ~Microsoft.Mapi.SearchState.UseCiForComplexQueries;
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06004ADE RID: 19166 RVA: 0x001395D1 File Offset: 0x001377D1
		// (set) Token: 0x06004ADF RID: 19167 RVA: 0x001395E6 File Offset: 0x001377E6
		public bool StatisticsOnly
		{
			get
			{
				return (this.searchState & Microsoft.Mapi.SearchState.StatisticsOnly) == Microsoft.Mapi.SearchState.StatisticsOnly;
			}
			set
			{
				if (value)
				{
					this.searchState |= Microsoft.Mapi.SearchState.StatisticsOnly;
					return;
				}
				this.searchState &= ~Microsoft.Mapi.SearchState.StatisticsOnly;
			}
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x00139610 File Offset: 0x00137810
		// (set) Token: 0x06004AE1 RID: 19169 RVA: 0x00139625 File Offset: 0x00137825
		public bool FailNonContentIndexedSearch
		{
			get
			{
				return (this.searchState & Microsoft.Mapi.SearchState.CiOnly) == Microsoft.Mapi.SearchState.CiOnly;
			}
			set
			{
				if (value)
				{
					this.searchState |= Microsoft.Mapi.SearchState.CiOnly;
					return;
				}
				this.searchState &= ~Microsoft.Mapi.SearchState.CiOnly;
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x0013964F File Offset: 0x0013784F
		// (set) Token: 0x06004AE3 RID: 19171 RVA: 0x00139664 File Offset: 0x00137864
		public bool EstimateCountOnly
		{
			get
			{
				return (this.searchState & Microsoft.Mapi.SearchState.EstimateCountOnly) == Microsoft.Mapi.SearchState.EstimateCountOnly;
			}
			set
			{
				if (value)
				{
					this.searchState |= Microsoft.Mapi.SearchState.EstimateCountOnly;
					return;
				}
				this.searchState &= ~Microsoft.Mapi.SearchState.EstimateCountOnly;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06004AE4 RID: 19172 RVA: 0x0013968E File Offset: 0x0013788E
		// (set) Token: 0x06004AE5 RID: 19173 RVA: 0x00139696 File Offset: 0x00137896
		public int? MaximumResultsCount
		{
			get
			{
				return this.maximumResultsCount;
			}
			set
			{
				this.maximumResultsCount = value;
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06004AE6 RID: 19174 RVA: 0x0013969F File Offset: 0x0013789F
		// (set) Token: 0x06004AE7 RID: 19175 RVA: 0x001396A7 File Offset: 0x001378A7
		public QueryFilter SearchQuery
		{
			get
			{
				return this.searchQuery;
			}
			set
			{
				this.searchQuery = value;
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06004AE8 RID: 19176 RVA: 0x001396B0 File Offset: 0x001378B0
		// (set) Token: 0x06004AE9 RID: 19177 RVA: 0x001396B8 File Offset: 0x001378B8
		public StoreId[] FolderScope
		{
			get
			{
				return this.folderScope;
			}
			set
			{
				SearchFolderCriteria.CheckFolderScope(value);
				this.folderScope = value;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06004AEA RID: 19178 RVA: 0x001396C7 File Offset: 0x001378C7
		public SearchState SearchState
		{
			get
			{
				return (SearchState)this.searchState;
			}
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x001396D0 File Offset: 0x001378D0
		public override string ToString()
		{
			return string.Format("Filter = {0}. FolderScopeCount = {1}. DeepTraversal = {2}. UseCiForComplexQueries = {3}", new object[]
			{
				this.SearchQuery,
				(this.folderScope != null) ? this.folderScope.Length : 0,
				this.DeepTraversal,
				this.UseCiForComplexQueries
			});
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x00139730 File Offset: 0x00137930
		private static void CheckFolderScope(StoreId[] folderScope)
		{
			if (folderScope != null)
			{
				for (int i = 0; i < folderScope.Length; i++)
				{
					if (folderScope[i] == null)
					{
						throw new ArgumentException("folderScope cannot contain null values.");
					}
				}
			}
		}

		// Token: 0x040028B1 RID: 10417
		private SearchState searchState;

		// Token: 0x040028B2 RID: 10418
		private QueryFilter searchQuery;

		// Token: 0x040028B3 RID: 10419
		private StoreId[] folderScope;

		// Token: 0x040028B4 RID: 10420
		private int? maximumResultsCount = null;
	}
}
