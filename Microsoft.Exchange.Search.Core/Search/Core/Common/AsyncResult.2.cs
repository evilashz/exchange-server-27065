using System;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000054 RID: 84
	internal class AsyncResult<TResult> : AsyncResult
	{
		// Token: 0x06000197 RID: 407 RVA: 0x000032CE File Offset: 0x000014CE
		public AsyncResult(AsyncCallback asyncCallback, object state) : base(asyncCallback, state)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000032D8 File Offset: 0x000014D8
		public void SetAsCompleted(TResult result)
		{
			this.result = result;
			base.InvokeCallback();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000032E8 File Offset: 0x000014E8
		public new TResult End()
		{
			base.End();
			return this.result;
		}

		// Token: 0x0400009C RID: 156
		private TResult result;
	}
}
