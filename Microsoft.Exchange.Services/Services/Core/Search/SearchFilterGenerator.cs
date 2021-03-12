using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search.AqsParser;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000277 RID: 631
	internal class SearchFilterGenerator
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x0004F5D8 File Offset: 0x0004D7D8
		private SearchFilterGenerator(QueryFilter advancedQueryFilter, CultureInfo userCultureInfo)
		{
			this.advancedQueryFilter = advancedQueryFilter;
			this.userCultureInfo = userCultureInfo;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0004F5F0 File Offset: 0x0004D7F0
		public static QueryFilter Execute(string searchString, CultureInfo userCultureInfo, QueryFilter advancedQueryFilter)
		{
			SearchFilterGenerator searchFilterGenerator = new SearchFilterGenerator(advancedQueryFilter, userCultureInfo);
			return searchFilterGenerator.Execute(searchString);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0004F60C File Offset: 0x0004D80C
		public QueryFilter Execute(string searchString)
		{
			if (searchString != null)
			{
				this.queryFilter = AqsParser.ParseAndBuildQuery(searchString, AqsParser.ParseOption.SuppressError, this.userCultureInfo, RescopedAll.Default, null, null);
			}
			if (this.advancedQueryFilter != null)
			{
				if (this.queryFilter == null)
				{
					this.queryFilter = this.advancedQueryFilter;
				}
				else
				{
					this.queryFilter = new AndFilter(new QueryFilter[]
					{
						this.queryFilter,
						this.advancedQueryFilter
					});
				}
			}
			if (this.queryFilter == null)
			{
				return null;
			}
			return this.queryFilter;
		}

		// Token: 0x04000C1C RID: 3100
		private QueryFilter advancedQueryFilter;

		// Token: 0x04000C1D RID: 3101
		private CultureInfo userCultureInfo;

		// Token: 0x04000C1E RID: 3102
		private QueryFilter queryFilter;
	}
}
