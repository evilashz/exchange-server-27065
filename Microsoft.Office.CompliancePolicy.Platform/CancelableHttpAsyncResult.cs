using System;
using System.Threading;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000038 RID: 56
	internal sealed class CancelableHttpAsyncResult : ICancelableAsyncResult, IAsyncResult
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00003754 File Offset: 0x00001954
		internal CancelableHttpAsyncResult(AsyncCallback completionCallback, object state, HttpClient httpClient)
		{
			if (httpClient == null)
			{
				throw new ArgumentNullException("httpClient");
			}
			this.asyncState = state;
			this.httpClient = httpClient;
			this.completionCallback = completionCallback;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000378A File Offset: 0x0000198A
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003794 File Offset: 0x00001994
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000037FC File Offset: 0x000019FC
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003804 File Offset: 0x00001A04
		public bool IsCanceled
		{
			get
			{
				return this.exception is DownloadCanceledException;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003814 File Offset: 0x00001A14
		public bool CompletedSynchronously
		{
			get
			{
				return this.httpClient.CompletedSynchronously;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003821 File Offset: 0x00001A21
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003829 File Offset: 0x00001A29
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003832 File Offset: 0x00001A32
		internal HttpClient HttpClient
		{
			get
			{
				return this.httpClient;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000383A File Offset: 0x00001A3A
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003842 File Offset: 0x00001A42
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

		// Token: 0x060000CE RID: 206 RVA: 0x0000384C File Offset: 0x00001A4C
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

		// Token: 0x060000CF RID: 207 RVA: 0x000038B4 File Offset: 0x00001AB4
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

		// Token: 0x060000D0 RID: 208 RVA: 0x00003930 File Offset: 0x00001B30
		internal void InvokeCompleted(Exception e)
		{
			this.exception = e;
			this.InvokeCompleted();
		}

		// Token: 0x04000064 RID: 100
		private object syncRoot = new object();

		// Token: 0x04000065 RID: 101
		private object asyncState;

		// Token: 0x04000066 RID: 102
		private HttpClient httpClient;

		// Token: 0x04000067 RID: 103
		private ManualResetEvent asyncWaitHandle;

		// Token: 0x04000068 RID: 104
		private bool isCompleted;

		// Token: 0x04000069 RID: 105
		private AsyncCallback completionCallback;

		// Token: 0x0400006A RID: 106
		private Exception exception;

		// Token: 0x0400006B RID: 107
		private bool endCalled;
	}
}
