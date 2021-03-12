using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ClientCallContext<TResult> : EasyAsyncResult where TResult : CallResult
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002192 File Offset: 0x00000392
		public ClientCallContext(AsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021A8 File Offset: 0x000003A8
		public IAsyncResult Begin()
		{
			try
			{
				this.latencyTracker.Start();
				this.OnBegin(new AsyncCallback(ClientCallContext<TResult>.InternalCallback), this);
			}
			catch (Exception ex)
			{
				this.result = this.ConvertExceptionToResult(ex);
				if (this.result == null)
				{
					this.exception = ex;
				}
				base.InvokeCallback();
			}
			return this;
		}

		// Token: 0x06000016 RID: 22
		protected abstract IAsyncResult OnBegin(AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000017 RID: 23
		protected abstract TResult OnEnd(IAsyncResult asyncResult);

		// Token: 0x06000018 RID: 24 RVA: 0x00002214 File Offset: 0x00000414
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

		// Token: 0x06000019 RID: 25 RVA: 0x00002254 File Offset: 0x00000454
		protected virtual TResult ConvertExceptionToResult(Exception exception)
		{
			return default(TResult);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000226A File Offset: 0x0000046A
		protected virtual TResult PostProcessResult(TResult result)
		{
			result.Latency = this.latencyTracker.Elapsed;
			result.Validate();
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002294 File Offset: 0x00000494
		private static void InternalCallback(IAsyncResult asyncResult)
		{
			ClientCallContext<TResult> clientCallContext = (ClientCallContext<TResult>)asyncResult.AsyncState;
			clientCallContext.InternalEnd(asyncResult);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022B4 File Offset: 0x000004B4
		private void InternalEnd(IAsyncResult asyncResult)
		{
			this.latencyTracker.Stop();
			try
			{
				this.result = this.OnEnd(asyncResult);
			}
			catch (Exception ex)
			{
				this.result = this.ConvertExceptionToResult(ex);
				if (this.result == null)
				{
					this.exception = ex;
				}
			}
			base.InvokeCallback();
		}

		// Token: 0x04000006 RID: 6
		private TResult result;

		// Token: 0x04000007 RID: 7
		private Exception exception;

		// Token: 0x04000008 RID: 8
		private Stopwatch latencyTracker = new Stopwatch();
	}
}
