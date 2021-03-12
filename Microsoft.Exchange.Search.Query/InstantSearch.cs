using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstantSearch : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000032B0 File Offset: 0x000014B0
		public InstantSearch(MailboxSession session, IReadOnlyList<StoreId> folderScope, Guid correlationId) : this(session, folderScope, null, correlationId)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000032BC File Offset: 0x000014BC
		public InstantSearch(MailboxSession session, IReadOnlyList<StoreId> folderScope, ICollection<PropertyDefinition> requestedProperties, Guid correlationId)
		{
			InstantSearch.ThrowOnNullArgument(session, "session");
			this.Session = session;
			this.RequestedProperties = requestedProperties;
			this.CorrelationId = correlationId;
			lock (this.Session)
			{
				this.PreferredCulture = this.Session.PreferedCulture;
				this.MdbGuid = this.Session.MdbGuid;
				this.MailboxGuid = this.Session.MailboxGuid;
				this.RootFolderId = this.Session.GetDefaultFolderId(DefaultFolderType.Root);
			}
			if (folderScope == null || folderScope.Count == 0)
			{
				this.FolderScope = new StoreId[]
				{
					this.RootFolderId
				};
			}
			else
			{
				this.FolderScope = folderScope;
			}
			this.Config = new FlightingSearchConfig(this.MdbGuid);
			this.Completions = new Completions(this.Config);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000033E0 File Offset: 0x000015E0
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000033E8 File Offset: 0x000015E8
		public MailboxSession Session { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000033F1 File Offset: 0x000015F1
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000033F9 File Offset: 0x000015F9
		public Action<IReadOnlyCollection<string>, ICancelableAsyncResult> HitHighlightingDataAvailableCallback { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003402 File Offset: 0x00001602
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000340A File Offset: 0x0000160A
		public Action<IReadOnlyCollection<QuerySuggestion>, ICancelableAsyncResult> SuggestionsAvailableCallback { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003413 File Offset: 0x00001613
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000341B File Offset: 0x0000161B
		public Action<IReadOnlyCollection<IReadOnlyPropertyBag>, ICancelableAsyncResult> ResultsUpdatedCallback { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003424 File Offset: 0x00001624
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000342C File Offset: 0x0000162C
		public Action<IReadOnlyCollection<RefinerData>, ICancelableAsyncResult> RefinerDataAvailableCallback { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003435 File Offset: 0x00001635
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000343D File Offset: 0x0000163D
		public ICollection<PropertyDefinition> RequestedProperties { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003446 File Offset: 0x00001646
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000344E File Offset: 0x0000164E
		public IReadOnlyList<StoreId> FolderScope { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003457 File Offset: 0x00001657
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000345F File Offset: 0x0000165F
		public bool SearchForConversations { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003468 File Offset: 0x00001668
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003470 File Offset: 0x00001670
		public bool FastQueryPath { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003479 File Offset: 0x00001679
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003486 File Offset: 0x00001686
		public int MaximumSuggestionsCount
		{
			[DebuggerStepThrough]
			get
			{
				return this.Completions.MaximumSuggestionsCount;
			}
			[DebuggerStepThrough]
			set
			{
				this.Completions.MaximumSuggestionsCount = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003494 File Offset: 0x00001694
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000349C File Offset: 0x0000169C
		public Guid CorrelationId { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000034A5 File Offset: 0x000016A5
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000034AD File Offset: 0x000016AD
		public IRecipientResolver RecipientResolver { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000034B6 File Offset: 0x000016B6
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000034BE File Offset: 0x000016BE
		public IPolicyTagProvider PolicyTagProvider { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000034C7 File Offset: 0x000016C7
		internal QueryHistoryInputDictionary QueryHistory
		{
			[DebuggerStepThrough]
			get
			{
				return this.queryHistory;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000034CF File Offset: 0x000016CF
		internal PagingImsFlowExecutor FlowExecutor
		{
			[DebuggerStepThrough]
			get
			{
				return InstantSearch.ServiceProxyWrapper.Value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000034DB File Offset: 0x000016DB
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000034E3 File Offset: 0x000016E3
		internal Completions Completions { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000034EC File Offset: 0x000016EC
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000034F4 File Offset: 0x000016F4
		internal InstantSearchSchema Schema { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000034FD File Offset: 0x000016FD
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003505 File Offset: 0x00001705
		internal CultureInfo PreferredCulture { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000350E File Offset: 0x0000170E
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003516 File Offset: 0x00001716
		internal Guid MdbGuid { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000351F File Offset: 0x0000171F
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003527 File Offset: 0x00001727
		internal Guid MailboxGuid { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003530 File Offset: 0x00001730
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003538 File Offset: 0x00001738
		internal StoreId RootFolderId { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003541 File Offset: 0x00001741
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003549 File Offset: 0x00001749
		internal SearchConfig Config { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003554 File Offset: 0x00001754
		private static Hookable<PagingImsFlowExecutor> ServiceProxyWrapper
		{
			get
			{
				if (InstantSearch.serviceProxyWrapperInstance == null)
				{
					string hostName = SearchConfig.Instance.HostName;
					int queryServicePort = SearchConfig.Instance.QueryServicePort;
					int fastImsChannelPoolSize = SearchConfig.Instance.FastImsChannelPoolSize;
					TimeSpan fastImsOpenTimeout = SearchConfig.Instance.FastImsOpenTimeout;
					TimeSpan fastImsSendTimeout = SearchConfig.Instance.FastImsSendTimeout;
					TimeSpan fastImsReceiveTimeout = SearchConfig.Instance.FastImsReceiveTimeout;
					int fastSearchRetryCount = SearchConfig.Instance.FastSearchRetryCount;
					long num = (long)SearchConfig.Instance.FastIMSMaxReceivedMessageSize;
					int fastIMSMaxStringContentLength = SearchConfig.Instance.FastIMSMaxStringContentLength;
					TimeSpan fastProxyCacheTimeout = SearchConfig.Instance.FastProxyCacheTimeout;
					Hookable<PagingImsFlowExecutor> value = Hookable<PagingImsFlowExecutor>.Create(true, new PagingImsFlowExecutor(hostName, queryServicePort, fastImsChannelPoolSize, fastImsOpenTimeout, fastImsSendTimeout, fastImsReceiveTimeout, fastProxyCacheTimeout, num, fastIMSMaxStringContentLength, fastSearchRetryCount));
					Interlocked.CompareExchange<Hookable<PagingImsFlowExecutor>>(ref InstantSearch.serviceProxyWrapperInstance, value, null);
				}
				return InstantSearch.serviceProxyWrapperInstance;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003610 File Offset: 0x00001810
		public static IDisposable SetPagingImsFlowExecutorTestHook(PagingImsFlowExecutor testHook)
		{
			return InstantSearch.ServiceProxyWrapper.SetTestHook(testHook);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000361D File Offset: 0x0000181D
		public static ICollection<PropertyDefinition> GetDefaultRequestedProperties(MailboxSession session)
		{
			InstantSearch.ThrowOnNullArgument(session, "session");
			return InstantSearchSchema.DefaultRequestedProperties;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000362F File Offset: 0x0000182F
		public static ICollection<PropertyDefinition> GetDefaultRequestedPropertiesConversations(MailboxSession session)
		{
			InstantSearch.ThrowOnNullArgument(session, "session");
			return InstantSearchSchema.DefaultRequestedPropertiesConversations;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003641 File Offset: 0x00001841
		public static IReadOnlyCollection<PropertyDefinition> GetDefaultRefinersFAST(MailboxSession session)
		{
			InstantSearch.ThrowOnNullArgument(session, "session");
			return InstantSearchSchema.DefaultRefinersFAST;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003653 File Offset: 0x00001853
		public static IReadOnlyCollection<PropertyDefinition> GetDefaultRefiners(MailboxSession session)
		{
			InstantSearch.ThrowOnNullArgument(session, "session");
			return InstantSearchSchema.DefaultRefiners;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003665 File Offset: 0x00001865
		public static IReadOnlyCollection<PropertyDefinition> GetDefaultRefinersConversations(MailboxSession session)
		{
			InstantSearch.ThrowOnNullArgument(session, "session");
			return InstantSearchSchema.DefaultRefinersConversations;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003677 File Offset: 0x00001877
		public static IReadOnlyCollection<SortBy> GetDefaultSortBySpec(MailboxSession session)
		{
			InstantSearch.ThrowOnNullArgument(session, "session");
			return InstantSearchSchema.DefaultSortBySpec;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003689 File Offset: 0x00001889
		public static IReadOnlyCollection<SortBy> GetDefaultSortBySpecConversations(MailboxSession session)
		{
			InstantSearch.ThrowOnNullArgument(session, "session");
			return InstantSearchSchema.DefaultSortBySpecConversations;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000369B File Offset: 0x0000189B
		public bool RemoveQueryHistoryItem(string query)
		{
			return this.QueryHistory.Remove(query);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000036AC File Offset: 0x000018AC
		public void ClearQueryHistory()
		{
			lock (this.Session)
			{
				SearchDictionary.ResetDictionary(this.Session, "Search.QueryHistoryInput", UserConfigurationTypes.Stream, true, false);
			}
			lock (this.QueryHistory)
			{
				this.QueryHistory.Clear();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003730 File Offset: 0x00001930
		public IAsyncResult BeginStartSession(AsyncCallback completionCallback, object state)
		{
			this.InitializeSchema();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, completionCallback);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartSessionWorker), lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003760 File Offset: 0x00001960
		public void EndStartSession(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = LazyAsyncResult.EndAsyncOperation<LazyAsyncResult>(asyncResult);
			Exception ex = lazyAsyncResult.Result as Exception;
			if (ex != null)
			{
				throw new InstantSearchPermanentException(new LocalizedString(ex.Message), ex, null);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003798 File Offset: 0x00001998
		public IAsyncResult BeginStopSession(AsyncCallback completionCallback, object state)
		{
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, completionCallback);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.StopSessionWorker), lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000037C4 File Offset: 0x000019C4
		public void EndStopSession(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = LazyAsyncResult.EndAsyncOperation<LazyAsyncResult>(asyncResult);
			Exception ex = lazyAsyncResult.Result as Exception;
			if (ex != null)
			{
				throw new InstantSearchPermanentException(new LocalizedString(ex.Message), ex, null);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000037FC File Offset: 0x000019FC
		public ICancelableAsyncResult BeginInstantSearchRequest(InstantSearchQueryParameters query, AsyncCallback completionCallback, object state)
		{
			ICancelableAsyncResult result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				InstantSearchRequest instantSearchRequest = disposeGuard.Add<InstantSearchRequest>(new InstantSearchRequest(this, query, state, completionCallback));
				bool flag = false;
				lock (this.lockObject)
				{
					try
					{
						if (this.canceled)
						{
							throw new InvalidOperationException("canceled");
						}
						if (query.HasOption(QueryOptions.ExplicitSearch))
						{
							lock (this.QueryHistory)
							{
								this.QueryHistory.Merge(query.KqlQuery);
							}
							this.accumulatedQueries = true;
						}
						this.activeRequests.Add(instantSearchRequest);
						flag = true;
						instantSearchRequest.StartSearch();
						disposeGuard.Success();
						result = instantSearchRequest;
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
					catch (Exception ex)
					{
						if (flag)
						{
							this.activeRequests.Remove(instantSearchRequest);
						}
						throw new InstantSearchPermanentException(new LocalizedString(ex.Message), ex, null);
					}
				}
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003954 File Offset: 0x00001B54
		public QueryStatistics EndInstantSearchRequest(ICancelableAsyncResult asyncResult)
		{
			InstantSearchRequest instantSearchRequest = LazyAsyncResult.EndAsyncOperation<InstantSearchRequest>(asyncResult);
			lock (this.lockObject)
			{
				this.activeRequests.Remove(instantSearchRequest);
				if (this.canceled && this.activeRequests.Count == 0 && this.cancelCompleteEvent != null)
				{
					this.cancelCompleteEvent.Set();
				}
			}
			object result = instantSearchRequest.Result;
			instantSearchRequest.Dispose();
			QueryStatistics queryStatistics = result as QueryStatistics;
			if (queryStatistics != null)
			{
				return queryStatistics;
			}
			throw (Exception)result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000039F0 File Offset: 0x00001BF0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000039FF File Offset: 0x00001BFF
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<InstantSearch>(this);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003A07 File Offset: 0x00001C07
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003A1C File Offset: 0x00001C1C
		internal static void ThrowOnNullArgument(object argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A28 File Offset: 0x00001C28
		internal static void ThrowOnNullOrEmptyArgument(IEnumerable argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			if (!argument.GetEnumerator().MoveNext())
			{
				throw new ArgumentException(argumentName);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003A48 File Offset: 0x00001C48
		private void StartSessionWorker(object state)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)state;
			Exception value = null;
			try
			{
				this.InitializeQueryHistory();
				this.InitializeTopN();
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
			catch (Exception ex)
			{
				value = ex;
			}
			lazyAsyncResult.InvokeCallback(value);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003AB8 File Offset: 0x00001CB8
		private void StopSessionWorker(object state)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)state;
			Exception value = null;
			try
			{
				this.Cancel(false);
				this.WaitForCancelToComplete();
				this.WriteQueryHistoryToStore();
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
			catch (Exception ex)
			{
				value = ex;
			}
			lazyAsyncResult.InvokeCallback(value);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003B30 File Offset: 0x00001D30
		private void Cancel(bool cancelOutstandingRequests)
		{
			lock (this.lockObject)
			{
				if (this.activeRequests.Count != 0)
				{
					if (cancelOutstandingRequests)
					{
						foreach (InstantSearchRequest instantSearchRequest in this.activeRequests)
						{
							instantSearchRequest.Cancel();
						}
					}
					this.cancelCompleteEvent = new ManualResetEvent(false);
				}
				this.canceled = true;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003BD0 File Offset: 0x00001DD0
		private void WaitForCancelToComplete()
		{
			lock (this.lockObject)
			{
				if (this.activeRequests.Count == 0 || this.cancelCompleteEvent == null)
				{
					return;
				}
			}
			this.cancelCompleteEvent.WaitOne();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003C30 File Offset: 0x00001E30
		private void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.Cancel(true);
				this.WaitForCancelToComplete();
				if (this.cancelCompleteEvent != null)
				{
					this.cancelCompleteEvent.Dispose();
					this.cancelCompleteEvent = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003C84 File Offset: 0x00001E84
		private void InitializeTopN()
		{
			lock (this.Session)
			{
				this.Completions.InitializeTopN(this.Session);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003CD0 File Offset: 0x00001ED0
		private void InitializeQueryHistory()
		{
			lock (this.Session)
			{
				using (UserConfiguration searchDictionaryItem = SearchDictionary.GetSearchDictionaryItem(this.Session, "Search.QueryHistoryInput"))
				{
					using (Stream stream = searchDictionaryItem.GetStream())
					{
						lock (this.QueryHistory)
						{
							this.QueryHistory.InitializeFrom(stream);
						}
					}
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003D8C File Offset: 0x00001F8C
		private void WriteQueryHistoryToStore()
		{
			if (this.accumulatedQueries)
			{
				lock (this.Session)
				{
					using (UserConfiguration searchDictionaryItem = SearchDictionary.GetSearchDictionaryItem(this.Session, "Search.QueryHistoryInput"))
					{
						using (Stream stream = searchDictionaryItem.GetStream())
						{
							lock (this.QueryHistory)
							{
								this.QueryHistory.SerializeTo(stream);
							}
							searchDictionaryItem.Save(SaveMode.NoConflictResolutionForceSave);
						}
					}
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003E5C File Offset: 0x0000205C
		private void InitializeSchema()
		{
			if (this.Schema != null)
			{
				return;
			}
			bool value;
			if (this.RequestedProperties != null)
			{
				this.Schema = new InstantSearchSchema(this.RequestedProperties);
				value = !this.Schema.HasUnsupportedXsoProperties;
			}
			else
			{
				value = true;
				if (this.SearchForConversations)
				{
					this.Schema = InstantSearchSchema.DefaultConversationsSchema;
				}
				else
				{
					this.Schema = InstantSearchSchema.DefaultSchema;
				}
			}
			this.FastQueryPath = this.DetermineFastQueryPath(value);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003ECC File Offset: 0x000020CC
		private bool DetermineFastQueryPath(bool value)
		{
			switch (this.Config.FastQueryPath)
			{
			case 0:
				return false;
			case 1:
				return true;
			default:
				return value;
			}
		}

		// Token: 0x0400001F RID: 31
		private static Hookable<PagingImsFlowExecutor> serviceProxyWrapperInstance;

		// Token: 0x04000020 RID: 32
		private readonly object lockObject = new object();

		// Token: 0x04000021 RID: 33
		private List<InstantSearchRequest> activeRequests = new List<InstantSearchRequest>();

		// Token: 0x04000022 RID: 34
		private QueryHistoryInputDictionary queryHistory = new QueryHistoryInputDictionary();

		// Token: 0x04000023 RID: 35
		private bool accumulatedQueries;

		// Token: 0x04000024 RID: 36
		private volatile bool canceled;

		// Token: 0x04000025 RID: 37
		private ManualResetEvent cancelCompleteEvent;

		// Token: 0x04000026 RID: 38
		private DisposeTracker disposeTracker;
	}
}
