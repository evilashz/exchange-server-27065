using System;
using System.Threading;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000730 RID: 1840
	internal sealed class CancelableHttpAsyncResult : ICancelableAsyncResult, IAsyncResult
	{
		// Token: 0x06002321 RID: 8993 RVA: 0x00047B90 File Offset: 0x00045D90
		internal CancelableHttpAsyncResult(CancelableAsyncCallback completionCallback, object state, HttpClient httpClient)
		{
			if (httpClient == null)
			{
				throw new ArgumentNullException("httpClient");
			}
			this.asyncState = state;
			this.httpClient = httpClient;
			this.completionCallback = completionCallback;
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x00047BC6 File Offset: 0x00045DC6
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x00047BD0 File Offset: 0x00045DD0
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				lock (this.syncRoot)
				{
					if (this.asyncWaitHandle == null)
					{
						this.asyncWaitHandle = new ManualResetEvent(false);
						if (this.isCompleted)
						{
							this.asyncWaitHandle.Set();
						}
					}
				}
				return this.asyncWaitHandle;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x00047C38 File Offset: 0x00045E38
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x00047C40 File Offset: 0x00045E40
		public bool IsCanceled
		{
			get
			{
				return this.exception is DownloadCanceledException;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x00047C50 File Offset: 0x00045E50
		public bool CompletedSynchronously
		{
			get
			{
				return this.httpClient.CompletedSynchronously;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x00047C5D File Offset: 0x00045E5D
		// (set) Token: 0x06002328 RID: 9000 RVA: 0x00047C65 File Offset: 0x00045E65
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			internal set
			{
				this.exception = value;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x00047C6E File Offset: 0x00045E6E
		internal HttpClient HttpClient
		{
			get
			{
				return this.httpClient;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x00047C76 File Offset: 0x00045E76
		// (set) Token: 0x0600232B RID: 9003 RVA: 0x00047C7E File Offset: 0x00045E7E
		internal bool EndCalled
		{
			get
			{
				return this.endCalled;
			}
			set
			{
				this.endCalled = value;
			}
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00047C88 File Offset: 0x00045E88
		public void Cancel()
		{
			bool flag = false;
			Exception e = new DownloadCanceledException();
			lock (this.syncRoot)
			{
				if (this.isCompleted)
				{
					return;
				}
				flag = this.httpClient.TryClose(HttpClient.GetDisconnectReason(e));
			}
			if (flag)
			{
				this.InvokeCompleted(e);
			}
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x00047CF0 File Offset: 0x00045EF0
		internal void InvokeCompleted()
		{
			lock (this.syncRoot)
			{
				if (this.isCompleted)
				{
					throw new InvalidOperationException("Operation already completed");
				}
				this.isCompleted = true;
				if (this.asyncWaitHandle != null)
				{
					this.asyncWaitHandle.Set();
				}
			}
			if (this.completionCallback != null)
			{
				this.completionCallback(this);
			}
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00047D6C File Offset: 0x00045F6C
		internal void InvokeCompleted(Exception e)
		{
			this.exception = e;
			this.InvokeCompleted();
		}

		// Token: 0x0400213B RID: 8507
		private object syncRoot = new object();

		// Token: 0x0400213C RID: 8508
		private object asyncState;

		// Token: 0x0400213D RID: 8509
		private HttpClient httpClient;

		// Token: 0x0400213E RID: 8510
		private ManualResetEvent asyncWaitHandle;

		// Token: 0x0400213F RID: 8511
		private bool isCompleted;

		// Token: 0x04002140 RID: 8512
		private CancelableAsyncCallback completionCallback;

		// Token: 0x04002141 RID: 8513
		private Exception exception;

		// Token: 0x04002142 RID: 8514
		private bool endCalled;
	}
}
