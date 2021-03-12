using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B12 RID: 2834
	internal class AsyncResult : IAsyncResult
	{
		// Token: 0x06003D0C RID: 15628 RVA: 0x0009EF2E File Offset: 0x0009D12E
		public AsyncResult(AsyncCallback callback, object asyncState)
		{
			this.stopWatch = Stopwatch.StartNew();
			this.callback = callback;
			this.AsyncState = asyncState;
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x0009EF5C File Offset: 0x0009D15C
		public void Complete(Exception exception, bool completedSynchronously)
		{
			if (!this.IsCompleted)
			{
				this.stopWatch.Stop();
				this.Exception = exception;
				this.CompletedSynchronously = completedSynchronously;
				this.IsCompleted = true;
				this.asyncWaitHandle.Set();
				if (this.callback != null)
				{
					this.callback(this);
				}
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x0009EFB1 File Offset: 0x0009D1B1
		// (set) Token: 0x06003D0F RID: 15631 RVA: 0x0009EFB9 File Offset: 0x0009D1B9
		public Exception Exception { get; private set; }

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06003D10 RID: 15632 RVA: 0x0009EFC2 File Offset: 0x0009D1C2
		// (set) Token: 0x06003D11 RID: 15633 RVA: 0x0009EFCA File Offset: 0x0009D1CA
		public object AsyncState { get; private set; }

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x0009EFD3 File Offset: 0x0009D1D3
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.asyncWaitHandle;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x0009EFDB File Offset: 0x0009D1DB
		// (set) Token: 0x06003D14 RID: 15636 RVA: 0x0009EFE3 File Offset: 0x0009D1E3
		public bool CompletedSynchronously { get; private set; }

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x0009EFEC File Offset: 0x0009D1EC
		// (set) Token: 0x06003D16 RID: 15638 RVA: 0x0009EFF4 File Offset: 0x0009D1F4
		public bool IsCompleted { get; private set; }

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06003D17 RID: 15639 RVA: 0x0009EFFD File Offset: 0x0009D1FD
		public long ElapsedTicks
		{
			get
			{
				return this.stopWatch.ElapsedTicks;
			}
		}

		// Token: 0x04003572 RID: 13682
		private Stopwatch stopWatch;

		// Token: 0x04003573 RID: 13683
		private AsyncCallback callback;

		// Token: 0x04003574 RID: 13684
		private ManualResetEvent asyncWaitHandle = new ManualResetEvent(false);
	}
}
