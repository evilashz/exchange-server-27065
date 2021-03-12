using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x02000006 RID: 6
	internal static class FqlQueryGenerator
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000036D5 File Offset: 0x000018D5
		public static string ToFqlString(this SearchCriteria value, CultureInfo culture)
		{
			return value.ToFqlString(FqlQueryGenerator.Options.Default, culture);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000036DF File Offset: 0x000018DF
		public static string ToFqlString(this SearchCriteria value, FqlQueryGenerator.Options options, CultureInfo culture)
		{
			return value.ToFql(options, culture).Value;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000036F0 File Offset: 0x000018F0
		public static FqlQuery ToFql(this SearchCriteria value, FqlQueryGenerator.Options options, CultureInfo culture)
		{
			FqlQueryGenerator.FqlQueryBuilder fqlQueryBuilder = new FqlQueryGenerator.FqlQueryBuilder();
			return fqlQueryBuilder.ToFql(value, options, culture);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000370C File Offset: 0x0000190C
		[Conditional("DEBUG")]
		internal static void AssertCanProcess(SearchCriteria value, bool looseCheck)
		{
			bool flag;
			FullTextIndexSchema.Current.GetCriteriaFullTextFlavor(value, Guid.Empty, looseCheck, out flag);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003730 File Offset: 0x00001930
		internal static bool IsValidFqlRangeType(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.DateTime:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x04000015 RID: 21
		private const string FalseCondition = "and(string(\"abc\", not(string(\"abc\"))";

		// Token: 0x02000007 RID: 7
		[Flags]
		public enum Options
		{
			// Token: 0x04000017 RID: 23
			Default = 0,
			// Token: 0x04000018 RID: 24
			LooseCheck = 1
		}

		// Token: 0x02000008 RID: 8
		private class FqlQueryBuilder
		{
			// Token: 0x06000035 RID: 53 RVA: 0x00003781 File Offset: 0x00001981
			public FqlQueryBuilder()
			{
				this.termReplacementMapping = new Dictionary<string, string>();
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00003794 File Offset: 0x00001994
			public FqlQuery ToFql(SearchCriteria value, FqlQueryGenerator.Options options, CultureInfo culture)
			{
				FqlQuery fqlQuery = new FqlQuery();
				this.AppendToFqlString(value, fqlQuery, options, culture);
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, string> keyValuePair in this.termReplacementMapping)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.AppendFormat("{0}={1}", keyValuePair.Value, keyValuePair.Key.Length);
				}
				fqlQuery.TermLength = stringBuilder.ToString();
				return fqlQuery;
			}

			// Token: 0x06000037 RID: 55 RVA: 0x0000383C File Offset: 0x00001A3C
			private static void SeparateNotCriteria(ICollection<SearchCriteria> source, out List<SearchCriteria> notCriteriaList, out List<SearchCriteria> rest)
			{
				notCriteriaList = new List<SearchCriteria>(source.Count);
				rest = new List<SearchCriteria>(source.Count);
				foreach (SearchCriteria searchCriteria in source)
				{
					SearchCriteriaNot searchCriteriaNot = searchCriteria as SearchCriteriaNot;
					if (searchCriteriaNot != null)
					{
						notCriteriaList.Add(searchCriteriaNot.Criteria);
					}
					else
					{
						rest.Add(searchCriteria);
					}
				}
			}

			// Token: 0x06000038 RID: 56 RVA: 0x000038B8 File Offset: 0x00001AB8
			private static void WrapIntoCompositeOperator(FqlQuery targetFqlString, FqlQuery sourceFqlString, int numTerms, string op)
			{
				if (numTerms > 1)
				{
					targetFqlString.Append(op);
					targetFqlString.Append("(");
					targetFqlString.Append(sourceFqlString);
					targetFqlString.Append(")");
					return;
				}
				targetFqlString.Append(sourceFqlString);
			}

			// Token: 0x06000039 RID: 57 RVA: 0x000038EC File Offset: 0x00001AEC
			private static FullTextIndexSchema.FullTextIndexInfo GetFastPropertyInfo(Column column)
			{
				FullTextIndexSchema.FullTextIndexInfo result = null;
				if (!FullTextIndexSchema.Current.IsColumnInFullTextIndex(column, Guid.Empty, out result))
				{
					return null;
				}
				return result;
			}

			// Token: 0x0600003A RID: 58 RVA: 0x00003914 File Offset: 0x00001B14
			private static string SanitizeSearchString(string input)
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
							stringBuilder.Append(c);
						}
					}
					else
					{
						stringBuilder.Append("\\\"");
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0600003B RID: 59 RVA: 0x00003984 File Offset: 0x00001B84
			private static bool CheckValueEmptyString(object value)
			{
				string text = value as string;
				return text != null && string.IsNullOrEmpty(text);
			}

			// Token: 0x0600003C RID: 60 RVA: 0x000039A4 File Offset: 0x00001BA4
			private void AppendToFqlString(SearchCriteria value, FqlQuery fqlQuery, FqlQueryGenerator.Options options, CultureInfo culture)
			{
				SearchCriteriaText searchCriteriaText = value as SearchCriteriaText;
				if (searchCriteriaText != null)
				{
					FqlQueryGenerator.FqlQueryBuilder.FqlTerm fqlTerm = this.ToFqlTerm(searchCriteriaText, options, culture);
					if (fqlTerm != null)
					{
						fqlTerm.Append(fqlQuery);
						return;
					}
					fqlQuery.Append("and(string(\"abc\", not(string(\"abc\"))");
					return;
				}
				else
				{
					SearchCriteriaCompare searchCriteriaCompare = value as SearchCriteriaCompare;
					if (searchCriteriaCompare != null)
					{
						FqlQueryGenerator.FqlQueryBuilder.FqlTerm fqlTerm2 = this.ToFqlTerm(searchCriteriaCompare, options, culture);
						if (fqlTerm2 != null)
						{
							fqlTerm2.Append(fqlQuery);
							return;
						}
						fqlQuery.Append("and(string(\"abc\", not(string(\"abc\"))");
						return;
					}
					else
					{
						SearchCriteriaOr searchCriteriaOr = value as SearchCriteriaOr;
						if (searchCriteriaOr != null)
						{
							this.AppendToFqlString(searchCriteriaOr, fqlQuery, options, culture);
							return;
						}
						SearchCriteriaAnd searchCriteriaAnd = value as SearchCriteriaAnd;
						if (searchCriteriaAnd != null)
						{
							this.AppendToFqlString(searchCriteriaAnd, fqlQuery, options, culture);
							return;
						}
						SearchCriteriaNot searchCriteriaNot = value as SearchCriteriaNot;
						if (searchCriteriaNot != null)
						{
							this.AppendToFqlString(searchCriteriaNot, fqlQuery, options, culture);
							return;
						}
						SearchCriteriaNear searchCriteriaNear = value as SearchCriteriaNear;
						if (searchCriteriaNear != null)
						{
							this.AppendToFqlString(searchCriteriaNear, fqlQuery, options, culture);
							return;
						}
						fqlQuery.Append("and(string(\"abc\", not(string(\"abc\"))");
						return;
					}
				}
			}

			// Token: 0x0600003D RID: 61 RVA: 0x00003A78 File Offset: 0x00001C78
			private void AppendToFqlString(SearchCriteriaAnd value, FqlQuery fqlQuery, FqlQueryGenerator.Options options, CultureInfo culture)
			{
				if (value.NestedCriteria.Length == 0)
				{
					fqlQuery.Append("and(string(\"abc\", not(string(\"abc\"))");
					return;
				}
				List<SearchCriteria> list;
				List<SearchCriteria> list2;
				FqlQueryGenerator.FqlQueryBuilder.SeparateNotCriteria(value.NestedCriteria, out list, out list2);
				if (list2.Count == 0)
				{
					FqlQuery fqlQuery2 = new FqlQuery();
					fqlQuery.Append("not(");
					FqlQueryGenerator.FqlQueryBuilder.WrapIntoCompositeOperator(fqlQuery, fqlQuery2, this.AppendTermsForAndOr(fqlQuery2, list, options, culture, false), "or");
					fqlQuery.Append(")");
					return;
				}
				if (list.Count > 0)
				{
					fqlQuery.Append("andnot(");
				}
				FqlQuery fqlQuery3 = new FqlQuery();
				FqlQueryGenerator.FqlQueryBuilder.WrapIntoCompositeOperator(fqlQuery, fqlQuery3, this.AppendTermsForAndOr(fqlQuery3, list2, options, culture, true), "and");
				if (list.Count > 0)
				{
					fqlQuery.Append(", ");
					FqlQuery fqlQuery4 = new FqlQuery();
					FqlQueryGenerator.FqlQueryBuilder.WrapIntoCompositeOperator(fqlQuery, fqlQuery4, this.AppendTermsForAndOr(fqlQuery4, list, options, culture, false), "or");
					fqlQuery.Append(")");
				}
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00003B5C File Offset: 0x00001D5C
			private void AppendToFqlString(SearchCriteriaOr value, FqlQuery fqlQuery, FqlQueryGenerator.Options options, CultureInfo culture)
			{
				if (value.NestedCriteria.Length == 0)
				{
					fqlQuery.Append("and(string(\"abc\", not(string(\"abc\"))");
					return;
				}
				List<SearchCriteria> list;
				List<SearchCriteria> list2;
				FqlQueryGenerator.FqlQueryBuilder.SeparateNotCriteria(value.NestedCriteria, out list, out list2);
				int num = 0;
				FqlQuery fqlQuery2 = new FqlQuery();
				if (list.Count > 0)
				{
					fqlQuery2.Append("not(");
					FqlQuery fqlQuery3 = new FqlQuery();
					FqlQueryGenerator.FqlQueryBuilder.WrapIntoCompositeOperator(fqlQuery2, fqlQuery3, this.AppendTermsForAndOr(fqlQuery3, list, options, culture, true), "and");
					fqlQuery2.Append(")");
					num++;
				}
				if (list2.Count > 0)
				{
					if (num > 0)
					{
						fqlQuery2.Append(", ");
					}
					num += this.AppendTermsForAndOr(fqlQuery2, list2, options, culture, false);
				}
				FqlQueryGenerator.FqlQueryBuilder.WrapIntoCompositeOperator(fqlQuery, fqlQuery2, num, "or");
			}

			// Token: 0x0600003F RID: 63 RVA: 0x00003C10 File Offset: 0x00001E10
			private int AppendTermsForAndOr(FqlQuery fqlQuery, IEnumerable<SearchCriteria> nestedCriteria, FqlQueryGenerator.Options options, CultureInfo culture, bool isAnd)
			{
				int num = 0;
				Dictionary<string, HashSet<FqlQueryGenerator.FqlQueryBuilder.FqlTerm>> dictionary = new Dictionary<string, HashSet<FqlQueryGenerator.FqlQueryBuilder.FqlTerm>>();
				foreach (SearchCriteria searchCriteria in nestedCriteria)
				{
					FqlQueryGenerator.FqlQueryBuilder.FqlTerm fqlTerm = null;
					SearchCriteriaText searchCriteriaText = searchCriteria as SearchCriteriaText;
					if (searchCriteriaText != null)
					{
						fqlTerm = this.ToFqlTerm(searchCriteriaText, options, culture);
					}
					else
					{
						SearchCriteriaCompare searchCriteriaCompare = searchCriteria as SearchCriteriaCompare;
						if (searchCriteriaCompare != null)
						{
							fqlTerm = this.ToFqlTerm(searchCriteriaCompare, options, culture);
						}
					}
					if (fqlTerm != null)
					{
						HashSet<FqlQueryGenerator.FqlQueryBuilder.FqlTerm> hashSet = null;
						if (!dictionary.TryGetValue(fqlTerm.FqlConstraint.Value, out hashSet))
						{
							hashSet = new HashSet<FqlQueryGenerator.FqlQueryBuilder.FqlTerm>();
							dictionary.Add(fqlTerm.FqlConstraint.Value, hashSet);
						}
						hashSet.Add(fqlTerm);
					}
					else
					{
						if (num > 0)
						{
							fqlQuery.Append(", ");
						}
						this.AppendToFqlString(searchCriteria, fqlQuery, options, culture);
						num++;
					}
				}
				foreach (HashSet<FqlQueryGenerator.FqlQueryBuilder.FqlTerm> collection in dictionary.Values)
				{
					List<FqlQueryGenerator.FqlQueryBuilder.FqlTerm> list = new List<FqlQueryGenerator.FqlQueryBuilder.FqlTerm>(collection);
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
					foreach (FqlQueryGenerator.FqlQueryBuilder.FqlTerm fqlTerm2 in list)
					{
						if (fqlTerm2 != null)
						{
							if (num > 0)
							{
								fqlQuery.Append(", ");
							}
							fqlTerm2.Append(fqlQuery);
							num++;
						}
					}
				}
				return num;
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00003E50 File Offset: 0x00002050
			private void AppendToFqlString(SearchCriteriaNot value, FqlQuery fqlQuery, FqlQueryGenerator.Options options, CultureInfo culture)
			{
				if (value.Criteria == null)
				{
					fqlQuery.Append("and(string(\"abc\", not(string(\"abc\"))");
					return;
				}
				fqlQuery.Append("not(");
				this.AppendToFqlString(value.Criteria, fqlQuery, options, culture);
				fqlQuery.Append(')');
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00003E8C File Offset: 0x0000208C
			private void AppendToFqlString(SearchCriteriaNear value, FqlQuery fqlString, FqlQueryGenerator.Options options, CultureInfo culture)
			{
				if (value.Criteria == null || value.Criteria.NestedCriteria.Length == 0)
				{
					fqlString.Append("and(string(\"abc\", not(string(\"abc\"))");
					return;
				}
				fqlString.Append(value.Ordered ? "onear(" : "near(");
				foreach (SearchCriteria value2 in value.Criteria.NestedCriteria)
				{
					this.AppendToFqlString(value2, fqlString, options, culture);
					fqlString.Append(", ");
				}
				fqlString.Append("N=");
				fqlString.Append(value.Distance.ToString());
				fqlString.Append(")");
			}

			// Token: 0x06000042 RID: 66 RVA: 0x00003F34 File Offset: 0x00002134
			private FqlQueryGenerator.FqlQueryBuilder.FqlTerm ToFqlTerm(SearchCriteriaCompare value, FqlQueryGenerator.Options options, CultureInfo culture)
			{
				ConstantColumn constantColumn = value.Rhs as ConstantColumn;
				if (!(constantColumn != null) || constantColumn.Value == null)
				{
					return null;
				}
				FullTextIndexSchema.FullTextIndexInfo fastPropertyInfo = FqlQueryGenerator.FqlQueryBuilder.GetFastPropertyInfo(value.Lhs);
				if (FqlQueryGenerator.FqlQueryBuilder.CheckValueEmptyString(constantColumn.Value))
				{
					return null;
				}
				FqlQuery fqlQuery = new FqlQuery();
				if (FqlQueryGenerator.IsValidFqlRangeType(constantColumn.Type))
				{
					if (fastPropertyInfo.Definition == null || !fastPropertyInfo.Definition.Queryable)
					{
						return null;
					}
					fqlQuery.Append("range(");
					switch (value.RelOp)
					{
					case SearchCriteriaCompare.SearchRelOp.Equal:
					case SearchCriteriaCompare.SearchRelOp.NotEqual:
					{
						FqlQuery fqlString = this.ValueToFqlString(constantColumn.Value, SearchCriteriaText.SearchTextFullness.PhraseMatch, options, culture);
						fqlQuery.Append(fqlString);
						fqlQuery.Append(", ");
						fqlQuery.Append(fqlString);
						fqlQuery.Append(", from=ge, to=le");
						break;
					}
					case SearchCriteriaCompare.SearchRelOp.LessThan:
						fqlQuery.Append("min, ");
						fqlQuery.Append(this.ValueToFqlString(constantColumn.Value, SearchCriteriaText.SearchTextFullness.PhraseMatch, options, culture));
						fqlQuery.Append(", to=lt");
						break;
					case SearchCriteriaCompare.SearchRelOp.LessThanEqual:
						fqlQuery.Append("min, ");
						fqlQuery.Append(this.ValueToFqlString(constantColumn.Value, SearchCriteriaText.SearchTextFullness.PhraseMatch, options, culture));
						fqlQuery.Append(", to=le");
						break;
					case SearchCriteriaCompare.SearchRelOp.GreaterThan:
						fqlQuery.Append(this.ValueToFqlString(constantColumn.Value, SearchCriteriaText.SearchTextFullness.PhraseMatch, options, culture));
						fqlQuery.Append(", max, from=gt");
						break;
					case SearchCriteriaCompare.SearchRelOp.GreaterThanEqual:
						fqlQuery.Append(this.ValueToFqlString(constantColumn.Value, SearchCriteriaText.SearchTextFullness.PhraseMatch, options, culture));
						fqlQuery.Append(", max, from=ge");
						break;
					}
					fqlQuery.Append(')');
				}
				else
				{
					if (value.RelOp != SearchCriteriaCompare.SearchRelOp.Equal && value.RelOp != SearchCriteriaCompare.SearchRelOp.NotEqual)
					{
						return null;
					}
					if (fastPropertyInfo.Definition != null && fastPropertyInfo.Definition == FastIndexSystemSchema.FolderId.Definition)
					{
						byte[] array = constantColumn.Value as byte[];
						if (array != null && array.Length == 26)
						{
							fqlQuery.Append("string(\"");
							string text = HexConverter.ByteArrayToHexString(array, 0, 24);
							fqlQuery.AppendValue(text, this.GetTermReplacements(text));
							fqlQuery.Append("\")");
						}
						else
						{
							fqlQuery.Append(this.ValueToFqlString(constantColumn.Value, SearchCriteriaText.SearchTextFullness.PhraseMatch, options, culture));
						}
					}
					else
					{
						fqlQuery.Append(this.ValueToFqlString(constantColumn.Value, SearchCriteriaText.SearchTextFullness.PhraseMatch, options, culture));
					}
				}
				return new FqlQueryGenerator.FqlQueryBuilder.FqlTerm(fastPropertyInfo, fqlQuery, value.RelOp == SearchCriteriaCompare.SearchRelOp.NotEqual);
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00004188 File Offset: 0x00002388
			private FqlQueryGenerator.FqlQueryBuilder.FqlTerm ToFqlTerm(SearchCriteriaText value, FqlQueryGenerator.Options options, CultureInfo culture)
			{
				ConstantColumn constantColumn = value.Rhs as ConstantColumn;
				if (!(constantColumn != null) || constantColumn.Value == null)
				{
					return null;
				}
				FullTextIndexSchema.FullTextIndexInfo fastPropertyInfo = FqlQueryGenerator.FqlQueryBuilder.GetFastPropertyInfo(value.Lhs);
				if (FqlQueryGenerator.FqlQueryBuilder.CheckValueEmptyString(constantColumn.Value))
				{
					return null;
				}
				return new FqlQueryGenerator.FqlQueryBuilder.FqlTerm(fastPropertyInfo, this.ValueToFqlString(constantColumn.Value, value.FullnessFlags, options, culture), false);
			}

			// Token: 0x06000044 RID: 68 RVA: 0x000041EC File Offset: 0x000023EC
			private string GetTermReplacements(string term)
			{
				string text;
				if (!this.termReplacementMapping.TryGetValue(term, out text))
				{
					if (term.IndexOf("31febf7b418e44878df6e5623e37c828", StringComparison.OrdinalIgnoreCase) >= 0)
					{
						text = term;
					}
					else
					{
						text = string.Format("term{0}", this.termReplacementMapping.Count + 1);
						this.termReplacementMapping[term] = text;
					}
				}
				return text;
			}

			// Token: 0x06000045 RID: 69 RVA: 0x00004248 File Offset: 0x00002448
			private FqlQuery ValueToFqlString(object value, SearchCriteriaText.SearchTextFullness flags, FqlQueryGenerator.Options options, CultureInfo culture)
			{
				FqlQuery fqlQuery = new FqlQuery();
				string text;
				switch (Type.GetTypeCode(value.GetType()))
				{
				case TypeCode.Boolean:
					fqlQuery.Append("string(");
					text = (((bool)value) ? 1 : 0).GetAsString<int>();
					fqlQuery.AppendValue(text, this.GetTermReplacements(text));
					fqlQuery.Append(")");
					return fqlQuery;
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
					fqlQuery.Append("int(");
					text = value.GetAsString<object>();
					fqlQuery.AppendValue(text, this.GetTermReplacements(text));
					goto IL_26A;
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					fqlQuery.Append("float(");
					text = value.GetAsString<object>();
					fqlQuery.AppendValue(text, this.GetTermReplacements(text));
					goto IL_26A;
				case TypeCode.DateTime:
					fqlQuery.Append("datetime(");
					text = ((DateTime)value).ToString("s");
					fqlQuery.AppendValue(text, this.GetTermReplacements(text));
					fqlQuery.Append('Z');
					goto IL_26A;
				case TypeCode.String:
					fqlQuery.Append("string(\"");
					text = FqlQueryGenerator.FqlQueryBuilder.SanitizeSearchString((string)value);
					fqlQuery.AppendValue(text, this.GetTermReplacements(text));
					if ((ushort)(flags & SearchCriteriaText.SearchTextFullness.PhraseMatch) == 32)
					{
						if (!(culture.TwoLetterISOLanguageName == "zh") && !(culture.TwoLetterISOLanguageName == "ja") && !(culture.TwoLetterISOLanguageName == "ko") && !(culture.TwoLetterISOLanguageName == "th") && !(culture.TwoLetterISOLanguageName == "km") && !(culture.TwoLetterISOLanguageName == "lo") && !(culture.TwoLetterISOLanguageName == "my"))
						{
							fqlQuery.Append("\"");
							goto IL_26A;
						}
						fqlQuery.Append((((string)value).Length == 1) ? "*\", mode=\"and\"" : "\"");
						goto IL_26A;
					}
					else
					{
						if ((ushort)(flags & SearchCriteriaText.SearchTextFullness.SubString) == 1 || (ushort)(flags & SearchCriteriaText.SearchTextFullness.Prefix) == 2 || (ushort)(flags & SearchCriteriaText.SearchTextFullness.PrefixOnAnyWord) == 16)
						{
							fqlQuery.Append("*\", mode=\"and\"");
							goto IL_26A;
						}
						fqlQuery.Append("\", mode=\"and\"");
						goto IL_26A;
					}
					break;
				}
				fqlQuery.Append("string(\"");
				text = FqlQueryGenerator.FqlQueryBuilder.SanitizeSearchString(value.GetAsString<object>());
				fqlQuery.AppendValue(text, this.GetTermReplacements(text));
				fqlQuery.Append("\", wildcard=off");
				IL_26A:
				fqlQuery.Append(')');
				return fqlQuery;
			}

			// Token: 0x04000019 RID: 25
			private Dictionary<string, string> termReplacementMapping;

			// Token: 0x02000009 RID: 9
			private class FqlTerm : IEquatable<FqlQueryGenerator.FqlQueryBuilder.FqlTerm>
			{
				// Token: 0x06000046 RID: 70 RVA: 0x000044C8 File Offset: 0x000026C8
				public FqlTerm(FullTextIndexSchema.FullTextIndexInfo scope, FqlQuery constraint, bool needsNegation)
				{
					this.scope = scope;
					this.constraint = constraint;
					this.needsNegation = needsNegation;
				}

				// Token: 0x1700000E RID: 14
				// (get) Token: 0x06000047 RID: 71 RVA: 0x000044E5 File Offset: 0x000026E5
				public FqlQuery FqlConstraint
				{
					get
					{
						return this.constraint;
					}
				}

				// Token: 0x06000048 RID: 72 RVA: 0x000044F0 File Offset: 0x000026F0
				public void Append(FqlQuery fqlString)
				{
					if (this.needsNegation)
					{
						fqlString.Append("not(");
					}
					if (this.scope.Definition != null && this.scope.Definition.Queryable)
					{
						fqlString.Append(this.scope.FastPropertyName);
						fqlString.Append(":");
					}
					fqlString.Append(this.FqlConstraint);
					if (this.needsNegation)
					{
						fqlString.Append(")");
					}
				}

				// Token: 0x06000049 RID: 73 RVA: 0x0000456C File Offset: 0x0000276C
				public bool IsStricterOrEquivalentTo(FqlQueryGenerator.FqlQueryBuilder.FqlTerm term)
				{
					if (this.needsNegation != term.needsNegation || this.FqlConstraint.Value != term.FqlConstraint.Value)
					{
						return false;
					}
					FqlQueryGenerator.FqlQueryBuilder.FqlTerm fqlTerm = this.needsNegation ? term : this;
					FqlQueryGenerator.FqlQueryBuilder.FqlTerm fqlTerm2 = this.needsNegation ? this : term;
					return fqlTerm.scope.Definition == fqlTerm2.scope.Definition || ((fqlTerm.scope.Definition == null || !fqlTerm.scope.Definition.Queryable) && (fqlTerm2.scope.Definition == null || !fqlTerm2.scope.Definition.Queryable)) || (fqlTerm.scope.Definition != null && fqlTerm.scope.Definition.Searchable && (fqlTerm2.scope.Definition == null || !fqlTerm2.scope.Definition.Queryable));
				}

				// Token: 0x0600004A RID: 74 RVA: 0x00004660 File Offset: 0x00002860
				public override int GetHashCode()
				{
					int num = this.FqlConstraint.Value.GetHashCode();
					string text = (this.scope.Definition == null) ? string.Empty : this.scope.FastPropertyName;
					num ^= text.GetHashCode();
					return num ^ (this.needsNegation ? 1 : 0);
				}

				// Token: 0x0600004B RID: 75 RVA: 0x000046B8 File Offset: 0x000028B8
				public bool Equals(FqlQueryGenerator.FqlQueryBuilder.FqlTerm other)
				{
					return this.scope.Definition == other.scope.Definition && this.FqlConstraint.Value == other.FqlConstraint.Value && this.needsNegation == other.needsNegation;
				}

				// Token: 0x0400001A RID: 26
				private readonly FullTextIndexSchema.FullTextIndexInfo scope;

				// Token: 0x0400001B RID: 27
				private readonly FqlQuery constraint;

				// Token: 0x0400001C RID: 28
				private readonly bool needsNegation;
			}
		}
	}
}
