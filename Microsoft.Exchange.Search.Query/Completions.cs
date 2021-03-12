using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Ceres.CoreServices.Services.Package;
using Microsoft.Ceres.NlpBase.Dictionaries;
using Microsoft.Ceres.NlpBase.DictionaryInterface;
using Microsoft.Ceres.NlpBase.DictionaryInterface.Resource;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000002 RID: 2
	internal class Completions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public Completions(ISearchServiceConfig config)
		{
			this.config = config;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F2 File Offset: 0x000002F2
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020FA File Offset: 0x000002FA
		public QueryCompletion TopNCompletions { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002103 File Offset: 0x00000303
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000210B File Offset: 0x0000030B
		public bool ReloadTopN { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002114 File Offset: 0x00000314
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000211C File Offset: 0x0000031C
		internal int MaximumSuggestionsCount
		{
			get
			{
				return this.maximumSuggestionsCount;
			}
			set
			{
				this.maximumSuggestionsCount = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002125 File Offset: 0x00000325
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000212D File Offset: 0x0000032D
		internal Completions.CompletionsDictionaryState TopNState { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002138 File Offset: 0x00000338
		private static TopNManagementClient CompilationClient
		{
			get
			{
				if (Completions.compilationClient == null)
				{
					lock (Completions.StaticConstructionLockObject)
					{
						if (Completions.compilationClient == null)
						{
							Completions.compilationClient = new TopNManagementClient(SearchConfig.Instance);
						}
					}
				}
				return Completions.compilationClient;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000219C File Offset: 0x0000039C
		private QueryCompletion Spelling
		{
			get
			{
				if (Completions.spellingCompletions == null)
				{
					lock (Completions.StaticConstructionLockObject)
					{
						Thread.MemoryBarrier();
						if (Completions.spellingCompletions == null)
						{
							Completions.spellingCompletions = this.GetAssemblyBasedCompletionDictionary(this.config.SpellingMaximumEditDistance, this.config.SpellingMinimalSimilarity, this.config.SpellingExactPrefixLength, "Microsoft.System_Dictionaries_Spellcheck");
						}
					}
				}
				return Completions.spellingCompletions;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002220 File Offset: 0x00000420
		private QueryCompletion Synonyms
		{
			get
			{
				if (Completions.synonymsCompletions == null)
				{
					lock (Completions.StaticConstructionLockObject)
					{
						Thread.MemoryBarrier();
						if (Completions.synonymsCompletions == null)
						{
							Completions.synonymsCompletions = this.GetAssemblyBasedCompletionDictionary(this.config.SynonymMaximumEditDistance, this.config.SynonymMinimalSimilarity, this.config.SynonymExactPrefixLength, "microsoft.synonymsdictionary");
						}
					}
				}
				return Completions.synonymsCompletions;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022A4 File Offset: 0x000004A4
		private QueryCompletion Nicknames
		{
			get
			{
				if (Completions.nicknamesCompletions == null)
				{
					lock (Completions.StaticConstructionLockObject)
					{
						Thread.MemoryBarrier();
						if (Completions.nicknamesCompletions == null)
						{
							Completions.nicknamesCompletions = this.GetAssemblyBasedCompletionDictionary(this.config.NicknameMaximumEditDistance, this.config.NicknameMinimalSimilarity, this.config.NicknameExactPrefixLength, "microsoft.nicknamesdictionary");
						}
					}
				}
				return Completions.nicknamesCompletions;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002328 File Offset: 0x00000528
		public void InitializeTopN(MailboxSession mailboxSession)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			this.ReloadTopN = false;
			this.TopNState = Completions.CompletionsDictionaryState.Unknown;
			try
			{
				if (this.config.TopNEnabled)
				{
					using (UserConfiguration searchDictionaryItem = SearchDictionary.GetSearchDictionaryItem(mailboxSession, "Search.TopN"))
					{
						using (Stream stream = searchDictionaryItem.GetStream())
						{
							lock (this.topNlockObject)
							{
								using (Stream stream2 = SearchDictionary.InitializeFrom(stream, 1))
								{
									if (stream2 != null)
									{
										long length = stream2.Length;
										this.TopNCompletions = this.GetStreamBasedCompletionDictionary(this.config.TopNMaximumEditDistance, this.config.TopNMinimalSimilarity, this.config.TopNExactPrefixLength, "Search.TopN", stream2);
										if (ExDateTime.UtcNow.Subtract(searchDictionaryItem.LastModifiedTime) > this.config.TopNDictionaryAgeThreshold)
										{
											this.TopNState = Completions.CompletionsDictionaryState.Stale;
										}
										else
										{
											this.TopNState = Completions.CompletionsDictionaryState.Initialized;
										}
										list.Add(new KeyValuePair<string, object>("LastModified Time", searchDictionaryItem.LastModifiedTime));
										list.Add(new KeyValuePair<string, object>("Dictionary Size", length));
										list.Add(new KeyValuePair<string, object>("Max Edit Distance", this.config.TopNMaximumEditDistance));
										list.Add(new KeyValuePair<string, object>("Minimal Similarity", this.config.TopNMinimalSimilarity));
										list.Add(new KeyValuePair<string, object>("Exact Prefix Length", this.config.TopNExactPrefixLength));
									}
								}
							}
						}
					}
					if (this.TopNState == Completions.CompletionsDictionaryState.Stale || this.TopNState == Completions.CompletionsDictionaryState.Unknown)
					{
						list.Add(new KeyValuePair<string, object>("Compilation", "Requested"));
						Completions.CompilationClient.BeginExecuteFlow(mailboxSession.MdbGuid, mailboxSession.MailboxGuid, null, new AsyncCallback(this.TopNDictionaryCompilationCallback));
					}
				}
				else
				{
					this.TopNState = Completions.CompletionsDictionaryState.Disabled;
				}
			}
			catch (OutOfMemoryException)
			{
				throw;
			}
			catch (StackOverflowException)
			{
				throw;
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception value)
			{
				list.Add(new KeyValuePair<string, object>("Failure", value));
				this.TopNState = Completions.CompletionsDictionaryState.Failed;
			}
			list.Add(new KeyValuePair<string, object>("Initialization state", this.TopNState));
			Interlocked.Exchange<List<KeyValuePair<string, object>>>(ref this.topNInitializationStatistics, list);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000262C File Offset: 0x0000082C
		public List<KeyValuePair<string, object>> ReadAndResetTopNInitializationStatistics()
		{
			return Interlocked.Exchange<List<KeyValuePair<string, object>>>(ref this.topNInitializationStatistics, null);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000263C File Offset: 0x0000083C
		public IDictionary<string, QuerySuggestion> GetSuggestions(string query, QuerySuggestionSources sources, string languageIdentifier)
		{
			Dictionary<string, QuerySuggestion> dictionary = new Dictionary<string, QuerySuggestion>();
			this.AddSuggestions(dictionary, query, sources, languageIdentifier);
			return dictionary;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000265C File Offset: 0x0000085C
		public List<KeyValuePair<string, object>> AddSuggestions(IDictionary<string, QuerySuggestion> suggestions, string query, QuerySuggestionSources sources, string languageIdentifier)
		{
			double num = 0.8999;
			List<KeyValuePair<string, object>> list = null;
			if (this.config.TopNEnabled)
			{
				if (this.TopNState == Completions.CompletionsDictionaryState.Initialized || this.TopNState == Completions.CompletionsDictionaryState.Stale)
				{
					if ((sources & QuerySuggestionSources.TopN) != QuerySuggestionSources.None)
					{
						int count = suggestions.Count;
						this.AddSingleSourceSuggestions(suggestions, query, QuerySuggestionSources.TopN, languageIdentifier, ref num);
						if (list == null)
						{
							list = new List<KeyValuePair<string, object>>(1);
						}
						list.Add(new KeyValuePair<string, object>("TopNSuggestions", "Count: " + (suggestions.Count - count)));
					}
				}
				else
				{
					if (list == null)
					{
						list = new List<KeyValuePair<string, object>>(1);
					}
					list.Add(new KeyValuePair<string, object>("TopNSuggestions", "Skipped. Reason: " + this.TopNState));
				}
			}
			if (this.config.SpellingSuggestionsEnabled && (sources & QuerySuggestionSources.Spelling) != QuerySuggestionSources.None)
			{
				this.AddSingleSourceSuggestions(suggestions, query, QuerySuggestionSources.Spelling, languageIdentifier, ref num);
			}
			if (this.config.SynonymSuggestionsEnabled && (sources & QuerySuggestionSources.Synonyms) != QuerySuggestionSources.None)
			{
				this.AddSingleSourceSuggestions(suggestions, query, QuerySuggestionSources.Synonyms, languageIdentifier, ref num);
			}
			if (this.config.NicknameSuggestionsEnabled && (sources & QuerySuggestionSources.Nicknames) != QuerySuggestionSources.None)
			{
				this.AddSingleSourceSuggestions(suggestions, query, QuerySuggestionSources.Nicknames, languageIdentifier, ref num);
			}
			return list;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002774 File Offset: 0x00000974
		internal QueryCompletion GetStreamBasedCompletionDictionary(int maximumEditDistance, double minimalSimilarity, int exactPrefixLength, string dictionaryName, Stream stream)
		{
			this.InitializeStaticResourcesIfNeeded();
			ICompletion item = CompletionFactory.Instance.Get(1, dictionaryName, stream, maximumEditDistance, minimalSimilarity, exactPrefixLength, 180);
			return new QueryCompletion(new List<ICompletion>
			{
				item
			});
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027B4 File Offset: 0x000009B4
		private void AddSingleSourceSuggestions(IDictionary<string, QuerySuggestion> suggestions, string query, QuerySuggestionSources source, string languageIdentifier, ref double rank)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"language",
					languageIdentifier
				}
			};
			string text = string.Empty;
			string text2 = string.Empty;
			IEnumerable<ICompletionResult> enumerable = null;
			char[] anyOf = Completions.DefaultExclusionChars;
			switch (source)
			{
			case QuerySuggestionSources.Spelling:
				enumerable = this.Spelling.GetCompletions(query, 0, dictionary, this.config.MaxCompletionTraversalCount, null, null, null);
				break;
			case QuerySuggestionSources.RecentSearches | QuerySuggestionSources.Spelling:
				break;
			case QuerySuggestionSources.Synonyms:
				text = "synonyms";
				enumerable = this.Synonyms.GetCompletions(query, 0, dictionary, this.config.MaxCompletionTraversalCount, null, null, null);
				break;
			default:
				if (source != QuerySuggestionSources.Nicknames)
				{
					if (source == QuerySuggestionSources.TopN)
					{
						if (this.config.FinalWordSuggestionsEnabled)
						{
							string text3 = query.Trim();
							int num = text3.LastIndexOf(' ') + 1;
							if (num > 0)
							{
								text2 = text3.Substring(0, num - 1);
								query = text3.Substring(num);
							}
						}
						anyOf = this.config.TopNExclusionCharacters;
						enumerable = this.TopNCompletions.GetCompletions(query, 0, dictionary, this.config.MaxCompletionTraversalCount, null, null, null);
					}
				}
				else
				{
					text = "nicknames";
					enumerable = this.Nicknames.GetCompletions(query, 0, dictionary, this.config.MaxCompletionTraversalCount, null, null, null);
				}
				break;
			}
			if (enumerable != null)
			{
				foreach (ICompletionResult completionResult in enumerable)
				{
					string text4 = completionResult.Query;
					if (text4.IndexOfAny(anyOf) == -1)
					{
						if (!string.IsNullOrEmpty(text2))
						{
							text4 = string.Format("{0} {1}", text2, text4);
						}
						if (!suggestions.Keys.Contains(text4))
						{
							QuerySuggestion value = new QuerySuggestion(text4, rank, source);
							suggestions.Add(text4, value);
							rank -= 0.0001;
						}
						foreach (IMatchResult matchResult in completionResult.Matches)
						{
							string text5;
							if (!string.IsNullOrEmpty(text))
							{
								text5 = matchResult.Attributes[text];
							}
							else
							{
								text5 = matchResult.MatchedItem;
							}
							if (!string.IsNullOrEmpty(text2))
							{
								text5 = string.Format("{0} {1}", text2, text5);
							}
							if (!suggestions.Keys.Contains(text5))
							{
								QuerySuggestion value2 = new QuerySuggestion(text5, rank, source);
								suggestions.Add(text5, value2);
								rank -= 0.0001;
								if (suggestions.Count >= this.MaximumSuggestionsCount)
								{
									break;
								}
							}
						}
						if (suggestions.Count >= this.MaximumSuggestionsCount)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002AA0 File Offset: 0x00000CA0
		private QueryCompletion GetAssemblyBasedCompletionDictionary(int maximumEditDistance, double minimalSimilarity, int exactPrefixLength, string assemblyName)
		{
			this.InitializeStaticResourcesIfNeeded();
			AssemblyName assemblyName2 = new AssemblyName(assemblyName);
			ICompletion item = CompletionFactory.Instance.Get(1, assemblyName2, maximumEditDistance, minimalSimilarity, exactPrefixLength, 180);
			return new QueryCompletion(new List<ICompletion>
			{
				item
			});
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002AE4 File Offset: 0x00000CE4
		private void InitializeStaticResourcesIfNeeded()
		{
			if (!Completions.staticResourcesInitialized)
			{
				lock (Completions.StaticConstructionLockObject)
				{
					Thread.MemoryBarrier();
					if (!Completions.staticResourcesInitialized)
					{
						string[] rootPath = new string[]
						{
							Path.Combine(ExchangeSetupContext.BinPath, "Search\\Ceres\\Resources\\Bundles")
						};
						IPackageManager packageManager = new DictionaryPackageManager(rootPath);
						NlpResourceManager.Instance.SetPackageManager(packageManager);
						Completions.staticResourcesInitialized = true;
					}
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002B68 File Offset: 0x00000D68
		private void TopNDictionaryCompilationCallback(IAsyncResult asyncResult)
		{
			try
			{
				Completions.CompilationClient.EndExecuteFlow(asyncResult);
				LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (LazyAsyncResultWithTimeout)asyncResult;
				if (lazyAsyncResultWithTimeout.IsCanceled)
				{
					this.TopNState = Completions.CompletionsDictionaryState.InitializationCancelled;
				}
				else
				{
					this.ReloadTopN = true;
					this.TopNState = Completions.CompletionsDictionaryState.CompilationCompleted;
				}
			}
			catch (Exception)
			{
				this.TopNState = Completions.CompletionsDictionaryState.Failed;
			}
		}

		// Token: 0x04000001 RID: 1
		internal const double StartingSuggestionsRank = 0.8999;

		// Token: 0x04000002 RID: 2
		private const string NicknamesDictionaryAttribute = "nicknames";

		// Token: 0x04000003 RID: 3
		private const string SynonmsDictionaryAttribute = "synonyms";

		// Token: 0x04000004 RID: 4
		private const double SuggestionRankInterval = 0.0001;

		// Token: 0x04000005 RID: 5
		private static readonly char[] DefaultExclusionChars = new char[0];

		// Token: 0x04000006 RID: 6
		private static readonly object StaticConstructionLockObject = new object();

		// Token: 0x04000007 RID: 7
		private static QueryCompletion spellingCompletions;

		// Token: 0x04000008 RID: 8
		private static QueryCompletion synonymsCompletions;

		// Token: 0x04000009 RID: 9
		private static QueryCompletion nicknamesCompletions;

		// Token: 0x0400000A RID: 10
		private static bool staticResourcesInitialized;

		// Token: 0x0400000B RID: 11
		private static volatile TopNManagementClient compilationClient;

		// Token: 0x0400000C RID: 12
		private readonly object topNlockObject = new object();

		// Token: 0x0400000D RID: 13
		private readonly ISearchServiceConfig config;

		// Token: 0x0400000E RID: 14
		private int maximumSuggestionsCount = 100;

		// Token: 0x0400000F RID: 15
		private List<KeyValuePair<string, object>> topNInitializationStatistics;

		// Token: 0x02000003 RID: 3
		internal enum CompletionsDictionaryState
		{
			// Token: 0x04000014 RID: 20
			Unknown,
			// Token: 0x04000015 RID: 21
			Initialized,
			// Token: 0x04000016 RID: 22
			InitializationCancelled,
			// Token: 0x04000017 RID: 23
			CompilationCompleted,
			// Token: 0x04000018 RID: 24
			Failed,
			// Token: 0x04000019 RID: 25
			Stale,
			// Token: 0x0400001A RID: 26
			Disabled
		}
	}
}
