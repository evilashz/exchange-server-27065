using System;

namespace Microsoft.Exchange.Net.JobQueues
{
	// Token: 0x0200073F RID: 1855
	[Serializable]
	public class EnqueueResult
	{
		// Token: 0x06002405 RID: 9221 RVA: 0x0004A7B9 File Offset: 0x000489B9
		public EnqueueResult(EnqueueResultType result) : this(result, null)
		{
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0004A7C3 File Offset: 0x000489C3
		public EnqueueResult(EnqueueResultType result, string resultDetail)
		{
			this.Result = result;
			this.ResultDetail = resultDetail;
		}

		// Token: 0x040021E3 RID: 8675
		public static EnqueueResult Success = new EnqueueResult(EnqueueResultType.Successful);

		// Token: 0x040021E4 RID: 8676
		public EnqueueResultType Result;

		// Token: 0x040021E5 RID: 8677
		public string ResultDetail;

		// Token: 0x040021E6 RID: 8678
		public QueueType Type = QueueType.Unknown;
	}
}
