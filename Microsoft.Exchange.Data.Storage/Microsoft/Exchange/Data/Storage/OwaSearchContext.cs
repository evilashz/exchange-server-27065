using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007BC RID: 1980
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OwaSearchContext
	{
		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x001380D6 File Offset: 0x001362D6
		// (set) Token: 0x06004A68 RID: 19048 RVA: 0x001380DE File Offset: 0x001362DE
		public string SearchQueryFilterString { get; set; }

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x06004A69 RID: 19049 RVA: 0x001380E7 File Offset: 0x001362E7
		// (set) Token: 0x06004A6A RID: 19050 RVA: 0x001380EF File Offset: 0x001362EF
		public QueryFilter SearchQueryFilter { get; set; }

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x06004A6B RID: 19051 RVA: 0x001380F8 File Offset: 0x001362F8
		// (set) Token: 0x06004A6C RID: 19052 RVA: 0x00138100 File Offset: 0x00136300
		public SortBy[] SearchSortBy { get; set; }

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x06004A6D RID: 19053 RVA: 0x00138109 File Offset: 0x00136309
		// (set) Token: 0x06004A6E RID: 19054 RVA: 0x00138111 File Offset: 0x00136311
		public SearchScope SearchScope { get; set; }

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x06004A6F RID: 19055 RVA: 0x0013811A File Offset: 0x0013631A
		// (set) Token: 0x06004A70 RID: 19056 RVA: 0x00138122 File Offset: 0x00136322
		public StoreId FolderIdToSearch { get; set; }

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x0013812B File Offset: 0x0013632B
		// (set) Token: 0x06004A72 RID: 19058 RVA: 0x00138133 File Offset: 0x00136333
		public StoreId SearchFolderId { get; set; }

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x0013813C File Offset: 0x0013633C
		// (set) Token: 0x06004A74 RID: 19060 RVA: 0x00138144 File Offset: 0x00136344
		public bool IncludeDeletedItems { get; set; }

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06004A75 RID: 19061 RVA: 0x0013814D File Offset: 0x0013634D
		// (set) Token: 0x06004A76 RID: 19062 RVA: 0x00138155 File Offset: 0x00136355
		public bool IsFilteredView { get; set; }

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06004A77 RID: 19063 RVA: 0x0013815E File Offset: 0x0013635E
		// (set) Token: 0x06004A78 RID: 19064 RVA: 0x00138166 File Offset: 0x00136366
		public OwaViewFilter ViewFilter { get; set; }

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x0013816F File Offset: 0x0013636F
		// (set) Token: 0x06004A7A RID: 19066 RVA: 0x00138177 File Offset: 0x00136377
		public bool IsSearchFailed { get; set; }

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x06004A7B RID: 19067 RVA: 0x00138180 File Offset: 0x00136380
		// (set) Token: 0x06004A7C RID: 19068 RVA: 0x00138188 File Offset: 0x00136388
		public bool IsSearchInProgress { get; set; }

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x00138191 File Offset: 0x00136391
		// (set) Token: 0x06004A7E RID: 19070 RVA: 0x00138199 File Offset: 0x00136399
		public string ClientSearchFolderIdentity { get; set; }

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06004A7F RID: 19071 RVA: 0x001381A2 File Offset: 0x001363A2
		// (set) Token: 0x06004A80 RID: 19072 RVA: 0x001381AA File Offset: 0x001363AA
		public bool IsResetCache { get; set; }

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06004A81 RID: 19073 RVA: 0x001381B3 File Offset: 0x001363B3
		// (set) Token: 0x06004A82 RID: 19074 RVA: 0x001381BB File Offset: 0x001363BB
		public bool WaitForSearchComplete { get; set; }

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x001381C4 File Offset: 0x001363C4
		// (set) Token: 0x06004A84 RID: 19076 RVA: 0x001381CC File Offset: 0x001363CC
		public bool OptimizedSearch { get; set; }

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x001381D5 File Offset: 0x001363D5
		// (set) Token: 0x06004A86 RID: 19078 RVA: 0x001381DD File Offset: 0x001363DD
		public ViewFilterActions ViewFilterActions { get; set; }

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x001381E6 File Offset: 0x001363E6
		// (set) Token: 0x06004A88 RID: 19080 RVA: 0x001381EE File Offset: 0x001363EE
		public string FromFilter { get; set; }

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x001381F7 File Offset: 0x001363F7
		// (set) Token: 0x06004A8A RID: 19082 RVA: 0x001381FF File Offset: 0x001363FF
		public int SearchTimeoutInMilliseconds
		{
			get
			{
				return this.searchTimeoutInMilliseconds;
			}
			set
			{
				this.searchTimeoutInMilliseconds = value;
			}
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x00138208 File Offset: 0x00136408
		// (set) Token: 0x06004A8C RID: 19084 RVA: 0x00138210 File Offset: 0x00136410
		public int MaximumTemporaryFilteredViewPerUser
		{
			get
			{
				return this.maximumTemporaryFilteredViewPerUser;
			}
			set
			{
				this.maximumTemporaryFilteredViewPerUser = value;
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06004A8D RID: 19085 RVA: 0x00138219 File Offset: 0x00136419
		// (set) Token: 0x06004A8E RID: 19086 RVA: 0x00138221 File Offset: 0x00136421
		public bool IsWarmUpSearch { get; set; }

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x0013822A File Offset: 0x0013642A
		// (set) Token: 0x06004A90 RID: 19088 RVA: 0x00138232 File Offset: 0x00136432
		public SearchContextType SearchContextType { get; set; }

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x0013823B File Offset: 0x0013643B
		// (set) Token: 0x06004A92 RID: 19090 RVA: 0x00138243 File Offset: 0x00136443
		public KeyValuePair<string, string>[] HighlightTerms { get; set; }

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x0013824C File Offset: 0x0013644C
		// (set) Token: 0x06004A94 RID: 19092 RVA: 0x00138254 File Offset: 0x00136454
		public ExTimeZone RequestTimeZone { get; set; }

		// Token: 0x0400282F RID: 10287
		private int searchTimeoutInMilliseconds = 60000;

		// Token: 0x04002830 RID: 10288
		private int maximumTemporaryFilteredViewPerUser = 20;
	}
}
