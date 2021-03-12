using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000260 RID: 608
	internal static class OpathFilterEvaluator
	{
		// Token: 0x06001471 RID: 5233 RVA: 0x0004017F File Offset: 0x0003E37F
		public static bool FilterMatches(QueryFilter filter, IReadOnlyPropertyBag obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return OpathFilterEvaluator.FilterMatches(filter, obj, null);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00040198 File Offset: 0x0003E398
		public static bool FilterMatches(QueryFilter filter, IReadOnlyPropertyBag obj, Func<PropertyDefinition, object> getValueDelegate)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (getValueDelegate == null && obj == null)
			{
				throw new ArgumentNullException("getValueDelegate && obj");
			}
			NotFilter notFilter = filter as NotFilter;
			if (notFilter != null)
			{
				return !OpathFilterEvaluator.FilterMatches(notFilter.Filter, obj, getValueDelegate);
			}
			AndFilter andFilter = filter as AndFilter;
			if (andFilter != null)
			{
				foreach (QueryFilter filter2 in andFilter.Filters)
				{
					if (!OpathFilterEvaluator.FilterMatches(filter2, obj, getValueDelegate))
					{
						return false;
					}
				}
				return true;
			}
			OrFilter orFilter = filter as OrFilter;
			if (orFilter != null)
			{
				foreach (QueryFilter filter3 in orFilter.Filters)
				{
					if (OpathFilterEvaluator.FilterMatches(filter3, obj, getValueDelegate))
					{
						return true;
					}
				}
				return false;
			}
			SinglePropertyFilter singlePropertyFilter = filter as SinglePropertyFilter;
			if (singlePropertyFilter != null)
			{
				SimpleProviderPropertyDefinition simpleProviderPropertyDefinition = singlePropertyFilter.Property as SimpleProviderPropertyDefinition;
				if (simpleProviderPropertyDefinition != null && simpleProviderPropertyDefinition.IsFilterOnly)
				{
					throw new FilterOnlyAttributesException(simpleProviderPropertyDefinition.Name);
				}
				object obj2 = (obj != null) ? obj[singlePropertyFilter.Property] : getValueDelegate(singlePropertyFilter.Property);
				if (OpathFilterEvaluator.IsNullOrEmpty(obj2))
				{
					return OpathFilterEvaluator.FilterMatchesValue(singlePropertyFilter, null);
				}
				ICollection collection = obj2 as ICollection;
				if (collection != null)
				{
					foreach (object value in collection)
					{
						if (OpathFilterEvaluator.FilterMatchesValue(singlePropertyFilter, value))
						{
							return true;
						}
					}
					return false;
				}
				return OpathFilterEvaluator.FilterMatchesValue(singlePropertyFilter, obj2);
			}
			else
			{
				CSharpFilter<IReadOnlyPropertyBag> csharpFilter = filter as CSharpFilter<IReadOnlyPropertyBag>;
				if (csharpFilter != null)
				{
					if (obj == null)
					{
						throw new ArgumentNullException("obj");
					}
					return csharpFilter.Match(obj);
				}
				else
				{
					if (filter is TrueFilter)
					{
						return true;
					}
					if (filter is FalseFilter)
					{
						return false;
					}
					throw new NotSupportedException("The specified filter type \"" + filter.GetType().Name + "\" is currently not supported.");
				}
			}
			bool result;
			return result;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000403C8 File Offset: 0x0003E5C8
		private static bool FilterMatchesValue(SinglePropertyFilter filter, object value)
		{
			if (filter is ExistsFilter)
			{
				return null != value;
			}
			if (filter is GenericBitMaskFilter)
			{
				ulong num;
				try
				{
					num = Convert.ToUInt64(value);
				}
				catch (InvalidCastException)
				{
					return false;
				}
				GenericBitMaskFilter genericBitMaskFilter = filter as GenericBitMaskFilter;
				if (genericBitMaskFilter is BitMaskOrFilter)
				{
					return 0UL != (genericBitMaskFilter.Mask & num);
				}
				if (genericBitMaskFilter is BitMaskAndFilter)
				{
					return genericBitMaskFilter.Mask == (genericBitMaskFilter.Mask & num);
				}
			}
			if (filter is TextFilter)
			{
				TextFilter textFilter = filter as TextFilter;
				string text = textFilter.Text ?? string.Empty;
				string text2 = (value == null) ? string.Empty : value.ToString();
				if (string.IsNullOrEmpty(text2))
				{
					return string.IsNullOrEmpty(text);
				}
				StringComparison stringComparison = OpathFilterEvaluator.GetStringComparison(textFilter.MatchFlags);
				switch (textFilter.MatchOptions)
				{
				case MatchOptions.FullString:
					return text2.Equals(text, stringComparison);
				case MatchOptions.SubString:
				case MatchOptions.ExactPhrase:
					return -1 != text2.IndexOf(text, stringComparison);
				case MatchOptions.Prefix:
					return text2.StartsWith(text, stringComparison);
				case MatchOptions.Suffix:
					return text2.EndsWith(text, stringComparison);
				case MatchOptions.WildcardString:
					return OpathFilterEvaluator.MatchesWildcardString(text, text2, stringComparison);
				}
				throw new NotSupportedException("Not a currently supported Match Option: " + textFilter.MatchOptions);
			}
			else
			{
				if (!(filter is ComparisonFilter))
				{
					throw new NotSupportedException("The specified filter type \"" + filter.GetType().Name + "\" is currently not supported.");
				}
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				object obj = ValueConvertor.ConvertValue(comparisonFilter.PropertyValue, comparisonFilter.Property.Type, null);
				switch (comparisonFilter.ComparisonOperator)
				{
				case ComparisonOperator.Equal:
					return OpathFilterEvaluator.Equals(obj, value, StringComparison.OrdinalIgnoreCase);
				case ComparisonOperator.NotEqual:
					return !OpathFilterEvaluator.Equals(obj, value, StringComparison.OrdinalIgnoreCase);
				case ComparisonOperator.LessThan:
					return 0 < Comparer.Default.Compare(obj, value);
				case ComparisonOperator.LessThanOrEqual:
					return 0 <= Comparer.Default.Compare(obj, value);
				case ComparisonOperator.GreaterThan:
					return 0 > Comparer.Default.Compare(obj, value);
				case ComparisonOperator.GreaterThanOrEqual:
					return 0 >= Comparer.Default.Compare(obj, value);
				default:
					throw new NotSupportedException("Not a currently supported comparison operator: " + comparisonFilter.ComparisonOperator);
				}
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00040614 File Offset: 0x0003E814
		internal static bool Equals(object obj1, object obj2, StringComparison comparisonType)
		{
			if (obj1 is string)
			{
				return string.Equals(obj1 as string, obj2 as string, comparisonType);
			}
			if (obj1 is ProxyAddressBase)
			{
				return ProxyAddressBase.Equals(obj1 as ProxyAddressBase, obj2 as ProxyAddressBase, comparisonType);
			}
			return object.Equals(obj1, obj2);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00040654 File Offset: 0x0003E854
		private static bool IsNullOrEmpty(object value)
		{
			if (value == null)
			{
				return true;
			}
			if (string.Empty.Equals(value))
			{
				return true;
			}
			if (Guid.Empty.Equals(value))
			{
				return true;
			}
			ICollection collection = value as ICollection;
			return collection != null && collection.Count == 0;
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000406A4 File Offset: 0x0003E8A4
		private static bool MatchesWildcardString(string wildcardString, string value, StringComparison comparison)
		{
			string[] array = wildcardString.Split(new char[]
			{
				'*'
			});
			if (array.Length == 1)
			{
				return array[0].Equals(value, comparison);
			}
			StringBuilder stringBuilder = new StringBuilder("^" + Regex.Escape(array[0]));
			for (int i = 1; i < array.Length; i++)
			{
				stringBuilder.Append(".*");
				stringBuilder.Append(Regex.Escape(array[i]));
			}
			stringBuilder.Append("$");
			RegexOptions regexOptions = RegexOptions.Singleline;
			if (StringComparison.OrdinalIgnoreCase == comparison)
			{
				regexOptions |= RegexOptions.IgnoreCase;
			}
			return Regex.IsMatch(value, stringBuilder.ToString(), regexOptions);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00040740 File Offset: 0x0003E940
		private static StringComparison GetStringComparison(MatchFlags flags)
		{
			bool flag = MatchFlags.Default != (flags & MatchFlags.IgnoreCase);
			bool flag2 = MatchFlags.Default != (flags & MatchFlags.IgnoreNonSpace);
			if (flag && flag2)
			{
				return StringComparison.OrdinalIgnoreCase;
			}
			if (!flag && flag2)
			{
				return StringComparison.Ordinal;
			}
			if (flag && !flag2)
			{
				return StringComparison.OrdinalIgnoreCase;
			}
			return StringComparison.Ordinal;
		}
	}
}
