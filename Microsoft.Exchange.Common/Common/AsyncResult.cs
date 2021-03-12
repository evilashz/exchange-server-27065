using System;
using System.Threading;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000061 RID: 97
	public class AsyncResult : IAsyncResult, IDisposable
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00009875 File Offset: 0x00007A75
		public AsyncResult(AsyncCallback callback, object asyncState)
		{
			this.Callback = callback;
			this.AsyncState = asyncState;
			this.IsCompleted = false;
			this.completedSynchronously = false;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009899 File Offset: 0x00007A99
		public AsyncResult(AsyncResultCallback callback, object asyncState)
		{
			this.AsyncResultCallback = callback;
			this.AsyncState = asyncState;
			this.IsCompleted = false;
			this.completedSynchronously = false;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000098BD File Offset: 0x00007ABD
		public AsyncResult(AsyncCallback callback, object asyncState, bool completedSynchronously)
		{
			this.Callback = callback;
			this.AsyncState = asyncState;
			this.completedSynchronously = completedSynchronously;
			this.IsCompleted = this.completedSynchronously;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000098E6 File Offset: 0x00007AE6
		public AsyncResult(AsyncResultCallback callback, object asyncState, bool completedSynchronously)
		{
			this.AsyncResultCallback = callback;
			this.AsyncState = asyncState;
			this.completedSynchronously = completedSynchronously;
			this.IsCompleted = this.completedSynchronously;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000990F File Offset: 0x00007B0F
		internal AsyncResult(AsyncEnumerator enumerator, AsyncResultCallback callback, object asyncState)
		{
			this.asyncEnumerator = enumerator;
			this.AsyncResultCallback = callback;
			this.AsyncState = asyncState;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000992C File Offset: 0x00007B2C
		protected AsyncResult(object asyncState, bool completedSynchronously)
		{
			this.AsyncState = asyncState;
			this.completedSynchronously = completedSynchronously;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009942 File Offset: 0x00007B42
		public void Dispose()
		{
			if (this.completedEvent != null)
			{
				((IDisposable)this.completedEvent).Dispose();
			}
			if (this.asyncEnumerator != null)
			{
				this.asyncEnumerator.Dispose();
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000996E File Offset: 0x00007B6E
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00009976 File Offset: 0x00007B76
		public object AsyncState { get; protected set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000997F File Offset: 0x00007B7F
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.completedEvent == null)
				{
					this.completedEvent = new ManualResetEvent(this.isCompleted);
				}
				return this.completedEvent;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000099A8 File Offset: 0x00007BA8
		// (set) Token: 0x0600020A RID: 522 RVA: 0x000099B0 File Offset: 0x00007BB0
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
			internal set
			{
				this.completedSynchronously = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000099B9 File Offset: 0x00007BB9
		// (set) Token: 0x0600020C RID: 524 RVA: 0x000099C4 File Offset: 0x00007BC4
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
			set
			{
				if (this.isCompleted)
				{
					throw new InvalidOperationException("Can't set completed multiple times");
				}
				this.isCompleted = value;
				if (this.isCompleted)
				{
					if (this.completedEvent != null)
					{
						this.completedEvent.Set();
					}
					this.InvokeCallback();
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00009A17 File Offset: 0x00007C17
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00009A1F File Offset: 0x00007C1F
		public bool IsAborted { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00009A28 File Offset: 0x00007C28
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00009A30 File Offset: 0x00007C30
		public Exception Exception { get; internal set; }

		// Token: 0x06000211 RID: 529 RVA: 0x00009A3C File Offset: 0x00007C3C
		public void Abort()
		{
			if (!this.IsCompleted)
			{
				try
				{
					if (this.OnAbort != null)
					{
						this.OnAbort();
					}
				}
				finally
				{
					this.IsCompleted = true;
					this.IsAborted = true;
				}
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009A88 File Offset: 0x00007C88
		public void End()
		{
			try
			{
				if (this.Exception != null)
				{
					throw AsyncExceptionWrapperHelper.GetAsyncWrapper(this.Exception);
				}
			}
			finally
			{
				this.Dispose();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00009AC4 File Offset: 0x00007CC4
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00009ACC File Offset: 0x00007CCC
		protected AsyncCallback Callback { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00009AD5 File Offset: 0x00007CD5
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00009ADD File Offset: 0x00007CDD
		private protected AsyncResultCallback AsyncResultCallback { protected get; private set; }

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000217 RID: 535 RVA: 0x00009AE8 File Offset: 0x00007CE8
		// (remove) Token: 0x06000218 RID: 536 RVA: 0x00009B20 File Offset: 0x00007D20
		public event Action OnAbort;

		// Token: 0x06000219 RID: 537 RVA: 0x00009B55 File Offset: 0x00007D55
		protected virtual void InvokeCallback()
		{
			if (this.AsyncResultCallback != null)
			{
				this.AsyncResultCallback(this);
				return;
			}
			if (this.Callback != null)
			{
				this.Callback(this);
			}
		}

		// Token: 0x040001A0 RID: 416
		private volatile ManualResetEvent completedEvent;

		// Token: 0x040001A1 RID: 417
		private volatile bool isCompleted;

		// Token: 0x040001A2 RID: 418
		private bool completedSynchronously;

		// Token: 0x040001A3 RID: 419
		private AsyncEnumerator asyncEnumerator;
	}
}
