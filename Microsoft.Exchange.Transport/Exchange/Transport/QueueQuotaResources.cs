using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000350 RID: 848
	[Flags]
	internal enum QueueQuotaResources : byte
	{
		// Token: 0x04001311 RID: 4881
		SubmissionQueueSize = 1,
		// Token: 0x04001312 RID: 4882
		TotalQueueSize = 2,
		// Token: 0x04001313 RID: 4883
		All = 255
	}
}
