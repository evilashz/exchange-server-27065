using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Search.KqlParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x0200022F RID: 559
	internal class SearchMailboxCriteria
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x0004415A File Offset: 0x0004235A
		internal SearchMailboxCriteria(CultureInfo queryCulture, string searchQuery, SearchUser[] searchUserScope) : this(queryCulture, searchQuery, null, searchUserScope)
		{
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00044168 File Offset: 0x00042368
		internal SearchMailboxCriteria(CultureInfo queryCulture, string searchQuery, string userQuery, SearchUser[] searchUserScope)
		{
			if (searchUserScope == null)
			{
				throw new ArgumentNullException("searchUserScope");
			}
			if (queryCulture == null)
			{
				throw new ArgumentNullException("queryCulture");
			}
			if (!string.IsNullOrEmpty(searchQuery))
			{
				this.searchQuery = searchQuery;
			}
			if (!string.IsNullOrEmpty(userQuery))
			{
				this.userQuery = userQuery;
			}
			this.searchUserScope = searchUserScope;
			this.queryCulture = queryCulture;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x000441D0 File Offset: 0x000423D0
		internal SearchUser[] SearchUserScope
		{
			get
			{
				return this.searchUserScope;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x000441D8 File Offset: 0x000423D8
		internal CultureInfo QueryCulture
		{
			get
			{
				return this.queryCulture;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x000441E0 File Offset: 0x000423E0
		internal string SearchQuery
		{
			get
			{
				return this.searchQuery;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x000441E8 File Offset: 0x000423E8
		internal QueryFilter SearchFilter
		{
			get
			{
				return this.searchFilter;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x000441F0 File Offset: 0x000423F0
		internal IDictionary<string, QueryFilter> SubSearchFilters
		{
			get
			{
				return this.subfilters;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x000441F8 File Offset: 0x000423F8
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x00044200 File Offset: 0x00042400
		internal bool SearchDumpster
		{
			get
			{
				return this.searchDumpster;
			}
			set
			{
				this.searchDumpster = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00044209 File Offset: 0x00042409
		internal string UserQuery
		{
			get
			{
				return this.userQuery;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00044211 File Offset: 0x00042411
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x00044219 File Offset: 0x00042419
		internal bool SearchDumpsterOnly
		{
			get
			{
				return this.searchDumpsterOnly;
			}
			set
			{
				this.searchDumpsterOnly = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00044222 File Offset: 0x00042422
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x0004422A File Offset: 0x0004242A
		internal bool ExcludePurgesFromDumpster
		{
			get
			{
				return this.excludePurgesFromDumpster;
			}
			set
			{
				this.excludePurgesFromDumpster = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00044233 File Offset: 0x00042433
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x0004423B File Offset: 0x0004243B
		internal bool IncludeUnsearchableItems { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00044244 File Offset: 0x00042444
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x0004424C File Offset: 0x0004244C
		internal bool IncludePersonalArchive { get; set; }

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00044255 File Offset: 0x00042455
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x0004425D File Offset: 0x0004245D
		internal bool IncludeRemoteAccounts { get; set; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00044266 File Offset: 0x00042466
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x0004426E File Offset: 0x0004246E
		internal bool EstimateOnly { get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00044277 File Offset: 0x00042477
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x0004427F File Offset: 0x0004247F
		internal bool ExcludeDuplicateMessages { get; set; }

		// Token: 0x06000F62 RID: 3938 RVA: 0x00044288 File Offset: 0x00042488
		public override string ToString()
		{
			return string.Format("SearchQuery={0}. Culture={1}. SearchUserScope count={2}. SearchDumpster={3}. IncludeUnsearchableItems={4}. IncludePersonalArchive={5}. IncludeRemoteAccounts={6}", new object[]
			{
				this.SearchQuery,
				this.QueryCulture,
				this.searchUserScope.Length,
				this.SearchDumpster,
				this.IncludeUnsearchableItems,
				this.IncludePersonalArchive,
				this.IncludeRemoteAccounts
			});
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00044304 File Offset: 0x00042504
		internal void ResolveQueryFilter(IRecipientSession recipientSession, IConfigurationSession configurationSession)
		{
			SearchMailboxCriteria.RecipientIdentityResolver recipientResolver = null;
			if (recipientSession != null)
			{
				recipientResolver = new SearchMailboxCriteria.RecipientIdentityResolver(recipientSession);
			}
			PolicyTagAdProvider policyTagProvider = null;
			if (configurationSession != null)
			{
				policyTagProvider = new PolicyTagAdProvider(configurationSession);
			}
			if (this.SearchQuery != null)
			{
				this.searchFilter = KqlParser.ParseAndBuildQuery(this.SearchQuery, KqlParser.ParseOption.ImplicitOr | KqlParser.ParseOption.UseCiKeywordOnly | KqlParser.ParseOption.DisablePrefixMatch | KqlParser.ParseOption.AllowShortWildcards | KqlParser.ParseOption.EDiscoveryMode, this.QueryCulture, RescopedAll.Default, recipientResolver, policyTagProvider);
				if (this.searchFilter == null)
				{
					throw new SearchQueryEmptyException();
				}
			}
			SearchMailboxCriteria.Tracer.TraceDebug<QueryFilter>((long)this.GetHashCode(), "SearchMailboxCriteria resolved QueryFilter:{0}", this.searchFilter);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0004437C File Offset: 0x0004257C
		internal StoreId[] GetFolderScope(MailboxSession mailbox)
		{
			StoreId defaultFolderId = mailbox.GetDefaultFolderId(DefaultFolderType.Root);
			StoreId defaultFolderId2 = mailbox.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot);
			StoreId[] array;
			if (!this.SearchDumpsterOnly)
			{
				array = new StoreId[]
				{
					defaultFolderId
				};
			}
			else
			{
				array = new StoreId[0];
			}
			if ((this.SearchDumpsterOnly || this.SearchDumpster) && defaultFolderId2 != null)
			{
				QueryFilter queryFilter = DumpsterFolderHelper.ExcludeAuditFoldersFilter;
				if (this.excludePurgesFromDumpster && SearchUtils.LegalHoldEnabled(mailbox))
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.NotEqual, StoreObjectSchema.DisplayName, "DiscoveryHolds")
					});
					if (mailbox.COWSettings.HoldEnabled() && !mailbox.COWSettings.IsOnlyInPlaceHoldEnabled())
					{
						queryFilter = new AndFilter(new QueryFilter[]
						{
							queryFilter,
							new ComparisonFilter(ComparisonOperator.NotEqual, StoreObjectSchema.DisplayName, "Purges")
						});
					}
				}
				using (Folder folder = Folder.Bind(mailbox, defaultFolderId2))
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, queryFilter, null, new PropertyDefinition[]
					{
						FolderSchema.Id
					}))
					{
						array = array.Concat(queryResult.Enumerator<StoreId>()).ToArray<StoreId>();
					}
				}
			}
			return array;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x000444CC File Offset: 0x000426CC
		internal void GenerateSubQueryFilters(IRecipientSession recipientSession, IConfigurationSession configurationSession)
		{
			if (!string.IsNullOrEmpty(this.userQuery))
			{
				SearchMailboxCriteria.RecipientIdentityResolver recipientResolver = null;
				if (recipientSession != null)
				{
					recipientResolver = new SearchMailboxCriteria.RecipientIdentityResolver(recipientSession);
				}
				PolicyTagAdProvider policyTagProvider = null;
				if (configurationSession != null)
				{
					policyTagProvider = new PolicyTagAdProvider(configurationSession);
				}
				QueryFilter queryFilter = KqlParser.ParseAndBuildQuery(this.userQuery, KqlParser.ParseOption.ImplicitOr | KqlParser.ParseOption.UseCiKeywordOnly | KqlParser.ParseOption.DisablePrefixMatch | KqlParser.ParseOption.AllowShortWildcards | KqlParser.ParseOption.EDiscoveryMode, this.QueryCulture, RescopedAll.Default, recipientResolver, policyTagProvider);
				if (queryFilter == null)
				{
					throw new SearchQueryEmptyException();
				}
				ICollection<QueryFilter> collection = null;
				if (queryFilter != null && queryFilter.GetType() == typeof(OrFilter))
				{
					collection = AqsParser.FlattenQueryFilter(queryFilter);
				}
				if (collection != null && collection.Count > 1)
				{
					string text = this.searchQuery.Replace(this.userQuery, "");
					QueryFilter queryFilter2 = null;
					if (!string.IsNullOrEmpty(text))
					{
						queryFilter2 = KqlParser.ParseAndBuildQuery(text, KqlParser.ParseOption.ImplicitOr | KqlParser.ParseOption.UseCiKeywordOnly | KqlParser.ParseOption.DisablePrefixMatch | KqlParser.ParseOption.AllowShortWildcards | KqlParser.ParseOption.EDiscoveryMode, this.QueryCulture, RescopedAll.Default, recipientResolver, policyTagProvider);
					}
					this.subfilters = new Dictionary<string, QueryFilter>(collection.Count);
					int num = 0;
					foreach (QueryFilter queryFilter3 in collection)
					{
						string keywordPhrase = MailboxDiscoverySearch.GetKeywordPhrase(queryFilter3, this.userQuery, ref num);
						QueryFilter value;
						if (queryFilter2 == null)
						{
							value = queryFilter3;
						}
						else
						{
							value = new AndFilter(new QueryFilter[]
							{
								queryFilter2,
								queryFilter3
							});
						}
						if (!this.subfilters.ContainsKey(keywordPhrase))
						{
							this.subfilters.Add(keywordPhrase, value);
						}
					}
				}
			}
		}

		// Token: 0x04000A7B RID: 2683
		protected static readonly Trace Tracer = ExTraceGlobals.SearchTracer;

		// Token: 0x04000A7C RID: 2684
		private SearchUser[] searchUserScope;

		// Token: 0x04000A7D RID: 2685
		private CultureInfo queryCulture;

		// Token: 0x04000A7E RID: 2686
		private string searchQuery;

		// Token: 0x04000A7F RID: 2687
		private string userQuery = string.Empty;

		// Token: 0x04000A80 RID: 2688
		private bool searchDumpster;

		// Token: 0x04000A81 RID: 2689
		private bool excludePurgesFromDumpster;

		// Token: 0x04000A82 RID: 2690
		private bool searchDumpsterOnly;

		// Token: 0x04000A83 RID: 2691
		private QueryFilter searchFilter;

		// Token: 0x04000A84 RID: 2692
		private IDictionary<string, QueryFilter> subfilters;

		// Token: 0x04000A85 RID: 2693
		internal static readonly ADPropertyDefinition[] RecipientTypeDetailsProperty = new ADPropertyDefinition[]
		{
			ADRecipientSchema.RecipientTypeDetailsValue,
			ADRecipientSchema.RawCapabilities,
			IADSecurityPrincipalSchema.SamAccountName
		};

		// Token: 0x02000230 RID: 560
		private class RecipientIdentityResolver : IRecipientResolver
		{
			// Token: 0x06000F67 RID: 3943 RVA: 0x00044680 File Offset: 0x00042880
			internal RecipientIdentityResolver(IRecipientSession recipientSession)
			{
				this.recipientSession = recipientSession;
			}

			// Token: 0x06000F68 RID: 3944 RVA: 0x00044690 File Offset: 0x00042890
			public string[] Resolve(string identity)
			{
				RecipientIdParameter recipientIdParameter = new RecipientIdParameter(identity);
				IEnumerable<ADRecipient> objects = recipientIdParameter.GetObjects<ADRecipient>(null, this.recipientSession);
				if (objects == null)
				{
					return null;
				}
				List<string> list = new List<string>();
				foreach (ADRecipient adrecipient in objects)
				{
					list.Add(adrecipient.DisplayName);
					list.Add(adrecipient.Alias);
					list.Add(adrecipient.LegacyExchangeDN);
					list.Add(adrecipient.PrimarySmtpAddress.ToString());
				}
				SearchMailboxCriteria.Tracer.TraceDebug<string, List<string>>((long)this.GetHashCode(), "SearchMailboxCriteria resolving Identity {0} to {1}", identity, list);
				return list.ToArray();
			}

			// Token: 0x04000A8B RID: 2699
			private IRecipientSession recipientSession;
		}
	}
}
