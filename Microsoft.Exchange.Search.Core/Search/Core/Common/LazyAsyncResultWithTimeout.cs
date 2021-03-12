using System;
using System.Threading;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000073 RID: 115
	internal class LazyAsyncResultWithTimeout : LazyAsyncResult, ICancelableAsyncResult, IAsyncResult
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000949A File Offset: 0x0000769A
		internal LazyAsyncResultWithTimeout(object workerObject, object callerState, AsyncCallback callback) : base(workerObject, callerState, callback)
		{
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000094A5 File Offset: 0x000076A5
		public bool IsCanceled
		{
			get
			{
				return base.InternalPeekCompleted && base.Result is OperationCanceledException;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000094BF File Offset: 0x000076BF
		public void Cancel()
		{
			base.InvokeCallback(new OperationCanceledException());
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000094CD File Offset: 0x000076CD
		internal void StartTimer(TimeSpan timeout)
		{
			if (this.timer == null)
			{
				this.timer = new Timer(new TimerCallback(this.TimerCallback));
			}
			this.timer.Change(timeout, LazyAsyncResultWithTimeout.Infinite);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00009500 File Offset: 0x00007700
		internal void TimeoutOperation(object state)
		{
			base.InvokeCallback(new TimeoutException());
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000950E File Offset: 0x0000770E
		internal void DisposeTimer()
		{
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000952A File Offset: 0x0000772A
		protected override void Cleanup()
		{
			this.DisposeTimer();
			base.Cleanup();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00009538 File Offset: 0x00007738
		private void TimerCallback(object state)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.TimeoutOperation));
		}

		// Token: 0x04000131 RID: 305
		private static readonly TimeSpan Infinite = new TimeSpan(-1L);

		// Token: 0x04000132 RID: 306
		private Timer timer;
	}
}
