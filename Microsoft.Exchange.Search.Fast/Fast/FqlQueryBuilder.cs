using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Ceres.SearchCore.Admin.Config;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000016 RID: 22
	internal static class FqlQueryBuilder
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00007160 File Offset: 0x00005360
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00007167 File Offset: 0x00005367
		public static bool ThrowOnInvalidExpression
		{
			get
			{
				return FqlQueryBuilder.throwOnInvalidExpression;
			}
			set
			{
				FqlQueryBuilder.throwOnInvalidExpression = value;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007170 File Offset: 0x00005370
		public static string ToFqlString(QueryFilter value, CultureInfo culture)
		{
			StringBuilder stringBuilder = new StringBuilder();
			FqlQueryBuilder.AppendToFqlString(value, stringBuilder, culture);
			return stringBuilder.ToString();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007194 File Offset: 0x00005394
		internal static bool IsValidFqlRangeType(IndexSystemFieldType type)
		{
			switch (type)
			{
			case 2:
			case 3:
			case 4:
			case 5:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000071C0 File Offset: 0x000053C0
		private static void AppendToFqlString(QueryFilter value, StringBuilder sb, CultureInfo culture)
		{
			TextFilter textFilter = value as TextFilter;
			if (textFilter != null)
			{
				FqlQueryBuilder.FqlTerm fqlTerm = FqlQueryBuilder.ToFqlTerm(textFilter, culture);
				if (fqlTerm != null)
				{
					fqlTerm.Append(sb);
					return;
				}
				sb.Append(FqlQueryBuilder.InvalidExpression("Attempted to convert unsupported TextFilter to FQL: " + textFilter.ToString()));
				return;
			}
			else
			{
				ComparisonFilter comparisonFilter = value as ComparisonFilter;
				if (comparisonFilter != null)
				{
					FqlQueryBuilder.FqlTerm fqlTerm2 = FqlQueryBuilder.ToFqlTerm(comparisonFilter, culture);
					if (fqlTerm2 != null)
					{
						fqlTerm2.Append(sb);
						return;
					}
					sb.Append(FqlQueryBuilder.InvalidExpression("Attempted to convert unsupported ComparisonFilter to FQL: " + comparisonFilter.ToString()));
					return;
				}
				else
				{
					BetweenFilter betweenFilter = value as BetweenFilter;
					if (betweenFilter != null)
					{
						FqlQueryBuilder.FqlTerm fqlTerm3 = FqlQueryBuilder.ToFqlTerm(betweenFilter, culture);
						if (fqlTerm3 != null)
						{
							fqlTerm3.Append(sb);
							return;
						}
						sb.Append(FqlQueryBuilder.InvalidExpression("Attempted to convert unsupported BetweenFilter to FQL: " + betweenFilter.ToString()));
						return;
					}
					else
					{
						OrFilter orFilter = value as OrFilter;
						if (orFilter != null)
						{
							FqlQueryBuilder.AppendToFqlString(orFilter, sb, culture);
							return;
						}
						AndFilter andFilter = value as AndFilter;
						if (andFilter != null)
						{
							FqlQueryBuilder.AppendToFqlString(andFilter, sb, culture);
							return;
						}
						NotFilter notFilter = value as NotFilter;
						if (notFilter != null)
						{
							FqlQueryBuilder.AppendToFqlString(notFilter, sb, culture);
							return;
						}
						NearFilter nearFilter = value as NearFilter;
						if (nearFilter != null)
						{
							FqlQueryBuilder.AppendToFqlString(nearFilter, sb, culture);
							return;
						}
						sb.Append(FqlQueryBuilder.InvalidExpression("Attempted to convert unsupported QueryFilter to FQL: " + value.ToString()));
						return;
					}
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000072F8 File Offset: 0x000054F8
		private static ICollection<QueryFilter> FlattenNestedAndOr<T>(T compositeFilter) where T : CompositeFilter
		{
			foreach (QueryFilter queryFilter in compositeFilter.Filters)
			{
				if (queryFilter is T && !(queryFilter is BetweenFilter))
				{
					List<QueryFilter> list = new List<QueryFilter>();
					FqlQueryBuilder.FlattenNestedAndOr<T>(compositeFilter, list);
					return list;
				}
			}
			return compositeFilter.Filters;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000737C File Offset: 0x0000557C
		private static void FlattenNestedAndOr<T>(T compositeFilter, List<QueryFilter> list) where T : CompositeFilter
		{
			foreach (QueryFilter queryFilter in compositeFilter.Filters)
			{
				T t = queryFilter as T;
				if (t != null && !(t is BetweenFilter))
				{
					FqlQueryBuilder.FlattenNestedAndOr<T>(t, list);
				}
				else
				{
					list.Add(queryFilter);
				}
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007400 File Offset: 0x00005600
		private static void AppendToFqlString(AndFilter value, StringBuilder sb, CultureInfo culture)
		{
			if (value.FilterCount == 0)
			{
				sb.Append(FqlQueryBuilder.InvalidExpression("Attempted to convert unsupported AndFilter to FQL: " + value.ToString()));
				return;
			}
			List<QueryFilter> list;
			List<QueryFilter> list2;
			FqlQueryBuilder.SeparateNotCriteria(FqlQueryBuilder.FlattenNestedAndOr<AndFilter>(value), out list, out list2);
			if (list2.Count == 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				sb.Append("not(");
				FqlQueryBuilder.WrapIntoCompositeOperator(sb, stringBuilder, FqlQueryBuilder.AppendTermsForAndOr(stringBuilder, list, culture, false), "or");
				sb.Append(")");
				return;
			}
			if (list.Count > 0)
			{
				sb.Append("andnot(");
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			FqlQueryBuilder.WrapIntoCompositeOperator(sb, stringBuilder2, FqlQueryBuilder.AppendTermsForAndOr(stringBuilder2, list2, culture, true), "and");
			if (list.Count > 0)
			{
				sb.Append(", ");
				FqlQueryBuilder.AppendTermsForAndOr(sb, list, culture, false);
				sb.Append(")");
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000074D8 File Offset: 0x000056D8
		private static void AppendToFqlString(OrFilter value, StringBuilder sb, CultureInfo culture)
		{
			if (value.FilterCount == 0)
			{
				sb.Append(FqlQueryBuilder.InvalidExpression("Attempted to convert unsupported OrFilter to FQL: " + value.ToString()));
				return;
			}
			List<QueryFilter> list;
			List<QueryFilter> list2;
			FqlQueryBuilder.SeparateNotCriteria(FqlQueryBuilder.FlattenNestedAndOr<OrFilter>(value), out list, out list2);
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			if (list.Count > 0)
			{
				stringBuilder.Append("not(");
				StringBuilder stringBuilder2 = new StringBuilder();
				FqlQueryBuilder.WrapIntoCompositeOperator(stringBuilder, stringBuilder2, FqlQueryBuilder.AppendTermsForAndOr(stringBuilder2, list, culture, true), "and");
				stringBuilder.Append(")");
				num++;
			}
			if (list2.Count > 0)
			{
				if (num > 0)
				{
					stringBuilder.Append(", ");
				}
				num += FqlQueryBuilder.AppendTermsForAndOr(stringBuilder, list2, culture, false);
			}
			FqlQueryBuilder.WrapIntoCompositeOperator(sb, stringBuilder, num, "or");
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007598 File Offset: 0x00005798
		private static void SeparateNotCriteria(ICollection<QueryFilter> source, out List<QueryFilter> notCriteriaList, out List<QueryFilter> rest)
		{
			notCriteriaList = new List<QueryFilter>(source.Count);
			rest = new List<QueryFilter>(source.Count);
			foreach (QueryFilter queryFilter in source)
			{
				NotFilter notFilter = queryFilter as NotFilter;
				if (notFilter != null)
				{
					notCriteriaList.Add(notFilter.Filter);
				}
				else
				{
					ComparisonFilter comparisonFilter = queryFilter as ComparisonFilter;
					if (comparisonFilter != null && comparisonFilter.ComparisonOperator == ComparisonOperator.NotEqual)
					{
						notCriteriaList.Add(new ComparisonFilter(ComparisonOperator.Equal, comparisonFilter.Property, comparisonFilter.PropertyValue));
					}
					else
					{
						rest.Add(queryFilter);
					}
				}
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007644 File Offset: 0x00005844
		private static void WrapIntoCompositeOperator(StringBuilder targetSb, StringBuilder sourceSb, int numTerms, string op)
		{
			if (numTerms > 1)
			{
				targetSb.Append(op);
				targetSb.Append("(");
				targetSb.Append(sourceSb.ToString());
				targetSb.Append(")");
				return;
			}
			targetSb.Append(sourceSb.ToString());
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007690 File Offset: 0x00005890
		private static int AppendTermsForAndOr(StringBuilder sb, IEnumerable<QueryFilter> nestedCriteria, CultureInfo culture, bool isAnd)
		{
			int num = 0;
			Dictionary<string, HashSet<FqlQueryBuilder.FqlTerm>> dictionary = new Dictionary<string, HashSet<FqlQueryBuilder.FqlTerm>>();
			foreach (QueryFilter queryFilter in nestedCriteria)
			{
				FqlQueryBuilder.FqlTerm fqlTerm = null;
				TextFilter textFilter = queryFilter as TextFilter;
				if (textFilter != null)
				{
					fqlTerm = FqlQueryBuilder.ToFqlTerm(textFilter, culture);
				}
				else
				{
					ComparisonFilter comparisonFilter = queryFilter as ComparisonFilter;
					if (comparisonFilter != null)
					{
						fqlTerm = FqlQueryBuilder.ToFqlTerm(comparisonFilter, culture);
					}
					else
					{
						BetweenFilter betweenFilter = queryFilter as BetweenFilter;
						if (betweenFilter != null)
						{
							fqlTerm = FqlQueryBuilder.ToFqlTerm(betweenFilter, culture);
						}
					}
				}
				if (fqlTerm != null)
				{
					HashSet<FqlQueryBuilder.FqlTerm> hashSet = null;
					if (!dictionary.TryGetValue(fqlTerm.Constraint, out hashSet))
					{
						hashSet = new HashSet<FqlQueryBuilder.FqlTerm>();
						dictionary.Add(fqlTerm.Constraint, hashSet);
					}
					hashSet.Add(fqlTerm);
				}
				else
				{
					if (num > 0)
					{
						sb.Append(", ");
					}
					FqlQueryBuilder.AppendToFqlString(queryFilter, sb, culture);
					num++;
				}
			}
			foreach (HashSet<FqlQueryBuilder.FqlTerm> collection in dictionary.Values)
			{
				List<FqlQueryBuilder.FqlTerm> list = new List<FqlQueryBuilder.FqlTerm>(collection);
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] != null)
					{
						for (int j = 0; j < list.Count; j++)
						{
							if (i != j && list[j] != null && ((isAnd && list[i].IsStricterOrEquivalentTo(list[j])) || (!isAnd && list[j].IsStricterOrEquivalentTo(list[i]))))
							{
								list[j] = null;
							}
						}
					}
				}
				foreach (FqlQueryBuilder.FqlTerm fqlTerm2 in list)
				{
					if (fqlTerm2 != null)
					{
						if (num > 0)
						{
							sb.Append(", ");
						}
						fqlTerm2.Append(sb);
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000078B0 File Offset: 0x00005AB0
		private static void AppendToFqlString(NotFilter value, StringBuilder sb, CultureInfo culture)
		{
			if (value.Filter == null)
			{
				sb.Append(FqlQueryBuilder.InvalidExpression("Attempted to convert unsupported NotFilter to FQL: " + value.ToString()));
				return;
			}
			sb.Append("not(");
			FqlQueryBuilder.AppendToFqlString(value.Filter, sb, culture);
			sb.Append(')');
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007904 File Offset: 0x00005B04
		private static void AppendToFqlString(NearFilter value, StringBuilder sb, CultureInfo culture)
		{
			AndFilter filter = value.Filter;
			if (filter == null || filter.FilterCount == 0)
			{
				sb.Append(FqlQueryBuilder.InvalidExpression("Attempted to convert unsupported NearFilter to FQL: " + value.ToString()));
				return;
			}
			sb.Append(value.Ordered ? "onear(" : "near(");
			foreach (QueryFilter value2 in filter.Filters)
			{
				FqlQueryBuilder.AppendToFqlString(value2, sb, culture);
				sb.Append(", ");
			}
			sb.Append("N=");
			sb.Append(value.Distance);
			sb.Append(")");
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000079D4 File Offset: 0x00005BD4
		private static FqlQueryBuilder.FqlTerm ToFqlTerm(BetweenFilter value, CultureInfo culture)
		{
			FastIndexSystemField fastIndexSystemField;
			if (!InstantSearchSchema.FastIndexSystemFieldsMap.TryGetValue(value.Property, out fastIndexSystemField))
			{
				FqlQueryBuilder.InvalidExpression("Unsupported search property: " + value);
				return null;
			}
			if (value.Left.PropertyValue == null || value.Right.PropertyValue == null)
			{
				return new FqlQueryBuilder.FqlTerm(fastIndexSystemField, "andnot(string(\"FalseCondition\"), string(\"FalseCondition\"))", false);
			}
			if (!FqlQueryBuilder.IsValidFqlRangeType(fastIndexSystemField.Definition.Type))
			{
				FqlQueryBuilder.InvalidExpression("Invalid range property: " + value);
				return null;
			}
			if (!fastIndexSystemField.Definition.Queryable)
			{
				FqlQueryBuilder.InvalidExpression("Unsupported search property: " + value);
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("range({0}, {1}", FqlQueryBuilder.ValueToFqlString(fastIndexSystemField, value.Left.PropertyValue, MatchOptions.ExactPhrase, culture), FqlQueryBuilder.ValueToFqlString(fastIndexSystemField, value.Right.PropertyValue, MatchOptions.ExactPhrase, culture));
			switch (value.Left.ComparisonOperator)
			{
			case ComparisonOperator.GreaterThan:
				stringBuilder.Append(", from=gt");
				break;
			case ComparisonOperator.GreaterThanOrEqual:
				stringBuilder.Append(", from=ge");
				break;
			default:
				FqlQueryBuilder.InvalidExpression(string.Format("Invalid comparison operator {0} on the left side of the comparison", value.Left.ComparisonOperator));
				return null;
			}
			switch (value.Right.ComparisonOperator)
			{
			case ComparisonOperator.LessThan:
				stringBuilder.Append(", to=lt");
				break;
			case ComparisonOperator.LessThanOrEqual:
				stringBuilder.Append(", to=le");
				break;
			default:
				FqlQueryBuilder.InvalidExpression(string.Format("Invalid comparison operator {0} on the left side of the comparison", value.Left.ComparisonOperator));
				return null;
			}
			stringBuilder.Append(")");
			return new FqlQueryBuilder.FqlTerm(fastIndexSystemField, stringBuilder.ToString(), false);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007B80 File Offset: 0x00005D80
		private static FqlQueryBuilder.FqlTerm ToFqlTerm(ComparisonFilter value, CultureInfo culture)
		{
			FastIndexSystemField fastIndexSystemField;
			if (!InstantSearchSchema.FastIndexSystemFieldsMap.TryGetValue(value.Property, out fastIndexSystemField))
			{
				FqlQueryBuilder.InvalidExpression("Unsupported search property: " + value);
				return null;
			}
			if (value.PropertyValue == null || (value.PropertyValue is string && string.IsNullOrWhiteSpace((string)value.PropertyValue)))
			{
				return new FqlQueryBuilder.FqlTerm(fastIndexSystemField, "andnot(string(\"FalseCondition\"), string(\"FalseCondition\"))", value.ComparisonOperator == ComparisonOperator.NotEqual);
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (FqlQueryBuilder.IsValidFqlRangeType(fastIndexSystemField.Definition.Type))
			{
				if (!fastIndexSystemField.Definition.Queryable)
				{
					FqlQueryBuilder.InvalidExpression("Unsupported search property: " + value);
					return null;
				}
				stringBuilder.Append("range(");
				switch (value.ComparisonOperator)
				{
				case ComparisonOperator.Equal:
				case ComparisonOperator.NotEqual:
				{
					string value2 = FqlQueryBuilder.ValueToFqlString(fastIndexSystemField, value.PropertyValue, MatchOptions.ExactPhrase, culture);
					stringBuilder.Append(value2);
					stringBuilder.Append(", ");
					stringBuilder.Append(value2);
					stringBuilder.Append(", from=ge, to=le");
					break;
				}
				case ComparisonOperator.LessThan:
					stringBuilder.Append("min, ");
					stringBuilder.Append(FqlQueryBuilder.ValueToFqlString(fastIndexSystemField, value.PropertyValue, MatchOptions.ExactPhrase, culture));
					stringBuilder.Append(", to=lt");
					break;
				case ComparisonOperator.LessThanOrEqual:
					stringBuilder.Append("min, ");
					stringBuilder.Append(FqlQueryBuilder.ValueToFqlString(fastIndexSystemField, value.PropertyValue, MatchOptions.ExactPhrase, culture));
					stringBuilder.Append(", to=le");
					break;
				case ComparisonOperator.GreaterThan:
					stringBuilder.Append(FqlQueryBuilder.ValueToFqlString(fastIndexSystemField, value.PropertyValue, MatchOptions.ExactPhrase, culture));
					stringBuilder.Append(", max, from=gt");
					break;
				case ComparisonOperator.GreaterThanOrEqual:
					stringBuilder.Append(FqlQueryBuilder.ValueToFqlString(fastIndexSystemField, value.PropertyValue, MatchOptions.ExactPhrase, culture));
					stringBuilder.Append(", max, from=ge");
					break;
				default:
					FqlQueryBuilder.InvalidExpression("Invalid comparison operator: " + value.ToString());
					return null;
				}
				stringBuilder.Append(')');
			}
			else
			{
				if (value.ComparisonOperator != ComparisonOperator.Equal && value.ComparisonOperator != ComparisonOperator.NotEqual)
				{
					FqlQueryBuilder.InvalidExpression("Attempted to convert unsupported ComparisonFilter to FQL: " + value.ToString());
					return null;
				}
				if (fastIndexSystemField.Name == FastIndexSystemSchema.FolderId.Name)
				{
					StoreObjectId storeObjectId = value.PropertyValue as StoreObjectId;
					string value3;
					if (storeObjectId != null)
					{
						byte[] providerLevelItemId = storeObjectId.ProviderLevelItemId;
						if (providerLevelItemId.Length != 46)
						{
							FqlQueryBuilder.InvalidExpression("Attempted to invalid FolderId to FQL: " + value.ToString());
							return null;
						}
						value3 = HexConverter.ByteArrayToHexString(providerLevelItemId, 22, 24);
					}
					else
					{
						byte[] array = value.PropertyValue as byte[];
						if (array == null || (array.Length != 26 && array.Length != 24))
						{
							FqlQueryBuilder.InvalidExpression("Attempted to invalid FolderId to FQL: " + value.ToString());
							return null;
						}
						value3 = HexConverter.ByteArrayToHexString(array, 0, 24);
					}
					stringBuilder.Append(FqlQueryBuilder.ValueToFqlString(fastIndexSystemField, value3, MatchOptions.ExactPhrase, culture));
				}
				else
				{
					stringBuilder.Append(FqlQueryBuilder.ValueToFqlString(fastIndexSystemField, value.PropertyValue, MatchOptions.ExactPhrase, culture));
				}
			}
			return new FqlQueryBuilder.FqlTerm(fastIndexSystemField, stringBuilder.ToString(), value.ComparisonOperator == ComparisonOperator.NotEqual);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007E80 File Offset: 0x00006080
		private static FqlQueryBuilder.FqlTerm ToFqlTerm(TextFilter value, CultureInfo culture)
		{
			FastIndexSystemField scope;
			if (!InstantSearchSchema.FastIndexSystemFieldsMap.TryGetValue(value.Property, out scope))
			{
				throw new NotSupportedException("Unsupported search property: " + value);
			}
			string constraint;
			if (string.IsNullOrWhiteSpace(value.Text))
			{
				constraint = "andnot(string(\"FalseCondition\"), string(\"FalseCondition\"))";
			}
			else
			{
				constraint = FqlQueryBuilder.ValueToFqlString(scope, value.Text, value.MatchOptions, culture);
			}
			return new FqlQueryBuilder.FqlTerm(scope, constraint, false);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007EE4 File Offset: 0x000060E4
		private static string SanitizeSearchString(string input, CultureInfo culture, bool lowerCase)
		{
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			foreach (char c in input)
			{
				char c2 = c;
				if (c2 != '"')
				{
					if (c2 == '\\')
					{
						stringBuilder.Append("\\\\");
					}
					else
					{
						stringBuilder.Append(lowerCase ? char.ToLower(c, culture) : c);
					}
				}
				else
				{
					stringBuilder.Append("\\\"");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007F5D File Offset: 0x0000615D
		private static string InvalidExpression(string reason)
		{
			if (FqlQueryBuilder.throwOnInvalidExpression)
			{
				throw new InvalidOperationException(reason);
			}
			return "andnot(string(\"FalseCondition\"), string(\"FalseCondition\"))";
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007F74 File Offset: 0x00006174
		private static string ValueToFqlString(FastIndexSystemField scope, object value, MatchOptions matchOptions, CultureInfo culture)
		{
			TypeCode typeCode = Type.GetTypeCode(value.GetType());
			if (typeCode != TypeCode.Boolean)
			{
				StringBuilder stringBuilder = new StringBuilder();
				switch (typeCode)
				{
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
					stringBuilder.Append("int(");
					stringBuilder.Append(value.ToString());
					goto IL_26D;
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					stringBuilder.Append("float(");
					stringBuilder.Append(value.ToString());
					goto IL_26D;
				case TypeCode.DateTime:
					stringBuilder.Append("datetime(");
					stringBuilder.Append(((DateTime)value).ToString("s"));
					stringBuilder.Append('Z');
					goto IL_26D;
				case TypeCode.String:
					stringBuilder.Append("string(\"");
					stringBuilder.Append(FqlQueryBuilder.SanitizeSearchString((string)value, culture, scope.Definition.TokenNormalization));
					switch (matchOptions)
					{
					case MatchOptions.SubString:
					case MatchOptions.Prefix:
					case MatchOptions.PrefixOnWords:
						stringBuilder.Append("*\", mode=\"and\"");
						goto IL_26D;
					case MatchOptions.ExactPhrase:
						if (((string)value).Length == 1 && (culture.TwoLetterISOLanguageName == "zh" || culture.TwoLetterISOLanguageName == "ja" || culture.TwoLetterISOLanguageName == "ko" || culture.TwoLetterISOLanguageName == "th" || culture.TwoLetterISOLanguageName == "km" || culture.TwoLetterISOLanguageName == "lo" || culture.TwoLetterISOLanguageName == "ms"))
						{
							stringBuilder.Append("*\", mode=\"and\"");
							goto IL_26D;
						}
						stringBuilder.Append("\"");
						goto IL_26D;
					}
					stringBuilder.Append("\", mode=\"and\"");
					goto IL_26D;
				}
				if (value is ExDateTime)
				{
					stringBuilder.Append("datetime(");
					stringBuilder.Append(((ExDateTime)value).ToString("s"));
					stringBuilder.Append('Z');
				}
				else
				{
					stringBuilder.Append("string(\"");
					stringBuilder.Append(FqlQueryBuilder.SanitizeSearchString((string)value, culture, scope.Definition.TokenNormalization));
					stringBuilder.Append("\", wildcard=off");
				}
				IL_26D:
				stringBuilder.Append(')');
				return stringBuilder.ToString();
			}
			if (!(bool)value)
			{
				return "string(0)";
			}
			return "string(1)";
		}

		// Token: 0x04000087 RID: 135
		internal const string FalseCondition = "andnot(string(\"FalseCondition\"), string(\"FalseCondition\"))";

		// Token: 0x04000088 RID: 136
		private static bool throwOnInvalidExpression;

		// Token: 0x02000017 RID: 23
		private class FqlTerm : IEquatable<FqlQueryBuilder.FqlTerm>
		{
			// Token: 0x06000150 RID: 336 RVA: 0x000081FF File Offset: 0x000063FF
			public FqlTerm(FastIndexSystemField scope, string constraint, bool needsNegation)
			{
				this.scope = scope;
				this.constraint = constraint;
				this.needsNegation = needsNegation;
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000151 RID: 337 RVA: 0x0000821C File Offset: 0x0000641C
			public string Constraint
			{
				get
				{
					return this.constraint;
				}
			}

			// Token: 0x06000152 RID: 338 RVA: 0x00008224 File Offset: 0x00006424
			public void Append(StringBuilder sb)
			{
				if (this.constraint == "andnot(string(\"FalseCondition\"), string(\"FalseCondition\"))")
				{
					sb.Append("andnot(string(\"FalseCondition\"), string(\"FalseCondition\"))");
					return;
				}
				if (this.needsNegation)
				{
					sb.Append("not(");
				}
				if (this.scope.Definition != null && this.scope.Definition.Queryable)
				{
					sb.Append(this.scope.Name);
					sb.Append(":");
				}
				sb.Append(this.constraint);
				if (this.needsNegation)
				{
					sb.Append(")");
				}
			}

			// Token: 0x06000153 RID: 339 RVA: 0x000082C4 File Offset: 0x000064C4
			public bool IsStricterOrEquivalentTo(FqlQueryBuilder.FqlTerm term)
			{
				if (this.needsNegation != term.needsNegation || this.constraint != term.constraint)
				{
					return false;
				}
				FqlQueryBuilder.FqlTerm fqlTerm = this.needsNegation ? term : this;
				FqlQueryBuilder.FqlTerm fqlTerm2 = this.needsNegation ? this : term;
				return fqlTerm.scope.Definition == fqlTerm2.scope.Definition || ((fqlTerm.scope.Definition == null || !fqlTerm.scope.Definition.Queryable) && (fqlTerm2.scope.Definition == null || !fqlTerm2.scope.Definition.Queryable)) || (fqlTerm.scope.Definition != null && fqlTerm.scope.Definition.Searchable && (fqlTerm2.scope.Definition == null || !fqlTerm2.scope.Definition.Queryable));
			}

			// Token: 0x06000154 RID: 340 RVA: 0x000083AC File Offset: 0x000065AC
			public override int GetHashCode()
			{
				int num = this.constraint.GetHashCode();
				string text = (this.scope.Definition == null) ? string.Empty : this.scope.Name;
				num ^= text.GetHashCode();
				return num ^ (this.needsNegation ? 1 : 0);
			}

			// Token: 0x06000155 RID: 341 RVA: 0x000083FE File Offset: 0x000065FE
			public bool Equals(FqlQueryBuilder.FqlTerm other)
			{
				return this.scope.Definition == other.scope.Definition && this.constraint == other.constraint && this.needsNegation == other.needsNegation;
			}

			// Token: 0x04000089 RID: 137
			private readonly FastIndexSystemField scope;

			// Token: 0x0400008A RID: 138
			private readonly string constraint;

			// Token: 0x0400008B RID: 139
			private readonly bool needsNegation;
		}
	}
}
