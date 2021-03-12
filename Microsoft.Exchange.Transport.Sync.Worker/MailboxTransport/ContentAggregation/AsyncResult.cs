using System;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AsyncResult<TState, TResultData> : LazyAsyncResult where TResultData : class
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00008265 File Offset: 0x00006465
		public AsyncResult(object asyncOperator, TState state, AsyncCallback callback, object callbackState, object syncPoisonContext) : base(asyncOperator, callbackState, callback)
		{
			this.state = state;
			this.syncPoisonContext = syncPoisonContext;
			this.SetPoisonContextOnCurrentThread();
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00008286 File Offset: 0x00006486
		public object SyncPoisonContext
		{
			get
			{
				return this.syncPoisonContext;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000828E File Offset: 0x0000648E
		public object AsyncOperator
		{
			get
			{
				return base.AsyncObject;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00008296 File Offset: 0x00006496
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000829E File Offset: 0x0000649E
		public TState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000082A7 File Offset: 0x000064A7
		public new bool CompletedSynchronously
		{
			get
			{
				return base.CompletedSynchronously && (this.completedSynchronously || base.CompletedSynchronously);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000082C3 File Offset: 0x000064C3
		public new AsyncCallback AsyncCallback
		{
			get
			{
				return base.AsyncCallback;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000082CB File Offset: 0x000064CB
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000082D3 File Offset: 0x000064D3
		public IAsyncResult PendingAsyncResult
		{
			get
			{
				return this.pendingAsyncResult;
			}
			set
			{
				this.pendingAsyncResult = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000082DC File Offset: 0x000064DC
		public bool IsCanceled
		{
			get
			{
				return this.Result != null && this.Result.IsCanceled;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000082F3 File Offset: 0x000064F3
		public Exception Exception
		{
			get
			{
				if (this.Result == null)
				{
					return null;
				}
				return this.Result.Exception;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000830A File Offset: 0x0000650A
		public new AsyncOperationResult<TResultData> Result
		{
			get
			{
				return (AsyncOperationResult<TResultData>)base.Result;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00008317 File Offset: 0x00006517
		public bool IsRetryable
		{
			get
			{
				return this.Exception is TransientException;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00008327 File Offset: 0x00006527
		private new WaitHandle AsyncWaitHandle
		{
			get
			{
				throw new InvalidOperationException("Invalid usage of AsyncResult. This method is not to be used outside of internal AsyncResult implementation.");
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008350 File Offset: 0x00006550
		public static WaitCallback GetWaitCallbackWithClearPoisonContext(WaitCallback userCallback)
		{
			if (userCallback == null)
			{
				return null;
			}
			return delegate(object state)
			{
				userCallback(state);
				AsyncResult<TState, TResultData>.ClearPoisonContextFromCurrentThread();
			};
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008380 File Offset: 0x00006580
		private static void ClearPoisonContextFromCurrentThread()
		{
			SyncPoisonHandler.ClearSyncPoisonContextFromCurrentThread();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000083B0 File Offset: 0x000065B0
		public WaitCallback GetWaitCallbackWithPoisonContext(WaitCallback userCallback)
		{
			if (userCallback == null)
			{
				return null;
			}
			return delegate(object state)
			{
				this.SetPoisonContextOnCurrentThread();
				userCallback(state);
				AsyncResult<TState, TResultData>.ClearPoisonContextFromCurrentThread();
			};
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008410 File Offset: 0x00006610
		public AsyncCallback GetAsyncCallbackWithPoisonContext(AsyncCallback userCallback)
		{
			if (userCallback == null)
			{
				return null;
			}
			return delegate(IAsyncResult asyncResult)
			{
				this.SetPoisonContextOnCurrentThread();
				userCallback(asyncResult);
				AsyncResult<TState, TResultData>.ClearPoisonContextFromCurrentThread();
			};
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000084E4 File Offset: 0x000066E4
		public AsyncCallback GetAsyncCallbackWithPoisonContextAndUnhandledExceptionRedirect(AsyncCallback userCallback)
		{
			if (userCallback == null)
			{
				return null;
			}
			return delegate(IAsyncResult asyncResult)
			{
				this.SetPoisonContextOnCurrentThread();
				try
				{
					userCallback(asyncResult);
					AsyncResult<TState, TResultData>.ClearPoisonContextFromCurrentThread();
				}
				catch (Exception exception)
				{
					Exception exception2;
					Exception exception = exception2;
					ThreadPool.QueueUserWorkItem(delegate(object state)
					{
						this.SetPoisonContextOnCurrentThread();
						throw new SyncUnhandledException(exception.GetType(), exception);
					});
				}
			};
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000085B8 File Offset: 0x000067B8
		public CancelableAsyncCallback GetCancelableAsyncCallbackWithPoisonContextAndUnhandledExceptionRedirect(CancelableAsyncCallback userCallback)
		{
			if (userCallback == null)
			{
				return null;
			}
			return delegate(ICancelableAsyncResult asyncResult)
			{
				this.SetPoisonContextOnCurrentThread();
				try
				{
					userCallback(asyncResult);
					AsyncResult<TState, TResultData>.ClearPoisonContextFromCurrentThread();
				}
				catch (Exception exception)
				{
					Exception exception2;
					Exception exception = exception2;
					SyncUtilities.RunUserWorkItemOnNewThreadAndBlockCurrentThread(delegate
					{
						this.SetPoisonContextOnCurrentThread();
						throw new SyncUnhandledException(exception.GetType(), exception);
					});
				}
			};
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000085EF File Offset: 0x000067EF
		public void SetCompletedSynchronously()
		{
			this.completedSynchronously = true;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000085F8 File Offset: 0x000067F8
		public AsyncOperationResult<TResultData> WaitForCompletion()
		{
			return (AsyncOperationResult<TResultData>)base.InternalWaitForCompletion();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008605 File Offset: 0x00006805
		public void ProcessCompleted(TResultData result)
		{
			this.InternalProcessCompleted(result, null);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008610 File Offset: 0x00006810
		public void ProcessCompleted(Exception exception)
		{
			this.InternalProcessCompleted(default(TResultData), exception);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000862D File Offset: 0x0000682D
		public void ProcessCompleted(TResultData result, Exception exception)
		{
			this.InternalProcessCompleted(result, exception);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008638 File Offset: 0x00006838
		public void ProcessCompleted()
		{
			this.InternalProcessCompleted(default(TResultData), null);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008658 File Offset: 0x00006858
		public void ProcessCanceled()
		{
			this.InternalProcessCompleted(default(TResultData), AsyncOperationResult<TResultData>.CanceledException);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008679 File Offset: 0x00006879
		protected virtual void ProtectedProcessCompleted(TResultData result, Exception exception)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000867C File Offset: 0x0000687C
		private void InternalProcessCompleted(TResultData result, Exception exception)
		{
			this.SetPoisonContextOnCurrentThread();
			this.pendingAsyncResult = null;
			this.ProtectedProcessCompleted(result, exception);
			AsyncOperationResult<TResultData> value = new AsyncOperationResult<TResultData>(result, exception);
			base.InvokeCallback(value);
			AsyncResult<TState, TResultData>.ClearPoisonContextFromCurrentThread();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000086B3 File Offset: 0x000068B3
		private void SetPoisonContextOnCurrentThread()
		{
			SyncPoisonHandler.SetSyncPoisonContextOnCurrentThread(this.syncPoisonContext);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000086C0 File Offset: 0x000068C0
		private new void InvokeCallback()
		{
			throw new InvalidOperationException("Invalid usage of AsyncResult. Use ProcessCompleted.");
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000086CC File Offset: 0x000068CC
		private new void InvokeCallback(object value)
		{
			throw new InvalidOperationException("Invalid usage of AsyncResult. Use ProcessCompleted.");
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000086D8 File Offset: 0x000068D8
		private new void InternalCleanup()
		{
			throw new InvalidOperationException("Invalid usage of AsyncResult. This method is not to be used outside of internal AsyncResult implementation.");
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000086E4 File Offset: 0x000068E4
		private new object InternalWaitForCompletion()
		{
			throw new InvalidOperationException("Invalid usage of AsyncResult. This method is not to be used outside of internal AsyncResult implementation.");
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000086F0 File Offset: 0x000068F0
		private new object InternalWaitForCompletionNoSideEffects()
		{
			throw new InvalidOperationException("Invalid usage of AsyncResult. This method is not to be used outside of internal AsyncResult implementation.");
		}

		// Token: 0x040000F1 RID: 241
		private readonly object syncPoisonContext;

		// Token: 0x040000F2 RID: 242
		private TState state;

		// Token: 0x040000F3 RID: 243
		private bool completedSynchronously;

		// Token: 0x040000F4 RID: 244
		private IAsyncResult pendingAsyncResult;
	}
}
