using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public abstract class QueryFilter
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00005FF0 File Offset: 0x000041F0
		internal static int[] InitializeFilterStringSizeEstimates()
		{
			int[] array = new int[256];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 1024;
			}
			return array;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006020 File Offset: 0x00004220
		internal static string ConvertPropertyName(string propertyName)
		{
			string result = null;
			switch (propertyName)
			{
			case "SubjectProperty":
				result = DataStrings.SubjectProperty;
				break;
			case "TextBody":
				result = DataStrings.TextBody;
				break;
			case "AttachmentContent":
			case "Attachments":
			case "AttachFileName":
			case "AttachLongFileName":
			case "AttachExtension":
			case "DisplayName":
				result = DataStrings.AttachmentContent;
				break;
			case "SentTime":
				result = DataStrings.SentTime;
				break;
			case "ReceivedTime":
				result = DataStrings.ReceivedTime;
				break;
			case "SearchRecipientsTo":
				result = DataStrings.SearchRecipientsTo;
				break;
			case "SearchRecipientsCc":
				result = DataStrings.SearchRecipientsCc;
				break;
			case "SearchRecipientsBcc":
				result = DataStrings.SearchRecipientsBcc;
				break;
			case "SearchRecipients":
				result = DataStrings.SearchRecipients;
				break;
			case "SearchSender":
				result = DataStrings.SearchSender;
				break;
			case "Recipients":
				result = DataStrings.Recipients;
				break;
			case "ItemClass":
				result = DataStrings.ItemClass;
				break;
			case "ToGroupExpansionRecipients":
				result = DataStrings.ToGroupExpansionRecipients;
				break;
			case "CcGroupExpansionRecipients":
				result = DataStrings.CcGroupExpansionRecipients;
				break;
			case "BccGroupExpansionRecipients":
				result = DataStrings.BccGroupExpansionRecipients;
				break;
			case "GroupExpansionRecipients":
				result = DataStrings.GroupExpansionRecipients;
				break;
			}
			return result;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000062B0 File Offset: 0x000044B0
		internal string GenerateInfixString(FilterLanguage language)
		{
			FilterSchema filterSchema;
			switch (language)
			{
			case FilterLanguage.Monad:
				filterSchema = QueryFilter.MonadLanguageSchema;
				goto IL_2C;
			case FilterLanguage.Kql:
				filterSchema = QueryFilter.KqlLanguageSchema;
				goto IL_2C;
			}
			filterSchema = QueryFilter.AdoLanguageSchema;
			IL_2C:
			StringBuilder stringBuilder = new StringBuilder();
			QueryFilter.GenerateInfixString(this, stringBuilder, filterSchema);
			return stringBuilder.ToString();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006300 File Offset: 0x00004500
		public override string ToString()
		{
			int num = this.Size % QueryFilter.filterStringSizeEstimates.Length;
			int num2 = QueryFilter.filterStringSizeEstimates[num];
			StringBuilder stringBuilder = new StringBuilder(num2);
			this.ToString(stringBuilder);
			if (stringBuilder.Length != num2)
			{
				Interlocked.Exchange(ref QueryFilter.filterStringSizeEstimates[num], stringBuilder.Length);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006358 File Offset: 0x00004558
		public virtual IEnumerable<string> Keywords()
		{
			return new List<string>();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000635F File Offset: 0x0000455F
		internal virtual IEnumerable<PropertyDefinition> FilterProperties()
		{
			return new List<PropertyDefinition>();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006366 File Offset: 0x00004566
		public virtual QueryFilter CloneWithPropertyReplacement(IDictionary<PropertyDefinition, PropertyDefinition> propertyMap)
		{
			throw new NotSupportedException("Cannot map filter of type" + base.GetType().ToString());
		}

		// Token: 0x0600015F RID: 351
		public abstract void ToString(StringBuilder sb);

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006382 File Offset: 0x00004582
		public virtual string PropertyName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006385 File Offset: 0x00004585
		internal int Size
		{
			get
			{
				if (this.size == 0)
				{
					this.size = this.GetSize();
				}
				return this.size;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000063A1 File Offset: 0x000045A1
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000063A9 File Offset: 0x000045A9
		internal bool IsAtomic { get; set; }

		// Token: 0x06000164 RID: 356 RVA: 0x000063B2 File Offset: 0x000045B2
		protected virtual int GetSize()
		{
			return 1;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000063B8 File Offset: 0x000045B8
		private static void GenerateInfixString(QueryFilter filter, StringBuilder sb, FilterSchema filterSchema)
		{
			if (filter is CompositeFilter)
			{
				sb.Append("(");
				CompositeFilter compositeFilter = (CompositeFilter)filter;
				int filterCount = compositeFilter.FilterCount;
				for (int i = 0; i < filterCount - 1; i++)
				{
					sb.Append("(");
					QueryFilter.GenerateInfixString(compositeFilter.Filters[i], sb, filterSchema);
					sb.Append(") ");
					if (filter is AndFilter)
					{
						sb.Append(filterSchema.And);
					}
					else if (filter is OrFilter)
					{
						sb.Append(filterSchema.Or);
					}
					sb.Append(" ");
				}
				sb.Append("(");
				QueryFilter.GenerateInfixString(compositeFilter.Filters[filterCount - 1], sb, filterSchema);
				sb.Append("))");
				return;
			}
			if (filter is NotFilter)
			{
				NotFilter notFilter = filter as NotFilter;
				sb.Append(filterSchema.Not);
				sb.Append("(");
				QueryFilter.GenerateInfixString(notFilter.Filter, sb, filterSchema);
				sb.Append(")");
				return;
			}
			if (filter is TextFilter)
			{
				TextFilter textFilter = filter as TextFilter;
				string propertyName = filterSchema.GetPropertyName(textFilter.Property.Name);
				if (!string.IsNullOrEmpty(propertyName))
				{
					sb.Append(propertyName);
					sb.Append(filterSchema.Like);
				}
				if (textFilter.MatchOptions == MatchOptions.FullString || textFilter.MatchOptions == MatchOptions.ExactPhrase || filterSchema.SupportQuotedPrefix)
				{
					sb.Append(filterSchema.QuotationMark);
				}
				if (textFilter.MatchOptions == MatchOptions.Suffix || textFilter.MatchOptions == MatchOptions.SubString)
				{
					sb.Append("*");
				}
				sb.Append(filterSchema.EscapeStringValue(textFilter.Text));
				if (textFilter.MatchOptions == MatchOptions.Prefix || textFilter.MatchOptions == MatchOptions.SubString || textFilter.MatchOptions == MatchOptions.PrefixOnWords)
				{
					sb.Append("*");
				}
				if (textFilter.MatchOptions == MatchOptions.FullString || textFilter.MatchOptions == MatchOptions.ExactPhrase || filterSchema.SupportQuotedPrefix)
				{
					sb.Append(filterSchema.QuotationMark);
					return;
				}
			}
			else
			{
				if (filter is ComparisonFilter)
				{
					ComparisonFilter comparisonFilter = filter as ComparisonFilter;
					sb.Append(filterSchema.GetPropertyName(comparisonFilter.Property.Name));
					sb.Append(filterSchema.GetRelationalOperator(comparisonFilter.ComparisonOperator));
					sb.Append(filterSchema.QuotationMark);
					sb.Append(filterSchema.EscapeStringValue(comparisonFilter.PropertyValue));
					sb.Append(filterSchema.QuotationMark);
					return;
				}
				if (filter is ExistsFilter)
				{
					sb.Append(filterSchema.GetExistsFilter(filter as ExistsFilter));
					return;
				}
				if (filter is FalseFilter)
				{
					sb.Append(filterSchema.GetFalseFilter());
				}
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006660 File Offset: 0x00004860
		private static QueryFilter SimplifyCompositeFilter<TFilter, TOther>(TFilter filter) where TFilter : CompositeFilter where TOther : CompositeFilter
		{
			List<QueryFilter> list = new List<QueryFilter>();
			Stack<QueryFilter> stack = new Stack<QueryFilter>();
			for (int i = filter.Filters.Count - 1; i >= 0; i--)
			{
				stack.Push(filter.Filters[i]);
			}
			while (stack.Count > 0)
			{
				QueryFilter queryFilter = stack.Pop();
				NotFilter notFilter = queryFilter as NotFilter;
				if (queryFilter.IsAtomic || (notFilter != null && notFilter.Filter.IsAtomic))
				{
					if (!list.Contains(queryFilter))
					{
						list.Add(queryFilter);
					}
				}
				else if (notFilter != null)
				{
					if (notFilter.Filter is TOther)
					{
						TOther tother = notFilter.Filter as TOther;
						for (int j = tother.Filters.Count - 1; j >= 0; j--)
						{
							QueryFilter filter2 = tother.Filters[j];
							stack.Push(QueryFilter.NotFilter(filter2));
						}
					}
					else if (notFilter.Filter is NotFilter)
					{
						stack.Push((notFilter.Filter as NotFilter).Filter);
					}
					else
					{
						QueryFilter item = QueryFilter.SimplifyFilter(queryFilter);
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
				}
				else if (queryFilter is TFilter)
				{
					TFilter tfilter = queryFilter as TFilter;
					for (int k = tfilter.Filters.Count - 1; k >= 0; k--)
					{
						QueryFilter item2 = tfilter.Filters[k];
						stack.Push(item2);
					}
				}
				else
				{
					QueryFilter item3 = QueryFilter.SimplifyFilter(queryFilter);
					if (!list.Contains(item3))
					{
						list.Add(item3);
					}
				}
			}
			if (typeof(TFilter).Equals(typeof(AndFilter)))
			{
				return QueryFilter.AndTogether(list.ToArray());
			}
			return QueryFilter.OrTogether(list.ToArray());
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006860 File Offset: 0x00004A60
		internal static QueryFilter SimplifyFilter(QueryFilter filter)
		{
			if (filter == null)
			{
				return null;
			}
			if (filter.IsAtomic)
			{
				return filter;
			}
			if (!(filter is NotFilter) && !(filter is CompositeFilter))
			{
				return filter;
			}
			NotFilter notFilter = filter as NotFilter;
			if (notFilter != null)
			{
				QueryFilter filter2 = notFilter.Filter;
				if (filter2.IsAtomic)
				{
					return filter;
				}
				if (filter2 is NotFilter)
				{
					return QueryFilter.SimplifyFilter(((NotFilter)filter2).Filter);
				}
				if (filter2 is CompositeFilter)
				{
					CompositeFilter compositeFilter = (CompositeFilter)filter2;
					QueryFilter[] array = new QueryFilter[compositeFilter.Filters.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = QueryFilter.NotFilter(compositeFilter.Filters[i]);
					}
					if (filter2 is AndFilter)
					{
						return QueryFilter.SimplifyFilter(QueryFilter.OrTogether(array));
					}
					if (filter2 is OrFilter)
					{
						return QueryFilter.SimplifyFilter(QueryFilter.AndTogether(array));
					}
				}
				if (filter2 is ComparisonFilter)
				{
					ComparisonFilter comparisonFilter = (ComparisonFilter)filter2;
					ComparisonOperator comparisonOperator;
					switch (comparisonFilter.ComparisonOperator)
					{
					case ComparisonOperator.Equal:
						comparisonOperator = ComparisonOperator.NotEqual;
						break;
					case ComparisonOperator.NotEqual:
						comparisonOperator = ComparisonOperator.Equal;
						break;
					case ComparisonOperator.LessThan:
						comparisonOperator = ComparisonOperator.GreaterThanOrEqual;
						break;
					case ComparisonOperator.LessThanOrEqual:
						comparisonOperator = ComparisonOperator.GreaterThan;
						break;
					case ComparisonOperator.GreaterThan:
						comparisonOperator = ComparisonOperator.LessThanOrEqual;
						break;
					case ComparisonOperator.GreaterThanOrEqual:
						comparisonOperator = ComparisonOperator.LessThan;
						break;
					default:
						return filter;
					}
					return new ComparisonFilter(comparisonOperator, comparisonFilter.Property, comparisonFilter.PropertyValue);
				}
				return QueryFilter.NotFilter(QueryFilter.SimplifyFilter(filter2));
			}
			else
			{
				AndFilter andFilter = filter as AndFilter;
				if (andFilter != null)
				{
					return QueryFilter.SimplifyCompositeFilter<AndFilter, OrFilter>(andFilter);
				}
				OrFilter orFilter = filter as OrFilter;
				if (orFilter != null)
				{
					return QueryFilter.SimplifyCompositeFilter<OrFilter, AndFilter>(orFilter);
				}
				return filter;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000069E2 File Offset: 0x00004BE2
		internal static QueryFilter AndTogether(params QueryFilter[] filters)
		{
			return QueryFilter.AndOrTogether((QueryFilter[] list) => new AndFilter(list), filters);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006A0F File Offset: 0x00004C0F
		internal static QueryFilter OrTogether(params QueryFilter[] filters)
		{
			return QueryFilter.AndOrTogether((QueryFilter[] list) => new OrFilter(list), filters);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006A34 File Offset: 0x00004C34
		private static QueryFilter AndOrTogether(Func<QueryFilter[], QueryFilter> ctor, params QueryFilter[] filters)
		{
			if (filters == null)
			{
				throw new ArgumentNullException("filters");
			}
			List<QueryFilter> list = new List<QueryFilter>(filters.Length);
			foreach (QueryFilter queryFilter in filters)
			{
				if (queryFilter != null)
				{
					list.Add(queryFilter);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return ctor(list.ToArray());
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006A9D File Offset: 0x00004C9D
		internal static QueryFilter NotFilter(QueryFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (filter is NotFilter)
			{
				return (filter as NotFilter).Filter;
			}
			return new NotFilter(filter);
		}

		// Token: 0x0400007C RID: 124
		private const int defaultFilterStringSizeEstimate = 1024;

		// Token: 0x0400007D RID: 125
		private const int numberOfFilterStringSizeEstimates = 256;

		// Token: 0x0400007E RID: 126
		public static QueryFilter True = new TrueFilter();

		// Token: 0x0400007F RID: 127
		public static QueryFilter False = new FalseFilter();

		// Token: 0x04000080 RID: 128
		private static FilterSchema MonadLanguageSchema = new MonadFilterSchema();

		// Token: 0x04000081 RID: 129
		private static FilterSchema AdoLanguageSchema = new AdoFilterSchema();

		// Token: 0x04000082 RID: 130
		private static FilterSchema KqlLanguageSchema = new KqlFilterSchema();

		// Token: 0x04000083 RID: 131
		private int size;

		// Token: 0x04000084 RID: 132
		internal static int[] filterStringSizeEstimates = QueryFilter.InitializeFilterStringSizeEstimates();
	}
}
