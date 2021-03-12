using System;
using System.Threading;

namespace Microsoft.Filtering
{
	// Token: 0x02000008 RID: 8
	internal class FipsDataStreamFilteringAsyncResult : IAsyncResult
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002204 File Offset: 0x00000404
		public FipsDataStreamFilteringAsyncResult(Func<AsyncCallback, IAsyncResult> beginOperation, AsyncCallback callback, FipsDataStreamFilteringRequest fipsDataStreamFilteringRequest)
		{
			FipsDataStreamFilteringAsyncResult <>4__this = this;
			this.FipsDataStreamFilteringRequest = fipsDataStreamFilteringRequest;
			this.InnerAsyncResult = beginOperation(delegate(IAsyncResult ar)
			{
				IAsyncResult innerAsyncResult = <>4__this.InnerAsyncResult;
				<>4__this.InnerAsyncResult = ar;
				if (callback != null)
				{
					callback(<>4__this);
				}
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002251 File Offset: 0x00000451
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002259 File Offset: 0x00000459
		public IAsyncResult InnerAsyncResult { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002262 File Offset: 0x00000462
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000226A File Offset: 0x0000046A
		public FipsDataStreamFilteringRequest FipsDataStreamFilteringRequest { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002273 File Offset: 0x00000473
		public bool IsCompleted
		{
			get
			{
				return this.InnerAsyncResult.IsCompleted;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002280 File Offset: 0x00000480
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.InnerAsyncResult.AsyncWaitHandle;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000228D File Offset: 0x0000048D
		public object AsyncState
		{
			get
			{
				return this.InnerAsyncResult.AsyncState;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000229A File Offset: 0x0000049A
		public bool CompletedSynchronously
		{
			get
			{
				return this.InnerAsyncResult.CompletedSynchronously;
			}
		}
	}
}
