using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200027C RID: 636
	internal class ServiceObjectToFilterConverter : BaseFilterConverter<SearchExpressionType, QueryFilter>
	{
		// Token: 0x0600109B RID: 4251 RVA: 0x000507B8 File Offset: 0x0004E9B8
		static ServiceObjectToFilterConverter()
		{
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("And", new AndFilterConverter());
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("Or", new OrFilterConverter());
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("Not", new NotFilterConverter());
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("Excludes", new BitmaskFilterConverter());
			ComparisonFilterConverter value = new ComparisonFilterConverter();
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("IsEqualTo", value);
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("IsNotEqualTo", value);
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("IsGreaterThan", value);
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("IsGreaterThanOrEqualTo", value);
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("IsLessThan", value);
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("IsLessThanOrEqualTo", value);
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("Exists", new ExistsFilterConverter());
			ServiceObjectToFilterConverter.elementNameToConverterMap.Add("Contains", new TextFilterConverter());
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000508B0 File Offset: 0x0004EAB0
		private static BaseSingleFilterConverter GetConverter(string elementName)
		{
			BaseSingleFilterConverter result = null;
			if (!ServiceObjectToFilterConverter.elementNameToConverterMap.TryGetValue(elementName, out result))
			{
				throw new InvalidRestrictionException(CoreResources.IDs.ErrorInvalidRestriction);
			}
			return result;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000508DF File Offset: 0x0004EADF
		public QueryFilter Convert(SearchExpressionType restrictionElement)
		{
			if (restrictionElement == null)
			{
				return null;
			}
			this.unrolledBitmasks.Clear();
			return base.InternalConvert(restrictionElement);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000508F8 File Offset: 0x0004EAF8
		protected override int GetFilterChildCount(SearchExpressionType parentFilter)
		{
			INonLeafSearchExpressionType nonLeafSearchExpressionType = parentFilter as INonLeafSearchExpressionType;
			if (nonLeafSearchExpressionType == null)
			{
				return 0;
			}
			return nonLeafSearchExpressionType.Items.Length;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0005091C File Offset: 0x0004EB1C
		protected override SearchExpressionType GetFilterChild(SearchExpressionType parentFilter, int childIndex)
		{
			INonLeafSearchExpressionType nonLeafSearchExpressionType = parentFilter as INonLeafSearchExpressionType;
			if (nonLeafSearchExpressionType == null || childIndex >= nonLeafSearchExpressionType.Items.Length)
			{
				return null;
			}
			return nonLeafSearchExpressionType.Items[childIndex];
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00050948 File Offset: 0x0004EB48
		protected override void ThrowTooLongException()
		{
			throw new RestrictionTooLongException();
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00050950 File Offset: 0x0004EB50
		protected override QueryFilter ConvertSingleElement(SearchExpressionType currentElement, Stack<QueryFilter> workingStack)
		{
			BaseSingleFilterConverter converter = ServiceObjectToFilterConverter.GetConverter(currentElement.FilterType);
			BaseLeafFilterConverter baseLeafFilterConverter = converter as BaseLeafFilterConverter;
			if (baseLeafFilterConverter != null)
			{
				return baseLeafFilterConverter.ConvertToQueryFilter(currentElement);
			}
			if (currentElement.FilterType == "Not" && workingStack.Peek() is BitMaskFilter)
			{
				BitMaskFilter bitMaskFilter = (BitMaskFilter)workingStack.Peek();
				if (!this.HasBeenUnrolled(bitMaskFilter))
				{
					this.unrolledBitmasks.Add(bitMaskFilter);
					return bitMaskFilter;
				}
			}
			BaseNonLeafFilterConverter baseNonLeafFilterConverter = (BaseNonLeafFilterConverter)converter;
			return baseNonLeafFilterConverter.ConvertToQueryFilter(workingStack);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000509CC File Offset: 0x0004EBCC
		private bool HasBeenUnrolled(BitMaskFilter bitmaskFilter)
		{
			foreach (BitMaskFilter objA in this.unrolledBitmasks)
			{
				if (object.ReferenceEquals(objA, bitmaskFilter))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00050A28 File Offset: 0x0004EC28
		protected override bool IsLeafExpression(SearchExpressionType expressionElement)
		{
			return !(expressionElement is INonLeafSearchExpressionType);
		}

		// Token: 0x04000C2A RID: 3114
		private static Dictionary<string, BaseSingleFilterConverter> elementNameToConverterMap = new Dictionary<string, BaseSingleFilterConverter>();

		// Token: 0x04000C2B RID: 3115
		private List<BitMaskFilter> unrolledBitmasks = new List<BitMaskFilter>();
	}
}
