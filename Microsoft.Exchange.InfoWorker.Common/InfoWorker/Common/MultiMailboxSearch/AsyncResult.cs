using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001BC RID: 444
	internal class AsyncResult : DisposeTrackableBase, IAsyncResult
	{
		// Token: 0x06000C04 RID: 3076 RVA: 0x000349E7 File Offset: 0x00032BE7
		internal AsyncResult(AsyncCallback callback, object state)
		{
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00034A09 File Offset: 0x00032C09
		public void ReportCompletion()
		{
			if (!this.searchCompleteEvent.WaitOne(0))
			{
				this.searchCompleteEvent.Set();
				if (this.callback != null)
				{
					this.callback(this);
				}
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00034A39 File Offset: 0x00032C39
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00034A41 File Offset: 0x00032C41
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.searchCompleteEvent;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00034A49 File Offset: 0x00032C49
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00034A4C File Offset: 0x00032C4C
		public bool IsCompleted
		{
			get
			{
				return this.searchCompleteEvent.WaitOne(0, false);
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00034A5C File Offset: 0x00032C5C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.searchCompleteEvent != null)
			{
				try
				{
					this.searchCompleteEvent.WaitOne();
				}
				finally
				{
					this.searchCompleteEvent.Close();
				}
				this.searchCompleteEvent = null;
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00034AA8 File Offset: 0x00032CA8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AsyncResult>(this);
		}

		// Token: 0x040008E7 RID: 2279
		private ManualResetEvent searchCompleteEvent = new ManualResetEvent(false);

		// Token: 0x040008E8 RID: 2280
		private AsyncCallback callback;

		// Token: 0x040008E9 RID: 2281
		private object state;
	}
}
