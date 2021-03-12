using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Search.KqlParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000210 RID: 528
	internal class SearchCriteria
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x0003E978 File Offset: 0x0003CB78
		public SearchCriteria(string queryString, string propertyFilterAqs, CultureInfo culture, SearchType type, IRecipientSession recipientSession, IConfigurationSession configurationSession, Guid queryCorrelationId, List<DefaultFolderType> excludedFolders)
		{
			Util.ThrowOnNull(queryString, "queryString");
			Util.ThrowOnNull(culture, "culture");
			this.queryString = queryString;
			this.queryCulture = culture;
			this.searchType = type;
			this.recipientSession = recipientSession;
			QueryFilter queryFilter = null;
			QueryFilter queryFilter2 = this.GetQueryFilter(queryString, this.recipientSession, configurationSession, out queryFilter);
			if (queryFilter2 == null)
			{
				throw new SearchQueryEmptyException();
			}
			if (propertyFilterAqs != null)
			{
				this.propertyFilterQuery = this.GetQueryFilter(propertyFilterAqs, this.recipientSession, configurationSession, out queryFilter);
				this.finalQueryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter2,
					this.propertyFilterQuery
				});
			}
			else
			{
				this.propertyFilterQuery = null;
				this.finalQueryFilter = queryFilter2;
			}
			int actualKeywordCount = queryFilter.Keywords().Count<string>();
			int maxAllowedKeywords = Factory.Current.GetMaxAllowedKeywords(this.RecipientSession);
			this.ValidateKeywordLimits(actualKeywordCount, maxAllowedKeywords);
			if (this.IsStatisticsSearch)
			{
				this.subFilters = this.GetSubFilters(queryFilter);
				if (this.subFilters != null)
				{
					this.ValidateKeywordLimits(this.subFilters.Count, Factory.Current.GetMaxAllowedKeywordsPerPage(this.RecipientSession));
					Dictionary<string, QueryFilter> dictionary = new Dictionary<string, QueryFilter>(this.subFilters.Count);
					foreach (KeyValuePair<string, QueryFilter> keyValuePair in this.subFilters)
					{
						dictionary.Add(keyValuePair.Key, Util.CreateNewQueryFilterForGroupExpansionRecipientsIfApplicable(keyValuePair.Value));
					}
					foreach (KeyValuePair<string, QueryFilter> keyValuePair2 in dictionary)
					{
						this.subFilters[keyValuePair2.Key] = keyValuePair2.Value;
					}
				}
			}
			this.queryCorrelationId = queryCorrelationId;
			this.excludedFolders = ((excludedFolders == null) ? new List<DefaultFolderType>() : excludedFolders);
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0003EB6C File Offset: 0x0003CD6C
		public string QueryString
		{
			get
			{
				return this.queryString;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0003EB74 File Offset: 0x0003CD74
		public QueryFilter Query
		{
			get
			{
				return this.finalQueryFilter;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0003EB7C File Offset: 0x0003CD7C
		public IDictionary<string, QueryFilter> SubFilters
		{
			get
			{
				return this.subFilters;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0003EB84 File Offset: 0x0003CD84
		public SearchType SearchType
		{
			get
			{
				return this.searchType;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0003EB8C File Offset: 0x0003CD8C
		public bool IsPreviewSearch
		{
			get
			{
				return SearchType.Preview == (this.searchType & SearchType.Preview);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0003EB99 File Offset: 0x0003CD99
		public bool IsStatisticsSearch
		{
			get
			{
				return SearchType.Statistics == (this.searchType & SearchType.Statistics);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0003EBA6 File Offset: 0x0003CDA6
		public CultureInfo QueryCulture
		{
			get
			{
				return this.queryCulture;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0003EBAE File Offset: 0x0003CDAE
		public List<DefaultFolderType> ExcludedFolders
		{
			get
			{
				return this.excludedFolders;
			}
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003EBB8 File Offset: 0x0003CDB8
		private IDictionary<string, QueryFilter> GetSubFilters(QueryFilter filter)
		{
			if (filter == null)
			{
				return null;
			}
			if (!(filter.GetType() == typeof(OrFilter)))
			{
				return null;
			}
			IDictionary<string, QueryFilter> subQueryString = this.GetSubQueryString(AqsParser.FlattenQueryFilter(filter));
			if (subQueryString != null && subQueryString.Count == 1)
			{
				Factory.Current.LocalTaskTracer.TraceInformation<Guid, string>(this.GetHashCode(), 0L, "Correlation Id:{0}. The query filter {1} is an OrFilter, but it only returns one sub filter.", this.queryCorrelationId, filter.ToString());
				return null;
			}
			return subQueryString;
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0003EC27 File Offset: 0x0003CE27
		internal IRecipientSession RecipientSession
		{
			get
			{
				return this.recipientSession;
			}
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003EC30 File Offset: 0x0003CE30
		private void ValidateKeywordLimits(int actualKeywordCount, int maxAllowedKeywords)
		{
			if (actualKeywordCount > maxAllowedKeywords)
			{
				Factory.Current.LocalTaskTracer.TraceInformation(this.GetHashCode(), 0L, "Correlation Id:{0}. Max keywords allowed per statistics search call is {1}, the request for the query:{2}, containted {3} keywords.Failing the search.", new object[]
				{
					this.queryCorrelationId,
					maxAllowedKeywords,
					this.queryString,
					actualKeywordCount
				});
				throw new TooManyKeywordsException(actualKeywordCount, maxAllowedKeywords);
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0003EC98 File Offset: 0x0003CE98
		private IDictionary<string, QueryFilter> GetSubQueryString(ICollection<QueryFilter> filters)
		{
			int maxAllowedKeywords = Factory.Current.GetMaxAllowedKeywords(this.RecipientSession);
			if (filters.Count > maxAllowedKeywords)
			{
				throw new TooManyKeywordsException(filters.Count, maxAllowedKeywords);
			}
			int num = 0;
			Dictionary<string, QueryFilter> dictionary = new Dictionary<string, QueryFilter>(filters.Count);
			foreach (QueryFilter queryFilter in filters)
			{
				string keywordPhrase = MailboxDiscoverySearch.GetKeywordPhrase(queryFilter, this.queryString, ref num);
				QueryFilter value = queryFilter;
				if (this.propertyFilterQuery != null)
				{
					value = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						this.propertyFilterQuery
					});
				}
				if (!dictionary.ContainsKey(keywordPhrase))
				{
					dictionary.Add(keywordPhrase, value);
				}
			}
			return dictionary;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0003ED64 File Offset: 0x0003CF64
		private QueryFilter GetQueryFilter(string queryString, IRecipientSession recipientSession, IConfigurationSession configurationSession, out QueryFilter nonDiscoveryQueryFilter)
		{
			SearchCriteria.RecipientIdentityResolver recipientResolver = null;
			if (recipientSession != null)
			{
				recipientResolver = new SearchCriteria.RecipientIdentityResolver(recipientSession);
			}
			PolicyTagAdProvider policyTagProvider = null;
			if (configurationSession != null)
			{
				policyTagProvider = new PolicyTagAdProvider(configurationSession);
			}
			KqlParser.ParseOption parseOption = KqlParser.ParseOption.ImplicitOr | KqlParser.ParseOption.UseCiKeywordOnly | KqlParser.ParseOption.DisablePrefixMatch | KqlParser.ParseOption.AllowShortWildcards;
			nonDiscoveryQueryFilter = KqlParser.ParseAndBuildQuery(queryString, parseOption, this.queryCulture, RescopedAll.Default, recipientResolver, policyTagProvider);
			parseOption |= KqlParser.ParseOption.EDiscoveryMode;
			return KqlParser.ParseAndBuildQuery(queryString, parseOption, this.queryCulture, RescopedAll.Default, recipientResolver, policyTagProvider);
		}

		// Token: 0x040009DA RID: 2522
		private readonly string queryString;

		// Token: 0x040009DB RID: 2523
		private readonly IRecipientSession recipientSession;

		// Token: 0x040009DC RID: 2524
		private readonly CultureInfo queryCulture;

		// Token: 0x040009DD RID: 2525
		private readonly QueryFilter propertyFilterQuery;

		// Token: 0x040009DE RID: 2526
		private readonly QueryFilter finalQueryFilter;

		// Token: 0x040009DF RID: 2527
		private readonly IDictionary<string, QueryFilter> subFilters;

		// Token: 0x040009E0 RID: 2528
		private readonly SearchType searchType;

		// Token: 0x040009E1 RID: 2529
		private readonly Guid queryCorrelationId;

		// Token: 0x040009E2 RID: 2530
		private readonly List<DefaultFolderType> excludedFolders;

		// Token: 0x02000211 RID: 529
		private class RecipientIdentityResolver : IRecipientResolver
		{
			// Token: 0x06000E5F RID: 3679 RVA: 0x0003EDBA File Offset: 0x0003CFBA
			internal RecipientIdentityResolver(IRecipientSession recipientSession)
			{
				this.recipientSession = recipientSession;
			}

			// Token: 0x06000E60 RID: 3680 RVA: 0x0003EDCC File Offset: 0x0003CFCC
			public string[] Resolve(string identity)
			{
				RecipientIdParameter recipientIdParameter = new RecipientIdParameter(identity);
				IEnumerable<ADRecipient> objects = recipientIdParameter.GetObjects<ADRecipient>(null, this.recipientSession);
				if (objects == null)
				{
					return null;
				}
				List<string> list = new List<string>(4);
				foreach (ADRecipient adrecipient in objects)
				{
					list.Add(adrecipient.DisplayName);
					list.Add(adrecipient.Alias);
					list.Add(adrecipient.LegacyExchangeDN);
					list.Add(adrecipient.PrimarySmtpAddress.ToString());
				}
				return list.ToArray();
			}

			// Token: 0x040009E3 RID: 2531
			private IRecipientSession recipientSession;
		}
	}
}
