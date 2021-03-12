using System;
using System.Threading;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000007 RID: 7
	internal sealed class SenderIdAsyncResult : IAsyncResult
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002417 File Offset: 0x00000617
		public SenderIdAsyncResult(AsyncCallback asyncCallback, object asyncState)
		{
			this.asyncCallback = asyncCallback;
			this.asyncState = asyncState;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000242D File Offset: 0x0000062D
		public SenderIdAsyncResult(AsyncCallback asyncCallback, object asyncState, object result) : this(asyncCallback, asyncState)
		{
			this.InvokeCompleted(result);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000243E File Offset: 0x0000063E
		public object GetResult()
		{
			if (!this.isCompleted)
			{
				throw new InvalidOperationException();
			}
			return this.result;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002454 File Offset: 0x00000654
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000245C File Offset: 0x0000065C
		public bool CompletedSynchronously
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002463 File Offset: 0x00000663
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000246A File Offset: 0x0000066A
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002472 File Offset: 0x00000672
		public void InvokeCompleted(object invokeResult)
		{
			this.result = invokeResult;
			this.isCompleted = true;
			if (this.asyncCallback != null)
			{
				this.asyncCallback(this);
			}
		}

		// Token: 0x0400000B RID: 11
		private AsyncCallback asyncCallback;

		// Token: 0x0400000C RID: 12
		private object asyncState;

		// Token: 0x0400000D RID: 13
		private bool isCompleted;

		// Token: 0x0400000E RID: 14
		private object result;
	}
}
