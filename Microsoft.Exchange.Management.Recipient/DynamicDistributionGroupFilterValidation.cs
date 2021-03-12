using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200001A RID: 26
	internal static class DynamicDistributionGroupFilterValidation
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00008518 File Offset: 0x00006718
		internal static bool ContainsNonOptimizedCondition(QueryFilter filter, out LocalizedString? errorMessage)
		{
			TextFilter[] array = DynamicDistributionGroupFilterValidation.ExtractNonOptimizedCondition(filter);
			errorMessage = ((array == null) ? null : new LocalizedString?(Strings.DDGFilterIsNonoptimized));
			return errorMessage != null;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008550 File Offset: 0x00006750
		internal static bool IsFullOptimizedOrImproved(QueryFilter oldFilter, QueryFilter newFilter, out LocalizedString? errorMessage)
		{
			errorMessage = null;
			TextFilter[] array = DynamicDistributionGroupFilterValidation.ExtractNonOptimizedCondition(newFilter);
			if (array != null)
			{
				TextFilter[] array2 = DynamicDistributionGroupFilterValidation.ExtractNonOptimizedCondition(oldFilter);
				if (array2 == null)
				{
					errorMessage = new LocalizedString?(Strings.DDGFilterIsNonoptimized);
				}
				else if (array2.Length <= array.Length)
				{
					errorMessage = new LocalizedString?(Strings.NewFilterNeitherOptimizedNorImproved);
				}
			}
			return errorMessage == null;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000085AC File Offset: 0x000067AC
		private static TextFilter[] ExtractNonOptimizedCondition(QueryFilter filter)
		{
			List<TextFilter> list = null;
			Queue<QueryFilter> queue = new Queue<QueryFilter>();
			while (filter != null)
			{
				if (filter is CompositeFilter)
				{
					using (ReadOnlyCollection<QueryFilter>.Enumerator enumerator = ((CompositeFilter)filter).Filters.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							QueryFilter item = enumerator.Current;
							queue.Enqueue(item);
						}
						goto IL_D0;
					}
					goto IL_54;
				}
				goto IL_54;
				IL_D0:
				filter = ((queue.Count > 0) ? queue.Dequeue() : null);
				continue;
				IL_54:
				if (filter is NotFilter)
				{
					queue.Enqueue(((NotFilter)filter).Filter);
					goto IL_D0;
				}
				if (filter is TextFilter)
				{
					TextFilter textFilter = (TextFilter)filter;
					if (textFilter.MatchOptions == MatchOptions.SubString || textFilter.MatchOptions == MatchOptions.Suffix)
					{
						if (list == null)
						{
							list = new List<TextFilter>();
						}
						list.Add(textFilter);
						goto IL_D0;
					}
					goto IL_D0;
				}
				else
				{
					if (!(filter is ComparisonFilter) && !(filter is ExistsFilter) && !(filter is FalseFilter))
					{
						throw new NotSupportedException("Unsupported filter type: " + filter.GetType());
					}
					goto IL_D0;
				}
			}
			if (list == null)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000086C0 File Offset: 0x000068C0
		private static string FiltersToString(TextFilter[] filters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (TextFilter textFilter in filters)
			{
				stringBuilder.Append(textFilter.GenerateInfixString(FilterLanguage.Monad));
				stringBuilder.Append(", ");
			}
			stringBuilder.Length -= ", ".Length;
			return stringBuilder.ToString();
		}

		// Token: 0x04000031 RID: 49
		private const string InfixDelimiter = ", ";
	}
}
