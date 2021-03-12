using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Ceres.InteractionEngine.Processing.BuiltIn.Parsing;
using Microsoft.Ceres.InteractionEngine.Processing.BuiltIn.Parsing.Kql;
using Microsoft.Ceres.NlpBase.RichTypes.QueryTree;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Search.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search.KqlParser
{
	// Token: 0x02000D08 RID: 3336
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class KqlParser
	{
		// Token: 0x060072D7 RID: 29399 RVA: 0x001FD3CE File Offset: 0x001FB5CE
		public static QueryFilter ParseAndBuildQuery(string query, KqlParser.ParseOption parseOption, CultureInfo cultureInfo, IRecipientResolver recipientResolver, IPolicyTagProvider policyTagProvider)
		{
			return KqlParser.ParseAndBuildQuery(query, parseOption, cultureInfo, RescopedAll.Default, recipientResolver, policyTagProvider);
		}

		// Token: 0x060072D8 RID: 29400 RVA: 0x001FD3DC File Offset: 0x001FB5DC
		public static QueryFilter ParseAndBuildQuery(string query, KqlParser.ParseOption parseOption, CultureInfo cultureInfo, RescopedAll rescopedAll, IRecipientResolver recipientResolver, IPolicyTagProvider policyTagProvider)
		{
			KqlParser kqlParser = new KqlParser();
			LocalizedKeywordMapping mapping = LocalizedKeywordMapping.GetMapping(cultureInfo);
			TreeNode treeNode = kqlParser.Parse(query, parseOption, cultureInfo, mapping);
			QueryFilterBuilder queryFilterBuilder = new QueryFilterBuilder(mapping, parseOption, rescopedAll, recipientResolver, policyTagProvider, cultureInfo);
			queryFilterBuilder.AllowedKeywords = KqlParser.GetSupportedProperties(parseOption);
			QueryFilter queryFilter = queryFilterBuilder.Build(treeNode);
			if (queryFilter == null)
			{
				if ((parseOption & KqlParser.ParseOption.SuppressError) == KqlParser.ParseOption.None)
				{
					throw new ParserException(new ParserErrorInfo(ParserErrorCode.ParserError));
				}
				queryFilter = queryFilterBuilder.BuildAllFilter(query);
			}
			return queryFilter;
		}

		// Token: 0x060072D9 RID: 29401 RVA: 0x001FD448 File Offset: 0x001FB648
		internal TreeNode Parse(string query, KqlParser.ParseOption parseOption, CultureInfo cultureInfo, LocalizedKeywordMapping keywordMapping)
		{
			if (string.IsNullOrEmpty(query))
			{
				throw new ArgumentNullException("query");
			}
			if (cultureInfo == null)
			{
				throw new ArgumentNullException("cultureInfo");
			}
			if ((parseOption & KqlParser.ParseOption.UseBasicKeywordsOnly) != KqlParser.ParseOption.None && (parseOption & KqlParser.ParseOption.UseCiKeywordOnly) != KqlParser.ParseOption.None)
			{
				throw new ArgumentException("GetBasicKeywordOnly can not be combined with UseCIKeywordOnly");
			}
			List<ParserErrorInfo> list = ((parseOption & KqlParser.ParseOption.SuppressError) != KqlParser.ParseOption.None) ? null : new List<ParserErrorInfo>();
			KqlParser kqlParser = new KqlParser();
			ParsingContext parsingContext = new ParsingContext
			{
				CultureInfo = cultureInfo,
				ImplicitAndBehavior = ((parseOption & KqlParser.ParseOption.ImplicitOr) == KqlParser.ParseOption.None),
				PropertyLookup = new PropertyLookup(keywordMapping, KqlParser.GetSupportedProperties(parseOption), list),
				SpecialPropertyHandler = new SpecialPropertyHandler(keywordMapping, list)
			};
			TreeNode treeNode = null;
			try
			{
				KqlParser.VerifyQuery(query);
				query = KqlParser.NormalizeQuery(query);
				treeNode = kqlParser.Parse(query, parsingContext);
			}
			catch (FormatException innerException)
			{
				if ((parseOption & KqlParser.ParseOption.SuppressError) == KqlParser.ParseOption.None)
				{
					throw new ParserException(new ParserErrorInfo(ParserErrorCode.KqlParseException), innerException);
				}
			}
			catch (ParseException innerException2)
			{
				if ((parseOption & KqlParser.ParseOption.SuppressError) == KqlParser.ParseOption.None)
				{
					throw new ParserException(new ParserErrorInfo(ParserErrorCode.KqlParseException), innerException2);
				}
			}
			catch (ParserException ex)
			{
				if ((parseOption & KqlParser.ParseOption.SuppressError) == KqlParser.ParseOption.None)
				{
					throw ex;
				}
			}
			if (list != null && list.Count > 0)
			{
				throw new ParserException(list);
			}
			if ((parseOption & KqlParser.ParseOption.SuppressError) == KqlParser.ParseOption.None && treeNode == null)
			{
				throw new ParserException(new ParserErrorInfo(ParserErrorCode.ParserError));
			}
			return treeNode;
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x001FD58C File Offset: 0x001FB78C
		private static void VerifyQuery(string query)
		{
			try
			{
				Match match = KqlParser.asteriskPattern.Match(query);
				if (match.Success)
				{
					TokenInfo errorToken = null;
					if (match.Groups != null)
					{
						if (match.Groups.Count == 1)
						{
							errorToken = new TokenInfo(match.Groups[0].Index, match.Groups[0].Length);
						}
						else if (match.Groups.Count > 1)
						{
							errorToken = new TokenInfo(match.Groups[1].Index, match.Groups[1].Length);
						}
					}
					throw new ParserException(new ParserErrorInfo(ParserErrorCode.UnexpectedToken, errorToken));
				}
			}
			catch (RegexMatchTimeoutException innerException)
			{
				throw new ParserException(new ParserErrorInfo(ParserErrorCode.KqlParseException, ServerStrings.KqlParserTimeout, null), innerException);
			}
		}

		// Token: 0x060072DB RID: 29403 RVA: 0x001FD660 File Offset: 0x001FB860
		private static string NormalizeQuery(string query)
		{
			query = KqlParser.ReplaceQuotes(query);
			query = KqlParser.ReplaceWhiteSpaces(query);
			return query;
		}

		// Token: 0x060072DC RID: 29404 RVA: 0x001FD673 File Offset: 0x001FB873
		private static string ReplaceWhiteSpaces(string query)
		{
			return Regex.Replace(query, "\\s+", " ");
		}

		// Token: 0x060072DD RID: 29405 RVA: 0x001FD688 File Offset: 0x001FB888
		private static string ReplaceQuotes(string query)
		{
			char[] array = query.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				char c = array[i];
				if (c == '“' || c == '”' || c == '„' || c == '‟' || c == '〝' || c == '〞' || c == '〟' || c == '＂')
				{
					array[i] = '"';
				}
			}
			return new string(array);
		}

		// Token: 0x060072DE RID: 29406 RVA: 0x001FD6F9 File Offset: 0x001FB8F9
		private static HashSet<PropertyKeyword> GetSupportedProperties(KqlParser.ParseOption parseOption)
		{
			if ((parseOption & KqlParser.ParseOption.UseCiKeywordOnly) != KqlParser.ParseOption.None)
			{
				return PropertyKeywordHelper.CiPropertyKeywords;
			}
			if ((parseOption & KqlParser.ParseOption.UseBasicKeywordsOnly) != KqlParser.ParseOption.None)
			{
				return PropertyKeywordHelper.BasicPropertyKeywords;
			}
			return PropertyKeywordHelper.AllPropertyKeywords;
		}

		// Token: 0x04005039 RID: 20537
		private static Regex asteriskPattern = new Regex("(?:^|[\\W-[\\*]]+)(\\*+)(?:[^\"]*\"[^\"]*\")*[^\"]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(10.0));

		// Token: 0x02000D09 RID: 3337
		[Flags]
		public enum ParseOption
		{
			// Token: 0x0400503B RID: 20539
			None = 0,
			// Token: 0x0400503C RID: 20540
			SuppressError = 1,
			// Token: 0x0400503D RID: 20541
			ImplicitOr = 2,
			// Token: 0x0400503E RID: 20542
			UseCiKeywordOnly = 4,
			// Token: 0x0400503F RID: 20543
			DisablePrefixMatch = 8,
			// Token: 0x04005040 RID: 20544
			UseBasicKeywordsOnly = 16,
			// Token: 0x04005041 RID: 20545
			ContentIndexingDisabled = 32,
			// Token: 0x04005042 RID: 20546
			QueryPreserving = 64,
			// Token: 0x04005043 RID: 20547
			AllowShortWildcards = 128,
			// Token: 0x04005044 RID: 20548
			EDiscoveryMode = 256
		}
	}
}
