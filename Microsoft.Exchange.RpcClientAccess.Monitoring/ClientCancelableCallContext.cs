using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ClientCancelableCallContext<TResult> : EasyAsyncResult where TResult : CallResult
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002318 File Offset: 0x00000518
		public ClientCancelableCallContext(AsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002377 File Offset: 0x00000577
		public IAsyncResult Begin()
		{
			if (!this.DeferExceptions<object>(delegate(object unused)
			{
				this.latencyTracker.Start();
				this.cancelableAsyncResult = this.OnBegin(delegate(ICancelableAsyncResult asyncResult)
				{
					((ClientCancelableCallContext<TResult>)asyncResult.AsyncState).InternalEnd(asyncResult);
				}, this);
			}, null))
			{
				this.InternalEnd(null);
			}
			return this;
		}

		// Token: 0x0600001F RID: 31
		protected abstract ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000020 RID: 32
		protected abstract TResult OnEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000021 RID: 33 RVA: 0x00002398 File Offset: 0x00000598
		protected TResult GetResult()
		{
			base.WaitForCompletion();
			if (this.exception != null)
			{
				throw this.exception;
			}
			TResult tresult = this.PostProcessResult(this.result);
			tresult.Validate();
			return tresult;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023D8 File Offset: 0x000005D8
		protected virtual TResult ConvertExceptionToResult(Exception exception)
		{
			return default(TResult);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023EE File Offset: 0x000005EE
		protected void Cancel()
		{
			if (this.cancelableAsyncResult != null)
			{
				this.cancelableAsyncResult.Cancel();
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002403 File Offset: 0x00000603
		protected virtual TResult PostProcessResult(TResult result)
		{
			result.Latency = this.latencyTracker.Elapsed;
			result.Validate();
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000242C File Offset: 0x0000062C
		protected bool DeferExceptions<TArgIn>(Action<TArgIn> guardedAction, TArgIn arg)
		{
			bool flag;
			try
			{
				guardedAction(arg);
				flag = true;
			}
			catch (Exception ex)
			{
				this.result = this.ConvertExceptionToResult(ex);
				if (this.result == null)
				{
					this.exception = ex;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000248C File Offset: 0x0000068C
		private void InternalEnd(ICancelableAsyncResult asyncResult)
		{
			this.latencyTracker.Stop();
			if (asyncResult != null)
			{
				this.cancelableAsyncResult = asyncResult;
				this.DeferExceptions<ICancelableAsyncResult>(delegate(ICancelableAsyncResult r)
				{
					this.result = this.OnEnd(r);
				}, asyncResult);
			}
			base.InvokeCallback();
		}

		// Token: 0x04000009 RID: 9
		private TResult result;

		// Token: 0x0400000A RID: 10
		private Exception exception;

		// Token: 0x0400000B RID: 11
		private ICancelableAsyncResult cancelableAsyncResult;

		// Token: 0x0400000C RID: 12
		private Stopwatch latencyTracker = new Stopwatch();
	}
}
