using System;
using System.Threading;

namespace Microsoft.Exchange.Net.Protocols
{
	// Token: 0x02000827 RID: 2087
	internal sealed class ProtocolAsyncResult : ICancelableAsyncResult, IAsyncResult
	{
		// Token: 0x06002C3C RID: 11324 RVA: 0x0006470A File Offset: 0x0006290A
		internal ProtocolAsyncResult(CancelableAsyncCallback completionCallback, object state, ProtocolClient protocolClient)
		{
			if (protocolClient == null)
			{
				throw new ArgumentNullException("protocolClient");
			}
			this.asyncState = state;
			this.completedSynchronously = true;
			this.protocolClient = protocolClient;
			this.completionCallback = completionCallback;
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x00064747 File Offset: 0x00062947
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x0006474F File Offset: 0x0006294F
		public CancelableAsyncCallback CompletionCallback
		{
			get
			{
				return this.completionCallback;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x00064758 File Offset: 0x00062958
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

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000647C0 File Offset: 0x000629C0
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000647C8 File Offset: 0x000629C8
		public bool IsCanceled
		{
			get
			{
				return this.protocolResult != null && this.protocolResult.IsCanceled;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x000647DF File Offset: 0x000629DF
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000647E8 File Offset: 0x000629E8
		public void Cancel()
		{
			bool flag = false;
			lock (this.syncRoot)
			{
				if (this.isCompleted)
				{
					return;
				}
				flag = this.protocolClient.TryCancel();
			}
			if (flag)
			{
				this.ProcessCompleted(ProtocolAsyncResult.OperationCanceledException);
			}
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x00064848 File Offset: 0x00062A48
		internal void SetAsync()
		{
			this.completedSynchronously = false;
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x00064851 File Offset: 0x00062A51
		internal ProtocolResult EndProcess()
		{
			this.EndCalled();
			this.WaitForCompletion();
			return this.protocolResult;
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x00064865 File Offset: 0x00062A65
		internal void ProcessCompleted(Exception exception)
		{
			this.protocolResult = new ProtocolResult(exception);
			this.ProcessCompleted();
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x00064879 File Offset: 0x00062A79
		internal void ProcessCompleted(ResultData resultData)
		{
			this.protocolResult = new ProtocolResult(resultData);
			this.ProcessCompleted();
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x00064890 File Offset: 0x00062A90
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

		// Token: 0x06002C49 RID: 11337 RVA: 0x0006490C File Offset: 0x00062B0C
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

		// Token: 0x06002C4A RID: 11338 RVA: 0x00064964 File Offset: 0x00062B64
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

		// Token: 0x04002685 RID: 9861
		private static readonly OperationCanceledException OperationCanceledException = new OperationCanceledException();

		// Token: 0x04002686 RID: 9862
		private object syncRoot = new object();

		// Token: 0x04002687 RID: 9863
		private object asyncState;

		// Token: 0x04002688 RID: 9864
		private ProtocolClient protocolClient;

		// Token: 0x04002689 RID: 9865
		private ManualResetEvent asyncWaitHandle;

		// Token: 0x0400268A RID: 9866
		private bool completedSynchronously;

		// Token: 0x0400268B RID: 9867
		private bool isCompleted;

		// Token: 0x0400268C RID: 9868
		private CancelableAsyncCallback completionCallback;

		// Token: 0x0400268D RID: 9869
		private ProtocolResult protocolResult;

		// Token: 0x0400268E RID: 9870
		private bool endCalled;
	}
}
