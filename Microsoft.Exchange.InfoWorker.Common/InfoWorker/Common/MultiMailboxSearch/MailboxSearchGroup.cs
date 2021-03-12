using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001B4 RID: 436
	internal abstract class MailboxSearchGroup : DisposeTrackableBase
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x00032A3E File Offset: 0x00030C3E
		protected MailboxSearchGroup(MailboxInfo[] mailboxes, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo executingUser)
		{
			this.mailboxes = mailboxes;
			this.searchCriteria = searchCriteria;
			this.pagingInfo = pagingInfo;
			this.executingUser = executingUser;
			this.resultAggregator = new ResultAggregator();
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00032A6E File Offset: 0x00030C6E
		protected ISearchResult ResultAggregator
		{
			get
			{
				return this.resultAggregator;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x00032A76 File Offset: 0x00030C76
		protected SearchCriteria SearchCriteria
		{
			get
			{
				return this.searchCriteria;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00032A7E File Offset: 0x00030C7E
		protected PagingInfo PagingInfo
		{
			get
			{
				return this.pagingInfo;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00032A86 File Offset: 0x00030C86
		protected CallerInfo ExecutingUser
		{
			get
			{
				return this.executingUser;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00032A8E File Offset: 0x00030C8E
		protected MailboxInfo[] Mailboxes
		{
			get
			{
				return this.mailboxes;
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00032A96 File Offset: 0x00030C96
		public static IDisposable SetAbortSearchTestHook(Action action)
		{
			return MailboxSearchGroup.abortSearchTestHook.SetTestHook(action);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00032AA3 File Offset: 0x00030CA3
		public IAsyncResult BeginExecuteSearch(AsyncCallback callback, object state)
		{
			if (this.searchState != MailboxSearchGroup.SearchState.NotStarted)
			{
				throw new InvalidOperationException();
			}
			this.ExecuteSearch();
			this.asyncResult = new AsyncResult(callback, state);
			this.searchState = MailboxSearchGroup.SearchState.Running;
			return this.asyncResult;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00032AD3 File Offset: 0x00030CD3
		public ISearchResult EndExecuteSearch(IAsyncResult result)
		{
			Util.ThrowOnNull(result, "result");
			if (!this.asyncResult.Equals(result))
			{
				throw new ArgumentException("asyncResult doesn't match with the group's async result");
			}
			this.asyncResult.AsyncWaitHandle.WaitOne();
			return this.resultAggregator;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00032B10 File Offset: 0x00030D10
		public void Abort()
		{
			if (this.searchState != MailboxSearchGroup.SearchState.Running)
			{
				return;
			}
			if (MailboxSearchGroup.abortSearchTestHook.Value != null)
			{
				MailboxSearchGroup.abortSearchTestHook.Value();
			}
			this.StopSearch();
			this.searchState = MailboxSearchGroup.SearchState.Stopped;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00032B44 File Offset: 0x00030D44
		protected virtual void ReportCompletion()
		{
			this.searchState = MailboxSearchGroup.SearchState.Completed;
			this.asyncResult.ReportCompletion();
		}

		// Token: 0x06000BA4 RID: 2980
		protected abstract void ExecuteSearch();

		// Token: 0x06000BA5 RID: 2981
		protected abstract void StopSearch();

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00032B58 File Offset: 0x00030D58
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					this.Abort();
				}
				finally
				{
					if (this.asyncResult != null)
					{
						this.asyncResult.Dispose();
						this.asyncResult = null;
					}
				}
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00032B9C File Offset: 0x00030D9C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxSearchGroup>(this);
		}

		// Token: 0x040008B2 RID: 2226
		private static Hookable<Action> abortSearchTestHook = Hookable<Action>.Create(false, null);

		// Token: 0x040008B3 RID: 2227
		private readonly ISearchResult resultAggregator;

		// Token: 0x040008B4 RID: 2228
		private readonly MailboxInfo[] mailboxes;

		// Token: 0x040008B5 RID: 2229
		private readonly SearchCriteria searchCriteria;

		// Token: 0x040008B6 RID: 2230
		private readonly PagingInfo pagingInfo;

		// Token: 0x040008B7 RID: 2231
		private readonly CallerInfo executingUser;

		// Token: 0x040008B8 RID: 2232
		private MailboxSearchGroup.SearchState searchState;

		// Token: 0x040008B9 RID: 2233
		private AsyncResult asyncResult;

		// Token: 0x020001B5 RID: 437
		protected enum SearchState
		{
			// Token: 0x040008BB RID: 2235
			NotStarted,
			// Token: 0x040008BC RID: 2236
			Running,
			// Token: 0x040008BD RID: 2237
			Stopped,
			// Token: 0x040008BE RID: 2238
			Completed
		}
	}
}
