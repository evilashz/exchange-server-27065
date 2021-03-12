using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000062 RID: 98
	public class AsyncResult<T> : AsyncResult
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00009B9C File Offset: 0x00007D9C
		public AsyncResult(AsyncResultCallback<T> callback, object asyncState) : base(delegate(AsyncResult ar)
		{
			callback((AsyncResult<T>)ar);
		}, asyncState)
		{
			this.AsyncResultCallback = callback;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009BDC File Offset: 0x00007DDC
		public AsyncResult(AsyncCallback callback, object asyncState) : base(callback, asyncState)
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009BE6 File Offset: 0x00007DE6
		public AsyncResult(AsyncResultCallback<T> callback, object asyncState, T result) : base(asyncState, true)
		{
			this.AsyncResultCallback = callback;
			this.Result = result;
			base.IsCompleted = true;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009C05 File Offset: 0x00007E05
		public AsyncResult(AsyncCallback callback, object asyncState, T result) : base(asyncState, true)
		{
			base.Callback = callback;
			this.Result = result;
			base.IsCompleted = true;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009C24 File Offset: 0x00007E24
		public AsyncResult(AsyncEnumerator<T> enumerator, AsyncResultCallback<T> callback, object asyncState) : base(enumerator, null, asyncState)
		{
			this.AsyncResultCallback = callback;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00009C36 File Offset: 0x00007E36
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00009C3E File Offset: 0x00007E3E
		public T Result { get; internal set; }

		// Token: 0x06000221 RID: 545 RVA: 0x00009C48 File Offset: 0x00007E48
		public new T End()
		{
			T result;
			try
			{
				if (base.Exception != null)
				{
					throw AsyncExceptionWrapperHelper.GetAsyncWrapper(base.Exception);
				}
				result = this.Result;
			}
			finally
			{
				base.Dispose();
			}
			return result;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00009C8C File Offset: 0x00007E8C
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00009C94 File Offset: 0x00007E94
		private protected new AsyncResultCallback<T> AsyncResultCallback { protected get; private set; }

		// Token: 0x06000224 RID: 548 RVA: 0x00009C9D File Offset: 0x00007E9D
		protected override void InvokeCallback()
		{
			if (this.AsyncResultCallback != null)
			{
				this.AsyncResultCallback(this);
				return;
			}
			base.InvokeCallback();
		}
	}
}
