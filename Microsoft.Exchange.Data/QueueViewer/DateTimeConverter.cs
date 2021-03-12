using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000276 RID: 630
	internal static class DateTimeConverter
	{
		// Token: 0x06001506 RID: 5382 RVA: 0x0004318C File Offset: 0x0004138C
		public static QueryFilter ConvertQueryFilter(QueryFilter filter)
		{
			if (filter == null)
			{
				return null;
			}
			if (filter is CompositeFilter)
			{
				CompositeFilter compositeFilter = (CompositeFilter)filter;
				QueryFilter[] array = new QueryFilter[compositeFilter.FilterCount];
				bool flag = false;
				int num = 0;
				foreach (QueryFilter queryFilter in compositeFilter.Filters)
				{
					array[num] = DateTimeConverter.ConvertQueryFilter(queryFilter);
					if (array[num] != queryFilter)
					{
						flag = true;
					}
					num++;
				}
				if (!flag)
				{
					return filter;
				}
				if (filter is AndFilter)
				{
					return new AndFilter(array);
				}
				if (filter is OrFilter)
				{
					return new OrFilter(array);
				}
				throw new Exception("Invalid filter type");
			}
			else if (filter is NotFilter)
			{
				NotFilter notFilter = filter as NotFilter;
				QueryFilter queryFilter2 = DateTimeConverter.ConvertQueryFilter(notFilter.Filter);
				if (queryFilter2 != notFilter)
				{
					return new NotFilter(queryFilter2);
				}
				return filter;
			}
			else
			{
				if (!(filter is ComparisonFilter))
				{
					return filter;
				}
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter.PropertyValue is DateTime)
				{
					return new ComparisonFilter(comparisonFilter.ComparisonOperator, comparisonFilter.Property, ((DateTime)comparisonFilter.PropertyValue).ToUniversalTime());
				}
				return filter;
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x000432C4 File Offset: 0x000414C4
		public static void ConvertResultSet<ObjectType>(ObjectType[] results) where ObjectType : PagedDataObject
		{
			for (int i = 0; i < results.Length; i++)
			{
				results[i].ConvertDatesToLocalTime();
			}
		}
	}
}
