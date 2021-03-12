using System;
using System.Threading;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B5A RID: 2906
	internal sealed class CustomContextAsyncResult : IAsyncResult
	{
		// Token: 0x06003E59 RID: 15961 RVA: 0x000A2C2A File Offset: 0x000A0E2A
		public CustomContextAsyncResult(AsyncCallback originalCallback, object originalState, object customState)
		{
			this.originalState = originalState;
			this.originalCallback = originalCallback;
			this.customState = customState;
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003E5A RID: 15962 RVA: 0x000A2C47 File Offset: 0x000A0E47
		public object AsyncState
		{
			get
			{
				return this.originalState;
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003E5B RID: 15963 RVA: 0x000A2C4F File Offset: 0x000A0E4F
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.InnerAsyncResult.AsyncWaitHandle;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003E5C RID: 15964 RVA: 0x000A2C5C File Offset: 0x000A0E5C
		public bool CompletedSynchronously
		{
			get
			{
				return this.InnerAsyncResult.CompletedSynchronously;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06003E5D RID: 15965 RVA: 0x000A2C69 File Offset: 0x000A0E69
		public bool IsCompleted
		{
			get
			{
				return this.InnerAsyncResult.IsCompleted;
			}
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x000A2C76 File Offset: 0x000A0E76
		internal void CustomCallback(IAsyncResult asyncResult)
		{
			this.InnerAsyncResult = asyncResult;
			if (this.originalCallback != null)
			{
				this.originalCallback(this);
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06003E5F RID: 15967 RVA: 0x000A2C93 File Offset: 0x000A0E93
		internal object CustomState
		{
			get
			{
				return this.customState;
			}
		}

		// Token: 0x04003655 RID: 13909
		private AsyncCallback originalCallback;

		// Token: 0x04003656 RID: 13910
		private object originalState;

		// Token: 0x04003657 RID: 13911
		private object customState;

		// Token: 0x04003658 RID: 13912
		internal IAsyncResult InnerAsyncResult;
	}
}
