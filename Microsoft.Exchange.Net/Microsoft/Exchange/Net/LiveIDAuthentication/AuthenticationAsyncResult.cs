using System;
using System.Threading;

namespace Microsoft.Exchange.Net.LiveIDAuthentication
{
	// Token: 0x0200075E RID: 1886
	internal sealed class AuthenticationAsyncResult : ICancelableAsyncResult, IAsyncResult
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x0004D613 File Offset: 0x0004B813
		internal AuthenticationAsyncResult(CancelableAsyncCallback completionCallback, object state, LiveIDAuthenticationClient authenticationClient)
		{
			this.asyncState = state;
			this.completedSynchronously = true;
			this.authenticationClient = authenticationClient;
			this.completionCallback = completionCallback;
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x0004D642 File Offset: 0x0004B842
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x0004D64C File Offset: 0x0004B84C
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

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x0004D6BC File Offset: 0x0004B8BC
		public bool IsCanceled
		{
			get
			{
				return this.exception is OperationCanceledException;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x0004D6CC File Offset: 0x0004B8CC
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x0004D6D4 File Offset: 0x0004B8D4
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x0004D6DC File Offset: 0x0004B8DC
		public void Cancel()
		{
			bool flag = false;
			lock (this.syncRoot)
			{
				if (this.isCompleted)
				{
					return;
				}
				flag = this.authenticationClient.TryCancel();
			}
			if (flag)
			{
				this.ProcessCompleted(new OperationCanceledException());
			}
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x0004D73C File Offset: 0x0004B93C
		internal void SetAsync()
		{
			this.completedSynchronously = false;
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x0004D745 File Offset: 0x0004B945
		internal AuthenticationResult EndProcess()
		{
			this.EndCalled();
			this.WaitForCompletion();
			return this.GetAuthenticationResult();
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x0004D759 File Offset: 0x0004B959
		internal void ProcessCompleted(Exception e)
		{
			this.exception = e;
			this.ProcessCompleted();
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0004D768 File Offset: 0x0004B968
		internal void ProcessCompleted(BaseAuthenticationToken authToken)
		{
			this.token = authToken;
			this.ProcessCompleted();
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0004D778 File Offset: 0x0004B978
		private void ProcessCompleted()
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

		// Token: 0x06002517 RID: 9495 RVA: 0x0004D7F4 File Offset: 0x0004B9F4
		private void WaitForCompletion()
		{
			WaitHandle waitHandle = null;
			lock (this.syncRoot)
			{
				if (!this.isCompleted)
				{
					waitHandle = this.AsyncWaitHandle;
				}
			}
			if (waitHandle != null)
			{
				waitHandle.WaitOne();
			}
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x0004D84C File Offset: 0x0004BA4C
		private AuthenticationResult GetAuthenticationResult()
		{
			if (this.exception == null)
			{
				return new AuthenticationResult(this.token);
			}
			return new AuthenticationResult(this.exception);
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x0004D870 File Offset: 0x0004BA70
		private void EndCalled()
		{
			lock (this.syncRoot)
			{
				if (this.endCalled)
				{
					throw new InvalidOperationException("The EndInvoke can only be called once with an async Result.");
				}
				this.endCalled = true;
			}
		}

		// Token: 0x04002285 RID: 8837
		private object syncRoot = new object();

		// Token: 0x04002286 RID: 8838
		private object asyncState;

		// Token: 0x04002287 RID: 8839
		private LiveIDAuthenticationClient authenticationClient;

		// Token: 0x04002288 RID: 8840
		private ManualResetEvent asyncWaitHandle;

		// Token: 0x04002289 RID: 8841
		private bool completedSynchronously;

		// Token: 0x0400228A RID: 8842
		private bool isCompleted;

		// Token: 0x0400228B RID: 8843
		private CancelableAsyncCallback completionCallback;

		// Token: 0x0400228C RID: 8844
		private Exception exception;

		// Token: 0x0400228D RID: 8845
		private BaseAuthenticationToken token;

		// Token: 0x0400228E RID: 8846
		private bool endCalled;
	}
}
