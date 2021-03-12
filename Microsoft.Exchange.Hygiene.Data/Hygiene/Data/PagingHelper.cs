using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000098 RID: 152
	internal static class PagingHelper
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0001147C File Offset: 0x0000F67C
		public static QueryFilter GetPagingQueryFilter(QueryFilter baseQueryFilter, string cookie)
		{
			Dictionary<PropertyDefinition, object> dictionary = new Dictionary<PropertyDefinition, object>();
			dictionary[DalHelper.PageCookieProp] = null;
			dictionary[DalHelper.FinishedReadingAllPagesProp] = false;
			dictionary[DalHelper.RetrievedAllPagesProp] = -1;
			QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, DalHelper.PageCookieProp, cookie ?? string.Empty),
				new ComparisonFilter(ComparisonOperator.Equal, DalHelper.StoredProcOutputBagProp, dictionary)
			});
			if (baseQueryFilter != null)
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					baseQueryFilter,
					queryFilter
				});
			}
			return queryFilter;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001150C File Offset: 0x0000F70C
		public static string GetProcessedCookie(QueryFilter pagingFilter, out bool complete)
		{
			if (pagingFilter == null)
			{
				throw new ArgumentException("pagingFilter");
			}
			Dictionary<PropertyDefinition, object> pagingMetadata = PagingHelper.GetPagingMetadata(pagingFilter);
			string result = (string)pagingMetadata[DalHelper.PageCookieProp];
			complete = (bool)pagingMetadata[DalHelper.FinishedReadingAllPagesProp];
			return result;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00011570 File Offset: 0x0000F770
		internal static Dictionary<PropertyDefinition, object> GetPagingMetadata(QueryFilter filter)
		{
			Dictionary<PropertyDefinition, object> pagedOutputBag = null;
			DalHelper.ForEachProperty(filter, delegate(PropertyDefinition prop, object propValue)
			{
				if (prop == DalHelper.StoredProcOutputBagProp)
				{
					pagedOutputBag = (Dictionary<PropertyDefinition, object>)propValue;
				}
			});
			if (pagedOutputBag == null)
			{
				throw new ArgumentException("Cookie output bag not found in query filter");
			}
			return pagedOutputBag;
		}
	}
}
