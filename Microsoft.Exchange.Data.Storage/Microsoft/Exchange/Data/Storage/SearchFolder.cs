using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007B3 RID: 1971
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SearchFolder : Folder, ISearchFolder, IFolder, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06004A13 RID: 18963 RVA: 0x00135D42 File Offset: 0x00133F42
		internal SearchFolder(CoreFolder coreFolder) : base(coreFolder)
		{
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x00135D4B File Offset: 0x00133F4B
		public static SearchFolder Create(StoreSession session, StoreId parentFolderId)
		{
			return (SearchFolder)Folder.Create(session, parentFolderId, StoreObjectType.SearchFolder);
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x00135D5A File Offset: 0x00133F5A
		public static SearchFolder Create(StoreSession session, StoreId parentFolderId, string displayName, CreateMode createMode)
		{
			return (SearchFolder)Folder.Create(session, parentFolderId, StoreObjectType.SearchFolder, displayName, createMode);
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x00135D6B File Offset: 0x00133F6B
		public new static SearchFolder Create(StoreSession session, StoreId parentFolderId, StoreObjectType folderType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x00135D72 File Offset: 0x00133F72
		public new static SearchFolder Create(StoreSession session, StoreId parentFolderId, StoreObjectType folderType, string displayName, CreateMode createMode)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x00135D79 File Offset: 0x00133F79
		public new static SearchFolder Bind(StoreSession session, StoreId folderId)
		{
			return SearchFolder.Bind(session, folderId, null);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x00135D83 File Offset: 0x00133F83
		public new static SearchFolder Bind(StoreSession session, StoreId folderId, params PropertyDefinition[] propsToReturn)
		{
			return SearchFolder.Bind(session, folderId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x00135D92 File Offset: 0x00133F92
		public new static SearchFolder Bind(StoreSession session, StoreId folderId, ICollection<PropertyDefinition> propsToReturn)
		{
			propsToReturn = InternalSchema.Combine<PropertyDefinition>(FolderSchema.Instance.AutoloadProperties, propsToReturn);
			return Folder.InternalBind<SearchFolder>(session, folderId, propsToReturn);
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x00135DAE File Offset: 0x00133FAE
		public static SearchFolder Bind(MailboxSession session, DefaultFolderType defaultFolderType)
		{
			return SearchFolder.Bind(session, defaultFolderType, null);
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x00135DB8 File Offset: 0x00133FB8
		public static SearchFolder Bind(MailboxSession session, DefaultFolderType defaultFolderType, ICollection<PropertyDefinition> propsToReturn)
		{
			EnumValidator.ThrowIfInvalid<DefaultFolderType>(defaultFolderType, "defaultFolderType");
			DefaultFolder defaultFolder = session.InternalGetDefaultFolder(defaultFolderType);
			if (defaultFolder.StoreObjectType != StoreObjectType.OutlookSearchFolder && defaultFolder.StoreObjectType != StoreObjectType.SearchFolder)
			{
				throw new ArgumentOutOfRangeException("defaultFolderType");
			}
			StoreObjectId folderId = session.SafeGetDefaultFolderId(defaultFolderType);
			ObjectNotFoundException ex = null;
			for (int i = 0; i < 2; i++)
			{
				try
				{
					return SearchFolder.Bind(session, folderId, propsToReturn);
				}
				catch (ObjectNotFoundException ex2)
				{
					ex = ex2;
					ExTraceGlobals.StorageTracer.Information<DefaultFolderType>(0L, "SearchFolder::Bind(defaultFolderType): attempting to recreate {0}.", defaultFolderType);
					if (!session.TryFixDefaultFolderId(defaultFolderType, out folderId))
					{
						throw;
					}
				}
			}
			throw ex;
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x00135E54 File Offset: 0x00134054
		private static SetSearchCriteriaFlags CalculateSearchCriteriaFlags(bool deepTraversal, bool useCiForComplexQueries, SetSearchCriteriaFlags statisticsOnly, bool failNonContentIndexedSearch, SearchFolder.SearchType searchType)
		{
			EnumValidator.ThrowIfInvalid<SearchFolder.SearchType>(searchType, "searchType");
			SetSearchCriteriaFlags setSearchCriteriaFlags = SetSearchCriteriaFlags.None;
			if (deepTraversal)
			{
				setSearchCriteriaFlags |= SetSearchCriteriaFlags.Recursive;
			}
			else
			{
				setSearchCriteriaFlags |= SetSearchCriteriaFlags.Shallow;
			}
			if (useCiForComplexQueries)
			{
				setSearchCriteriaFlags |= SetSearchCriteriaFlags.UseCiForComplexQueries;
			}
			setSearchCriteriaFlags |= statisticsOnly;
			if (failNonContentIndexedSearch)
			{
				setSearchCriteriaFlags |= SetSearchCriteriaFlags.FailNonContentIndexedSearch;
			}
			switch (searchType)
			{
			case SearchFolder.SearchType.RunOnce:
				setSearchCriteriaFlags |= (SetSearchCriteriaFlags.ContentIndexed | SetSearchCriteriaFlags.Static);
				break;
			case SearchFolder.SearchType.ContinousUpdate:
				setSearchCriteriaFlags |= SetSearchCriteriaFlags.NonContentIndexed;
				break;
			}
			return setSearchCriteriaFlags | SetSearchCriteriaFlags.Restart;
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x00135EC0 File Offset: 0x001340C0
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SearchFolder>(this);
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x00135EC8 File Offset: 0x001340C8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeCurrentSearch();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x00135EDA File Offset: 0x001340DA
		public SearchFolderCriteria GetSearchCriteria()
		{
			this.CheckDisposed("GetSearchCriteria");
			if (SearchFolder.TestInjectCriteriaFailure != null)
			{
				SearchFolder.TestInjectCriteriaFailure();
			}
			return base.CoreFolder.GetSearchCriteria(true);
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x00135F04 File Offset: 0x00134104
		public void ApplyContinuousSearch(SearchFolderCriteria searchFolderCriteria)
		{
			this.CheckDisposed("ApplyContinuousSearch");
			ExTraceGlobals.StorageTracer.Information<SearchFolderCriteria>((long)this.GetHashCode(), "SearchFolder::ApplyContinuousSearch. SearchCriteria = {0}", searchFolderCriteria);
			SetSearchCriteriaFlags setSearchCriteriaFlags;
			this.PreprocessSearch(searchFolderCriteria, SearchFolder.SearchType.ContinousUpdate, out setSearchCriteriaFlags);
			base.CoreFolder.SetSearchCriteria(searchFolderCriteria, setSearchCriteriaFlags);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x00135F4C File Offset: 0x0013414C
		public void ApplyOneTimeSearch(SearchFolderCriteria searchFolderCriteria)
		{
			this.CheckDisposed("ApplyOneTimeSearch");
			ExTraceGlobals.StorageTracer.Information<SearchFolderCriteria>((long)this.GetHashCode(), "SearchFolder::ApplyOneTimeSearch. SearchCriteria = {0}", searchFolderCriteria);
			SetSearchCriteriaFlags setSearchCriteriaFlags;
			this.PreprocessSearch(searchFolderCriteria, SearchFolder.SearchType.RunOnce, out setSearchCriteriaFlags);
			base.CoreFolder.SetSearchCriteria(searchFolderCriteria, setSearchCriteriaFlags);
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x00135F94 File Offset: 0x00134194
		public IAsyncResult BeginApplyContinuousSearch(SearchFolderCriteria searchFolderCriteria, AsyncCallback asyncCallback, object state)
		{
			this.CheckDisposed("BeginApplyContinuousSearch");
			ExTraceGlobals.StorageTracer.Information<SearchFolderCriteria>((long)this.GetHashCode(), "SearchFolder::BeginApplyContinuousSearch. SearchCriteria = {0}", searchFolderCriteria);
			SetSearchCriteriaFlags setSearchCriteriaFlags;
			this.PreprocessSearch(searchFolderCriteria, SearchFolder.SearchType.ContinousUpdate, out setSearchCriteriaFlags);
			this.asyncSearch = new SearchFolderAsyncSearch(base.Session, base.Id.ObjectId, asyncCallback, state);
			base.CoreFolder.SetSearchCriteria(searchFolderCriteria, setSearchCriteriaFlags);
			return this.asyncSearch.AsyncResult;
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x00136003 File Offset: 0x00134203
		public void EndApplyContinuousSearch(IAsyncResult asyncResult)
		{
			this.CheckDisposed("EndApplyContinuousSearch");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "SearchFolder::EndApplyContinuousSearch.");
			this.EndApplySearch(asyncResult);
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x00136030 File Offset: 0x00134230
		public IAsyncResult BeginApplyOneTimeSearch(SearchFolderCriteria searchFolderCriteria, AsyncCallback asyncCallback, object state)
		{
			this.CheckDisposed("BeginApplyOneTimeSearch");
			ExTraceGlobals.StorageTracer.Information<SearchFolderCriteria>((long)this.GetHashCode(), "SearchFolder::BeginApplyOneTimeSearch. SearchCriteria = {0}", searchFolderCriteria);
			SetSearchCriteriaFlags setSearchCriteriaFlags;
			this.PreprocessSearch(searchFolderCriteria, SearchFolder.SearchType.RunOnce, out setSearchCriteriaFlags);
			this.asyncSearch = new SearchFolderAsyncSearch(base.Session, base.Id.ObjectId, asyncCallback, state);
			base.CoreFolder.SetSearchCriteria(searchFolderCriteria, setSearchCriteriaFlags);
			return this.asyncSearch.AsyncResult;
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x0013609F File Offset: 0x0013429F
		public void EndApplyOneTimeSearch(IAsyncResult asyncResult)
		{
			this.CheckDisposed("EndApplyOneTimeSearch");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "SearchFolder::EndApplyOneTimeSearch.");
			this.EndApplySearch(asyncResult);
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x001360CC File Offset: 0x001342CC
		public bool IsPopulated()
		{
			this.CheckDisposed("IsPopulated");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "SearchFolder::IsPopulated.");
			SearchFolderCriteria searchCriteria = this.GetSearchCriteria();
			return (searchCriteria.SearchState & SearchState.Rebuild) != SearchState.Rebuild;
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x0013610F File Offset: 0x0013430F
		protected override void CheckItemBelongsToThisFolder(StoreObjectId storeObjectIds)
		{
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x00136114 File Offset: 0x00134314
		private void EndApplySearch(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (this.asyncSearch == null)
			{
				throw new InvalidOperationException(ServerStrings.ExNoSearchHasBeenInitiated);
			}
			if (this.asyncSearch.AsyncResult != asyncResult)
			{
				throw new ArgumentException(ServerStrings.ExInvalidAsyncResult, "asyncResult");
			}
			this.asyncSearch.AsyncResult.AsyncWaitHandle.WaitOne();
			this.DisposeCurrentSearch();
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x00136186 File Offset: 0x00134386
		private void DisposeCurrentSearch()
		{
			if (this.asyncSearch != null)
			{
				this.asyncSearch.Dispose();
				this.asyncSearch = null;
			}
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x001361A4 File Offset: 0x001343A4
		private void PreprocessSearch(SearchFolderCriteria searchFolderCriteria, SearchFolder.SearchType searchType, out SetSearchCriteriaFlags searchCriteriaFlags)
		{
			if (searchFolderCriteria == null)
			{
				throw new ArgumentNullException("searchFolderCriteria");
			}
			if (this.IsDirty)
			{
				throw new InvalidOperationException(ServerStrings.ExMustSaveFolderToApplySearch);
			}
			this.DisposeCurrentSearch();
			SetSearchCriteriaFlags setSearchCriteriaFlags = SetSearchCriteriaFlags.None;
			if (searchFolderCriteria.StatisticsOnly)
			{
				setSearchCriteriaFlags |= SetSearchCriteriaFlags.StatisticsOnly;
			}
			if (searchFolderCriteria.EstimateCountOnly)
			{
				setSearchCriteriaFlags |= SetSearchCriteriaFlags.EstimateCountOnly;
			}
			searchCriteriaFlags = SearchFolder.CalculateSearchCriteriaFlags(searchFolderCriteria.DeepTraversal, searchFolderCriteria.UseCiForComplexQueries, setSearchCriteriaFlags, searchFolderCriteria.FailNonContentIndexedSearch, searchType);
		}

		// Token: 0x04002800 RID: 10240
		internal static SearchFolder.CriteriaFailureDelegate TestInjectCriteriaFailure;

		// Token: 0x04002801 RID: 10241
		private SearchFolderAsyncSearch asyncSearch;

		// Token: 0x020007B4 RID: 1972
		// (Invoke) Token: 0x06004A2E RID: 18990
		internal delegate void CriteriaFailureDelegate();

		// Token: 0x020007B5 RID: 1973
		private enum SearchType
		{
			// Token: 0x04002803 RID: 10243
			RunOnce,
			// Token: 0x04002804 RID: 10244
			ContinousUpdate
		}
	}
}
