using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AsyncResult<TState, TResultData> : LazyAsyncResult where TResultData : class
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002183 File Offset: 0x00000383
		public AsyncResult(object asyncOperator, TState state, AsyncCallback callback, object callbackState) : base(asyncOperator, callbackState, callback)
		{
			this.State = state;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002196 File Offset: 0x00000396
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000219E File Offset: 0x0000039E
		public TState State { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021A7 File Offset: 0x000003A7
		public new bool CompletedSynchronously
		{
			get
			{
				return base.CompletedSynchronously && (this.completedSynchronously || base.CompletedSynchronously);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021C3 File Offset: 0x000003C3
		public new AsyncCallback AsyncCallback
		{
			get
			{
				return base.AsyncCallback;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021CB File Offset: 0x000003CB
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000021D3 File Offset: 0x000003D3
		public IAsyncResult PendingAsyncResult { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021DC File Offset: 0x000003DC
		public bool IsCanceled
		{
			get
			{
				return this.Result != null && this.Result.IsCanceled;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021F3 File Offset: 0x000003F3
		public Exception Exception
		{
			get
			{
				if (this.Result != null)
				{
					return this.Result.Exception;
				}
				return null;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000220A File Offset: 0x0000040A
		public new AsyncOperationResult<TResultData> Result
		{
			get
			{
				return (AsyncOperationResult<TResultData>)base.Result;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002217 File Offset: 0x00000417
		public bool IsRetryable
		{
			get
			{
				return this.Exception is TransientException;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002227 File Offset: 0x00000427
		public void SetCompletedSynchronously()
		{
			this.completedSynchronously = true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002230 File Offset: 0x00000430
		public AsyncOperationResult<TResultData> WaitForCompletion()
		{
			return (AsyncOperationResult<TResultData>)base.InternalWaitForCompletion();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000223D File Offset: 0x0000043D
		public void ProcessCompleted(TResultData result)
		{
			this.InternalProcessCompleted(result, null);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002248 File Offset: 0x00000448
		public void ProcessCompleted(Exception exception)
		{
			this.InternalProcessCompleted(default(TResultData), exception);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002265 File Offset: 0x00000465
		public void ProcessCompleted(TResultData result, Exception exception)
		{
			this.InternalProcessCompleted(result, exception);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002270 File Offset: 0x00000470
		public void ProcessCompleted()
		{
			this.InternalProcessCompleted(default(TResultData), null);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002290 File Offset: 0x00000490
		public void ProcessCanceled()
		{
			this.InternalProcessCompleted(default(TResultData), AsyncOperationResult<TResultData>.CanceledException);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022B1 File Offset: 0x000004B1
		protected virtual void ProtectedProcessCompleted(TResultData result, Exception exception)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022B4 File Offset: 0x000004B4
		private void InternalProcessCompleted(TResultData result, Exception exception)
		{
			this.PendingAsyncResult = null;
			this.ProtectedProcessCompleted(result, exception);
			AsyncOperationResult<TResultData> value = new AsyncOperationResult<TResultData>(result, exception);
			base.InvokeCallback(value);
		}

		// Token: 0x04000004 RID: 4
		private bool completedSynchronously;
	}
}
