using System;
using System.Threading;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000179 RID: 377
	internal class OwaAsyncResult : IAsyncResult
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x000592E7 File Offset: 0x000574E7
		internal OwaAsyncResult(AsyncCallback callback, object extraData)
		{
			this.callback = callback;
			this.extraData = extraData;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x000592FD File Offset: 0x000574FD
		public object AsyncState
		{
			get
			{
				return this.extraData;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00059308 File Offset: 0x00057508
		public bool CompletedSynchronously
		{
			get
			{
				bool result;
				lock (this)
				{
					result = this.completedSynchronously;
				}
				return result;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00059348 File Offset: 0x00057548
		public bool IsCompleted
		{
			get
			{
				bool result;
				lock (this)
				{
					result = this.isCompleted;
				}
				return result;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00059388 File Offset: 0x00057588
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.setEvent == null)
				{
					this.setEvent = new ManualResetEvent(false);
				}
				return this.setEvent;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x000593A4 File Offset: 0x000575A4
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x000593E4 File Offset: 0x000575E4
		public Exception Exception
		{
			get
			{
				Exception result;
				lock (this)
				{
					result = this.exception;
				}
				return result;
			}
			set
			{
				lock (this)
				{
					if (this.isCompleted || this.exception != null)
					{
						throw new OwaInvalidOperationException("The request is already finished, or an exception was already registered for this OwaAsyncResult." + ((this.exception != null) ? ("Previous exception message: " + this.exception.Message) : string.Empty) + ((value != null) ? (" Current exception message: " + value.Message) : string.Empty));
					}
					this.exception = value;
				}
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00059480 File Offset: 0x00057680
		internal void CompleteRequest(bool completedSynchronously)
		{
			lock (this)
			{
				if (this.isCompleted)
				{
					return;
				}
				this.isCompleted = true;
				this.completedSynchronously = completedSynchronously;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
			if (this.setEvent != null)
			{
				this.setEvent.Set();
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000594F8 File Offset: 0x000576F8
		internal void CompleteRequest(bool completedSynchronously, Exception exception)
		{
			this.Exception = exception;
			this.CompleteRequest(completedSynchronously);
		}

		// Token: 0x04000926 RID: 2342
		private bool isCompleted;

		// Token: 0x04000927 RID: 2343
		private bool completedSynchronously;

		// Token: 0x04000928 RID: 2344
		private AsyncCallback callback;

		// Token: 0x04000929 RID: 2345
		private object extraData;

		// Token: 0x0400092A RID: 2346
		private ManualResetEvent setEvent;

		// Token: 0x0400092B RID: 2347
		private Exception exception;
	}
}
