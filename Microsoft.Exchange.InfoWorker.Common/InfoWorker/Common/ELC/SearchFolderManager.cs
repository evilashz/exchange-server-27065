using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x020001B3 RID: 435
	internal class SearchFolderManager
	{
		// Token: 0x06000B8F RID: 2959 RVA: 0x0003264C File Offset: 0x0003084C
		internal SearchFolderManager(MailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003265B File Offset: 0x0003085B
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Mailbox:" + this.mailboxSession.MailboxOwner.ToString() + " in SearchFolderManager.";
			}
			return this.toString;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00032690 File Offset: 0x00030890
		internal Folder GetAutoTaggedMailSearchFolder()
		{
			SearchFolder searchFolder = this.CreateSearchFolder("AutoTaggedMailSearchFolder" + Guid.NewGuid().ToString());
			QueryFilter queryFilter = this.CreateAutoTaggedSearchFolderQuery();
			this.PopulateSearchFolder(searchFolder, queryFilter);
			return searchFolder;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000326D4 File Offset: 0x000308D4
		internal Folder GetNoArchiveTagSearchFolder()
		{
			bool flag = false;
			SearchFolder searchFolder = null;
			Folder result;
			try
			{
				searchFolder = this.CreateSearchFolder("NoArchiveTagSearchFolder8534F96D-4183-41fb-8A05-9B7112AE2100");
				SearchFolderCriteria searchFolderCriteria = new SearchFolderCriteria(SearchFolderManager.NoArchiveTagFolderQuery, new StoreId[]
				{
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Root)
				});
				searchFolderCriteria.DeepTraversal = true;
				SearchFolderCriteria searchFolderCriteria2 = null;
				try
				{
					searchFolderCriteria2 = searchFolder.GetSearchCriteria();
				}
				catch (ObjectNotInitializedException)
				{
					SearchFolderManager.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: SearchFolderCriteria could not be retrieved from search folder. Need to set it.");
				}
				if (searchFolderCriteria2 == null || !this.SearchCriterionMatches(searchFolderCriteria2, searchFolderCriteria))
				{
					this.PopulateDynamicSearchFolder(searchFolder, searchFolderCriteria);
				}
				flag = true;
				result = searchFolder;
			}
			finally
			{
				if (!flag && searchFolder != null)
				{
					searchFolder.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0003278C File Offset: 0x0003098C
		private QueryFilter CreateAutoTaggedSearchFolderQuery()
		{
			QueryFilter queryFilter = new ExistsFilter(StoreObjectSchema.PolicyTag);
			QueryFilter queryFilter2 = new AndFilter(new QueryFilter[]
			{
				new ExistsFilter(StoreObjectSchema.RetentionFlags),
				new BitMaskFilter(StoreObjectSchema.RetentionFlags, 4UL, true)
			});
			return new AndFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter2
			});
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000327EC File Offset: 0x000309EC
		private SearchFolder CreateSearchFolder(string searchFolderName)
		{
			bool flag = false;
			SearchFolder searchFolder = null;
			SearchFolder result;
			try
			{
				searchFolder = SearchFolder.Create(this.mailboxSession, this.mailboxSession.GetDefaultFolderId(DefaultFolderType.SearchFolders), searchFolderName, CreateMode.OpenIfExists);
				searchFolder.Save();
				searchFolder.Load();
				SearchFolderManager.Tracer.TraceDebug<SearchFolderManager>((long)this.GetHashCode(), "{0}: Created or opened archive search folder.", this);
				flag = true;
				result = searchFolder;
			}
			finally
			{
				if (!flag && searchFolder != null)
				{
					searchFolder.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00032860 File Offset: 0x00030A60
		private void PopulateSearchFolder(SearchFolder searchFolder, QueryFilter queryFilter)
		{
			IAsyncResult asyncResult = searchFolder.BeginApplyOneTimeSearch(new SearchFolderCriteria(queryFilter, new StoreId[]
			{
				this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Root)
			})
			{
				DeepTraversal = true
			}, null, null);
			bool flag = asyncResult.AsyncWaitHandle.WaitOne(SearchFolderManager.MaximumSearchTime, false);
			if (flag)
			{
				searchFolder.EndApplyOneTimeSearch(asyncResult);
			}
			else
			{
				SearchFolderManager.Tracer.TraceDebug<SearchFolderManager>((long)this.GetHashCode(), "{0}: ELC Auto Tag search timed out.", this);
			}
			searchFolder.Save();
			searchFolder.Load();
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000328E0 File Offset: 0x00030AE0
		private void PopulateDynamicSearchFolder(SearchFolder searchFolder, SearchFolderCriteria searchCriteria)
		{
			IAsyncResult asyncResult = searchFolder.BeginApplyContinuousSearch(searchCriteria, null, null);
			bool flag = asyncResult.AsyncWaitHandle.WaitOne(SearchFolderManager.MaximumSearchTime, false);
			if (flag)
			{
				searchFolder.EndApplyContinuousSearch(asyncResult);
			}
			else
			{
				SearchFolderManager.Tracer.TraceDebug<SearchFolderManager>((long)this.GetHashCode(), "{0}: ELC Non Archive Tag search timed out.", this);
			}
			searchFolder.Save();
			searchFolder.Load();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0003293C File Offset: 0x00030B3C
		private bool SearchCriterionMatches(SearchFolderCriteria existingCriteria, SearchFolderCriteria desiredCriteria)
		{
			if (existingCriteria.SearchQuery == null || !existingCriteria.SearchQuery.Equals(desiredCriteria.SearchQuery))
			{
				SearchFolderManager.Tracer.TraceDebug<SearchFolderManager>((long)this.GetHashCode(), "{0}: The existingCriteria.SearchQuery does not match the desiredCriteria.SearchQuery", this);
				return false;
			}
			if (existingCriteria.FolderScope == null || existingCriteria.FolderScope.Length != desiredCriteria.FolderScope.Length || !existingCriteria.FolderScope[0].Equals(this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Root)))
			{
				SearchFolderManager.Tracer.TraceDebug<SearchFolderManager>((long)this.GetHashCode(), "{0}: The existingCriteria.FolderScope does not match the desiredCriteria.FolderScope", this);
				return false;
			}
			if (existingCriteria.DeepTraversal != desiredCriteria.DeepTraversal)
			{
				SearchFolderManager.Tracer.TraceDebug<SearchFolderManager>((long)this.GetHashCode(), "{0}: The existingCriteria.DeepTraversal does not match the desiredCriteria.DeepTraversal", this);
				return false;
			}
			SearchFolderManager.Tracer.TraceDebug<SearchFolderManager>((long)this.GetHashCode(), "{0}: The criteria match.", this);
			return true;
		}

		// Token: 0x040008AB RID: 2219
		private const int AutoTagBit = 4;

		// Token: 0x040008AC RID: 2220
		private const string ArchiveSearchFolderGuid = "8534F96D-4183-41fb-8A05-9B7112AE2100";

		// Token: 0x040008AD RID: 2221
		private static readonly TimeSpan MaximumSearchTime = TimeSpan.FromSeconds(300.0);

		// Token: 0x040008AE RID: 2222
		protected static readonly Trace Tracer = ExTraceGlobals.AutoTaggingTracer;

		// Token: 0x040008AF RID: 2223
		private static readonly QueryFilter NoArchiveTagFolderQuery = new NotFilter(new ExistsFilter(StoreObjectSchema.ArchiveTag));

		// Token: 0x040008B0 RID: 2224
		private string toString;

		// Token: 0x040008B1 RID: 2225
		private MailboxSession mailboxSession;
	}
}
