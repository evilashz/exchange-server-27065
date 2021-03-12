using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200005E RID: 94
	public class AsyncEnumerator<T> : AsyncEnumerator
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00009728 File Offset: 0x00007928
		public AsyncEnumerator(AsyncResultCallback<T> callback, object asyncState, Func<AsyncEnumerator<T>, IEnumerator<int>> enumeratorCallback) : this(callback, asyncState, enumeratorCallback, true)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009734 File Offset: 0x00007934
		public AsyncEnumerator(AsyncCallback callback, object asyncState, Func<AsyncEnumerator<T>, IEnumerator<int>> enumeratorCallback) : this(callback, asyncState, enumeratorCallback, true)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009740 File Offset: 0x00007940
		public AsyncEnumerator(AsyncResultCallback<T> callback, object asyncState, Func<AsyncEnumerator<T>, IEnumerator<int>> enumeratorCallback, bool startAsyncOperation)
		{
			base.AsyncResult = new AsyncResult<T>(this, callback, asyncState);
			this.enumerator = enumeratorCallback(this);
			if (startAsyncOperation)
			{
				base.Begin();
			}
			base.ConstructorDone = true;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000978C File Offset: 0x0000798C
		public AsyncEnumerator(AsyncCallback callback, object asyncState, Func<AsyncEnumerator<T>, IEnumerator<int>> enumeratorCallback, bool startAsyncOperation)
		{
			base.AsyncResult = new AsyncResult<T>(this, delegate(AsyncResult<T> ar)
			{
				callback(ar);
			}, asyncState);
			this.enumerator = enumeratorCallback(this);
			if (startAsyncOperation)
			{
				base.Begin();
			}
			base.ConstructorDone = true;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000097EA File Offset: 0x000079EA
		public new AsyncResult<T> AsyncResult
		{
			get
			{
				return (AsyncResult<T>)base.AsyncResult;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000097F7 File Offset: 0x000079F7
		public void End(T result)
		{
			this.AsyncResult.CompletedSynchronously = !base.ConstructorDone;
			this.AsyncResult.Result = result;
			this.endedWithResult = true;
			base.End();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009826 File Offset: 0x00007A26
		protected override void VerifySuccessfullyCompleted()
		{
			if (this.AsyncResult.Exception == null && this.AsyncResult.IsCompleted && !this.endedWithResult)
			{
				throw new InvalidOperationException("Resulted not assigned to async operation");
			}
			base.VerifySuccessfullyCompleted();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000985B File Offset: 0x00007A5B
		protected override void ThrowForMoreAsyncsAfterCompletion()
		{
			if (this.AsyncResult.IsCompleted)
			{
				throw new InvalidOperationException("Can't do more asyncCalls after End has been called on AsyncEnumerator");
			}
		}

		// Token: 0x0400019F RID: 415
		private bool endedWithResult;
	}
}
