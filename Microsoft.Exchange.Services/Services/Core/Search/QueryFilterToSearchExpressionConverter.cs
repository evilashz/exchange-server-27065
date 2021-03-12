using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000275 RID: 629
	internal class QueryFilterToSearchExpressionConverter : BaseFilterConverter<QueryFilter, SearchExpressionType>
	{
		// Token: 0x06001071 RID: 4209 RVA: 0x0004F3FC File Offset: 0x0004D5FC
		static QueryFilterToSearchExpressionConverter()
		{
			QueryFilterToSearchExpressionConverter.queryFilterToConverterMap.Add(typeof(AndFilter), new AndFilterConverter());
			QueryFilterToSearchExpressionConverter.queryFilterToConverterMap.Add(typeof(OrFilter), new OrFilterConverter());
			QueryFilterToSearchExpressionConverter.queryFilterToConverterMap.Add(typeof(NotFilter), new NotFilterConverter());
			QueryFilterToSearchExpressionConverter.queryFilterToConverterMap.Add(typeof(BitMaskFilter), new BitmaskFilterConverter());
			QueryFilterToSearchExpressionConverter.queryFilterToConverterMap.Add(typeof(ExistsFilter), new ExistsFilterConverter());
			QueryFilterToSearchExpressionConverter.queryFilterToConverterMap.Add(typeof(SubFilter), new SubFilterConverter());
			QueryFilterToSearchExpressionConverter.queryFilterToConverterMap.Add(typeof(TextFilter), new TextFilterConverter());
			ComparisonFilterConverter value = new ComparisonFilterConverter();
			QueryFilterToSearchExpressionConverter.queryFilterToConverterMap.Add(typeof(ComparisonFilter), value);
			QueryFilterToSearchExpressionConverter.queryFilterToConverterMap.Add(typeof(PropertyComparisonFilter), value);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0004F4F4 File Offset: 0x0004D6F4
		private static BaseSingleFilterConverter GetConverter(QueryFilter incomingFilter)
		{
			BaseSingleFilterConverter result = null;
			if (!QueryFilterToSearchExpressionConverter.queryFilterToConverterMap.TryGetValue(incomingFilter.GetType(), out result))
			{
				throw new UnsupportedQueryFilterException(CoreResources.IDs.ErrorUnsupportedQueryFilter);
			}
			return result;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0004F528 File Offset: 0x0004D728
		public SearchExpressionType Convert(QueryFilter inputFilter)
		{
			if (inputFilter == null)
			{
				return null;
			}
			return base.InternalConvert(inputFilter);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0004F536 File Offset: 0x0004D736
		protected override bool IsLeafExpression(QueryFilter inputFilter)
		{
			return QueryFilterToSearchExpressionConverter.GetConverter(inputFilter).IsLeafFilter;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0004F544 File Offset: 0x0004D744
		protected override int GetFilterChildCount(QueryFilter inputFilter)
		{
			BaseNonLeafFilterConverter baseNonLeafFilterConverter = QueryFilterToSearchExpressionConverter.GetConverter(inputFilter) as BaseNonLeafFilterConverter;
			if (baseNonLeafFilterConverter == null)
			{
				return 0;
			}
			return baseNonLeafFilterConverter.GetQueryFilterChildCount(inputFilter);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0004F56C File Offset: 0x0004D76C
		protected override QueryFilter GetFilterChild(QueryFilter parentFilter, int childIndex)
		{
			BaseNonLeafFilterConverter baseNonLeafFilterConverter = QueryFilterToSearchExpressionConverter.GetConverter(parentFilter) as BaseNonLeafFilterConverter;
			if (baseNonLeafFilterConverter != null)
			{
				return baseNonLeafFilterConverter.GetQueryFilterChild(parentFilter, childIndex);
			}
			return null;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0004F592 File Offset: 0x0004D792
		protected override void ThrowTooLongException()
		{
			throw new QueryFilterTooLongException();
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0004F59C File Offset: 0x0004D79C
		protected override SearchExpressionType ConvertSingleElement(QueryFilter inputFilter, Stack<SearchExpressionType> workingStack)
		{
			BaseSingleFilterConverter converter = QueryFilterToSearchExpressionConverter.GetConverter(inputFilter);
			BaseLeafFilterConverter baseLeafFilterConverter = converter as BaseLeafFilterConverter;
			if (baseLeafFilterConverter != null)
			{
				return baseLeafFilterConverter.ConvertToSearchExpression(inputFilter);
			}
			BaseNonLeafFilterConverter baseNonLeafFilterConverter = (BaseNonLeafFilterConverter)converter;
			return baseNonLeafFilterConverter.ConvertToSearchExpresson(workingStack);
		}

		// Token: 0x04000C18 RID: 3096
		private static Dictionary<Type, BaseSingleFilterConverter> queryFilterToConverterMap = new Dictionary<Type, BaseSingleFilterConverter>();
	}
}
