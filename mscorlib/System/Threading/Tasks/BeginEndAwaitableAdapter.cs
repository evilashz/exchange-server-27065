using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200055B RID: 1371
	internal sealed class BeginEndAwaitableAdapter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x0600417C RID: 16764 RVA: 0x000F3A0A File Offset: 0x000F1C0A
		public BeginEndAwaitableAdapter GetAwaiter()
		{
			return this;
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x000F3A0D File Offset: 0x000F1C0D
		public bool IsCompleted
		{
			get
			{
				return this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN;
			}
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000F3A1F File Offset: 0x000F1C1F
		[SecurityCritical]
		public void UnsafeOnCompleted(Action continuation)
		{
			this.OnCompleted(continuation);
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x000F3A28 File Offset: 0x000F1C28
		public void OnCompleted(Action continuation)
		{
			if (this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN || Interlocked.CompareExchange<Action>(ref this._continuation, continuation, null) == BeginEndAwaitableAdapter.CALLBACK_RAN)
			{
				Task.Run(continuation);
			}
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x000F3A5C File Offset: 0x000F1C5C
		public IAsyncResult GetResult()
		{
			IAsyncResult asyncResult = this._asyncResult;
			this._asyncResult = null;
			this._continuation = null;
			return asyncResult;
		}

		// Token: 0x04001AFD RID: 6909
		private static readonly Action CALLBACK_RAN = delegate()
		{
		};

		// Token: 0x04001AFE RID: 6910
		private IAsyncResult _asyncResult;

		// Token: 0x04001AFF RID: 6911
		private Action _continuation;

		// Token: 0x04001B00 RID: 6912
		public static readonly AsyncCallback Callback = delegate(IAsyncResult asyncResult)
		{
			BeginEndAwaitableAdapter beginEndAwaitableAdapter = (BeginEndAwaitableAdapter)asyncResult.AsyncState;
			beginEndAwaitableAdapter._asyncResult = asyncResult;
			Action action = Interlocked.Exchange<Action>(ref beginEndAwaitableAdapter._continuation, BeginEndAwaitableAdapter.CALLBACK_RAN);
			if (action != null)
			{
				action();
			}
		};
	}
}
