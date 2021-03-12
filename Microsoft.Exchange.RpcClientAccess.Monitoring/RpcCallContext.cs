using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MapiHttp;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RpcCallContext<TResult> : ClientCancelableCallContext<TResult> where TResult : RpcCallResult
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002759 File Offset: 0x00000959
		public RpcCallContext(TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
			this.timeout = timeout;
			this.timer = null;
			this.completedTimer = 0;
		}

		// Token: 0x06000048 RID: 72
		protected abstract TResult OnRpcException(RpcException rpcException);

		// Token: 0x06000049 RID: 73 RVA: 0x00002778 File Offset: 0x00000978
		protected virtual TResult OnProtocolException(ProtocolException protocolException)
		{
			throw new NotImplementedException("OnProtocolException");
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002784 File Offset: 0x00000984
		protected override TResult ConvertExceptionToResult(Exception exception)
		{
			RpcException ex = exception as RpcException;
			if (ex != null)
			{
				return this.OnRpcException(ex);
			}
			ProtocolException ex2 = exception as ProtocolException;
			if (ex2 != null)
			{
				return this.OnProtocolException(ex2);
			}
			AggregateException ex3 = exception as AggregateException;
			if (ex3 != null)
			{
				foreach (Exception ex4 in ex3.Flatten().InnerExceptions)
				{
					ex2 = (ex4 as ProtocolException);
					if (ex2 != null)
					{
						return this.OnProtocolException(ex2);
					}
				}
			}
			return default(TResult);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002824 File Offset: 0x00000A24
		protected void StartRpcTimer()
		{
			this.timer = new Timer(new TimerCallback(this.TimeoutCallback), null, this.timeout, TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002852 File Offset: 0x00000A52
		protected void StopAndCleanupRpcTimer()
		{
			if (Interlocked.CompareExchange(ref this.completedTimer, 1, 0) == 0)
			{
				this.TimerCleanup();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002878 File Offset: 0x00000A78
		private void TimeoutCallback(object state)
		{
			if (Interlocked.CompareExchange(ref this.completedTimer, 1, 0) == 0)
			{
				base.DeferExceptions<object>(delegate(object unused)
				{
					this.TimerCleanup();
					base.Cancel();
				}, null);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000028AF File Offset: 0x00000AAF
		private void TimerCleanup()
		{
			if (this.timer != null)
			{
				this.timer.Change(-1, -1);
				this.timer.Dispose();
				this.timer = null;
			}
		}

		// Token: 0x0400001D RID: 29
		private readonly TimeSpan timeout;

		// Token: 0x0400001E RID: 30
		private Timer timer;

		// Token: 0x0400001F RID: 31
		private int completedTimer;
	}
}
