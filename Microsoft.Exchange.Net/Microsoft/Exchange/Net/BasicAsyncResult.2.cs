using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000B0D RID: 2829
	internal class BasicAsyncResult<TResult> : BasicAsyncResult
	{
		// Token: 0x06003CEA RID: 15594 RVA: 0x0009EA3C File Offset: 0x0009CC3C
		public BasicAsyncResult(AsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
			this.Result = default(TResult);
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x0009EA60 File Offset: 0x0009CC60
		// (set) Token: 0x06003CEC RID: 15596 RVA: 0x0009EA68 File Offset: 0x0009CC68
		public TResult Result { get; set; }

		// Token: 0x06003CED RID: 15597 RVA: 0x0009EA71 File Offset: 0x0009CC71
		public void Complete(TResult result, bool completedSynchronously = false)
		{
			this.Result = result;
			base.Complete(completedSynchronously);
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x0009EA81 File Offset: 0x0009CC81
		public new TResult End()
		{
			base.End();
			return this.Result;
		}
	}
}
