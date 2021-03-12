using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007CA RID: 1994
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SearchFolderAsyncSearch : IDisposeTrackable, IDisposable
	{
		// Token: 0x06004ACA RID: 19146 RVA: 0x001393BC File Offset: 0x001375BC
		internal SearchFolderAsyncSearch(StoreSession session, StoreObjectId searchFolderId, AsyncCallback userCallback, object state)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
			this.state = state;
			this.userCallback = userCallback;
			this.asyncResult = new SearchFolderAsyncSearch.SearchFolderAsyncResult(this);
			this.subscription = Subscription.Create(session, new NotificationHandler(this.NotificationHandler), NotificationType.SearchComplete, searchFolderId, false);
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x00139424 File Offset: 0x00137624
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SearchFolderAsyncSearch>(this);
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x0013942C File Offset: 0x0013762C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06004ACD RID: 19149 RVA: 0x00139441 File Offset: 0x00137641
		internal IAsyncResult AsyncResult
		{
			get
			{
				return this.asyncResult;
			}
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x00139449 File Offset: 0x00137649
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x0013946B File Offset: 0x0013766B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x0013947A File Offset: 0x0013767A
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x0013949F File Offset: 0x0013769F
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.subscription.Dispose();
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x001394C2 File Offset: 0x001376C2
		private void NotificationHandler(Notification notification)
		{
			this.searchCompleteEvent.Set();
			if (this.userCallback != null)
			{
				this.userCallback(this.asyncResult);
			}
		}

		// Token: 0x040028A9 RID: 10409
		private readonly Subscription subscription;

		// Token: 0x040028AA RID: 10410
		private readonly SearchFolderAsyncSearch.SearchFolderAsyncResult asyncResult;

		// Token: 0x040028AB RID: 10411
		private readonly object state;

		// Token: 0x040028AC RID: 10412
		private readonly ManualResetEvent searchCompleteEvent = new ManualResetEvent(false);

		// Token: 0x040028AD RID: 10413
		private readonly AsyncCallback userCallback;

		// Token: 0x040028AE RID: 10414
		private bool isDisposed;

		// Token: 0x040028AF RID: 10415
		private readonly DisposeTracker disposeTracker;

		// Token: 0x020007CB RID: 1995
		private class SearchFolderAsyncResult : IAsyncResult
		{
			// Token: 0x06004AD3 RID: 19155 RVA: 0x001394E9 File Offset: 0x001376E9
			internal SearchFolderAsyncResult(SearchFolderAsyncSearch searchFolderAsyncSearch)
			{
				this.searchFolderAsyncSearch = searchFolderAsyncSearch;
			}

			// Token: 0x17001552 RID: 5458
			// (get) Token: 0x06004AD4 RID: 19156 RVA: 0x001394F8 File Offset: 0x001376F8
			public object AsyncState
			{
				get
				{
					return this.searchFolderAsyncSearch.state;
				}
			}

			// Token: 0x17001553 RID: 5459
			// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x00139505 File Offset: 0x00137705
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return this.searchFolderAsyncSearch.searchCompleteEvent;
				}
			}

			// Token: 0x17001554 RID: 5460
			// (get) Token: 0x06004AD6 RID: 19158 RVA: 0x00139512 File Offset: 0x00137712
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001555 RID: 5461
			// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x00139515 File Offset: 0x00137715
			public bool IsCompleted
			{
				get
				{
					return this.searchFolderAsyncSearch.searchCompleteEvent.WaitOne(0);
				}
			}

			// Token: 0x040028B0 RID: 10416
			private readonly SearchFolderAsyncSearch searchFolderAsyncSearch;
		}
	}
}
