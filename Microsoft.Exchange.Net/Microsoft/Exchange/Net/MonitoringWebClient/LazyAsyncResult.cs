using System;
using System.Threading;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007C6 RID: 1990
	internal class LazyAsyncResult : IAsyncResult
	{
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x00058165 File Offset: 0x00056365
		public object ResultObject
		{
			get
			{
				return this.resultObject;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x0005816D File Offset: 0x0005636D
		public object Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x00058175 File Offset: 0x00056375
		public LazyAsyncResult(AsyncCallback callback, object state)
		{
			this.callback = callback;
			this.asyncState = state;
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x00058196 File Offset: 0x00056396
		object IAsyncResult.AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x000581A0 File Offset: 0x000563A0
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.waitHandle == null)
				{
					lock (this.lockObject)
					{
						if (this.waitHandle == null)
						{
							this.waitHandle = new ManualResetEvent(false);
							if (this.isCompleted)
							{
								(this.waitHandle as ManualResetEvent).Set();
							}
						}
					}
				}
				return this.waitHandle;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x00058218 File Offset: 0x00056418
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x0005821B File Offset: 0x0005641B
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x00058228 File Offset: 0x00056428
		public void Complete(object resultObject, Exception exception)
		{
			this.resultObject = resultObject;
			this.exception = exception;
			this.isCompleted = true;
			if (this.waitHandle != null)
			{
				(this.waitHandle as ManualResetEvent).Set();
				this.waitHandle.Close();
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x04002470 RID: 9328
		private object asyncState;

		// Token: 0x04002471 RID: 9329
		private WaitHandle waitHandle;

		// Token: 0x04002472 RID: 9330
		private volatile bool isCompleted;

		// Token: 0x04002473 RID: 9331
		private object lockObject = new object();

		// Token: 0x04002474 RID: 9332
		private AsyncCallback callback;

		// Token: 0x04002475 RID: 9333
		private object resultObject;

		// Token: 0x04002476 RID: 9334
		private Exception exception;
	}
}
